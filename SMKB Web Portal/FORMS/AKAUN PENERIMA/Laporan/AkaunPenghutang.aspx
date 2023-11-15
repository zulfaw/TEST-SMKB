<%@ Page Title="Laporan | Akaun Penghutang" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="AkaunPenghutang.aspx.vb" Inherits="SMKB_Web_Portal.AkaunPenghutang" %>

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
                            <h5 class="modal-title" id="exampleModalCenterTitle">Laporan Akaun Penghutang</h5>
                        </div>
            
                        <!-- Create the dropdown filter -->
                        <div class="search-filter">
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-8">
                                    <label for="tahun" class="col-sm-4 col-form-label" style="text-align: right">Carian :</label>
                                    <div class="col-sm-6">
                                        <select id="penghutangReportFilter" class="form-control">
                                            <option value="" selected hidden>-- Sila Pilih --</option>
                                            <option value="PAP">Penyata Akaun Penghutang</option>
                                            <option value="PLAP">Penyata Akaun Penghutang Terperinci</option>
                                            <option value="LPAT">Laporan Penyata Kewangan Penghutang</option>
                                        </select>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row justify-content-center mt-4" id="noPenghutangSection">
                                <div class="form-group row col-md-8">
                                    <label for="ptj" class="col-sm-4 col-form-label" style="text-align: right">No. Penghutang :</label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <%--<input type="text" id="noPenghutang" name="noPenghutang" autocomplete="off" placeholder="No. Penghutang / ID Penghutang" class="form-control"/>--%>
                                            <select id="ddlNoAkaunPenghutang" class="ui search dropdown" name="ddlNoAkaunPenghutang"></select>
                                        </div>
                                        <%--<div class="col-6">--%>
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        
                            <div class="form-row justify-content-center mt-4" id="monthSection">
                                <div class="form-group row col-md-8">
                                    <label for="ptj" class="col-sm-4 col-form-label" style="text-align: right">Bulan :</label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <select name="ddlMonth" id="ddlMonth" class="form-control">
                                                <option value="" selected hidden>-- Sila Pilih --</option>
                                                <option value="01">Januari</option>
                                                <option value="02">Februari</option>
                                                <option value="03">Mac</option>
                                                <option value="04">April</option>
                                                <option value="05">Mei</option>
                                                <option value="06">Jun</option>
                                                <option value="07">Julai</option>
                                                <option value="08">Ogos</option>
                                                <option value="09">September</option>
                                                <option value="10">Oktober</option>
                                                <option value="11">November</option>
                                                <option value="12">Disember</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="form-row justify-content-center mt-4" id="yearSection">
                                <div class="form-group row col-md-8">
                                    <label for="ptj" class="col-sm-4 col-form-label" style="text-align: right">Tahun :</label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <select name="ddlYear" id="ddlYear" class="form-control">
                                                <option value="" selected hidden>-- Sila Pilih --</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row justify-content-center mt-4" id="btnSearchSection">
                                <button id="btnSearch" class="btn btn-primary btnSearch" onclick="" type="button">
                                    Text
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        
    </contenttemplate>
    <script type="text/javascript">

        var shouldPop = true;

        $(document).ready(function () {
            // set #penghutangReportFilter to default value
            $('#penghutangReportFilter').val('');
            // trigger change event
            $('#penghutangReportFilter').trigger('change');

            // append #ddlYear options to the last 10 years
            var currentYear = new Date().getFullYear();
            for (var i = 0; i < 10; i++) {
                var year = currentYear - i;
                $('#ddlYear').append(`<option value="${year}">${year}</option>`);
            }

            // set #ddlYear to default value
            $('#ddlYear').val('');

            // set #ddlMonth to default value
            $('#ddlMonth').val('');

            $('#ddlNoAkaunPenghutang').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                apiSettings: {
                    url: 'AkaunPenghutangWS.asmx/GetNoAkaunPenghutangList?q={query}',
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
                            var text = option.kod + " - " + option.id + " - " + option.nama;
                            $(objItem).append($('<div class="item" data-value="' + option.kod + '">').html(text));
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

        $('#penghutangReportFilter').on('change', function () {
            var selectedValue = $(this).val();
            if (selectedValue == 'PAP') {
                $('#noPenghutangSection').show();
                $('#monthSection').hide();
                $('#yearSection').hide();

                // show #btnSearchSection
                $('#btnSearchSection').show();
                $('#btnSearch').text('Penyata Akaun Penghutang')
            } else if (selectedValue == 'PLAP') {
                $('#noPenghutangSection').show();
                $('#monthSection').show();
                $('#yearSection').show();

                // show #btnSearchSection
                $('#btnSearchSection').show();
                $('#btnSearch').text('Penyata Akaun Penghutang Terperinci')
            } else if (selectedValue == 'LPAT') {
                $('#noPenghutangSection').hide();
                $('#monthSection').hide();
                $('#yearSection').show();

                // show #btnSearchSection
                $('#btnSearchSection').show();
                $('#btnSearch').text('Laporan Pemprosesan Akhir Tahun')
            } else {
                $('#noPenghutangSection').hide();
                $('#monthSection').hide();
                $('#yearSection').hide();

                // hide #btnSearchSection
                $('#btnSearchSection').hide();
            }
        });

        $('.btnSearch').click(async function () {
            var selectedValue = $('#penghutangReportFilter').val();
            var params = `scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=0,height=0,left=-1000,top=-1000`;

            if (selectedValue == 'PAP') {
                var noPenghutang = $('#ddlNoAkaunPenghutang').val();
                if (noPenghutang == '') {
                    alert('Sila masukkan No. Penghutang / ID Penghutang');
                    return;
                }
                window.open('<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Laporan/CetakPenyataAkaunPenghutang.aspx")%>?category=PAP&noAkaun=' + noPenghutang, '_blank', params);
            } else if (selectedValue == 'PLAP') {
                var noPenghutang = $('#ddlNoAkaunPenghutang').val();
                if (noPenghutang == '') {
                    alert('Sila masukkan No. Penghutang / ID Penghutang');
                    return;
                }
                var month = $('#ddlMonth').val();
                var year = $('#ddlYear').val();
                window.open('<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Laporan/CetakPenyataLanjutAkaunPenghutang.aspx")%>?category=PLAP&noAkaun=' + noPenghutang + '&month=' + month + '&year=' + year, '_blank', params);
            } else if (selectedValue == 'LPAT') {
                var year = $('#ddlYear').val();
                if (year == '') {
                    alert('Sila masukkan No. Penghutang / ID Penghutang');
                    return;
                }
                window.open('<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Laporan/CetakLaporanPemprosesanAkhirTahun.aspx")%>?category=LPAT&year=' + year, '_blank', params);
            }
        });

    </script>
</asp:Content>

