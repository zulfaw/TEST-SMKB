<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Laporan_Transaksi.aspx.vb" Inherits="SMKB_Web_Portal.Laporan_Transaksi_GL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <contenttemplate>
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <div id="divpendaftaraninv" runat="server" visible="true">

                <div>

                    <hr>
                        <!-- Create the dropdown filter -->
                    <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="inputEmail3" class="col-sm-2 col-form-label">Tahun</label>
                                <div class="col-sm-8">
                                    <div class="input-group">
                                        <select id="idTahun" class="custom-select">
                                            <option value="2023">2023</option>
                                          

                                        </select>
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table class="table table-bordered" id="tblDataSenarai" style="width: 100%;">
                                        <thead>
                                            <tr style="width: 100%; text-align: center">
                                                <th scope="col" style="width: 10%">Tahun Transaksi</th>
                                                <th scope="col" style="width: 10%">Vot</th>
                                                <th scope="col" style="width: 10%">Kod PTJ</th>
                                                <th scope="col" style="width: 10%">Kumpulan Wang</th>
                                                <th scope="col" style="width: 10%">Kod Operasi</th>
                                                <th scope="col" style="width: 10%">Kod Projek</th>
                                                <%--<th scope="col" style="width: 20%">Perkara</th>--%>
                                                <th scope="col" style="width: 10%">Debit (RM)</th>
                                                <th scope="col" style="width: 10%">Kredit (RM)</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableID_Senarai">
                                            <tr style="display: none; width: 100%">
                                                <td style="text-align:center">
                                                    <label id="lblTkh" name="lblTkh" class="lblTkh"></label>
                                                </td>
                                                 <td>
                                                    <label id="lblVot" name="lblVot" class="lblVot"></label>
                                                </td>
                                                <td>
                                                    <label id="lblPTJ" name="lblPTJ" class="lblPTJ"></label>
                                                </td>
                                               
                                                <td>
                                                    <label id="lblKw" name="lblKw" class="lblKw"></label>
                                                </td>
                                                <td>
                                                    <label id="lblKo" name="lblKo" class="lblKo"></label>
                                                </td>
                                                <td>
                                                    <label id="lblKp" name="lblKp" class="lblKp"></label>
                                                </td>
                                               
                                                <td style="text-align:right">
                                                     <label id="Debit" name="Debit" class="Debit"></label>
                                                </td>
                                                <td style="text-align:right">
                                                     <label id="Kredit" name="Kredit" class="Kredit"></label>
                                                </td>
                                               
                                            </tr>

                                        </tbody>
                                        <tfoot>
                                                <tr style="text-align:right">
                                                <td colspan="6"></td>
                                                <td>
                                                    <label id="totalDbt" name="totalDbt" class="totalDbt"></label>
                                               </td>
                                                <td style="text-align:right">
                                                     <label id="totalKt" name="totalKt" class="totalKt"></label>
                                              </td>
                                              
                                            </tr>
                                        </tfoot>
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
        function ShowPopup(elm) {

            //alert("test");
            if (elm == "1") {

                $('#Senarai').modal('toggle');


            }
            else if (elm == "2") {

                $(".modal-body div").val("");
                $('#Senarai').modal('toggle');

            }
        }
        //$(document).ready(function () {
        //    $("#tblDataSenarai").DataTable({
        //        "responsive": true,
        //        "sPaginationType": "full_numbers",
        //        "oLanguage": {
        //            "oPaginate": {
        //                "sNext": '<i class="fa fa-forward"></i>',
        //                "sPrevious": '<i class="fa fa-backward"></i>',
        //                "sFirst": '<i class="fa fa-step-backward"></i>',
        //                "sLast": '<i class="fa fa-step-forward"></i>'
        //            },
        //            "sLengthMenu": "Tunjuk _MENU_ rekod",
        //            "sZeroRecords": "Tiada rekod yang sepadan ditemui",
        //            "sInfo": "Menunjukkan _END_ dari _TOTAL_ rekod",
        //            "sInfoEmpty": "Menunjukkan 0 ke 0 dari rekod",
        //            "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
        //            "sEmptyTable": "Tiada rekod."
        //        }
        //    });

        //});

        var searchQuery = "";
        var oldSearchQuery = "";
        var curNumObject = 0;
        var tableID = "#tblData";
        var shouldPop = true;
        var totalID = "#total";
        var totalCukai = "#TotalTax";
        var totalDiskaun = "#TotalDiskaun";
        var totalwoCukai = "#totalwoCukai";
        var tableID_Senarai = "#tblDataSenarai";

        var totalDebit = "#totalDbt";
        var totalKredit = "#totalKt";

        



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

            loadKelulusanRecords()

            $('#ddlJenTransaksi').dropdown({
                fullTextSearch: false,
                apiSettings: {
                    url: 'Transaksi_WS.asmx/GetJenisTransaksi?q={query}',
                    method: 'POST',
                    dataType: "json",
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

        async function loadKelulusanRecords() {
            var record = await AjaxLoadOrderRecord_Senarai("");
            $('#lblNoJurnal').val("")
            await clearAllRows_senarai();
            await paparSenarai(null, record);
        }

        async function AjaxLoadOrderRecord_Senarai(id) {

            try {
                const response = await fetch('LejarPenghutangWS.asmx/LoadOrderRecord_LejarPenghutang', {
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

        async function clearAllRows_senarai() {
            $(tableID_Senarai + " > tbody > tr ").each(function (index, obj) {
                if (index > 0) {
                    obj.remove();
                }
            })
        }

        async function paparSenarai(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblDataSenarai');


             


            //alert("hai")
            if (objOrder !== null && objOrder !== undefined) {
                totalClone = objOrder.length;

            }
            console.log(objOrder)

            while (counter <= totalClone) {


                var row = $('#tblDataSenarai tbody>tr:first').clone();
                row.attr("style", "");
                var val = "";

                $('#tblDataSenarai tbody').append(row);

                if (objOrder !== null && objOrder !== undefined) {

                    if (counter <= objOrder.length) {
                        await setValueToRow(row, objOrder[counter - 1]);
                    }
                }

                counter += 1;
            }

       


        }



        async function setValueToRow(row, orderDetail) {

            var tkh = $(row).find("td > .lblTkh");
            var ptj = $(row).find("td > .lblPTJ");
            var vot = $(row).find("td > .lblVot");
            var kw = $(row).find("td > .lblKw");
            var ko = $(row).find("td > .lblKo");
            var kp = $(row).find("td > .lblKp");
            //var perkara = $(row).find("td > .lblPerkara");
            var debit = $(row).find("td > .Debit"); 
            var kredit = $(row).find("td > .Kredit"); 

            tkh.html(orderDetail.Tahun);
            ptj.html(orderDetail.Kod_PTJ);
            vot.html(orderDetail.Kod_Vot);
            kw.html(orderDetail.Kod_Kump_Wang);
            ko.html(orderDetail.Kod_Operasi);
            kp.html(orderDetail.Kod_Projek);   
            
            debit.html(orderDetail.Debit.toFixed(2));           

            kredit.html(orderDetail.Kredit.toFixed(2));

            await calculateGrandTotal();

            
        }



        $(tableID_Senarai).on('click', '.btnView', async function () {

            event.preventDefault();
            var curTR = $(this).closest("tr");
            var recordID = curTR.find("td > .lblNo");

            //var bool = true;
            var id = recordID.html();
            //alert("hai");
            //console.log(id)
            if (id !== "") {

                //BACA HEADER JURNAL
                var recordHdr = await AjaxGetRecordHdrJurnal(id);
                await AddRowHeader(null, recordHdr);

                //BACA DETAIL JURNAL
                var record = await AjaxGetRecordJurnal(id);
                await clearAllRows();
                await AddRow(null, record);
            }

            return false;
        })

        async function AjaxGetRecordHdrJurnal(id) {

            try {

                const response = await fetch('LejarPenghutangWS.asmx/LoadHdrInvois', {
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

        async function AddRowHeader(totalClone, objOrder) {
            var counter = 1;
            //var table = $('#tblDataSenarai');

            if (objOrder !== null && objOrder !== undefined) {
                totalClone = objOrder.Payload.length;
            }


            if (counter <= objOrder.Payload.length) {
                await setValueToRow_HdrJurnal(objOrder.Payload[counter - 1]);
            }
            // console.log(objOrder)
        }

        async function AjaxGetRecordJurnal(id) {

            try {

                const response = await fetch('LejarPenghutangWS.asmx/LoadRecordInvois', {
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

        async function AddRow(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblData');

            if (objOrder !== null && objOrder !== undefined) {
                //totalClone = objOrder.Payload.OrderDetails.length;
                totalClone = objOrder.Payload.length;
                console.log(totalClone)
                //if (totalClone < 5) {
                //    totalClone = 5;
                //}
            }

            while (counter <= totalClone) {
                curNumObject += 1;
                var newCarianVot = "ddlVotCarian" + curNumObject;

                var row = $('#tblData tbody>tr:first').clone();
                var votcarianlist = $(row).find(".vot-carian-list").attr("id", newCarianVot);

                row.attr("style", "");
                var val = "";

                $('#tblData tbody').append(row);


                await initCarianVot(newCarianVot)

                //$(newId).api("query");
                //(newVotID).api("query");
                //$(newKwID).api("query");
                //(newKOID).api("query");
                //$(newKPID).api("query");
                $(newCarianVot).api("query");

                if (objOrder !== null && objOrder !== undefined) {
                    //await setValueToRow(row, objOrder.Payload.OrderDetails[counter - 1]);
                    if (counter <= objOrder.Payload.length) {
                        await setValueToRow_Transaksi(row, objOrder.Payload[counter - 1]);

                    }
                }
                counter += 1;
            }
        }

        async function setValueToRow_Transaksi(row, orderDetail) {

            //console.log(orderDetail)
            var ddl = $(row).find("td > .vot-carian-list");
            var ddlSearch = $(row).find("td > .vot-carian-list > .search");
            var ddlText = $(row).find("td > .vot-carian-list > .text");
            var selectObj = $(row).find("td > .vot-carian-list > select");
            $(ddl).dropdown('set selected', orderDetail.Kod_Vot);
            selectObj.append("<option value = '" + orderDetail.Kod_Vot + "'>" + orderDetail.ButiranVot + "</option>")


            var butirptj = $(row).find("td > .label-ptj-list");
            butirptj.html(orderDetail.ButiranPTJ);

            var hidbutirptj = $(row).find("td > .Hid-ptj-list");
            hidbutirptj.html(orderDetail.colhidptj);

            var butirKW = $(row).find("td > .label-kw-list");
            butirKW.html(orderDetail.colKW);

            var hidbutirkw = $(row).find("td > .Hid-kw-list");
            hidbutirkw.html(orderDetail.colhidkw);

            var butirKo = $(row).find("td > .label-ko-list");
            butirKo.html(orderDetail.colKO);

            var hidbutirko = $(row).find("td > .Hid-ko-list");
            hidbutirko.html(orderDetail.colhidko);

            var butirKp = $(row).find("td > .label-kp-list");
            butirKp.html(orderDetail.colKp);

            var hidbutirkp = $(row).find("td > .Hid-kp-list");
            hidbutirkp.html(orderDetail.colhidkp);

            var details = $(row).find("td > .details");
            details.val(orderDetail.Perkara);

            var quantity = $(row).find("td > .quantity");
            quantity.val(orderDetail.Kuantiti);
            console.log(orderDetail)
            var cukai = $(row).find("td > .cukai");
            cukai.val(orderDetail.Cukai.toFixed(2));

            var kdr_hrga = $(row).find("td > .price");
            kdr_hrga.val(orderDetail.Kadar_Harga.toFixed(2));

            var diskaun = $(row).find("td > .diskaun");
            diskaun.val(orderDetail.Diskaun.toFixed(2));

            var amount = $(row).find("td > .amount");
            amount.val(orderDetail.Jumlah);

            //var quantity = $(curTR).find("td > .quantity");
            //var price = $(curTR).find("td > .price");
            //var amount = $(curTR).find("td > .amount");
            //var cukai = $(curTR).find("td > .cukai");
            var JUMcukai = $(row).find("td > .JUMcukai");
            //var diskaun = $(curTR).find("td > .diskaun");
            var JUMdiskaun = $(row).find("td > .JUMdiskaun");
            var amountwocukai = $(row).find("td > .amountwocukai");

            var totalPrice = NumDefault(quantity.val()) * NumDefault(kdr_hrga.val())
            var amauncukai = NumDefault(cukai.val()) / 100
            var total_cukai = totalPrice * amauncukai
            var amaundiskaun = NumDefault(diskaun.val()) / 100
            var total_diskaun = totalPrice * amaundiskaun
            var amountxcukai = totalPrice - total_diskaun
            //alert(amaundiskaun)

            totalPrice = totalPrice + total_cukai - total_diskaun
            amount.val(totalPrice.toFixed(2));
            JUMcukai.val(total_cukai.toFixed(2));
            JUMdiskaun.val(total_diskaun.toFixed(2));
            amountwocukai.val(amountxcukai.toFixed(2));
            calculateGrandTotal();

        }

        async function setValueToRow_HdrJurnal(orderDetail) {

            $('#txtnoinv').val(orderDetail.No_Bil)
            //$('#txtNoRujukan').val(orderDetail.No_Rujukan)
            //$('#ddlPenghutang').val(orderDetail.Nama_Penghutang)

            $('#tkhMula').val(orderDetail.Tkh_Mula)
            $('#tkhTamat').val(orderDetail.Tkh_Tamat)
            //$('#rdKontrak').val(orderDetail.Kontrak)
            //$('#ddlUrusniaga').val(orderDetail.JenisUrusniaga)
            $('#txtTujuan').val(orderDetail.Tujuan)

            var newId = $('#ddlJenTransaksi')

            //await initDropdownPtj(newId)
            //$(newId).api("query");

            var ddlPenghutang = $('#ddlPenghutang')
            var ddlSearchP = $('#ddlPenghutang')
            var ddlTextP = $('#ddlPenghutang')
            var selectObj_JenisTransaksiP = $('#ddlPenghutang')
            $(ddlPenghutang).dropdown('set selected', orderDetail.Kod_Pelanggan);
            selectObj_JenisTransaksiP.append("<option value = '" + orderDetail.Kod_Pelanggan + "'>" + orderDetail.Nama_Penghutang + "</option>")

            var ddlUrusniaga = $('#ddlUrusniaga')
            var ddlSearch = $('#ddlUrusniaga')
            var ddlText = $('#ddlUrusniaga')
            var selectObj_JenisTransaksi = $('#ddlUrusniaga')
            $(ddlUrusniaga).dropdown('set selected', orderDetail.Jenis_Urusniaga);
            selectObj_JenisTransaksi.append("<option value = '" + orderDetail.Jenis_Urusniaga + "'>" + orderDetail.Butiran + "</option>")
        }

        async function clearAllRows() {
            $(tableID + " > tbody > tr ").each(function (index, obj) {
                if (index > 0) {
                    obj.remove();
                }
            })
            $(totalID).val("0.00");

        }

        async function initCarianVot(id) {
            $('#' + id).dropdown({
                fullTextSearch: true,
                apiSettings: {
                    url: 'LejarPenghutangWS.asmx/GetCarianVotList?q={query}',
                    method: 'POST',
                    dataType: "json",
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
                        //$(obj).dropdown('set selected', $(this).find('.menu .item').first().data('value'));
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
        }

        function NumDefault(theVal) {
            return setDefault(theVal, 0)
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

      async function calculateGrandTotal() {

                //debit
                var grandTotal_Dt = $(totalDebit);

                var curTotal_Dt = 0;

                $('.Debit').each(function (index, obj) {
                    curTotal_Dt += parseFloat(NumDefault($(obj).html()));
                });

                //grandTotal_Dt.html(curTotal_Dt.toFixed(2));

           grandTotal_Dt.html(curTotal_Dt.toLocaleString('en-US', {maximumFractionDigits:2}))

                //kredit
                var grandTotal_Kt = $(totalKredit);
                var curTotal_Kt = 0;

                $('.Kredit').each(function (index, obj) {
                    curTotal_Kt += parseFloat(NumDefault($(obj).html()));
                });

                //grandTotal_Kt.html(curTotal_Kt.toFixed(2));

                grandTotal_Kt.html(curTotal_Kt.toLocaleString('en-US', {maximumFractionDigits:2}))
              

            }

        $('.btnLulus').click(async function () {
            var jumRecord = 0;
            var acceptedRecord = 0;
            var msg = "";
            var id = $('#txtnoinv').val();
            
            msg = "Anda pasti ingin meluluskan rekod ini?"

            if (!confirm(msg)) {
                return false;
            }
            //console.log(newOrder)
            var result = JSON.parse(await ajaxSaveOrder(id));
            alert(result.Message);
            //$('#orderid').val(result.Payload.OrderID)
            //loadExistingRecords();
            await clearAllRows();
            //AddRow(5);

        });

        async function ajaxSaveOrder(id) {

            return new Promise((resolve, reject) => {
                $.ajax({

                    url: 'InvoisWS.asmx/SaveLulus',
                    method: 'POST',
                    data: JSON.stringify(id),
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
            //console.log("tst")
        }

        $('#ddlPenghutang').dropdown({
            selectOnKeydown: true,
            fullTextSearch: true,
            apiSettings: {
                url: 'LejarPenghutangWS.asmx/GetPenghutangList?q={query}',
                method: 'POST',
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                cache: false,
                beforeSend: function (settings) {
                    // Replace {query} placeholder in data with user-entered search term
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
                        $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.text));
                    });

                    //if (searchQuery !== oldSearchQuery) {
                    //$(obj).dropdown('set selected', $(this).find('.menu .item').first().data('value'));
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
    </script>
</asp:Content>
