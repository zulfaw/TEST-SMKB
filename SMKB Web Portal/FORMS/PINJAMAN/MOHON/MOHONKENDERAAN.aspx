<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="MOHONKENDERAAN.aspx.vb" Inherits="SMKB_Web_Portal.MOHONKENDERAAN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <table>
        <tbody>
            <tr>
                <td>BAHAGIAN II - Butir-Butir Pembelian</td>
            </tr>
        </tbody>
    </table>
    <table style="margin: 10px">
        <tbody>
            <tr>
                <td width="20%">Nama penuh</td>
                <td>:</td>
                <td><asp:Label ID="lblNamaPenuh" runat="server" Text="lblNamaPenuh"></asp:Label></td>
                <td width="60%"></td>
                <td width="20%">No. Pekerja</td>
                <td>:</td>
                <td><asp:Label ID="lblNoPekerja" runat="server" Text="lblNoPerkerja"></asp:Label></td>
            </tr>
            <tr>
                <td width="20%">No. Kad Pengenalan</td>
                <td>:</td>
                <td><asp:Label ID="lblNoKadPengenalan" runat="server" Text="lblNoKadPengenalan"></asp:Label></td>
                <td width="60%"></td>
                <td width="20%">No. Pinjaman Sementara</td>
                <td>:</td>
                <td><asp:Label ID="lblNoPinjamanSementara" runat="server" Text="lblNoPinjamanSementara"></asp:Label></td>
            </tr>
        </tbody>
    </table>

    <table>
         <table style="margin: 10px">
        <tbody>
            <tr>
                <td width="20%">Jenis Pinjaman</td>
                <td>:</td>
                <td style="width: 178px"><asp:DropDownList ID="ddlJenisPinjaman" runat="server" Height="16px" Width="368px"></asp:DropDownList></td> 
                <td width="60%"></td>
                <td width="20%">Jumlah yang dipohon</td>
                <td>:</td>
                <td><asp:DropDownList ID="ddlJumlahDipohon" runat="server" Height="16px" Width="157px"></asp:DropDownList></td>

            </tr>
            <tr>
                <td width="20%">Tempoh Pembayaran Balik Selama</td>
                <td>:</td>
                <td style="width: 178px"><asp:DropDownList ID="ddlTPBS" runat="server" Height="16px" Width="193px"></asp:DropDownList></td> 
                <td width="60%"></td>
                <td width="20%">Semak Kelayakkan</td>
                <td>:</td>
                <td>
                    <asp:Button ID="btnSemakKelayakkan" runat="server" Height="22px" Text="Semak Kelayakkan" Width="149px" />
                </td>
            </tr>
        </tbody>
    </table>

        <table>
        <tbody>
            <tr>
                <td>Butir - butir kenderaan yang hendak dibeli</td>
            </tr>
        </tbody>
    </table>

        <table>
            <tbody>
                <tr>
                <td width="20%" style="height: 43px">(i)- Jenama</td>
                <td style="height: 43px">:</td>
                <td style="width: 253px; height: 43px;"><asp:DropDownList ID="ddlJenamaKenderaan" runat="server" Height="18px" Width="299px"></asp:DropDownList></td> 
                <td style="height: 43px; width: 30%;"></td>
                <td style="height: 43px; width: 14%;">(ii)- Sukatan silinder (5.5)</td>
                <td style="height: 43px">:</td>
                <td style="height: 43px"><asp:DropDownList ID="ddlSukatanSilinder" runat="server" Height="16px" Width="157px"></asp:DropDownList></td>
                </tr>

                <tr>
                    <td width="20%">Model</td>
                <td>:</td>
                <td style="width: 253px"><asp:DropDownList ID="ddlModelKenderaan" runat="server" Height="16px" Width="302px"></asp:DropDownList></td> 
                <td style="width: 30%"></td>
                <td style="width: 14%">No Enjin</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="148px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td width="20%">No Pendaftaran</td>
                <td>:</td>
                <td style="width: 253px"><asp:TextBox ID="TextBox6" runat="server" Width="292px" Height="16px"></asp:TextBox></td> 
                <td style="width: 30%"></td>
                <td style="width: 14%">No Chasis</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Width="150px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td width="20%">Kelas Kenderaan</td>
                <td>:</td>
                <td style="width: 253px"><asp:DropDownList ID="DropDownList1" runat="server" Height="16px" Width="301px"></asp:DropDownList></td> 
                <td style="width: 30%">&nbsp;</td>
                <td style="width: 14%">Bahan Bakar</td>
                <td>:</td>
                <td><asp:DropDownList ID="ddlBahanBakar" runat="server" Height="16px" Width="157px"></asp:DropDownList></td>
                </tr>

                <tr>
                    <td width="20%">Tahun Dibuat</td>
                <td>:</td>
                <td style="width: 253px"><asp:DropDownList ID="DropDownList3" runat="server" Height="16px" Width="186px"></asp:DropDownList></td> 
                <td style="width: 30%"></td>
                <td style="width: 14%">Bahan Kenderaan</td>
                <td>:</td>
                <td><asp:DropDownList ID="ddlBahanKenderaan" runat="server" Height="16px" Width="157px"></asp:DropDownList></td>
                </tr>

                <tr>
                <td width="20%">(iii)- Harga Bersih</td>
                <td>:</td>
               <td style="width: 253px">RM<asp:TextBox ID="TextBox3" runat="server" Width="169px"></asp:TextBox></td>
                <td style="width: 30%">&nbsp;</td>
                <td style="width: 14%">(iv)- Baru atau sudah pakai</td>
                <td>:</td>
   
                </tr>
            </tbody>
        </table>

        <table>
            <tbody>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" Width="452px">
                        </asp:GridView>
                    </td>
                   
                </tr>

            </tbody>
        </table>

        <table>
            <tbody>

                <tr>
                    <td style="width: 30%">Jenis Kenderaan uang digunakan sekarang semasa menjalankan tugas rasmi</td>
                <td>:</td>
                <td><asp:TextBox ID="TextBox4" runat="server" Width="232px" Height="56px"></asp:TextBox></td>
                </tr>
                <tr>
                    
                <td style="width: 30%">Jenis Kenderaan yang digunakan sekarang semasa menjalankan tugas rasmi</td>
                <td>:</td>
                <td><asp:TextBox ID="TextBox5" runat="server" Width="237px" Height="56px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnSimpan" runat="server" Text="Simpan" Width="149px" />
                    </td>
                </tr>

            </tbody>
        </table>
   
    


</asp:Content>
