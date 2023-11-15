<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pemfaktoran.aspx.vb" Inherits="SMKB_Web_Portal.Pemfaktoran" EnableEventValidation ="False" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div class="row">

                <div class="panel-group">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="panel panel-default" >
                        <div class="panel-heading"><h4 class="panel-title">Tapisan</h4></div>
                        <div class="panel-body">
                 <table style="width: 100%;">
                    
                     <tr style="height:25px">
                      <td style="width: 100px; height: 22px;">Kategori</td>
                      <td>:&nbsp<asp:DropDownList ID="ddlKategori" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 200px;">
                          <asp:ListItem Selected="True" Value="0">Sila Pilih</asp:ListItem>      
                          <asp:ListItem  Value="1">No PT</asp:ListItem>
                            <asp:ListItem Value="2">Pembekal</asp:ListItem>
                            <asp:ListItem Value="3">Tarikh</asp:ListItem>
                          </asp:DropDownList>
                      </td>
                  </tr>
                    <tr id="trPembekal" runat="server">
                    <td style="width: 100px; height: 22px;">Pembekal</td>
                    <td>:&nbsp<asp:DropDownList ID="ddlKodNiaga" runat="server" AutoPostBack="True" CssClass="form-control" Width="30%"></asp:DropDownList>
                        &nbsp<asp:DropDownList ID="ddlPembekal" runat="server" AutoPostBack="True" CssClass="form-control" Width="60%"></asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="rqvKodNiaga" runat="server" ControlToValidate="ddlPembekal" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnTambah" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>                         
                    </td>
			        </tr>

                     <tr id="trTarikh" runat="server">
                    <td style="width: 100px; height: 22px;">Tarikh Dari</td>
                    <td>:&nbsp<asp:TextBox ID="txtDari" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                         <ajaxtoolkit:CalendarExtender ID="calDari" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDari" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnDari"/>
                        <asp:LinkButton ID="lbtnDari" runat="server" ToolTip="Klik untuk papar kalendar">
                                <i class="far fa-calendar-alt fa-lg"></i>
                            </asp:LinkButton>
                         &nbsp<Label class="control-label" for="">Hingga</Label>
                        &nbsp<asp:TextBox ID="txtHingga" runat="server" CssClass="form-control" Width="100px" ></asp:TextBox>
                        <ajaxtoolkit:CalendarExtender ID="calHingga" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtHingga" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtnHigga"/>
                       <asp:LinkButton ID="lbtnHingga" runat="server" ToolTip="Klik untuk papar kalendar">
                                <i class="far fa-calendar-alt fa-lg"></i>
                            </asp:LinkButton>
                    </td>
			        </tr>
                    <tr style="height:25px" id="trNoPT" runat="server">
                    <td style="width: 100px; height: 22px;">No PT /Inden/ SST</td>
                    <td> :&nbsp<asp:DropDownList ID="ddlNoPT" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 200px;">
                        </asp:DropDownList>
                    </td>
                  </tr>
                   <tr>
                    <td>&nbsp</td>
                    <td style="height:40px">
                    <asp:LinkButton ID="lbtnPapar" runat="server" CssClass="btn btn-info">
                        <i class="fa fa-folder-open-o"></i>&nbsp;&nbsp;&nbsp;Papar
                    </asp:LinkButton>                    &nbsp &nbsp
                   	</td>
                </tr>
                  </table>
          
            </div>
                         </div>

                        <div class="panel panel-default" style="width:90%">
                        <div class="panel-body">
                            <table style="width:100%" class="table table table-borderless">
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">No PT</Label></td>
                                <td>
                                    <asp:TextBox ID="txtNoPT" runat="server" Width="150px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    &nbsp &nbsp &nbsp &nbsp
                                    <Label class="control-label" for="">Tarikh PT</Label>
                                    &nbsp &nbsp
                                    <asp:TextBox ID="txtTarikhPT" runat="server" Width="20%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">No Daftar Syarikat</Label></td>
                                <td>
                                    
                                    <asp:TextBox ID="txtNoDaftarSyarikat" runat="server"  Width="150px" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtNoIDSya" runat="server"  Width="100px" ReadOnly="true" CssClass="form-control" Visible="false"></asp:TextBox>
                                </td>
                            </tr>                          
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Nama Syarikat</Label></td>
                                <td>
                                    <asp:TextBox ID="txtNamaSyarikat" runat="server" Width="80%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </td>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Alamat</Label></td>
                               <td>
                                    <asp:TextBox ID="txtAlamatAsal1" runat="server" Width="80%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for=""></Label></td>
                               <td>
                                    <asp:TextBox ID="txtAlamatAsal2" runat="server" Width="80%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        </div>
                        </div>

                        <div class="panel panel-default" style="width:90%">
                        <div class="panel-body">
                            <table style="width:100%" class="table table table-borderless">
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">NamaBank</Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlNamaBank" runat="server" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvNamaBank" runat="server" ControlToValidate="ddlNamaBank" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                                                    
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Bayar Atas Nama</Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlBayarAtasNama" runat="server" CssClass="form-control" Width="50%" AutoPostBack="True"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBayarAtasNama" runat="server" ControlToValidate="ddlBayarAtasNama" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">No Akaun</Label></td>
                                <td><asp:TextBox ID="txtNoAkaun" runat="server" Width="50%" CssClass="form-control" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Email</Label></td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" Width="50%" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Alamat</Label></td>
                                <td>
                                    <asp:TextBox ID="txtAlamat1" runat="server" Width="80%" CssClass="form-control"  ReadOnly="true" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for=""></Label></td>
                                <td>
                                    <asp:TextBox ID="txtAlamat2" runat="server" Width="80%" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Bandar</Label></td>
                                <td>
                                    <asp:TextBox ID="txtBandar" runat="server" Width="20%" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                   &nbsp &nbsp &nbsp &nbsp
                                    <Label class="control-label" for="">Negeri</Label>
                                    &nbsp &nbsp
                                    <asp:TextBox ID="txtNegeri" runat="server" Width="20%" CssClass="form-control"  ReadOnly="true"></asp:TextBox>

                                 </td>

                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Poskod</Label></td>
                                <td>
                                    <asp:TextBox ID="txtPoskod" runat="server" Width="20%" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                     &nbsp &nbsp &nbsp &nbsp
                                    <Label class="control-label" for="">Negara</Label>
                                    &nbsp &nbsp
                                    <asp:TextBox ID="txtNegara" runat="server" Width="20%" CssClass="form-control"  ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                        </table>

                        </div>
                        </div>
                        <br>
                        <div style="text-align:center">                    
                    <asp:Button ID="btnReset" text="Reset" runat="server" CssClass="btn" ValidationGroup="btnReset" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');"/> &nbsp;    &nbsp; &nbsp;                
                            <asp:Button ID="btnSimpan" text="Simpan" runat="server" CssClass="btn" ValidationGroup="btnSimpan" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?');"/> &nbsp;                    
                    
                </div>

                    </div>

                    
                </div>
                </div>
         

                </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>  
   
</asp:Content>
