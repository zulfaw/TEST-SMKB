<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Agihan_Kumpulan_Wang.aspx.vb" Inherits="SMKB_Web_Portal.Pengagihan_Kumpulan_Wang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div class="container-fluid">--%>
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

  <%--<script type="text/javascript">
         
    function CalcTotal(obj) {
        try {
       
              var row = obj.parentNode.parentNode;
              var intRowidx = row.rowIndex;

              var col1;
              //var col6;
              var totAgihan = 0;
              var grid = document.getElementById('<%=gvObjAm.ClientID %>')
              var val;
              //var val2;
               
              //find all texbox value in gv
              for (i = 0; i < grid.rows.length-1; i++) {
                  col1 = grid.rows[i].cells[2];
                  for (j = 0; j < col1.childNodes.length; j++) {
                      if (col1.childNodes[j].type == "text") {
                          
                          //remove ',' character before it can be parsefloated                
                          val = col1.childNodes[j].value;

                          if (col1.childNodes[j].value=="") {
                              val = 0                      
                          }
                          else {
                             
                              val = val.replace(/,/g, '')

                              var val2 = val;
                              val2 = fFormatCommas(val2);
                              col1.childNodes[j].value = val2;                
                          }
                          val = parseFloat(val);
                          totAgihan += val;
                      }              
                         
                  }
              }
                   
            totAgihan = parseFloat(totAgihan);
            totAgihan = totAgihan.toFixed(2) //format numer to 2 decimal //#.00             
            totAgihan = formatMoney(totAgihan) //format number to '#,###.00'           
            var a = $(".cssTotBajet").text(totAgihan); //set Footer label value //Total col 'Bajet'
                                
          }
          catch (err) {             
              alert(err);
          }
    }


    
