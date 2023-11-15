<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TableTransaksiPotongan.ascx.vb" Inherits="SMKB_Web_Portal.TableTransaksiPotongan" %>
<table>
    <tr>
        <%--<td runat="server" id="lblTajuk"></td>--%>
     <%--   <td style="width: 5%" class="tdbg1">Bil</td>
        <td style="width: 15%" class="tdbg1">No</td>
        <td style="width: 60%" class="tdbg1">Nama</td>
        <td style="width: 20%" class="tdbg1 valuetengah">Amaun</td>--%>
    </tr>
</table>
<div runat="server" class="vot-list">
    <asp:Repeater runat="server" ID="rptone" OnItemDataBound="rptone_ItemDataBound">
        <ItemTemplate> 
        <div class="print-div">
            <table>
                <thead>
                    <tr>
                        <td colspan="3"><div class="header-space">&nbsp;</div></td>
                    </tr>
                    <tr>
                        <td style="width: 15%" class="tdbg1 valuetengah" >No</td>
                        <td style="width: 45%" class="tdbg1 valuetengah">Nama</td>
                        <td style="width: 20%" class="tdbg1"></td>
                        <td style="width: 20%" class="tdbg1 valuetengah">Amaun</td>
                    </tr>
                    <tr>
                        <td colspan="4" class="tdbg1"><span class="underline-text"><%#Eval("Butiran")%></span></td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repeaterDetail" runat="server" OnItemDataBound="repeaterDetail_ItemDataBound">
                                <ItemTemplate>
                                <tr>
                                    <td class="valuetengah"><%#Eval("No")%></td>
                                    <td><%#Eval("Nama")%></td>
                                    <td></td>
                                    <td style="text-align:right"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Amaun")) %> </td>
                                </tr>
                            </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td colspan="4" style="border-bottom: 1px solid #000;"></td>
                    </tr>
                    <tr>
                        <td colspan="2" id="jumlahPekerja" runat="server" style="font-weight: bold"></td>
                        <td colspan="2" id="jumlahAmaun" runat="server" style="text-align:right;font-weight: bold"></td>
                    </tr>
                    <tr>
                        <td colspan="4" style="border-bottom: 1px solid #000;"></td>
                    </tr>
                </tbody>
            </table>
             
        </div>
        </ItemTemplate>
</asp:Repeater>
</div> 