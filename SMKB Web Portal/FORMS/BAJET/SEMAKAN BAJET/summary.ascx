<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="summary.ascx.vb" Inherits="SMKB_Web_Portal.summary" %>

<style type="text/css">
    .auto-style1 {
        width: 170px;
    }
    .auto-style2 {
        width: 130px;
    }
</style>

<table border="1" class="table table table-borderless" style="width: 30%;">
    <tr style="background-color: #FECB18">
        <td class="text-center" style="border: 1px solid black;">&nbsp;</td>
        <td class="text-center" style="border: 1px solid black;"><strong>Jumlah (RM)</strong></td>
        <td class="auto-style2" style="border: 1px solid black;"><strong>Perbezaan Jumlah KPT</strong></td>
    </tr>
    <tr>
        <td class="auto-style1" style="border: 1px solid black;">Permohonan</td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblAmaunMohon" runat="server"></asp:Label></td>
        <td class="text-right" style="border: 1px solid black;">-</td>
    </tr>
    <tr>
        <td class="auto-style1" style="border: 1px solid black;">Lulus KPT</td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblLulusKPT" runat="server"/></td>
        <td class="auto-style2" style="border: 1px solid black;">-</td>
    </tr>
    <tr>
        <td class="auto-style7" style="border: 1px solid black;">Agihan Bendahari</td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblAmaunBend" runat="server"/></td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblBezaBen" runat="server"/></td>
    </tr>
    <tr>
        <td class="auto-style1" style="border: 1px solid black;">Agihan NC</td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblAmaunNC" runat="server"/></td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblBezaNC" runat="server"/></td>
    </tr>
    <tr>
        <td class="auto-style1" style="border: 1px solid black;">Agihan LPU</td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblAmaunLPU" runat="server"/></td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblBezaLPU" runat="server"/></td>
    </tr>
    <tr>
        <td class="auto-style1" style="border: 1px solid black;">Geran Kerajaan</td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblAmaunGeran" runat="server"/></td>
        <td class="text-right" style="border: 1px solid black;">-</td>
    </tr>
     <tr>
        <td class="auto-style1" style="border: 1px solid black;">Reserved</td>
        <td class="text-right" style="border: 1px solid black;"><asp:Label ID="lblAmaunReserved" runat="server"/></td>
        <td class="text-right" style="border: 1px solid black;">-</td>
    </tr>
</table>

