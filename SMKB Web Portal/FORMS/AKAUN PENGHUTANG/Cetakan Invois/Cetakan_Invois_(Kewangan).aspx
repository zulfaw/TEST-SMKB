<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Cetakan_Invois_(Kewangan).aspx.vb" Inherits="SMKB_Web_Portal.Cetakan_Invois_Kewangan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
   
     <asp:UpdatePanel ID="UpdatePanel" runat="server">
         <ContentTemplate>
             <div class="row" style="width: 700px;">
                 <div class="well">
                     <table style="width: 100%;">
                         <tr>
                             <td colspan="3">
                                       <asp:RadioButtonList ID="rbJns" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                                    <asp:ListItem Text=" Kewangan" Value="0" Selected="True" />
                                    <asp:ListItem Text=" Pelajar" Value="1" />
                                </asp:RadioButtonList>
                             </td>
                         </tr>
                         <tr>
                             <td>Carian</td>
                             <td>:</td>
                             <td>
                                 <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlCarian_SelectedIndexChanged" Width="200px" />
                                 &nbsp;&nbsp;
                                 <asp:TextBox ID="txtCarian" runat="server" Width="250px"></asp:TextBox>
                             </td>
                         </tr>
                         <tr>
                             <td></td>
                             <td>&nbsp;</td>
                             <td>
                                 <div style="margin-top: 20px;">
                                     <asp:LinkButton ID="lbtnFindInv" runat="server" CssClass="btn ">
						<i class="fas fa-search"></i>&nbsp;&nbsp;&nbsp;Cari
                                     </asp:LinkButton>
                                 </div>
                             </td>
                         </tr>
                     </table>



                 </div>
             </div>

             <div class="row">
                 <div class="panel panel-default" style="width: 80%;">
                     <div class="panel-heading">Senarai Kelulusan Invois</div>
                     <div class="panel-body">
                         
                         <div class="GvTopPanel" style="height: 33px;">
                             <div style="float: left; margin-top: 5px; margin-left: 10px;">
                                 <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                                 &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;

                    <label class="control-label" for="Klasifikasi">Saiz Rekod :</label>
                                 <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                     <asp:ListItem Text="10" Value="10" />
                                     <asp:ListItem Text="25" Value="25" Selected="True" />
                                     <asp:ListItem Text="50" Value="50" />
                                     <asp:ListItem Text="100" Value="100" />
                                 </asp:DropDownList>

                                 &nbsp;&nbsp;
                             </div>
                         </div>

                         <asp:GridView ID="gvCetakInv" runat="server"  AllowPaging= "true" AllowSorting="True" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" " 
                    PageSize="25" cssclass="table table-striped table-bordered table-hover" Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True"  >
                                 <Columns>

                                 <asp:TemplateField HeaderText="Bil">
                                     <ItemTemplate>
                                         <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                     </ItemTemplate>
                                     <ItemStyle Width="3%" HorizontalAlign="Center" />
                                 </asp:TemplateField>

                                 <asp:TemplateField HeaderText="No. Invois">
                                     <ItemTemplate>
                                         <asp:Label ID="lblNoInvCuk" runat="server" Text='<%# Eval("AR01_NoBil")%>' />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left" Width="20%" />
                                 </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Tarikh Invois">
                                     <ItemTemplate>
                                         <asp:Label ID="lblTkhInv" runat="server" Text='<%# Eval("TkhBil", "{0:dd/MM/yyyy}")%>' />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="center" Width="15%" />
                                 </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Kategori">
                                     <ItemTemplate>
                                         <asp:Label ID="lblKat" runat="server" Text='<%# Eval("AR01_Kategori")%>' />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Center" Width="10%" />
                                 </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Nama Penerima">
                                     <ItemTemplate>
                                         <asp:Label ID="lblNPenerima" runat="server" Text='<%# Eval("AR01_NamaPenerima")%>' />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="Left" Width="30%" />
                                 </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Jumlah Besar (RM)">
                                     <ItemTemplate>
                                         <asp:Label ID="lblJum" runat="server" Text='<%# Eval("Jumlah", "{0:###,###,###.00}")%>' ForeColor="#003399" />
                                     </ItemTemplate>
                                     <ItemStyle HorizontalAlign="right" Width="10%" />
                                 </asp:TemplateField>
                                                                 
                                 <asp:TemplateField>
                                     <ItemTemplate>
                                         <asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Maklumat Lanjut">
											<i class="fas fa-print fa-lg"></i>
                                         </asp:LinkButton>
                                     </ItemTemplate>
                                     <ItemStyle Width="3%" HorizontalAlign="Center" />
                                 </asp:TemplateField>
                             </Columns>
                            <HeaderStyle BackColor="#6699FF" />
                    </asp:GridView>

                     </div>
                 </div>
             </div>
             </b>
         </ContentTemplate>
         </asp:UpdatePanel>
</asp:Content>
