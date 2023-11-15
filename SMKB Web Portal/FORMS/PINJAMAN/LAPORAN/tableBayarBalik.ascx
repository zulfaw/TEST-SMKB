<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tableBayarBalik.ascx.vb" Inherits="SMKB_Web_Portal.tableBayarBalik" %>
<style>

    table {
            width: 75%; /* Set the table width to 75% of its container */
            border-collapse: collapse; /* Optional: This removes the spacing between table cells */
        }


</style>
<table>
    <tr>
        <td runat="server" id="lblTajuk"></td>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table>
<div runat="server" class="vot-list">

    <table runat="server" id="tableData">
       
        <tr>
            <th style="width: 1%">Bill</th>
            <th style="width: 5%">Ansuran</th>
            <th style="width: 5%">Faedah</th>
            <th style="width: 5%">Pokok</th>
            <th style="width: 5%">Jumlah Faedah</th>
            <th style="width: 5%">Jumlah Pokok</th>
            <th style="width: 5%">Baki Pokok</th>
        </tr>

       
             
    </table>
</div> <%--close header emolumen--%>
