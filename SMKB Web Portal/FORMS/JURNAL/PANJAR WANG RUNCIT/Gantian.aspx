<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Gantian.aspx.vb" Inherits="SMKB_Web_Portal.Gantian" %>

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
    </style>

    <div id="GantianTab" class="tabcontent" style="display: block">

        <div class="modal-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <input type="text" id="lblPtj" name="lblPtj" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />
                            <label class="input-group__label" for="lblPtj">No. PTj</label>
                        </div>

                        <div class="form-group col-md-4">
                            <select id="ddlKump" name="ddlKump" class="input-group__select ui search dropdown Kumpulan" placeholder="&nbsp;">
                            </select>
                            <label class="input-group__label" for="ddlKump">Kumpulan</label>
                        </div>
                    </div>

                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="form-row">
                        <h6>Maklumat Agihan Panjar Wang Runcit</h6>
                    </div>
                    <hr />
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Agihan (RM)</label>
                        </div>
                        <div class="form-group col-md-6" align="right">
                            <input type="number" id="lblAgihan" name="lblAgihan" class="form-control"
                                style="display: inline; width: 50%; text-align: right" readonly />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Baki (RM)</label>
                        </div>
                        <div class="form-group col-md-6" align="right">
                            <input type="number" id="lblBaki" name="lblBaki" class="form-control Baki"
                                style="display: inline; width: 50%; text-align: right" readonly />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label>Baki Yang Sepatutnya (RM)</label>
                        </div>
                        <div class="form-group col-md-6" align="right">
                            <input type="number" id="lblBakiPatut" name="lblBakiPatut" class="form-control"
                                style="display: inline; width: 50%; text-align: right" readonly />
                        </div>
                    </div>
                </div>
            </div>
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

        <div class="form-row">
            <h6>Senarai Penambahan</h6>
            <br />
            <div class="col-md-12">
                <div class="gantianlist-table table-responsive">
                    <table id="tblSenarai" class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">Tarikh Mohon</th>
                                <th scope="col">No. WPR</th>
                                <th scope="col">Amaun</th>
                                <th scope="col">Status</th>
                            </tr>
                        </thead>
                        <tbody id="tableID"></tbody>
                    </table>
                </div>
                <%--<div class="pwrlist-table table-responsive">
                    <table class="table table-bordered" id="tblGantian">
                        <thead>
                            <tr>
                                <th scope="col">Gantian</th>
                                <th scope="col">Butir Perbelanjaan</th>
                                <th scope="col">No. Resit</th>
                                <th scope="col">COA</th>
                                <th scope="col">PTj</th>
                                <th scope="col">Amaun (RM)</th>
                            </tr>
                        </thead>

                        <tbody id="tableID">
                            <tr style="display: none; width: 100%" class="table-list">
                                <td>
                                    <label id="statGanti" name="statGanti" class="ganti"></label>
                                </td>
                                <td>
                                    <label id="butirBelanja" name="butirBelanja" class="butiran-belanja"></label>
                                </td>
                                <td>
                                    <label id="noResit" name="noResit" class="resit"></label>
                                </td>
                                <td>
                                    <select class="ui search dropdown coa-list" id="ddlCoa" name="ddlCoa" style="width: 100%"></select>
                                    <input type="hidden" class="data-id" value="" />
                                    <label id="hidCoa" name="hidCoa" class="Hid-coa-list" style="visibility: hidden"></label>
                                </td>
                                <td>
                                    <label id="kodPtj" name="kodPtj" class="kod-ptj"></label>
                                </td>
                                <td>
                                    <label id="amaun" name="amaun" class="amaun"></label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>--%>
            </div>
        </div>

        <%--<div class="form-row">
            <div class="form-group col-md-12" align="right">
                <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Draft">Simpan</button>
            </div>
        </div>
        <br />--%>

        <%--<div class="form-row">
            <div class="form-group col-md-6">
                <label>Penambahan</label>
            </div>
            <div class="form-group col-md-6" align="right">
                <input type="number" id="lblPenambahan" name="lblPenambahan" class="form-control"
                    style="display: inline; width: 50%; text-align: right" value=".00" readonly />
            </div>
        </div>
        <div class="form-row">
            <div class="form-group col-md-6">
                <label>Jumlah Panjar Wang Runcit Selepas Ditambah</label>
            </div>
            <div class="form-group col-md-6" align="right">
                <input type="number" id="lblJumLTambah" name="lblJumLTambah" class="form-control"
                    style="display: inline; width: 50%; text-align: right" value="500.00" readonly />
            </div>
        </div>--%>
    </div>

    <script>

        var ptj = <%= Session("ssusrKodPTj")%>;
        $('#lblPtj').html(ptj);
        //var noptj = '020000';
        var tbl = null;
        var isClicked = false;

        $('#ddlKump').on('change', async function (e) {
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            //console.log(valueSelected);
            await loadTableGantian(valueSelected);

        })

        async function loadTableGantian(kump) {

            tbl = $('#tblSenarai').DataTable({
                responsive: true,
                searching: true,
                language: {
                    paginate: {
                        next: '<i class="fa fa-forward"></i>',
                        previous: '<i class="fa fa-backward"></i>'
                    },
                    lengthMenu: "Tunjuk _MENU_ rekod",
                    zeroRecords: "Tiada rekod ditemui",
                    info: "Menunjukkan _END_ dari _TOTAL_ rekod",
                    infoEmpty: "Menunjukan 0 ke 0 dari rekod"
                },
                ajax: {
                    url: "Panjar_WS.asmx/LoadOrder_Gantian",
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
                            tkhTamat: endDate,
                            ptj: ptj,
                            kump: kump
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
                    { "data": "No_Wpr" },
                    { "data": "Jumlah_Belanja" },
                    {
                        "data": "Status_Dok",
                        render: function (data, type, row, meta) {

                            var link
                            if (data = '04')
                                data = "GAGAL KELULUSAN";

                            link = `<td style="width: 10%" >
                                                <p id="lblStatus" name="lblStatus" class="lblStatus" value="${data}" style="color:red;" >${data}</p>                                              
                                            </td>`;

                            return link;
                        }
                    }
                ]
            })
        }

        //$(document).ready(function () {
        //    $('#tblgantian').datatable({
        //        responsive: true,
        //        searching: false,
        //        language: {
        //            paginate: {
        //                next: '<i class="fa fa-forward"></i>',
        //                previous: '<i class="fa fa-backward"></i>'
        //            },
        //            lengthmenu: "tunjuk _menu_ rekod",
        //            zerorecords: "tiada rekod ditemui",
        //            info: "menunjukkan _end_ dari _total_ rekod",
        //            infoempty: "menunjukan 0 ke 0 dari rekod"
        //        },
        //    });

        //});

        var curNumObject = 0;
        var shouldPop = true;

        $(document).ready(function () {
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

            $('#ddlKump').dropdown({
                fullTextSearch: false,
                apiSettings: {
                    url: 'Panjar_WS.asmx/GetKump?q={query}&kodptj={kodptj}',
                    method: 'POST',
                    dataType: "JSON",
                    contentType: 'application/json; charset=utf-8',
                    cache: false,
                    beforeSend: function (settings) {
                        // Replace {query} placeholder in data with user-entered search term

                        settings.urlData.kodptj = ptj
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

        //async function getAjax(noKump) {
        //    var record = await AjaxGetGantian(noptj, noKump);
        //    await AddRowHeader(null, record);
        //    await AddRow(null, record);
        //}

        $('.btnSearch').click(async function () {
            // show_loader();
            isClicked = true;
            tbl.ajax.reload();
            //close_loader();
        })

        async function setValueToGantian(order) {
            $('.Baki').val(parseFloat(order.Baki_Peruntukan).toFixed(2));
        }

        async function AddRowHeader(totalClone, objOrder) {
            var counter = 1;
            if (objOrder !== null && objOrder !== undefined) {
                totalClone = objOrder.Payload.length;
            }


            if (counter <= objOrder.Payload.length) {
                await setValueToGantian(objOrder.Payload[counter - 1]);
            }

        }

        async function setValueToGantianTable(row, order) {
            var ddl = $(row).find("td > .coa-list");
            var ddlSearch = $(row).find("td > .coa-list > .search");
            var ddlText = $(row).find("td > .coa-list > .text");
            var selectObj = $(row).find("td > .coa-list > select");
            $(ddl).dropdown('set selected', order.Kod_Vot);
            selectObj.append("<option value = '" + order.Kod_Vot + "'>" + order.Butiran + "</option>")

            var belanja = $(row).find("td > .butiran-belanja");
            belanja.html(order.Butiran_Belanja);

            var resit = $(row).find("td > .resit");
            resit.html(order.No_Resit);

            var kodPtj = $(row).find("td > .kod-ptj");
            kodPtj.html(order.Kod_PTJ);

            var amaun = $(row).find("td > .amaun");
            amaun.html(parseFloat(order.Amaun).toFixed(2));

        }

        async function AddRow(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblGantian');

            if (objOrder !== null && objOrder !== undefined) {
                totalClone = objOrder.Payload.length;
            }

            //console.log("aa" + objOrder)
            while (counter <= totalClone) {
                curNumObject += 1;

                var newId_coa = "ddlCoa" + curNumObject;
                var row = $('#tblGantian tbody>tr:first').clone();
                var dropdown5 = $(row).find(".coa-list").attr("id", newId_coa);

                var val = ""

                row.attr("style", "");
                $('#tblGantian tbody').append(row);

                //await initDropdownCOA(newId_coa)

                //$(newId_coa).api("query");

                if (objOrder !== null && objOrder !== undefined) {
                    //await setValueToRow(row, objOrder.Payload.OrderDetails[counter - 1]);
                    if (counter <= objOrder.Payload.length) {
                        await setValueToGantianTable(row, objOrder.Payload[counter - 1]);
                    }
                }
                counter += 1;
            }
        }

        async function AjaxGetGantian(ptj, kump) {

            try {

                const response = await fetch('Panjar_WS.asmx/LoadGantian', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ ptj: ptj, kump: kump })
                });
                const data = await response.json();
                return JSON.parse(data.d);
            } catch (error) {
                console.error('Error:', error);
                return false;
            }
        }
    </script>

</asp:Content>
