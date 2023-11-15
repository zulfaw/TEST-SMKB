<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TableVot.ascx.vb" Inherits="SMKB_Web_Portal.TableVot" %>
<table>
    <tr>
        <td runat="server" id="lblTajuk"></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table>
<div runat="server" class="vot-list">

    <table runat="server" id="tableData" class="vot-table">
        <tr>
            <th style="width: 10%"></th>
            <th style="width: 40%"></th>
            <th class="valuekanan" style="width: 25%"></th>
            <th class="valuekanan" style="width: 25%"></th>
        </tr>

        <tr id="showemolumen" runat="server">
            <td class="headerkiri" runat="server"></td>
            <td class="align-left" runat="server"></td>
            <td class="valuekanan" runat="server"></td>
            <td class="valuekanan" runat="server"></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td class="valuekanan " runat="server" id="amaunEmo"></td>
            <td class="valuekanan " runat="server" id="amaun2Emo"  ></td>
        </tr>

    </table>
</div> <%--close header emolumen--%>