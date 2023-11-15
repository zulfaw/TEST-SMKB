<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Pengagihan.aspx.vb" Inherits="SMKB_Web_Portal.Pengagihan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <style>
        .custom-select {
            /*-ms-box-sizing: content-box;*/
            white-space: nowrap;
            -webkit-appearance: none;
            -moz-appearance: none;
        }
    </style>

    <div id="PengagihanTab" class="tabcontent" style="display: block">

        <%--Modal for popup pengesahan--%>
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

        <%--Modal for popup makluman--%>
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

        <!-- Create the dropdown filter -->
        <div class="search-filter">
            <div class="form-row justify-content-center">
                <div class="form-group row col-md-6">
                    <label for="inputEmail3" class="col-sm-4 col-form-label" style="text-align: right">Pejabat/Fakulti :</label>
                    <div class="col-sm-8">
                        <div class="input-group">
                            <select id="categoryFilter" class="search custom-select"></select>

                            <div class="input-group-append">
                                <button id="btnSearch" runat="server" class="btn btn-outline btnSearch" type="button">
                                    <i class="fa fa-search"></i>
                                    Cari
                                </button>
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>

        <div class="form-row">
            <div class="col-md-12">
                <div class="distributon-table table-responsive">
                    <table class="table table-bordered" id="tblAgihan" style="width: 95%">
                        <thead>
                            <tr>
                                <th scope="col" style="width:40%">KUMPULAN</th>
                                <th scope="col">JUMLAH AGIH(RM)</th>
                                <th scope="col">BAKI(RM)</th>
                            </tr>
                        </thead>

                        <tbody id="tableID">
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="1"></td>
                                <td>
                                    <input class="form-control underline-input" id="totalAgih" name="totalAgih"
                                        style="text-align: right; font-weight: bold" width="10%" readonly /></td>
                                <td>
                                    <input class="form-control underline-input" id="totalBaki" name="totalBaki"
                                        style="text-align: right; font-weight: bold" width="10%" readonly /></td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
        <br />
        <div class="form-row">
            <div class="form-group col-md-12" align="right">
                <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Draft">Simpan</button>
            </div>
        </div>
    </div>

    <script>

        var tbl = null;
        var validate = function (e) {
            var t = e.value;
            e.value = (t.indexOf(".") >= 0) ? (t.substr(0, t.indexOf(".")) + t.substr(t.indexOf("."), 3)) : t;
        }
        var searchQuery = "";
        var oldSearchQuery = "";
        var shouldPop = true;

        $(document).ready(async function () {
            tbl = $('#tblAgihan').DataTable({
                responsive: true,
                searching: false,
                language: {
                    paginate: {
                        next: '<i class="fa fa-forward"></i>',
                        previous: '<i class="fa fa-backward"></i>',
                        first: '<i class="fa fa-step-backward"></i>',
                        last: '<i class="fa fa-step-forward"></i>'
                    },
                    lengthMenu: "Tunjuk _MENU_ rekod",
                    zeroRecords: "Tiada rekod ditemui",
                    info: "Menunjukkan _END_ dari _TOTAL_ rekod",
                    infoEmpty: "Menunjukan 0 ke 0 dari rekod"
                },
                ajax: {
                    url: "Panjar_WS.asmx/LoadRecord_Pengagihan",
                    method: 'POST',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    dataSrc: function (json) {
                        return JSON.parse(json.d);
                    },
                    data: function () {
                        //Filter date bermula dari sini - 20 julai 2023
                        var ptj = $('#categoryFilter').val();
                        return JSON.stringify({
                            ptj: ptj,
                        })
                        //akhir sini
                    }
                },
                columns: [
                    //{ "data": "Pejabat" },
                    { "data": "Butiran" },
                    {
                        "data": "Jumlah_Agih",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {

                                return data;

                            }

                            var link = `<td>
                                                <input type="number" id="jumAgih" name="jumAgih" style="text-align:right" class="form-control Agih"
                                                value="${parseFloat(data).toFixed(2)}" oninput="validate(this)"/>
                                                <input type ="hidden" class = "hidAgih" value="${parseFloat(data).toFixed(2)}"/>
                                            </td>`;
                            return link;
                        }
                    },
                    {
                        "data": "Baki",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {

                                return data;

                            }

                            var link = `<td>
                                                <input type="number" id="baki" name="baki" style="text-align:right" class="form-control Baki" value="${parseFloat(data).toFixed(2)}" readonly/>
                                                <input type ="hidden" class = "hidBaki" value="${parseFloat(data).toFixed(2)}"/>
                                            </td>`;
                            return link;
                        }
                    }
                ]
            });
            calculateGrandTotal();

            $('#categoryFilter').dropdown({
                fullTextSearch: false,
                apiSettings: {
                    url: 'Panjar_WS.asmx/GetPTJAgih?q={query}',
                    method: 'POST',
                    dataType: "JSON",
                    contentType: 'application/json; charset=utf-8',
                    cache: false,
                    beforeSend: function (settings) {
                        // Replace {query} placeholder in data with user-entered search term                       

                        settings.data = JSON.stringify({ q: settings.urlData.query });
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

        var tableID = '#tableID';
        var agih = 0;
        var baki = 0;
        var kump = null;
        var ptj = null;

        $('.btnSearch').click(async function () {
            // show_loader();
            //isClicked = true;
            tbl.ajax.reload();
            //close_loader();
        })

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

        function setDefault(theVal, defVal) {

            if (defVal === null || defVal === undefined) {
                defVal = "";
            }

            if (theVal === "" || theVal === undefined || theVal === null) {
                theVal = defVal;
            }
            return theVal;
        }

        function NumDefault(theVal) {

            return setDefault(theVal, 0)
        }

        function showMessageBox(title, message, type) {
            $('#resultModalMessage').html(message);
            if (type === "success")
                $('#resultModal').removeClass("modal-error").addClass("modal-success");
            else if (type === "error")
                $('#resultModal').removeClass("modal-success").addClass("modal-error");

            $('#resultModal').modal("show");
        }

        $('.btnSimpan').off('click').on('click', async function () {

            var msg = "";
            msg = "Anda pasti untuk menyimpan rekod ini?";

            //Popup modal pengesahan
            $('#confirmMessage').html(msg);
            $('#modalConfirm').modal('show');

            $('#confirmButton').off('click').on('click', async function () {
                $('#modalConfirm').modal('hide');

                var result = JSON.parse(await AjaxSaveOrderPengagihan(ptj, kump, agih));

                //Makluman
                if (result.Status === "success")
                    showMessageBox("Success", result.Message, "success");
                else
                    showMessageBox("Error", result.Message, "error");

                tbl.ajax.reload();
                calculateGrandTotal();
            });
        });

        $(tableID).on('keyup', '.Agih, .Baki', '.hidKump', async function () {

            var curTR = $(this).closest("tr");

            var agih_ = $(curTR).find("td > .Agih");
            var totalA = NumDefault(agih_.val());

            kump = tbl.row(curTR).data().Kod_Kump_Wang;
            console.log(kump);

            ptj = tbl.row(curTR).data().Kod_PTJ;
            console.log(ptj);

            var baki_ = $(curTR).find("td > .Baki");
            var totalB = NumDefault(baki_.val());


            agih = parseFloat(totalA);
            baki = parseFloat(totalB);

            calculateGrandTotal();
        });

        async function calculateGrandTotal() {

            //Jumlah Baki
            var totalBaki = $('#totalBaki');
            var curTotal_Baki = 0;

            $('.Baki').each(function (index, obj) {
                curTotal_Baki += parseFloat(NumDefault($(obj).val()));
            });
            totalBaki.val(curTotal_Baki.toFixed(2));

            //Jumlah Agihan
            var totalAgih = $('#totalAgih');
            var curTotal_Agih = 0;

            $('.Agih').each(function (index, obj) {
                curTotal_Agih += parseFloat(NumDefault($(obj).val()));
            });
            totalAgih.val(curTotal_Agih.toFixed(2));
        }

        async function AjaxSaveOrderPengagihan(ptj, kump, agih) {

            return new Promise((resolve, reject) => {
                $.ajax({
                    url: 'Panjar_WS.asmx/SaveOrder_Pengagihan',
                    method: 'POST',
                    data: JSON.stringify({ ptj: ptj, kumpulan: kump, agih: agih }),
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

</asp:Content>
