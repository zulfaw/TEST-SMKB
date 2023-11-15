<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="RegisterPenghutang.aspx.vb" Inherits="SMKB_Web_Portal.RegisterPenghutang" %>

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
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <%-- DIV DAFTAR PENGHUTANG --%>
            <div id="divdaftarpenghutang" runat="server" visible="true">
                <div class="modal-body">
                    <div class="table-title">
                        <h4>Daftar Penghutang</h4>
                        <div class="btn btn-primary btnPapar" onclick="ShowPopup('2')">
                            Senarai Penghutang  
                        </div>
                    </div>
                    <hr>
                    <div>
                        <h6>Maklumat Penghutang</h6>
                        <hr>
                        <div class="row">
                            <div class="col-md">
                                <div class="form-col">
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="ddlKategoriPenghutang" class="form-label">Kategori Penghutang</label>
                                        </div>
                                        <div class="col-3">
                                            <select id="ddlKategoriPenghutang" class="ui search dropdown" name="ddlKategoriPenghutang"></select>
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="txtNama" class="form-label">Nama</label>
                                        </div>
                                        <div class="col-6">
                                            <input type="text" class="form-control" placeholder="" id="txtNama" name="txtNama" autocomplete="off">
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="txtId" class="form-label">ID Penghutang</label>
                                        </div>
                                        <div class="col-3">
                                            <input type="text" class="form-control d-none" id="txtOldId" name="txtOldId" autocomplete="off" value="">
                                            <input type="text" class="form-control" placeholder="" id="txtId" name="txtId" autocomplete="off">
                                        </div>
                                    </div>

                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="txtNoAkaun" class="form-label">No. Akaun</label>
                                        </div>
                                        <div class="col-4">
                                            <input type="text" class="form-control" placeholder="" id="txtNoAkaun" name="txtNoAkaun" autocomplete="off">
                                        </div>
                                    </div>
                                    <div class="form-group mb-2 d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="" class="form-label">Alamat</label>
                                        </div>
                                        <div class="col-6">
                                            <input type="text" class="form-control" placeholder="" id="txtAlamat1" name="txtAlamat1" autocomplete="off">
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                        </div>
                                        <div class="col-6">
                                            <input type="text" class="form-control" placeholder="" id="txtAlamat2" name="txtAlamat2" autocomplete="off">
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="ddlNegara" class="form-label">Negara</label>
                                        </div>
                                        <div class="col-3">
                                            <select id="ddlNegara" class="ui search dropdown" name="ddlNegara"></select>
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="ddlNegeri" class="form-label">Negeri</label>
                                        </div>
                                        <div class="col-3">
                                            <select id="ddlNegeri" class="ui search dropdown" name="ddlNegeri"></select>
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="txtPoskod" class="form-label">Poskod</label>
                                        </div>
                                        <div class="col-3">
                                            <select id="ddlPoskod" class="ui search dropdown" name="ddlPoskod"></select>
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="txtEmel" class="form-label">Emel</label>
                                        </div>
                                        <div class="col-4">
                                            <input type="email" class="form-control" placeholder="" id="txtEmel" name="txtEmel" autocomplete="off">
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="txtNoTelefon" class="form-label">No. Telefon</label>
                                        </div>
                                        <div class="col-4">
                                            <input type="text" class="form-control" placeholder="" id="txtNoTelefon" name="txtNoTelefon" autocomplete="off">
                                        </div>
                                    </div>
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="rdStatus" class="form-label">Status</label>
                                        </div>
                                        <div class="radio-btn-form col-md" id="rdStatus" name="rdStatus" autocomplete="off">
                                            <div class="form-check form-check-inline radio-size">
                                                <input type="radio" id="rdAktif" name="rdStatus" value="1" />&nbsp;  Aktif
                                            </div>
                                            <div class="form-check form-check-inline radio-size">
                                                <input type="radio" id="rdTidakAktif" name="rdStatus" value="0" />&nbsp;  Tidak Aktif
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group col-md-12" align="right">
                            <button type="button" class="btn btn-danger btnBatal">Batal</button>
                            <button type="button" class="btn btn-success btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Simpan dan Hantar">Simpan</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal -->
            <div class="modal fade" id="KategoriPenghutang" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle_"><label id="tajuk" name="tajuk"></label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table id="tblDataSenarai_Kategori" class="table table-striped" style="width: 99%">
                                        <thead>
                                            <tr style="width:100%">
                                                <th scope="col" style="width: 15%"><label id="no_rujukan" name="no_rujukan"></label></th>
                                                <th scope="col" style="width: 10%">Nama</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableID_Senarai_Kategori">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        <script>
            $('#ddlKategoriPenghutang').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                onChange: function (value, text, $selectedItem) {

                    //console.log(value,text);
                    $(".modal-body div").val("");
                    KategoriPenghutang(value);
                    $('#KategoriPenghutang').modal('toggle');
                    //if (value === '10') {
                    //    $('#no_rujukan').html("No.Bil");
                    //    $('#id_penghutang').html("ID Penghutang");
                    //    loadurusniaga(value);
                    //    $('#tajuk').html(text);
                    //    $('#JenisUrusniaga').modal('toggle');
                    //} else {

                    //}
                },
                apiSettings: {
                    url: 'PenghutangWS.asmx/GetKodPenghutangList?q={query}',
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

                        // Refresh dropdown
                        $(obj).dropdown('refresh');

                        if (shouldPop === true) {
                            $(obj).dropdown('show');
                        }
                    }
                }
            });
            async function KategoriPenghutang(id) {
                var result = JSON.parse(await AjaxLoadRecord_KategoriPenghutang())
                console.log("haii")
                tbl = $("#tblDataSenarai_Kategori").DataTable({
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
                        { "data": "MS01_NoStaf" },
                        { "data": "MS01_Nama" }
                    ]
                });
            }
            // call ajax to load data from server for kategori Penghutang
            async function AjaxLoadRecord_KategoriPenghutang() {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'PenghutangWS.asmx/GetKATEGORIList',
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
        </script>
    </contenttemplate>
</asp:Content>
