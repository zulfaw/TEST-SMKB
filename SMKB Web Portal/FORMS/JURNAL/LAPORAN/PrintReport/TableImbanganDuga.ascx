<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TableImbanganDuga.ascx.vb" Inherits="SMKB_Web_Portal.TableImbanganDuga" %>
<table>
    <tr>
        <td runat="server" id="lblTajuk"></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table>
<div runat="server" class="vot-list">
         <asp:Repeater runat="server" ID="rptone" OnItemDataBound="rptone_ItemDataBound">
             <ItemTemplate> 
                <div class="print-div">
                    <table>
                        <thead>
                            <tr><td colspan="6">
                                <div class="header-space">&nbsp;</div>
                                </td></tr>
                            <tr>
                                <td style="width: 10%" class="tdbg1">KW</td>
                                <td style="width: 30%" class="tdbg1"><%#Eval("Butiran")%> </td>
                                <td style="width: 30%" class="tdbg1 valuetengah" colspan ="2">Terkumpul</td>
                                <td style="width: 30%" class="tdbg1 valuetengah" colspan="2">Semasa</td>
                            </tr>
                             <tr>
                                <td style="width: 10%" class="tdbg1">Vot</td>
                                <td style="width: 30%" class="tdbg1">Butiran</td>
                                <td style="width: 15%" class="tdbg1 valuetengah">Debit (RM)</td>
                                <td style="width: 15%" class="tdbg1 valuetengah">Kredit (RM)</td>
                                <td style="width: 15%" class="tdbg1 valuetengah">Debit (RM)</td>
                                <td style="width: 15%" class="tdbg1 valuetengah">Kredit (RM)</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repeaterDetail" runat="server">
                                     <ItemTemplate>
                                     <tr>
                                         <td><%#Eval("kodvot")%></td>
                                         <td><%#Eval("ButiranDetail")%></td>
                                         <td class="amaunKumpulDebit<%#Eval("KodKW_RptTwo")%>"><%#Eval("amaunTerkumpulDebit")%></td>
                                         <td class="amaunKumpulKredit<%#Eval("KodKW_RptTwo")%>"><%#Eval("amaunTerkumpulKredit")%></td>
                                         <td class="amaunSemasaDebit<%#Eval("KodKW_RptTwo")%>"><%#Eval("amaunSemasaDebit")%></td>
                                         <td class="amaunSemasaKredit<%#Eval("KodKW_RptTwo")%>"><%#Eval("amaunSemasaKredit")%></td>
                                     </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="2" style="text-align:right;padding-right:30px;">Jumlah</td>
                                <td id="jumlahKumpulDebit<%#Eval("Kod_Kump_Wang")%>"></td>
                                <td id="jumlahKumpulKredit<%#Eval("Kod_Kump_Wang")%>"></td>
                                <td id="jumlahSemasaDebit<%#Eval("Kod_Kump_Wang")%>"></td>
                                <td id="jumlahSemasaKredit<%#Eval("Kod_Kump_Wang")%>"></td>
                            </tr>

                            <script>
                                var $listKumpulDebit = $('.amaunKumpulDebit<%#Eval("Kod_Kump_Wang")%>');
                                var $listKumpulKredit = $('.amaunKumpulKredit<%#Eval("Kod_Kump_Wang")%>');
                                var $listSemasaDebit = $('.amaunSemasaDebit<%#Eval("Kod_Kump_Wang")%>');
                                var $listSemasaKredit = $('.amaunSemasaKredit<%#Eval("Kod_Kump_Wang")%>');

                                var $jumKumpulDebit = $('#jumlahKumpulDebit<%#Eval("Kod_Kump_Wang")%>');
                                var $jumKumpulKredit = $('#jumlahKumpulKredit<%#Eval("Kod_Kump_Wang")%>');
                                var $jumSemasaDebit = $('#jumlahSemasaDebit<%#Eval("Kod_Kump_Wang")%>');
                                var $jumSemasaKredit = $('#jumlahSemasaKredit<%#Eval("Kod_Kump_Wang")%>');
                                var total = 0.00;

                                $listKumpulDebit.each(function (ind, debitObject) {
                                   
                                    if (isNaN($(debitObject).html())) {
                                        return;
                                    }

                                    total += parseFloat($(debitObject).html());
                                });
                                
                                $jumKumpulDebit.html(total.toFixed(2));

                                /////////////////////////////////
                                total = 0.00;
                                $listKumpulKredit.each(function (ind, obj) {

                                    if (isNaN($(obj).html())) {
                                        return;
                                    }

                                    total += parseFloat($(obj).html());
                                });

                                $jumKumpulKredit.html(total.toFixed(2));

                                /////////////////////////////////
                                total = 0.00;
                                $listSemasaDebit.each(function (ind, obj) {

                                    if (isNaN($(obj).html())) {
                                        return;
                                    }

                                    total += parseFloat($(obj).html());
                                });

                                $jumSemasaDebit.html(total.toFixed(2));

                                /////////////////////////////////
                                total = 0.00;
                                $listSemasaKredit.each(function (ind, obj) {

                                    if (isNaN($(obj).html())) {
                                        return;
                                    }

                                    total += parseFloat($(obj).html());
                                });

                                $jumSemasaKredit.html(total.toFixed(2));

                            </script>
                        </tbody>
                    </table>
                </div>
             </ItemTemplate>
        </asp:Repeater>

</div> 