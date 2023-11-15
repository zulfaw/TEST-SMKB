<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kelulusan_Naib_Canselor.aspx.vb" Inherits="SMKB_Web_Portal.Kelulusan_Naib_Canselor" EnableEventValidation="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>  
            
            <script type="text/javascript">
                function showNestedGridView(obj) {
                     debugger           
            var nestedGridView = document.getElementById(obj);
            var imageID = document.getElementById('image' + obj);
            if (nestedGridView.style.display == "none") {
                nestedGridView.style.display = "inline";
                imageID.src = "../../../Images/minus.png";
            } else {
                nestedGridView.style.display = "none";
                imageID.src = "../../../Images/plus.png";
            }
        }
</script>
                               
            <div class="container-fluid">            
                <div class="row" style="width :80%;">                                
                  <div class="col-sm-9 col-md-6" style="width :100%;">                                                                
                      <table style="width: 100%">
                          <tr>
                              <td colspan="2">
                                  <asp:Button ID="btnLulus" runat="server" CssClass="btn" Text="Lulus" />
                              </td>

                          </tr>
                          <tr>
                              <td colspan="2">&nbsp;</td>
                          </tr>
                          <tr>
                              <td colspan="2">
                                  <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" CssClass="tabCtrl" Height="800px" Width="100%">
                                      <ajaxToolkit:TabPanel ID="tabRingkasan" runat="Server" HeaderText="Ringkasan Bajet Universiti">
                                          <ContentTemplate>
                                              <table style="width: 100%">
                                                  <tr>
                                                      <td>
                                                          <asp:GridView ID="gvRingkasan" runat="server" AutoGenerateColumns="False" BackColor="#FFFFD4" BorderWidth="1px" CellPadding="2" color="#000000" CssClass="table table-striped table-bordered table-hover" DataKeyNames="Objek" font-size="11px" ShowFooter="True" width="100%">
                                                              <Columns>
                                                                  
                                                                  <asp:BoundField DataField="Objek" HeaderText="Objek Am/Objek Sebagai"   >
                                                                  <ItemStyle Font-Bold="True" HorizontalAlign="Center" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="Deskripsi" HeaderText="Deskripsi Objek Am/Objek Sebagai" />
                                                                  <asp:BoundField DataField="Perbelanjaan2016" HeaderText="Perbelanjaan Sebenar Tahun 2016">
                                                                  <ItemStyle HorizontalAlign="Right" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="Peruntukan2017" HeaderText="Peruntukan Asal Tahun 2017">
                                                                  <ItemStyle HorizontalAlign="Right" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="Anggaran2018PTj" HeaderText="Anggaran Diminta Tahun 2018 (PTj)">
                                                                  <ItemStyle HorizontalAlign="Right" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="Anggaran2018Bendahari" HeaderText="Anggaran Diminta Tahun 2018 (Bendahari)">
                                                                  <ItemStyle HorizontalAlign="Right" />
                                                                  </asp:BoundField>
                                                                  <asp:BoundField DataField="Anggaran2018NC" HeaderText="Anggaran Diminta Tahun 2018 (NC)">
                                                                  <ItemStyle HorizontalAlign="Right" />
                                                                  </asp:BoundField>
                                                              </Columns>
                                                              <PagerStyle HorizontalAlign="Center" />
                                                              <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                              <HeaderStyle Font-Bold="True" />
                                                              <AlternatingRowStyle BackColor="#E3E3B8" />
                                                          </asp:GridView>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                      <td style="text-align:center;">&nbsp;</td>
                                                  </tr>
                                              </table>
                                          </ContentTemplate>
                                      </ajaxToolkit:TabPanel>
                                      <ajaxToolkit:TabPanel ID="tabPermohonan" runat="Server" HeaderText="Senarai Permohonan Bajet PTj">
                                          <ContentTemplate>
                                              <table style="width: 100%">
                                                  <tr>
                                                      <td></td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td colspan="2">&nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td colspan="2">
                                                          <asp:GridView ID="gvPermohonan" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="#FFFFD4" BorderWidth="1px" CellPadding="2" color="#000000" CssClass="table table-striped table-bordered table-hover" DataKeyNames="NoPermohonan" font-size="11px" width="100%">
                                                              <AlternatingRowStyle BackColor="#E3E3B8" />
                                                              <Columns>
                                                                  <asp:TemplateField>
                                                                      <ItemTemplate>
                                                                          <a href="javascript:showNestedGridView('NoPermohonan-<%# Eval("NoPermohonan") %>');">
                                                                          <img id="imageNoPermohonan-<%# Eval("NoPermohonan") %>" alt="Klik untuk melihat butiran permohonan bajet." border="0" src="../../../Images/plus.png" width="10px" />
                                                                          </a>
                                                                      </ItemTemplate>
                                                                  </asp:TemplateField>
                                                                  <asp:BoundField DataField="Bil" HeaderText="Bil" ReadOnly="True" />
                                                                  <asp:BoundField DataField="NoPermohonan" HeaderText="No. Permohonan" SortExpression="NoPermohonan" />
                                                                  <asp:BoundField DataField="PTj" HeaderText="PTj" />
                                                                  <asp:BoundField DataField="Anggaran" HeaderText="Anggaran Perbelanjaan (RM)" />
                                                                  <asp:TemplateField>
                                                                      <ItemTemplate>
                                                                          <tr>
                                                                              <td colspan="100%">
                                                                                  <div id='NoPermohonan-<%# Eval("NoPermohonan") %>' style="display:none;position:relative;left:25px;">
                                                                                      <asp:GridView ID="nestedGridView" runat="server" AutoGenerateColumns="False" Width="98%">
                                                                                          <RowStyle BackColor="White" ForeColor="#000080" VerticalAlign="Top" />
                                                                                          <Columns>
                                                                                          
                                                                                              <asp:BoundField DataField="Bil" HeaderStyle-Width="2%" HeaderText="Bil" InsertVisible="False" ReadOnly="True" />
                                                                                              <asp:BoundField DataField="NoPermohonan" HeaderStyle-Width="10%" HeaderText="No Permohonan" SortExpression="NoPermohonanUnit" />
                                                                                              <asp:BoundField DataField="Bahagian" HeaderStyle-Width="15%" HeaderText="Bahagian" SortExpression="Bahagian" />
                                                                                              <asp:BoundField DataField="Unit" HeaderStyle-Width="15%" HeaderText="Unit" SortExpression="Unit" />
                                                                                              <asp:BoundField DataField="KO" HeaderStyle-Width="5%" HeaderText="KO" SortExpression="KO" />
                                                                                              <asp:BoundField DataField="Anggaran" HeaderStyle-Width="10%" HeaderText="Anggaran Perbelanjaan (RM)" SortExpression="Anggaran" />
                                                                                              <asp:BoundField DataField="Justifikasi"  HeaderText="Justifikasi" SortExpression="Justifikasi" />
                                                                                          </Columns>
                                                                                          <FooterStyle />
                                                                                          <PagerStyle HorizontalAlign="Center" />
                                                                                          <AlternatingRowStyle BackColor="#d1d1d0" Font-Bold="false" ForeColor="#000080" />
                                                                                          <HeaderStyle BackColor="#6f6f6d" Font-Bold="True" ForeColor="White" />
                                                                                      </asp:GridView>
                                                                                  </div>
                                                                              </td>
                                                                          </tr>
                                                                      </ItemTemplate>
                                                                  </asp:TemplateField>

                                                              </Columns>
                                                              <HeaderStyle Font-Bold="True" />
                                                              <PagerStyle HorizontalAlign="Center" />
                                                              <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                          </asp:GridView>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                      <td>&nbsp;</td>
                                                      <td>&nbsp;</td>
                                                  </tr>
                                              </table>
                                          </ContentTemplate>
                                      </ajaxToolkit:TabPanel>
                                  </ajaxToolkit:TabContainer>
                              </td>
                          </tr>
                          <tr>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                          </tr>
                          <tr>
                              <td colspan="2" style="text-align:center;">
                                  &nbsp;</td>
                          </tr>
                      </table>
                
                     </div>
                </div>

            </div>

              

            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
