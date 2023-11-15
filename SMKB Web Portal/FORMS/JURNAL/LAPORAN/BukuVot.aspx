<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="BukuVot.aspx.vb" Inherits="SMKB_Web_Portal.BukuVot" %>
<%@ Register Src="~/FORMS/JURNAL/LAPORAN/PrintReport/TableImbanganDuga.ascx" TagPrefix="Vot" TagName="Table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
    <%--<script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>--%>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.html5.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.print.min.js" crossorigin="anonymous"></script>


    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
    <%--        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>--%>
<%--    <script type="text/javascript" src="<%=ResolveClientUrl("~/Scripts/select2custom.min.js")%>"></script>--%>

    <style>
        .dropdown-list {
            width: 100%; /* You can adjust the width as needed */
        }

        .align-right {
            text-align: right;
        }

        .center-align {
            text-align: center;
        }

        .tblPendapatan {
            display: none;
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

        <div id="headerreport" style="display: none">
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%; text-align: right">
                        <asp:Image ID="imgMyImage" runat="server" Style="width: 140px; height: 80px; text-align: right" />
                    </td>
                    <td style="text-align: center;">
                        <p style="margin: 0px;" class="pheader"><strong>
                            <asp:Label ID="lblNamaKorporat" runat="server"></asp:Label></strong></p>
                        <p class="pheader2" style="font-size: 12px!important; margin: 0px; text-transform: capitalize">
                            <asp:Label ID="lblAlamatKorporat" runat="server"></asp:Label></p>
                        <p class="pheader2" style="font-size: 12px!important; margin: 0px;">
                            <asp:Label ID="lblNoTelFaks" runat="server"></asp:Label></p>
                        <p class="pheader2" style="font-size: 12px!important; margin: 0px;">
                            <asp:Label ID="lblEmailKorporat" runat="server"></asp:Label></p>

                    </td>
                    <td style="width: 30%; font-size: 12px!important; text-align: right">
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
                        <h5 class="modal-title" id="exampleModalCenterTitle">Buku Vot&nbsp;</h5>

                    </div>

                    <!-- Create the dropdown filter -->
                    <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align: right">Syarikat :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="Nama" DataValueField="Nama" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="txtPerihal1" class="col-sm-2 col-form-label" style="text-align: right">Ptj :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Pejabat" DataValueField="KodPejabat" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />


                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="kodKewangan" class="col-sm-2 col-form-label" style="text-align: right">Kod KW :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="kodkw" runat="server" DataTextField="Butiran" DataValueField="Kod_Kump_Wang" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="container">
                            <div class="row justify-content-center">
                                <div class="col-md-9">
                                    <div class="form-row">
                                        <div class="col-md-2 order-md-1">
                                            </div>
                                        <div class="col-md-3 order-md-3">
                                            <div class="form-group">
                                                <label for="txtTarikhTamat" style="display: block; text-align: center; width: 100%;">Hingga</label>
                                               <input type="date" id="txtTarikhHingga" name="txtTarikhHingga"  class="form-control date-range-filter">
                                            </div>
                                        </div>

                                        <div class="col-md-3 order-md-2">
                                            <div class="form-group">
                                                <label for="txtTarikhMula" style="display: block; text-align: center; width: 100%;">Tarikh Dari</label>
                                               <input type="date" id="txtTarikhDari" name="txtTarikhDari"  class="form-control date-range-filter">
                                            </div>
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                       <div class="container">
                            <div class="row justify-content-center">
                                <div class="col-md-9">
                                    <div class="form-row">
                                        <div class="col-md-2 order-md-1">
                                            </div>
                                        <div class="col-md-3 order-md-3">
                                            <div class="form-group">
                                                <label for="txtTarikhTamat" style="display: block; text-align: center; width: 100%;">Hingga</label>
                                               <input type="text" id="txtVotHingga" name="txtVotHingga"  class="form-control">
                                            </div>
                                        </div>

                                        <div class="col-md-3 order-md-2">
                                            <div class="form-group">
                                                <label for="txtTarikhMula" style="display: block; text-align: center; width: 100%;">Vot Dari</label>
                                               <input type="text" id="txtVotDari" name="txtVotDari"  class="form-control">
                                            </div>
                                        </div>



                                        <div class="col-md-3 order-md-4">
                                            <div class="form-group">
                                                <label></label>
                                                <button id="Button1" runat="server" class="btn btnSearch" onclick="return beginSearch();" type="button" style="margin-top: 33px">
                                                    <i class="fa fa-search"></i>
                                                </button>
                                            </div>
                                        </div>
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
                                            <th scope="col">Tarikh</th>
                                            <th scope="col">Butiran</th>
                                            <th scope="col">Tanggungan dibawa ke hadapan</th>
                                            <th scope="col">Kod Vot</th>
                                            <th scope="col">No. PT/Baucar</th>
                                            <th scope="col">Debit (RM)</th>
                                            <th scope="col">Kredit (RM)</th>
                                            <th scope="col">Tanggungan dikenakan (dijelaskan)</th>
                                            <th scope="col">Tanggungan/Belum Selesai</th>
                                            <th scope="col">Perbelanjaan Bersih</th>
                                            <th scope="col">Baki Masih Ada</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_trans">
                                    </tbody>
                                   
                                </table>                                 

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

</div>
    

        <script type="text/javascript">


            var tbl = null;
            var bulan = '';
            var thn = '';
            $(document).ready(function () {

                $(".ui.dropdown").dropdown({
                    fullTextSearch: true
                });

                tbl = $("#tblDataSenarai_trans_rpt").DataTable({
                    "responsive": true,
                    "searching": true,
                    //"pageLength": -1,
                    cache: true,
                    dom: 'Bfrtip',
                    "buttons": [
                        'csv', 'excel', {
                            extend: 'print',
                            text: '<i class="fa fa-files-o green"></i> Print',
                            titleAttr: 'Print',
                            className: 'ui green basic button',
                            action: function (e, dt, button, config) {

                                //config.pageLength = -1;

                                var printWindow = window.open('', '_blank');
                                printWindow.document.write('<html><head>');
                                printWindow.document.write('<style>@page { size: landscape; }</style>');
                                printWindow.document.write('<style>#printTable { border-collapse: collapse; } #printTable th, #printTable td { border: 1px solid #000;  }</style>'); // Add table striping styles                  

                                printWindow.document.write('</head><body>');

                                // Add the A4 container here
                                printWindow.document.write('<div class="a4-container">');

                                // Append the header content to the print window
                                var headerContent = document.getElementById('headerreport').innerHTML;
                                printWindow.document.write(headerContent);

                                printWindow.document.write('<div style="height: 20px;"></div>'); // Adjust the height as needed

                                // Append the table content
                                //var tableContent = document.getElementById('tblDataSenarai_trans_rpt').outerHTML;

                                //// Retrieve the DataTable's data and structure
                                //var tableData = dt.data().toArray();
                                //var columns = dt.columns().header().toArray();

                                //tableContent = tableContent.replace('<table', '<table style="text-align: center;"');

                                //// Set the style for the second column's data cells (left-aligned)
                                //tableContent = tableContent.replace(/<td>(.*?)<\/td>/g, '<td style="text-align: left;">$1</td>');
                                //printWindow.document.write(tableContent);

                                // Create a custom header and append it to the print window
                                //var customHeader = '<div class="custom-header">' +
                                //    '<thead>' +
                                //    '<tr>' +
                                //    '<th scope="col">Tarikh</th>' +
                                //    '<th scope="col">Butiran</th>' +
                                //    '<th scope="col">Tanggungan dibawa ke hadapan</th>' +
                                //    '<th scope="col">Kod Vot</th>' +
                                //    '<th scope="col">No. PT/Baucar</th>' +
                                //    '<th scope="col">Debit (RM)</th>' +
                                //    '<th scope="col">Kredit (RM)</th>' +
                                //    '<th scope="col">Tanggungan dikenakan (dijelaskan)</th>' +
                                //    '<th scope="col">Tanggungan/Belum Selesai</th>' +
                                //    '<th scope="col">Perbelanjaan Bersih</th>' +
                                //    '<th scope="col">Baki Masih Ada</th>' +
                                //    '</tr > ' +
                                //    '</thead > ' +
                                //    '</div>';

                     

                                //printWindow.document.write(customHeader);

                                printWindow.document.write('<div style="height: 20px;"></div>'); // Adjust the height as needed                       

                                printWindow.document.write('<table id="printTable" style="text-align: center;" >' +
                                    '<thead>' +
                                    '<tr>' +
                                    '<th scope="col">Tarikh</th>' +
                                    '<th scope="col">Butiran</th>' +
                                    '<th scope="col">Tanggungan dibawa ke hadapan</th>' +
                                    '<th scope="col">Kod Vot</th>' +
                                    '<th scope="col">No. PT/Baucar</th>' +
                                    '<th scope="col">Debit (RM)</th>' +
                                    '<th scope="col">Kredit (RM)</th>' +
                                    '<th scope="col">Tanggungan dikenakan (dijelaskan)</th>' +
                                    '<th scope="col">Tanggungan/Belum Selesai</th>' +
                                    '<th scope="col">Perbelanjaan Bersih</th>' +
                                    '<th scope="col">Baki Masih Ada</th>' +
                                    '</tr>' +
                                    ' </thead>' +
                                    '</table > ');

                                // Create a DataTable for the print table
                                var printTable = $('#printTable', printWindow.document).DataTable({
                                    "pageLength": -1,
                                    "searching": false,
                                    "paging": false,
                                    "dom": 't', // Display table only (no info, pagination, etc.)
                                    "columns": [
                                        {
                                            "data": "TARIKH", "width": "20%", "render": function (data) {
                                                // Assuming data is in "DD-MM-YYYY" format
                                                var parts = data.split("-");
                                                if (parts.length === 3) {
                                                    var formattedDate = parts[2] + "-" + parts[1] + "-" + parts[0];
                                                    return formattedDate;
                                                } else {
                                                    return data; // Return as is if not in expected format
                                                }
                                            }
                                        },
                                        {
                                            "data": "BUTIRAN", "width": "20%",
                                            "className": "align-left"},
                                        {
                                            "data": "TNG",
                                            "width": "15%",
                                            "render": function (data) {
                                                return parseFloat(data).toLocaleString();
                                            },
                                            "className": "align-right"
                                        },
                                        { "data": "VOT", "width": "20%" },
                                        { "data": "NO_DOK", "width": "20%" },

                                        {
                                            "data": "DEBIT",
                                            "width": "15%",
                                            "render": function (data) {
                                                return parseFloat(data).toLocaleString();
                                            },
                                            "className": "align-right"
                                        },

                                        {
                                            "data": "CREDIT",
                                            "width": "15%",
                                            "render": function (data) {
                                                return parseFloat(data).toLocaleString();
                                            },
                                            "className": "align-right"
                                        },
                                        { "data": "VOT", "width": "20%" },
                                        { "data": "VOT", "width": "20%" },
                                        { "data": "VOT", "width": "20%" },
                                        { "data": "VOT", "width": "20%" }



                                    ]
                                    // ... other DataTable options
                                });

                                // Fetch and append the data from the original DataTable
                                var tableData = dt.rows().data().toArray();
                                for (var i = 0; i < tableData.length; i++) {
                                    printTable.row.add(tableData[i]);
                                }

                                printTable.draw();



                                // Add the additional div with centered text
                                printWindow.document.write('<div style="text-align:center">');
                                printWindow.document.write('<br/>')
                                printWindow.document.write('<span><strong>*** Senarai Tamat ***</strong></span>');
                                printWindow.document.write('</div>');

                                // Close the A4 container
                                printWindow.document.write('</div>');

                                printWindow.document.write('</body></html>');
                                printWindow.document.close();

                                // Open the print dialog for the new window
                                printWindow.print();

                                // Close the print window after printing (optional)
                                printWindow.close();
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
                        "url": "LejarPenghutangWS.asmx/LoadRecord_BukuVot",
                        type: 'POST',
                        data: function (d) {
                           // console.log($('#MainContent_FormContents_DropDownList1').val()+" text " + $('#txtVotDari').val())
                            return "{ votMula: '" + $('#txtVotDari').val() + "',votHingga: '" + $('#txtVotHingga').val() + "',tkhMula: '" + $('#txtTarikhDari').val() + "', tkhHingga: '" + $('#txtTarikhHingga').val() + "', kodkw: '" + $('#MainContent_FormContents_kodkw').val() + "', syarikat: '" + $('#MainContent_FormContents_DropDownList2').val() + "', ptj: '" + $('#MainContent_FormContents_DropDownList1').val() + "'}";
                        },
                        "contentType": "application/json; charset=utf-8",
                        "dataType": "json",
                        "dataSrc": function (json) {
                            return JSON.parse(json.d);
                        }

                    },

                    "columns": [
                        { "data": "TARIKH", "width": "20%" },
                        {
                            "data": "BUTIRAN", "width": "20%",
                            "className": "align-left"
                        },
                        { "data": "VOT", "width": "20%" },
                        { "data": "VOT", "width": "20%" },
                        { "data": "NO_DOK", "width": "20%" },
                       
                        {
                            "data": "DEBIT",
                            "width": "15%",
                            "render": function (data) {
                                return parseFloat(data).toLocaleString();
                            },
                            "className": "align-right" 
                        },

                        {
                            "data": "CREDIT",
                            "width": "15%",
                            "render": function (data) {
                                return parseFloat(data).toLocaleString();
                            },
                            "className": "align-right"
                        },
                        { "data": "VOT", "width": "20%" },
                        { "data": "VOT", "width": "20%" },
                        { "data": "VOT", "width": "20%" },
                        { "data": "VOT", "width": "20%" }
                      


                    ]
                });

            });


            function beginSearch() {
                tbl.ajax.reload();
            }
        </script>

</asp:Content>
