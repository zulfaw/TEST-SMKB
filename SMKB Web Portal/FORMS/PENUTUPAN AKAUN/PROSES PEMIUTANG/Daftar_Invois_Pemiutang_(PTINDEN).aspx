<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Daftar_Invois_Pemiutang_(PTINDEN).aspx.vb" Inherits="SMKB_Web_Portal.Daftar_Invois_Pemiutang__PTINDEN_" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <style>
        .ajax__calendar_body 
    {
      height: 175px;
    margin: auto;
    overflow: hidden;
    position: relative;
    width: 210px;
    }


     .ajax__calendar_container{
        cursor: default;
    font-family: tahoma,verdana,helvetica;
    font-size: 11px;
    padding: 4px;
    text-align: center;
    width: 215px;
    }
    </style>

    <script type="text/javascript">
        function Validate() {
            debugger;
            var isValid = false;
            try{
                var valJenInv = document.getElementById("<%=ddlJenInv.ClientID %>").value;

                if (valJenInv == "ILL") {
                    isValid = Page_ClientValidate('grpILL');

                    if (isValid) {
                        var valKat = document.getElementById("<%=ddlKat.ClientID %>").value;
                        if (valKat == "OA") {
                            isValid = Page_ClientValidate('grpOA');
                        }
                        else {
                            isValid = Page_ClientValidate('grpSY');
                        }

                    }              
                }
                else {
                    isValid = Page_ClientValidate('grpIPT');
                }

                if (isValid) {
                    isValid = Page_ClientValidate('grpVal');
                }
                
                Page_BlockSubmit = false;
                return isValid;

            }
            catch (ex) {
                alert(ex)
                return false
            }
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
               <div class="row">
            <div style="text-align:left;margin-bottom:10px;margin-top:20px;">             
                     <asp:LinkButton ID="lbtnListInv" runat="server" CssClass="btn ">
						<i class="fas fa-list fa-lg"></i>&nbsp;&nbsp;&nbsp;Senarai Invois
					</asp:LinkButton>
                </div>
                   </div>


        <div class="row">
         <div class="panel panel-default" style="width:70%;margin-left:0px;">
              <div class="panel-heading">Invois</div>
    <div class="panel-body">
        
        <div class="table-responsive">
            <table class="table" style="width:100%">
                <tr>
                    <td style="width: 10px;">&nbsp;</td>
                    <td style="width: 100px;">ID Invois</td>
                    <td style="width: 10px;">:</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtIdInv" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="true" Style="width: 250px;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10px;">
                        <asp:Label ID="Label31" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                    </td>
                    <td style="width: 100px;">Jenis Invois</td>
                    <td style="width: 10px;">:</td>
                    <td colspan="4">
                        <asp:DropDownList ID="ddlJenInv" runat="server" AutoPostBack="true" CssClass="form-control" Width="350px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trKat" runat="server" visible="false">
                    <td style="width: 10px;">
                        <asp:Label ID="Label32" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                    </td>
                    <td>Kategori</td>
                    <td style="width: 10px;">:</td>
                    <td colspan="4">
                        <asp:DropDownList ID="ddlKat" runat="server" AutoPostBack="true" CssClass="form-control" Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trPT" runat="server">
                    <td style="width: 10px;">
                        <asp:Label ID="Label30" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNoPt" runat="server" Text="No. PT/PB" /></td>
                    <td style="width: 10px;">:</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtNoPT" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="true" Style="width: 250px;"></asp:TextBox>
                        <button id="btnShowMpePT" runat="server" class="btnNone" style="margin-top: -4px;">
                            <i class="fas fa-search"></i>
                        </button>
                        &nbsp;<asp:RequiredFieldValidator runat="server" ControlToValidate="txtNoPT" CssClass="text-danger" ErrorMessage="Sila pilih No. PT/PB!" ValidationGroup="grpIPT" Display="Dynamic"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10px;">
                        <asp:Label ID="Label25" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNoInv" runat="server" Text="No. Invois" /></td>
                    <td style="width: 10px;">:</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtNoInv" runat="server" CssClass="form-control" Style="width: 250px;"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNoInv" CssClass="text-danger" ErrorMessage="Sila masukkan No. Invois!" ValidationGroup="grpIPT" Display="Dynamic" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtNoInv" CssClass="text-danger" ErrorMessage="Sila masukkan No. Invois!" ValidationGroup="grpILL" Display="Dynamic"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label27" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblTkhInv" runat="server" Text="Tarikh Invois" /></td>
                    <td>:</td>
                    <td style="width: 263px">
                        <asp:TextBox ID="txtTkhInv" runat="server" BackColor="#FFFFCC" contenteditable="false" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="150px"></asp:TextBox>
                        <button id="btnCal1" class="btnCal">
                            <i class="far fa-calendar-alt icon-4x"></i>
                        </button>
                        <cc1:CalendarExtender ID="cal1" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="btnCal1" TargetControlID="txtTkhInv" TodaysDateFormat="dd/MM/yyyy" />
                        <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTkhInv" CssClass="text-danger" ErrorMessage="Sila pilih Tarikh Invois!" ValidationGroup="grpIPT" Display="Dynamic" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtTkhInv" CssClass="text-danger" ErrorMessage="Sila pilih Tarikh Invois!" ValidationGroup="grpILL" Display="Dynamic"/>

                    </td>
                    <td style="width: 150px;">
                        <asp:Label ID="Label29" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                        <asp:Label ID="lblTTkhInv" runat="server" Text="Tarikh Terima Invois" /></td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtTkhTInv" runat="server" BackColor="#FFFFCC" contenteditable="false" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="150px"></asp:TextBox>
                        <button id="btnCal2" class="btnCal">
                            <i class="far fa-calendar-alt icon-4x"></i>
                        </button>
                        <cc1:CalendarExtender ID="cal2" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="btnCal2" TargetControlID="txtTkhTInv" TodaysDateFormat="dd/MM/yyyy" />
                        <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtTkhTInv" CssClass="text-danger" ErrorMessage="Sila pilih Tarikh Terima Invois!" ValidationGroup="grpIPT" Display="Dynamic"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtTkhTInv" CssClass="text-danger" ErrorMessage="Sila pilih Tarikh Terima Invois!" ValidationGroup="grpILL" Display="Dynamic"/>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>No. DO</td>
                    <td>:</td>
                    <td colspan="4">

                        <asp:TextBox ID="txtNoDO" runat="server" CssClass="form-control" Style="width: 250px;"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Tarikh DO</td>
                    <td>:</td>
                    <td style="width: 263px">
                        <asp:TextBox ID="txtTkhDO" runat="server" BackColor="#FFFFCC" contenteditable="false" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="150px"></asp:TextBox>
                        <button id="btnCal3" class="btnCal">
                            <i class="far fa-calendar-alt icon-4x"></i>
                        </button>
                        <cc1:CalendarExtender ID="cal3" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="btnCal3" TargetControlID="txtTkhDO" TodaysDateFormat="dd/MM/yyyy" />

                    </td>
                    <td>Tarikh Terima DO</td>
                    <td>:</td>
                    <td>
                        <asp:TextBox ID="txtTkhTDO" runat="server" BackColor="#FFFFCC" contenteditable="false" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="150px"></asp:TextBox>
                        <button id="btnCal4" class="btnCal">
                            <i class="far fa-calendar-alt icon-4x"></i>
                        </button>
                        <cc1:CalendarExtender ID="cal4" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" PopupButtonID="btnCal4" TargetControlID="txtTkhTDO" TodaysDateFormat="dd/MM/yyyy" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Jenis Bayar</td>
                    <td>:</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtJenByr" runat="server" BackColor="#FFFFCC" contenteditable="false" CssClass="form-control" Style="width: 250px;"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        
                      
         </div>
    </div>
        </div>

        <div class="row">         
            <div class="panel panel-default" style="width: 90%; margin-left: 0px;">
                <div class="panel-heading">Penerima </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table" style="width: 100%">
                            <tr>
                                <td style="width: 10px;">&nbsp;</td>
                                <td style="width: 100px;">Kategori</td>
                                <td>:</td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtIdKat" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 60px;"></asp:TextBox>
                                    &nbsp; - &nbsp;<asp:TextBox ID="txtKat" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 250px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="trPPT" runat="server">
                                <td>
                                    <asp:Label ID="Label33" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                </td>
                                <td style="width: 100px;">Nama Penerima</td>
                                <td>:</td>
                                <td colspan="5">
                                    <asp:TextBox ID="txtIDPenerima" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px;"></asp:TextBox>
                                    &nbsp; - &nbsp;<asp:TextBox ID="txtNmPenerima" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 400px;"></asp:TextBox>
                                    &nbsp;&nbsp;
                          <button id="btnShowMpeNom" runat="server" class="btnNone" style="margin-top: -4px;">
                              <i class="fas fa-search"></i>
                          </button>
                                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtIDPenerima" CssClass="text-danger" ErrorMessage="Sila pilih Penerima!" ValidationGroup="grpSY" Display="Dynamic"/>
                                </td>
                            </tr>
                            <div id="trPOA" runat="server" visible=" false">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label34" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                    </td>
                                    <td style="width: 100px;">Nama Penerima</td>
                                    <td>&nbsp;</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtNamaOA" runat="server" CssClass="form-control" Style="width: 400px;"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNamaOA" CssClass="text-danger" ErrorMessage="Sila masukkan Nama Penerima!" ValidationGroup="grpOA" Display="Dynamic"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label35" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                    </td>
                                    <td style="width: 100px;">Kad Pengenalan</td>
                                    <td>&nbsp;</td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtKPOA" runat="server" CssClass="form-control" Style="width: 120px;" MaxLength="12"></asp:TextBox>
                                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNamaOA" CssClass="text-danger" ErrorMessage="Sila masukkan Kad Pengenalan!" ValidationGroup="grpOA" Display="Dynamic"/>
                                    </td>
                                </tr>
                            </div>
                            <tr>
                                <td>
                                    <asp:Label ID="Label36" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                </td>
                                <td style="width: 100px;">Alamat</td>
                                <td>:</td>
                                <td style="width: 500px;">
                                    <asp:TextBox ID="txtAlmt1" runat="server" CssClass="form-control" Style="width: 400px;"></asp:TextBox>
                                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAlmt1" CssClass="text-danger" ErrorMessage="Sila masukkan Alamat 1!" ValidationGroup="grpOA" Display="Dynamic"/>
                                </td>
                                <td>
                                    <asp:Label ID="Label42" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                </td>
                                <td style="vertical-align: top; width: 100px;">Bank</td>
                                <td style="vertical-align: top;">:</td>
                                <td>
                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" Width="450px">
                                    </asp:DropDownList>
                                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" CssClass="text-danger" ControlToValidate="ddlBank" InitialValue="0" ErrorMessage="Sila pilih Bank" ValidationGroup="grpOA" Display="Dynamic"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label37" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>

                                    <asp:TextBox ID="txtAlmt2" runat="server" CssClass="form-control" Style="width: 400px;"></asp:TextBox>
                                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtAlmt2" CssClass="text-danger" ErrorMessage="Sila masukkan Alamat 2!" ValidationGroup="grpOA" Display="Dynamic"/>
                                </td>
                                <td>
                                    <asp:Label ID="Label43" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                </td>
                                <td style="vertical-align: top;">No Akaun</td>
                                <td style="vertical-align: top;">:</td>
                                <td>
                                    <asp:TextBox ID="txtNoAkaun" runat="server" CssClass="form-control" Style="width: 300px;"></asp:TextBox>
                                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtNoAkaun" CssClass="text-danger" ErrorMessage="Sila masukkan No. Akaun!" ValidationGroup="grpOA" Display="Dynamic"/>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label38" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                </td>
                                <td>Bandar</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtBandar" runat="server" CssClass="form-control" Style="width: 300px;"></asp:TextBox>
                                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtBandar" CssClass="text-danger" ErrorMessage="Sila masukkan Bandar!" ValidationGroup="grpOA" Display="Dynamic"/>
                                </td>
                                <td>&nbsp;</td>
                                <td>Jumlah</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtJumInv" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ForeColor="#003399" ReadOnly="True" Style="width: 100px;"></asp:TextBox>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label39" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                </td>
                                <td>Poskod</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtPoskod" runat="server" CssClass="form-control" Style="width: 100px;" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtPoskod" CssClass="text-danger" ErrorMessage="Sila masukkan Poskod!" ValidationGroup="grpOA" Display="Dynamic"/>
                                <td>&nbsp;</td>
                                <td class="">&nbsp;</td>
                                <td class="">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label40" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                </td>
                                <td>Negeri</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlNegeri" runat="server" CssClass="form-control" Width="300px">
                                    </asp:DropDownList>
                                    <br /><asp:RequiredFieldValidator ID="rfv1" runat="server" CssClass="text-danger" ControlToValidate="ddlNegeri" InitialValue="0" ErrorMessage="Sila pilih Negeri" ValidationGroup="grpOA"/>
                                </td>
                                <td>&nbsp;</td>
                                <td class="">&nbsp;</td>
                                <td class="">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label41" runat="server" ForeColor="Red" Text="Label">*</asp:Label>
                                </td>
                                <td>Negara</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlNegara" runat="server" CssClass="form-control" Width="300px">
                                    </asp:DropDownList>
                                    <br /><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" CssClass="text-danger" ControlToValidate="ddlNegara" InitialValue="0" ErrorMessage="Sila pilih Negara" ValidationGroup="grpOA" Display="Dynamic"/>
                                </td>
                                <td>&nbsp;</td>
                                <td class="">&nbsp;</td>
                                <td class="">&nbsp;</td>
                                <td>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>Emel</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtEmel" runat="server" CssClass="form-control" Style="width: 400px;"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td class="">&nbsp;</td>
                                <td class="">&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>       
        </div>

        <div class="row">
            
          <div class="panel panel-default" style="width:80%;margin-left:0px;">
              <div class="panel-heading">Transaksi</div>
    <div class="panel-body">
        
        <div id="divGvPT" runat="server">

            <asp:GridView ID="gvTransPT" runat="server" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                <columns>
                    <asp:TemplateField HeaderText="Bil">
                        <ItemTemplate>
                            <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="20px" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPtDtID" runat="server" Text='<%#Eval("PtDtId")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KW">
                        <ItemTemplate>
                            <asp:Label ID="lblKW" runat="server" Text='<%#Eval("KW")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KO">
                        <ItemTemplate>
                            <asp:Label ID="lblKO" runat="server" Text='<%#Eval("KO")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PTj">
                        <ItemTemplate>
                            <asp:Label ID="lblPTj" runat="server" Text='<%#Eval("PTj")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="KP">
                        <ItemTemplate>
                            <asp:Label ID="lblKP" runat="server" Text='<%#Eval("KP")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vot">
                        <ItemTemplate>
                            <asp:Label ID="lblVot" runat="server" Text='<%#Eval("Vot")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Barangan/Perkara">
                        <ItemTemplate>
                            <asp:Label ID="lblItem" runat="server" Text='<%#Eval("Item")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                    </asp:TemplateField>
                    <%----ASAL-----%>
                    <asp:TemplateField HeaderText="Kuantiti">
                        <ItemTemplate>
                            <asp:Label ID="lblQtyAsal" runat="server" Text='<%#Eval("QtyAsl")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Harga (RM)">
                        <ItemTemplate>
                            <asp:Label ID="lblHargaAsl" runat="server" Text='<%#Eval("HargaAsl")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah (RM)">
                        <ItemTemplate>
                            <asp:Label ID="lblJumAsl" runat="server" Text='<%#Eval("JumAsl")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <%---BELUM------%>
                    <asp:TemplateField HeaderText="Kuantiti">
                        <ItemTemplate>
                            <asp:Label ID="lblQtyBlm" runat="server" Text='<%#Eval("QtyBlm")%>'  />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah (RM)">
                        <ItemTemplate>
                            <asp:Label ID="lblJumBlm" runat="server" Text='<%#Eval("JumBlm")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <%----AKAN-----%>
                    <asp:TemplateField HeaderText="Kuantiti">
                        <ItemTemplate>
                            <asp:TextBox ID="txtQtyByr" runat="server" CssClass="rightAlign" onkeypress="return isNumberKey(event,this)" AutoPostBack="true" OnTextChanged="txtQtyByr_TextChanged" Text='<%#Eval("QtyAkan")%>' Width="98%"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle BackColor="#e8a196" HorizontalAlign="Center" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kaedah">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlKaedah" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlKaedah_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle BackColor="#e8a196" HorizontalAlign="Left" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtPeratus" runat="server" CssClass="rightAlign"  Width="98%" OnTextChanged="txtPeratus_TextChanged" AutoPostBack="true" MaxLength="2"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div style="text-align:right;font-weight:bold;">
                                <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                            </div>
                        </FooterTemplate>
                        <ItemStyle BackColor="#e8a196" HorizontalAlign="Right" Width="100px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah (RM)">
                        <ItemTemplate>
                            <asp:TextBox ID="txtJumAkan" runat="server" CssClass="rightAlign" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("JumAkan")%>' Width="98%" AutoPostBack="true" OnTextChanged="txtJumAkan_TextChanged"></asp:TextBox>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div style="text-align:right;">
                                <asp:Label ID="lblGJumByr" runat="server" ClientIDMode="Static" CssClass="cssGJumByr" Font-Bold="true" Font-Size="9pt" ForeColor="#003399" />
                            </div>
                        </FooterTemplate>
                        <ItemStyle BackColor="#e8a196" HorizontalAlign="Right" Width="100px" />
                        </asp:TemplateField>

                    <asp:TemplateField HeaderText="Baki Peruntukan (RM)">
                        <ItemTemplate>
                            <asp:Label ID="lblBaki" runat="server" Text='<%#Eval("Baki")%>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" Width="100px" />
                        </asp:TemplateField>
                        
                </columns>
                <HeaderStyle BackColor="#FECB18" />
            </asp:GridView>

        </div>
               
        <div id="divGvLain" runat="server" visible="false">
        <asp:GridView ID="gvTrans" runat="server" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
            <columns>
                <asp:TemplateField HeaderText="Bil">
                    <ItemTemplate>
                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                </asp:TemplateField>


                <asp:TemplateField HeaderText="KW">
                    <ItemTemplate>
                        <asp:HiddenField ID="hidKW" runat="server" Value='<%#Eval("KW")%>' />
                        <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px" OnSelectedIndexChanged="ddlKW_SelectedIndexChanged">
                          </asp:DropDownList>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="50px" />                   
                </asp:TemplateField>

                <asp:TemplateField HeaderText="KO">
                    <ItemTemplate>
                         <asp:HiddenField ID="hidKO" runat="server" Value='<%#Eval("KO")%>' />
                        <asp:DropDownList ID="ddlKO" runat="server" AutoPostBack="true" CssClass="form-control" Width="100px" OnSelectedIndexChanged="ddlKO_SelectedIndexChanged">
                          </asp:DropDownList>
                    </ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" Width="50px" />                  
                </asp:TemplateField>

                <asp:TemplateField HeaderText="PTj">
                    <ItemTemplate>
                        <asp:HiddenField ID="hidPTj" runat="server" Value='<%#Eval("PTj")%>' />
                       <asp:DropDownList ID="ddlPTj" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px" OnSelectedIndexChanged="ddlPTj_SelectedIndexChanged">
                          </asp:DropDownList>
                    </ItemTemplate>     
                    <ItemStyle HorizontalAlign="Center" Width="50px" />               
                </asp:TemplateField>

                <asp:TemplateField HeaderText="KP">
                    <ItemTemplate>
                        <asp:HiddenField ID="hidKP" runat="server" Value='<%#Eval("KP")%>' />
                        <asp:DropDownList ID="ddlKP" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px" OnSelectedIndexChanged="ddlKP_SelectedIndexChanged">
                          </asp:DropDownList>
                    </ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" Width="50px" />                  
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Vot">
                    <ItemTemplate>
                        <asp:HiddenField ID="hidVot" runat="server" Value='<%#Eval("Vot")%>' />
                       <asp:DropDownList ID="ddlVot" runat="server" AutoPostBack="true" CssClass="form-control" Width="150px">
                          </asp:DropDownList>
                    </ItemTemplate>  
                    <ItemStyle HorizontalAlign="Center" Width="50px" />                  
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Butiran">
                    <ItemTemplate>
                        <asp:TextBox ID="txtButiran" runat="server"  CssClass="form-control" style="width: 100%;" Text='<%#Eval("Butiran")%>'></asp:TextBox>
                    </ItemTemplate>                       
                    <ItemStyle HorizontalAlign="Left" Width="300px" />           
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Harga/Unit (RM)">
                    <ItemTemplate>
                         <asp:TextBox ID="txtHarga" runat="server"  CssClass="form-control rightAlign" style="width: 100%;" Text='<%#Eval("Harga")%>' AutoPostBack="true" OnTextChanged="txtHarga_TextChanged" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                    </ItemTemplate>                    
                    <ItemStyle HorizontalAlign="Right" Width="150px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Kuantiti">
                    <ItemTemplate>
                         <asp:TextBox ID="txtKttByr" runat="server"  CssClass="form-control rightAlign" style="width: 100%;" Text='<%#Eval("Kuantiti")%>' AutoPostBack="true" OnTextChanged="txtKttByr_TextChanged" onkeypress="return isNumberKey(event,this)"></asp:TextBox>
                    </ItemTemplate>
                     <FooterTemplate>
                     <div style="text-align:right;font-weight:bold;">
                        <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                      </div>
                   </FooterTemplate>    
                    <ItemStyle HorizontalAlign="Center" Width="100px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Jumlah (RM)">
                    <ItemTemplate>
                        <%--<asp:TextBox ID="txtJumlah" runat="server"  CssClass="form-control rightAlign" style="width: 100%;" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("Jumlah")%>' OnTextChanged="txtJumlah_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                        <asp:Label ID="lblJumlah" runat="server" Text='<%#Eval("Jumlah")%>' />
                    </ItemTemplate>   
                    <FooterTemplate>
                        <div style="text-align:right;">
                            <asp:Label ID="lblTotJum" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true"/>
                        </div>
                    </FooterTemplate> 
                    <ItemStyle HorizontalAlign="Right" Width="150px"/>                     
                </asp:TemplateField>   
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Delete" CssClass="btn-xs" ToolTip="Hapus rekod">
                            <i class="fas fa-trash-alt fa-lg"></i>
                        </asp:LinkButton>                      
                        </ItemTemplate>
                    <FooterTemplate>                       
                        <asp:LinkButton ID="lbtnTambah" runat="server" CausesValidation="true" CssClass="btn " OnClick="lbtnTambah_Click" ToolTip="Tambah rekod" Width="50px">
                        <i class="fas fa-plus fa-lg"></i>
					    </asp:LinkButton>
                    </FooterTemplate> 
                     <ItemStyle HorizontalAlign="left" Width="50px"/>   
                    </asp:TemplateField>      
            </columns>
            <HeaderStyle BackColor="#FECB18" />
        </asp:GridView>
                
            <div style="text-align:right;margin-bottom:10px;margin-top:20px;">
                </div>
        </div>

        </div>           
              </div>
        </div>
    
        <div class="row">
            <div style="text-align:center;margin-bottom:10px;margin-top:20px;">             
                     <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " CausesValidation="true" OnClientClick="return Validate()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
                &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk hapus rekod ini?');" Visible="False">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                                                    </asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:LinkButton ID="lbtnRekBaru" runat="server" CssClass="btn ">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
					</asp:LinkButton>
                                </div>
                 </div>

            <asp:Button ID="btnPopup3" runat="server" style="display:none;"   />                
                    <ajaxToolkit:ModalPopupExtender ID="mpePnlListNom" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PnlListNom" TargetControlID="btnPopup3" CancelControlID="Button4">
                                     </ajaxToolkit:ModalPopupExtender>  
                     
            <asp:Panel ID="PnlListNom" runat="server" BackColor="White" Width="800px" Style="display: none;">
                <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                        <td style="height: 10%; text-align: center;" class="">
                            <asp:Label ID="lblTitle" runat="server" Font-Bold="true" /></td>
                        <td style="width: 50px; text-align: center;">
                            <button runat="server" id="Button4" title="Tutup" class="btnNone ">
                                <i class="far fa-window-close fa-2x"></i>
                            </button>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center;">
                            <div class="row" style="margin-top: 10px;">

                                <div id="tblSY" runat="server" visible="false">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="width: 50px">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50px">Carian</td>
                                            <td>:</td>
                                            <td style="width: 150px">
                                                <asp:DropDownList ID="ddlCariSY" runat="server" CssClass="form-control" Width="200px">
                                                    <asp:ListItem Text="ID Syarikat" Value="0" Selected="true"></asp:ListItem>
                                                    <asp:ListItem Text="Nama Syarikat" Value="1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp;
                                        <asp:TextBox ID="txtCariSY" runat="server" CssClass="form-control" Style="width: 300px;"></asp:TextBox>
                                                &nbsp;&nbsp;
                                        <button id="btnCariSY" runat="server" class="btnNone" style="margin-top: -4px;">
                                            <i class="fas fa-search"></i>
                                        </button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="gvSY" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid"
                                                    CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" PageSize="10">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Bil">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ID Syarikat">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIDSya" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("IDSya"))) %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Nama Syarikat">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNamaSya" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("NamaSya"))) %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                                        <i class="fas fa-edit"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#6699FF" />
                                                </asp:GridView>

                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <div id="tblST" runat="server" visible="false">
                                    <table style="width: 100%; text-align: left;">
                                        <tr>
                                            <td style="width: 50px">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50px">Carian</td>
                                            <td>:</td>
                                            <td style="width: 150px">
                                                <asp:DropDownList ID="ddlCariST" runat="server" CssClass="form-control" Width="200px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp;
                                        <asp:TextBox ID="txtCariST" runat="server" CssClass="form-control" Style="width: 300px;"></asp:TextBox>
                                                &nbsp;&nbsp;
                                        <button id="btnCariST" runat="server" class="btnNone" style="margin-top: -4px;">
                                            <i class="fas fa-search"></i>
                                        </button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="gvST" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid"
                                                    CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" PageSize="10">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Bil">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="No Staf">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNoStaf" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("NoStaf"))) %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" Width="50px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Nama Staf">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNamaStaf" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("NamaStaf"))) %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPejabat" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("Pejabat"))) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("Email"))) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBank" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("Bank"))) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNoAkaun" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("NoAkaun"))) %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                                        <i class="fas fa-edit"></i>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="20px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#6699FF" />
                                                </asp:GridView>

                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                        </td>
                    </tr>
                </table>
            </asp:Panel>

                <asp:Button ID="btnPopup2" runat="server" style="display:none;"   />                
                    <ajaxToolkit:ModalPopupExtender ID="mpePnlNoPT" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PnlNoPT" TargetControlID="btnPopup2" CancelControlID="Button3">
                                     </ajaxToolkit:ModalPopupExtender>                       
            <asp:Panel ID="PnlNoPT" runat="server" BackColor="White" Width="800px" style="display:none;">
               
                <table  style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                         <td style="height: 10%;text-align:center;" class="">
                            <b> Senarai Pesanan Tempatan</b></td>
                        <td style="width:50px;text-align:center;">   
                           <button runat="server" id="Button3" title="Tutup" class="btnNone ">
    <i class="far fa-window-close fa-2x"></i>
