<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Agihan_PTj.aspx.vb" Inherits="SMKB_Web_Portal.Agihan_PTj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <script>
        //function isNumberKey(evt) {
        //    var charCode = (evt.which) ? evt.which : evt.keyCode;
        //    if (charCode > 31 && (charCode < 48 || charCode > 57))
        //        return false;
        //    return true;
        //}

        function fCalcTotal(obj) {
            try {
               
                //find all texbox value in gv
                var grid = document.getElementById('<%=gvPTj.ClientID %>');
                var valBajet;
                var col1;
                var totBajet = 0;
                var row = obj.parentNode.parentNode;

                for (i = 0; i < grid.rows.length; i++) {
                    col1 = grid.rows[i].cells[3];
                    for (j = 0; j < col1.childNodes.length; j++) {
                        if (col1.childNodes[j].type == "text") {
                        
                            //remove ',' character before it can be parsefloated                
                            valBajet = col1.childNodes[j].value;

                            if (col1.childNodes[j].value == "") {
                                valBajet = 0
                            }
                            else {
                                valBajet = valBajet.replace(/,/g, '')

                                var val2 = valBajet;
                                val2 = fFormatCommas(val2);
                                col1.childNodes[j].value = val2;

                            }
                            valBajet = parseFloat(valBajet);
                            totBajet += valBajet;
                        }
                    }
                }
            
                var totBajetKW = document.getElementById('<%=txtBajetKW.ClientID%>').value
                totBajetKW = totBajetKW.replace(/,/g, '')
                totBajetKW = parseFloat(totBajetKW);

                var totBaki = totBajetKW - totBajet
              var totBaki2 = totBaki
              totBaki = totBaki.toFixed(2) //format numer to 2 decimal //#.00             
              totBaki = formatMoney(totBaki) //format number to '#,###.00'
              document.getElementById('<%=txtBakiKW.ClientID%>').value = totBaki

                totBajet = totBajet.toFixed(2) //format numer to 2 decimal //#.00             
                totBajet = formatMoney(totBajet) //format number to '#,###.00'
                var a = $(".cssTotBajet").text(totBajet); //set Footer label value //Total col 'Bajet'

                document.getElementById('<%=txtAgihKW.ClientID%>').value = totBajet;



                var intRowidx = row.rowIndex;
                var colBajet;
                colBajet = grid.rows[intRowidx].cells[3];
                valBajet = colBajet.childNodes[1].value; //get col Bajet value        
                valBajet = valBajet.replace(/,/g, '')
                valBajet = parseFloat(valBajet);

                var valTB, valKG, valBakiBF
                valTB = parseFloat(grid.rows[intRowidx].cells[4].innerText.replace(/,/g, ''));
                valKG = parseFloat(grid.rows[intRowidx].cells[5].innerText.replace(/,/g, ''));
                valBakiBF = parseFloat(grid.rows[intRowidx].cells[6].innerText.replace(/,/g, ''));

                var valJum = valBajet + valTB + valBakiBF - valKG

                valJum = valJum.toFixed(2) //format numer to 2 decimal //#.00             
                valJum = formatMoney(valJum) //format number to '#,###.00'
                grid.rows[intRowidx].cells[7].innerHTML = valJum; //set col Jumlah value

                var gTotJumlah = 0;
                var valgJum;

                for (i = 1; i < grid.rows.length - 1; i++) {
                    val = grid.rows[i].cells[7].innerText;
                    valgJum = parseFloat(val.replace(/,/g, ''));
                    gTotJumlah += valgJum;
                }

                gTotJumlah = gTotJumlah.toFixed(2) //format numer to 2 decimal //#.00             
                gTotJumlah = formatMoney(gTotJumlah) //format number to '#,###.00'             
                var b = $(".cssTotBajet2").text(gTotJumlah); //set Footer label value //Total col 'Jumlah'

                if (totBaki2 < 0) {
                  document.getElementById('<%=txtBakiKW.ClientID%>').style.backgroundColor = '#FFD4D4';
                  alert('Amaun yang dimasukkan melebihi had peruntukan kumpulan wang! Sila masukkan semula.')
              }
              else {                 
                  document.getElementById('<%=txtBakiKW.ClientID%>').style.backgroundColor = '#FFFFCC';
                }



            }
            catch (err) {
                alert(err)
            }
        }

        //function fFormatCommas(nStr) {
        //    nStr += '';
        //    var x = nStr.split('.');
        //    var x1 = x[0];
        //    var x2 = x.length > 1 ? '.' + x[1] : '';
        //    var rgx = /(\d+)(\d{3})/;
        //    while (rgx.test(x1)) {
        //        x1 = x1.replace(rgx, '$1' + ',' + '$2');
        //    }
        //    return x1 + x2;
        //}

        //function formatMoney(number, places, symbol, thousand, decimal) {
        //    number = number || 0;
        //    places = !isNaN(places = Math.abs(places)) ? places : 2;
        //    symbol = symbol !== undefined ? symbol : "";
        //    thousand = thousand || ",";
        //    decimal = decimal || ".";
        //    var negative = number < 0 ? "-" : "",
        //        i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
        //        j = (j = i.length) > 3 ? j % 3 : 0;
        //    return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
        //}

        function fConfirm() {

            try {
 
                var blnComplete;
                var ddlKW = document.getElementById("<%=ddlKW.ClientID %>");
                var intSelKW = ddlKW.selectedIndex

                // KW
                if (intSelKW == 0 || intSelKW == -1) {
                    blnComplete = false
                    document.getElementById("lblMsgKW").style.display = 'inline-block';
                }
                else {
                    document.getElementById("lblMsgKW").style.display = 'none';
                }

                if (blnComplete == false) {
                    alert('Sila lengkapkan maklumat!')
                    return false;
                }
               <%-- var ddlKW = document.getElementById("<%=ddlKW.ClientID %>");
                var intSelKW = ddlKW.selectedIndex;
                if (intSelKW == 0) {
                  alert('Sila Pilih Kumpulan Wang!');
                  return false;
              }--%>
                                    
              var valAgih = parseFloat(document.getElementById('<%=txtAgihKW.ClientID%>').value.replace(/,/g, ''))
              if (valAgih == 0) {
                  alert('Sila masukkan amaun agihan kepada PTj!')
                  return false;
              }

          var valBaki = parseFloat(document.getElementById('<%=txtBakiKW.ClientID%>').value.replace(/,/g, ''))        
              //if baki neg(-)
            if (valBaki < 0) {
                alert('Amaun yang dimasukkan melebihi had peruntukan kumpulan wang! Sila masukkan semula.')
                return false;
            }

            if (confirm('Anda pasti untuk simpan?')) {
                return true;
            } else {
                return false;
            }        
          }
                catch (err) {
                    alert(err)
                    return false;
          }          
      }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="panel-body well" style="width: 65%;">
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

                <div class="row">
                    <table>
                        <tr>
                            <td style="width: 100px; height: 22px;">
                                <label class="control-label" for="">Bajet (RM) : </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBajetKW" runat="server" ReadOnly="True" CssClass="form-control rightAlign" Style="width: 200px;" BackColor="#FFFFCC"></asp:TextBox>
                            </td>
                            <td style="width: 100px; height: 22px; text-align: right;">
                                <label class="control-label" for="">Agihan (RM) : </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAgihKW" runat="server" ReadOnly="True" CssClass="form-control rightAlign" Style="width: 200px;" BackColor="#FFFFCC"></asp:TextBox>
                                <asp:TextBox ID="hidTxtAgihKW" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" Style="width: 100px; display: none;"></asp:TextBox>
                            </td>
                            <td style="width: 100px; height: 22px; text-align: right;">
                                <label class="control-label" for="">Baki (RM) : </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBakiKW" runat="server" ReadOnly="True" CssClass="form-control rightAlign" Style="width: 200px;" BackColor="#FFFFCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 22px;">
                                <asp:HiddenField ID="hidIndKW" runat="server" />
                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 100px; height: 22px; text-align: right;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 100px; height: 22px; text-align: right;">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>


            <div class="panel panel-default" style="width: 90%;">
                <div class="panel-heading">Senarai PTj</div>
                <div class="panel-body">

                    <div class="row">
                        <div class="GvTopPanel">
                            <div style="float: left; margin-top: 8px; margin-left: 10px;">
                                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                            </div>
                        </div>
                        <asp:GridView ID="gvPTj" runat="server" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid"
                            CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt"
                            HeaderStyle-BackColor="#6699FF" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True"
                            Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Bil">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Kod PTj">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKodPTJ" runat="server" Text='<%#Eval("KodPTj")%>' />
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="PTJ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPTJ" runat="server" Text='<%#Eval("PTJ")%>' />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right; font-weight: bold;">
                                            <asp:Label Text="Jumlah Besar (RM)" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
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
                                            <asp:Label ID="lblTotBajetPast" runat="server" ClientIDMode="Static" CssClass="cssTotBajet" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
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
                                            <asp:Label ID="lblTotBajetUsed" runat="server" ClientIDMode="Static" CssClass="cssTotBajet" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblBajetCur" runat="server" />
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBajet" runat="server" onkeypress="return isNumberKey(event,this)" Text='<%#Eval("JumBajet")%>' Width="100%" onblur="if (this.dirty){this.onchange();}" OnTextChanged="txtBajet_TextChanged" AutoPostBack="true" CssClass="form-control rightAlign"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotBajet" runat="server" CssClass="cssTotBajet" ForeColor="#003399" Font-Bold="true" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" BackColor="#e8a196" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Tambahan (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTB" runat="server" Text='<%#Eval("TB")%>'>  
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotTB" runat="server" ForeColor="#003399" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Kurangan (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblKG" runat="server" Text='<%#Eval("KG")%>'>  
                                        </asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotKG" runat="server" ForeColor="#003399" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Baki BF (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBakiBF" runat="server" Text='<%#Eval("BakiBF")%>'>  
                                        </asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotBakiBF" ForeColor="#003399" runat="server" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Jumlah (RM)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblJum" runat="server" ForeColor="#003399" Text='<%#Eval("Jumlah")%>'>  
                                        </asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblTotJum" runat="server" CssClass="cssTotBajet2" ForeColor="#003399" />
                                        </div>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                </asp:TemplateField>

                            </Columns>
                            <HeaderStyle BackColor="#6699FF" />
                        </asp:GridView>


                        <div style="text-align: center; margin-bottom: 10px; margin-top: 30px;">
                            <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                            </asp:LinkButton>
                        </div>
                    </div>
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
