
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Lejar_Pemiutang.aspx.vb" Inherits="SMKB_Web_Portal.Lejar_Pemiutang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <style>
        .dropdown-list {
            width: 100%;
            /* You can adjust the width as needed */
        }
    
        .align-right {
            text-align: right;
        }
    
        .center-align {
            text-align: center;
        }
    </style>
    <contenttemplate>
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <!-- Modal -->
            <div id="permohonan">
                <div>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Laporan Transaksi Lejar Pemiutang</h5>
                        </div>
            
                        <!-- Create the dropdown filter -->
                        <div class="search-filter">
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-6">
                                    <label for="syarikat" class="col-sm-2 col-form-label" style="text-align: right">Syarikat
                                        :</label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <asp:DropDownList ID="syarikat" runat="server" DataTextField="Nama"
                                                DataValueField="Nama" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
            
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-6">
                                    <label for="ptj" class="col-sm-2 col-form-label" style="text-align: right">Ptj :</label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <asp:DropDownList ID="ptj" runat="server" DataTextField="Pejabat"
                                                DataValueField="KodPejabat" class="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
            
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-6">
                                    <label for="tahun" class="col-sm-2 col-form-label" style="text-align: right">Tahun :</label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <asp:DropDownList ID="tahun" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <button id="btnSearch" runat="server" class="btn btnSearch" onclick="return beginSearch();"
                                        type="button">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </div>
                            </div>
                            <br />
                        </div>
            
                        <div class="modal-body">
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table id="tblDataSenarai_LejarPemiutang" class="table table-striped" style="width: 100%">
                                        <thead>
                                            <tr >
                                                <th colspan="2" style="text-align:center;border-right:1px solid lightgrey">Maklumat Pemiutang</th>
                                                <th colspan="5" style="text-align:center;border-right:1px solid lightgrey">Carta Akaun</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Jan</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Feb</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Mac</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Apr</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Mei</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Jun</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">July</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Ogos</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Sep</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Okt</th>
                                                <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Nov</th>
                                                <th scope="col" colspan ="2" style="text-align:center">Dis</th>
                                            </tr>
                                            <tr>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">No. Pemiutang</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Nama Pemiutang</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kod KW</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kod Operasi</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kod PTJ</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kod Vot</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kod Projek</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                                <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                                <th scope="col" style="text-align:center">Kt</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableID_Senarai_LejarPemiutang">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        
    </contenttemplate>
    <script type="text/javascript">

        var tbl = null;
        var thn = '';

        $(document).ready(function () {

            // set asp:DropDownList ID="ptj" to default value
            $('#<%=ptj.ClientID%>').val('00');

            tbl = $("#tblDataSenarai_LejarPemiutang").DataTable({
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
                            window.open('<%=ResolveClientUrl("~/index.aspx")%>', '_blank');

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
                    "url": "LejarPemiutangWS.asmx/LoadOrderRecord_TransaksiLejarPemiutang",
                    type: 'POST',
                    data: function (d) {
                        return "{ tahun: '" + $('#<%=tahun.ClientID%>').val() + "',syarikat: '" + $('#<%=syarikat.ClientID%>').val() + "', ptj: '" + $('#<%=ptj.ClientID%>').val() + "'}"
                    },
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    }
                },
                "drawCallback": function (settings) {
                    // Your function to be called after loading data
                    close_loader();
                },
                "columns": [
                    { "data": "Kod_Pemiutang" },
                    { "data": "Nama_Pemiutang" },
                    { "data": "Kod_Kump_Wang" },
                    { "data": "Kod_Operasi" },
                    { "data": "Kod_PTJ" },
                    { "data": "Kod_Vot" },
                    { "data": "Kod_Projek" },
                    { "data": "Dr_1", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_1", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_2", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_2", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_3", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_3", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_4", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_4", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_5", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_5", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_6", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_6", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_7", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_7", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_8", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_8", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_9", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_9", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_10", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_10", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_11", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_11", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_12", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_12", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                ]
            });


        });

        function beginSearch() {
            show_loader();
            tbl.ajax.reload();
        }
    </script>
</asp:Content>

