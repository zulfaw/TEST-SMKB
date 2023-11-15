<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Agihan_Pej_Bendahari.aspx.vb" Inherits="SMKB_Web_Portal.Pengagihan_Pej_Bendahari" EnableEventValidation ="false"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

        <script type="text/javascript">
          
            function fConfirm() {

            try {
                
                var ddlKW = document.getElementById("<%=ddlKW.ClientID %>");
                var intSelKW = ddlKW.selectedIndex;
                if (intSelKW == 0) {
                  alert('Sila Pilih Kumpulan Wang!');
                  return false;
              }

                var ddlPTj = document.getElementById("<%=ddlPTJ.ClientID %>");
                var intSelPTj = ddlPTj.selectedIndex;
                if (intSelPTj == 0) {
                  alert('Sila Pilih PTj!');
                  return false;
                }

                var ddlKO = document.getElementById("<%=ddlKO.ClientID %>");
                var intSelKO = ddlKO.selectedIndex;
                if (intSelKO == 0) {
                  alert('Sila Pilih Kod Operasi!');
                  return false;
              }

                var ddlKp = document.getElementById("<%=ddlKp.ClientID %>");
                var intSelKP = ddlKp.selectedIndex;
                if (intSelKP == 0) {
                  alert('Sila Pilih Kod Projek!');
                  return false;
                }


                debugger;
              var valBajetPTj = parseFloat(document.getElementById('<%=txtBajetPTj.ClientID%>').value.replace(/,/g, ''))
              if (valBajetPTj == 0) {
                  alert('Sila masukkan syiling agihan PTj ini!')
                  return false;
              }
             
              var valAgih = parseFloat(document.getElementById('<%=txtAgihanP.ClientID%>').value.replace(/,/g, ''))
              if (valAgih == 0) {
                  alert('Sila masukkan amaun agihan kepada Objek Am!')
                  return false;
              }

          var valBaki = parseFloat(document.getElementById('<%=txtBakiP.ClientID%>').value.replace(/,/g, ''))        
              //if baki neg(-)
            if (valBaki < 0) {
                alert('Amaun yang dimasukkan melebihi had peruntukan untuk objek ini! Sila masukkan semula.')
                return false;
            }
              //if agihan belum selesai
            if (valBaki > 0){
                if (confirm('Syiling PTJ belum selesai diagihkan. Anda pasti untuk simpan?')) {
                    return true;
                } else {
                    return false;
                }
            }
            else {               
                if (confirm('Anda pasti untuk simpan?')) {
                    return true;
                } else {
                    return false;
                }
            }
          }
                catch (err) {
                    alert(err)
                    return false;
          }          
      }

            function isNumberKey(evt) {
                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                    return false;
                return true;
            }

            function fCalc(obj) {
                try {
                    debugger;
                    var valBajetPTj2
                    var valBajetPTj = document.getElementById('<%=txtBajetPTj.ClientID%>').value
                  
                    if (isNaN(parseFloat(valBajetPTj)) == true) {
                        valBajetPTj = 0
                    }
                    else {
                        valBajetPTj = valBajetPTj.replace(/,/g, '');
                        valBajetPTj = parseFloat(valBajetPTj)                      
                    }
                                        
                    valBajetPTj2 = valBajetPTj
                    var valBajetKW = document.getElementById('<%=txtBajetKW.ClientID%>').value;
                    var valAgihKW = document.getElementById('<%=hidTxtAgihKW.ClientID%>').value;

                    valBajetKW = valBajetKW.replace(/,/g, '');
                    valBajetKW = parseFloat(valBajetKW);

                    valAgihKW = valAgihKW.replace(/,/g, '');
                    valAgihKW = parseFloat(valAgihKW);

                    var valhidBajetPTj = document.getElementById('<%=hidTxtBajetPTj.ClientID%>').value
                    
                    if (valhidBajetPTj !== 0) {
                        valhidBajetPTj = valhidBajetPTj.replace(/,/g, '');
                        valhidBajetPTj = parseFloat(valhidBajetPTj);

                        valAgihKW = valAgihKW - valhidBajetPTj;

                    }


                    valAgihKW = valAgihKW + valBajetPTj;
                    var valBakiKW, valBakiKW2
                    valBakiKW = valBajetKW - valAgihKW;
                    valBakiKW2 = valBakiKW;
                   
                    valBakiKW = valBakiKW.toFixed(2) //format numer to 2 decimal //#.00             
                    valBakiKW = formatMoney(valBakiKW) //format number to '#,###.00'
                    document.getElementById('<%=txtBakiKW.ClientID%>').value = valBakiKW

                    valAgihKW = valAgihKW.toFixed(2) //format numer to 2 decimal //#.00             
                    valAgihKW = formatMoney(valAgihKW) //format number to '#,###.00'
                    document.getElementById('<%=txtAgihKW.ClientID%>').value = valAgihKW
                   
                valBajetPTj = valBajetPTj.toFixed(2) //format numer to 2 decimal //#.00             
                valBajetPTj = formatMoney(valBajetPTj) //format number to '#,###.00'

                document.getElementById('<%=txtJumP.ClientID%>').value = valBajetPTj
                    document.getElementById('<%=txtBakiP.ClientID%>').value = valBajetPTj

                    if (valBakiKW2 < 0) {
                        document.getElementById('<%=txtBakiKW.ClientID%>').style.backgroundColor = '#FFD4D4';
                        alert('Amaun Bajet PTj yang dimasukkan melebihi amaun Bajet KW ini! Sila masukkan semula amaun Bajet PTj ini.');
                    }
                    else {
                        document.getElementById('<%=txtBakiKW.ClientID%>').style.backgroundColor = '#FFFFCC';
                    }

                    var grid = document.getElementById('<%=gvObjAm.ClientID %>');
                    var btnSimpan = document.getElementById('<%=lbtnSimpan.ClientID%>')
                    if ((valBajetPTj2 <= 0) || (valBakiKW2 < 0))
                    {
                        debugger;
                        for (i = 0; i < grid.rows.length; i++) {
                            col1 = grid.rows[i].cells[2];
                            for (j = 0; j < col1.childNodes.length; j++) {
                                debugger;
                                if (col1.childNodes[j].type == "text") {
                                    debugger;
                                    col1.childNodes[j].disabled = true
                                }
                            }
                        }
                        btnSimpan.disabled = true
                    }
                    else {
                        for (i = 0; i < grid.rows.length; i++) {
                            col1 = grid.rows[i].cells[2];
                            for (j = 0; j < col1.childNodes.length; j++) {
                                if (col1.childNodes[j].type == "text") {
                                    col1.childNodes[j].disabled = false
                                }
                            }
                        }
                        btnSimpan.disabled = false
                    }
                    
           var valBajetPTj = document.getElementById('<%=txtBajetPTj.ClientID%>').value
                    if (valBajetPTj !== "") {
                        valBajetPTj = valBajetPTj.replace(/,/g, '')
                        valBajetPTj = parseFloat(valBajetPTj)
                        valBajetPTj = fFormatCommas(valBajetPTj);
              var txtbajetptj = document.getElementById('<%=txtBajetPTj.ClientID%>');
                        txtbajetptj.value = valBajetPTj;
          }



                }
                catch (err) {
                    alert (err)
                }              
            }


            function fCalcTotal(obj) {
                try {
                    var valBakiKW;
                    valBakiKW =  document.getElementById('<%=txtBakiKW.ClientID%>').value;
                    valBakiKW = valBakiKW.replace(/,/g, '');
                    valBakiKW = parseFloat(valBakiKW);

                    if (valBakiKW < 0) {
                        alert('Amaun Bajet PTj yang dimasukkan melebihi amaun Bajet KW ini! Sila masukkan semula amaun Bajet PTj ini.');
                        return;
                    }

              var row = obj.parentNode.parentNode;
              var intRowidx = row.rowIndex;
            
              var col1;
              var totBajet = 0;
              var grid = document.getElementById('<%=gvObjAm.ClientID %>');
              var valBajet;
         
              //find all texbox value in gv
        for (i = 0; i < grid.rows.length; i++) {
            col1 = grid.rows[i].cells[2];
            for (j = 0; j < col1.childNodes.length; j++) {
                if (col1.childNodes[j].type == "text") {                  
                    //remove ',' character before it can be parsefloated                
                    valBajet = col1.childNodes[j].value;

                    if (col1.childNodes[j].value=="") {
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

        var totJumPTj = document.getElementById('<%=txtJumP.ClientID%>').value
              totJumPTj = totJumPTj.replace(/,/g, '')
              totJumPTj = parseFloat(totJumPTj);

              var totBaki = totJumPTj - totBajet
              var totBaki2 = totBaki
              totBaki = totBaki.toFixed(2) //format numer to 2 decimal //#.00             
              totBaki = formatMoney(totBaki) //format number to '#,###.00'
        document.getElementById('<%=txtBakiP.ClientID%>').value = totBaki

        totBajet = totBajet.toFixed(2) //format numer to 2 decimal //#.00             
        totBajet = formatMoney(totBajet) //format number to '#,###.00'
        var a = $(".cssTotBajet").text(totBajet); //set Footer label value //Total col 'Bajet'

        document.getElementById('<%=txtAgihanP.ClientID%>').value = totBajet;

        var colBajet;
        colBajet = grid.rows[intRowidx].cells[2];
        valBajet = colBajet.childNodes[1].value; //get col Bajet value        
        valBajet = valBajet.replace(/,/g, '')
        valBajet = parseFloat(valBajet);
     
        var valTB, valKG, valBakiBF
        valTB = parseFloat(grid.rows[intRowidx].cells[3].innerText.replace(/,/g, ''));
        valKG = parseFloat(grid.rows[intRowidx].cells[4].innerText.replace(/,/g, ''));
        valBakiBF = parseFloat(grid.rows[intRowidx].cells[5].innerText.replace(/,/g, ''));

        var valJum = valBajet + valTB + valBakiBF - valKG

        valJum = valJum.toFixed(2) //format numer to 2 decimal //#.00             
        valJum = formatMoney(valJum) //format number to '#,###.00'
        grid.rows[intRowidx].cells[6].innerHTML = valJum; //set col Jumlah value
        
        var gTotJumlah = 0;
        var valgJum;
        
        for (i = 1; i < grid.rows.length - 1; i++) {
            val = grid.rows[i].cells[6].innerText;
            valgJum = parseFloat(val.replace(/,/g, ''));
            gTotJumlah += valgJum;
        }
   
        gTotJumlah = gTotJumlah.toFixed(2) //format numer to 2 decimal //#.00             
        gTotJumlah = formatMoney(gTotJumlah) //format number to '#,###.00'             
        var b = $(".cssTotBajet2").text(gTotJumlah); //set Footer label value //Total col 'Jumlah'
            
              if (totBaki2 < 0) {
                  document.getElementById('<%=txtBakiP.ClientID%>').style.backgroundColor = '#FFD4D4';
                  alert('Amaun yang dimasukkan melebihi had peruntukan untuk Objek Am ini! Sila masukkan semula.')
              }
              else {                 
                  document.getElementById('<%=txtBakiP.ClientID%>').style.backgroundColor = '#FFFFCC';
              }
          }
          catch (err) {             
              alert(err);
          }
      }

      function fFormatCommas(nStr) {
          nStr += '';
          var x = nStr.split('.');
          var x1 = x[0];
          var x2 = x.length > 1 ? '.' + x[1] : '';
          var rgx = /(\d+)(\d{3})/;
          while (rgx.test(x1)) {
             x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
                return x1 + x2;
           }

      function formatMoney(number, places, symbol, thousand, decimal) {
          number = number || 0;
          places = !isNaN(places = Math.abs(places)) ? places : 2;
          symbol = symbol !== undefined ? symbol : "";
          thousand = thousand || ",";
          decimal = decimal || ".";
          var negative = number < 0 ? "-" : "",
              i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
              j = (j = i.length) > 3 ? j % 3 : 0;
          return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
      }
          
        </script>



        <style type="text/css">
    
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>                      
                <div class="panel-body">
                  <div class="row">  
                    <table style="width: 100%;">
                      <tr>
                            <td style="width: 70px">
                                <label class="control-label" for="">Tahun :</label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlTahunAgih" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 100px; height: 21px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                 
                      <tr>
                      <td style="width: 100px; height: 25px;">
                          <label class="control-label" for="">Kumpulan Wang :</label>
                      </td>
                      <td>
                          <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 400px;">
                          </asp:DropDownList>
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
                                <asp:TextBox ID="txtBajetKW" runat="server" ReadOnly="True"  CssClass="form-control rightAlign" style="width: 100px;" BackColor="#FFFFCC"></asp:TextBox>
                            </td>
                            <td style="width: 100px; height: 22px;text-align:right;">
                                <label class="control-label" for="">Agihan (RM) : </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAgihKW" runat="server" ReadOnly="True" CssClass="form-control rightAlign" style="width: 100px;" BackColor="#FFFFCC"></asp:TextBox>
                                <asp:TextBox ID="hidTxtAgihKW" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 100px;display:none;"></asp:TextBox>
                            </td>
                            <td style="width: 100px; height: 22px;text-align:right;">
                                <label class="control-label" for="">Baki (RM) : </label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBakiKW" runat="server" ReadOnly="True" CssClass="form-control rightAlign" style="width: 100px;" BackColor="#FFFFCC"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px; height: 22px;">
                                <asp:TextBox ID="hidTxtIndKW" runat="server"  style="display:none;"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>
                            <td style="width: 100px; height: 22px;text-align:right;">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="width: 100px; height: 22px;text-align:right;">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                        </div>
                    </div>
                  

            <div class="panel panel-default">
              <div class="panel-heading">Agihan Peruntukan PTj</div>
    <div class="panel-body">
        <div class="row">
        <table style="width: 100%;">
                      <tr>
                            <td style="width: 100px">
                                <label class="control-label" for="">
                                PTj :</label></td>
                            <td>
                                <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 600px; height: 21px;">
                                </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                             <td style="width: 100px;height: 25px;">
                                 <label class="control-label" for="">
                                 Kod Operasi :</label></td>
                             <td>
                                 <asp:DropDownList ID="ddlKO" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 400px;">
                                 </asp:DropDownList>
                             </td>
                      </tr>
                         <tr>
                             <td style="width: 100px;height: 25px;">
                                 <label class="control-label" for="">
                                 Kod Projek :</label></td>
                             <td>
                                 <asp:DropDownList ID="ddlKp" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 400px;">
                                 </asp:DropDownList>
                             </td>
                      </tr>
                         <tr>
                             <td >
                                  </td>
                             <td>
                                 &nbsp;</td>
                      </tr>
                         <table style="width:100%;">
                        <tr>
                            <td style="width: 100px;height: 22px;">
                                <label class="control-label" for="">Bajet (RM) :</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBajetPTj" runat="server" CssClass="form-control rightAlign" style="width: 150px;" onkeypress="return isNumberKey(event)" onkeyup="fCalc(this);"></asp:TextBox>
                                <asp:TextBox ID="hidTxtBajetPTj" runat="server" style="display:none;"></asp:TextBox>
                            </td>
                            <td style="width: 150px; ">
                                <label class="control-label" for="">Tambahan (RM) :</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtTambahan" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 150px;"></asp:TextBox>
                            </td>
                            <td>
                                Baki BF :</td>
                            <td>
                                <asp:TextBox ID="txtBF" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 150px;"></asp:TextBox>
                            </td>
                            <td style="width: 100px;">
                                <label class="control-label" for="">Kurangan (RM) :</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtKurangan" runat="server" ReadOnly="True" CssClass="form-control rightAlign" style="width: 150px;" BackColor="#FFFFCC"></asp:TextBox>
                            </td>
                        </tr>                          
                                     <tr>
                                         <td style="height: 10px;">
                                             &nbsp;</td>
                                         <td>
                                             &nbsp;</td>
                                         <td>
                                             </td>
                                         <td>
                                             &nbsp;</td>
                                         <td>
                                             &nbsp;</td>
                                         <td>
                                             &nbsp;</td>
                                     </tr>
                                                   
                             <tr>
                                 <td style="height: 25px;">
                                     <label class="control-label" for="">
                                     Jumlah (RM) :</label> </td>
                                 <td>
                                     <asp:TextBox ID="txtJumP" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 150px;"></asp:TextBox>
                                 </td>
                                 <td>
                                     <label class="control-label" for="">
                                     Agihan (RM) :</label> </td>
                                 <td>
                                     <asp:TextBox ID="txtAgihanP" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 150px;"></asp:TextBox>
                                 </td>
                                 <td>
                                     <label class="control-label" for="">
                                     Baki (RM) :</label> </td>
                                 <td>
                                     <asp:TextBox ID="txtBakiP" runat="server" BackColor="#FFFFCC" CssClass="form-control rightAlign" ReadOnly="True" style="width: 150px;"></asp:TextBox>
                                 </td>
                             </tr>
                                                   
                    </table>
               
                  </table>
            </div>
        </div>


                <div class="panel panel-default" style="width:95%;">
              <div class="panel-heading">Senarai Agihan Peruntukan Objek Am</div>
    <div class="panel-body">

        <div class="GvTopPanel">
                <div style="float:left;margin-top: 8px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                    </div>
            </div>
        <asp:GridView ID="gvObjAm" runat="server" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid" 
            cssclass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" 
            HeaderStyle-BackColor="#6699FF" Height="100%" ShowFooter="True" ShowHeaderWhenEmpty="True" 
            Width="100%">
            <columns>
                <asp:TemplateField HeaderText="Bil">
                    <ItemTemplate>
                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                    </ItemTemplate>
                    <ItemStyle Width="3%" HorizontalAlign="Right" />
                </asp:TemplateField>
                <%--<asp:BoundField DataField="ObjAm" HeaderText="Objek Am" ReadOnly="True">
                <ItemStyle Width="25%" />
                </asp:BoundField>--%>

                <asp:TemplateField HeaderText="Objek Am"  >  
                        <ItemTemplate><asp:Label ID ="ObjAm" runat="server"  Text='<%#Eval("ObjAm")%>'  /></ItemTemplate>  
  
                        <FooterTemplate>                            
                            <div style="text-align:right;font-weight:bold;"><asp:Label Text="Jumlah Besar (RM)" runat="server" /></div>  
                        </FooterTemplate>    
                    </asp:TemplateField> 

                <%--<asp:BoundField DataField="Bajet" HeaderText="Bajet (RM)">
                <ItemStyle Width="10%" HorizontalAlign="Right"/>
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="Bajet (RM)">  
                        <ItemTemplate>
                            <asp:TextBox ID="txtBajet" runat="server" Width="98%" Text='<%#Eval("Bajet")%>' 
                               onkeypress="return isNumberKey(event)" onkeyup="fCalcTotal(this);" CssClass="form-control rightAlign"></asp:TextBox> 
                        </ItemTemplate>  
  
                        <FooterTemplate> 
                            <div style="text-align:right;">  
                            <asp:Label ID="lblTotBajet" runat="server" CssClass="cssTotBajet"/>  
                                </div>
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>

                <%--<asp:BoundField DataField="Tambahan" HeaderText="Tambahan (RM)" ReadOnly="true">
                <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="Tambahan (RM)">  
                        <ItemTemplate><asp:Label ID="lblTB" runat="server" Text='<%#Eval("TB")%>'>  
                            </asp:Label></ItemTemplate>  
  
                        <FooterTemplate>  
                            <div style="text-align:right;"> 
                            <asp:Label ID="lblTotTB" runat="server" />  
                                </div>
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>

                <%--<asp:BoundField DataField="Kurangan" HeaderText="Kurangan (RM)">
                <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="Kurangan (RM)">  
                        <ItemTemplate><asp:Label ID="lblKG" runat="server" Text='<%#Eval("KG")%>'>  
                            </asp:Label></ItemTemplate>  
  
                        <FooterTemplate> 
                            <div style="text-align:right;">  
                            <asp:Label ID="lblTotKG" runat="server" />  
                                </div>
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>

               <%-- <asp:BoundField DataField="BakiBF" HeaderText="Baki BF (RM)" ReadOnly="true">
                <ItemStyle Width="10%" HorizontalAlign="Right" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="Baki BF (RM)">  
                        <ItemTemplate><asp:Label ID="lblBakiBF" runat="server" Text='<%#Eval("BakiBF")%>'>  
                            </asp:Label></ItemTemplate>  
  
                        <FooterTemplate>  
                            <div style="text-align:right;"> 
                            <asp:Label ID="lblTotBakiBF" runat="server" /> 
                                </div> 
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>

                <%--<asp:BoundField DataField="Jumlah" HeaderText="Jumlah (RM)" ItemStyle-HorizontalAlign="Right" ReadOnly="true">
                <ItemStyle Width="10%" />
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="Jumlah (RM)">  
                        <ItemTemplate><asp:Label ID="lblJum" runat="server" Text='<%#Eval("Jumlah")%>'>  
                            </asp:Label></ItemTemplate>  
  
                        <FooterTemplate> 
                            <div style="text-align:right;"> 
                            <asp:Label ID="lblTotJum" runat="server" CssClass="cssTotBajet2"/>  
                                </div>
                        </FooterTemplate>  
                        <ItemStyle HorizontalAlign="Right" Width="12%" />
                    </asp:TemplateField>
                <%--<asp:TemplateField>
                    <EditItemTemplate>
                        <asp:ImageButton ID="btnUpdate" runat="server" CommandName="Update" Height="25px" ImageUrl="~/Images/Save_48x48.png" OnItemCommand="gvSubMenu_ItemCommand" ToolTip="Simpan" Width="20px" />
                        &nbsp;&nbsp;
                        <asp:ImageButton ID="btnCancel0" runat="server" CommandName="Cancel" Height="25px" ImageUrl="~/Images/Cancel16x16.png" OnItemCommand="gvSubMenu_ItemCommand" ToolTip="Batal" Width="20px" />
                        &nbsp;&nbsp;
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="5%" />
                </asp:TemplateField>--%>
            </columns>
            <HeaderStyle BackColor="#6699FF" />
        </asp:GridView>
        
            
                    <div style="text-align:center;margin-bottom:10px;">                           
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm()">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
                                </div>

        </ContentTemplate>
    </asp:UpdatePanel>

   
   
</asp:Content>

