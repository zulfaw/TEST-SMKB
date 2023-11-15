<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="summary_ABM7.ascx.vb" Inherits="SMKB_Web_Portal.summary2_ABM7" %>

                                    <style type="text/css">
                                        .auto-style1 {
                                            width: 1028px;
                                        }
                                        .auto-style4 {
                                            width: 100px;
                                            text-align: center;
                                        }
                                        .auto-style5 {
                                            width: 1028px;
                                            text-align: center;
                                        }
                                        </style>

                                    <table border="1" style="width:70%" align="right">
                                        <tr style="background-color: #FECB18">
                                            <td class="auto-style5" style="border: 1px solid black; align-items:center" ><strong>Dasar</strong></td>
                                            <td class="auto-style4" style="border: 1px solid black;"><strong>ABM - 7</strong></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" style="border: 1px solid black;">Keseluruhan</td>
                                            <td style="text-align:center"><asp:LinkButton ID="lblCetak" runat="server" CssClass="HelpDeskLink" ToolTip="Lihat" Text="Cetak" >											
                                             <i class="fas fa-print fa-lg"></i></asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" style="border: 1px solid black;">Dasar Sedia Ada</td>
                                            <td style="text-align:center"><asp:LinkButton ID="lblCetakdS" runat="server" CssClass="HelpDeskLink" ToolTip="Lihat" Text="Cetak" >											
                                             <i class="fas fa-print fa-lg"></i></asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" style="border: 1px solid black;">Dasar Baru</td>
                                            <td style="text-align:center"><asp:LinkButton ID="lblCetakdB" runat="server" CssClass="HelpDeskLink" ToolTip="Lihat" Text="Cetak" >											
                                             <i class="fas fa-print fa-lg"></i></asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style1" style="border: 1px solid black;">One-Off</td>
                                            <td style="text-align:center"><asp:LinkButton ID="lblCetakoff" runat="server" CssClass="HelpDeskLink" ToolTip="Lihat" Text="Cetak" >											
                                             <i class="fas fa-print fa-lg"></i></asp:LinkButton></td>
                                        </tr>
                                    </table>

