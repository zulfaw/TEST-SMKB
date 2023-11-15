<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="summary2.ascx.vb" Inherits="SMKB_Web_Portal.summary2" %>

                                    <style type="text/css">
                                        .auto-style1 {
                                            width: 964px;
                                        }
                                        .auto-style2 {
                                            width: 1106px;
                                        }
                                        .auto-style4 {
                                            width: 1106px;
                                            text-align: center;
                                        }
                                        .auto-style5 {
                                            width: 964px;
                                            text-align: center;
                                        }
                                    </style>

                                    <table border="1" style="width:50%">
                                        <tr style="background-color: #FECB18">
                                            <td class="auto-style5" style="border: 1px solid black; align-items:center" ><strong>Peringkat</strong></td>
                                            <td class="auto-style4" style="border: 1px solid black;"><strong>Jumlah (RM)</strong></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" style="border: 1px solid black;">Permohonan</td>
                                            <td class="auto-style2" style="border: 1px solid black;text-align-last:right"><asp:Label ID="lblAmaunMohon" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" style="border: 1px solid black;">Agihan Bendahari</td>
                                            <td class="auto-style2" style="border: 1px solid black;text-align-last:right"><asp:Label ID="lblAmaunBend" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" style="border: 1px solid black;">Agihan NC</td>
                                            <td class="auto-style2" style="border: 1px solid black;text-align-last:right"><asp:Label ID="lblAmaunNC" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" style="border: 1px solid black;">Agihan LPU</td>
                                            <td class="auto-style2" style="border: 1px solid black;text-align-last:right"><asp:Label ID="lblAmaunLPU" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>

