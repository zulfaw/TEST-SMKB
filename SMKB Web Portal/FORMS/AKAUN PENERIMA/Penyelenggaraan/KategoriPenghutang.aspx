<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="KategoriPenghutang.aspx.vb" Inherits="SMKB_Web_Portal.KategoriPenghutang" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script type="text/javascript">
        function ShowPopup(elm) {
            if (elm == "1") {
                $('#permohonan').modal('toggle');
            }
            else if (elm == "2") {
                $(".modal-body div").val("");
                $('#permohonan').modal('toggle');
            }
        }
    </script>
    <contenttemplate>

        <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <%-- DIV PENDAFTARAN KATEGORI PENGHUTANG --%>
            <div id="divdaftarkategoripenghutang" runat="server" visible="true">
                <div class="modal-body">
                    <div class="table-title">
                        <h6>Kategori Penghutang</h6>
                        <div class="btn btn-primary btnPapar" onclick="ShowPopup('2')">
                           Senarai Kategori Penghutang  
                        </div>

                    </div>

                    <hr>
                    <div>
                        <h6>Maklumat Kategori Penghutang</h6>
                        <hr>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-col">
                                    <div class="mb-2 form-group d-flex align-items-center">
                                        <input type="text" class="form-control d-none" id="txtOldKod" name="txtOldKod" autocomplete="off" value="">
                                        <div class="col-sm">
                                            <label for="txtKod" class="form-label">Kod Kategori</label>
                                        </div>
                                        <div class="col-md">
                                            <input type="text" class="form-control" placeholder="" id="txtKod" name="txtKod" autocomplete="off">
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-sm">
                                            <label for="txtButiran" class="form-label">Butiran</label>
                                        </div>
                                        <div class="col-md">
                                            <input type="text" class="form-control" placeholder="" id="txtButiran" name="txtButiran" autocomplete="off">
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-sm">
                                            <label for="rdStatus" class="form-label">Status</label>
                                        </div>
                                        <div class="radio-btn-form col-md" id="rdStatus" name="rdStatus">
                                            <div class="form-check form-check-inline radio-size">
                                                <input type="radio" id="rdAktif" name="rdStatus" value="1"/>&nbsp;  Aktif
                                            </div>
                                            <div class="form-check form-check-inline radio-size">
                                                <input type="radio" id="rdTidakAktif" name="rdStatus" value="0"/>&nbsp;  Tidak Aktif
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-row">
                        <%--<div class="form-group col-md-6">
                            <asp:LinkButton id="lbtnKembali" class="btn btn-primary" runat="server" onclick="lbtnKembali_Click"><i class="las la-angle-left"></i>Kembali</asp:LinkButton>
                        </div>--%>
                        <div class="form-group col-md-12" align="right">
                            <button type="button" class="btn btn-danger btnBatal">Batal</button>
                            <button type="button" class="btn btn-success btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Simpan dan Hantar">Simpan</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Invois</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <!-- Create the dropdown filter -->
                        <div class="search-filter">
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-6">
                                    <label for="ddStatusFilter" class="col-sm-4 col-form-label">Status Kategori:</label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <select id="ddStatusFilter" class="custom-select" onchange="filterByStatus()">
                                                <option value="ALL">SEMUA</option>
                                                <option value="AKTIF">AKTIF</option>
                                                <option value="TIDAK AKTIF">TIDAK AKTIF</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-body">

                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table id="tblDataSenarai" class="table table-striped" style="width: 99%">
                                        <thead>
                                            <tr style="width:100%">
                                                <th scope="col" style="width: 15%">Kod Kategori</th>
                                                <th scope="col" style="width: 15%">Butiran</th>
                                                <th scope="col" style="width: 10%">Status</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableID_Senarai">
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

            $(document).ready(function () {
                InitializeSenaraiKategoriPenghutang();
            });

            async function ajaxSaveCategory(category) {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'PenghutangWS.asmx/SaveCategory',
                        method: 'POST',
                        data: JSON.stringify(category),
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            resolve(data.d);
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.error('Error:', errorThrown);
                            reject(false);
                        }
                    });
                })
            }
            $('.btnSimpan').click(async function () {
                var msg = "";
                var category = null;

                if ($('#txtOldKod').val() == "") {
                    category = {
                        category: {
                            Kod: $('#txtKod').val(),
                            OldKod: '',
                            Butiran: $('#txtButiran').val(),
                            Status: $("input[name='rdStatus']:checked").val()
                        }
                    }
                } else {
                    category = {
                        category: {
                            Kod: $('#txtKod').val(),
                            OldKod: $('#txtKod').val(),
                            Butiran: $('#txtButiran').val(),
                            Status: $("input[name='rdStatus']:checked").val(),
                        }
                    }
                }

                msg = "Anda pasti ingin menyimpan rekod ini?"

                if (!confirm(msg)) {
                    return false;
                }

                var result = JSON.parse(await ajaxSaveCategory(category));
                if (result.Status) {
                    alert(result.Message);
                    clearAllFields();
                    location.reload();
                } else {
                    alert(result.Message);
                }
            });

            $('.btnBatal').click(function () {
                clearAllFields();
            });

            function clearAllFields() {
                $('#txtKod').val('');
                $('#txtOldKod').val('');
                $('#txtButiran').val('');
                $("input[name='rdStatus']").prop('checked', false);
            }

            var categoryIndex = 0;
            $("#tableID_Senarai th").each(function (i) {
                if ($($(this)).html() == "Jenis Urusniaga") {
                    categoryIndex = i; return false;
                }
            });

            // call ajax to load data from server for Senarai Kategori Penghutang
            async function AjaxLoadRecord_SenaraiKategoriPenghutang() {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'PenghutangWS.asmx/GetKategoriPenghutangList',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ q: '' }), // Replace 'your_parameter_value' with the actual parameter value
                        success: function (data) {
                            resolve(data.d);
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.error('Error:', errorThrown);
                            console.error('Error:', xhr);
                            reject(false);
                        }
                    });
                });
            }

            async function InitializeSenaraiKategoriPenghutang() {
                var result = JSON.parse(await AjaxLoadRecord_SenaraiKategoriPenghutang())

                tbl = $("#tblDataSenarai").DataTable({
                    "responsive": true,
                    "searching": true,
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
                        "sInfoEmpty": "Menunjukkan 0 ke 0 dari rekod",
                        "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
                        "sEmptyTable": "Tiada rekod.",
                        "sSearch": "Carian"
                    },
                    "data": result,
                    "rowCallback": function (row, data) {
                        // Add hover effect
                        $(row).hover(function () {
                            $(this).addClass("hover pe-auto bg-warning");
                        }, function () {
                            $(this).removeClass("hover pe-auto bg-warning");
                        });

                        // Add click event
                        $(row).on("click", function () {
                            rowClickHandler(data);
                        });
                    },
                    "columns": [
                        { "data": "kodDetail" },
                        { "data": "butiran" },
                        {
                            "data": "status",
                            "render": function (data, type, row) {
                                var status = (data == "1") ? "AKTIF" : "TIDAK AKTIF";
                                return status;
                            }
                        }
                    ]
                });
            }

            // andling the row click event
            function rowClickHandler(item) {
                $('#permohonan').modal('toggle');
                $('#txtKod').val(item.kodDetail);
                $('#txtOldKod').val(item.kodDetail);
                $('#txtButiran').val(item.butiran);
                $("input[name='rdStatus'][value='" + item.status + "']").prop('checked', true);
            }
            // function to filter status in Senarai Penghutang table
            function filterByStatus() {
                var statusVal = $('#ddStatusFilter').val();
                var rex = new RegExp(statusVal)
                if (statusVal === "ALL") {
                    tbl.columns(2).search('').draw(); // Clear the status column filter
                } else if (statusVal == "TIDAK AKTIF") {
                    tbl.columns(2).search('^TIDAK AKTIF$', true, false).draw(); // Apply filter for TIDAK AKTIF status

                } else {
                    tbl.columns(2).search('^AKTIF$', true, false).draw(); // Apply filter for AKTIF status
                }
            }
        </script>
    </contenttemplate>
</asp:Content>
