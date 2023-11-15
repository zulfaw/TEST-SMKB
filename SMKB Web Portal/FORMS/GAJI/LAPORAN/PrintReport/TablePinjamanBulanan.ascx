<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TablePinjamanBulanan.ascx.vb" Inherits="SMKB_Web_Portal.TablePinjamanBulanan" %>
<table>
    <tr>
        <td runat="server" id="lblTajuk"></td>
       <%-- <td></td>
        <td></td>
        <td></td>--%>
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
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: center; width: 15%;" class="tdbg1">Kod Akaun Pinjaman</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1">No</td>
                                <td style="text-align: center; width: 20%;" class="tdbg1">Nama</td>
                                <td style="text-align: center; width: 15%;" class="tdbg1">Pinjaman</td>
                                <td style="text-align: center; width: 15%;" class="tdbg1">Potongan Bulanan</td>
                                <td style="text-align: center; width: 15%;" class="tdbg1">Pokok</td>
                                <td style="text-align: center; width: 15%;" class="tdbg1">Untung</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repeaterDetail" runat="server" OnItemDataBound="repeaterDetail_ItemDataBound">
                                     <ItemTemplate>
                                     <tr>
                                         <td style="text-align: center;width: 15%;"><%#Eval("No_Trans")%></td>
                                         <td style="text-align: center;width: 5%;"><%#Eval("No_Staf")%></td>
                                         <td style="width: 20%;"><%#Eval("Nama")%></td>
                                         <td style="text-align: right;width: 15%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Pinjaman")) %></td>
                                         <td style="text-align: right;width: 15%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Pot_bulanan")) %></td>
                                         <td style="text-align: right;width: 15%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Pokok")) %></td>
                                         <td style="text-align: right;width: 15%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Untung")) %></td>
                                     </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="7" style="border-bottom: 1px solid #000;"></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align:right;font-weight: bold">Jumlah</td>
                                <td id="jumlahPinjaman" runat="server" style="text-align:right;font-weight: bold"></td>
                                <td id="jumlahPot_bulanan" runat="server" style="text-align:right;font-weight: bold"></td>
                                <td id="jumlahPokok" runat="server" style="text-align:right;font-weight: bold"></td>
                                <td id="jumlahUntung" runat="server" style="text-align:right;font-weight: bold"></td>
                            </tr>
                            <tr>
                                <td colspan="7" style="border-bottom: 1px solid #000;"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
             </ItemTemplate>
        </asp:Repeater>

</div> 