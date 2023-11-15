<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Transaksi.aspx.vb" Inherits="SMKB_Web_Portal.Transaksi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
        <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
        <script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.html5.min.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.print.min.js" crossorigin="anonymous"></script>

   

    <div id="PermohonanTab" class="tabcontent" style="display: block">

        <!-- Modal -->
        <div id="permohonan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Laporan Transaksi Jurnal</h5>

                    </div>
                    <!-- Create the dropdown filter -->
                    <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align:right">Carian :</label>
                                <div class="col-sm-8">
                                    <div class="input-group">
                                        <select id="categoryFilter" class="custom-select" >
                                            <option value="">SEMUA</option>
                                            <option value="1" selected="selected">Hari Ini</option>
                                            <option value="2">Semalam</option>
                                            <option value="3">7 Hari Lepas</option>
                                            <option value="4">30 Hari Lepas</option>
                                            <option value="5">60 Hari Lepas</option>
                                            <option value="6">Pilih Tarikh</option>
                                        </select>
                                         <button id="btnSearch" runat="server" class="btn btnSearch" type="button">
                                                <i class="fa fa-search"></i>
                                            </button>
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
                                            <label id="lblMula" style="text-align: right;"  >Mula:</label>
                                        </div>
                                        
                                        <div class="form-group col-md-4">
                                            <input type="date" id="txtTarikhStart" name="txtTarikhStart" class="form-control date-range-filter">
                                        </div>
                                         <div class="form-group col-md-1">
                                     
                                        </div>
                                        <div class="form-group col-md-1">
                                            <label id="lblTamat" style="text-align: right;" >Tamat:</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <input type="date" id="txtTarikhEnd" name="txtTarikhEnd" class="form-control date-range-filter">
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai_trans_rpt" class="table table-striped" style="width: 95%">
                                    <thead>
                                        <tr>                                          
                                            <th scope="col">No. Jurnal</th>
                                            <th scope="col">No. Rujukan</th>
                                            <th scope="col">Butiran</th>
                                            <th scope="col">Tarikh Transaksi</th>
                                            <th scope="col">Kod Vot</th>                                            
                                            <th scope="col">Debit (RM)</th>
                                            <th scope="col">Kredit (RM)</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_trans">
                                    </tbody>

                                </table>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="modal fade" id="transaksi" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Papar Transaksi Jurnal</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="table-title">
                            <h6>Transaksi Jurnal</h6>
                            <%--  <div class="btn btn-primary btnPapar" onclick="ShowPopup('2')">
                    <i class="fa fa-plus"></i>Senarai Transaksi Jurnal  
                </div>--%>
                        </div>

                        <hr>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-row">
                                    <div class="form-group col-md-2">
                                        <label>No. Jurnal</label>
                                        <input type="text" class="form-control" id="lblNoJurnal" readonly>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Jenis Transaksi</label>
                                        <br />
                                        <select class="ui search dropdown JenTransaksi" name="ddlJenTransaksi" id="ddlJenTransaksi"></select>
                                    </div>
                                    <div class="form-group col-md-4" style="visibility: hidden">
                                        <br />
                                        <br />
                                        <asp:CheckBox ID="chkStaLejar" runat="server" Text=" Pelarasan lejar sahaja (WIP/Pelupusan Aset)/Hapus Kira" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">

                                <div class="form-row">
                                    <div class="form-group col-md-3">
                                        <label>Tarikh Transaksi</label>
                                        <input type="date" class="form-control" id="txtTarikh" readonly>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>No. Rujukan</label>
                                        <input type="text" class="form-control" id="txtNoRujukan" readonly>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Perihal</label>
                                        <textarea class="form-control" rows="1" id="txtPerihal" readonly></textarea>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <hr>
                        <div class="form-row">

                            <h6>Transaksi</h6>


                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table class="table table-bordered" id="tblData">
                                        <thead>
                                            <tr>
                                                <th scope="col">COA</th>
                                                <th scope="col">PTj</th>
                                                <th scope="col">Kumpulan Wang</th>
                                                <th scope="col">Operasi</th>
                                                <th scope="col">Projek</th>
                                                <th scope="col">Debit (RM)</th>
                                                <th scope="col">Kredit(RM)</th>
                                                <th scope="col">Tindakan</th>

                                            </tr>
                                        </thead>
                                        <tbody id="tableID">
                                            <tr style="display: none; width: 100%" class="table-list">
                                                <td style="width: 20%">
                                                    <select class="ui search dropdown COA-list" name="ddlCOA" id="ddlCOA" style="width: 100%"></select>
                                                    <input type="hidden" class="data-id" value="" />
                                                    <label id="hidVot" name="hidVot" class="Hid-vot-list"></label>
                                                </td>
                                                <td style="width: 12%">
                                                    <label id="lblPTj" name="lblPTj" class="label-ptj-list"></label>
                                                    <label id="HidlblPTj" name="HidlblPTj" class="Hid-ptj-list" style="visibility: hidden"></label>
                                                </td>

                                                <td style="width: 12%">
                                                    <label id="lblKw" name="lblKw" class="label-kw-list"></label>
                                                    <label id="HidlblKw" name="HidlblKw" class="Hid-kw-list" style="visibility: hidden"></label>
                                                </td>
                                                <td style="width: 10%">
                                                    <label id="lblKo" name="lblKo" class="label-ko-list"></label>
                                                    <label id="HidlblKo" name="HidlblKo" class="Hid-ko-list" style="visibility: hidden"></label>
                                                </td>
                                                <td style="width: 10%">
                                                    <label id="lblKp" name="lblKp" class="label-kp-list"></label>
                                                    <label id="HidlblKp" name="HidlblKp" class="Hid-kp-list" style="visibility: hidden"></label>
                                                </td>
                                                <td style="width: 8%">
                                                    <input id="Debit" name="Debit" runat="server" type="number" class="form-control underline-input Debit" style="text-align: right" width="10%" value="0.00" /></td>
                                                <td style="width: 8%">
                                                    <input id="Kredit" name="Kredit" runat="server" type="number" class="form-control underline-input Kredit" style="text-align: right" width="10%" value="0.00" /></td>
                                                <td style="width: 3%">
                                                    <button id="lbtnDelete" runat="server" class="btn btnDelete" type="button" style="color: red">
                                                        <i class="fa fa-trash"></i>
                                                    </button>
                                                </td>

                                            </tr>
                                        </tbody>
                                        <tfoot>
                                            <tr>
                                                <td colspan="5"></td>
                                                <td>
                                                    <input class="form-control underline-input" id="totalDbt" name="totalDbt" style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly /></td>
                                                <td>
                                                    <input class="form-control underline-input" id="totalKt" name="totalKt" style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly /></td>
                                                <td colspan="7"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <div class="btn-group">
                                                        <button type="button" class="btn btn-warning btnAddRow One" data-val="1" value="1"><b>+ Tambah</b></button>
                                                        <button type="button" class="btn btn-warning btnAddRow dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                            <span class="sr-only">Toggle Dropdown</span>
                                                        </button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item btnAddRow five" value="5" data-val="5">Tambah 5</a>
                                                            <a class="dropdown-item btnAddRow" value="10" data-val="10">Tambah 10</a>

                                                        </div>
                                                    </div>
                                                </td>

                                                <th colspan="4" style="text-align: right; font-size: large">JUMLAH BEZA <font style="color: grey">(DEBIT - KREDIT)</font></th>
                                                <td tyle="text-align:center">
                                                    <input class="form-control underline-input" id="totalBeza" name="totalBeza" style="text-align: right; font-size: large; font-weight: bold" value="0.00" readonly />
                                                </td>
                                                <td colspan="3"></td>
                                            </tr>
                                        </tfoot>
                                    </table>

                                </div>
                            </div>

                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-12" align="right">
                                <%--    <button type="button" class="btn btn-danger">Padam</button>
                                <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Draft">Simpan</button>
                                <button type="button" class="btn btn-success btnHantar" data-toggle="tooltip" data-placement="bottom" title="Simpan dan Hantar">Hantar</button>--%>
                                <button type="button" class="btn btn-success btnLulus" data-toggle="tooltip" data-placement="bottom" title="Simpan dan Hantar">Lulus</button>
                                <button type="button" class="btn btn-danger btnXLulus" data-toggle="tooltip" data-placement="bottom" title="Simpan dan Hantar">Tidak Lulus</button>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        <script type="text/javascript">

            function ShowPopup(elm) {

                if (elm == "1") {

                    $('#transaksi').modal('toggle');


                }
                else if (elm == "2") {

                    $(".modal-body div").val("");
                    $('#transaksi').modal('toggle');

                }
            }



            var tbl = null
            $(document).ready(function () {
                //Take the category filter drop down and append it to the datatables_filter div. 
                //You can use this same idea to move the filter anywhere withing the datatable that you want.
                $("#tblDataSenarai_trans_rpt_filter.dataTables_filter").append($("#categoryFilter"));

                tbl = $("#tblDataSenarai_trans_rpt").DataTable({
                    "responsive": true,
                    "searching": true,
                    dom: 'Bfrtip',
                    buttons: [
                     'csv', 'excel', 'pdf', 'print' 
                    ],

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
                        "sInfoEmpty": "Menunjukkan 0 ke 0 daripada rekod",
                        "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
                        "sEmptyTable": "Tiada rekod.",
                        "sSearch": "Carian"
                    },
                    "ajax": {
                        "url": "LejarPenghutangWS.asmx/LoadOrderRecord_SenaraiLulusTransaksiJurnal",
                        method: 'POST',
                        "contentType": "application/json; charset=utf-8",
                        "dataType": "json",
                        "dataSrc": function (json) {
                            return JSON.parse(json.d);
                        }

                    },               
                    "columns": [
                        {
                            "data": "No_Jurnal",
                            render: function (data, type, row, meta) {

                                if (type !== "display") {

                                    return data;

                                }

                                var link = `<td style="width: 10%" >
                                                <label id="lblNo" name="lblNo" class="lblNo" value="${data}" >${data}</label>
                                                <input type ="hidden" class = "lblNo" value="${data}"/>
                                            </td>`;
                                return link;
                            }
                        },
                        { "data": "No_Rujukan" },
                        { "data": "Butiran" },                       
                        { "data": "Tkh_Transaksi" },
                        { "data": "Kod_Vot" },
                        {
                            "data": "Debit",
                            "width": "30%",
                            "render": function (data) {
                                return parseFloat(data).toLocaleString();
                            },
                            "className": "align-right" // Add CSS class for right alignment
                        },
                        {
                            "data": "Kredit",
                            "width": "30%",
                            "render": function (data) {
                                return parseFloat(data).toLocaleString();
                            },
                            "className": "align-right" // Add CSS class for right alignment
                        }

                   
                    ]

                });



                //Get the column index for the Category column to be used in the method below ($.fn.dataTable.ext.search.push)
                //This tells datatables what column to filter on when a user selects a value from the dropdown.
                //It's important that the text used here (Category) is the same for used in the header of the column to filter
                var categoryIndex = 0;
                $("#tblDataSenarai_trans_rpt th").each(function (i) {
                    if ($($(this)).html() == "Tarikh Transaksi") {
                        categoryIndex = i; return false;
                    }
                });
              

                //Use the built in datatables API to filter the existing rows by the Category column
                $.fn.dataTable.ext.search.push(
                    function (settings, data, dataIndex) {
                        var selectedItem = $('#categoryFilter').val()

                        if (selectedItem == "1") {
                            selectedItem = moment().format('DD-MM-YYYY');

                           // $('#txtTarikhStart').attr('readonly', true);
                           // $('#txtTarikhEnd').attr('readonly', true);

                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("")
                            $('#txtTarikhEnd').val("")

                            $('#lblMula').hide();
                            $('#lblTamat').hide(); 

                  
                        }

                        else if (selectedItem == "2") {
                            selectedItem = moment().subtract(1, "days").format('DD-MM-YYYY');

                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("")
                            $('#txtTarikhEnd').val("")

                            $('#lblMula').hide();
                            $('#lblTamat').hide(); 
                        }

                        else if (selectedItem == "3") {
                            var min = moment().subtract(6, "days").format('DD-MM-YYYY');
                            var max = moment().format('DD-MM-YYYY');

                            var createdAt = data[categoryIndex]; // Our date column in the table
                            // selectedItem = moment().format('DD-MM-YYYY');


                            if (                                
                                (min <= createdAt && createdAt <= max)
                            ) {
                                return true;
                            }
                            return false;



                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("")
                            $('#txtTarikhEnd').val("")

                            $('#lblMula').hide();
                            $('#lblTamat').hide(); 

                            //var createdAt = data[categoryIndex]; // Our date column in the table
                            //// selectedItem = moment().format('DD-MM-YYYY');


                            //if (
                            //    (min === null && max === null) ||
                            //    (min === null && createdAt <= max) ||
                            //    (min <= createdAt && max === null) ||
                            //    (min <= createdAt && createdAt <= max)
                            //) {
                            //    return true;
                            //}
                            //return false;

                        }

                        else if (selectedItem == "4") {
                            var min = moment().subtract(29, "days").format('DD-MM-YYYY');
                            var max = moment().format('DD-MM-YYYY');

                           
                            var createdAt = data[categoryIndex]; // Our date column in the table
                            // selectedItem = moment().format('DD-MM-YYYY');

                            if (   (min <= createdAt && createdAt <= max) ) {
                                return true;
                            }
                            return false;

                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("")
                            $('#txtTarikhEnd').val("")

                            $('#lblMula').hide();
                            $('#lblTamat').hide(); 
                        }

                        else if (selectedItem == "5") {
                            var min = moment().subtract(59, "days").format('DD-MM-YYYY');
                            var max = moment().format('DD-MM-YYYY');

                            var createdAt = data[categoryIndex]; // Our date column in the table
                            // selectedItem = moment().format('DD-MM-YYYY');


                            if (
                                (min === null && max === null) ||
                                (min === null && createdAt <= max) ||
                                (min <= createdAt && max === null) ||
                                (min <= createdAt && createdAt <= max)
                            ) {
                                return true;
                            }
                            return false;

                            $('#txtTarikhStart').hide();
                            $('#txtTarikhEnd').hide();

                            $('#txtTarikhStart').val("")
                            $('#txtTarikhEnd').val("")

                            $('#lblMula').hide();
                            $('#lblTamat').hide(); 
                        }

                        else if (selectedItem == "6") {


                            if (($('#txtTarikhStart').val() != "") || ($('#txtTarikhEnd').val() != "")) {
                               

                                var min = new Date($('#txtTarikhStart').val());
                                var max = new Date($('#txtTarikhEnd').val());

                                min = ("0" + min.getDate()).slice(-2) + "-" + ("0" + (min.getMonth() + 1)).slice(-2) + "-" + min.getFullYear();
                                max = ("0" + max.getDate()).slice(-2) + "-" + ("0" + (max.getMonth() + 1)).slice(-2) + "-" + max.getFullYear();

                            

                                var createdAt = data[categoryIndex]; // Our date column in the table
                               // selectedItem = moment().format('DD-MM-YYYY');
                              

                                if (
                                    (min === null && max === null) ||
                                    (min === null && createdAt <= max) ||
                                    (min <= createdAt && max === null) ||
                                    (min <= createdAt && createdAt <= max)
                                ) {
                                    return true;
                                }
                                return false;
                                 


                            }
                            else {
                                $('#txtTarikhStart').show();
                                $('#txtTarikhEnd').show();

                                $('#txtTarikhStart').val("")
                                $('#txtTarikhEnd').val("")

                                $('#lblMula').show();
                                $('#lblTamat').show(); 
                            }

                            
                        }

                        if (selectedItem != "6") {
                            var category = data[categoryIndex];

                            if (selectedItem === "" || category.includes(selectedItem)) {
                                return true;
                            }
                            return false;
                        }
                    }
                );

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

                //// Re-draw the table when the a date range filter changes
                //$('.date-range-filter').change(function (e) {
                //    tbl.draw();
                //});

                // Refilter the table
                $('#btnSearch').clikc(function (e) {
                    tbl.draw();
                });

                //tbl.draw();

            });

            //$('.btnPapar').click(async function () {
            //    tbl.ajax.reload();
            //});

          

            var searchQuery = "";
            var oldSearchQuery = "";
            var curNumObject = 0;
            var tableID = "#tblData";
            var tableID_Senarai = "#tblDataSenarai_trans_rpt";
            var shouldPop = true;
            var totalID = "#totalBeza";

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

                //loadKelulusanRecords()

                $('#ddlJenTransaksi').dropdown({
                    fullTextSearch: false,
                    apiSettings: {
                        url: 'LejarPenghutangWS.asmx/GetJenisTransaksi?q={query}',
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

            $(function () {
                $('.btnAddRow.five').click();
            });

            $('.btnHantar').click(async function () {
                var jumRecord = 0;
                var acceptedRecord = 0;
                var msg = "";
                var newOrder = {
                    order: {
                        OrderID: $('#lblNoJurnal').val(),
                        NoRujukan: $('#txtNoRujukan').val(),
                        Perihal: $('#txtPerihal').val(),
                        Tarikh: $('#txtTarikh').val(),
                        JenisTransaksi: $('#ddlJenTransaksi').val(),
                        JumlahDebit: $('#totalDbt').val(),
                        Jumlahkredit: $('#totalkt').val(),
                        JumlahBeza: $('#totalBeza').val(),
                        OrderDetails: []
                    }

                }


                $('.ptj-list').each(function (index, obj) {

                    if (index > 0) {
                        var tcell = $(obj).closest("td");
                        //alert("ce;; "+tcell)
                        var orderDetail = {
                            OrderID: $('#lblNoJurnal').val(),
                            ddlPTJ: $(obj).dropdown("get value"),
                            ddlVot: $('.vot-list').eq(index).dropdown("get value"),
                            ddlKw: $('.kw-list').eq(index).dropdown("get value"),
                            ddlKo: $('.ko-list').eq(index).dropdown("get value"),
                            ddlKp: $('.kp-list').eq(index).dropdown("get value"),
                            debit: $('.Debit').eq(index).val(),
                            kredit: $('.Kredit').eq(index).val(),
                            id: $(tcell).find(".data-id").val()
                        };

                        //console.log(orderDetail);

                        //if (orderDetail.ddlPTJ === "" || orderDetail.ddlVot === "" || orderDetail.ddlKw === "" || orderDetail.ddlKo === "" || orderDetail.ddlKp === "" ||
                        //    orderDetail.debit === "" ||  orderDetail.kredit === "") {
                        //    return;
                        //}

                        acceptedRecord += 1;
                        newOrder.order.OrderDetails.push(orderDetail);

                    }

                });

                //console.log(newOrder.order);

                msg = "Anda pasti ingin menghantar " + acceptedRecord + " rekod ini?"

                if (!confirm(msg)) {
                    return false;
                }

                var result = JSON.parse(await ajaxSubmitOrder(newOrder));
                alert(result.Message)
                //$('#orderid').val(result.Payload.OrderID)

                //loadExistingRecords();
                //await clearAllRows();
                // AddRow(5);

            });

            $('.btnLulus').click(async function () {



                var jumRecord = 0;
                var acceptedRecord = 0;
                var msg = "";
                var newOrder = {
                    order: {
                        OrderID: $('#lblNoJurnal').val(),
                        NoRujukan: $('#txtNoRujukan').val(),
                        Perihal: $('#txtPerihal').val(),
                        Tarikh: $('#txtTarikh').val(),
                        JenisTransaksi: $('#ddlJenTransaksi').val(),
                        JumlahDebit: $('#totalDbt').val(),
                        Jumlahkredit: $('#totalkt').val(),
                        JumlahBeza: $('#totalBeza').val(),
                        OrderDetails: []
                    }

                }


                $('.COA-list').each(function (index, obj) {

                    if (index > 0) {
                        var tcell = $(obj).closest("td");
                        //alert("ce;; "+tcell)
                        var orderDetail = {
                            OrderID: $('#lblNoJurnal').val(),
                            ddlVot: $(obj).dropdown("get value"),
                            ddlPTJ: $('.Hid-ptj-list').eq(index).html(),
                            ddlKw: $('.Hid-kw-list').eq(index).html(),
                            ddlKo: $('.Hid-ko-list').eq(index).html(),
                            ddlKp: $('.Hid-kp-list').eq(index).html(),
                            debit: $('.Debit').eq(index).val(),
                            kredit: $('.Kredit').eq(index).val(),
                            id: $(tcell).find(".data-id").val()
                        };


                        //console.log( orderDetail);

                        //if (orderDetail.ddlPTJ === "" || orderDetail.ddlVot === "" || orderDetail.ddlKw === "" || orderDetail.ddlKo === "" || orderDetail.ddlKp === "" ||
                        //    orderDetail.debit === "" ||  orderDetail.kredit === "") {
                        //    return;
                        //}

                        acceptedRecord += 1;
                        newOrder.order.OrderDetails.push(orderDetail);

                    }

                });


                msg = "Anda pasti ingin meluluskan jurnal ini?"

                if (!confirm(msg)) {
                    return false;
                }

                var result = JSON.parse(await ajaxSaveOrderLulus(newOrder));
                alert(result.Message)


                $(".modal-body div").val("");
                $('#transaksi').modal('toggle');

                tbl.ajax.reload();

            });


            $('.btnXLulus').click(async function () {
                var jumRecord = 0;
                var acceptedRecord = 0;
                var msg = "";
                var newOrder = {
                    order: {
                        OrderID: $('#lblNoJurnal').val(),
                        NoRujukan: $('#txtNoRujukan').val(),
                        Perihal: $('#txtPerihal').val(),
                        Tarikh: $('#txtTarikh').val(),
                        JenisTransaksi: $('#ddlJenTransaksi').val(),
                        JumlahDebit: $('#totalDbt').val(),
                        Jumlahkredit: $('#totalkt').val(),
                        JumlahBeza: $('#totalBeza').val(),
                        OrderDetails: []
                    }

                }


                $('.COA-list').each(function (index, obj) {

                    if (index > 0) {
                        var tcell = $(obj).closest("td");
                        //alert("ce;; "+tcell)
                        var orderDetail = {
                            OrderID: $('#lblNoJurnal').val(),
                            ddlVot: $(obj).dropdown("get value"),
                            ddlPTJ: $('.Hid-ptj-list').eq(index).html(),
                            ddlKw: $('.Hid-kw-list').eq(index).html(),
                            ddlKo: $('.Hid-ko-list').eq(index).html(),
                            ddlKp: $('.Hid-kp-list').eq(index).html(),
                            debit: $('.Debit').eq(index).val(),
                            kredit: $('.Kredit').eq(index).val(),
                            id: $(tcell).find(".data-id").val()
                        };


                        //console.log(orderDetail);

                        //if (orderDetail.ddlPTJ === "" || orderDetail.ddlVot === "" || orderDetail.ddlKw === "" || orderDetail.ddlKo === "" || orderDetail.ddlKp === "" ||
                        //    orderDetail.debit === "" ||  orderDetail.kredit === "") {
                        //    return;
                        //}

                        acceptedRecord += 1;
                        newOrder.order.OrderDetails.push(orderDetail);

                    }

                });


                msg = "Anda pasti ingin TIDAK meluluskan jurnal ini?"

                if (!confirm(msg)) {
                    return false;
                }

                var result = JSON.parse(await ajaxSaveOrderXLulus(newOrder));
                alert(result.Message)


                $(".modal-body div").val("");
                $('#transaksi').modal('toggle');

                tbl.ajax.reload();

            });

            $('.btnSearch').click(async function () {

                var startDate = $('#txtTarikhStart').val()
                var endDate = $('#txtTarikhEnd').val()
                tbl.draw();

            
            })


            $('.btn-danger').click(async function () {
                //alert("test");
                //var result = JSON.parse(await ajaxDeleteOrder($('#lblNoJurnal').val()))
                $('#lblNoJurnal').val("")
                await clearAllRows();
                await clearAllRowsHdr();
                AddRow(5);
            });

            //$('.btnPapar').click(async function () {
            //    var record = await AjaxLoadOrderRecord_Senarai("");
            //    $('#lblNoJurnal').val("")
            //    await clearAllRows_senarai();
            //    await paparSenarai(null, record);
            //});

            $('.btnLoad').on('click', async function () {
                loadExistingRecords();
            });


            //async function loadKelulusanRecords() {
            //    var record = await AjaxLoadOrderRecord_Senarai("");
            //    $('#lblNoJurnal').val("")
            //    await clearAllRows_senarai();
            //    await paparSenarai(null, record);
            //}

            async function loadExistingRecords() {
                var record = await AjaxLoadOrderRecord($('#lblNoJurnal').val());
                await clearAllRows();
                await AddRow(null, record);
            }

            async function clearAllRows() {
                $(tableID + " > tbody > tr ").each(function (index, obj) {
                    if (index > 0) {
                        obj.remove();
                    }
                })
                $(totalDebit).val("0.00");
                $(totalKredit).val("0.00");
                $(totalID).val("0.00"); //total beza
            }

            async function clearAllRowsHdr() {

                $('#lblNoJurnal').val("");
                $('#txtNoRujukan').val("");
                $('#txtTarikh').val("");
                $('#txtPerihal').val("");
                $('#ddlJenTransaksi').empty();


            }

            async function clearAllRows_senarai() {
                $(tableID_Senarai + " > tbody > tr ").each(function (index, obj) {
                    if (index > 0) {
                        obj.remove();
                    }
                })
            }

            $(tableID).on('click', '.btnDelete', async function () {
                event.preventDefault();
                var curTR = $(this).closest("tr");
                var recordID = curTR.find("td > .data-id");
                var bool = true;
                var id = setDefault(recordID.val());

                if (id !== "") {
                    bool = await DelRecord(id);
                }

                if (bool === true) {
                    curTR.remove();
                }

                calculateGrandTotal();
                return false;
            })

            async function ajaxSubmitOrder(order) {

                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'LejarPenghutangWS.asmx/SubmitOrders',
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

            async function ajaxSaveOrder(order) {

                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'LejarPenghutangWS.asmx/Lulusorder',
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

            async function ajaxSaveOrderLulus(order) {

                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'LejarPenghutangWS.asmx/Lulusorder',
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

            async function ajaxSaveOrderXLulus(order) {

                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'LejarPenghutangWS.asmx/XLulusorder',
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

            async function ajaxDeleteOrder(id) {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'LejarPenghutangWS.asmx/DeleteOrder',
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
            async function AjaxDelete(id) {
                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: 'LejarPenghutangWS.asmx/DeleteRecord',
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

            async function AjaxLoadOrderRecord(id) {

                try {
                    const response = await fetch('LejarPenghutangWS.asmx/LoadOrderRecord', {
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

            async function AjaxLoadOrderRecord_Senarai(id) {

                try {
                    const response = await fetch('LejarPenghutangWS.asmx/LoadOrderRecord_SenaraiLulusTransaksiJurnal', {
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
            async function DelRecord(id) {
                var bool = false;
                var result = JSON.parse(await AjaxDelete(id));

                if (result.Code === "00") {
                    bool = true;
                }

                return bool;
            }


            $(tableID).on('keyup', '.Debit , .Kredit', async function () {

                var curTR = $(this).closest("tr");

                var debit_ = $(curTR).find("td > .Debit");
                var totalDebit = NumDefault(debit_.val())

                var kredit_ = $(curTR).find("td > .Kredit");
                calculateGrandTotal();

                var totalKredit = NumDefault(kredit_.val())
                calculateGrandTotal();


            });

            async function calculateGrandTotal() {

                //debit
                var grandTotal_Dt = $(totalDebit);

                var curTotal_Dt = 0;

                $('.Debit').each(function (index, obj) {
                    curTotal_Dt += parseFloat(NumDefault($(obj).val()));
                });

                grandTotal_Dt.val(curTotal_Dt.toFixed(2));

                //kredit
                var grandTotal_Kt = $(totalKredit);
                var curTotal_Kt = 0;

                $('.Kredit').each(function (index, obj) {
                    curTotal_Kt += parseFloat(NumDefault($(obj).val()));
                });

                grandTotal_Kt.val(curTotal_Kt.toFixed(2));

                //beza
                var grandTotal_Beza = $(totalID);
                var cal = curTotal_Dt - curTotal_Kt
                grandTotal_Beza.val(cal.toFixed(2));

            }


            //async function calculateGrandBeza() {
            //    var grandTotal = $(totalID);

            //    var totalDebit = 0;
            //    var totalKredit = 0;

            //    var curTR = $(this).closest("tr");

            //    var debit_ = $(curTR).find("td > .totalDbt");
            //    var totalDebit = parseFloat(NumDefault(debit_.val()));


            //    var kredit_ = $(curTR).find("td > .totalKt");
            //    var totalKredit = parseFloat(NumDefault(kredit_.val()));

            //    alert("a " + totalDebit)
            //    grandTotal.val(totalDebit - totalKredit);

            //}

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

            async function initDropdownKw(id, idVot) {

                $('#' + id).dropdown({
                    fullTextSearch: true,
                    apiSettings: {
                        url: 'LejarPenghutangWS.asmx/GetKWList?q={query}&kodkw={kodkw}',
                        method: 'POST',
                        dataType: "json",
                        contentType: 'application/json; charset=utf-8',
                        cache: false,
                        beforeSend: function (settings) {
                            // Replace {query} placeholder in data with user-entered search term

                            var kodVot = $('#' + idVot).dropdown("get value");
                            settings.urlData.kodkw = kodVot;
                            settings.data = JSON.stringify({ q: settings.urlData.query, kodkw: settings.urlData.kodkw });
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



            }

            async function initDropdownKo(id, idKw) {


                $('#' + id).dropdown({
                    fullTextSearch: true,
                    apiSettings: {
                        url: 'LejarPenghutangWS.asmx/GetKoList?q={query}&kodko={kodko}',
                        method: 'POST',
                        dataType: "json",
                        contentType: 'application/json; charset=utf-8',
                        cache: false,
                        beforeSend: function (settings) {
                            // Replace {query} placeholder in data with user-entered search term
                            var kodkw = $('#' + idKw).dropdown("get value");
                            settings.urlData.kodko = kodkw;
                            settings.data = JSON.stringify({ q: settings.urlData.query, kodko: settings.urlData.kodko });
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

            }

            async function initDropdownKp(id, idKo) {

                $('#' + id).dropdown({
                    fullTextSearch: true,
                    apiSettings: {
                        url: 'LejarPenghutangWS.asmx/GetKpList?q={query}&kodko={kodko}',
                        method: 'POST',
                        dataType: "json",
                        contentType: 'application/json; charset=utf-8',
                        cache: false,
                        beforeSend: function (settings) {

                            var kodkp = $('#' + idKo).dropdown("get value");
                            settings.urlData.kodko = kodkp;
                            settings.data = JSON.stringify({ q: settings.urlData.query, kodko: settings.urlData.kodko });
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


            }

            $(tableID_Senarai).on('click', '.btnView', async function () {

                event.preventDefault();
                var curTR = $(this).closest("tr");
                var recordID = curTR.find("td > .lblNo");
                //var bool = true;
                var id = setDefault(recordID.html());


                if (id !== "") {
                    await clearAllRows();

                    //BACA HEADER JURNAL
                    var recordHdr = await AjaxGetRecordHdrJurnal(id);
                    await AddRowHeader(null, recordHdr);

                    //BACA DETAIL JURNAL
                    var record = await AjaxGetRecordJurnal(id);
                    // console.log("record 123")
                    await AddRow(null, record);

                }

                return false;
            })

            async function initDropdownVot(id, idPtj) {

                $('#' + id).dropdown({
                    fullTextSearch: true,
                    apiSettings: {
                        url: 'LejarPenghutangWS.asmx/GetVotList?q={query}&kodVot={kodVot}',
                        method: 'POST',
                        dataType: "json",
                        contentType: 'application/json; charset=utf-8',
                        cache: false,
                        beforeSend: function (settings) {
                            // Replace {query} placeholder in data with user-entered search term

                            var kodPtj = $('#' + idPtj).dropdown("get value");
                            settings.urlData.kodVot = kodPtj;
                            settings.data = JSON.stringify({ q: settings.urlData.query, kodVot: settings.urlData.kodVot });
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
            }

            async function initDropdownPtj(id) {

                $('#' + id).dropdown({
                    fullTextSearch: true,
                    apiSettings: {
                        url: 'LejarPenghutangWS.asmx/GetVotPTJ?q={query}',
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
            }

            $('.btnAddRow').click(async function () {
                var totalClone = $(this).data("val");

                await AddRow(totalClone);
            });

            //async function AddRow(totalClone, objOrder) {
            //    var counter = 1;
            //    var table = $('#tblData');

            //    if (objOrder !== null && objOrder !== undefined) {
            //        //totalClone = objOrder.Payload.OrderDetails.length;
            //        totalClone = objOrder.Payload.length;

            //        //if (totalClone < 5) {
            //        //    totalClone = 5;
            //        //}
            //    }

            //    while (counter <= totalClone) {
            //        curNumObject += 1;
            //        var newId_kw = "ddlKW" + curNumObject;
            //        var newId_Ko = "ddlKo" + curNumObject;
            //        var newId_Kp = "ddlKp" + curNumObject;
            //        var newId_vot = "ddlVot" + curNumObject;
            //        var newId_ptj = "ddlPTJ" + curNumObject;

            //        var row = $('#tblData tbody>tr:first').clone();

            //        var dropdown = $(row).find(".kw-list").attr("id", newId_kw);
            //        var dropdown1 = $(row).find(".ko-list").attr("id", newId_Ko);
            //        var dropdown2 = $(row).find(".kp-list").attr("id", newId_Kp);
            //        var dropdown3 = $(row).find(".vot-list").attr("id", newId_vot);
            //        var dropdown4 = $(row).find(".ptj-list").attr("id", newId_ptj);

            //        row.attr("style", "");
            //        var val = "";

            //        $('#tblData tbody').append(row);

            //        await initDropdownPtj(newId_ptj)
            //        $(newId_ptj).api("query");


            //        await initDropdownVot(newId_vot, newId_ptj)
            //        $(newId_vot).api("query");

            //        await initDropdownKw(newId_kw, newId_vot)
            //        $(newId_kw).api("query");

            //        await initDropdownKo(newId_Ko, newId_kw)
            //        $(newId_Ko).api("query");

            //        await initDropdownKp(newId_Kp, newId_Ko)
            //        $(newId_Kp).api("query");


            //        if (objOrder !== null && objOrder !== undefined) {
            //            //await setValueToRow(row, objOrder.Payload.OrderDetails[counter - 1]);
            //            if (counter <= objOrder.Payload.length) {
            //                await setValueToRow_Transaksi(row, objOrder.Payload[counter - 1]);
            //            }
            //        }
            //        counter += 1;
            //    }
            //}

            async function AddRow(totalClone, objOrder) {

                var counter = 1;
                var table = $('#tblData');


                if (objOrder !== null && objOrder !== undefined) {
                    //totalClone = objOrder.Payload.OrderDetails.length;
                    totalClone = objOrder.Payload.length;

                    //if (totalClone < 5) {
                    //    totalClone = 5;
                    //}
                }
                //console.log("totalClone " + counter + " " + totalClone )


                while (counter <= totalClone) {

                    curNumObject += 1;

                    var newId_coa = "ddlCOA" + curNumObject;
                    // console.log("coa1 " + newId_coa)
                    //var newId_kw = "ddlKW" + curNumObject;
                    //var newId_Ko = "ddlKo" + curNumObject;
                    //var newId_Kp = "ddlKp" + curNumObject;
                    //var newId_vot = "ddlVot" + curNumObject;
                    //var newId_ptj = "ddlPTJ" + curNumObject;

                    var row = $('#tblData tbody>tr:first').clone();

                    var dropdown5 = $(row).find(".COA-list").attr("id", newId_coa);

                    //console.log("coa " + dropdown5)
                    //var label = $(row).find(".label-ptj-list").attr("id", newId_kw);
                    //var dropdown1 = $(row).find(".ko-list").attr("id", newId_Ko);
                    //var dropdown2 = $(row).find(".kp-list").attr("id", newId_Kp);
                    //var dropdown3 = $(row).find(".vot-list").attr("id", newId_vot);
                    //var dropdown4 = $(row).find(".ptj-list").attr("id", newId_ptj);

                    row.attr("style", "");
                    var val = "";

                    $('#tblData tbody').append(row);

                    await initDropdownCOA(newId_coa)
                    $(newId_coa).api("query");



                    if (objOrder !== null && objOrder !== undefined) {

                        //await setValueToRow(row, objOrder.Payload.OrderDetails[counter - 1]);
                        if (counter <= objOrder.Payload.length) {
                            await setValueToRow_Transaksi(row, objOrder.Payload[counter - 1]);

                        }
                    }


                    counter += 1;
                }
            }

            async function initDropdownCOA(id) {

                $('#' + id).dropdown({
                    fullTextSearch: true,
                    onChange: function (value, text, $selectedItem) {

                        //console.log($selectedItem);

                        var curTR = $(this).closest("tr");

                        var recordIDVotHd = curTR.find("td > .Hid-vot-list");
                        recordIDVotHd.html($($selectedItem).data("coltambah5"));

                        //var selectObj = $($selectedItem).find("td > .COA-list > select");
                        //selectObj.val($($selectedItem).data("coltambah5"));



                        var recordIDPtj = curTR.find("td > .label-ptj-list");
                        recordIDPtj.html($($selectedItem).data("coltambah1"));

                        var recordIDPtjHd = curTR.find("td > .Hid-ptj-list");
                        recordIDPtjHd.html($($selectedItem).data("coltambah5"));

                        var recordID_ = curTR.find("td > .label-kw-list");
                        recordID_.html($($selectedItem).data("coltambah2"));

                        var recordIDkwHd = curTR.find("td > .Hid-kw-list");
                        recordIDkwHd.html($($selectedItem).data("coltambah6"));

                        var recordID_ko = curTR.find("td > .label-ko-list");
                        recordID_ko.html($($selectedItem).data("coltambah3"));

                        var recordIDkoHd = curTR.find("td > .Hid-ko-list");
                        recordIDkoHd.html($($selectedItem).data("coltambah7"));

                        var recordID_kp = curTR.find("td > .label-kp-list");
                        recordID_kp.html($($selectedItem).data("coltambah4"));

                        var recordIDkpHd = curTR.find("td > .Hid-kp-list");
                        recordIDkpHd.html($($selectedItem).data("coltambah8"));


                    },
                    apiSettings: {
                        url: 'LejarPenghutangWS.asmx/GetVotCOA?q={query}',
                        method: 'POST',
                        dataType: "json",
                        contentType: 'application/json; charset=utf-8',
                        cache: false,
                        fields: {

                            value: "value",      // specify which column is for data
                            name: "text",      // specify which column is for text
                            colPTJ: "colPTJ",
                            colhidptj: "colhidptj",
                            colKW: "colKW",
                            colhidkw: "colhidkw",
                            colKO: "colKO",
                            colhidko: "colhidko",
                            colKp: "colKp",
                            colhidkp: "colhidkp",

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
                                $(objItem).append($('<div class="item" data-value="' + option.value + '" data-coltambah1="' + option.colPTJ + '" data-coltambah5="' + option.colhidptj + '" data-coltambah2="' + option.colKW + '" data-coltambah6="' + option.colhidkw + '" data-coltambah3="' + option.colKO + '" data-coltambah7="' + option.colhidko + '" data-coltambah4="' + option.colKp + '" data-coltambah8="' + option.colhidkp + '" >').html(option.text));
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

            async function paparSenarai(totalClone, objOrder) {
                var counter = 1;
                var table = $('#tblDataSenarai');

                if (objOrder !== null && objOrder !== undefined) {
                    totalClone = objOrder.Payload.length;

                }
                // console.log(objOrder)

                while (counter <= totalClone) {


                    var row = $('#tblDataSenarai tbody>tr:first').clone();
                    row.attr("style", "");
                    var val = "";

                    $('#tblDataSenarai tbody').append(row);

                    if (objOrder !== null && objOrder !== undefined) {

                        if (counter <= objOrder.Payload.length) {
                            await setValueToRow(row, objOrder.Payload[counter - 1]);
                        }
                    }

                    counter += 1;
                }
            }

            async function initDropdownCOA_trans(id) {

                $('#' + id).dropdown({
                    fullTextSearch: true,

                    apiSettings: {
                        url: 'LejarPenghutangWS.asmx/GetVotCOA?q={query}',
                        method: 'POST',
                        dataType: "json",
                        contentType: 'application/json; charset=utf-8',
                        cache: false,
                        fields: {

                            value: "value",      // specify which column is for data
                            name: "text"      // specify which column is for text


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
                                $(objItem).append($('<div class="item" data-value="' + option.value + '" data-coltambah1="' + option.colPTJ + '" data-coltambah5="' + option.colhidptj + '" data-coltambah2="' + option.colKW + '" data-coltambah6="' + option.colhidkw + '" data-coltambah3="' + option.colKO + '" data-coltambah7="' + option.colhidko + '" data-coltambah4="' + option.colKp + '" data-coltambah8="' + option.colhidkp + '" >').html(option.text));
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


            async function setValueToRow_HdrJurnal(orderDetail) {

                //console.log(orderDetail)
                if (orderDetail.Kod_Status_Dok == "04") {

                    $('.btnLulus').hide();
                    $('.btnXLulus').hide();
                    $('.btnAddRow').hide();
                    $('.btnDelete').prop('disabled', true);
                }
                else if (orderDetail.Kod_Status_Dok == "09") {

                    $('.btnLulus').hide();
                    $('.btnXLulus').hide();
                    $('.btnAddRow').hide();
                    $('.btnDelete').prop('disabled', true);
                }
                else {

                    $('.btnLulus').show();
                    $('.btnXLulus').show();
                    $('.btnAddRow').hide();
                    $('.btnDelete').show();
                }

                $('#lblNoJurnal').val(orderDetail.No_Jurnal)
                $('#txtNoRujukan').val(orderDetail.No_Rujukan)
                $('#txtTarikh').val(orderDetail.Tkh_Transaksi)
                $('#txtPerihal').val(orderDetail.Butiran)

                var newId = $('#ddlJenTransaksi')

                //await initDropdownPtj(newId)
                //$(newId).api("query");

                var ddlJenTransaksi = $('#ddlJenTransaksi')
                var ddlSearch = $('#ddlJenTransaksi')
                var ddlText = $('#ddlJenTransaksi')
                var selectObj_JenisTransaksi = $('#ddlJenTransaksi')
                $(ddlJenTransaksi).dropdown('set selected', orderDetail.Jenis_Trans);
                selectObj_JenisTransaksi.append("<option value = '" + orderDetail.Jenis_Trans + "'>" + orderDetail.ButiranJenis + "</option>")
            }

            async function setValueToRow(row, orderDetail) {

                //console.log(orderDetail.No_Jurnal)
                var no = $(row).find("td > .lblNo");
                var no1 = $(row).find("td > .lblNo");
                var rujukan = $(row).find("td > .lblRujukan");
                var butiran = $(row).find("td > .lblButiran");
                var jumlah = $(row).find("td > .lblJumlah");
                var tarikh = $(row).find("td > .lblTkh");

                no.html(orderDetail.No_Jurnal);
                no1.val(orderDetail.No_Jurnal);
                rujukan.html(orderDetail.No_Rujukan);
                butiran.html(orderDetail.Butiran);
                jumlah.html(orderDetail.Jumlah);
                tarikh.html(orderDetail.Tkh_Transaksi);



            }

            async function setValueToRow_Transaksi(row, orderDetail) {

                //console.log(row)



                var ddl = $(row).find("td > .COA-list");
                var ddlSearch = $(row).find("td > .COA-list > .search");
                var ddlText = $(row).find("td > .COA-list > .text");
                var selectObj = $(row).find("td > .COA-list > select");
                $(ddl).dropdown('set selected', orderDetail.Kod_Vot);
                selectObj.append("<option value = '" + orderDetail.Kod_Vot + "'>" + orderDetail.Kod_Vot + "</option>")


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

                var debit = $(row).find("td > .Debit");
                debit.val(orderDetail.Debit);

                var kredit = $(row).find("td > .Kredit");
                kredit.val(orderDetail.Kredit);

                await calculateGrandTotal();

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

                    const response = await fetch('LejarPenghutangWS.asmx/LoadRecordJurnal', {
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

            async function AjaxGetRecordHdrJurnal(id) {

                try {

                    const response = await fetch('LejarPenghutangWS.asmx/LoadHdrJurnal', {
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

        //$(document).ready(function () {
        //    $('#tblDataSenarai').DataTable({
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
        //            "sEmptyTable": "Tiada rekod.",
        //            "sSearch": "Carian"
        //        }
        //    });
        //});
        </script>
    </div>
</asp:Content>
