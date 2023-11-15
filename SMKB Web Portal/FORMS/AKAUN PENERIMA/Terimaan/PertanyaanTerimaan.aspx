<%@ Page Title="" Language="vb" Async="true" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Kelulusan_Terima.aspx.vb" Inherits="SMKB_Web_Portal.Kelulusan_Terima" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
    <contenttemplate>
        <style>
            .default-primary {
                background-color: #007bff !important;
                color: white;
            }
            .default-primary {
                background-color: #007bff !important;
                color: white;
            }

            /*input CSS */
            .input-group {
                margin-bottom: 20px;
                position: relative;
            }

            .input-group__input {
                width: 100%;
                height: 40px;
                border: 1px solid #dddddd;
                border-radius: 5px;
                padding: 0 10px;
            }

            .input-group__input:not(:-ms-input-placeholder) + label {
                background-color: white;
                line-height: 10px;
                opacity: 1;
                font-size: 10px;
                top: -5px;
            }

            .input-group__input:not(:placeholder-shown) + label, .input-group__input:focus + label {
                background-color: white;
                line-height: 10px;
                opacity: 1;
                font-size: 10px;
                top: -5px;
            }

            .input-group__input:focus {
                outline: none;
                border: 1px solid #01080D;
            }

            .input-group__select + label {
                background-color: white;
                line-height: 10px;
                opacity: 1;
                font-size: 10px;
                top: -5px;
            }

            .input-group__select:focus + label {
                color: #01080D;
            }

            /* Styles for the focused dropdown */
            .input-group__select:focus {
                outline: none;
                border: 1px solid #01080D;
            }
        </style>
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <div id="SenaraiKelulusan">
                <div>
                    <div class="modal-body">
                        <div class="modal-header">
                            <h5 class="modal-title">Senarai Resit</h5>
                        </div>
                        <!-- Create the dropdown filter -->
                        <div class="search-filter">
                             <div class="form-row justify-content-center">
                                  <div class="form-group row col-md-6">
                                     <label for="" class="col-sm-1 col-form-label">Carian:</label>
                                     <div class="col-sm-8">
                                         <div class="input-group">
                                             <select id="categoryFilter" class="custom-select">
                                                 <option value="" selected>Semua</option>
                                                 <option value="1">Hari Ini</option>
                                                 <option value="2">Semalam</option>
                                                 <option value="3">7 Hari Lepas</option>
                                                 <option value="4">30 Hari Lepas</option>
                                                 <option value="5">60 Hari Lepas</option>
                                                 <option value="6">Pilih Tarikh</option>
                                             </select>
                                             <div class="input-group-append">
                                                 <button id="btnSearch" runat="server" class="btn btn-outline btnSearch" type="button">
                                                     <i class="fa fa-search"></i>
                                                     Cari
                                                 </button>
                                             </div> 
                                         </div>
                                     </div>
                                    <div class="col-sm-10 mt-4 d-none" id="divDatePicker">
                                        <div class="d-flex flex-row justify-content-around align-items-center">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label text-nowrap">Mula:</label>
                                                <div class="col-sm-9">
                                                    <input type="date" id="txtTarikhStart" name="txtTarikhStart" class="form-control date-range-filter">
                                                </div>
                                            </div>

                                            <div class="form-group row ml-3">
                                                <label class="col-sm-3 col-form-label text-nowrap">Tamat:</label>
                                                <div class="col-sm-9">
                                                    <input type="date" id="txtTarikhEnd" name="txtTarikhEnd" class="form-control date-range-filter">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table id="tblDataSenarai" class="table table-striped">
                                        <thead>
                                            <tr style="width: 100%">
                                                <th scope="col" style="width: 10%">No. Resit</th>
                                                <th scope="col" style="width: 5%">Bil. Item</th>
                                                <th scope="col" style="width: 15%">Pembayar</th>
                                                <th scope="col" style="width: 10%">Jenis Urusniaga</th>
                                                <th scope="col" style="width: 10%">Mod Terima</th>
                                                <th scope="col" style="width: 10%">Tarikh Resit</th>
                                                <th scope="col" style="width: 15%">Jumlah Terima</th>
                                                <th scope="col" style="width: 15%">Nama Penyedia</th>
                                                <th scope="col" style="width: 10%">Status</th>
                                            </tr>
                                        </thead>
                                        <tbody id=" ">
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
        var tbl = null
        var isClicked = false;
        var category_filter, startDate, endDate;
        $(document).ready(function () {
            tbl = $("#tblDataSenarai").DataTable({
                "responsive": true,
                "searching": true,
                "sPaginationType": "full_numbers",
                "oLanguage": {
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
                },
                "ajax":
                {
                    "url": "TerimaWS.asmx/LoadOrderRecord_PertanyaanTerimaan",
                    type: 'POST',
                    data: function (d) {
                        return "{ category_filter: '" + category_filter + "',isClicked: '" + isClicked + "',tkhMula: '" + startDate + "',tkhTamat: '" + endDate + "'}";
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
                    { "data": "No_Dok" },
                    { "data": "BILITEM" },
                    { "data": "Nama_Penghutang" },
                    { "data": "UrusNiaga" },
                    { "data": "MOD_TERIMA" },
                    { "data": "Tkh_Daftar" },
                    {
                        "data": "Jumlah_Bayar",
                        "render": function (data, type, row, meta) {
                            if (type === "display") {
                                return data.toLocaleString('en-MY', { valute: 'MYR', minimumFractionDigits: 2 });
                            }
                            return data;
                        }
                    },
                    { "data": "MS01_Nama" },
                    { "data": "Status" }
                ],
            });
            $('.btnSearch').click(async function () {
                show_loader();
                isClicked = true;
                category_filter = $('#categoryFilter').val();

                if (category_filter == "6") {
                    startDate = $('#txtTarikhStart').val();
                    endDate = $('#txtTarikhEnd').val();
                } else {
                    startDate = "";
                    endDate = "";
                }
                tbl.ajax.reload();
            })
        });
        $("#categoryFilter").change(function () {
            var selectedValue = $('#categoryFilter').val()

            if (selectedValue === "6") {
                // Show the date inputs
                $('#divDatePicker').removeClass("d-none").addClass("d-flex");
            } else {
                // Hide the date inputs
                $('#divDatePicker').removeClass("d-flex").addClass("d-none");

            }
        });
    </script>
</asp:Content>