</button></td>
                                                    
                        
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                            <div class="row" style="margin-top:10px;">
                                    <table style="width:100%;text-align:left;">
                                <tr>
                                    <td style="width:50px">&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                     </td>
                                </tr>
                                <tr>
                                    <td style="width:50px">Carian</td>
                                    <td>:</td>
                                    <td style="width:150px">
                                        <asp:DropDownList ID="ddlCarian" runat="server" CssClass="form-control" Width="200px">
                                        </asp:DropDownList>                                      
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:TextBox ID="txtCarian" runat="server" CssClass="form-control" style="width: 300px;"></asp:TextBox>
                                        &nbsp;&nbsp;
                                        <button id="btnCariPt" runat="server" class="btnNone" style="margin-top:-4px;">
                                            <i class="fas fa-search"></i>
                                        </button>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:GridView ID="gvPT" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" 
                                            cssclass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                            <columns>
                                                <asp:TemplateField HeaderText="Bil">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBil1" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" />
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPtID" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("PtId"))) %>' />
                                                    </ItemTemplate>                                                   
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No PT/PB">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoPT" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("NoPT"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Tarikh PT/PB">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTkhPT" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("TkhPT"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="ID Syarikat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoSya" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("IDSya"))) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20%" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Nama Syarikat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTkhPT" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("TkhPT"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="Left" Width="110px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNamaSya0" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("NamaSya"))) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="20%" />
                                                </asp:TemplateField>
                                                                                     
                                                 <asp:TemplateField HeaderText="Jum. Asal (RM)">
                                                     <ItemTemplate>
                                                         <asp:Label ID="lblJumAsal" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("JumAsal"))) %>' />
                                                     </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Right" Width="20%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Jum. Belum Bayar (RM)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJumBB" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("JumBB"))) %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="20%" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField Visible ="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFlagFIR" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("FlagFIR"))) %>' />
                                                    </ItemTemplate>                                                                                                  
                                                </asp:TemplateField>--%>
                                                                                     
                                                 <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect0" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                                        <i class="fas fa-edit"></i>
                                                    </asp:LinkButton>                                                                            
                                                <%-- &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
                                                <asp:LinkButton ID="lbtnDetail" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Detail" CssClass="btn-xs" ToolTip="Butiran">
                                                        <i class="fas fa-ellipsis-h"></i>
                                                    </asp:LinkButton>--%>                                                                            
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>
                                            </columns>
                                            <HeaderStyle BackColor="#6699FF" />
                                        </asp:GridView>
                                                                             
                                    </td>
                                </tr>
                            </table>                                                                                                                                                          
                            </div>

                        </td>
                    </tr>                           
          </table> 
                
            </asp:Panel>

            <asp:Button ID="btnPopup1" runat="server" style="display:none;"   />                
                    <ajaxToolkit:ModalPopupExtender ID="mpeLstInv" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PnlLstInv" TargetControlID="btnPopup1" CancelControlID="Button2">
                                     </ajaxToolkit:ModalPopupExtender>                       
            <asp:Panel ID="PnlLstInv" runat="server" BackColor="White" Width="800px" style="display:none;">
               
                <table  style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                         <td style="height: 10%;text-align:center;" class="">
                            <b> Senarai Invois</b></td>
                        <td style="width:50px;text-align:center;">   
                           <button runat="server" id="Button2" title="Tutup" class="btnNone ">
    <i class="far fa-window-close fa-2x"></i>
