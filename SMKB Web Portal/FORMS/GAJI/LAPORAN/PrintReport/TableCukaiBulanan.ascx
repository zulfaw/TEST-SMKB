<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TableCukaiBulanan.ascx.vb" Inherits="SMKB_Web_Portal.TableCukaiBulanan"%>
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
                                <td style="width: 10%" class="tdbg1">No</td>
                                <td style="width: 30%" class="tdbg1">Nama</td>
                                <td style="width: 15%" class="tdbg1">No Cukai</td>
                                <td style="width: 15%" class="tdbg1">No Kad Pengenalan</td>
                                <td style="width: 15%" class="tdbg1">Kategori</td>
                                <td style="width: 15%" class="tdbg1">Amaun</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repeaterDetail" runat="server">
                                     <ItemTemplate>
                                     <tr>
                                         <td><%#Eval("No_Staf")%></td>
                                         <td><%#Eval("Nama")%></td>
                                         <td><%#Eval("No_Cukai")%></td>
                                         <td><%#Eval("No_KP")%></td>
                                         <td><%#Eval("Kategori_Cukai")%></td>
                                         <td><%#Eval("Amaun")%></td>
                                     </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                            <script>
                            </script>
                        </tbody>
                    </table>
                </div>
             </ItemTemplate>
        </asp:Repeater>

</div> 