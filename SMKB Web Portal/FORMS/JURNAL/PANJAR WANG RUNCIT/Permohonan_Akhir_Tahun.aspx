<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Permohonan_Akhir_Tahun.aspx.vb" Inherits="SMKB_Web_Portal.Permohonan_Akhir_Tahun" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <div id="PermohonanTab" class="tabcontent" style="display: block">

        <div class="modal fade" id="kemaskini" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Kemaskini Permohonan</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="application-table table-responsive">
                                <table id="tblDataSenarai_perm" class="table table-striped" style="width: 95%">
                                    <thead>
                                        <tr>
                                            <th scope="col">Tarikh Mohon</th>
                                            <th scope="col">No. PWR</th>
                                            <th scope="col">Jabatan/Fakulti</th>
                                            <th scope="col">Amaun (RM)</th>
                                            <th scope="col">Kemaskini</th>
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
                <h6>Kemaskini Permohonan</h6>
                <div class="btn btn-primary btnPaparan" onclick="PopupSenarai('2')">
                    <i class="fa fa-list"></i>Senarai Permohonan
                </div>
            </div>

            <hr />
            <div class="row">
                <div class="col-md-12">
                    <div class="form-row">
                        <div class="form-group col-md-3">
                            <label>No. Siri</label>
                            <input type="text" class="form-control" id="lblNoSiri" readonly />
                        </div>

                        <div class="form-group col-md-2">
                            <label>Tarikh</label>
                            <input type="date" class="form-control" id="txtTarikh">
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group col-md-5">
                            <label>No. PTj</label>
                            <input type="text" class="form-control" id="lblNoPtj" />
                        </div>

                        <div class="form-group col-md-4">
                            <label>Kumpulan</label>
                            <br />
                            <select class="ui search dropdown Kumpulan" name="ddlKumpulan" id="ddlKumpulan"></select>
                        </div>

                        <div class="form-group col-md-3" align="right">
                            <label style="visibility: hidden">Tambah</label>
                            <br />
                            <div class="btn btn-primary btnPaparan" onclick="PopupSenarai2('2')">
                                <i class="fa fa-plus"></i>Tambah Senarai Permohonan
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="form-row">

            <h6>Permohonan</h6>

            <div class="col-md-12">
                <div class="application-table table-responsive">
                    <table class="table table-bordered" id="tblPermohonanAkhir">
                        <thead>
                            <tr>
                                <th scope="col">Butir Perbelanjaan</th>
                                <th scope="col">COA</th>
                                <th scope="col">PTj</th>
                                <th scope="col">Jumlah</th>
                                <th scope="col">Baki</th>
                            </tr>
                        </thead>

                        <tbody id="tableID">
                            <tr style="/*display: none; */width: 100%;" class="table-list">
                                <td>
                                    <input type="text" name="lblButirPerb" id="lblButirPerb" class="form-control" />
                                </td>
                                <td style="width: 20%">
                                    <%--<select class="ui search dropdown coa-list" id="ddlCoa" name="ddlCoa" style="width100%">
                                        <option selected="selected">111-01-11201-00</option>
                                    </select>--%>
                                    <input type="text" id="lblCoa" name="lblCoa" class="form-control" />
                                </td>
                                <td style="width: 12%">
                                    <input type="number" name="lblPtj" id="lblPtj" class="form-control" />
                                </td>
                                <td style="width: 10%">
                                    <input id="jumlah" name="jumlah" runat="server" type="number" class="form-control Jumlah"
                                        style="text-align: right" width="10%" />
                                </td>
                                <td style="width: 10%">
                                    <input id="baki" name="baki" runat="server" type="number" class="form-control Baki"
                                        style="text-align: right" width="10%" />
                                </td>
                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td>
                                    <button type="button" class="btn btn-warning btnAddRow" data-val="1" value="1"><b>+ Tambah</b></button>
                                </td>
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group col-md-12" align="right">
                <button type="button" class="btn btn-danger">Padam</button>
                <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip"
                    data-placement="bottom" title="Draft">
                    Simpan</button>
                <button type="button" class="btn btn-success btnHantar" data-toggle="tooltip"
                    data-placement="bottom" title="Simpan dan Hantar">
                    Hantar</button>
            </div>
        </div>
        <br />

        <div class="col-md-12">
            <div class="form-row">
                <div class="form-group col-md-6">
                    <label>Jumlah Pusingan</label>
                    <input type="text" class="form-control" id="lblJumPusingan" style="width: 50%; display: inline" readonly />
                </div>

                <div class="form-group col-md-6" align="right">
                    <label>Jumlah Permohonan</label>
                    <input type="text" class="form-control" id="lblJumPermohonan" style="width: 50%; display: inline" readonly />

                </div>

                <div class="form-group col-md-6">
                    <label style="width: 21.5%">Baki Semasa</label>
                    <input type="text" class="form-control" id="lblBakiSemasa" style="width: 50%; display: inline" readonly />
                </div>
            </div>
        </div>
    </div>

    <script>

        function PopupSenarai(elm) {

            if (elm == "1") {
                $('#kemaskini').modal('toggle');
            }

            else if (elm == "2") {
                $('.modal-body div').val("");
                $('#kemaskini').modal('toggle');
            }
        }

        $(document).ready(function () {
            $('.btnAddRow').click(function () {
                var row = $('#tblPermohonanAkhir tbody>tr:first').clone();
                row.attr("style", "");
                $('#tblPermohonanAkhir').append(row);
            });
        });

        var searchQuery = "";
        var oldSearchQuery = "";
        var shouldPop = true;
        $(document).ready(function () {
            $('#tblPermohonanAkhir').DataTable({
                responsive: true,
                searching: false,
                paging: false,
                language: {
                    lengthMenu: "",
                    zeroRecords: "",
                    info: "",
                    infoEmpty: ""
                },
            });

            $('#tblDataSenarai_perm').DataTable({
                responsive: true,
                searching: false,
                paging: false,
                language: {
                    lengthMenu: "",
                    zeroRecords: "",
                    info: "",
                    infoEmpty: ""
                },
            });
        });

        $(document).ready(function () {
            $('#ddlKumpulan').dropdown({
                fullTextSearch: false,
                apiSettings: {
                    url: 'Panjar_WS.asmx/GetKump',
                    method: 'POST',
                    dataType: "JSON",
                    contentType: 'application/json; charset=utf-8',
                    cache: false,
                    beforeSend: function (settings) {
                        // Replace {query} placeholder in data with user-entered search term
                        settings.data = JSON.stringify({ q: settings.urlData.query });
                        /*searchQuery = settings.urlData.query;*/
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
                            $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.value + " - " +option.text));
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

    </script>

</asp:Content>
