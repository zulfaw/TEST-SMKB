<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Penyediaan_Bajet_PTJ.aspx.vb" Inherits="SMKB_Web_Portal.Penyediaan_Bajet_PTJ" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div class="container-fluid">
    
    <div class="row">

        <div class="col-sm-9 col-md-6 col-lg-8">
            <div class="panel-group">
            <div class="panel panel-default">
             <div class="panel-heading">
            <h4 class="panel-title">Senarai Status Permohonan</h4>
            </div>
            <div class="panel-body">
                <table class="nav-justified" style="width:80%;">
                      <tr style="height:25px">
                          <td style="width: 116px"><label class="control-label" for="">Bahagian:</label></td>
                          <td style="width: 80%">
                              <asp:DropDownList ID="ddlBahagian" runat="server" AutoPostBack="True" CssClass="form-control" Height="21px" Width="80%">
                              </asp:DropDownList>
                          </td>
                      </tr>
                      <tr style="height:25px">
                          <td style="width: 116px"><label class="control-label" for="">Unit:</label></td>
                          <td style="width: 80%">
                              <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="True" CssClass="form-control" Height="21px" Width="80%">

                              </asp:DropDownList>
                          </td>
                    </tr>
                     <tr style="height:25px">
                          <td style="width: 116px"><label class="control-label" for="">Kod Operasi:</label></td>
                          <td style="width: 80%">
                               <asp:DropDownList ID="ddlKodOperasi" runat="server" AutoPostBack="True" CssClass="form-control" Height="21px" Width="80%">
                                <asp:ListItem Selected="True">Keseluruhan</asp:ListItem>
                                <asp:ListItem>Komited</asp:ListItem>
                                <asp:ListItem>Biasa</asp:ListItem>
                                 </asp:DropDownList>

                              &nbsp;</td>
                    </tr>

                    <tr style="height:25px">

                        <td style="width: 361px" >
                        <asp:Button ID="btnPapar" runat="server" Text="Papar" CssClass="btnStyle" OnClick = "OnConfirm" />
                           
                        </td>
                    </tr>

                  </table>
                
                <br/>
               <%-- <h4>Senarai Permohonan Bajet</h4>--%>
                  <asp:GridView ID="gvStatusPermohonan" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" PageSize="5" emptydatatext="no data"
                CssClass= "table table-striped table-bordered table-hover" Width="100%"  Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True" >
                  <columns>
                    <%--<asp:TemplateField HeaderText="Pilih" HeaderStyle-CssClass="centerAlign">
                    <ItemTemplate>
                         <asp:CheckBox ID="chkPilih" runat="server" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="3%"/>
                    </asp:TemplateField>--%>

                    <asp:BoundField DataField="Bil" HeaderText="Bil" SortExpression="Bil" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="3%" HorizontalAlign="Center"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="NoPermohonan" HeaderText="No Permohonan" SortExpression="NoPermohonan" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>

                    <%--<asp:BoundField DataField="Bahagian" HeaderText="Bahagian" SortExpression="Bahagian" ReadOnly="True">
                    <ItemStyle Width="10%" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Unit" HeaderText="Unit" SortExpression="Unit" ReadOnly="True">
                    <ItemStyle Width="10%" />
                    </asp:BoundField>--%>

                    <asp:BoundField DataField="Program" HeaderText="Program" SortExpression="Program" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="30%"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="KodOperasi" HeaderText="Kod Operasi" SortExpression="KodOperasi" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="5%"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="Anggaran" HeaderText="Anggaran Perbelanjaan (RM)" SortExpression="Anggaran" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="3%" HorizontalAlign="Right"/>
                    </asp:BoundField>

                    
                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="10%" />
                    </asp:BoundField>

                    <asp:TemplateField>                        
                    <ItemTemplate>
                            <asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
                                <i class="fa fa-ellipsis-h fa-lg"></i>
                            </asp:LinkButton>
                     </ItemTemplate>
                     <ItemStyle Width="3%" HorizontalAlign="Center"/>
                     </asp:TemplateField>
                   
                  </columns>
           
                  <HeaderStyle BackColor="#6699FF" />

                  <RowStyle Height="5px" />

                  <SelectedRowStyle  ForeColor="Blue" />

            </asp:GridView>
                <div style="text-align:center">
                    <asp:LinkButton ID="lbtnDrafABM" runat="server" CssClass="btn btn-info">
                        Draf ABM 
                    </asp:LinkButton>
                    &nbsp;&nbsp;&nbsp;
                    <%--<asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info">
                        <i class="fa fa-paper-plane-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                    </asp:LinkButton>--%>
                </div>
                </div>
             </div>
                

            </div>
            <br /><br />
        </div>


    </div>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>  
</asp:Content>
