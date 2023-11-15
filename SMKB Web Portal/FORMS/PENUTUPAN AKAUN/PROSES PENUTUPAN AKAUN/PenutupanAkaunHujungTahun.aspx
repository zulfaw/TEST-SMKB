<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PenutupanAkaunHujungTahun.aspx.vb" Inherits="SMKB_Web_Portal.PenutupanAkaunHujungTahun" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
 
    <div class="search-filter">
        <div class="form-row justify-content-center" style="width: 100%">
            <div class="form-group row col-md-6">
                <asp:Label ID="lblReturnValue" runat="server" visible="false"></asp:Label>
                
                <label for="tahun" class="col-sm-2 col-form-label" style="text-align: right">Tahun :</label>
                <div class="input-group col-sm-6">
                    <asp:DropDownList ID="tahun" runat="server" CssClass="form-control" AutoPostback="true"></asp:DropDownList>
                </div>
            </div>
        </div>
        <br />

        <hr />

        <div class="d-flex flex-column m-2">
            <div class="form-row justify-content-center mb-5" style="width: 100%">
                <div class="form-group justify-content-center" style="width: 95%">
                    <div style="width: 100%" class="d-flex flex-column justify-content-center align-items-center">
                        <div class="d-flex flex-row justify-content-around mb-3 align-items-center" style="width: 25%;">
                            <div class="d-flex flex-row justify-content-center align-items-center">                                
                                <asp:Label ID="lblBackupStatusIndicator" runat="server" Text="" align="right"></asp:Label>&nbsp&nbsp
                                <asp:Label ID="lblBackupStatus" runat="server" Text="" CssClass="font-weight-bolder"></asp:Label>
                            </div>
                        </div>
                        <div style="width: 100%">
                            <button type="button" class="btn btn-warning" style="width: 100%" id="btnBackup" runat="server"><i class="fa fa-refresh"></i>Backup Data</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-row justify-content-center mb-5" style="width: 100%">
                <div class="form-group justify-content-center" style="width: 95%">
                    <div style="width: 100%">
                        <div style="width: 100%">
                            <button type="button" class="btn btn-warning" style="width: 100%" id="btnProcess" runat="server"><i class="fa fa-spinner"></i>Mula Proses Akhir tahun</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-row justify-content-center" style="width: 100%">
                <div class="form-group justify-content-center" style="width: 95%">
                    <div style="width: 100%">
                        <div style="width: 100%">
                            <button type="button" class="btn btn-warning" style="width: 100%" id="btnRestore" runat="server"><i class="fa fa-redo"></i>Restore Data</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--Inform Modal--%>
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
                    Sila masukkan tahun
                </div>
                <div id="result"></div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Tutup</button>
                </div>
            </div>
        </div>
    </div>

    <%--modal popup confirmation--%>
    <div class="modal fade" id="YearEndProcessing" tabindex="-1" role="dialog" aria-labelledby="YearEndProcessingLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="YearEndProcessingLabel">Pengesahan</h5>
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

    <script type="text/javascript">

        function showLoader() {
            show_loader();
        }

        function closeLoader() {
            close_loader();
        }

        $(document).ready(function () {
            $('#DDYearEndProcess').dropdown({
                fullTextSearch: false,
                apiSettings: {
                    url: '../PenutupanAkaunWS.asmx/GetTahunProses?q={query}',
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

                        console.log("test " + listOptions)

                        $.each(listOptions, function (index, option) {
                            $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.text));
                        });

                        // Refresh dropdown
                        $(obj).dropdown('refresh');

                        /*if (shouldPop === true)*/ {
                            $(obj).dropdown('show');
                        }
                    }
                }
            });
        });

        $('.btn-warning').click(function() {
            showLoader();
        })

        $('.btn-success').click(function () {
            var Year = $('#DDYearEndProcess').val();
            if (checkflagNull(Year) == 1) {
                $('#YearEndProcessing').modal('show');
            } else {
                $('#InformBox').modal('show');
            }
        });

        //check if the dropdown is null
        function checkflagNull(Year) {
            if (Year == null) {
                return 0;
            } else {
                return 1;
            }
        }

        //checking penutupan akaun for each month

        //checking penutupan akaun past year
        //proses summary masuk ke akaun 13
    </script>
</asp:Content>
