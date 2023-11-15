<%@ Page Title="" Language="vb" AutoEventWireup="false"  CodeBehind="CetakEOT.aspx.vb" Inherits="SMKB_Web_Portal.CetakEOT" %>
<!DOCTYPE html>
  <script>
      function fnCetak() {
          window.print();

      }


  </script>
  <style>

 #dotted-line {
 border: none;
 color: rgb(7, 189, 245);
 margin: 1px 60px;
}

    </style>
<html xmlns="http://www.w3.org/1999/xhtml">

<script type="text/javascript">
    $(document).ready(function () {
        window.print();
    });
</script>
  
 <head runat="server">
    <title>Cetak EOT</title>            
     <style type="text/css">
         .auto-style3 {
             height: 25px;
         }
         .auto-style4 {
             width: 554px;
         }
         .auto-style7 {
             width: 77px;
         }
         .auto-style8 {
             width: 556px;
         }
         .auto-style9 {
             width: 95px;
         }
         .auto-style10 {
             width: 259px;
         }
         .auto-style15 {
             width: 35%;
         }
         .auto-style16 {
             width: 30%;
         }
         .auto-style17 {
             width: 458px;
         }
         .auto-style18 {
             height: 23px;
         }
         .auto-style19 {
             width: 259px;
             height: 23px;
         }
         .auto-style20 {
             width: 77px;
             height: 23px;
         }
         .auto-style21 {
             width: 556px;
             height: 23px;
         }
         .auto-style22 {
             width: 95px;
             height: 23px;
         }
         .auto-style23 {
             width: 35%;
             height: 23px;
         }
         .auto-style24 {
             width: 1277px;
         }
     </style>
</head>
<body>


<form id="form1" runat="server">
      
 <table width="100%" border="1">
  <tr>      
    <td>&nbsp;</td>
            
    <td>
        <div style="text-align: center;">
        <table width="100%" border="1">        
        
            <tr>
            <td><img src="../Images/logo.png" /></td>
            </tr>
                        
        </table>
        </div>
    </td>
           
    <td>
        <table width="100%" border="1">
          <tr>
            <td colspan="2" style="text-align: center;">PERMOHONAN TUNTUTAN KERJA LEBIH MASA</td>
          </tr>
          <tr>
            <td colspan="2" style="text-align: center;">UNIVERSITI TEKNIKAL MALAYSIA MELAKA</td>
          </tr>
          <tr>
            <td style="text-align: right" class="auto-style4">Pejabat/Jabatan/Fakulti : </td>
               <td><label class="" id="lblPejabat" name="lblPejabat" runat="server"></label></td>
          </tr>
          <tr>
            <td style="text-align: right" class="auto-style4">BULAN :</td>
               <td class="auto-style3"><label class="" id="lblBulan" name="lblBulan" runat="server"></label></td>
          </tr>
        </table>
    </td>
   <td>&nbsp;</td>
  </tr>
</table>
<div id="dotted-line"><hr /></div>     
        <br />
        <div>
            <table  class="form-control"  style="width: 100%;">
                <tr>
                    <td style="width:10%" >A.</td>
                    <td style="width:40%" colspan="3">Pengesahan Kerja(untuk diisi oleh staf dan disokong oleh penyelia)</td>
                  
                </tr>
                
                <tr>
                    <td >Peringatan :</td>
                    <td>No.Mohon</td>
                    <td>Tel(Samb)</td>
                    <td>&nbsp;</td>
                </tr>
              
            </table>
        </div>
<div id="dotted-line"><hr /></div>     
 <div>
            <table  class="form-control"  style="width: 100%;">
                <tr>
                    <td class="auto-style10">A.</td>
                    <td colspan="3">Kelulusanpermohonan kerja lebih masa (Untuk diisi oleh pemohon dan pegawai penyelia)</td>
                  
                </tr>
                <tr>
                    <td class="auto-style19"></td>
                    <td class="auto-style20">No. Staf</td>
                    <td class="auto-style21" colspan="3"><label class="" id="Nostaf" name="Nostaf" runat="server"></label></td>
                  
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style7">Nama</td>
                    <td class="auto-style8" colspan="3"><label class="" id="lblNama" name="lblNama" runat="server"></label></td>
                    
                    
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style7">Jawatan</td>
                    <td class="auto-style8" colspan="3"><label class="" id="lblJawtan" name="lblJawtan" runat="server"></label></td>
                  
                </tr>
                <tr>
                    <td class="auto-style10">&nbsp;</td>
                    <td class="auto-style7">No.Mohon</td>
                    <td class="auto-style8"><label class="" id="lblNoMohon" name="lblNoMohon" runat="server"></label></td>
                    <td class="auto-style9">Tel(Samb)</td>
                    <td><label class="" id="Label1" name="lblNoTel" runat="server"></label></td>
                </tr>
                <tr>
                    <td colspan="5">
                            <div id="dotted-line"><hr></div>    
                    </td>

                </tr>      
                <tr>
                    <td class="auto-style10">B.</td>
                    <td colspan="3">Pengesahan Kerja (untuk diisi oleh staf dan disokong oleh penyelia) </td>
                  
                </tr>
            </table>
        </div>
