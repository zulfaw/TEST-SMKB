<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CetakLaporanPelbagaiPenghutang.aspx.vb" Inherits="SMKB_Web_Portal.CetakLaporanPelbagaiPenghutang" %>

<!DOCTYPE html>

   <script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>
 <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
 <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
 <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N" crossorigin="anonymous">

    <style>
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
            font-size: 14px;
            font-weight: bold;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }

        .pheader2 {
            font-size: 18px;
            margin-top:0px!important;
        }

        .pheader3 {
            font-size: 18px;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }
        .gridview-style {
            border-collapse: collapse;
        }

         .gridview-style th, .gridview-style td, .gridview-style thead th{
             border: 1px solid black;
             padding: 7px;
         }
         @media print {
            @page {
                size: A4 landscape; /* or letter, legal, etc. */
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
     <div id="PermohonanTab" class="tabcontent" style="display: block">
          <div id="headerreport">
             <table style="width:100%"> 
                  <tr>
                     <td style="width: 15px;text-align: right">
                         <asp:Image ID="imgMyImage" runat="server" style="width:220px; Height:140px;text-align:right; margin-right: 10px"/>
                     </td>
                     <td style="text-align:left;">
                         <p style="margin-top:50px; margin-bottom:0px; font-size: 18px"><strong><asp:label ID="lblNamaKorporat" runat="server"></asp:label></strong></p>
                         <p class="pheader2" style="margin:0px; text-transform: capitalize"><asp:label ID="lblAlamatKorporat" runat="server"></asp:label></p>
                         <p class="pheader2" style="margin-bottom:10px;"><asp:label ID="lblNoTelFaks" runat="server"></asp:label></p>
                         <p class="pheader3"><strong>LAPORAN PELBAGAI PENGHUTANG - BIL TUNTUTAN</strong></p>
                        <p class="pheader3"><strong>BAGI TAHUN <asp:Label id="selectedYear" runat="server"></asp:Label></strong></p>
                         <br />
                     </td>
                 </tr>
             </table>
         </div> <%--close header report--%>
          <table id="tblPenghutang" class="table table-striped gridview-style" style="width: 100%">
             <thead>
                 <tr>                                          
                     <th scope="col">NO. BIL</th>
                     <th scope="col">TARIKH</th>
                     <th scope="col">Nama PENERIMA</th>
                     <th scope="col">AGENSI/SYARIKAT</th>
                     <th scope="col">BAKI AWAL</th>
                     <th scope="col">JUMLAH</th>                                         
                     <th scope="col">NOTA DEBIT</th>
                     <th scope="col">NOTA KREDIT</th>
                     <th scope="col">BAYARAN</th>
                     <th scope="col">NO DOKUMEN</th>
                     <th scope="col">CEK 'RETURN</th>
                     <th scope="col">BAKI PENUTUP</th>
                     <th scope="col">PERINGATAN I</th>
                     <th scope="col">PERINGATAN II</th>
                     <th scope="col">PERINGATAN III</th>
                 </tr>
             </thead>
             <tbody id="tableID_Penghutang">
             </tbody>
         </table>
    </div>
<script type="text/javascript">
    var tbl = null;
    $(document).ready(function () {
        const params = new URLSearchParams(window.location.search);
        const tahun = params.get('tahun');
        document.getElementById('selectedYear').innerHTML = tahun;

        tbl = $("#tblPenghutang").DataTable({
            "responsive": false,
            "searching": false,
            "paging": false,
            "ajax": {
                url: "LejarPenghutangLaporanWS.asmx/LoadOrderRecord_Penghutang",
                type: "POST",
                data: function (d) {
                    return "{ tahun: '" + tahun + "'}"
                },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                "dataSrc": function (json) {
                    return JSON.parse(json.d);
                }
            },
            "columns": [
                { "data": "No_Bil" },
                { "data": "Tkh_Mohon" },
                { "data": "Nama_Penghutang" },
                { "data": "ALAMAT" },
                {
                    "data": "BAKIAWAL", "render": function (data, type, row) {
                        if (type === 'display') {
                            return parseFloat(data).toLocaleString('en-MY');
                        }
                        return data;
                    },
                    "className": "align-right"
                },
                {
                    "data": "JUMLAH", "render": function (data, type, row) {
                        if (type === 'display') {
                            return parseFloat(data).toLocaleString('en-MY');
                        }
                        return data;
                    },
                    "className": "align-right"
                },
                {
                    "data": "NOTADEBIT", "render": function (data, type, row) {
                        if (type === 'display') {
                            return parseFloat(data).toLocaleString('en-MY');
                        }
                        return data;
                    },
                    "className": "align-right"
                },
                {
                    "data": "NOTAKREDIT", "render": function (data, type, row) {
                        if (type === 'display') {
                            return parseFloat(data).toLocaleString('en-MY');
                        }
                        return data;
                    },
                    "className": "align-right"
                },
                { "data": "Jumlah_Bayar",
                    "render": function (data, type, row) {
                        if (data !== null) {
                            return parseFloat(data).toLocaleString('en-MY');
                        } else {
                            return '';
                        }
                    },
                    "className": "align-right"
                },
                { "data": "No_Dok" },
                { "data": "CEKRETURN" },
                {
                    "data": "BAKITUTUP", "render": function (data, type, row) {
                        if (type === 'display') {
                            return parseFloat(data).toLocaleString('en-MY');
                        } else {
                            return '';
                        }
                    },
                    "className": "align-right"
                },
                {
                    "data": "Peringatan_1",
                    "render": function (data, type, row) {
                        if (data !== null) {
                            return data;
                        } else {
                            return '';
                        }
                    }
                },
                {
                    "data": "Peringatan_2",
                    "render": function (data, type, row) {
                        if (data !== null) {
                            return data;
                        } else {
                            return '';
                        }
                    }
                },
                {
                    "data": "Peringatan_3",
                    "render": function (data, type, row) {
                        if (data !== null) {
                            return data;
                        } else {
                            return '';
                        }
                    }
                },
            ]
        });
    });
    window.onload = function () {
        setTimeout(function () {
            window.print();
        }, 1000); // Adjust the delay if necessary
    }

</script>

