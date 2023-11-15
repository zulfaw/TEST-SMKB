<%@ Page Title="" Language="vb" AutoEventWireup="false" Async="true" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="SOKONGAN_PERMOHONAN_PEMBAYARAN.aspx.vb" Inherits="SMKB_Web_Portal.PENGESAHAN_INVOIS" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>

    <script type="text/javascript">
        function ShowPopup(elm) {

            //alert("test");
            if (elm == "1") {

                $('#permohonan').modal('toggle');


            }
            else if (elm == "2") {
                s
                $(".modal-body div").val("");
                $('#permohonan').modal('toggle');

            }
        }
         

    </script>
    <style>
        #modalInvoisDataP .modal-body {
            max-height: 70vh; /* Adjust height as needed to fit your layout */
            min-height: 70vh;
            overflow-y: scroll;
            scrollbar-width: thin;
        }

        .data-penerima {
            overflow-y: hidden;
        }

            .data-penerima::-webkit-scrollbar {
                height: 1px;
            }

        #subTab a {
            cursor: pointer;
        }
    </style>
    <link rel="stylesheet" href="../style.css" />
    <contenttemplate>


        <%--<form id="form1" runat="server">--%>
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <%-- DIV PENDAFTARAN INVOIS --%>
            <div id="divpendaftaraninv" runat="server" visible="true">
                <div class="modal-body">
                    <%--<div class="table-title">
                        <h6></h6>
                        <div class="btn btn-primary btnPapar" onclick="ShowPopup('2')">
                            <i class="fa fa-plus"></i>Senarai Invois  
                        </div>
                    </div>--%>
                    <div>
                        <hr />
                        <h6>Senarai Permohonan Pembayaran</h6> 
                        <hr />
                        <!-- Create the dropdown filter -->

                        <div class="search-filter">
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-6">
                                    <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align: right">Carian :</label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <select id="invoisDateFilter" class="custom-select" onchange="dateFilterHandler(event)">
                                                <option value="all">SEMUA</option>
                                                <option value="0" selected="selected">Hari Ini</option>
                                                <option value="1">Semalam</option>
                                                <option value="7">7 Hari Lepas</option>
                                                <option value="30">30 Hari Lepas</option>
                                                <option value="60">60 Hari Lepas</option>
                                                <option value="select">Pilih Tarikh</option>
                                            </select>
                                            <button id="btnSearch" class="btn btnSearch btn-outline" type="button" onclick="loadInvois()">
                                                <i class="fa fa-search"></i>Cari
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
                                    <div class="col-md-11" id="specificDateFilter" style="display: none">
                                        <div class="form-row">
                                            <div class="form-group col-md-1">
                                                <label id="lblMula" style="text-align: right;">Mula:</label>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <input type="date" id="txtTarikhStart" name="txtTarikhStart" class="form-control input-sm date-range-filter">
                                            </div>
                                            <div class="form-group col-md-1">
                                            </div>
                                            <div class="form-group col-md-1">
                                                <label id="lblTamat" style="text-align: right;">Tamat:</label>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <input type="date" id="txtTarikhEnd" name="txtTarikhEnd" class="form-control input-sm date-range-filter">
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table id="tblSenaraiInvois" class="table table-striped" style="width: 99%">
                                        <thead>
                                            <tr>
                                                <th scope="col">Kod Rujukan</th>
                                                <th scope="col">No. Invois</th>
                                                <th scope="col">Jenis Invois</th>
                                                <th scope="col">Tarikh Daftar</th>
                                                <th scope="col">Tarikh Invois</th>
                                                <th scope="col">Tarikh Terima Ivois</th>
                                                <th scope="col">Jenis Bayar</th>
                                                <th scope="col">Tujuan</th>
                                                <th scope="col">Jumlah (RM)</th>
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

        <div class="modal fade" id="modalInvoisData" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" style="min-width: 80%" id="modalInvoisDataP" role="document">
                <div class="modal-content">
                    <div class="modal-header modal-header--sticky" style="border-bottom: none !important">


                        <div class="container-fluid mt-3">
                            <ul class="nav nav-tabs" id="subTab" role="tablist">
                                <li class="nav-item" role="presentation" onclick="subTabChange(event)" data-tab="ALL">
                                    <a class="nav-link active" tabindex="-1" data-tab="ALL" aria-disabled="true">Maklumat Permohonan</a>
                                </li> 
                                <li class="nav-item" role="presentation" onclick="subTabChange(event)" data-tab="TRK">
                                    <a class="nav-link " tabindex="-1" data-tab="TRK" aria-disabled="true">Maklumat Transaksi</a>
                                </li>
                            </ul>
                        </div>


                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="INV" class="modal-sub-tab mt-2">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row justify-content-center">
                                        <div class="col-md-3">

                                            <div class="form-group input-group">
                                                <input type="text" class=" input-group__input form-control input-sm " placeholder="No. Invois" id="txtidinv" name="txtidinv" readonly />
                                                <label class="input-group__label">ID Invois</label>
                                            </div>

                                            <div class="form-group input-group">
                                                <input type="date" class=" input-group__input form-control input-sm " placeholder="Tarikh Invois" name="tkhInv" id="tkhInv" readonly>
                                                <label class="input-group__label">Tarikh Invois</label>
                                            </div>

                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group input-group">
                                                <input type="text" class=" input-group__input form-control input-sm  " placeholder="No Invois" id="txtnoinv" name="txtnoinv" readonly />
                                                <label class="input-group__label">No Invois</label>
                                            </div>
                                            <div class="form-group input-group">
                                                <input type="date" class="input-group__input form-control input-sm  " placeholder="Tarikh Terima Invois" id="tkhTerimaInv" name="tkhTerimaInv" readonly>
                                                <label class="input-group__label">Tarikh Terima Invois</label>
                                            </div>

                                        </div>
                                        <div class="col-md-3">


                                            <div class="form-group input-group">
                                                <input type="text" class=" input-group__input form-control input-sm " placeholder="No DO" id="txtnoDO" name="txtnoDO" readonly />
                                                <label class="input-group__label">No DO</label>
                                            </div>
                                            <div class="form-group input-group">
                                                <input type="date" class=" input-group__input form-control input-sm " placeholder="Tarikh DO" name="tkhDO" id="tkhDO" readonly>
                                                <label class="input-group__label">Tarikh DO</label>
                                            </div>

                                        </div>
                                        <div class="col-md-3">

                                            <div class="form-group input-group">
                                                <input type="text" class=" input-group__input form-control input-sm " placeholder="Tarikh DO" name="tkhDO" id="jenInv" readonly>
                                                <label class="input-group__label">Jenis Invois</label>
                                            </div>

                                            <div class="form-group input-group">
                                                <input type="date" class=" input-group__input form-control input-sm " placeholder="Tarikh Terima DO" id="tkhTerimaDO" name="tkhTerimaDO" readonly>
                                                <label class="input-group__label">Tarikh Terima DO</label>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="row justify-content-center">
                                <div class="col-md-9">
                                    <div class="form-group input-group">
                                        <textarea id="txtTujuan" class=" input-group__input form-control" style="background-color: #e9ecef;" name="message" rows="1" placeholder="" readonly></textarea>
                                        <label class="input-group__label">Tujuan</label>
                                    </div>
                                </div>
                                <div class="col-md-3">


                                    <div class="form-group input-group">
                                        <input type="text" class=" input-group__input form-control input-sm " placeholder="Tarikh DO" name="tkhDO" id="jenByr" readonly>
                                        <label class="input-group__label">Jenis Pembayaran</label>
                                    </div>
                                </div>
                            </div>

                        </div>
                
                        <div id="TRK" class="modal-sub-tab mt-2">
                            <div class="row">
                                <div class="col-md-12">

                                    <table class="table table-striped" id="tblData" style="width: 100%;">
                                        <thead>
                                            <tr style="width: 100%; text-align: center">
                                                <th scope="col">Penerima</th>
                                                <th scope="col">Vot</th>
                                                <th scope="col">Kod PTJ</th>
                                                <th scope="col">Kumpulan Wang</th>
                                                <th scope="col">Kod Operasi</th>
                                                <th scope="col">Kod Projek</th>
                                                <th scope="col">Perkara</th>
                                                <th scope="col">Kuantiti</th>
                                                <th scope="col">Harga Seunit (RM)</th>
                                                <th scope="col">Cukai (%)</th>
                                                <th scope="col">Diskaun (%)</th>
                                                <th scope="col">Jumlah (RM)</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableID">
                                        </tbody>
                                    </table>
                                </div>
                                <div class="col-md-12">

                                      <div class="sticky-footer">
                    <br />
                    <div class="form-row">
                        <div class="form-group col-md-12"> 
                            <div class="float-right">
                                <span style="font-family: roboto!important; font-size: 18px!important"><b>Jumlah (<span id="result" style="margin-right: 5px">0</span> item) :RM <span id="stickyJumlah" style="margin-right: 5px">0.00</span></b></span>
                                <button type="button" class="btn" id="showModalButton"><i class="fas fa-angle-up"></i></button> 
                            </div>


                        </div>
                    </div>
                </div> 

                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer modal-footer--sticky">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-6"> 
                                </div>
                                <div class="col-md-6 ">
                                    <div class="row justify-content-end">

                                        <button type="button" class="btn btn-danger btnXLulus" data-toggle="tooltip" title="Simpan dan Hantar">Tidak Sokong</button>
                                        <p>&nbsp;</p>
                                        <button type="button" onclick="sokong()" class="btn btn-secondary btnXLulus" data-toggle="tooltip" title="Simpan dan Hantar">Sokong</button>

                                    </div>
                                </div>

                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>

        <!--modal penerima-->
        <div class="modal fade" id="modalPenerima" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog  modal-dialog-centered modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Maklumat Penerima</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-6">
                                <div class="input-group">
                                    <div class=" input-group__input form-control data-penerima" name="Nama_Pemiutang" readonly></div>
                                    <label class="input-group__label">Nama </label>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group">

                                            <div class="input-group__input form-control  data-penerima" name="Kategori_Pemiutang" readonly></div>
                                            <label class="input-group__label">Kategori</label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">

                                            <div class="input-group__input form-control  data-penerima" name="Kod_Pemiutang" readonly></div>

                                            <label class="input-group__label">ID Penerima</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">

                                        <div class="input-group">
                                            <div class=" input-group__input  form-control  data-penerima" name="Emel" readonly></div>
                                            <label class="input-group__label">Email</label>

                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        
                                        <div class="input-group">
                                            <div class=" input-group__input  form-control  data-penerima" name="Tel_Bimbit" readonly></div>
                                            <label class="input-group__label">No Telefon</label>

                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-6">
                                        
                                        <div class="input-group">

                                            <div class=" input-group__input form-control  data-penerima" name="Bank_Detail" readonly></div>
                                            <label class="input-group__label">Bank Penerima</label>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">

                                            <div class=" input-group__input form-control data-penerima" name="No_Akaun" readonly></div>
                                            <label class="input-group__label ">No Akaun</label>
                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div class="col-md-6">
                                <div class="input-group">

                                    <div class="input-group__input form-control  data-penerima" name="Alamat_1" readonly></div>
                                    <label class="input-group__label">Alamat 1</label>
                                </div>
                                <div class="input-group">
                                    <div class="input-group__input form-control  data-penerima" name="Alamat_2" readonly></div>
                                    <label class="input-group__label ">Alamat 2</label>

                                </div>
                                <div class="input-group">

                                    <div class="input-group__input form-control  data-penerima" name="Bandar_Detail" readonly></div>
                                    <label class="input-group__label">Bandar</label>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <div class="input-group__input form-control  data-penerima" name="Poskod" readonly></div>
                                            <label class="input-group__label">Poskod</label>

                                        </div>

                                    </div>
                                    <div class="col-md-6">
                                        <div class="input-group">
                                            <div class="input-group__input form-control  data-penerima" name="Negeri_Detail" readonly></div>
                                            <label class="input-group__label">Negeri</label>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>

        <!--modal penerima end-->

        <!--modal total-->
        <div class="modal fade" id="detailTotal" tabindex="-1" aria-labelledby="showDetails" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Jumlah Terperinci</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row justify-content-end mt-2">
                            <div class="col-md-6 text-right">
                                <label class="col-form-label ">Jumlah (RM)</label>

                            </div>
                            <div class="col-md-6">
                                <input class="form-control  underline-input" id="totalwoCukai" name="totalwoCukai" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly />

                            </div>
                        </div>
                        <div class="row justify-content-end mt-2">
                            <div class="col-md-6 text-right">
                                <label class="col-form-label ">Jumlah Cukai (RM)</label>

                            </div>
                            <div class="col-md-6">
                                <input class="form-control  underline-input" id="TotalTax" name="TotalTax" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly />

                            </div>
                        </div>
                        <div class="row justify-content-end mt-2">
                            <div class="col-md-6 text-right">
                                <label class="col-form-label ">Jumlah Diskaun (RM)</label>

                            </div>
                            <div class="col-md-6">
                                <input class="form-control  underline-input" id="TotalDiskaun" name="TotalDiskaun" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly />


                            </div>
                        </div>
                        <div class="row justify-content-end mt-2">
                            <div class="col-md-6 text-right">
                                <label class="col-form-label ">Jumlah Sebenar (RM)</label>

                            </div>
                            <div class="col-md-6">
                                <input class="form-control underline-input" id="total" name="total" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly />

                            </div>
                        </div>
                    </div> 
                </div>
            </div>
        </div>

        <!--modal total end-->
        <!--Modalmesej -->
        <div class="modal fade" id="MessageModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Pengesahan</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Tidak</button>
                        <button type="button" class="btn btn-secondary btnYA" data-toggle="modal"
                            data-target="#ModulForm" data-dismiss="modal">
                            Ya</button>
                    </div>
                </div>
            </div>
        </div>
        <!--Modalmesej end -->

        <script type="text/javascript" src="../../../Content/js/SharedFunction.js"></script>
        <script type="text/javascript">
            // jumlah terperinci

            const showModalButton = document.getElementById('showModalButton');
            const detailTotalModal = new bootstrap.Modal(document.getElementById('detailTotal'));

            showModalButton.addEventListener('click', () => {
                detailTotalModal.show();
            });

            showModalButton.addEventListener('mouseenter', () => {
                const buttonRect = showModalButton.getBoundingClientRect();
                const modalDialog = detailTotalModal._dialog;

                // Position the modal above and to the left of the button with adjusted offsets
                const offsetLeft = 360; // Adjust this value to move the modal to the left
                const offsetBottom = -30; // Adjust this value to move the modal downwards
                modalDialog.style.position = 'fixed';
                modalDialog.style.left = buttonRect.left - offsetLeft + 'px'; // Subtract the offset
                modalDialog.style.bottom = window.innerHeight - buttonRect.top + offsetBottom + 'px'; // Add the offset

                detailTotalModal.show();
            });

            showModalButton.addEventListener('mouseleave', () => {
                detailTotalModal.hide();
            });
            ///////

            //modal stacking
            $(document).on('show.bs.modal', '.modal', function (event) {
                var zIndex = 1040 + (10 * $('.modal:visible').length);
                $(this).css('z-index', zIndex);
                setTimeout(function () {
                    $('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
                }, 0);
            });
            //modal stacing end


            var tbl = null
            var cacheJenByr = null
            var cacheJenInvois = null
            var tblTransaksi 

            var coaVotKeyCache = []
            var coaVotValueCache = []
            var pemiutangKeyCache = []
            var pemiutangValueCache = []

            //pair of kod kump w, kod op, kod proj and sum val 

            async function sokong() {

                let conf = await show_message_async('Sokong permohonan Pembayaran?<br>  <textarea id="txtUlasan" placeholder="Ulasan"  class="form-control" rows = "1" ></textarea > ')
                if (!conf) {
                    return
                }
                let ulasan = $("#txtUlasan").val()  


                let fulldata = cacheInvois


                console.log(fulldata)
                show_loader()
                let res = await sokongPP(fulldata, ulasan).catch(function (err) {
                    console.log(err)
                })
                if (!res) {
                    notification("Gagal disokong")
                    console.log(res.Message)
                    return
                }
                notification("permohonan pembayaran berjaya disokong")
                $("#modalInvoisData").modal('hide')
                $("#btnSearch").click()
                close_loader()
            }

            function notification(msg) {
                $("#notify").html(msg)
                $("#NotifyModal").modal('show');
            }
            function sokongPP(invois,ulasan) {
                return new Promise(function (resolve, reject) {
                    let url = '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/sokongPermohonanPembayaran") %>'
                    $.ajax({
                        url: url,
                        method: "POST",
                        data: JSON.stringify({
                            "invois": invois,
                            "ulasan": ulasan
                        }),
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            console.log(data)
                            var jsondata = JSON.parse(data.d)

                            if (jsondata.Code == "200") {
                                resolve(true)
                            }
                            else {
                                reject(jsondata.Message)
                            }
                        }
                    })
                })

            }
            $(document).ready(function () {

                var today = toSqlDateString(new Date())

                document.getElementById('txtTarikhStart').setAttribute('max', today);
                document.getElementById('txtTarikhEnd').setAttribute('max', today);
                tbl = $("#tblSenaraiInvois").DataTable({
                    "responsive": true,
                    "searching": true,
                    "rowCallback": function (row, data) {
                        // Add hover effect
                        $(row).hover(function () {
                            $(this).addClass("hover pe-auto bg-warning");
                        }, function () {
                            $(this).removeClass("hover pe-auto bg-warning");
                        });
                    },
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
                    columnDefs: [
                        { targets: [8], className: 'text-right' }
                    ],
                    "columns": [
                        { "data": "ID_Rujukan" },
                        { "data": "No_Invois" },
                        { "data": "Jenis_Invois" },
                        {
                            "data": "Tarikh_Daftar",
                            render: function (data, type, row) {
                                if (!data) {

                                    return ''
                                }
                                var dateParts = data.split('T')[0].split('-');
                                return dateParts[2] + '/' + dateParts[1] + '/' + dateParts[0];
                            }
                        },
                        {
                            "data": "Tarikh_Invois",
                            render: function (data, type, row) {
                                if (!data) {

                                    return ''
                                }
                                var dateParts = data.split('T')[0].split('-');
                                return dateParts[2] + '/' + dateParts[1] + '/' + dateParts[0];
                            }
                        },
                        {
                            "data": "Tarikh_Terima_Invois",
                            render: function (data, type, row) {
                                if (!data) {

                                    return ''
                                }
                                var dateParts = data.split('T')[0].split('-');
                                return dateParts[2] + '/' + dateParts[1] + '/' + dateParts[0];
                            }
                        },
                        { "data": "Jenis_Bayar" },
                        { "data": "Tujuan" },
                        {
                            "data": "Jumlah",
                            render: function (data, type, row) {
                                return formatPrice(data)
                            }
                        }
                    ],
                    createdRow: function (row, data, dataIndex) {
                        // row.dataset.jenisinvois = data["Jenis_Invois"];
                        row.dataset.idrujukan = data["ID_Rujukan"]
                        row.onclick = showInvoisData
                    }

                });
                 
                tblTransaksi = $("#tblData").DataTable({
                    "responsive": true,
                    "searching": true, ordering: true,
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
                    "columns":
                        [
                            {
                                "data": "Kod_Pemiutang",
                                render: function (data, type, row) {
                                    let txt = ""
                                    if (row.PMINFO) {
                                        txt += (row.PMINFO.Kategori_Pemiutang != null) ? row.PMINFO.Kategori_Pemiutang : ""
                                        txt += " - "
                                        txt += (row.PMINFO.Nama_Pemiutang != null) ? row.PMINFO.Nama_Pemiutang : ""
                                    }
                                    return '<span style="cursor: help;" data-kp="' + data + '" onclick="showPenerima(this)">' + data
                                        + '&nbsp<i class="fa fa-info-circle" aria-hidden="true"></i>'
                                        + '<br><span class="additional-info">' + txt + "</span></span>"
                                }
                            },
                            {
                                data: 'Kod_Vot',
                                render: function (data, type, row) {
                                    let txt = ""
                                    if (row.VOTINFO) {
                                        txt = (row.VOTINFO.VOT != null) ? row.VOTINFO.VOT : ""
                                    }
                                    return data + '<br><span class="additional-info">' + txt + "</span>"
                                }
                            },
                            {
                                data: 'Kod_PTJ',
                                render: function (data, type, row) {
                                    let txt = ""
                                    if (row.VOTINFO) {
                                        txt = (row.VOTINFO.PTJ != null) ? row.VOTINFO.PTJ : ""
                                    }
                                    return data + '<br><span class="additional-info">' + txt + "</span>"
                                }
                            },
                            {
                                data: 'Kod_Kump_Wang',
                                render: function (data, type, row) {
                                    let txt = ""
                                    if (row.VOTINFO) {
                                        txt = (row.VOTINFO.KW != null) ? row.VOTINFO.KW : ""
                                    }
                                    return data + '<br><span class="additional-info">' + txt + "</span>"
                                }
                            },
                            {
                                data: 'Kod_Operasi',
                                render: function (data, type, row) {
                                    let txt = ""
                                    if (row.VOTINFO) {
                                        txt = (row.VOTINFO.KO != null) ? row.VOTINFO.KO : ""
                                    }
                                    return data + '<br><span class="additional-info">' + txt + "</span>"
                                }
                            },
                            {
                                data: 'Kod_Projek',
                                render: function (data, type, row) {
                                    let txt = ""
                                    if (row.VOTINFO) {
                                        txt = (row.VOTINFO.KP != null) ? row.VOTINFO.KP : ""
                                    }
                                    return data + '<br><span class="additional-info">' + txt + "</span>"
                                }
                            },
                            { "data": "Butiran" },
                            { "data": "Kuantiti_Sebenar" },
                            {
                                "data": "Kadar_Harga",
                                render: function (data, type, row) {
                                    return formatPrice(data)
                                }
                            },
                            { "data": "Cukai" },
                            { "data": "Diskaun" },
                            {
                                "data": "Jumlah_Perlu_Bayar",
                                render: function (data, type, row) {
                                    return formatPrice(data)
                                }
                            }
                        ],
                    columnDefs: [
                        { targets: 0, width: '15%' },
                        { targets: 1, width: '15%' },
                        { targets: 2, width: '5%' },
                        { targets: 3, width: '5%' },
                        { targets: 4, width: '5%' },
                        { targets: 5, width: '5%' },
                        { targets: 6, width: '10%' },
                        { targets: 7, width: '5%' },
                        { targets: 8, width: '10%' },
                        { targets: 9, width: '6%' },
                        { targets: 10, width: '6%' },
                        { targets: 11, width: '10%' },
                        { targets: [7, 8, 9, 10, 11], className: 'text-right' }
                    ],
                    createdRow: function (row, data, dataIndex) {
                        row.dataset.noitem = data["No_Item"];
                        // $(row).find('.additional-info').hide();
                    }

                });
                //load jenis bayar to display in baucr modal
                $.ajax({
                    url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/INVOIS/Transaksi_InvoisWS.asmx/GetJenByrList") %>',
                    method: "POST",
                    data: JSON.stringify({
                        q: ""
                    }),
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        jsondata = JSON.parse(data.d)
                        cacheJenByr = {}
                        jsondata.forEach(d => {
                            cacheJenByr[d.value] = d.text
                        })

                    },
                    error: function (xhr, status, error) {
                        console.error(error);
                    }
                })
                // cache jenis invois to display in baucr modal
                $.ajax({
                    url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/INVOIS/Transaksi_InvoisWS.asmx/GetJenInvList") %>',
                    method: "POST",
                    data: JSON.stringify({
                        q: ""
                    }),
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        jsondata = JSON.parse(data.d)
                        cacheJenInvois = {}
                        jsondata.forEach(d => {
                            cacheJenInvois[d.value] = d.text
                        })

                    },
                    error: function (xhr, status, error) {
                        console.error(error);
                    }
                })
            });


            //coa


            function getCOAVot(VOT, PTJ, KW, KP, KO) {
                return coaVotValueCache.filter(item =>
                    item.KodVOT === VOT &&
                    item.KodPTJ === PTJ &&
                    item.KodKW === KW &&
                    item.KodKO === KO &&
                    item.KodKP === KP
                )[0]
            }
            function searchCOAVot(arrVot) {
                //kod vot yang belum search je
                let filter = arrVot.filter(value => !coaVotKeyCache.includes(value));
                return loadCOAVot(filter)
            }


            function loadCOAVot(arrVot) {

                return new Promise(function (resolve, reject) {
                    if (arrVot.length > 0) {

                        $.ajax({
                            url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/GetVotCOASpecific") %>',
                            method: "POST",
                            data: JSON.stringify({ Kod: arrVot }),
                            dataType: "json",
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data.d) {
                                    data = JSON.parse(data.d)

                                    coaVotValueCache.push(...data)
                                    coaVotKeyCache.push(...arrVot)


                                }
                                resolve(true)

                            },
                            error: function (xhr, status, error) {
                                console.error(error);
                                reject()
                            }
                        })

                    }
                    else {
                        resolve(true)
                    }
                })
            }

            //pemiutang

            function getPemiutang(kod) {
                return pemiutangValueCache.filter(item =>
                    item.Kod_Pemiutang === kod
                )[0]
            }
            function searchPemiutang(arrKod) {
                //kod vot yang belum search je
                let filter = arrKod.filter(value => !pemiutangKeyCache.includes(value));
                return loadPemiutang(filter)
            }

            function loadPemiutang(arrKod) {
                return new Promise(function (resolve, reject) {
                    if (arrKod.length > 0) {

                        $.ajax({
                            url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/getSpecificPemiutang") %>',
                            method: "POST",
                            data: JSON.stringify({ Kod: arrKod }),
                            dataType: "json",
                            contentType: "application/json;charset=utf-8",
                            success: function (data) {
                                if (data.d) {
                                    data = JSON.parse(data.d)
                                    data.forEach(d => {
                                        let butiranAlamat = d.Butiran_Kod_Alamat
                                        try {
                                            d.Negeri_Detail = butiranAlamat.split('|')[1].split('--')[1]
                                            d.Bandar_Detail = butiranAlamat.split('|')[2].split('--')[1]
                                            d.Negara_Detail = butiranAlamat.split('|')[0].split('--')[1]

                                        }
                                        catch (e) {

                                        }

                                    })

                                    pemiutangValueCache.push(...data)
                                    pemiutangKeyCache.push(...arrKod)
                                }
                                resolve(true)

                            },
                            error: function (xhr, status, error) {
                                console.error(error);
                                reject()
                            }
                        })

                    }
                    else {
                        resolve(true)
                    }
                })
            }
            //load data into modal
            async function showInvoisData(e) {
                var target = e.target
                if (target.tagName == "TD") {
                    target = target.parentElement
                }
                show_loader()
                let invHdr = await getInvoisHdr(target.dataset.idrujukan)
                close_loader()

                $("#modalInvoisData").modal('show')
            }

            function getInvoisHdr(ID_Rujukan) {
                return new Promise(function (resolve, reject) {
                    $.ajax({
                        url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/getFullInvoisData") %>',
                        method: "POST",
                        data: JSON.stringify({
                            ID_Rujukan: ID_Rujukan

                        }),
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        success: async function (data) {
                            data = JSON.parse(data.d)
                            data = data.Payload
                            fillInvoisHdr(data.hdr[0])

                            //grand totals
                            let gJumplahPB = 0
                            let gJumlahDiskaun = 0
                            let gJumlahCukai = 0
                            let gJumlahRaw = 0

                            if (data.dtl) {

                                let arrvot = []
                                let arrpm = []
                                data.dtl.forEach(dtl => {
                                    arrvot.push(dtl.Kod_Vot)
                                    arrpm.push(dtl.Kod_Pemiutang)
                                })
                                let coaload = await searchCOAVot(arrvot).catch(function (err) {
                                    console.log(err)
                                })
                                if (!coaload) {
                                    dialogMakluman("masalah dalam mendapatkan data vot dari sistem")
                                }

                                let pmload = await searchPemiutang(arrpm).catch(function (err) {
                                    console.log(err)
                                })
                                if (!pmload) {
                                    dialogMakluman("masalah dalam mendapatkan data pemiutang dari sistem")
                                }

                                let tmpKodPemiutang = {}
                                //{kodpemiutangvalue:Jumlah_Bayarvalue}


                                data.dtl.forEach(dtl => {

                                    dtl.VOTINFO = getCOAVot(dtl.Kod_Vot, dtl.Kod_PTJ, dtl.Kod_Kump_Wang, dtl.Kod_Projek, dtl.Kod_Operasi)
                                    dtl.PMINFO = getPemiutang(dtl.Kod_Pemiutang)
                                    dtl.debit = true
                                    let jumlah_raw = readInt(dtl.Kadar_Harga) * readInt(dtl.Kuantiti_Sebenar)
                                    let jumlah_cukai = readInt(dtl.Cukai) / 100 * jumlah_raw
                                    let jumlah_diskaun = readInt(dtl.Diskaun) / 100 * jumlah_raw
                                    let Jumlah_Perlu_Bayar = jumlah_raw + jumlah_cukai - jumlah_diskaun
                                    dtl.Jumlah_Perlu_Bayar = to2Decimal(Jumlah_Perlu_Bayar)
                                    gJumlahRaw += jumlah_raw
                                    gJumplahPB += Jumlah_Perlu_Bayar
                                    gJumlahDiskaun += jumlah_diskaun
                                    gJumlahCukai += jumlah_cukai
                                    dtl.Kadar_Harga = to2Decimal(dtl.Kadar_Harga)

                                    if (tmpKodPemiutang[dtl.Kod_Pemiutang]) {
                                        //kalau ada tambah
                                        tmpKodPemiutang[dtl.Kod_Pemiutang] = parseFloat(tmpKodPemiutang[dtl.Kod_Pemiutang]) + parseFloat(dtl.Jumlah_Perlu_Bayar)
                                    }
                                    else {
                                        //kalau xde masukkan 
                                        tmpKodPemiutang[dtl.Kod_Pemiutang] = parseFloat(dtl.Jumlah_Perlu_Bayar)

                                    }
                                })
                                $("#TotalTax").val(formatPrice(gJumlahCukai))
                                $("#totalwoCukai").val(formatPrice(gJumlahRaw))
                                $("#TotalDiskaun").val(formatPrice(gJumlahDiskaun))
                                $("#total").val(formatPrice(gJumplahPB))
                                $("#stickyJumlah").html(formatPrice(gJumplahPB))
                                $("#result").html(data.dtl.length)
                                 

                            }
                            tblTransaksi.clear()
                            tblTransaksi.rows.add(data.dtl).draw()

                            resolve(true)
                        },
                        error: function (xhr, status, error) {
                            console.error(error);
                            reject(false)
                        }
                    })
                })
            }
            var cacheInvois = null
            function fillInvoisHdr(inv) {
                cacheInvois = inv
                $("#txtidinv").val(inv.ID_Rujukan)
                $("#jenByr").val((cacheJenByr[inv.Jenis_Bayar]) ? cacheJenByr[inv.Jenis_Bayar] : inv.Jenis_Bayar);
                $("#jenInv").val((cacheJenInvois[inv.Jenis_Invois]) ? cacheJenInvois[inv.Jenis_Invois] : inv.Jenis_Invois);
                $("#txtnoinv").val(inv.No_Invois)
                $("#txtnoDO").val(inv.No_DO)
                $("#tkhInv").val(dateStrFromSQl(inv.Tarikh_Invois))
                $("#tkhTerimaInv").val(dateStrFromSQl(inv.Tarikh_Terima_Invois))
                $("#tkhDO").val(dateStrFromSQl(inv.Tarikh_DO))
                $("#tkhTerimaDO").val(dateStrFromSQl(inv.Tarikh_Terima_DO))
                $("#txtTujuan").val(inv.Tujuan)
            }

            function subTabChange(e) {
                e.preventDefault()
                Array.from(e.target.parentElement.parentElement.getElementsByClassName("active")).forEach(e => {
                    e.classList.remove("active")
                })
                e.target.classList.add("active")
                console.log(e.target)
                showTab(e.target.dataset.tab)
            }

            function showTab(id) {
                $(".modal-sub-tab").hide()
                if (id == "ALL") {
                    $(".modal-sub-tab").show()
                    return

                }
                $("#" + id).show()

            }

            function getPilihanKodVot(date) {

                //assume date is already sql formatted
                return new Promise(function (resolve, reject) {

                    $.ajax({
                        url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/getKodVotBelanja") %>',
                        method: "POST",
                        data: JSON.stringify({
                            tarikh: date
                        }),
                        dataType: "json",
                        contentType: "application/json;charset=utf-8",
                        success: function (data) {
                            data = JSON.parse(data.d)
                            cachepilihanVot = []
                            cachepilihanVot.push(...data)

                            populatePilihanVot()

                            //     tbl.clear()
                            //     tbl.rows.add(data).draw()
                            resolve(true)
                        },
                        error: function (xhr, status, error) {
                            console.error(error);
                            reject(false)
                        }
                    })
                })

            }

            function populatePilihanVot() {
                let html = ''
                cachepilihanVot.forEach(v => {
                    html += '<option value="' + v.Kod_Vot + '" >' + v.Kod_Vot + " " + v.Butiran + '</option>'
                })
                $("#ddlPilihanVot").empty()
                $("#ddlPilihanVot").append(html)
                $("#ddlPilihanVot").val(cachepilihanVot[0].Kod_Vot)

            }

            ///////// senarai invois
            function loadInvoisLists(dateStart, dateEnd) {
                console.log('load invois')
                if (!dateEnd) {
                    dateEnd = ""
                }
                if (!dateStart) {
                    dateStart = ""
                }
                show_loader()
                $.ajax({
                    url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/getPPHantar") %>',
                    method: "POST",
                    data: JSON.stringify({
                        DateStart: dateStart,
                        DateEnd: dateEnd
                    }),
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        close_loader()
                        console.log('invois loaded')
                        data = JSON.parse(data.d)
                        //     tbl.clear()
                        //     tbl.rows.add(data).draw() 
                        tbl.clear()
                        tbl.rows.add(data.Payload).draw()
                    },
                    error: function (xhr, status, error) {
                        console.error(error);
                        close_loader()
                    }
                })
            }

            function dateFilterHandler(e) {
                if (e.target.value == "select") {
                    $("#specificDateFilter").show()
                }
                else {
                    $("#specificDateFilter").hide()
                }
            }

            function loadInvois() {

                if ($("#invoisDateFilter").val() == "select") {
                    let dateStart = $("#txtTarikhStart").val()
                    let dateEnd = $("#txtTarikhEnd").val()
                    if (dateStart == "") {
                        dialogMakluman("sila pilih tarikh carian")
                        return
                    }
                    loadInvoisLists(dateStart, dateEnd)
                }
                else if ($("#invoisDateFilter").val() == "all") {
                    loadInvoisLists()

                }
                else {
                    let days = $("#invoisDateFilter").val()
                    let dateString = toSqlDateString(getDateBeforeDays(days))
                    loadInvoisLists(dateString, "")
                }
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////


            function dateStrFromSQl(dateString) {
                try {

                    var dateComponents = dateString.split("T")[0].split("-");
                    var year = dateComponents[0];
                    var month = dateComponents[1];
                    var day = dateComponents[2];

                    return formattedDate = year + "-" + month + "-" + day;
                }
                catch (e) {
                    return ''
                }
            }
            function showError(errors) {
                let msg = ""
                errors.forEach(e => {
                    msg += e + "<br>"
                })
                dialogMakluman(msg)
            }


            function dialogPengesahan(msg) {
                $("#dialogConfirmContent").html(msg)
                var decision = false

                return new Promise(function (resolve) {
                    $("#modalDialogConfirm").modal('show')

                    $('#btnDialogModalConfirm').on('click', function () {
                        decision = true // Confirm button clicked
                    });
                    $('#modalDialogConfirm').on('hidden.bs.modal', function (e) {
                        resolve(decision); // Modal closed without confirming
                    });

                });
            }

            var queueMakluman = []
            function dialogMakluman(msg) {
                queueMakluman.push(msg)
                if (!$('#modalDialogInfo').hasClass('show')) {
                    showDialogMakluman();
                }

            }

            function showDialogMakluman() {

                if (queueMakluman.length > 0) {
                    var msg = queueMakluman.shift()
                    $("#dialogInfoContent").html(msg)
                    $('#modalDialogInfo').modal('show');

                    $('#modalDialogInfo').one('hidden.bs.modal', function () {
                        showDialogMakluman(); // Process the next alert after the modal is closed 
                    });
                }
            }

            function toSqlDateString(date) {
                let dd = date.getDate();
                let mm = date.getMonth() + 1; // January is 0!
                let yyyy = date.getFullYear();

                if (dd < 10) {
                    dd = '0' + dd;
                }

                if (mm < 10) {
                    mm = '0' + mm;
                }

                return yyyy + '-' + mm + '-' + dd;
            }

            function getDateBeforeDays(days) {
                let pastDate = new Date();
                pastDate.setDate(pastDate.getDate() - days);
                return pastDate;
            }

            function readInt(val) {
                try {
                    const parsedValue = parseInt(val);

                    if (!isNaN(parsedValue)) {
                        return parsedValue;
                    } else {
                        return 0;
                    }
                } catch (error) {

                    return 0;
                }
            }

            function showPenerima(e) {
                console.log('clicked', e)
                console.log(e.dataset.kp)
                showModalMaklumatPenerima(e.dataset.kp)
            }
            function showModalMaklumatPenerima(kod_pemiutang) {
                console.log('kp', kod_pemiutang)
                let pm = getPemiutang(kod_pemiutang)

                $("#modalPenerima").find('.data-penerima').each(function () {
                    $(this).html('')
                    let name = $(this).attr("name")
                    if (name) {
                        if (pm[name]) {  

                                $(this).html(pm[name]) 
                        }
                    }
                })
                $("#modalPenerima").modal("show")
            }
    
            function show_message_async(msg) {

                $("#MessageModal .modal-body").html(msg);
                var decision = false
                return new Promise(function (resolve) {

                    $('.btnYA').click(function () {
                        console.log("rreessoollvveedd")
                        decision = true
                    });
                    $("#MessageModal").on('hidden.bs.modal', function () {
                        resolve(decision);
                    });


                    $("#MessageModal").modal('show');
                })

            }
        </script>
    </contenttemplate>
</asp:Content>
