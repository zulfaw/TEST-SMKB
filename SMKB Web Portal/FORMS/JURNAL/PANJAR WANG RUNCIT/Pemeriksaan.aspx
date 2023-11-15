<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Pemeriksaan.aspx.vb" Inherits="SMKB_Web_Portal.Pemeriksaan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.2/moment.min.js"></script>
    <script src="//cdn.datatables.net/plug-ins/1.10.12/sorting/datetime-moment.js"></script>

    <style>
        /*input CSS */
        .input-group {
            margin-bottom: 20px;
            position: relative;
        }

        .input-group__label {
            display: block;
            position: absolute;
            top: 0;
            line-height: 40px;
            color: #aaa;
            left: 5px;
            padding: 0 5px;
            transition: line-height 200ms ease-in-out, font-size 200ms ease-in-out, top 200ms ease-in-out;
            pointer-events: none;
        }

        .input-group__input {
            width: 100%;
            height: 40px;
            border: 1px solid #dddddd;
            border-radius: 5px;
            padding: 0 10px;
        }

            .input-group__input:not(:-moz-placeholder-shown) + label {
                background-color: white;
                line-height: 10px;
                opacity: 1;
                font-size: 10px;
                top: -5px;
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

                .input-group__input:focus + label {
                    color: #01080D;
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


        .input-group__label-floated {
            /* Apply styles for the floating label */
            /* For example: */
            top: -5px;
            font-size: 10px;
            line-height: 10px;
            color: #01080D;
            opacity: 1;
        }
    </style>

    <div id="PemeriksaanTab" class="tabcontent" style="display: block">

        <div class="modal fade" id="pemeriksaan" tabindex="-1" role="dialog"
            aria-labelby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Papar Permohonan</h5>
                        <button type="button" class="close" data-dismiss="modal" id="closeModal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-row">
                                    <div class="form-group col-md-3">
                                        <input type="text" class="input-group__input" id="lblNoSiri" name="No Wpr" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />
                                        <label class="input-group__label" for="No Wpr">No. WPR</label>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <input type="date" class="input-group__input form-control " id="txtTarikh" placeholder="&nbsp;" name="Tarikh Permohonan">
                                        <label class="input-group__label" for="Tarikh Permohonan">Tarikh</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="form-row">
                                    <div class="form-group col-md-6">
                                        <input type="text" class="input-group__input" id="lblPtj" name="No PTJ" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />
                                        <label class="input-group__label" for="No PTJ">No. PTj</label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <select class="input-group__select ui search dropdown Kumpulan" name="ddlKumpulan" id="ddlKumpulan"
                                            placeholder="&nbsp;" onchange="getBaki(this.value);">
                                        </select>
                                        <label class="input-group__label" for="ddlKumpulan">Kumpulan</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="form-row">
                            <h6>Permohonan</h6>
                            <div class="col-md-12">
                                <div class="application-table table-responsive">
                                    <table class="table table-striped" id="tblPemeriksaan">
                                        <thead>
                                            <tr>
                                                <th scope="col">Vot</th>
                                                <th scope="col">PTj</th>
                                                <th scope="col">Kumpulan Wang</th>
                                                <th scope="col">Operasi</th>
                                                <th scope="col">Projek</th>
                                                <th scope="col">No. Resit</th>
                                                <th scope="col">Butir Perbelanjaan</th>
                                                <th scope="col">Mohon(RM)</th>
                                                <th scope="col">Baki(RM)</th>
                                            </tr>
                                        </thead>

                                        <tbody class="tableID">
                                            <tr style="display: none; width: 100%" class="table-list">
                                                <td style="width: 20%">
                                                    <select class="ui search dropdown coa-list" id="ddlCoa" name="ddlCoa" style="width: 100%"></select>
                                                    <input type="hidden" class="data-id" value="" />
                                                    <label id="hidCoa" name="hidCoa" class="Hid-coa-list" style="visibility: hidden"></label>
                                                </td>
                                                <td>
                                                    <label id="kodptj" name="kodptj" class="kod-ptj"></label>
                                                    <label id="HidlblPTj" name="HidlblPTj" class="Hid-ptj-list" style="visibility: hidden"></label>
                                                </td>
                                                <td>
                                                    <label id="lblKumpWang" name="lblKumpWang" class="label-kump-wang"></label>
                                                    <label id="HidlblKumpWang" name="HidlblKumpWang" class="Hid-kump-wang" style="visibility: hidden"></label>
                                                </td>
                                                <td>
                                                    <label id="lblOperasi" name="lblOperasi" class="label-operasi"></label>
                                                    <label id="HidlblOperasi" name="HidlblOperasi" class="Hid-operasi" style="visibility: hidden"></label>
                                                </td>
                                                <td>
                                                    <label id="lblProjek" name="lblProjek" class="label-projek"></label>
                                                    <label id="HidlblProjek" name="HidlblProjek" class="Hid-projek" style="visibility: hidden"></label>
                                                </td>
                                                <td style="width: 10%">
                                                    <label name="lblResit" id="lblResit" class="label-resit"></label>
                                                    <label id="HidlblResit" name="HidlblResit" class="Hid-resit" style="visibility: hidden"></label>
                                                </td>
                                                <td>
                                                    <label name="lblButirPerb" id="lblButirPerb" class="label-butir"></label>
                                                    <label id="HidlblButir" name="HidlblButir" class="Hid-butir-list" style="visibility: hidden"></label>
                                                </td>
                                                <td style="width: 10%">
                                                    <input id="mohon" name="mohon" runat="server" type="number" class="form-control Mohon"
                                                        style="text-align: right" width="10%" readonly />
                                                </td>
                                                <td style="width: 10%">
                                                    <input id="baki" name="baki" runat="server" type="number" class="form-control Baki"
                                                        style="text-align: right" width="10%" readonly />
                                                </td>
                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="7"></td>
                                                <td>
                                                    <input class="form-control underline-input" id="lblJumlah" name="lblJumlah"
                                                        style="text-align: right; font-weight: bold" width="10%" readonly />
                                                </td>
                                                <td>
                                                    <input class="form-control underline-input" id="lblBakiSemasa" name="lblBakiSemasa"
                                                        style="text-align: right; font-weight: bold" width="10%" readonly />
                                                </td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-12" align="right">
                                <button type="button" class="btn btn-success btnLulus" data-toggle="tooltip"
                                    data-placement="bottom" title="Simpan dan Hantar">
                                    Lulus</button>
                                <button type="button" class="btn btn-danger btnXLulus" data-toggle="tooltip"
                                    data-placement="bottom" title="Simpan dan Hantar">
                                    Tidak Lulus</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="modalConfirm" tabindex="-1" role="dialog" aria-labelledby="modalConfirmLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="modalConfirmLabel">Pengesahan</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p id="confirmMessage"></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Tidak</button>
                        <button type="button" class="btn btn-secondary" id="confirmButton">Ya</button>
                    </div>
                </div>
            </div>
        </div>

        <div class="modal fade" id="resultModal" tabindex="-1" role="dialog" aria-labelledby="resultModalLabel" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="resultModalLabel">Makluman</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <p id="resultModalMessage"></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Tutup</button>
                    </div>
                </div>
            </div>
        </div>

        <div id="kelulusan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Permohonan</h5>
                    </div>

                    <!-- Create the dropdown filter -->
                    <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align: right">Carian :</label>
                                <div class="col-sm-8">
                                    <div class="input-group">
                                        <select id="categoryFilter" class="custom-select">
                                            <option value="">SEMUA</option>
                                            <option value="1" selected="selected">Hari Ini</option>
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
                                            <label id="lblMula" style="text-align: right; display: none;">Mula: </label>
                                        </div>

                                        <div class="form-group col-md-4">
                                            <input type="date" id="txtTarikhStart" name="txtTarikhStart" style="display: none;" class="form-control date-range-filter">
                                        </div>
                                        <div class="form-group col-md-1">
                                        </div>
                                        <div class="form-group col-md-1">
                                            <label id="lblTamat" style="text-align: right; display: none;">Tamat: </label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <input type="date" id="txtTarikhEnd" name="txtTarikhEnd" style="display: none;" class="form-control date-range-filter">
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class=" -body">
                        <div class="col-md-12">
                            <div class="application-table table-responsive">
                                <table id="tblDataSenarai_perm" class="table table-striped" style="width: 95%">
                                    <thead>
                                        <tr>
                                            <th scope="col">Tarikh Mohon</th>
                                            <th scope="col">No. WPR</th>
                                            <th scope="col">Jabatan/Fakulti</th>
                                            <th scope="col">Jumlah(RM)</th>
                                            <th scope="col">Status</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_perm">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script>

            function PopupSemak(elm) {

                if (elm == "1") {
                    $('#pemeriksaan').modal('toggle');
                }

                else if (elm == "2") {
                    $('.modal-body div').val("");
                    $('#pemeriksaan').modal('toggle');
                }
            }

            var tbl = null;
            var tbl2 = null
            var tableID_Senarai = "#tblDataSenarai_perm tbody"
            var tableID = "#tblPemeriksaan"
            var curNumObject = 0;
            var searchQuery = "";
            var oldSearchQuery = "";
            var shouldPop = true;
            var isClicked = false;

            $(document).ready(function () {
                $.fn.dataTable.moment('D-MM-YYYY');
                tbl = $('#tblDataSenarai_perm').DataTable({
                    responsive: true,
                    searching: true,
                    language: {
                        paginate: {
                            next: '<i class="fa fa-forward"></i>',
                            previous: '<i class="fa fa-backward"></i>',
                            first: '<i class="fa fa-step-backward"></i>',
                            last: '<i class="fa fa-step-forward"></i>'
                        },
                        lengthMenu: "Tunjuk _MENU_ rekod",
                        zeroRecords: "Tiada rekod yang sepadan ditemui",
                        info: "Menunjukkan _END_ dari _TOTAL_ rekod",
                        infoEmpty: "Menunjukkan 0 ke 0 daripada rekod",
                        infoFiltered: "(ditapis dari _MAX_ jumlah rekod)",
                        emptyTable: "Tiada rekod.",
                        search: "Carian"
                    },
                    ajax: {
                        url: "Panjar_WS.asmx/LoadOrderRecord_SenaraiPemeriksaanPermohonan",
                        method: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        dataSrc: function (json) {
                            return JSON.parse(json.d);
                        },
                        data: function () {
                            //Filter date bermula dari sini - 20 julai 2023
                            var startDate = $('#txtTarikhStart').val();
                            var endDate = $('#txtTarikhEnd').val();
                            return JSON.stringify({
                                category_filter: $('#categoryFilter').val(),
                                isClicked: isClicked,
                                tkhMula: startDate,
                                tkhTamat: endDate
                            })
                            //akhir sini
                        }
                    },
                    rowCallback: function (row, data) {
                        // Add hover effect
                        $(row).hover(function () {
                            $(this).addClass("hover pe-auto bg-warning");
                            $(this).css("cursor", "pointer");
                        }, function () {
                            $(this).removeClass("hover pe-auto bg-warning");
                        });

                        // Add click event
                        $(row).on("click", function () {
                            rowHoverClick(data.No_Wpr);
                        });
                    },
                    columns: [
                        { "data": "Tarikh_Mohon" },
                        {
                            "data": "No_Wpr",
                            render: function (data, type, row, meta) {
                                if (type !== "display") {

                                    return data;

                                }

                                var link = `<td style="width: 10%" >
                                                <label id="lblNoWpr" name="lblNoWpr" class="lblNoWpr" value="${data}" >${data}</label>
                                                <input type ="hidden" class = "lblNoWpr" value="${data}"/>
                                            </td>`;
                                return link;
                            }
                        },
                        { "data": "Kod_PTJ" },
                        { "data": "Jumlah_Belanja" },
                        {
                            "data": "Kod_Status_Dok",
                            render: function (data, type, row, meta) {

                                var link


                                link = `<td style="width: 10%" >
                                                <p id="lblStatus" name="lblStatus" class="lblStatus" value="${data}" style="color:red;" >${data}</p>                                              
                                            </td>`;

                                return link;
                            }
                        }
                    ]
                });

                //Set the change event for the Category Filter dropdown to redraw the datatable each time
                //a user selects a new filter.
                $("#categoryFilter").change(function (e) {

                    var selectedItem = $('#categoryFilter').val()
                    if (selectedItem == "6") {
                        $('#txtTarikhStart').show();
                        $('#txtTarikhEnd').show();

                        $('#lblMula').show();
                        $('#lblTamat').show();

                        $('#txtTarikhStart').val("")
                        $('#txtTarikhEnd').val("")
                    }
                    else {
                        $('#txtTarikhStart').hide();
                        $('#txtTarikhEnd').hide();

                        $('#txtTarikhStart').val("")
                        $('#txtTarikhEnd').val("")

                        $('#lblMula').hide();
                        $('#lblTamat').hide();

                    }
                });

                $('#ddlKumpulan').dropdown({
                    fullTextSearch: false,
                    apiSettings: {
                        url: 'Panjar_WS.asmx/GetKump?q={query}&kodptj={kodptj}',
                        method: 'POST',
                        dataType: "JSON",
                        contentType: 'application/json; charset=utf-8',
                        cache: false,
                        beforeSend: function (settings) {
                            // Replace {query} placeholder in data with user-entered search term
                            kptj = $('#lblPtj').text();
                            settings.urlData.kodptj = kptj
                            settings.data = JSON.stringify({ q: settings.urlData.query, kodptj: settings.urlData.kodptj });
                            searchQuery = settings.urlData.query;
                            return settings;
                        },
                        onSuccess: function (response) {
                            // Clear existing dropdown options
                            var obj = $(this);

                            var objItem = $(this).find('.menu');
                            $(objItem).html('');

                            // Add new options to dropdown
                            if (response.d.length === 0) {
                                $(obj.dropdown("clear"));
                                return false;
                            }

                            var listOptions = JSON.parse(response.d);

                            $.each(listOptions, function (index, option) {
                                $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.text));
                            });

                            //if (searchQuery !== oldSearchQuery) {
                            // $(obj).dropdown('set selected', $(this).find('.menu .item').first().data('value'));
                            //}

                            //oldSearchQuery = searchQuery;

                            // Refresh dropdown
                            $(obj).dropdown('refresh');

                            if (shouldPop === true) {
                                $(obj).dropdown('show');
                            }
                        }
                    }
                });
            });

            $('.btnSearch').click(async function () {
                // show_loader();
                isClicked = true;
                tbl.ajax.reload();
                //close_loader();
            })

            var ptj = null;

            function setDefault(theVal, defVal) {

                if (defVal === null || defVal === undefined) {
                    defVal = "";
                }

                if (theVal === "" || theVal === undefined || theVal === null) {
                    theVal = defVal;
                }
                return theVal;
            }

            function GetPejabat(ptj) {
                $.ajax({
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: "Panjar_WS.asmx/LoadRecord_PTj",
                    data: JSON.stringify({ ptj: ptj }),
                    success: function (data) {
                        var jsonData = JSON.parse(data.d);
                        $('#lblPtj').val(ptj + " - " + jsonData[0].Pejabat);
                    },
                    error: function (error) {
                        console.log("Error: " + error);
                    }
                });
            }

            $('.btnLulus').off('click').on('click', async function () {

                var msg = "";
                var id = $('#lblNoSiri').val();

                msg = "Anda pasti untuk meluluskan permohonan ini";
                $('#confirmMessage').html(msg);
                $('#modalConfirm').modal('show');

                $('#confirmButton').off('click').on('click', async function () {
                    $('#modalConfirm').modal('hide');
                    var result = JSON.parse(await ajaxSaveOrderSemak(id));

                    if (result.Status === "success")
                        showMessageBox("Success", result.Message, "success");
                    else
                        showMessageBox("Error", result.Message, "error");

                    $('.modal-body div').val("");
                    $('#pemeriksaan').modal('toggle');

                    tbl.ajax.reload();
                });

            });

            $('.btnXLulus').off('click').on('click', async function () {

                var msg = "";
                var id = $('#lblNoSiri').val();

                msg = "Anda pasti untuk TIDAK meluluskan semakan ini?";
                $('#confirmMessage').html(msg);
                $('#modalConfirm').modal('show');

                $('#confirmButton').off('click').on('click', async function () {
                    $('#modalConfirm').modal('hide');

                    var result = JSON.parse(await ajaxSaveOrderXSemak(id));

                    if (result.Status === "success")
                        showMessageBox("Success", result.Message, "success");
                    else
                        showMessageBox("Error", result.Message, "error");

                    $('.modal-body div').val("");
                    $('#pemeriksaan').modal('toggle');

                    tbl.ajax.reload();
                });
            });

            async function initDropdownCOA(id) {
                $('#' + id).dropdown({
                    fullTextSearch: true,
                    onChange: function (value, text, $selectedItem) {
                        var curTR = $(this).closest("tr");

                        var recordIDCoaHd = curTR.find("td > .Hid-coa-list");
                        recordIDCoaHd.html($($selectedItem).data("coltambah5"));

                        var recordIDPtjHd = curTR.find("td > .kod-ptj");
                        recordIDPtjHd.html($($selectedItem).data("coltambah1"));

                    },
                    apiSettings: {
                        url: 'Panjar_WS.asmx/GetCOA?q={query}',
                        method: 'POST',
                        dataType: "json",
                        contentType: 'application/json; charset=utf-8',
                        cache: false,
                        fields: {
                            value: "value",
                            text: "text",
                            colhidptj: "colhidptj",
                            colhidbutir: "colhidbutir",
                        },
                        beforeSend: function (settings) {
                            // Replace {query} placeholder in data with user-entered search term

                            //settings.urlData.param2 = "secondvalue";

                            settings.data = JSON.stringify({ q: settings.urlData.query });

                            //searchQuery = settings.urlData.query;

                            return settings;
                        },
                        onSuccess: function (response) {
                            // Clear existing dropdown options
                            var obj = $(this);

                            var objItem = $(this).find('.menu');
                            $(objItem).html('');

                            // Add new options to dropdown
                            if (response.d.length === 0) {
                                $(obj.dropdown("clear"));
                                return false;
                            }

                            var listOptions = JSON.parse(response.d);

                            $.each(listOptions, function (index, option) {
                                //$(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.text));
                                $(objItem).append($('<div class="item" data-value="' + option.value + '" data-coltambah5="' + option.colhidbutir + '" data-coltambah1="' + option.colhidptj + '" >').html(option.text));
                            });

                            // Refresh dropdown
                            $(obj).dropdown('refresh');

                            if (shouldPop === true) {
                                $(obj).dropdown('show');
                            }
                        }
                    }
                });
            }

            function showMessageBox(title, message, type) {
                $('#resultModalMessage').html(message);
                if (type === "success")
                    $('#resultModal').removeClass("modal-error").addClass("modal-success");
                else if (type === "error")
                    $('#resultModal').removeClass("modal-success").addClass("modal-error");

                $('#resultModal').modal("show");
            }

            async function AddRow(totalClone, objOrder) {
                var counter = 1;
                var table = $('#tblPemeriksaan');

                if (objOrder !== null && objOrder !== undefined) {
                    totalClone = objOrder.Payload.length;
                }

                while (counter <= totalClone) {
                    curNumObject += 1;

                    var newId_coa = "ddlCoa" + curNumObject;
                    var row = $('#tblPemeriksaan tbody>tr:first').clone();
                    var dropdown5 = $(row).find(".coa-list").attr("id", newId_coa);

                    var val = ""
                    row.attr("style", "");
                    $('#tblPemeriksaan tbody').append(row);

                    await initDropdownCOA(newId_coa)

                    $(newId_coa).api("query");

                    if (objOrder !== null && objOrder !== undefined) {
                        //await setValueToRow(row, objOrder.Payload.OrderDetails[counter - 1]);
                        if (counter <= objOrder.Payload.length) {
                            await setValueToRow_Pemeriksaan(row, objOrder.Payload[counter - 1]);
                        }
                    }
                    counter += 1;
                }
            }

            function NumDefault(theVal) {

                return setDefault(theVal, 0)
            }

            async function setValueToRow_HdrPemeriksaan(orderMDetail) {

                $('#lblNoSiri').val(orderMDetail.No_Wpr);
                $('#txtTarikh').val(orderMDetail.Tarikh_Mohon);
                $('#lblPtj').html(orderMDetail.Kod_PTJ);
                GetPejabat(orderMDetail.Kod_PTJ);

                var newId = $('#ddlKumpulan')

                var ddlKumpulan = $('#ddlKumpulan')
                var ddlSearch = $('#ddlKumpulan')
                var ddlText = $('#ddlKumpulan')
                var selectObj_Kumpulan = $('#ddlKumpulan')
                $(ddlKumpulan).dropdown('set selected', orderMDetail.Kod_Kump_Wang);
                selectObj_Kumpulan.append("<option value = '" + orderMDetail.Kod_Kump_Wang + "'>" + orderMDetail.ButiranKumpulan + "</option>")

                //status - permohonan & kemaskini         

                if (orderMDetail.Kod_Status_Dok == "03") {

                    $('.btnSimpan').hide();
                }
                else {

                    $('.btnSimpan').show();

                }
            }

            async function clearAllRows() {
                $(tableID + ">tbody>tr").each(function (index, obj) {
                    if (index > 0) {
                        obj.remove();
                    }
                })
            }

            async function setValueToRow_Pemeriksaan(row, orderMDetail) {

                var ddl = $(row).find("td > .coa-list");
                var ddlSearch = $(row).find("td > .coa-list > .search");
                var ddlText = $(row).find("td > .coa-list > .text");
                var selectObj = $(row).find("td > .coa-list > select");
                $(ddl).dropdown('set selected', orderMDetail.Kod_Vot);
                selectObj.append("<option value = '" + orderMDetail.Kod_Vot + "'>" + orderMDetail.Kod_Vot + ' - ' + orderMDetail.Butiran + "</option>")

                var colhidcoa = $(row).find("td > .Hid-coa-list");
                colhidcoa.html(orderMDetail.Kod_Vot);

                var colptj = $(row).find("td > .kod-ptj");
                colptj.html(orderMDetail.ButiranPTJ);

                var colhidptj = $(row).find("td > .Hid-ptj-list");
                colhidptj.html(orderMDetail.Kod_PTJ);

                var colkw = $(row).find("td > .label-kump-wang");
                colkw.html(orderMDetail.ButiranKw);

                var colhidkw = $(row).find("td > .Hid-kump-wang");
                colhidkw.html(orderMDetail.Kod_Kump_Wang);

                var colKo = $(row).find("td > .label-operasi");
                colKo.html(orderMDetail.ButiranKo);

                var colhidKo = $(row).find("td > .Hid-operasi");
                colhidKo.html(orderMDetail.Kod_Operasi);

                var colKp = $(row).find("td > .label-projek");
                colKp.html(orderMDetail.ButiranKp);

                var colhidKp = $(row).find("td > .Hid-projek");
                colhidKp.html(orderMDetail.Kod_Projek);

                var butir = $(row).find("td > .label-butir");
                butir.html(orderMDetail.Butiran_Belanja);

                var noresit = $(row).find("td > .label-resit");
                noresit.html(orderMDetail.No_Resit);

                var mohon = $(row).find("td > .Mohon");
                mohon.val(orderMDetail.Jumlah_Butiran);

                var jumlah = $(row).find("td > .Jumlah");
                jumlah.val(orderMDetail.Jumlah_Butiran);

                var baki = $(row).find("td > .Baki");
                baki.val(orderMDetail.Baki);

                

                await calculateGrandTotal();

            }

            async function calculateGrandTotal() {

                //Baki Semasa
                var totalBaki = $('#lblBakiSemasa');
                var curTotal_Baki = 0;

                $('.Baki').each(function (index, obj) {
                    curTotal_Baki += parseFloat(NumDefault($(obj).val()));
                });
                totalBaki.val(curTotal_Baki.toFixed(2));

                //Jumlah
                var totalJumlah = $('#lblJumlah');
                var curTotal_Jumlah = 0;

                $('.Mohon').each(function (index, obj) {
                    curTotal_Jumlah += parseFloat(NumDefault($(obj).val()));
                });
                totalJumlah.val(curTotal_Jumlah.toFixed(2));

            }

            function NumDefault(theVal) {

                return setDefault(theVal, 0)
            }

            async function rowHoverClick(id) {
                if (id !== "") {
                    var recordHdr = await AjaxGetRecordHdrPemeriksaan(id);
                    await AddRowHeader(null, recordHdr);

                    var record = await AjaxGetRecordPermohonan(id);
                    await clearAllRows();
                    await AddRow(null, record);
                    PopupSemak(2);
                }
            }

            async function AddRowHeader(totalClone, objOrder) {
                var counter = 1;
                if (objOrder !== null && objOrder !== undefined) {
                    totalClone = objOrder.Payload.length;
                }

                if (counter <= objOrder.Payload.length) {
                    await setValueToRow_HdrPemeriksaan(objOrder.Payload[counter - 1]);
                }
            }

            async function AjaxGetRecordPermohonan(id) {

                try {

                    const response = await fetch('Panjar_WS.asmx/LoadRecordPermohonan', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({ id: id })
                    });
                    const data = await response.json();
                    return JSON.parse(data.d);
                } catch (error) {
                    console.error('Error:', error);
                    return false;
                }
            }

            async function AjaxGetRecordHdrPemeriksaan(id) {

                try {

                    const response = await fetch('Panjar_WS.asmx/LoadHdrPermohonan', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({ id: id })
                    });
                    const data = await response.json();
                    return JSON.parse(data.d);
                } catch (error) {
                    console.error('Error:', error);
                    return false;
                }
            }

            async function ajaxSaveOrderSemak(id) {

                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'Panjar_WS.asmx/SemakOrderLulus',
                        method: 'POST',
                        data: JSON.stringify({ id: id }),
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            resolve(data.d);
                            //alert(resolve(data.d));
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.error('Error:', errorThrown);
                            reject(false);
                        }
                    });
                })
            }

            async function ajaxSaveOrderXSemak(id) {

                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'Panjar_WS.asmx/SemakOrderXLulus',
                        method: 'POST',
                        data: JSON.stringify({ id: id }),
                        dataType: 'json',
                        contentType: 'application/json; charset=utf-8',
                        success: function (data) {
                            resolve(data.d);
                            //alert(resolve(data.d));
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.error('Error:', errorThrown);
                            reject(false);
                        }
                    });
                })
            }
        </script>
    </div>
</asp:Content>
