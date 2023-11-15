<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Permohonan.aspx.vb" Inherits="SMKB_Web_Portal.Permohonan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

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

        input::-webkit-outer-spin-button,
        input::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }
    </style>

    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.2/moment.min.js"></script>
    <script src="//cdn.datatables.net/plug-ins/1.10.12/sorting/datetime-moment.js"></script>


    <div id="PermohonanTab" class="tabcontent" style="display: block">
        <div class="modal fade" id="kemaskini" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Permohonan</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
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

                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="application-table table-responsive">
                                <table id="tblDataSenarai_perm" class="table table-striped" style="width: 95%">
                                    <thead>
                                        <tr>
                                            <th scope="col">Tarikh Mohon</th>
                                            <th scope="col">No. WPR</th>
                                            <th scope="col">No. PTj</th>
                                            <th scope="col">Amaun (RM)</th>
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

        <div class="modal-body">
            <div class="table-title">
                <h5>Kemaskini Permohonan</h5>
                <div class="btn btn-primary btnPaparan" onclick="PopupSenarai('2')">
                    <i class="fa fa-list"></i>Senarai Permohonan
                </div>
            </div>

            <hr />
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

                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <input type="text" class="input-group__input" id="lblNoPtj" name="No PTJ" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />
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

        <div class="form-row">

            <h6>Permohonan</h6>

            <div class="col-md-12">
                <div class="application-table table-responsive">
                    <table class="table table-striped" id="tblPermohonan">
                        <thead>
                            <tr>
                                <th scope="col">Vot</th>
                                <th scope="col">PTj</th>
                                <th scope="col">Kumpulan Wang</th>
                                <th scope="col">Operasi</th>
                                <th scope="col">Projek</th>
                                <th scope="col">No. Resit</th>
                                <th scope="col">Butir Perbelanjaan</th>
                                <th scope="col">Jumlah</th>
                                <th scope="col">Baki</th>
                            </tr>
                        </thead>

                        <tbody id="tableID">
                            <tr style="display: none; width: 100%;" class="table-list">
                                <td style="width: 20%">
                                    <select class="ui search dropdown coa-list" id="ddlCoa" name="ddlCoa" style="width: 100%"></select>
                                    <input type="hidden" class="data-id" value="" />
                                    <label id="hidCoa" name="hidCoa" class="Hid-coa-list" style="visibility: hidden"></label>
                                </td>
                                <td>
                                    <label id="lblPTj" name="lblPTj" class="label-ptj-list"></label>
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
                                    <input type="text" name="lblResit" id="lblResit" class="form-control label-resit" maxlength="30" />
                                    <label id="HidlblResit" name="HidlblResit" class="Hid-resit" style="visibility: hidden"></label>
                                </td>
                                <td style="width: 15%">
                                    <input type="text" name="lblButirPerb" id="lblButirPerb" class="form-control label-butir" maxlength="100" />
                                    <label id="HidlblButir" name="HidlblButir" class="Hid-butir-list" style="visibility: hidden"></label>
                                </td>
                                <td style="width: 10%">
                                    <input id="jumlah" name="jumlah" runat="server" type="number" class="form-control Jumlah"
                                        style="text-align: right" width="10%" oninput="validate(this)" />
                                </td>
                                <td style="width: 10%">
                                    <input id="baki" name="baki" runat="server" type="number" class="form-control Baki"
                                        style="text-align: right" width="10%" readonly />
                                </td>

                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="7">
                                    <button type="button" class="btn btn-warning btnAddRow" data-val="1" value="1"><b>+ Tambah</b></button>
                                    <button type="button" class="btn btn-warning btnAddRow five"
                                        data-val="5" value="5" style="visibility: hidden">
                                        <b>+ Tambah</b></button>
                                </td>
                                <td>
                                    <input class="form-control underline-input" id="lblJumPermohonan" name="lblJumPermohonan"
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
                <button type="button" class="btn btn-danger btnPadam">Padam</button>
                <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Draft">Simpan</button>
                <button type="button" class="btn btn-success btnHantar" data-toggle="tooltip" data-placement="bottom" title="Simpan dan Hantar">Hantar</button>
            </div>
        </div>
    </div>

    <script>

        function PopupSenarai(elm) {

            if (elm == "1") {
                $('#kemaskini').modal('toggle');
            }

            else if (elm == "2") {
                $('.btnSearch').click();
                $('.modal-body div').val("");
                $('#kemaskini').modal('toggle');
            }
        }

        var tbl = null
        var isClicked = false;
        var today = "";
        var balance = 0;

        $(document).ready(async function () {
            var d = new Date();
            var day = ("0" + d.getDate()).slice(-2);
            var month = ("0" + (d.getMonth() + 1)).slice(-2);
            today = d.getFullYear() + "-" + (month) + "-" + (day);
            $('#txtTarikh').val(today);

            await calculateGrandTotal();

            $.fn.dataTable.moment('D-MM-YYYY');
            tbl = $('#tblDataSenarai_perm').DataTable({
                responsive: true,
                searching: true,
                pagingType: "full_numbers",
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
                    infoEmpty: "Menunjukkan 0 ke 0 dari rekod",
                    emptyTable: "Tiada rekod",
                    search: "Carian"
                },
                ajax: {
                    url: "Panjar_WS.asmx/LoadOrderRecord_SenaraiPermohonan",
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
                    { "data": "Kod_Status_Dok" }
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
        });

        $('.btnSearch').click(async function () {
            // show_loader();
            isClicked = true;
            tbl.ajax.reload();
            //close_loader();
        })

        var validate = function (e) {
            var t = e.value;
            e.value = (t.indexOf(".") >= 0) ? (t.substr(0, t.indexOf(".")) + t.substr(t.indexOf("."), 3)) : t;
        }

        var curNumObject = 0;
        var searchQuery = "";
        var oldSearchQuery = "";
        var shouldPop = true;
        $('#lblNoPtj').html(<%= Session("ssusrKodPTj")%>);
        var ptj = $('#lblNoPtj').text();
        var tableID = '#tblPermohonan';
        var tableID_Senarai = '#tblDataSenarai_perm tbody';
        GetPejabat(ptj);

        $('.Jumlah').val("0.00");
        $('.Baki').val("0.00");
        $('#lblBakiSemasa').val('0.00');

        var totalMohon = '#lblJumPermohonan';
        var baki = '#lblBakiSemasa';

        function GetPejabat(ptj) {
            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: "Panjar_WS.asmx/LoadRecord_PTj",
                data: JSON.stringify({ ptj: ptj }),
                success: function (data) {
                    var jsonData = JSON.parse(data.d);
                    $('#lblNoPtj').val(ptj + " - " + jsonData[0].Pejabat);
                },
                error: function (error) {
                    console.log("Error: " + error);
                }
            });
        }

        async function getBaki(kumpulan) {

            await AjaxGetAgih_Baki(ptj, kumpulan);
        }

        var objMetadata = [{
            "obj1": {
                "id": "",
                "oldSearchQurey": "",
                "searchQuery": ""
            }
        }, {
            "obj2": {
                "id": "",
                "oldSearchQurey": "",
                "searchQuery": ""
            }
        }]

        $(document).ready(function () {

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

                        var kptj = $('#lblNoPtj').text();

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

        $('.btnPaparan').click(async function () {
            tbl.ajax.reload();
        });

        $(function () {
            $('.btnAddRow.five').click();
        });

        $('.btnHantar').off('click').on('click', async function () {
            var jumRecord = 0;
            var acceptedRecord = 0;
            var msg = "";

            msg = "Anda pasti ingin menghantar rekod ini?";
            $('#confirmMessage').html(msg);
            $('#modalConfirm').modal('show');

            $('#confirmButton').off('click').on('click', async function () {
                $('#modalConfirm').modal('hide');

                var newOrderM = {
                    orderM: {
                        OrderMId: $('#lblNoSiri').val(),
                        Tarikh: $('#txtTarikh').val(),
                        NoPtj: $('#lblNoPtj').text(),
                        Kumpulan: $('#ddlKumpulan').val(),
                        Jumlah: $('#lblJumPermohonan').val(),
                        OrderMDetails: []
                    }

                }


                $('.coa-list').each(function (index, obj) {

                    if (index > 0) {
                        var tcell = $(obj).closest("td");
                        //alert("ce;; "+tcell)
                        var orderMDetail = {
                            OrderMId: $('#lblNoSiri').val(),
                            butiran: $('.label-butir').eq(index).val(),
                            resit: $('.label-resit').eq(index).val(),
                            kump: $('#ddlKumpulan').val(),
                            ptj: $('.Hid-ptj-list').eq(index).html(),
                            kw: $('.Hid-kump-wang').eq(index).html(),
                            ko: $('.Hid-operasi').eq(index).html(),
                            kp: $('.Hid-projek').eq(index).html(),
                            ddlcoa: $(obj).dropdown("get value"),
                            jumlah: $('.Jumlah').eq(index).val(),
                            baki: $('.Baki').eq(index).val(),
                            id: $(tcell).find(".data-id").val()
                        };

                        acceptedRecord += 1;
                        newOrderM.orderM.OrderMDetails.push(orderMDetail);

                    }

                });

                var result = JSON.parse(await ajaxSubmitOrder(newOrderM));

                if (result.Status === "success")
                    showMessageBox("Success", result.Message, "success");
                else
                    showMessageBox("Error", result.Message, "error");

                $('#lblNoSiri').val("");
                $('#lblJumPermohonan').val("");
                $('#lblBakiSemasa').val("");
                await clearAllRows();
                await clearAllRowsHdr();
                await clearHiddenButton();
                AddRow(5);
            });

        });

        $('.btnSimpan').off('click').on('click', async function () {
            var jumRecord = 0;
            var acceptedRecord = 0;
            var msg = "";

            msg = "Anda pasti ingin menyimpan rekod ini?";
            $('#confirmMessage').html(msg);
            $('#modalConfirm').modal('show');

            $('#confirmButton').off('click').on('click', async function () {
                $('#modalConfirm').modal('hide')

                var newOrderM = {
                    orderM: {
                        OrderMId: $('#lblNoSiri').val(),
                        Tarikh: $('#txtTarikh').val(),
                        NoPtj: $('#lblNoPtj').text(),
                        Kumpulan: $('#ddlKumpulan').val(),
                        Jumlah: $('#lblJumPermohonan').val(),
                        OrderMDetails: []
                    }

                }


                $('.coa-list').each(function (index, obj) {

                    if (index > 0) {
                        var tcell = $(obj).closest("td");
                        //alert("ce;; "+tcell)
                        var orderMDetail = {
                            OrderMId: $('#lblNoSiri').val(),
                            butiran: $('.label-butir').eq(index).val(),
                            resit: $('.label-resit').eq(index).val(),
                            ptj: $('.Hid-ptj-list').eq(index).html(),
                            kw: $('.Hid-kump-wang').eq(index).html(),
                            ko: $('.Hid-operasi').eq(index).html(),
                            kp: $('.Hid-projek').eq(index).html(),
                            ddlcoa: $(obj).dropdown("get value"),
                            jumlah: $('.Jumlah').eq(index).val(),
                            baki: $('.Baki').eq(index).val(),
                            id: $(tcell).find(".data-id").val()
                        };

                        acceptedRecord += 1;
                        newOrderM.orderM.OrderMDetails.push(orderMDetail);
                    }

                });

                var result = JSON.parse(await ajaxSaveOrder(newOrderM));
                if (result.Status === "success") {
                    showMessageBox("Success", result.Message, "success");
                }
                else
                    showMessageBox("Error", result.Message, "error");


                $('#lblNoSiri').val("");
                $('#lblJumPermohonan').val("");
                $('#lblBakiSemasa').val("");
                await clearAllRows();
                await clearAllRowsHdr();
                await clearHiddenButton();
                AddRow(5);
            });
        });





        function showMessageBox(title, message, type) {
            $('#resultModalMessage').html(message);
            if (type === "success")
                $('#resultModal').removeClass("modal-error").addClass("modal-success");
            else if (type === "error")
                $('#resultModal').removeClass("modal-success").addClass("modal-error");

            $('#resultModal').modal("show");
        }

        //$('#ddlKumpulan').on('change', function (e) {
        //    var optionSelected = $("option:selected", this);
        //    var valueSelected = this.value;
        //    console.log(valueSelected);
        //});

        async function clearAllRowsHdr() {
            $('#lblNoSiri').val("");
            $('#txtTarikh').val(today);
            var ptj = $('#lblNoPtj').html(<%= Session("ssusrKodPTj")%>);
            $("#ddlKumpulan").dropdown('clear');
            $("#ddlKumpulan").dropdown('refresh');
            GetPejabat(ptj);
        }

        async function clearHiddenButton() {

            $('.btnSimpan').show();
            $('.btnHantar').show();
            $('.btnAddRow').show();

        }

        async function clearAllRows_senarai() {
            $(tableID_Senarai + " > tbody > tr ").each(function (index, obj) {
                if (index > 0) {
                    obj.remove();
                }
            })
        }

        function setDefault(theVal, defVal) {

            if (defVal === null || defVal === undefined) {
                defVal = "";
            }

            if (theVal === "" || theVal === undefined || theVal === null) {
                theVal = defVal;
            }
            return theVal;
        }

        async function initDropdownCOA(id) {
            $('#' + id).dropdown({
                fullTextSearch: true,
                onChange: function (value, text, $selectedItem) {
                    var curTR = $(this).closest("tr");

                    var recordIDPtj = curTR.find("td > .label-ptj-list");
                    recordIDPtj.html($($selectedItem).data("coltambah1"));

                    var recordIDHidPtj = curTR.find("td > .Hid-ptj-list");
                    recordIDHidPtj.html($($selectedItem).data("coltambah5"));

                    var recordIDKw = curTR.find("td > .label-kump-wang");
                    recordIDKw.html($($selectedItem).data("coltambah2"));

                    var recordIDHidKw = curTR.find("td > .Hid-kump-wang");
                    recordIDHidKw.html($($selectedItem).data("coltambah6"));

                    var recordIDKo = curTR.find("td > .label-operasi");
                    recordIDKo.html($($selectedItem).data("coltambah3"));

                    var recordIDHidKo = curTR.find("td > .Hid-operasi");
                    recordIDHidKo.html($($selectedItem).data("coltambah7"));

                    var recordIDKp = curTR.find("td > .label-projek");
                    recordIDKp.html($($selectedItem).data("coltambah4"));

                    var recordIDHidKp = curTR.find("td > .Hid-projek");
                    recordIDHidKp.html($($selectedItem).data("coltambah8"));

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
                        colPTJ: "colPTJ",
                        colKW: "colKW",
                        colKo: "colKo",
                        colKp: "colKp",
                        colhidPtj: "colhidPtj",
                        colhidKw: "colhidKw",
                        colhidKo: "colhidKo",
                        colhidKp: "colhidKp"
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
                            $(objItem).append($('<div class="item" data-value="' + option.value + '" data-coltambah1="' + option.colPTJ + '" data-coltambah5="' + option.colhidPtj +
                                '" data-coltambah2="' + option.colKW + '" data-coltambah6="' + option.colhidKw + '" data-coltambah3="' + option.colKo + '" data-coltambah7="' +
                                option.colhidKo + '" data-coltambah4="' + option.colKp + '" data-coltambah8="' + option.colhidKp + '" >').html(option.text));
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

        $('.btnAddRow').click(async function () {
            var totalClone = $(this).data("val");
            await AddRow(totalClone);
        });

        async function AddRow(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblPermohonan');

            if (objOrder !== null && objOrder !== undefined) {
                totalClone = objOrder.Payload.length;
            }

            while (counter <= totalClone) {
                curNumObject += 1;

                var newId_coa = "ddlCoa" + curNumObject;
                var row = $('#tblPermohonan tbody>tr:first').clone();
                var dropdown5 = $(row).find(".coa-list").attr("id", newId_coa);

                var val = ""

                row.attr("style", "");
                $('#tblPermohonan tbody').append(row);

                await initDropdownCOA(newId_coa)

                $(newId_coa).api("query");

                if (objOrder !== null && objOrder !== undefined) {
                    //await setValueToRow(row, objOrder.Payload.OrderDetails[counter - 1]);
                    if (counter <= objOrder.Payload.length) {
                        await setValueToRow_Permohonan(row, objOrder.Payload[counter - 1]);
                    }
                }
                counter += 1;
            }
        }

        $('.btnPadam').off('click').on('click', async function () {

            var msg = ""
            var id = $('#lblNoSiri').val();

            msg = "Adakah anda pasti untuk menghapuskan rekod ini?"
            $('#confirmMessage').html(msg);
            $('#modalConfirm').modal('show');

            $('#confirmButton').off('click').on('click', async function () {
                $('#modalConfirm').modal('hide');

                var result = JSON.parse(await ajaxDeleteOrder(id));
                if (result.Status === "success")
                    showMessageBox("Success", result.Message, "success");
                else
                    showMessageBox("Error", result.Message, "error");

                $('#lblNoSiri').val("");
                $('#lblJumPermohonan').val("");
                $('#lblBakiSemasa').val("");
                await clearAllRows();
                await clearAllRowsHdr();
                await clearHiddenButton();

                AddRow(5);
            });

        });

        async function clearAllRows() {
            $(tableID + ">tbody>tr").each(function (index, obj) {
                if (index > 0) {
                    obj.remove();
                }
            })
            $('.Jumlah').val("0.00");
            $('.Baki').val("0.00");
        }

        async function ajaxDeleteOrder(id) {

            return new Promise((resolve, reject) => {
                $.ajax({
                    url: 'Panjar_WS.asmx/DeleteOrder',
                    method: 'POST',
                    data: JSON.stringify({ id: id }),
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
            });
        }

        async function AjaxLoadOrderRecord_Senarai(id) {

            try {
                const response = await fetch('Panjar_WS.asmx/LoadOrderRecord_SenaraiPermohonan', {
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

        $(tableID).on('keyup', '.Jumlah , .Baki', async function () {

            calculateGrandTotal();

            var curTR = $(this).closest("tr");
            var totalBaki = $(baki);

            var jumlah_ = $(curTR).find("td > .Jumlah");
            var totalDebit = NumDefault(jumlah_.val())

            var newBaki = balance - $(totalMohon).val();
            console.log($(totalMohon).val());
            baki_ = $(curTR).find("td > .Baki").val(parseFloat(newBaki).toFixed(2));
            totalBaki.val(baki_.val());
            calculateGrandTotal();
        });

        async function calculateGrandTotal() {
         
            //Jumlah Permohonan
            var totalJumlah = $(totalMohon);
            var curTotal_Jumlah = 0;

            $('.Jumlah').each(function (index, obj) {
                curTotal_Jumlah += parseFloat(NumDefault($(obj).val()));
            });
            totalJumlah.val(curTotal_Jumlah.toFixed(2));
        }

        function NumDefault(theVal) {

            return setDefault(theVal, 0)
        }

        async function setValueToRow_HdrPermohonan(orderMDetail) {

            $('#lblNoSiri').val(orderMDetail.No_Wpr)
            $('#txtTarikh').val(orderMDetail.Tarikh_Mohon)
            $('#lblNoPtj').html(orderMDetail.Kod_PTJ)

            var newId = $('#ddlKumpulan')

            var ddlKumpulan = $('#ddlKumpulan')
            //var ddlSearch = $('#ddlKumpulan')
            //var ddlText = $('#ddlKumpulan')
            var selectObj_Kumpulan = $('#ddlKumpulan')
            $(ddlKumpulan).dropdown('set selected', orderMDetail.Kod_Kump_Wang);
            selectObj_Kumpulan.append("<option value = '" + orderMDetail.Kod_Kump_Wang + "'>" + orderMDetail.ButiranKumpulan + "</option>")

            //status - permohonan & kemaskini         

            if (orderMDetail.Kod_Status_Dok == "01" || orderMDetail.Kod_Status_Dok == "02") {

                $('.btnSimpan').hide();
                $('.btnHantar').hide();
                $('.btnAddRow').hide();
            }
            else {

                $('.btnSimpan').show();
                $('.btnHantar').show();
                $('.btnAddRow').show();
            }
        }

        async function ajaxSubmitOrder(order) {

            return new Promise((resolve, reject) => {
                $.ajax({
                    url: 'Panjar_WS.asmx/SubmitOrders',
                    method: 'POST',
                    data: JSON.stringify(order),
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

        async function ajaxSaveOrder(orderM) {

            return new Promise((resolve, reject) => {
                $.ajax({
                    url: 'Panjar_WS.asmx/SaveOrder',
                    method: 'POST',
                    data: JSON.stringify(orderM),
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

        async function setValueToRow_Permohonan(row, orderMDetail) {

            var ddl = $(row).find("td > .coa-list");
            var selectObj = $(row).find("td > .coa-list > select");
            $(ddl).dropdown('set selected', orderMDetail.Kod_Vot);
            selectObj.append("<option value = '" + orderMDetail.Kod_Vot + "'>" + orderMDetail.Kod_Vot + ' - ' + orderMDetail.Butiran + "</option>")


            var butir = $(row).find("td > .label-butir");
            butir.val(orderMDetail.Butiran_Belanja);

            var noresit = $(row).find("td > .label-resit");
            noresit.val(orderMDetail.No_Resit);

            var colptj = $(row).find("td > .label-ptj-list");
            colptj.html(orderMDetail.ButiranPTJ)

            var colhidptj = $(row).find("td > .Hid-ptj-list");
            colhidptj.html(orderMDetail.Kod_PTJ);

            var colKw = $(row).find("td > .label-kump-wang");
            colKw.html(orderMDetail.ButiranKw);

            var colhidKw = $(row).find("td > .Hid-kump-wang");
            colhidKw.html(orderMDetail.Kod_Kump_Wang);

            var colKo = $(row).find("td > .label-operasi");
            colKo.html(orderMDetail.ButiranKo);

            var colhidKo = $(row).find("td > .Hid-operasi");
            colhidKo.html(orderMDetail.Kod_Operasi);

            var colKp = $(row).find("td > .label-projek");
            colKp.html(orderMDetail.ButiranKp);

            var colhidKp = $(row).find("td > .Hid-projek");
            colhidKp.html(orderMDetail.Kod_Projek);

            var jumlah = $(row).find("td > .Jumlah");
            jumlah.val(orderMDetail.Jumlah_Butiran);

            var baki = $(row).find("td > .Baki");
            baki.val(orderMDetail.Baki);

            balance = baki.val();
            $("#lblBakiSemasa").val(orderMDetail.Baki);
            //console.log(balance.val());

            await calculateGrandTotal();

        }

        async function rowHoverClick(id) {
            $('#kemaskini').modal('hide');
            if (id !== "") {
                var recordHdr = await AjaxGetRecordHdrPermohonan(id);
                await AddRowHeader(null, recordHdr);

                var record = await AjaxGetRecordPermohonan(id);
                await clearAllRows();
                await AddRow(null, record)

                GetPejabat($('#lblNoPtj').text());
            }
        }

        async function AddRowHeader(totalClone, objOrder) {
            var counter = 1;
            if (objOrder !== null && objOrder !== undefined) {
                totalClone = objOrder.Payload.length;
            }


            if (counter <= objOrder.Payload.length) {
                await setValueToRow_HdrPermohonan(objOrder.Payload[counter - 1]);
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

        async function AjaxGetRecordHdrPermohonan(id) {

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

        async function AjaxGetAgih_Baki(ptj, kumpulan) {

            $.ajax({
                type: "POST",
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                url: "Panjar_WS.asmx/LoadBaki_Pengagihan",
                data: JSON.stringify({ ptj: ptj, kumpulan: kumpulan }),
                success: function (data) {
                    var jsonData = JSON.parse(data.d);
                    $('.Baki').eq(1).val(parseFloat(jsonData[0].Baki).toFixed(2));
                    balance = parseFloat(jsonData[0].Baki).toFixed(2);
                },
                error: function (error) {
                    console.log("Error: " + error);
                }
            });

            await calculateGrandTotal();
        }
    </script>
</asp:Content>