</script>--%>

    <style>
        .tooltip-inner {
            width: 100px;
        }

    </style>
     
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          
            <div class="panel panel-default" style="width:80%">
                <div class="panel-heading">Bajet Kumpulan Wang</div>
                <div class="panel-body">
                    <div class="row">
                       Agihan untuk tahun :
                        
                        <asp:DropDownList ID="ddlTahunAgih" runat="server" AutoPostBack="true" CssClass="form-control" Width="250px">
                        </asp:DropDownList>
                        
                         <%--<asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="100px"></asp:TextBox>--%>                           
                        </div>

                    <div class="row">
                        <div class="GvTopPanel">
                            <div style="float: left; margin-top: 8px; margin-left: 10px;">
                                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                            </div>
                        </div>
                        
                        <asp:GridView ID="gvAgihKW" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kumpulan Wang">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKW" runat="server" Text='<%#Eval("KW")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="300px"/>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bajet (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBajet" runat="server" Text='<%#Eval("Bajet", "{0:###,###,###.00}")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumBajet" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Tambahan (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTamb" runat="server" Text='<%#Eval("Tambahan", "{0:###,###,###.00}")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumTamb" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kurangan (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKur" runat="server" Text='<%#Eval("Kurangan", "{0:###,###,###.00}")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumKur" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baki BF (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBakiBF" runat="server" Text='<%#Eval("BakiBF", "{0:###,###,###.00}")%>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblJumBakiBF" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jumlah (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJumlah" runat="server" ForeColor="#003399" Text='<%#Eval("Jumlah", "{0:###,###,###.00}")%>' />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotJum" runat="server" ClientIDMode="Static" CssClass="cssTotJum" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih" Visible="false">
                                          <i class="fas fa-edit"></i>
                                        </asp:LinkButton>
                                       
                                        &nbsp;
                                        <asp:LinkButton ID="lbtnBajetPast" runat="server" CommandName="ShowPast" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Rekod tahun sebelum" Visible="false">
                                          <i class="fas fa-history"></i>
                                        </asp:LinkButton>
                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="7%" />
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle BackColor="#6699FF" />
                        </asp:GridView>




                    </div>
                    
                </div>
            </div>

            <div class="panel panel-default">
                <div class="panel-heading">Agihan Objek Am</div>
                <div class="panel-body">
                    <table style="width: 100%; height: 100%;">
                        <tr>
                            <td style="width: 110px; text-align: left; height: 30px;">Kumpulan Wang :</td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtKodKW" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="5%"></asp:TextBox>
                                &nbsp;-
                                                <asp:TextBox ID="txtKW" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="true" Width="500px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td colspan="2">
                                
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvObjAm" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Objek Am">
                                            <ItemTemplate>
                                                <asp:Label ID="ObjAm" runat="server" Text='<%#Eval("ObjAm")%>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right; font-weight: bold;">
                                                    <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" Width="300px"/>
                                        </asp:TemplateField>

                                         <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblBajetPast" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJumBajetPast" runat="server" Text='<%#Eval("JumBajetPast", "{0:N2}")%>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotBajetPast" runat="server" ClientIDMode="Static" CssClass="cssTotBajet" ForeColor="#003399" Font-Bold="true"/>
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="160px"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblJumGuna" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJumBajetUsed" runat="server" Text='<%#Eval("JumBajetUsed", "{0:N2}")%>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotBajetUsed" runat="server" ClientIDMode="Static" CssClass="cssTotBajet" ForeColor="#003399" Font-Bold ="true"/>
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="160px"/>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblBajetCur" runat="server"/>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtBajet" AutoPostBack="true"  runat="server" CssClass="form-control rightAlign" onkeypress="return isNumberKey(event,this)" OnTextChanged="txtBajet_TextChanged" Text='<%#Eval("JumBajet", "{0:N2}")%>' Width="100%"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <div style="text-align: right;">
                                                    <asp:Label ID="lblTotBajet" runat="server" ClientIDMode="Static" CssClass="cssTotBajet" ForeColor="#003399" Font-Bold ="true"/>
                                                </div>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="160px" BackColor="#e8a196"/>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#6699FF" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="text-align: center; margin-bottom: 10px;">
                                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn" Visible ="false">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                                    </asp:LinkButton>
                                </div>
                            </td>
                        </tr>

                        <asp:HiddenField runat="server" ID="hidBajAsal"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hidBajTamb"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hidBajKur"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hidBajJumBesar"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hidBajet"></asp:HiddenField>
                        <asp:HiddenField runat="server" ID="hidJumBajet"></asp:HiddenField>

                    </table>
                </div>
            </div>

          <asp:Button ID="btnPopObjAmPast" runat ="server" style ="display:none" />

            <ajaxToolkit:ModalPopupExtender ID="modPopBajetPast" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlObjAmPast" TargetControlID="btnPopObjAmPast" BehaviorID="mpe2"  >
                                     </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlObjAmPast" runat="server" BackColor="White" Width="800px" Style="display: none;">
                
                    <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                        <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                            <td style="height: 10%; text-align: center;">
                                <span style="font-weight:bold"> Agihan Objek Am Tahun Sebelum </span>
                            </td>
                            <td style="width: 50px; text-align: center;">
                                <button runat="server" id="btnClose2" title="Tutup" class="btnNone ">
                                    <i class="far fa-window-close fa-2x"></i>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <div style="margin:10px;">
                                <table style="width: 100%; height: 100%;">
                                    <tr>
                                        <td style="width: 110px; text-align: left; height: 30px;">Kumpulan Wang</td>
                                        <td style="width: 10px; text-align: left; height: 30px;">:</td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtKodKWPast" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="True" Width="5%"></asp:TextBox>
                                            &nbsp;-
                                                <asp:TextBox ID="txtKWPast" runat="server" BackColor="#FFFFCC" class="form-control" ReadOnly="true" Width="500px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; text-align: left; height: 30px;">Tahun</td>
                                        <td style="width: 10px; text-align: left; height: 30px;">:</td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" CssClass="form-control">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">                                          
                                            <asp:GridView ID="gvObjAmPast" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" BorderWidth="1px" CssClass="table table-striped table-bordered table-hover" EmptyDataText=" Tiada rekod" Font-Size="8pt" HeaderStyle-BackColor="#6699FF" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Bil">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="3%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Objek Am">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ObjAm" runat="server" Text='<%#Eval("ObjAm")%>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: right; font-weight: bold;">
                                                                <asp:Label runat="server" Text="Jumlah Besar (RM)" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bajet (RM)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBajetObjAm" runat="server" Text='<%#Eval("JumBajet", "{0:N2}")%>' />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <div style="text-align: right;">
                                                                <asp:Label ID="lblTotBajet" runat="server" ClientIDMode="Static" CssClass="cssTotBajet" ForeColor="#003399" />
                                                            </div>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
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

        </ContentTemplate>
    </asp:UpdatePanel>
        
   
</asp:Content>
