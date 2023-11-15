<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kelulusan_Viremen_(BendahariNc).aspx.vb" Inherits="SMKB_Web_Portal.Kelulusan_Viremen_Bendahari" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
          
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="divLst" runat="server">
                <div class="row">
                    <div class="panel-default well"  style="width:60%;">
                    <table style="width: 100%;">

                        <tr>
                            <td style="height: 25px; width: 50px;">
                                Tahun
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 150px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                PTj
                            </td>
                            <td>:</td>
                            <td>
                                <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 600px;">
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                          <label id="lblMsgPTJ" class="control-label" for="" style="display: none; color: #820303;">
                              (Pilih PTJ)
                          </label>
                            </td>
                        </tr>
                    </table>
                        </div>
                </div>

                <div class="row">
                    <div class="panel panel-default" style="margin-top: 15px; width: 80%;">
                        <div class="panel-heading">
                            Senarai Permohonan Viremen
                        </div>
                        <div class="panel-body">
                            <div class="GvTopPanel">
                                <div style="float: left; margin-top: 8px; margin-left: 10px;">
                                    <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                                </div>

                            </div>

                            <asp:GridView ID="gvViremen" runat="server" ShowHeaderWhenEmpty="True"
                                AutoGenerateColumns="False" AllowSorting="True" EmptyDataText="Tiada rekod"
                                CssClass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#ffffb3" Font-Size="8pt"
                                BorderColor="#333333" BorderStyle="Solid">
                                <Columns>
                                    <%-- <asp:BoundField DataField="Bil" HeaderText="Bil" SortExpression="Bil" ReadOnly="True">
                                <ItemStyle Width="2.5%" />
                            </asp:BoundField>--%>
                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            <%--<asp:TextBox ID="hidKod" runat="server" Visible ="false" Text='<%# Eval("KodKw") %>'></asp:TextBox>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="10px" />

                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="No. Viremen" DataField="NoViremen" SortExpression="NoViremen" ReadOnly="true">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Tarikh Mohon" DataField="TkhMohon" SortExpression="TkhMohon" ReadOnly="true">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="No. Staf" DataField="NoStaf" SortExpression="NoStaf" ReadOnly="true">
                                        <ItemStyle Width="10%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="KW" DataField="KwF" SortExpression="DrKw" ReadOnly="true">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="KoF" HeaderText="KO">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="PTj" DataField="PtjF" SortExpression="DrPtj" ReadOnly="true">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="KpF" HeaderText="KP">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Objek Sebagai" DataField="ObjSbgF" SortExpression="DrObjSbg" ReadOnly="true">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amaun (RM)" DataField="JumlahF" SortExpression="DrJumlah" ReadOnly="true">
                                        <ItemStyle Width="10%" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="KW" DataField="KwT" ReadOnly="true">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="KoT" HeaderText="KO">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="PTj" DataField="PtjT" ReadOnly="true">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="KpT" HeaderText="KP">
                                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Objek Sebagai" DataField="ObjSbgT" ReadOnly="true">
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Amaun (RM)" DataField="JumlahT" ReadOnly="true">
                                        <ItemStyle Width="10%" HorizontalAlign="Right" />
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

                                <HeaderStyle BackColor="#FFFFB3" />

                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divDt" runat="server" visible="false">

                <div class="row">
                    <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn " Width="180px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali ke Senarai
                    </asp:LinkButton>
                </div>


                <div class="row" style="width: 40%;">
                    <table style="width: 100%;">

                        <tr>
                            <td style="width: 170px;">No. Viremen
                            </td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNoViremen" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Ruj. Surat</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNoRujSurat" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>No. Ruj. Surat Kelulusan</td>
                            <td>:</td>
                            <td>
                                <asp:TextBox ID="txtNoRujLulus" runat="server" CssClass="form-control" Style="width: 200px;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Tarikh Lulus</td>
                            <td>:</td>
                            <td><asp:TextBox ID="txtTkhLulus" runat="server" BackColor="#FFFFCC" contenteditable="false" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="150px"></asp:TextBox>
                                    <button id="btnCal1" class="btnCal">
                                    <i class="far fa-calendar-alt icon-4x"></i>
                                        </button>
                        <cc1:CalendarExtender ID="cal1" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="btnCal1" TargetControlID="txtTkhLulus" TodaysDateFormat="dd/MM/yyyy" />
                        <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTkhLulus" CssClass="text-danger" ErrorMessage="Sila pilih Tarikh Lulus!" ValidationGroup="grpLulus" Display="Dynamic" /></td>
                        </tr>
                    </table>
                </div>

                <div class="panel panel-default" style="margin-top: 15px; width: 1000px;">
                    <div class="panel-heading">
                        Viremen Keluar
                    </div>
                    <div class="panel-body">
                        <table style="width: 100%;">

                            <tr>
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Kumpulan Wang :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodKWF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 50px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtKwF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 400px;"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Kod Operasi :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodKoF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 50px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtKoF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        PTJ :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodPTjF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtPTjF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 600px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Kod Projek :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodKPF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtKPF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Objek Sebagai :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodSbgF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtObjSbgF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 500px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Amaun Keluar (RM) :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAmaunF" runat="server" BackColor="#FFFFCC" ForeColor="#003399" Font-Bold="true" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: right;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Baki Peruntukan (RM) :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBakiF" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: right;"></asp:TextBox>
                                </td>
                            </tr>
                        </table>


                    </div>
                </div>

                <div class="panel panel-default" style="margin-top: 15px; width: 1000px;">
                    <div class="panel-heading">
                        Viremen Masuk
                    </div>
                    <div class="panel-body">
                        <table style="width: 100%;">

                            <tr>
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Kumpulan Wang :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodKWT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 50px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtKWT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 400px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Kod Operasi :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodKoT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 50px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtKoT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        PTJ :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodPTjT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtPTjT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 600px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Kod Projek :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodKpT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtKpT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Objek Sebagai :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKodSbgT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                    &nbsp;-
                          <asp:TextBox ID="txtObjSbgT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 300px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Amaun Masuk (RM) :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAmaunT" runat="server" BackColor="#FFFFCC" ForeColor="#003399" Font-Bold="true" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: right;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 25px">
                                <td style="height: 25px;">
                                    <label class="control-label" for="">
                                        Baki Peruntukan (RM) :
                                    </label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBakiT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: right;"></asp:TextBox>
                                </td>
                            </tr>
                        </table>


                    </div>
                </div>

                <div class="panel panel-default" style="width: 700px;">
                    <div class="panel-heading">Pemohon</div>
                    <div class="panel-body">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 106px; height: 23px;">Nama</td>
                                <td style="height: 23px">:</td>
                                <td style="height: 23px">
                                    <asp:Label ID="lblNoStafPem" runat="server"></asp:Label>
                                    &nbsp;-&nbsp;<asp:Label ID="lblNmPem" runat="server"></asp:Label>
                                    &nbsp;</td>
                                <td style="height: 23px"></td>
                            </tr>
                            <tr>
                                <td style="width: 106px">Jawatan</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblJawPem" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td style="width: 106px">PTj</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblKodPTjPem" runat="server"></asp:Label>
                                    &nbsp;-&nbsp;<asp:Label ID="lblNmPTjPem" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td style="width: 106px">Tarikh Mohon</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblTkhMohon" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                        </table>
                    </div>
                </div>

                <div class="panel panel-default" style="width: 700px;">
                    <div class="panel-heading">Pelulus 1 (Ketua Jabatan)</div>
                    <div class="panel-body">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 106px; height: 23px;">Nama</td>
                                <td style="height: 23px">:</td>
                                <td style="height: 23px">
                                    <asp:Label ID="lblNoStafPel1" runat="server"></asp:Label>
                                    &nbsp;-&nbsp;<asp:Label ID="lblNmStafPel1" runat="server"></asp:Label>
                                    &nbsp;</td>
                                <td style="height: 23px"></td>
                            </tr>
                            <tr>
                                <td style="width: 106px">Jawatan</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblJawPel1" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td style="width: 106px">PTj</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblKodPTjPel1" runat="server"></asp:Label>
                                    &nbsp;-&nbsp;<asp:Label ID="lblNmPTjPel1" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td style="width: 106px">Tarikh Lulus</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblTkhLulus1" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                        </table>
                    </div>
                </div>

                

                <div class="panel panel-default" style="width: 700px;">
                    <div class="panel-heading">Pelulus 2 (Bendahari)</div>
                    <div class="panel-body">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 106px; height: 23px;">Nama</td>
                                <td style="height: 23px">:</td>
                                <td style="height: 23px">
                                    <asp:Label ID="lblNoStafPel2" runat="server"></asp:Label>
                                    &nbsp;-&nbsp;<asp:Label ID="lblNmStafPel2" runat="server"></asp:Label>
                                    &nbsp;</td>
                                <td style="height: 23px"></td>
                            </tr>
                            <tr>
                                <td style="width: 106px">Jawatan</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblJawPel2" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td style="width: 106px">PTj</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblKodPTjPel2" runat="server"></asp:Label>
                                    &nbsp;-&nbsp;<asp:Label ID="lblNmPTjPel2" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td style="width: 106px">Tarikh Lulus</td>
                                <td>:</td>
                                <td>
                                    <asp:Label ID="lblTkhLulus2" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top;">Ulasan</td>
                                <td style="vertical-align: top;">:</td>
                                <td>

                                    <asp:TextBox ID="txtUlasan" runat="server" CssClass="form-control" Height="70px" TextMode="multiline" Width="90%" AutoPostBack="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="txtUlasan" ValidationGroup="grpXLulus"></asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="row">
                    <div style="text-align: center; margin-top: 50px;">
                        <asp:LinkButton ID="lbtnLulus" runat="server" CssClass="btn" ValidationGroup="grpLulus" OnClientClick="return confirm('Anda pasti untuk luluskan permohonan viremen ini?');">
						<i class="fas fa-check-circle"></i>&nbsp;&nbsp;&nbsp;Lulus
                        </asp:LinkButton>

                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="grpLulus" DisplayMode="SingleParagraph" HeaderText="Sila lengkapkan maklumat!" />


                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:LinkButton ID="lbtnXLulus" runat="server" CssClass="btn " ValidationGroup="grpXLulus" OnClientClick="return confirm('Anda pasti untuk tidak meluluskan permohonan viremen ini?');">
						<i class="fas fa-times-circle"></i>&nbsp;&nbsp;&nbsp;Tidak Lulus
                         </asp:LinkButton>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="grpXLulus" DisplayMode="SingleParagraph" HeaderText="Sila masukkan ulasan jika tidak lulus!" />

                    </div>
                </div>
            </div>
                            
            </ContentTemplate>

     </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>

            <div style="background-color: #D2D2D2; filter: alpha(opacity=80); opacity: 0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">
            </div>
            <div style="margin: auto; font-family: Trebuchet MS; filter: alpha(opacity=100); opacity: 1; font-size: small; vertical-align: middle; top: auto; position: absolute; left: auto; color: #FFFFFF; position: fixed; top: 50%; left: 50%; margin-top: -50px; margin-left: -100px;">
                <table>
                    <tr>
                        <td style="text-align: center;">
                            <img src="../../../Images/loader.gif" alt="" />
                        </td>
                    </tr>
                </table>

            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>


</asp:Content>

