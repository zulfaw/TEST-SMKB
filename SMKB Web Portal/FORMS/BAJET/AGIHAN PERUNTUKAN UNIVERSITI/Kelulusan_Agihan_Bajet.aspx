<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kelulusan_Agihan_Bajet.aspx.vb" Inherits="SMKB_Web_Portal.Kelulusan_Agihan_Bajet_Ketua_PTj" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>KELULUSAN AGIHAN BAJET</h1>

    <style>
        .panel
        {
            width:70%;
        }

        @media (max-width: 1600px) {
            .panel {
                width: 95%;
            }
        }
    </style>

   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>           
            <div class="row">
                <div class="panel-body well" style="width: 400px;">
                    <div class="row">
                        <table style="width: 100%;">

                            <tr>
                                <td style="width:100px;">Bajet Tahun</td>
                                <td>:</td>
                                <td>
                                    <asp:DropDownList ID="ddlTahunAgih" runat="server" AutoPostBack="True" CssClass="form-control" />
                                </td>
                            </tr>

                        </table>
                        </table>
                    </div>
                </div>
            </div>

            <div class="row" style="width: 80%;">
                <div class="panel panel-default" style="width: 100%;">
                    <div class="panel-heading">Senarai Agihan PTj</div>
                    <div class="panel-body" style="max-height: 1000px; overflow: scroll;">
                        <div style="margin: 10px; width: 75%;">
                            <asp:GridView ID="GvPTjAgih" runat="server" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid"
                            CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt"
                            HeaderStyle-BackColor="#6699FF" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True"
                            Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="true" CssClass="borderNone" onclick="Check_Click(this)" />
                                        <asp:LinkButton ID="btnChecked" runat="server" CssClass="borderNone" Visible="false">
                                    <a href="#" data-toggle="tooltip" title="Agihan kepada PTj ini belum selesai." style="cursor:default;border:none;">
                                    <span style="color:red;">
                                        <i class="fa fa-exclamation-triangle fa-lg" aria-hidden="true"></i>
                                    </span></a>
                                        </asp:LinkButton>

                                        <asp:LinkButton ID="btnLulus" runat="server" Visible="false">
      <a href="#" data-toggle="tooltip" title="Agihan telah lulus." style="cursor:default;">
          <span style="color:#008000;"><i class="fas fa-check-circle fa-lg"></i></span></a>
                                        </asp:LinkButton>
                                 
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Kod PTj">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIndPTj" runat="server" Text='<%#Eval("BG05_IndPTJ")%>' Visible="false" />
                                        <asp:Label ID="lblKodPTJ" runat="server" Text='<%#Eval("KodPTj")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PTJ">
                                    <ItemTemplate>                                         
                                        <asp:Label ID="lblPTJ" runat="server" Text='<%#Eval("butPTj")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right; font-weight: bold;">
                                            <asp:Label Text="Jumlah Besar (RM)" runat="server" /></div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Jumlah (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBajetPTj" runat="server" ForeColor="#003399" Text='<%#Eval("BG05_Amaun", "{0:N2}")%>'>  
                                        </asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumBajet" runat="server" CssClass="cssTotBajet2" ForeColor="#003399" Font-Bold="true"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="20%" />
                                </asp:TemplateField>


                                <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Maklumat agihan">
                                          <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                                                            
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle BackColor="#6699FF" />
                        </asp:GridView>
                        </div>
                        <asp:HiddenField ID="hidRecCount" runat="server" />

                    </div>
                </div>

                <div style="text-align: center; margin-bottom: 10px; width: 100%">
                    <asp:LinkButton ID="lbtnLulus" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk luluskan agihan bajet ini?');">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Lulus
                    </asp:LinkButton>
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
