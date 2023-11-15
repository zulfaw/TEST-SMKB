<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Capaian_Individu.aspx.vb" EnableEventValidation="False" Inherits="SMKB_Web_Portal.Capaian_Individu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Capaian Individu</h1>

    <script type="text/javascript">
        function fCloseStaf() {
            $find("mpe3").hide();
        }
        </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>          


                <div class="row">
                    
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <table style="width: 100%;" class="table table table-borderless">
                                    <tr>
                                        <td>
                                            <label class="control-label" for="Butiran">No. Staf </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNoStaf" runat="server" class="form-control" ReadOnly="True" Width="100px"></asp:TextBox>
                                            <%--<asp:ImageButton ID="btnSearch" runat="server" CommandName="Search" Height="20px" ImageUrl="~/Images/find.png" ToolTip="Carian " Width="20px" />--%>
                                            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-info">
					            <i class="fas fa-search fa-lg"></i>&nbsp;&nbsp;&nbsp;Cari
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>
                                            <label class="control-label" for="Butiran">Nama Staf </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNamaStaf" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label" for="Butiran">
                                                Jawatan
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtJawatan" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="100%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Modul"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlKodModul" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" Text="Sub Modul"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlKodSubModul" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label class="control-label" for="">
                                            Sub Menu:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlKodSubMenu" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvKodSubMenu" runat="server" ControlToValidate="ddlKodSubMenu" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                     <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Jenis Capaian" Style="width: 20%;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlJenisCapaian" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 130px;"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvDdlJnsCapaian" runat="server" ControlToValidate="ddlJenisCapaian" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnSave" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trTarikh" runat="server">
                            <td>
                                <asp:Label ID="Label7" runat="server" Text="Tarikh Mula" Style="width: 20%;"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control centerAlign" Text="<%# System.DateTime.Now.ToShortDateString() %>" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtStartDate" TodaysDateFormat="dd/MM/yyyy" />
                                <asp:RequiredFieldValidator ID="rfvTarikhMula" runat="server" ControlToValidate="txtStartDate" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSave" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>

                                &nbsp;&nbsp;&nbsp;<label class="control-label" for="">Tarikh Tamat:</label>
                                &nbsp;&nbsp;&nbsp;
									<asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control centerAlign" Text="<%# System.DateTime.Now.AddDays(1).ToShortDateString() %>" Width="100px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtEndDate" TodaysDateFormat="dd/MM/yyyy" />
                                <asp:RequiredFieldValidator ID="rfvTarikhAkhir" runat="server" ControlToValidate="txtEndDate" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSave" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Fungsi Dibenarkan" Style="width: 20%;"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFungsi" runat="server" AutoPostBack="True" CssClass="form-control centerAlign" Style="width: 150px;"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvKod" runat="server" ControlToValidate="ddlFungsi" ErrorMessage="" ForeColor="#820303" Text="*Sila pilih" ValidationGroup="btnSave" Display="Dynamic" CssClass="fontValidatation"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div class="row">
                        <div style="text-align: center">
                            <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
						            <i class="fas fa-sync-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
                            </asp:LinkButton>
                            &nbsp;&nbsp;&nbsp;
					            <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" ValidationGroup="btnSimpan">
						            <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                </asp:LinkButton>
                            <asp:LinkButton ID="lbtnKemaskini" runat="server" CssClass="btn btn-info" ToolTip="Kemaskini" ValidationGroup="btnSimpan">
                                    <i class="fas fa-edit fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemaskini
                            </asp:LinkButton>
                            &nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnUndo" runat="server" CssClass="btn btn-info" ToolTip="Kemaskini">
                                    <i class="fas fa-undo fa-lg"></i>&nbsp;&nbsp;&nbsp;Undo
                                </asp:LinkButton>
                        </div>
                    </div>


                        <br />
                        <div class="GvTopPanel" style="height: 33px;">
                        <div style="float: left; margin-top: 5px; margin-left: 10px;">
                            Jumlah rekod :&nbsp;
                            <asp:Label ID="lblJumRekodS" runat="server" style="color: mediumblue;" ></asp:Label>

                            &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                            Saiz Rekod : 
                            <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                <asp:ListItem Text="25" Value="25" Selected="True" />
                                <asp:ListItem Text="50" Value="50" />
                                <asp:ListItem Text="100" Value="100" />
                                <asp:ListItem Text="500" Value="500" />
                            </asp:DropDownList>
                            <%-- &nbsp;&nbsp;
                           Cari : 
                           &nbsp;&nbsp;
                               <asp:TextBox ID="txtCariStaf" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCariStaf" runat="server" ControlToValidate="txtCariStaf" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnCariStaf"></asp:RequiredFieldValidator>
                            &nbsp;
                               <asp:DropDownList ID="ddlCariStaf" runat="server" Width="150px" class="form-control">
                                   <asp:ListItem Text="Kod Submenu" Value=1 Selected="True" />
                                   <asp:ListItem Text="Nama Submenu" Value=2 />
                                   <asp:ListItem Text="Kod Tahap" Value=3 />
                               </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCariStaf" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariStaf">
                        <i class="fa fa-search fa-lg" aria-hidden="true"></i>
                               </asp:LinkButton>--%>
                        </div>
                    </div>

                    <asp:GridView ID="gvTahapPengguna" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" PageSize="25"
                        CssClass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt"  BorderStyle="Solid" EmptyDataText=" ">
                        <Columns>
                            <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" SortExpression="Bil" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Kod Sub Menu" DataField="KodSubMenu" SortExpression="KodSubMenu" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Nama Sub Menu" DataField="NamaSubMenu" SortExpression="NamaSubMenu" />
                            <asp:BoundField HeaderText="No Staf" DataField="nostaf" SortExpression="nostaf" ItemStyle-HorizontalAlign="Center"/>
                            <asp:TemplateField SortExpression="JenCapai" HeaderText="Jenis Capaian">
                                <ItemTemplate>
                                    <asp:Label ID="lblJnsCapaian" runat="server" Text='<%#IIf(Eval("JenCapai") = "U", "U - TIADA HAD", "L - TERHAD") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField HeaderText="Jenis Capaian" DataField="JenisCapaian" SortExpression="JenisCapaian" />--%>
                            <asp:BoundField HeaderText="Tarikh Mula" DataField="TkhMula" SortExpression="TkhMula" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tarikh Tamat" DataField="TkhTamat" SortExpression="TkhTamat" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField SortExpression="Fungsi" HeaderText="Fungsi Dibenarkan">
                                <ItemTemplate>
                                    <asp:Label ID="lblFungsi" runat="server" Text='<%#IIf(Eval("Fungsi") = "R", "R - BACA SAHAJA", "W - BACA & TULIS") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="Fungsi" HeaderText="Fungsi Dibenarkan" SortExpression="Fungsi" />--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
						<i class="fas fa-edit fa-lg"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                        OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
						<i class="fas fa-trash-alt fa-lg"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="6%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle ForeColor="#0000FF" />
                    </asp:GridView>
                    </div>

                </div>


            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID="btnOpenStaf" runat="server" Text="" Style="display: none" />
    <ajaxToolkit:ModalPopupExtender ID="MPEStaf" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe3"
        CancelControlID="btnCloseStaf" PopupControlID="pnlStaf" TargetControlID="btnOpenStaf">
    </ajaxToolkit:ModalPopupExtender>

    <asp:Panel ID="pnlStaf" runat="server" BackColor="White" Width="70%" Height="70%" Style="overflow: auto;">
        <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">

                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td colspan="2" style="height: 10%; font-weight: bold; text-align: center;">Maklumat Lanjut </td>
                        <td style="width: 10px; text-align: center;">
                            <button id="btnCloseStaf" runat="server" class="btnNone " title="Tutup" onclick="fCloseStaf();">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>

                    <tr style="height: 30px;">
                        <td colspan="3">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodPend" runat="server" class="control-label" style="color: mediumblue;"></label>
                            &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekodStaf" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true" />
                                   <asp:ListItem Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>
                            &nbsp;&nbsp;
                           Cari : 
                           &nbsp;&nbsp;
                               <asp:TextBox ID="txtCariStaf" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCariStaf" runat="server" ControlToValidate="txtCariStaf" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnCariStaf"></asp:RequiredFieldValidator>
                            &nbsp;
                               <asp:DropDownList ID="ddlCariStaf" runat="server" Width="150px" class="form-control">
                                   <asp:ListItem Text="Nama" Value="1" Selected="True" />
                                   <asp:ListItem Text="No Staf" Value="2" />
                                   <asp:ListItem Text="PTJ" Value="3" />
                               </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCariStaf" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariStaf">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                               </asp:LinkButton>
                        </td>
                    </tr>


                    <tr style="vertical-align: top;">

                        <td colspan="3">
                            <div style="margin-bottom: 10px; margin-top: 10px; margin-left: 10px; margin-right: 10px;">
                                <asp:GridView ID="gvstaf" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" DataKeyNames="MS01_NoStaf" PageSize="25">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                        
                                        <asp:BoundField HeaderText="No Staf" DataField="MS01_NoStaf" SortExpression="MS01_NoStaf"/>
                                        <asp:BoundField HeaderText="Nama" DataField="MS01_Nama" SortExpression="MS01_Nama" />
                                        <asp:BoundField HeaderText="PTJ" DataField="Pejabat" SortExpression="Pejabat" />
                                        <asp:BoundField HeaderText="Jawatan" DataField="JawGiliran" SortExpression="JawGiliran" />                                        
                                        <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>

                </table>

            </ContentTemplate>

        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>

