<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TableVotbaru.ascx.vb" Inherits="SMKB_Web_Portal.TableVotbaru" %>

<table>
    <tr>
        <td runat="server" id="lblTajuk"><strong></strong></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table>
<div runat="server" class="vot-list">
    <table runat="server" id="tableData" class="vot-table">
        <tr>
            <th style="width: 10%"></th>
            <th style="width: 39%"></th>
            <th class="valuekanan" style="width: 17%"></th>
            <th class="valuekanan" style="width: 17%"></th>
            <th style="width: 17%"></th>
        </tr>
    </table>
</div> 