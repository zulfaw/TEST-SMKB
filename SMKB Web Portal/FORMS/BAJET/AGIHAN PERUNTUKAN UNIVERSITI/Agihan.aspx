<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Agihan.aspx.vb" Inherits="SMKB_Web_Portal.Agihan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                    <div class="panel-body well" style="width: 65%;padding:5px;">
                <div class="row">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70px">
                                <label class="control-label" for="">Tahun :</label>
                            </td>
                            <td>
                              <%--  <asp:TextBox ID="txtTahun" runat="server" ReadOnly="True" CssClass="form-control" Style="width: 100px;" BackColor="#FFFFCC"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlTahunAgih" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 100px; height: 21px;"/>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 100px; height: 25px;">
                                <label class="control-label" for="">Kumpulan Wang :</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" Style="width: 400px;">
                                </asp:DropDownList>
                                &nbsp;&nbsp;
                          <label id="lblMsgKW" class="control-label" for="" style="display: none; color: #820303;">
                              (Pilih KW)
                          </label>
                            </td>

                        </tr>
                    </table>
                </div>

            
            </div>


            <div class="panel panel-default" style="width: 98%;">
                <div class="panel-heading">Agihan PTj</div>
                <div class="panel-body" style="overflow:scroll;">
                    <div class="row" style="width:99%;">
                        <asp:GridView ID="gvAgihPTj" runat="server" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid"
                            CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt"
                            HeaderStyle-BackColor="#6699FF" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True"
                            Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                    </ItemTemplate>
                                    <ItemStyle Width="25px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PTJ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodPTJ" runat="server" Text='<%#Eval("KodPTj")%>' Visible="false" />
                                        <asp:Label ID="lblPTJ" runat="server" Text='<%#Eval("ButPTj")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right; font-weight: bold;">
                                            <asp:Label Text="Jumlah Besar (RM)" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="1000px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Permohonan (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmtMohon" runat="server" Text='<%#Eval("AmtMohon", "{0:N2}")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumAm10000" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Operasi">
                                    <ItemTemplate>
                                        <%-- <asp:HyperLink ID="hlAmt10000" runat="server" Text='<%#Eval("Amt10000", "{0:N2}")%>'/>--%>

                                        <asp:Label ID="lblAmt10000Op" runat="server" Text='<%#Eval("Amt10000Op", "{0:N2}")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumAm10000Op" runat="server" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#acd6ff" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Komited">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmt10000Ko" runat="server" Text='<%#Eval("Amt10000Ko", "{0:N2}")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumAm10000Ko" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#d0e8ff" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Operasi">
                                    <ItemTemplate>
                                        <%--<asp:HyperLink ID="hlAmt20000" runat="server" Text='<%#Eval("Amt20000", "{0:N2}")%>'/>--%>
                                        <asp:Label ID="lblAmt20000Op" runat="server" Text='<%#Eval("Amt20000Op", "{0:N2}")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumAm20000Op" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#acd6ff" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Komited">
                                    <ItemTemplate>
                                        <%--<asp:HyperLink ID="hlAmt20000" runat="server" Text='<%#Eval("Amt20000", "{0:N2}")%>'/>--%>
                                        <asp:Label ID="lblAmt20000Ko" runat="server" Text='<%#Eval("Amt20000Ko", "{0:N2}")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumAm20000Ko" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#d0e8ff" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Operasi">
                                    <ItemTemplate>
                                        <%--<asp:HyperLink ID="hlAmt30000" runat="server" Text='<%#Eval("Amt30000", "{0:N2}")%>'/>--%>
                                        <asp:Label ID="lblAmt30000Op" runat="server" Text='<%#Eval("Amt30000Op", "{0:N2}")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumAm30000Op" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#acd6ff" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Komited">
                                    <ItemTemplate>
                                        <%--<asp:HyperLink ID="hlAmt30000" runat="server" Text='<%#Eval("Amt30000", "{0:N2}")%>'/>--%>
                                        <asp:Label ID="lblAmt30000Ko" runat="server" Text='<%#Eval("Amt30000Ko", "{0:N2}")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumAm30000Ko" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#d0e8ff" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Operasi">
                                    <ItemTemplate>
                                        <%--<asp:HyperLink ID="hlAmt40000" runat="server" Text='<%#Eval("Amt40000", "{0:N2}")%>'/>--%>
                                        <asp:Label ID="lblAmt40000Op" runat="server" Text='<%#Eval("Amt40000Op", "{0:N2}")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumAm40000Op" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#acd6ff" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Komited">
                                    <ItemTemplate>
                                        <%--<asp:HyperLink ID="hlAmt40000" runat="server" Text='<%#Eval("Amt40000", "{0:N2}")%>'/>--%>
                                        <asp:Label ID="lblAmt40000Ko" runat="server" Text='<%#Eval("Amt40000Ko", "{0:N2}")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumAm40000Ko" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" BackColor="#d0e8ff" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Agihan (RM)">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblTitleAgihan" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblJumAgih" runat="server" ForeColor="#003399" Text='<%#Eval("JumAgih", "{0:N2}")%>'>  
                                        </asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotJum" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Pengurangan (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmtKurang" runat="server">  
                                        </asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotAmtKurang" runat="server" ForeColor="#003399" Font-Bold="true" Font-Size="9pt"/>
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="200px" Font-Size="9pt"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Peratus Pengurangan">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPerKurang" runat="server">  
                                        </asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotPerKurang" runat="server" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="100px" Font-Size="9pt"/>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemas kini">
                                          <i class="fas fa-edit"></i>
                                        </asp:LinkButton>

                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="5px" />
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle BackColor="#6699FF" />
                        </asp:GridView>

                    </div>

                </div>
            </div>
            </ContentTemplate>
         </asp:UpdatePanel>

   
</asp:Content>
