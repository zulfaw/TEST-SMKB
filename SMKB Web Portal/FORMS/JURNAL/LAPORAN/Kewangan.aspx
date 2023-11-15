<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Kewangan.aspx.vb" Inherits="SMKB_Web_Portal.Kewangan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.html5.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.print.min.js" crossorigin="anonymous"></script>


    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />

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
/*            text-align: center;
*/            font-size: 14px;
            font-weight: bold;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }

        .pheader2 {
/*            text-align: center;
*/            font-size: 14px;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }

        .pheader3 {
            text-align: center;
            font-size: 14px;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }
        th, td {
            padding: 1px;
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
     <div id="PermohonanTab" class="tabcontent" style="display: block">
          <div id="headerreport" style="display:none">
             <table style="width:100%"> 
                  <tr>
                     <td style="width: 20%;text-align: right">
                         <asp:Image ID="imgMyImage" runat="server" style="width:140px; Height:80px;text-align:right"/>
                     </td>
                     <td style="text-align:center;">
                         <p style="margin:0px;" class="pheader"><strong><asp:label ID="lblNamaKorporat" runat="server"></asp:label></strong></p>
                         <p class="pheader2" style="font-size:12px!important;margin:0px; text-transform: capitalize"><asp:label ID="lblAlamatKorporat" runat="server"></asp:label></p>
                         <p class="pheader2" style="font-size:12px!important;margin:0px;"><asp:label ID="lblNoTelFaks" runat="server"></asp:label></p>
                         <p class="pheader2" style="font-size:12px!important;margin:0px;"><asp:label ID="lblEmailKorporat" runat="server"></asp:label></p>

                     </td>
                     <td style="width: 30%; font-size:12px!important; text-align: right">
                         <span class="ptarikh">Tarikh : <%= DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") %></span><br />
                         <span class="ptarikh">Laporan : CLS006</span><br />
                         <span class="ptarikh">Pengguna : <%= Session("ssusrID") %></span>

                     </td>
                 </tr>
                 <tr>
                     <td colspan="3" style="text-align: center">
                         <br />
                         <p class="pheader3"><strong>Laporan Senarai Kumpulan Kewangan</strong></p>
                    </td>
                 </tr>
             </table>
         </div> <%--close header report--%>
        <!-- Modal -->
        <div id="permohonan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="width:100%" id="exampleModalCenterTitle">Laporan Senarai Kumpulan Kewangan</h5>

                    </div>
                </div>
            </div>
        </div>
    </div>

     <div class="modal-body">
         <div class="col-md-12">
             <div class="transaction-table table-responsive">
                 <table id="tblDataSenarai_trans_rpt" class="table table-striped" style="width: 100%">
                     <thead>
                         <tr>
                             <th scope="col">Kumpulan Wang</th>
                             <th scope="col">Butiran</th>

                         </tr>
                     </thead>
                     <tbody id="tableID_Senarai_trans">
                     </tbody>
                
                 </table>
            </div>
        </div>

    </div>
            <script type="text/javascript">
                $(document).ready(function () {
                    $(".ui.dropdown").dropdown({
                        fullTextSearch: true
                    });

                    tbl = $("#tblDataSenarai_trans_rpt").DataTable({
                        "responsive": true,
                        "searching": true,
                        cache: true,
                        dom: 'Bfrtip',
                        buttons: [
                            'csv', 'excel', {
                                extend: 'print',
                                text: '<i class="fa fa-files-o green"></i> Print',
                                titleAttr: 'Print',
                                className: 'ui green basic button',
                                action: function (e, dt, button, config) {
                                    var printWindow = window.open('', '_blank');
                                    printWindow.document.write('<html><head>');
                                    printWindow.document.write('<style>@page { size: A4; margin: 1cm; }</style>');
                                    printWindow.document.write('<style>.a4-container { width: 21cm; height: 29.7cm; margin: 1cm auto; }</style>'); // Add this line to set up the A4 container
                                    printWindow.document.write('<style>#tblDataSenarai_trans_rpt { border-collapse: collapse; } #tblDataSenarai_trans_rpt th, #tblDataSenarai_trans_rpt td { border: 1px solid #000; padding: 8px; }</style>'); // Add table striping styles
                                    printWindow.document.write('</head><body>');

                                    // Add the A4 container here
                                    printWindow.document.write('<div class="a4-container">');

                                    // Append the header content to the print window
                                    var headerContent = document.getElementById('headerreport').innerHTML;
                                    printWindow.document.write(headerContent);

                                    printWindow.document.write('<div style="height: 20px;"></div>'); // Adjust the height as needed

                                    // Append the table content
                                    var tableContent = document.getElementById('tblDataSenarai_trans_rpt').outerHTML;

                                    tableContent = tableContent.replace('<table', '<table style="text-align: center;"');

                                    // Set the style for the second column's data cells (left-aligned)
                                    tableContent = tableContent.replace(/<td>(.*?)<\/td>/g, '<td style="text-align: left;">$1</td>');
                                    printWindow.document.write(tableContent);

                                    printWindow.document.write('<div style="height: 20px;"></div>'); // Adjust the height as needed

                                    // Add the additional div with centered text
                                    printWindow.document.write('<div style="text-align:center">');
                                    printWindow.document.write('<span><strong>*** Senarai Tamat ***</strong></span>');
                                    printWindow.document.write('</div>');

                                    // Close the A4 container
                                    printWindow.document.write('</div>');

                                    printWindow.document.write('</body></html>');
                                    printWindow.document.close();
                                    printWindow.print();
                                }
                            }
                        ],
                        "sPaginationType": "full_numbers",
                        "oLanguage": {
                            "oPaginate": {
                                "sNext": '<i class="fa fa-forward"></i>',
                                "sPrevious": '<i class="fa fa-backward"></i>',
                                "sFirst": '<i class="fa fa-step-backward"></i>',
                                "sLast": '<i class="fa fa-step-forward"></i>'
                            },
                            "sLengthMenu": "Tunjuk _MENU_ rekod",
                            "sZeroRecords": "Tiada rekod yang sepadan ditemui",
                            "sInfo": "Menunjukkan _END_ dari _TOTAL_ rekod",
                            "sInfoEmpty": "Menunjukkan 0 ke 0 daripada rekod",
                            "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
                            "sEmptyTable": "Tiada rekod.",
                            "sSearch": "Carian"
                        },
                        "ajax":
                        {
                            "url": "LejarPenghutangWS.asmx/LoadOrderRecord_SenaraiKewangan",
                            "type": "POST",
                            "contentType": "application/json; charset=utf-8",
                            "dataType": "json",
                            "dataSrc": function (json) {
                                return JSON.parse(json.d);
                            }
                        },
                        "columns": [
                            { "data": "Kod_Kump_Wang", "width": "20%" },
                            { "data": "Butiran", "width": "20%" },
                        ]
                    });
                });
            </script>
</asp:Content>