<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Senarai_Pelbagai_Penghutang.aspx.vb" Inherits="SMKB_Web_Portal.Senarai_Pelbagai_Penghutang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
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
            font-size: 14px;
            font-weight: bold;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }

        .pheader2 {
            font-size: 14px;
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
        <!-- Modal -->
        <div id="permohonan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" style="width:100%" id="exampleModalCenterTitle">Laporan Pelbagai Penghutang - Bil Tuntutan</h5>

                    </div>
                </div>
            </div>
        </div>
    </div>
     <!-- Create the dropdown filter -->
    <div class="search-filter">
        <div class="form-row justify-content-center">
            <div class="form-group row col-md-6">
                <label for="tahun" class="col-sm-2 col-form-label" style="text-align: right">Tahun :</label>
                <div class="col-sm-8">
                    <div class="input-group">
                        <asp:DropDownList id="ddlTahun" runat="server" CssClass="form-control"></asp:DropDownList>
                         <button id="btnSearch" runat="server" class="btn btnSearch" onclick="return beginSearch();" type="button">
                            <i class="fa fa-search"></i>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal-body">
        <div class="col-md-12">
            <div class="transaction-table table-responsive">
                <table id="tblPenghutang" class="table table-striped" style="width: 100%">
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
        </div>
    </div>
            <script type="text/javascript">
                var tbl = null;
                $(document).ready(function () {
                    $(".ui.dropdown").dropdown({
                        fullTextSearch: true
                    });

                    tbl = $("#tblPenghutang").DataTable({
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
                                    var selectedYear = document.getElementById('<%=ddlTahun.ClientID%>').value;
                                    var params = "scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=0,height=0,left=-1000,top=-1000";
                                    var url = '<%= ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Laporan/CetakLaporanPelbagaiPenghutang") %>?tahun=' + selectedYear;
                                    window.open(url, '_blank', params);
                                }
                            }
                        ],
                        "sPaginationType": "full_numbers",
                        "oLanguage": {
                            "oPaginate": {
                                "sNext": '<i class="fa fa-forward"></i>',
                                "sPrevious": '<i class="fa fa-backward"></i>', // Corrected the closing tag
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
                            url: "LejarPenghutangLaporanWS.asmx/LoadOrderRecord_Penghutang",
                            type: "POST",
                            data: function (d) {
                                return "{ tahun: '" + $('#<%=ddlTahun.ClientID%>').val() + "'}"
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
                            {
                                "data": "Jumlah_Bayar",
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
                function beginSearch() {
                    tbl.ajax.reload();
                } 
            </script>
</asp:Content>