</button></td>
                                                                            
                    </tr>
                    <tr>
                        <td colspan="2" >
                            <div class="row" style="margin-top:10px;width:100%;">
                                   
                                <table style="width: 100%;">
                  <tr>
                      <td style="width:100px;">Jenis Invois</td>
                      <td style="width:10px;">:</td>
                      <td>                        
                         <asp:DropDownList ID="ddlTapisan" runat="server" CssClass="form-control">
                          </asp:DropDownList>                
                      </td>
                  </tr>
                                    <tr>
                                        <td >&nbsp;</td>
                                        <td >&nbsp;</td>
                                        <td style="height:45px;">
                                            <asp:LinkButton ID="lnkBtnCari" runat="server" CssClass="btn " ToolTip="Simpan" Width="80px">
						<i class="fas fa-search fa-lg"></i> &nbsp;&nbsp; Cari
					</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            
                                        </td>
                                    </tr>
                 </table>

<div style="margin-top:20px;">
                  <asp:GridView ID="gvLstInv" runat="server" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" " 
       cssclass="table table-striped table-bordered table-hover" Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True" PageSize="15"  >
                                 <columns> 
                                                                    
                    <asp:TemplateField HeaderText = "Bil">
                        <ItemTemplate>
                            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                    </asp:TemplateField>      
                                        
                                     <asp:TemplateField Visible="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblIdInv" runat ="server" text="ID Invois" />&nbsp;
                            <asp:LinkButton id="lnkIdInv" runat="server" CommandName="Sort" CommandArgument="IdInv"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblIdInv" runat ="server" text='<%# Eval("IdInv")%>' />
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>
                         
                     <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblNmNom" runat ="server" text="Nama Penerima" />&nbsp;
                            <asp:LinkButton id="lnkNPembekal" runat="server" CommandName="Sort" CommandArgument="NPembekal"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNmNom" runat ="server" text='<%# Eval("NmNom")%>' />
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Width="25%" />
                     </asp:TemplateField>
                                                                           
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblNoInv" runat ="server" text="No. Invois" />&nbsp;
                            <asp:LinkButton id="lnkNoInv" runat="server" CommandName="Sort" CommandArgument="NoInv"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNoInv" runat ="server" text='<%# Eval("NoInv")%>' />
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>

                   <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblNoDO" runat ="server" text="No. DO" />&nbsp;
                            <asp:LinkButton id="lnkNoDO" runat="server" CommandName="Sort" CommandArgument="NoDO"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNoDO" runat ="server" text='<%# Eval("NoDO")%>' />
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>
                                     
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblNoPT" runat ="server" text="No. PT/PB" />&nbsp;
                            <asp:LinkButton id="lnkNoPT" runat="server" CommandName="Sort" CommandArgument="NoPT"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblNoPT" runat ="server" text='<%# Eval("NoPT")%>' />
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>
                                                      
                     <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblJenInv" runat ="server" text="Jenis Invois" />&nbsp;
                            <asp:LinkButton id="lnkJenInv" runat="server" CommandName="Sort" CommandArgument="JenInv"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblJenInv" runat ="server" text='<%# Eval("JenInv")%>' />
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                     </asp:TemplateField>

                     <asp:BoundField HeaderText="Jumlah Bayar (RM)" DataField="Jumlah">
                         <ItemStyle HorizontalAlign="Right" Width="10%" ForeColor="#003399" />
                     </asp:BoundField>    
                                     
                     <asp:TemplateField Visible ="false">                        
                        <ItemTemplate>
                            <asp:Label ID="lblPadan" runat ="server" Text='<%# Eval("Padan")%>' />
                         </ItemTemplate>
                         <ItemStyle HorizontalAlign="Left" Width="25%" />
                     </asp:TemplateField>                                
                        
                     <asp:TemplateField>
                        <ItemTemplate>
                          <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Maklumat Lanjut">
                              <i class="fas fa-edit"></i>                           
                          </asp:LinkButton>                                                                                                                              
                        </ItemTemplate>
                        <ItemStyle Width="5px" />
                     </asp:TemplateField>

                                </columns>

<HeaderStyle BackColor="#6699FF"></HeaderStyle>
                            </asp:GridView>
     </div>                                                                                                                                                                          
                            </div>

                        </td>
                    </tr>                           
          </table> 
                
            </asp:Panel>
            <asp:HiddenField runat="server" ID="hidJenByr"></asp:HiddenField>
            </ContentTemplate>
     </asp:UpdatePanel>
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

