<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PenutupanAkaun.aspx.vb" Inherits="SMKB_Web_Portal.PenutupanAkaun" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <hr />
    <div id="PenutupanAkaun" class="tabcontent" style="display: block">    
        <div class="col-md-8" style="max-width:80%;margin-left:10%">
            <div class="d-flex justify-content-around">
                <label for="inputMonth" class="p-2">Bulan</label>
                <select class="ui search dropdown" name="DDMonthEndProcess" id="DDMonthEndProcess"></select>
                <label for="inputYear" class="p-2">Tahun</label>
                <select class="ui search dropdown" name="DDYearMonthEndProcess" id="DDYearMonthEndProcess"></select>
            </div>
        </div>
        <div class="form-row justify-content-center" style="margin-top:5rem">
            <button type="button" class="btn btn-success">
                Proses
            </button>
        </div>
    </div>

    <%--modal popup confirmation--%>
    <div class="modal fade" id="MonthlyEndProcessing" tabindex="-1" role="dialog" aria-labelledby="MonthlyEndProcessingLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="MonthlyEndProcessingLabel">Pengesahan</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Anda pasti ingin menyimpan rekod ini?
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                    <button type="button" class="btn btn-secondary btnHantar" data-toggle="tooltip" data-placement="bottom">Ya</button>
                </div>
            </div>
        </div>
    </div>

    <%--modal popup information--%>
    <div class="modal fade" id="InformBox" tabindex="-1" role="dialog" aria-labelledby="InformBox" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="InformBoxLabel">Makluman</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Sila masukkan bulan dan tahun
                </div>
                <div id="result"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Tutup</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Include SweetAlert2 JS -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.20/dist/sweetalert2.all.min.js"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11.0.20/dist/sweetalert2.min.css">

    <script>
        $(document).ready(function () {
            $('#DDMonthEndProcess').dropdown({
                fullTextSearch: false,
                apiSettings: {
                    url: '../PenutupanAkaunWS.asmx/GetMonthEndProcess?q={query}',
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

                        // Refresh dropdown
                        $(obj).dropdown('refresh');

                        if (shouldPop === true) {
                            $(obj).dropdown('show');
                        }
                    }
                }
            });

            $('#DDYearMonthEndProcess').dropdown({
                fullTextSearch: false,
                apiSettings: {
                    url: '../PenutupanAkaunWS.asmx/GetYearMonthEndProcess?q={query}',
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

                        // Refresh dropdown
                        $(obj).dropdown('refresh');

                        if (shouldPop === true) {
                            $(obj).dropdown('show');
                        }
                    }
                }
            });

        });
        //buat proses checking bulan lepas

        //buat penutupan akaun masuk ke lejar am
        $('.btnHantar').click(function () {
            var Month = $('#DDMonthEndProcess').val();
            var Year = $('#DDYearMonthEndProcess').val();

            //close modal confirmation
            $('#MonthlyEndProcessing').modal('hide');

            //MEP(Month);
            Swal.fire({
                allowOutsideClick: false,
                title: 'Processing...',
                //html: 'I will close in <b></b> milliseconds.',
                timer: 2000,
                didOpen: () => {
                    Swal.showLoading()
                    const b = Swal.getHtmlContainer().querySelector('b')
                    timerInterval = setInterval(() => {
                        b.textContent = Swal.getTimerLeft()
                    }, 100)
                },
                willClose: () => {
                    clearInterval(timerInterval)
                }
            })

            //var query = "SELECT SUM(DEBIT),SUM(CREDIT) FROM SMKB_Jurnal_Hdr WHERE MONTH(Tkh_Transaksi) = " + Month;
            //var result = JSON.parse(await ajaxSubmitMonthlyEndProcessing(Month));
        });

        $('.btn-success').click(function () {
            var Month = $('#DDMonthEndProcess').val();
            var Year = $('#DDYearMonthEndProcess').val();

            if (checkflagNull(Month, Year) == 1) {
                $('#MonthlyEndProcessing').modal('show');
            } else {
                $('#InformBox').modal('show');
            }
        });

        function MEP(Month) {
            if (Month !== '') {
                $.ajax({
                    type: 'POST',
                    url: '../PenutupanAkaunWS.asmx/SubmitOrders',
                    data: '{ Month: "' + Month + '" }',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (data) {
                        $('#result').text(data.d);
                            alert(data.d);
                        },
                    error: function (error) {
                        console.log(error);
                        }
                    });
                }
        }

        //check if the dropdown is null
        function checkflagNull(Month, Year) {
            if (Month == null || Year == null) {
                return 0;
            } else {
                return 1;
            }
        }

            //async function ajaxSubmitMonthlyEndProcessing(Month) {
            //    return new Promise((resolve, reject) => {
            //        $.ajax({
            //            url: '../PenutupanAkaunWS/SubmitOrders',
            //            method: 'POST',
            //            data: JSON.stringify(Month),
            //            dataType: 'json',
            //            contentType: 'application/json; charset=utf-8',
            //            success: (response) => {
            //                resolve(response); // Resolve the Promise with the response data
            //            },
            //            error: (xhr, textStatus, errorThrown) => {
            //                reject(new Error(textStatus)); // Reject the Promise with the error object
            //            }
            //        });
            //    });
            //}

            //tunjuk interface
    </script>
</asp:Content>
