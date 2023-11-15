<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Peruntukan_UniversitiXX.aspx.vb" Inherits="SMKB_Web_Portal.Peruntukan_UniversitiXX" %>
<%@ Import Namespace="System.Web.Mail"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
       
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
            <div class="panel panel-default">
          <div class="panel-heading">
              Maklumat Peruntukan Universiti
          </div>
         <div class="panel-body">
             <div class="row">
                 <div class="GvTopPanel">
                     <div style="float: left; margin-top: 8px; margin-left: 10px;">
                         <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                     </div>
                 </div>
                 <asp:GridView ID="gvPeruntukan" runat="server" AllowSorting="True" 
                     AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="Double" 
                     BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" 
                     EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" 
                     Height="100%" ShowFooter="True"
                     ShowHeaderWhenEmpty="True" Width="100%">
                     <Columns>
                         <asp:TemplateField HeaderText="Bil">
                             <ItemTemplate>
                                 <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                             </ItemTemplate>
                             <ItemStyle Width="30px" />
                         </asp:TemplateField>

                         <asp:TemplateField>
                             <HeaderTemplate>
                                 <asp:Label ID="lblTahun" runat="server" Text="Tahun" />&nbsp;
                            <asp:LinkButton ID="lnkTahun" runat="server" CommandName="Sort" CommandArgument="BG03_Tahun"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                             </HeaderTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="lblTahun" runat="server" Text='<%# Eval("BG03_Tahun")%>' />
                             </ItemTemplate>

                             <ItemStyle HorizontalAlign="Center" />
                         </asp:TemplateField>


                         <asp:TemplateField>
                             <HeaderTemplate>
                                 <asp:Label ID="lblAsal" runat="server" Text="Bajet (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkAsal" runat="server" CommandName="Sort" CommandArgument="BG03_Asal"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                             </HeaderTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="lblAsal" runat="server" Text='<%# Eval("BG03_Asal")%>' />
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                         </asp:TemplateField>

                         <asp:TemplateField>
                             <HeaderTemplate>
                                 <asp:Label ID="lblTB" runat="server" Text="Tambahan (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkTB" runat="server" CommandName="Sort" CommandArgument="BG03_Tambahan"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                             </HeaderTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="lblTB" runat="server" Text='<%# Eval("BG03_Tambahan")%>' />
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                         </asp:TemplateField>

                         <asp:TemplateField>
                             <HeaderTemplate>
                                 <asp:Label ID="lblKG" runat="server" Text="Kurangan (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkKG" runat="server" CommandName="Sort" CommandArgument="BG03_Kurangan"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                             </HeaderTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="lblKG" runat="server" Text='<%# Eval("BG03_Kurangan")%>' />
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                         </asp:TemplateField>

                         <asp:TemplateField>
                             <HeaderTemplate>
                                 <asp:Label ID="lblBF" runat="server" Text="Baki BF (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkBF" runat="server" CommandName="Sort" CommandArgument="jumbf"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                             </HeaderTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="lblBF" runat="server" Text='<%# Eval("jumbf")%>' />
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                         </asp:TemplateField>

                         <asp:TemplateField>
                             <HeaderTemplate>
                                 <asp:Label ID="lblJumBesar" runat="server" Text="Jumlah (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkJumBesar" runat="server" CommandName="Sort" CommandArgument="BG03_JumBesar"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                             </HeaderTemplate>
                             <ItemTemplate>
                                 <asp:Label ID="lblJumBesar" runat="server" Text='<%# Eval("BG03_JumBesar")%>' ForeColor="#003399" Font-Bold="true" />
                             </ItemTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                         </asp:TemplateField>

                     </Columns>
                     <FooterStyle Font-Bold="True" ForeColor="#000000" />
                     <HeaderStyle BackColor="#6699FF" />
                 </asp:GridView>
             </div>

             </div>

            <div class="row"> 
              

            <div class="row">
        <asp:Button ID="btnSendMail" runat="server" Text="Send Mail" Visible="false" /></div>
        
    </div>

        </ContentTemplate>
    </asp:UpdatePanel>


   
</asp:Content>
