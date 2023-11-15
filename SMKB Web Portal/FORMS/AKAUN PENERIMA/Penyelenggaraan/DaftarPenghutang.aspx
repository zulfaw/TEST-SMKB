<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="DaftarPenghutang.aspx.vb" Inherits="SMKB_Web_Portal.DaftarPenghutang" %>


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

        <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <%-- DIV DAFTAR PENGHUTANG --%>
            <div id="divdaftarpenghutang" runat="server" visible="true">
                <div class="modal-body">
                    <div class="table-title">
                        <h6>Daftar Penghutang</h6>
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
                                   <%-- <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="txtNoPenghutang" class="form-label">No. Penghutang</label>
                                        </div>
                                        <div class="col-3">
                                            <input type="text" class="form-control" placeholder="" id="txtNoPenghutang" name="txtNoPenghutang" readonly>
                                        </div>
                                    </div>--%>
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
                                            <label for="ddlKategoriPenghutang" class="form-label">Kategori Penghutang</label>
                                        </div>
                                        <div class="col-3">
                                            <select id="ddlKategoriPenghutang" class="ui search dropdown" name="ddlKategoriPenghutang"></select>
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
                                                <input type="radio" id="rdAktif" name="rdStatus" value="1"/>&nbsp;  Aktif
                                            </div>
                                            <div class="form-check form-check-inline radio-size">
                                                <input type="radio" id="rdTidakAktif" name="rdStatus" value="0"/>&nbsp;  Tidak Aktif
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="form-row">
                        <%--<div class="form-group col-md-6">
                            <asp:LinkButton id="lbtnKembali" class="btn btn-primary" runat="server" onclick="lbtnKembali_Click"><i class="las la-angle-left"></i>Kembali</asp:LinkButton>
                        </div>--%>
                        <div class="form-group col-md-12" align="right">
                            <button type="button" class="btn btn-danger btnBatal">Batal</button>
                            <button type="button" class="btn btn-success btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Simpan dan Hantar">Simpan</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="permohonan" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle"
                aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Penghutang</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="btnCloseModal">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <!-- Create the dropdown filter -->
                        <div class="search-filter">
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-6">
                                    <label for="ddStatusFilter" class="col-sm-4 col-form-label">Status Penghutang:</label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <select id="ddStatusFilter" class="custom-select" onchange='filterByStatus()'>
                                                <option value="ALL" selected>SEMUA</option>
                                                <option value="AKTIF">AKTIF</option>
                                                <option value="TIDAK AKTIF">TIDAK AKTIF</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
            
                        <div class="modal-body">
            
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table id="tblDataSenarai" class="table table-striped" style="width: 99%">
                                        <thead>
                                            <tr style="width:100%">
                                                <th scope="col" style="width: 15%">Nama Penghutang</th>
                                                <th scope="col" style="width: 10%">ID Penghutang</th>
                                                <th scope="col" style="width: 5%">Kategori</th>
                                                <th scope="col" style="width: 10%">No. Akaun</th>
                                                <th scope="col" style="width: 10%">Alamat 1</th>
                                                <th scope="col" style="width: 10%">Alamat 2</th>
                                                <th scope="col" style="width: 5%">Negara</th>
                                                <th scope="col" style="width: 5%">Negeri</th>
                                                <th scope="col" style="width: 5%">Poskod</th>
                                                <th scope="col" style="width: 10%">Emel</th>
                                                <th scope="col" style="width: 10%">No. Telefon</th>
                                                <th scope="col" style="width: 5%">Status</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableID_Senarai">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script>
            var shouldPop = true;

            var fetchKodKategoriPenghutang;
            var kodKategoriArray = [];

            var fetchKodNegara;
            var kodNegaraArray = [];

            var fetchKodNegeri;
            var kodNegeriArray = [];

            var fetchKodPoskod;
            var kodPoskodArray = [];

            var tbl = null;
            var senaraiPenghutangData = null;

            $(document).ready(function () {
                // get all kategori penghutang and store in array
                fetchKodKategoriPenghutang = GetKategoriValue('');
                fetchKodKategoriPenghutang.then(function (result) {
                    if (result && result.length > 0) {
                        kodKategoriArray = result;
                    }
                }).catch(function (error) {
                    console.error('Error:', error);
                });

                // get all kod negara and store in array
                fetchKodNegara = GetNegaraValue('');
                fetchKodNegara.then(function (result) {
                    if (result && result.length > 0) {
                        kodNegaraArray = result;
                    }
                }).catch(function (error) {
                    console.error('Error:', error);
                });

                // get all kod negeri and store in array
                fetchKodNegeri = GetNegeriValue('');
                fetchKodNegeri.then(function (result) {
                    if (result && result.length > 0) {
                        kodNegeriArray = result;
                    }
                }).catch(function (error) {
                    console.error('Error:', error);
                });

                // get all kod poskod and store in array
                fetchKodPoskod = GetPoskodValue('');
                fetchKodPoskod.then(function (result) {
                    if (result && result.length > 0) {
                        kodPoskodArray = result;
                    }
                }).catch(function (error) {
                    console.error('Error:', error);
                });

                Promise.all([fetchKodKategoriPenghutang, fetchKodNegara, fetchKodNegeri, fetchKodPoskod]).then(function (values) {
                    InitializeSenaraiPenghutang();
                });

                // initilize value of status in senarai penghutang
                $('#ddStatusFilter').val('ALL');
            });

            async function InitializeSenaraiPenghutang() {
                var result = JSON.parse(await AjaxLoadRecord_SenaraiPenghutang())

                tbl = $("#tblDataSenarai").DataTable({
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
                        { "data": "nama" },
                        { "data": "idPenghutang" },
                        {
                            "data": "kategoriPenghutang",
                            "render": function (data, type, row) {
                                var kategori = "";
                                kodKategoriArray.forEach(function (itemKategori) {
                                    if (itemKategori.value == data) {
                                        kategori = itemKategori.text;
                                        return;
                                    }
                                });
                                return kategori;
                            }
                        },
                        { "data": "noAkaun" },
                        { "data": "alamat1" },
                        { "data": "alamat2" },
                        {
                            "data": "kodNegara",
                            "render": function (data, type, row) {
                                var negara = "";
                                kodNegaraArray.forEach(function (itemNegara) {
                                    if (itemNegara.value == data) {
                                        negara = itemNegara.text;
                                        return;
                                    }
                                });
                                return negara;
                            }
                        },
                        {
                            "data": "kodNegeri",
                            "render": function (data, type, row) {
                                var negeri = "";
                                kodNegeriArray.forEach(function (itemNegeri) {
                                    if (itemNegeri.value == data) {
                                        negeri = itemNegeri.text;
                                        return;
                                    }
                                });
                                return negeri;
                            }
                        },
                        {
                            "data": "poskod",
                            "render": function (data, type, row) {
                                var poskod = "";

                                kodPoskodArray.forEach(function (itemPoskod) {
                                    if (itemPoskod.value == data) {
                                        poskod = itemPoskod.text;
                                    }

                                    if (poskod == "") {
                                        // GetPoskodValue(data)
                                        //     .then(function (result) {
                                        //         if (result && result.length > 0) {
                                        //             var text = result[0].text;
                                        //             poskod = text;
                                        //         }
                                        //     })
                                        //     .catch(function (error) {
                                        //         console.error('Error:', error);
                                        //     });
                                    }
                                });
                                return poskod;
                            }
                        },
                        { "data": "emel" },
                        { "data": "telBimbit" },
                        {
                            "data": "status",
                            "render": function (data, type, row) {
                                var status = (data == "1") ? "AKTIF" : "TIDAK AKTIF";
                                return status;
                            }
                        }
                    ]
                });
            }

            async function ajaxSavePenghutang(category) {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'PenghutangWS.asmx/SavePenghutang',
                        method: 'POST',
                        data: JSON.stringify(category),
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
            }
            $('.btnSimpan').click(async function () {
                var msg = "";
                var penghutang = null;

                if ($('#txtOldId').val() == "") {
                    penghutang = {
                        penghutang: {
                            Nama: $('#txtNama').val(),
                            Id: $('#txtId').val(),
                            OldId: '',
                            NoAkaun: $('#txtNoAkaun').val(),
                            Alamat1: $('#txtAlamat1').val(),
                            Alamat2: $('#txtAlamat2').val(),
                            KodNegara: $('#ddlNegara').val(),
                            KodNegeri: $('#ddlNegeri').val(),
                            Poskod: $('#ddlPoskod').val(),
                            NoTelefon: $('#txtNoTelefon').val(),
                            Email: $('#txtEmel').val(),
                            KategoriPenghutang: $('#ddlKategoriPenghutang').val(),
                            Status: $("input[name='rdStatus']:checked").val()
                        }
                    }
                } else {
                    penghutang = {
                        penghutang: {
                            Nama: $('#txtNama').val(),
                            OldId: $('#txtOldId').val(),
                            Id: $('#txtId').val(),
                            NoAkaun: $('#txtNoAkaun').val(),
                            Alamat1: $('#txtAlamat1').val(),
                            Alamat2: $('#txtAlamat2').val(),
                            KodNegara: $('#ddlNegara').val(),
                            KodNegeri: $('#ddlNegeri').val(),
                            Poskod: $('#ddlPoskod').val(),
                            NoTelefon: $('#txtNoTelefon').val(),
                            Email: $('#txtEmel').val(),
                            KategoriPenghutang: $('#ddlKategoriPenghutang').val(),
                            Status: $("input[name='rdStatus']:checked").val()
                        }
                    }
                }

                msg = "Anda pasti ingin menyimpan rekod ini?"

                if (!confirm(msg)) {
                    return false;
                }
                var result = JSON.parse(await ajaxSavePenghutang(penghutang));
                if (result.Status) {
                    clearAllFields();
                    alert(result.Message);
                    location.reload();
                } else {
                    alert(result.Message);
                }
            });

            $('.btnBatal').click(function () {
                clearAllFields();
            });

            function clearAllFields() {
                $('#txtNama').val('');
                $('#txtId').val('');
                $('#txtOldId').val('');
                $('#txtNoAkaun').val('');
                $('#txtAlamat1').val('');
                $('#txtAlamat2').val('');
                $('#ddlNegara').empty();
                $('#ddlNegara').val('');
                $('#ddlNegeri').empty();
                $('#ddlNegeri').val('');
                $('#ddlPoskod').empty();
                $('#ddlPoskod').val('');
                $('#txtNoTelefon').val('');
                $('#txtEmel').val('');

                console.log($('#ddlKategoriPenghutang').val());
                console.log($('#ddlKategoriPenghutang').html());
                $('#ddlKategoriPenghutang').val('');
                $('#ddlKategoriPenghutang').html(null);
                $('#ddlKategoriPenghutang').removeClass('text');
                $('#ddlKategoriPenghutang').empty();
                $('#ddlKategoriPenghutang').find('option').remove();
                $('#ddlKategoriPenghutang').dropdown('refresh');
                console.log($('#ddlKategoriPenghutang').val());
                console.log($('#ddlKategoriPenghutang').html());

                $("input[name='rdStatus']").prop('checked', false);
            }

            $('#ddlKategoriPenghutang').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
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
            $('#ddlNegara').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                apiSettings: {
                    url: 'PenghutangWS.asmx/GetNegaraList?q={query}',
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

            $('#ddlNegeri').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                apiSettings: {
                    url: 'PenghutangWS.asmx/GetNegeriList?q={query}',
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

            $('#ddlPoskod').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                apiSettings: {
                    url: 'PenghutangWS.asmx/GetPoskodList?q={query}',
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

            // call ajax to load data from server for Senarai Penghutang
            async function AjaxLoadRecord_SenaraiPenghutang() {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'PenghutangWS.asmx/GetPenghutangList',
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

            // append data in view Senarai Penghutang table
            function appendDataToTable(data) {
                var table = $('#tableID_Senarai');
                table.empty(); // Clear existing table rows
                $.each(data, function (index, item) {
                    var row = $('<tr class="data-row" style="cursor: pointer;">').appendTo(table);

                    var status = "";
                    if (item.status == "1") {
                        status = "AKTIF";
                    }
                    else {
                        status = "TIDAK AKTIF";
                    }

                    row.click(function () {
                        rowClickHandler(item); // Call the rowClickHandler function passing the item data
                    });

                    row.hover(function () {
                        $(this).addClass('bg-warning');
                    }, function () {
                        $(this).removeClass('bg-warning');
                    });

                    $('<td>').text(item.nama).appendTo(row);
                    $('<td>').text(item.idPenghutang).appendTo(row);

                    var kategori = "";
                    kodKategoriArray.forEach(function (itemKategori) {
                        if (itemKategori.value == item.kategoriPenghutang) {
                            kategori = itemKategori.text;
                        }
                    });
                    $('<td>').text(kategori).appendTo(row);
                        
                    $('<td>').text(item.noAkaun).appendTo(row);
                    $('<td>').text(item.alamat1 + ' ' + item.alamat2).appendTo(row);

                    var negara = "";
                    kodNegaraArray.forEach(function (itemNegara) {
                        if (itemNegara.value == item.kodNegara) {
                            negara = itemNegara.text;
                        }
                    });
                    $('<td>').text(negara).appendTo(row);

                    var negeri = "";
                    kodNegeriArray.forEach(function (itemNegeri) {
                        if (itemNegeri.value == item.kodNegeri) {
                            negeri = itemNegeri.text;
                        }
                    });
                    $('<td>').text(negeri).appendTo(row);

                    var poskod = "";
                    kodPoskodArray.forEach(function (itemPoskod) {
                        if (itemPoskod.value == item.poskod) {
                            poskod = itemPoskod.text;
                            $('<td>').text(poskod).appendTo(row);
                            return;
                        }
                    });

                    var id = item.idPenghutang;
                    if (poskod == "") {
                        $('<td>').attr('id', id).text("fetching..").appendTo(row);
                        poskod = GetPoskodValue(item.poskod)
                        poskod.then(function (result) {
                            if (result && result.length > 0) {
                                var text = result[0].text;
                                // update poskod in <td id="#{id}">
                                $('td#' + id).text(text);
                            }
                        })
                        .catch(function (error) {
                            console.error('Error:', error);
                        });
                    }

                    $('<td>').text(item.emel).appendTo(row);
                    $('<td>').text(item.telBimbit).appendTo(row);
                    $('<td>').text(status).appendTo(row);
                });
            }

            // function to filter status in Senarai Penghutang table
            function filterByStatus() {
                var statusVal = $('#ddStatusFilter').val();
                var rex = new RegExp(statusVal)
                if (statusVal === "ALL") { 
                    tbl.columns(11).search('').draw(); // Clear the status column filter
                } else if (statusVal == "TIDAK AKTIF") {
                    tbl.columns(11).search('^TIDAK AKTIF$', true, false).draw(); // Apply filter for TIDAK AKTIF status

                } else {
                    tbl.columns(11).search('^AKTIF$', true, false).draw(); // Apply filter for AKTIF status
                }
            }

            function clearFilter() {
                $('.data-row').show();
            }

            // andling the row click event
            function rowClickHandler(item) {
                $('#permohonan').modal('toggle');
                $('#txtNama').val(item.nama);
                $('#txtId').val(item.idPenghutang);
                $('#txtOldId').val(item.idPenghutang);
                $('#txtNoAkaun').val(item.noAkaun);
                $('#txtAlamat1').val(item.alamat1);
                $('#txtAlamat2').val(item.alamat2);

                // append $('#ddlKategoriPenghutang') with data from server
                $('#ddlKategoriPenghutang').empty();
                var kategoriPromise = GetKategoriValue(item.kategoriPenghutang);
                kategoriPromise.then(function (result) {
                    if (result && result.length > 0) {
                        var text = result[0].text;
                        var option = $('<option>').attr('value', item.kategoriPenghutang).text(text);
                        $('#ddlKategoriPenghutang').append(option);
                    }
                }).catch(function (error) {
                    console.error('Error:', error);
                });

                $('#ddlNegara').empty();
                var negaraPromise = GetNegaraValue(item.kodNegara);
                negaraPromise.then(function (result) {
                    if (result && result.length > 0) {
                        var text = result[0].text;
                        var option = $('<option>').attr('value', item.kodNegara).text(text);
                        $('#ddlNegara').append(option);
                    }
                }).catch(function (error) {
                    console.error('Error:', error);
                });

                $('#ddlNegeri').empty();
                var negeriPromise = GetNegeriValue(item.kodNegeri);
                negeriPromise.then(function (result) {
                    if (result && result.length > 0) {
                        var text = result[0].text;
                        var option = $('<option>').attr('value', item.kodNegeri).text(text);
                        $('#ddlNegeri').append(option);
                    }
                }).catch(function (error) {
                    console.error('Error:', error);
                });

                $('#ddlPoskod').empty();
                var poskodPromise = GetPoskodValue(item.poskod);
                poskodPromise.then(function (result) {
                    if (result && result.length > 0) {
                        var text = result[0].text;
                        var option = $('<option>').attr('value', item.poskod).text(text);
                        $('#ddlPoskod').append(option);
                    }
                }).catch(function (error) {
                    console.error('Error:', error);
                });
                
                $('#txtNoTelefon').val(item.telBimbit);
                $('#txtEmel').val(item.emel);

                $("input[name='rdStatus'][value='" + item.status + "']").prop('checked', true);
            }

            // get data from server for $('#ddlKategoriPenghutang') dropdown when row is selected
            function GetKategoriValue(kod, callback) {
                return new Promise(function (resolve, reject) {
                    $.ajax({
                        url: 'PenghutangWS.asmx/GetKategoriValue',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ q: kod }),
                        success: function (data) {
                            resolve(JSON.parse(data.d)); // Pass the result to the callback function
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.error('Error:', errorThrown);
                            console.error('Error:', xhr);
                            reject(false); // Pass false to the callback function indicating an error
                        }
                    });
                });
            }
            // get data from server for $('#ddlNegara') dropdown when row is selected
            function GetNegaraValue(kod, callback) {
                return new Promise(function (resolve, reject) {
                    $.ajax({
                        url: 'PenghutangWS.asmx/GetNegaraValue',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ q: kod }),
                        success: function (data) {
                            resolve(JSON.parse(data.d)); // Pass the result to the callback function
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.error('Error:', errorThrown);
                            console.error('Error:', xhr);
                            reject(false); // Pass false to the callback function indicating an error
                        }
                    });
                });
            }
            // get data from server for $('#ddlNegeri') dropdown when row is selected
            function GetNegeriValue(kod, callback) {
                return new Promise(function (resolve, reject) {
                    $.ajax({
                        url: 'PenghutangWS.asmx/GetNegeriValue',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ q: kod }),
                        success: function (data) {
                            resolve(JSON.parse(data.d)); // Pass the result to the callback function
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.error('Error:', errorThrown);
                            console.error('Error:', xhr);
                            reject(false); // Pass false to the callback function indicating an error
                        }
                    });
                });
            }
            // get data from server for $('#ddlPoskod') dropdown when row is selected
            function GetPoskodValue(kod, callback) {
                return new Promise(function (resolve, reject) {
                    $.ajax({
                        url: 'PenghutangWS.asmx/GetPoskodValue',
                        method: 'POST',
                        contentType: 'application/json; charset=utf-8',
                        data: JSON.stringify({ q: kod }),
                        success: function (data) {
                            resolve(JSON.parse(data.d)); // Pass the result to the callback function
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            console.error('Error:', errorThrown);
                            console.error('Error:', xhr);
                            reject(false); // Pass false to the callback function indicating an error
                        }
                    });
                });
            }

        </script>
    </contenttemplate>
</asp:Content>