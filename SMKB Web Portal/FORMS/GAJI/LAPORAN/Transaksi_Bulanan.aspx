<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Transaksi_Bulanan.aspx.vb" Inherits="SMKB_Web_Portal.Transaksi_Bulanan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <%--<script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>--%>
   <%-- <script src="https://code.jquery.com/jquery-3.6.0.min.js" crossorigin="anonymous"></script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.html5.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.print.min.js" crossorigin="anonymous"></script>
    <%--<script src="https://cdn.datatables.net/buttons/2.4.2/js/buttons.colVis.min.js" crossorigin="anonymous"></script>--%>

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />

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

        .TableTransaksi {
            display: none;
        }

    </style>

    <div id="PermohonanTab" class="tabcontent" style="display: block">
        <!-- Modal -->
        <div id="permohonan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Laporan Transaksi Bulanan</h5>

                    </div>
                    <!-- Create the dropdown filter -->
                    <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align: right">Syarikat :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="syarikatFilter" runat="server" DataTextField="Nama" DataValueField="Nama" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
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
                                        <%--<asp:DropDownList ID="ptjFilter" runat="server" DataTextField="Pejabat" DataValueField="KodPejabat" class="ui fluid search dropdown selection custom-select" AutoPostBack="true" OnSelectedIndexChanged="ptjFilter_SelectedIndexChanged"></asp:DropDownList>--%>
                                        <asp:DropDownList ID="ptjFilter" runat="server" DataTextField="Pejabat" DataValueField="KodPejabat" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                       <%-- <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="txtPerihal1" class="col-sm-2 col-form-label" style="text-align: right">No Staf :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="staffFilter" runat="server" DataTextField="NamaStaf" DataValueField="NoStaf" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div><br />--%>
                        
                        <div class="container">
                            <div class="row justify-content-center">
                                <div class="col-md-9">
                                    <div class="form-row">
                                        <div class="col-md-2 order-md-1">
                                            </div>
                                        <div class="col-md-3 order-md-3">
                                            <div class="form-group">
                                               <label for="txtTarikh1" style="display: block; text-align: center; width: 100%;">Bulan</label>
                                               <select id="ddlMonths" runat="server" class="ui fluid search dropdown selection custom-select">
                                                    <option value="">-- Sila Pilih --</option>
                                                    <option value="1">Januari</option>
                                                    <option value="2">Februari</option>
                                                    <option value="3">Mac</option>
                                                    <option value="4">April</option>
                                                    <option value="5">Mei</option>
                                                    <option value="6">Jun</option>
                                                    <option value="7">Julai</option>
                                                    <option value="8">Ogos</option>
                                                    <option value="9">September</option>
                                                    <option value="10">Oktober</option>
                                                    <option value="11">November</option>
                                                    <option value="12">Disember</option>
                                                </select>
                                            </div>
                                        </div>

                                        <div class="col-md-3 order-md-2">
                                            <div class="form-group">
                                                <label for="txtNoRujukan1" style="display: block; text-align: center; width: 100%;">Tahun</label>
                                                <select id="ddlyear" runat="server" class="ui fluid search dropdown selection custom-select">
                                                    <option value="">-- Sila Pilih --</option>
                                                    <option value="2023">2023</option>
                                                    <option value="2022">2022</option>
                                                    <option value="2021">2021</option>
                                                    <option value="2020">2020</option>                                                   
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-md-3 order-md-4">
                                            <div class="form-group">
                                                <label></label>
                                                <button id="btnSearch" runat="server" class="btn btnSearch" onclick="return beginSearch();" type="button" style="margin-top: 33px">
                                               <%-- <button id="btnSearch" runat="server" class="btn btnSearch" type="button" style="margin-top: 33px">--%>
                                                    <i class="fa fa-search"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="txtPerihal1" class="col-sm-2 col-form-label" style="text-align: right">Jenis Report :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <div class="checkbox">
                                            <input type="checkbox" id="CheckBox1" class="column-toggle" data-column="1" checked /> Semua
                                            <input type="checkbox" id="CheckBox2" class="column-toggle" data-column="2" /> Ringkasan Gaji
                                            <input type="checkbox" id="CheckBox3" class="column-toggle" data-column="5" /> Gaji Pokok
                                            <input type="checkbox" id="CheckBox4" class="column-toggle" data-column="2" /> Cukai
                                            <input type="checkbox" id="CheckBox5" class="column-toggle" data-column="3" /> Perkeso
                                            <input type="checkbox" id="CheckBox6" class="column-toggle" data-column="4" /> KWSP
                                            <input type="checkbox" id="CheckBox7" class="column-toggle" data-column="6" /> OT
                                            <input type="checkbox" id="CheckBox8" class="column-toggle" data-column="7" /> Pencen
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai_trans_rpt" class="table table-striped" style="width: 100%;">
                                    <thead>
                                        <tr>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">No</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 15%;">Nama</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">No KP</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Gaji Pokok</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Bonus</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Elaun</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">OT</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Gaji Kasar</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Potongan</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Cuti</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">No KWSP</th>
                                            <th scope="col" colspan="2" style="text-align: center; width: 5%;">KWSP</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">No Cukai</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Kategori Cukai</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Cukai</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Gaji Bersih</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">No Perkeso</th>
                                            <th scope="col" colspan="2" style="text-align: center; width: 5%;">Perkeso</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">No Pencen</th>
                                            <th scope="col" rowspan="2" style="text-align: center; width: 5%;">Pencen</th>
                                        </tr>
                                        <tr>
                                            <th scope="col">Pekerja</th>
                                            <th scope="col">Majikan</th>
                                            <th scope="col">Pekerja</th>
                                            <th scope="col">Majikan</th>
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
        var tbl = null
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
                    {
                        extend: 'csv',
                        exportOptions: {
                            columns: ':visible' // Export only visible columns
                        }
                    },
                    {
                        extend: 'excel',
                        exportOptions: {
                            columns: ':visible' // Export only visible columns
                        }
                    }
                   /* 'csv', 'excel',*/
                    <%--{
                        extend: 'print',
                        text: '<i class="fa fa-files-o green"></i> Print',
                        titleAttr: 'Print',
                        className: 'ui green basic button',
                        action: function (e, dt, button, config) {
                            debugger;
                            // Get the array of visible column indices
                            var visibleColumns = dt.columns(':visible').indexes().toArray();

                            // Serialize the visibleColumns array to a JSON string
                            var visibleColumnsJson = JSON.stringify(visibleColumns);

                            // Store the serialized JSON string in a cookie
                            document.cookie = "VisibleColumns=" + visibleColumnsJson + "; path=/";

                            window.open('<%= ResolveClientUrl("~/FORMS/GAJI/LAPORAN/PrintReport/reportTransaksi.aspx") %>', '_blank');
                        }
                    }--%>
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
                "ajax": {
                    "url": "Laporan_WS.asmx/LoadRecord_Transaksi",
                    type: 'POST',
                    data: function (d) {
                        return "{ bulan: '" + $('#<%=ddlMonths.ClientID%>').val() + "', tahun: '" + $('#<%=ddlyear.ClientID%>').val() + "',syarikat: '" + $('#<%=syarikatFilter.ClientID%>').val() + "', ptj: '" + $('#<%=ptjFilter.ClientID%>').val() + "'}"
                    },
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    }

                },
                "columns": [
                    { "data": "No_Staf", "className": "center-align"},
                    { "data": "Nama" },
                    { "data": "KP", "className": "center-align"},
                    {
                        "data": "Gaji_Pokok",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    {
                        "data": "Bonus",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    {
                        "data": "Elaun",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    {
                        "data": "OT",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    {
                        "data": "Gaji_Kasar",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    {
                        "data": "Potongan",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    {
                        "data": "Cuti",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    { "data": "No_KWSP", "className": "center-align" },
                    {
                        "data": "KWSP",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    {
                        "data": "KWSM",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    { "data": "No_Cukai", "className": "center-align" },
                    { "data": "Kategori_Cukai", "className": "center-align" },
                    {
                        "data": "Cukai",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    {
                        "data": "Gaji_Bersih",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    { "data": "No_Perkeso", "className": "center-align" },
                    {
                        "data": "SOCP",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    {
                        "data": "SOCM",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    },
                    { "data": "No_Pencen", "className": "center-align" },
                    {
                        "data": "Pencen",
                        "render": function (data, type, row) {
                            if (type === 'display' || type === 'filter') {
                                return parseFloat(data).toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
                            }
                            return data;
                        },
                        "className": "text-right"
                    }
                ]
            });
            //create a mapping between checkbox IDs and DataTable column indices
            var checkboxColumnMap = {
                'CheckBox1': [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21],
                'CheckBox2': [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 11, 12, 15, 16, 18, 19],
                'CheckBox3': [0, 1, 2, 3],
                'CheckBox4': [0, 1, 2, 13, 14, 15],
                'CheckBox5': [0, 1, 2, 17, 18, 19],
                'CheckBox6': [0, 1, 2, 10, 11, 12],
                'CheckBox7': [0, 1, 2, 6],
                'CheckBox8': [0, 1, 2, 20, 21]
            }
            // Event listener for checkbox change
            $('.column-toggle').on('change', function () {
                var columnIndex = $(this).data('column');
                var isChecked = $(this).is(':checked');
                //uncheck checkbox1 if other checkbox checked
                if (columnIndex != 1) {
                    $('#CheckBox1').prop('checked', false);
                } else {
                    $('#CheckBox2').prop('checked', false);
                    $('#CheckBox3').prop('checked', false);
                    $('#CheckBox4').prop('checked', false);
                    $('#CheckBox5').prop('checked', false);
                    $('#CheckBox6').prop('checked', false);
                    $('#CheckBox7').prop('checked', false);
                    $('#CheckBox8').prop('checked', false);
                }
                // Create an array to store the DataTable column indices to show
                var columnsToShow = [];

                // Loop through the checked checkboxes and add their corresponding column indices
                $('.column-toggle:checked').each(function () {
                    var checkboxId = $(this).attr('id');
                    var columnIndices = checkboxColumnMap[checkboxId];
                    if (Array.isArray(columnIndices)) {
                        // If an array is associated with the checkbox ID, add all indices to columnsToShow
                        columnsToShow = columnsToShow.concat(columnIndices);
                    } else if (typeof columnIndices !== 'undefined') {
                        // If a single index is associated with the checkbox ID, add it to columnsToShow
                        columnsToShow.push(columnIndices);
                    }
                });

                // Show the columns based on the indices in the columnsToShow array
                tbl.columns().every(function (index) {
                    var isVisible = columnsToShow.includes(index);
                    this.visible(isVisible);
                });
            });

        });

        function beginSearch() {
            debugger;
            show_loader();
            tbl.ajax.reload();
            close_loader();
        }

    </script>
</asp:Content>