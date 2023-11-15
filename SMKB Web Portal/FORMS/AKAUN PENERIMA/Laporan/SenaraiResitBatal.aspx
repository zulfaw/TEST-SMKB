<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="SenaraiResitBatal.aspx.vb" Inherits="SMKB_Web_Portal.SenaraiResitBatal" %>

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
            /*text-align: center;*/ 
            font-size: 14px;
            font-weight: bold;
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }

        .pheader2 {
            /*text-align: center;*/ 
            font-size: 14px;
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }

        .pheader3 {
            text-align: center;
            font-size: 14px;
            margin-top: 0px !important;
            margin-bottom: 0px !important;
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
        <div id="headerreport" class="header" style="display: none;">
            <table style="width=100%;">
                <tr>
                    <td style="width: 20%; text-align: right">
                        <img src="../../../Images/logo.png" />
                        <%--<asp:Image ID="imgMyImage" runat="server" style="width:140px; Height:80px;text-align:right"/>--%>
                    </td>
                    <td style="width: 50%; text-align: center">
                        <p class="pheader"><strong>Universiti Teknikal Malaysia Melaka</strong></p>
                        <p class="pheader2">Hang Tuah Jaya,76100, Durian Tunggal,Melaka</p>
                        <p class="pheader2">No Tel: +606-270 1019  Fax:+606-331 6115</p>
                    </td>
                    <td style="width: 30%; text-align: right">
                        <span class="ptarikh">Tarikh : <%= DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") %></span><br />
                        <span class="ptarikh">Senarai Resit Batal</span><br />
                        <%--<span class="ptarikh">Pengguna : <%= Session("ssusrID") %></span>--%>

                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: center">
                        <br />
                        <p class="pheader3"><strong>Senarai Resit Batal</strong></p>
                        <p class="pheader3">
                            <strong>
                                <label id="lblDari">Dari </label>
                                <label id="TkhMula"></label>
                                <label id="lblHingga">Hingga</label>
                                <label id="TkhTamat"></label>
                            </strong>
                        </p>
                    </td>
                    <td style="width: 25%"></td>
                </tr>
            </table>
        </div>
        <!-- Modal -->
        <div id="permohonan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Laporan Senarai Resit Batal</h5>

                    </div>
                    <!-- Create the dropdown filter -->
                    <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align: right">Carian :</label>
                                <div class="col-sm-8">
                                    <div class="input-group">
                                        <select id="categoryFilter" class="custom-select form-control">
                                            <option value="">SEMUA</option>
                                            <option value="1" selected="selected">Hari Ini</option>
                                            <option value="2">Semalam</option>
                                            <option value="3">7 Hari Lepas</option>
                                            <option value="4">30 Hari Lepas</option>
                                            <option value="5">60 Hari Lepas</option>
                                            <option value="6">Pilih Tarikh</option>
                                        </select>
                                        <button id="btnSearch" class="btn btn-outline btnSearch" type="button">
                                            <i class="fa fa-search"></i>
                                            Cari
                                               
                                        </button>
                                    </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-row">
                                        <div class="form-group col-md-5">
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-11">
                                    <div class="form-row">
                                        <div class="form-group col-md-1">
                                            <label id="lblMula" style="text-align: right;">Mula :</label>
                                        </div>

                                        <div class="form-group col-md-4">
                                            <input type="date" id="txtTarikhStart" name="txtTarikhStart" class="form-control date-range-filter">
                                        </div>
                                        <div class="form-group col-md-1">
                                        </div>
                                        <div class="form-group col-md-1">
                                            <label id="lblTamat" style="text-align: right;">Tamat :</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <input type="date" id="txtTarikhEnd" name="txtTarikhEnd" class="form-control date-range-filter">
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai_ResitBatal_rpt" class="table table-striped" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th scope="col" width="5%">No. Bil/No.Resit</th>
                                            <th scope="col" width="5%">Tarikh Daftar</th>
                                            <th scope="col" width="5%">Tarikh Batal</th>
                                            <th scope="col" width="10%">Pembayar</th>
                                            <th scope="col" width="5%">KW</th>
                                            <th scope="col" width="10%">Operasi</th>
                                            <th scope="col" width="10%">PTJ</th>
                                            <th scope="col" width="10%">Projek</th>
                                            <th scope="col" width="5%">Vot</th>
                                            <th scope="col" width="5%">Debit (RM)</th>
                                            <th scope="col" width="5%">Kredit (RM)</th>
                                            <th scope="col" width="15%">Status</th>
                                            <th scope="col" width="10%">Staff</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_ResitBatal">
                                    </tbody>

                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">

            $('#lblMula').hide();
            $('#lblTamat').hide();
            $('#txtTarikhStart').hide();
            $('#txtTarikhEnd').hide();

            var selectedDateMula;
            var SelectedDateTamat;

            var tbl = $('#tblDataSenarai_ResitBatal_rpt');
            $(document).ready(function () {
                //Take the category filter drop down and append it to the datatables_filter div. 
                //You can use this same idea to move the filter anywhere withing the datatable that you want.
                $("#tblDataSenarai_ResitBatal_rpt_filter.dataTables_filter").append($("#categoryFilter"));
       
                tbl = $("#tblDataSenarai_ResitBatal_rpt").DataTable({
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
                                printWindow.document.write('<style>@page { size: A4 landscape; margin: 1cm; }</style>');
                                printWindow.document.write('<style>.a4-container { width: 30cm; height: 29.7cm; margin: 1cm auto; }</style>'); // Add this line to set up the A4 container
                                printWindow.document.write('<style>#tblDataSenarai_ResitBatal_rpt { border-collapse: collapse; } #tblDataSenarai_ResitBatal_rpt th, #tblDataSenarai_ResitBatal_rpt td { border: 1px solid #000; padding: 8px; }</style>'); // Add table striping styles
                                printWindow.document.write('</head><body>');

                                // Add the A4 container here
                                printWindow.document.write('<div class="a4-container">');

                                // Append the header content to the print window
                                var headerContent = document.getElementById('headerreport').innerHTML;
                                printWindow.document.write(headerContent);

                                printWindow.document.write('<div style="height: 20px;"></div>'); // Adjust the height as needed

                                // Append the table content
                                var tableContent = document.getElementById('tblDataSenarai_ResitBatal_rpt').outerHTML;

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
                        "url": "TransaksiPenghutangWS.asmx/LoadOrderRecord_SenaraiResitBatal",
                        "type": "POST",
                        "contentType": "application/json; charset=utf-8",
                        "dataType": "json",
                        "dataSrc": function (json) {
                            return JSON.parse(json.d);
                        }
                    },
                    "columns": [
                        {
                            "data": "No_Dok",
                            render: function (data, type, row, meta) {

                                if (type !== "display") {

                                    return data;

                                }

                                var link = `<td style="width: 10%" >
                                                <label id="lblNo" name="lblNo" class="lblNo" value="${data}" >${data}</label>
                                                <input type ="hidden" class = "lblNo" value="${data}"/>
                                            </td>`;
                                return link;
                            }, "width": "5%"
                        },
                        { "data": "TkhDaftar", "width": "5%" },
                        { "data": "TkhBatal", "width": "5%" },
                        { "data": "Kod_Penghutang", "width": "10%" },
                        { "data": "KW", "width": "5%" },
                        { "data": "Operasi", "width": "10%" },
                        { "data": "PTJ", "width": "10%" },
                        { "data": "Kod_Projek", "width": "10%" },
                        { "data": "Vot", "width": "5%" },
                        {
                            "data": "Debit",
                            render: function (data, type, row, meta) {
                                if (type !== "display") {

                                    return data;

                                }
                                var DEBIT = data.toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 });
                                return DEBIT;
                            }, "width": "5%"
                        },
                        {
                            "data": "Kredit",
                            render: function (data, type, row, meta) {
                                if (type !== "display") {

                                    return data;

                                }
                                var KREDIT = data.toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 });
                                return KREDIT;
                            }, "width": "5%"
                        },
                        { "data": "STATUS", "width": "15%" },
                        { "data": "Staf_Terima", "width": "10%" },
                    ]
                });

                //Get the column index for the Category column to be used in the method below ($.fn.dataTable.ext.search.push)
                //This tells datatables what column to filter on when a user selects a value from the dropdown.
                //It's important that the text used here (Category) is the same for used in the header of the column to filter
                var categoryIndex = 0;
                $("#tblDataSenarai_ResitBatal_rpt th").each(function (i) {
                    if ($($(this)).html() == "Tarikh Batal") {
                        categoryIndex = i; return false;
                    }
                });
            
                //Use the built in datatables API to filter the existing rows by the Category column
                $.fn.dataTable.ext.search.push(
                    function (settings, data, dataIndex) {

                        var selectedItem = $('#categoryFilter').val();

                        if (selectedItem == "1") {
                            selectedItem = moment().format('DD/MM/YYYY');

                            $('#lblDari').show();
                            $('#TkhMula').show();
                            $('#lblHingga').show();
                            $('#TkhTamat').show();

                            $('#TkhMula').html(selectedItem);
                            $('#TkhTamat').html(selectedItem);

                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("");
                            $('#txtTarikhEnd').val("");

                            $('#lblMula').hide();
                            $('#lblTamat').hide();

                        } else if (selectedItem == "2") {
                            selectedItem = moment().subtract(1, "days").format('DD/MM/YYYY');

                            $('#lblDari').show();
                            $('#TkhMula').show();
                            $('#lblHingga').show();
                            $('#TkhTamat').show();

                            $('#TkhMula').html(selectedItem);
                            $('#TkhTamat').html(selectedItem);

                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("");
                            $('#txtTarikhEnd').val("");

                            $('#lblMula').hide();
                            $('#lblTamat').hide();

                        } else if (selectedItem == "3") {
                            var min = moment().subtract(6, "days").format('DD/MM/YYYY');
                            var max = moment().format('DD/MM/YYYY');

                            $('#lblDari').show();
                            $('#TkhMula').show();
                            $('#lblHingga').show();
                            $('#TkhTamat').show();

                            $('#TkhMula').html(min);
                            $('#TkhTamat').html(max);

                            var createdAt = data[categoryIndex]; // Our date column in the table

                            if (
                                (min <= createdAt && createdAt <= max)
                            ) {
                                return true;
                            }
                            return false;

                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("");
                            $('#txtTarikhEnd').val("");

                            $('#lblMula').hide();
                            $('#lblTamat').hide();

                        }
                        else if (selectedItem == "4") {
                            var min = moment().subtract(29, "days").format('DD/MM/YYYY');
                            var max = moment().format('DD/MM/YYYY');

                            var minDate = moment(min, 'DD/MM/YYYY', true).toDate();
                            var maxDate = moment(max, 'DD/MM/YYYY', true).toDate();

                            $('#lblDari').show();
                            $('#TkhMula').show();
                            $('#lblHingga').show();
                            $('#TkhTamat').show();

                            $('#TkhMula').html(min);
                            $('#TkhTamat').html(max);

                            var createdAt = data[categoryIndex]; // Our date column in the table
                            var createdAtDate = moment(createdAt, 'DD/MM/YYYY').toDate();

                            if (
                                (minDate <= createdAtDate && createdAtDate <= maxDate)
                            ) {

                                return true;
                            }
                            return false;

                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("");
                            $('#txtTarikhEnd').val("");

                            $('#lblMula').hide();
                            $('#lblTamat').hide();

                        }

                        else if (selectedItem == "5") {
                            var min = moment().subtract(59, "days").format('DD/MM/YYYY');
                            var max = moment().format('DD/MM/YYYY');

                            var minDate = moment(min, 'DD/MM/YYYY', true).toDate();
                            var maxDate = moment(max, 'DD/MM/YYYY', true).toDate();

                            $('#lblDari').show();
                            $('#TkhMula').show();
                            $('#lblHingga').show();
                            $('#TkhTamat').show();

                            $('#TkhMula').html(min);
                            $('#TkhTamat').html(max);

                            var createdAt = data[categoryIndex]; // Our date column in the table
                            var createdAtDate = moment(createdAt, 'DD/MM/YYYY').toDate();
                            // selectedItem = moment().format('DD-MM-YYYY');

                            if (
                                (min === null && max === null) ||
                                (min === null && createdAt <= max) ||
                                (min <= createdAt && max === null) ||
                                (minDate <= createdAtDate && createdAtDate <= maxDate)
                            ) {
                                return true;
                            }
                            return false;

                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("");
                            $('#txtTarikhEnd').val("");

                            $('#lblMula').hide();
                            $('#lblTamat').hide();
                        }

                        else if (selectedItem == "6") {

                            if (($('#txtTarikhStart').val() != "") || ($('#txtTarikhEnd').val() != "")) {

                                var min = new Date($('#txtTarikhStart').val());
                                var max = new Date($('#txtTarikhEnd').val());


                                min = ("0" + min.getDate()).slice(-2) + "/" + ("0" + (min.getMonth() + 1)).slice(-2) + "/" + min.getFullYear();
                                max = ("0" + max.getDate()).slice(-2) + "/" + ("0" + (max.getMonth() + 1)).slice(-2) + "/" + max.getFullYear();

                                var createdAt = data[categoryIndex]; // Our date column in the table

                                $('#lblDari').show();
                                $('#TkhMula').show();
                                $('#lblHingga').show();
                                $('#TkhTamat').show();

                                $('#TkhMula').html(min);
                                $('#TkhTamat').html(max);


                                if (
                                    (min === null && max === null) ||
                                    (min === null && createdAt <= max) ||
                                    (min <= createdAt && max === null) ||
                                    (min <= createdAt && createdAt <= max)
                                ) {
                                    return true;
                                }
                                return false;

                            } else {

                                $('#txtTarikhStart').show();
                                $('#txtTarikhEnd').show();

                                $('#txtTarikhStart').val("")
                                $('#txtTarikhEnd').val("")

                                $('#lblMula').show();
                                $('#lblTamat').show(); 
                            }    
                        }
                        if (selectedItem != "6") {

                            var category = data[categoryIndex];

                            if (selectedItem === "" || category.includes(selectedItem)) {
                                $('#lblDari').hide();
                                $('#TkhMula').hide();
                                $('#lblHingga').hide();
                                $('#TkhTamat').hide();
                                return true;
                            }
                            return false;
                        }
                    }
                );

                //Set the change event for the Category Filter dropdown to redraw the datatable each time
                //a user selects a new filter.
                $("#categoryFilter").change(function (e) {

                    var selectedItem = $('#categoryFilter').val()
                    if (selectedItem == "6") {
                        $('#txtTarikhStart').show();
                        $('#txtTarikhEnd').show();

                        $('#lblMula').show();
                        $('#lblTamat').show();

                        $('#txtTarikhStart').val("");
                        $('#txtTarikhEnd').val("");
                        tbl.draw();
                    }
                    else {
                        $('#txtTarikhStart').hide();
                        $('#txtTarikhEnd').hide();

                        $('#txtTarikhStart').val("")
                        $('#txtTarikhEnd').val("")

                        $('#lblMula').hide();
                        $('#lblTamat').hide();
                        tbl.draw();

                    }
                });
            });

            $(document).ready(function () {
                $('#btnSearch').on('click', function () {
                    tbl.draw();
                });
               
            });

        </script>
    </div>

</asp:Content>
