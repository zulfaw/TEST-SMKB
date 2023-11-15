<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CetakSuratPeringatan.aspx.vb" Inherits="SMKB_Web_Portal.CetakSuratPeringatan" %>

 <script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>
 <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
 <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
 <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N" crossorigin="anonymous">

    <style>
        body {
            width: 210mm; /* A4 width */
            height: 297mm; /* A4 height */
            margin: 0 auto; /* Center content on page */
        }
        tr {
            page-break-inside: avoid;
}
        .align-right {
            text-align: right;
        }

        .center-align {
            text-align: center;
        }
        .pheader {
            font-weight: bold;
           font-size: 13px;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }

        .pheader2 {
            font-size: 14px;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }

        .pheader3 {
            font-size: 20px;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }
        th, td {
            padding: 1px;
        }
          .gridview-style {
            border-collapse: collapse;
        }

        .gridview-style th, .gridview-style td {
            border: 1px solid black;
            padding: 10px;
        }

         @media print {
             table.table-striped > tbody > tr:nth-child(odd) {
                background-color: rgba(0, 0, 0, 0.05); /* Adjust as needed */
            }
            @page {
                size: A4; /* or letter, legal, etc. */
                margin: 1cm; /* adjust margins as needed */
            }

            .auto-style1 {
                width: 25%;
            }

            .auto-style2 {
                width: 48%;
            }
        }

    </style>
<body>
     <div id="PermohonanTab" class="tabcontent" style="display: block">
          <div id="headerreport">
             <table style="width:100%"> 
                  <tr>
                     <td style="width: 20%;text-align: right; padding-right:15px" >
                         <asp:Image ID="imgMyImage" runat="server" style="width:160px; Height:100px;text-align:right"/>
                     </td>
                      <td style="text-align:left; padding-left:15px; position: relative; font-size: 14px">
                         <div style="border-left: 2px solid #000; height: 90%; position: absolute; left: 0;"></div>
                         <p style="margin:0px;" class="pheader"><asp:label ID="lblNamaKorporat" runat="server"></asp:label></p>
                         <p class="pheader2" style="margin:0px;"><asp:label ID="lblAlamatKorporat" runat="server"></asp:label></p>
                          <p class="pheader2" style="margin:0px;"><asp:label ID="lblPoskod" runat="server"></asp:label></p>
                          <p class="pheader2" style="margin:0px;"><asp:label ID="lblNegara" runat="server"></asp:label></p>
                     </td>
                 </tr>
             </table>
              <hr style="border-top: 1px solid #000; margin-top: 10px; margin-bottom: 10px;">
         </div> <%--close header report--%>
          <div id="bodyreport">
            <table style="width:100%"> 
                <tr>
                    <td colspan="3" style="text-align: center;">
                        <p class="pheader3" style="word-spacing: 6px"><strong>PEJABAT BENDAHARI</strong></p>
                        <p style="font-size:15px!important;margin:0px;"><asp:label ID="lblNoTelFaks" runat="server"></asp:label></p>
                        <p style="font-size:15px!important;margin:0px;"><asp:label ID="lblEmailKorporat" runat="server"></asp:label></p>
                   </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                       
                   </td>
                    <td style="width: 30%; font-size:16px!important; text-align: left">
                         <span>Rujukan Kami (Our Ref):</span><br />
                         <span>UTeM(T).400-3/4/2 Jld 8 (32)</span><br />
                         <span>Rujukan Tuan (Your Ref):</span>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left!important">
                        <p style="margin:0px; font-size: 20px"><asp:label ID="lblNamaPenghutang" runat="server"></asp:label></p>
                        <p style="margin:0px;font-size: 20px"><asp:label ID="lblAlamatPenghutang" runat="server"></asp:label></p>
                         <p style="margin:0px;font-size: 20px"><asp:label ID="lblBandarPenghutang" runat="server"></asp:label></p>
                         <p style="margin:0px;font-size: 20px"><asp:label ID="lblNegaraPenghutang" runat="server"></asp:label></p>
                   </td>
                    <td style="width: 30%">

                    </td>
                     <td style="width: 20%;">
                        <div style="border: 2px solid black; padding: 5px; text-align: center; margin-left: auto; margin-right: 0; margin-bottom: 70px">
                            <strong>PERINGATAN 1</strong>
                        </div>
                    </td>
                </tr>
            </table>
              <div id="isiSurat" style="font-size:20px">
                  <br /><br />
                  <p>Tuan,</p>
                  <asp:Label ID="lblBilYear" runat="server" CssClass="pheader3"></asp:Label>
                  <p>Perkara di atas adalah dirujuk.</p>
                  <p>Dengan ini dimaklumkan bahawa mengikut rekod kami, pihak tuan masih belum menjelaskan tuntutan seperti berikut:</p><br />
                  <form id="gvBil" runat="server">
                      <asp:GridView ID="GridView1" runat="server" CssClass="w-100 gridview-style" AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" style="border-collapse: collapse;">
                           <Columns>
                                <asp:BoundField DataField="bil" HeaderText="Bil" />
                                <asp:BoundField DataField="No_Bil" HeaderText="No. Bil Tuntutan" />
                                <asp:BoundField DataField="Tkh_Bil" HeaderText="Tarikh Bil" />
                                <asp:BoundField DataField="Tujuan" HeaderText="Tujuan" />
                                <asp:BoundField DataField="Jumlah" HeaderText="Jumlah Tuntutan" ItemStyle-CssClass="align-right"/>
                            </Columns>
                        </asp:GridView>
                    </form>
                  <br />
                  <p>Mohon kerjasama pihak tuan untuk menjelaskan tuntutan tersebut dalam masa 14 hari dari tarikh surat ini. Kegagalan tuan menjelaskan tuntutan terebut 
                      dari tarikh yang ditetapkan akan menyebabkan tindakan undang-undang boleh diambil ke atas tuan.
                  </p>
              </div>
        </div> <%--close body report--%>
    </div>
   <script type="text/javascript">
       window.onload = function () {
           window.print();
       }
   </script>

</body>
