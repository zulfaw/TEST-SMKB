<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Maklumat_PTJ.aspx.vb" Inherits="SMKB_Web_Portal.Maklumat_PTJ" EnableEventValidation="False" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div class="container-fluid">
        <div class="col-sm-9 col-md-6 col-lg-8">
        <p></p>
        <div class="row" style="width :100%;">  
            <%--<div class="panel panel-default">--%>
                <div class="panel-body">
                <table class="nav-justified" style="width:100%;">
                      <tr style="height:25px">
                          <td style="width: 15%"><label class="control-label" for="">Kategori :</label></td>
                          <td style="width:85%;">
                            <asp:DropDownList ID="ddlKategori" runat="server" Width="150px" class="form-control" AutoPostBack="True">
                            <asp:ListItem Text="PTJ" Value="PTJ" selected="True" />
                            <asp:ListItem Text="Bahagian" Value="Bahagian" />
                            <asp:ListItem Text="Unit" Value="Unit" />
                            </asp:DropDownList>
                          </td>
                      </tr>
                  </table>
                </div>

            <div class="panel panel-default">
                <div class="panel-body">
                    <table class="nav-justified" style="width:100%;">
                      <tr style="height:25px">
                          <td style="width: 10%"><label class="control-label" for="">PTJ :</label></td>
                          <td style="width:50%;">
                            <asp:DropDownList ID="ddlPTJ" runat="server" Width="90%" class="form-control" AutoPostBack="True">
                            </asp:DropDownList>
                          </td>
                          <td style="width: 10%"><label class="control-label" for="">Bahagian :</label></td>
                          <td style="width:35%;">
                            <asp:DropDownList ID="ddlBahagian" runat="server" Width="80%" class="form-control" AutoPostBack="True">
                            </asp:DropDownList>
                          </td>
                      </tr>
                    </table>
                    <table class="nav-justified" style="width:100%;">
                      <tr style="height:25px">
                          <td style="width: 10%"><label class="control-label" for="">Nama :</label></td>
                          <td style="width:85%;">
                            <asp:DropDownList ID="ddlNama" runat="server" Width="90%" class="form-control" AutoPostBack="True">
    
                            </asp:DropDownList>
                          </td>                    
                  </table>
                    <br>
                   <table class="nav-justified" style="width:100%;">
                      <tr style="height:25px">
                          <td style="width: 10%"><label class="control-label" for="">Status :</label></td>
                          <td style="width:90%;">
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="20%" class="form-control">
                            <asp:ListItem Text="Aktif" Value="1" selected="True" />
                            <asp:ListItem Text="Tidak Aktif" Value="0" />
                            </asp:DropDownList>
                          </td>
                         
                          </td>
                      </tr>  
                      
                     <tr>
                          <td>&nbsp; </td>
                          <td>&nbsp;<asp:Button ID="btnReset" runat="server" CssClass="btn" Text="Reset" />
                              &nbsp;&nbsp;
                              <asp:Button ID="btnSavePTJ" runat="server" CssClass="btn" Text="Simpan" ValidationGroup="btnSavePTJ" />
                          </td>
                    </tr>
                  </table>


                </div>
            </div>
            <%--</div>--%>


        </div>
        </div>
        </div>         
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