<table class="form-control"  style="width: 100%;">
   <tr>
      <td align="left" style="display: inline;font-weight:bold" colspan="2">Peringatan :</td>
      
    </tr>
    <tr>
        <td align="center" style="display: inline;font-weight:bold" class="auto-style15" colspan="2"><div align="left" class="style8">* Jam / Masa : 0600 - 2200 (kadar siang) 2200 - 0600 (kadar malam) </div></td>
    </tr>
    <tr>
        <td align="left" valign="top" class="auto-style17"><i>Kadar tuntutan kerja lebih masa :</i></td>
        <td align="center" class="auto-style16">
            <table  class="form-control"  style="width: 50%;">
                <tr>
                    <td>Status</td>
                    <td>Hari tuntutan</td>
                    <td>Kadar</td>
                </tr>
                <tr>
                    <td>1</td>
                    <td>Biasa Siang</td>
                    <td>1.125</td>
                </tr>
                <tr>
                    <td class="auto-style18">2</td>
                    <td class="auto-style18">Biasa Malam</td>
                    <td class="auto-style18">1.250</td>
                </tr>
                <tr>
                    <td>3</td>
                    <td>Hujung Minggu Siang</td>
                    <td>1.250</td>
                </tr>
                <tr>
                    <td>4</td>
                    <td>Hujung Minggu Malam</td>
                    <td>1.500</td>
                </tr>
                <tr>
                    <td>5</td>
                    <td>Cuti Umum Siang</td>
                    <td>1.750</td>
                </tr>
                 <tr>
                    <td>6</td>
                    <td>Cuti Umum Malam</td>
                    <td>2.000</td>
                </tr>

            </table>

        </td>
      
     
</table>
<br />
    <div>
        <asp:GridView ID="EOTransaksi" runat="server" Width="100%" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField HeaderText="Tarikh Tuntut" DataField="Perkara" SortExpression="Perkara" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Justify"/>
                <asp:BoundField HeaderText="Jam Mula" DataField="Kuantiti" SortExpression="Kuantiti" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField HeaderText="Jam Tamat" DataField="Kadar_Harga" SortExpression="Kadar_Harga" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField HeaderText="jam Tuntut" DataField="Cukai" SortExpression="Cukai" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="10%"  ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField HeaderText="Status Hari" DataField="Diskaun" SortExpression="Diskaun" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" />
              
            </Columns>
            
        </asp:GridView>
        <br />
     </div>
<div>
<table>
    <tr>
        <td align="center" style="display: inline;font-weight:bold" class="auto-style23"><div align="center" class="auto-style24">Butir-Butir Borang Tuntutan</div></td>
    </tr>

</table>

</div>
    <br />
    <div> 
         <asp:GridView ID="EOTButir" runat="server" Width="100%" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField HeaderText="Tarikh" DataField="Perkara" SortExpression="Perkara" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Justify"/>
                    <asp:BoundField HeaderText="Tujuan" DataField="Kuantiti" SortExpression="Kuantiti" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField HeaderText="Catatn" DataField="Kadar_Harga" SortExpression="Kadar_Harga" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"/>
               
                </Columns>
            
            </asp:GridView>
      
    </div>
<br />

    <div style="text-align:center">              
                            <asp:LinkButton ID="LnkCetak" CssClass="btn btn-info" OnClientClick="return fnCetak()"  runat   ="server" >Cetak</asp:LinkButton>                  
                            &nbsp;
                            <asp:LinkButton ID="lnkNext" CssClass="btn btn-info" OnClick="return fnNext()"  runat   ="server" >Next</asp:LinkButton>                  
                         </div>

</form>

</body>
</html>

