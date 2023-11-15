<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Daftar_Kawalan_KW.aspx.vb" Inherits="SMKB_Web_Portal.Daftar_Kawalan_KW" %>
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
                                <td style="height: 22px;width:117px;">
                                    Tahun Permohonan</td>
                                <td style="height: 22px;width:10px;">:</td>
                                <td>
                                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKodDsr" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSave" Display="Dynamic" ></asp:RequiredFieldValidator>--%><label id="lblMsgKodDsr" class="control-label" for="" style="display: none; color: #820303;">(Masukkan Kod Dasar)
                                    </label>
                                    <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-html="true" data-placement="right" data-toggle="tooltip" title="&nbsp;Masukkan 3 aksara.">
                                    <asp:DropDownList ID="ddlBahagian1" runat="server" AutoPostBack="True" CssClass="form-control" Height="21px" Width="10%">
                                    </asp:DropDownList>
                                    </i>
                                </td>
                            </tr>

                            <tr style="height: 25px">
                                <td style="height: 22px; vertical-align: top; width: 117px;">
                                    <label class="control-label" for="">Butiran</label>
                                </td>
                                <td style="height: 22px; vertical-align: top;">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlBahagian0" runat="server" AutoPostBack="True" CssClass="form-control" Height="21px" Width="30%">
                                    </asp:DropDownList>
                                    &nbsp;<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtButiranDsr" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSave" Display="Dynamic" ></asp:RequiredFieldValidator>--%><label id="lblMsgButiran" class="control-label" for="" style="display: none; color: #820303;">(Masukkan Butiran Dasar)
                                    </label>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 22px; vertical-align: top; width: 117px;">Tarikh Mula</td>
                                <td style="height: 22px; vertical-align: top;">&nbsp;</td>
                                <td>
                                    <asp:TextBox ID="txtTarikhMesyuarat" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="101px"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTarikhMesyuarat" TodaysDateFormat="dd/MM/yyyy" />
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 22px; vertical-align: top; width: 117px;">Status</td>
                                <td style="height: 22px; vertical-align: top;">:</td>
                                <td>
                                    <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                        <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                                        <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>

                            <tr style="height: 55px; vertical-align: bottom">
                                <td style="width: 117px">&nbsp;</td>
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

                <div style="width: 800px;margin-left: 20px;">
                    <div class="GvTopPanel">
                        <div style="float: left; margin-top: 8px; margin-left: 10px;">
                            <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                        </div>
                    </div>
                    <asp:GridView ID="gvDasar" runat="server" 
                        AllowPaging="True" AllowSorting="True" 
                        AutoGenerateColumns="False" EmptyDataText=" "
                        BorderColor="#333333" BorderStyle="Solid" 
                        CssClass="table table-striped table-bordered table-hover" 
                        Font-Size="8pt" PageSize="25" ShowHeaderWhenEmpty="True" 
                        Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Bil">
                                <ItemTemplate>
                                    <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="" HeaderText="Tahun Permohonan" ReadOnly="True" >
                                <ControlStyle Width="10px" />
                                <ItemStyle Width="20%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="Peringkat">
                                <ControlStyle Width="50%" />
                                <ItemStyle Width="50%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="Tarikh Mula">
                                <ControlStyle Width="50%" />
                                <ItemStyle Width="50%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="" HeaderText="Tarikh Tamat">
                                 <ControlStyle Width="50%" />
                                <ItemStyle Width="50%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status">
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih">
                                            <i class="fas fa-edit"></i>
                                    </asp:LinkButton>
                                </ItemTemplate>

                                <ItemStyle Width="5%" />
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
