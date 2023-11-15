<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master"
    CodeBehind="Daftar_Invois.aspx.vb" Inherits="SMKB_Web_Portal.Daftar_Invois" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

        <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"
            crossorigin="anonymous"></script>


        <style>
            .nospn {
                -moz-appearance: textfield;
            }

            .nospn::-webkit-outer-spin-button,
            .nospn::-webkit-inner-spin-button {
                -webkit-appearance: none;
                margin: 0;
            }



            #permohonan .modal-body {
                max-height: 70vh;
                /* Adjust height as needed to fit your layout */
                min-height: 70vh;
                overflow-y: scroll;
                scrollbar-width: thin;
            }

            #subTab a {
                cursor: pointer;
            }

            #tblData {
                margin: 0 auto;
                border-collapse: collapse;
                table-layout: fixed;
            }

            .sticky-col-start {
                position: sticky;
                left: 0;
                box-sizing: border-box !important;
            }

            .col-ddl:focus-within {
                z-index: 1;
            }

            .sticky-col-end {
                position: sticky;
                right: 0;
                box-sizing: border-box !important;
            }

            .left-200 {
                left: 200px;
            }

            .right-50 {
                right: 50px;
            }

            .spn-ddl-dtl {
                display: none;
            }

            .ddlKodVot:focus-within .spn-ddl-dtl {
                display: inline-block;
            }

            .spn-dtl {
                width: 100%;
                cursor: pointer;
            }

            .spn-dtl::after {
                top: 0;
                font-size: .8em;
                line-height: 1em;
                vertical-align: text-top;
                content: "\f05a";
                /* Unicode for the FontAwesome icon you want to use */
                font-family: FontAwesome;
                /* Specify the font family */
                margin-left: 2px;
                /* Adjust as needed for spacing */
            }
        </style>
        <link rel="stylesheet" href="../style.css" />
        <contenttemplate>

            <div class="container-fluid mt-3">
                <ul class="nav nav-tabs" id="subTab" role="tablist">
                    <li class="nav-item" role="presentation" onclick="subTabChange(event)">
                        <a class="nav-link active" aria-current="page" data-tab="PP">Tanpa Invois</a>
                    </li>
                    <li class="nav-item" role="presentation" onclick="subTabChange(event)">
                        <a class="nav-link " tabindex="-1" data-tab="INV" aria-disabled="true">Dengan Invois</a>
                    </li>
                </ul>
            </div>

            <%--<form id="form1" runat="server">--%>
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
                    <div id="PermohonanTab" class="tabcontent" style="display: block">
                        <%-- DIV PENDAFTARAN INVOIS --%>
                            <div id="divpendaftaraninv" runat="server" visible="true">
                                <div class="modal-body">
                                    <div class="table-title">
                                        <h5>Maklumat Invois</h5>
                                        <hr>
                                        <div class="btn btn-primary btnPapar" onclick="ShowPopup('2')">
                                            Senarai Permohonan
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-3">


                                                    <div class="form-group input-group">
                                                        <input type="text"
                                                            class=" input-group__input form-control input-sm "
                                                            placeholder="" id="txtidinv" name="txtidinv" readonly />
                                                        <label class="input-group__label">ID Invois</label>
                                                    </div>

                                                    <div class="form-group input-group">
                                                        <input type="date"
                                                            class="input-group__input form-control input-sm "
                                                            placeholder="" name="tkhInv" id="tkhInv">
                                                        <label class="input-group__label">Tarikh Invois</label>
                                                    </div>

                                                </div>

                                                <div class="col-md-3">


                                                    <div class="form-group input-group">
                                                        <input type="text"
                                                            class="input-group__input form-control input-sm "
                                                            placeholder="" id="txtnoinv" name="txtnoinv" />
                                                        <label class="input-group__label">No Invois</label>
                                                    </div>
                                                    <div class="form-group input-group">
                                                        <input type="date"
                                                            class=" input-group__input form-control input-sm"
                                                            placeholder="" id="tkhTerimaInv" name="tkhTerimaInv">
                                                        <label class="input-group__label">Tarikh Terima Invois</label>
                                                    </div>



                                                </div>
                                                <div class="col-md-3">

                                                    <div class="form-group input-group">
                                                        <input type="text"
                                                            class=" input-group__input form-control input-sm"
                                                            placeholder="" id="txtnoDO" name="txtnoDO" />
                                                        <label class="input-group__label">No DO</label>
                                                    </div>


                                                    <div class="form-group input-group">
                                                        <input type="date"
                                                            class=" input-group__input form-control input-sm"
                                                            placeholder="" name="tkhDO" id="tkhDO">
                                                        <label class="input-group__label">Tarikh DO</label>
                                                    </div>

                                                </div>
                                                <div class="col-md-3">

                                                    <div class="form-group input-group">
                                                        <select class=" input-group__select ui search dropdown"
                                                            placeholder="" name="ddlJenInv" id="ddlJenInv">
                                                        </select>
                                                        <label class="input-group__label">Jenis Invois</label>
                                                    </div>

                                                    <div class="form-group input-group">
                                                        <input type="date"
                                                            class=" input-group__input form-control input-sm"
                                                            placeholder="" id="tkhTerimaDO" name="tkhTerimaDO">
                                                        <label class="input-group__label">Tarikh Terima DO</label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-9">
                                            <div class="form-group input-group">
                                                <textarea id="txtTujuan" class="input-group__input form-control"
                                                    name="message" rows="1" placeholder=""></textarea>
                                                <label class="input-group__label">Tujuan</label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">


                                            <div class="form-group input-group">
                                                <select
                                                    class=" input-group__input form-control input-sm ui search dropdown"
                                                    name="ddlJenByr" id="ddlJenByr">
                                                </select>
                                                <label class="input-group__label">Jenis Pembayaran</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="dvPenerima" style="display: none">

                                        <div class="row">
                                            <div class="col-md-9">
                                                <div class="form-group input-group">

                                                    <select class=" input-group__input ui search dropdown ddlPemiutang "
                                                        name="ddlPemiutang" id="ddlPemiutang">
                                                    </select>
                                                    <label class="input-group__label">Bayar Kepada</label>
                                                </div>
                                            </div>
                                            <div class="col-md-3">


                                                <input id="docUpload" type="file" id="fileInput" accept=".xlsx">
                                            </div>

                                        </div>
                                    </div>
                                    <hr />
                                    <div class="table-title">
                                        <h5>Maklumat Transaksi</h5>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <div class="col-md-12">
                                                    <table class="stripe row-border order-column" id="tblData"
                                                        style="width: 100%;">
                                                        <thead>
                                                            <tr style="text-align: center">
                                                                <th id="theadPenerima" style="background-color:white">
                                                                    Penerima</th>
                                                                <th style="background-color:white">Vot</th>
                                                                <th>Kod PTJ</th>
                                                                <th>Kumpulan Wang</th>
                                                                <th>Kod Operasi</th>
                                                                <th>Kod Projek</th>
                                                                <th>Perkara</th>
                                                                <th>Kuantiti</th>
                                                                <th>Harga Seunit (RM)</th>
                                                                <th>Amaun Dicukai (RM)</th>
                                                                <th>Cukai (%)</th>
                                                                <th>Diskaun (%)</th>
                                                                <th style="background-color:white">Jumlah (RM)</th>
                                                                <th style="background-color:white">Tindakan</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody id="tableID">
                                                        </tbody>
                                                    </table>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="sticky-footer">
                                    <br />
                                    <div class="form-row">
                                        <div class="form-group col-md-12">
                                            <div class="btn-group float-left">
                                                <button type="button" class="btn btn-warning btnAddRow font-weight-bold"
                                                    data-val="1" value="1" onclick="btnAddrowHandler(event)">
                                                    + Tambah</button>
                                                <button type="button"
                                                    class="btn btn-warning dropdown-toggle dropdown-toggle-split"
                                                    data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <span class="sr-only">Toggle Dropdown</span>
                                                </button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item btnAddRow five" value="5" data-val="5"
                                                        onclick="btnAddrowHandler(event)" id="btnAdd5">Tambah 5</a>
                                                    <a class="dropdown-item btnAddRow" value="10" data-val="10"
                                                        onclick="btnAddrowHandler(event)">Tambah 10</a>
                                                </div>

                                            </div>
                                            <div class="float-right">
                                                <span
                                                    style="font-family: roboto!important; font-size: 18px!important"><b>Jumlah
                                                        (<span id="result" style="margin-right: 5px">0</span> item) :RM
                                                        <span id="stickyJumlah"
                                                            style="margin-right: 5px">0.00</span></b></span>
                                                <button type="button" class="btn" id="showModalButton"><i
                                                        class="fas fa-angle-up"></i></button>
                                                <button type="button" class="btn btn-setsemula btnPadam "
                                                    onclick="btnReset()">Rekod Baru</button>
                                                <button type="button" class="btn btn-secondary btnSimpan"
                                                    onclick="SaveInvoisData()">Simpan</button>
                                                <button type="button" class="btn btn-success btnHantar"
                                                    onclick="SaveInvoisData(true)">Hantar</button>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                    </div>
                    <!-- Modal -->
                    <div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
                        aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" style="min-width: 80%" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Invois</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">

                                    <div class="search-filter">
                                        <div class="form-row justify-content-center">
                                            <div class="form-group row col-md-6">
                                                <label for="inputEmail3" class="col-sm-2 col-form-label"
                                                    style="text-align: right">Carian :</label>
                                                <div class="col-sm-8">
                                                    <div class="input-group">
                                                        <select id="invoisDateFilter" class="custom-select"
                                                            onchange="dateFilterHandler(event)">
                                                            <option value="all">SEMUA</option>
                                                            <option value="0" selected="selected">Hari Ini</option>
                                                            <option value="1">Semalam</option>
                                                            <option value="7">7 Hari Lepas</option>
                                                            <option value="30">30 Hari Lepas</option>
                                                            <option value="60">60 Hari Lepas</option>
                                                            <option value="select">Pilih Tarikh</option>
                                                        </select>
                                                        <button id="btnSearch" class="btn btnSearch btn-outline"
                                                            type="button" onclick="loadInvois()">
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
                                                            <input type="date" id="txtTarikhStart" name="txtTarikhStart"
                                                                class="form-control input-sm date-range-filter">
                                                        </div>
                                                        <div class="form-group col-md-1">
                                                        </div>
                                                        <div class="form-group col-md-1">
                                                            <label id="lblTamat"
                                                                style="text-align: right;">Tamat:</label>
                                                        </div>
                                                        <div class="form-group col-md-4">
                                                            <input type="date" id="txtTarikhEnd" name="txtTarikhEnd"
                                                                class="form-control input-sm date-range-filter">
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12 ">

                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="transaction-table table-responsive">
                                                    <table id="tblSenaraiInvois" class="table table-striped"
                                                        style="width: 99%">
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
                    </div>
                    <!--modal total-->
                    <div class="modal fade" id="detailTotal" tabindex="-1" aria-labelledby="showDetails"
                        aria-hidden="true">
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
                                            <input class="form-control  underline-input" id="totalwoCukai"
                                                name="totalwoCukai"
                                                style="text-align: right; font-size: medium; font-weight: bold"
                                                placeholder="0.00" readonly />

                                        </div>
                                    </div>
                                    <div class="row justify-content-end mt-2">
                                        <div class="col-md-6 text-right">
                                            <label class="col-form-label ">Jumlah Cukai (RM)</label>

                                        </div>
                                        <div class="col-md-6">
                                            <input class="form-control  underline-input" id="TotalTax" name="TotalTax"
                                                style="text-align: right; font-size: medium; font-weight: bold"
                                                placeholder="0.00" readonly />

                                        </div>
                                    </div>
                                    <div class="row justify-content-end mt-2">
                                        <div class="col-md-6 text-right">
                                            <label class="col-form-label ">Jumlah Diskaun (RM)</label>

                                        </div>
                                        <div class="col-md-6">
                                            <input class="form-control  underline-input" id="TotalDiskaun"
                                                name="TotalDiskaun"
                                                style="text-align: right; font-size: medium; font-weight: bold"
                                                placeholder="0.00" readonly />


                                        </div>
                                    </div>
                                    <div class="row justify-content-end mt-2">
                                        <div class="col-md-6 text-right">
                                            <label class="col-form-label ">Jumlah Sebenar (RM)</label>

                                        </div>
                                        <div class="col-md-6">
                                            <input class="form-control underline-input" id="total" name="total"
                                                style="text-align: right; font-size: medium; font-weight: bold"
                                                placeholder="0.00" readonly />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--modal total end-->
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
                                    <button type="button" class="btn btn-danger btnTidak"
                                        data-dismiss="modal">Tidak</button>
                                    <button type="button" class="btn btn-secondary btnYA" data-toggle="modal"
                                        data-target="#ModulForm" data-dismiss="modal">
                                        Ya</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Modal -->
                    <div class="modal fade" id="NotifyModal" role="dialog" tabindex="-1" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <!-- Modal content-->
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="lblNotify">Makluman</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <p id="notify"></p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Tutup</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <script type="text/javascript" src="../../../Content/js/SharedFunction.js"></script>

                    <script type="text/javascript" src="../../../Content/js/xlsx.full.min.js"></script>
                    <script type="text/javascript">

                        toNearest5Cents
                        function toggleAmaunDicukai(isVisible) {

                            tblDetails.column(9).visible(visible);

                        }

                        //// excel parsing

                        function parseExcel(inpt) {

                            console.log('parsing excl')



                            var file = inpt.target.files[0];
                            var reader = new FileReader();

                            reader.onload = function (e) {
                                const data = new Uint8Array(e.target.result);
                                const workbook = XLSX.read(data, { type: 'array' });
                                workbook.SheetNames.forEach(function (sheetName) {
                                    // parse each sheet 

                                    // parse all row into array 
                                    let allrow = XLSX.utils.sheet_to_json(workbook.Sheets[sheetName], { header: 1 })

                                    //loop through to identify row with labels
                                    let headerIndex = 0
                                    while (headerIndex < allrow.length) {

                                        //check first column 
                                        if (allrow[headerIndex].length > 0 && allrow[headerIndex][0].toString().toLowerCase().trim() == "account no.") {
                                            break;
                                        }
                                        headerIndex++;
                                    }
                                    console.log('head idx', headerIndex)

                                    var body = XLSX.utils.sheet_to_json(workbook.Sheets[sheetName], { range: headerIndex })
                                    console.log(allrow)
                                    console.log(body)
                                    //parse body data into invois details

                                    tblDetails.rows().remove().draw();
                                    body.forEach(r => {
                                        if (r['Account No.']) {
                                            let dtl = {}
                                            dtl['Butiran'] = r['Premises Location']
                                            dtl['Kadar_Harga'] = formattedPriceToFloat(r['Amount Without GST / Service Tax (ST) (RM)'])
                                            dtl['Amaun_Dicukai'] = formattedPriceToFloat(r['Tax Based Amount (RM)'])
                                            dtl['Cukai'] = roundToTwoDecimalPlaces(  (parseFloat(formattedPriceToFloat(r['GST / Service Tax (ST) Amount (RM)'])) / dtl['Amaun_Dicukai']) * 100)
                                            dtl['Kuantiti_Sebenar'] = 1
                                            dtl.Kod_PTJ = "0"
                                            dtl.Kod_Kump_Wang = "0"
                                            dtl.Kod_Operasi = "0"
                                            dtl.Kod_Projek = "0"
                                            console.log('dt',dtl)
                                            addInvoisDetails(dtl)
                                        }
                                    })
                                    console.log('parsed body')
                                   tblDetails.rows().every( function () {  
                                        let totalPrice = calculateHarga(this)
    
                                        $(this.node()).find('input[name="Jumlah"]').val(formatPrice(toNearest5Cents(totalPrice)));
                                    });
                                    calculateGrandTotalFromDT()

                                })


                            };

                            reader.readAsArrayBuffer(file);

                        }
                        //// excel parsing end


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

                        var kategoriForm = "PP"
                        var editMode = false
                        var ddlCaches = {}

                        function testFunc() {
                            //linked with test button to simulate stuffs
                            let val = $("#ddlPemiutang").dropdown("get value")
                            console.log(val)
                        }

                        function btnReset() {
                            editMode = false
                            clearForm()
                        }

                        function toggleEdit(mode) {
                            editMode = mode
                        }

                        function switchToTab(tab) {
                            console.log("swtich" + tab)
                            $("#subTab").find("[data-tab='" + tab + "']").parent().click()
                        }

                        function subTabChange(e) {
                            e.preventDefault()
                            if (editMode) {

                                show_message("Mengubah kategori akan memadam maklumat yang belum disimpan, anda pasti untuk teruskan?",
                                    function () {
                                        toggleEdit(false)
                                        changeTab(e)
                                    }
                                )
                            }
                            else {
                                changeTab(e)
                            }
                        }

                        function changeTab(e) {
                            console.log(e.target)

                            Array.from(e.target.parentElement.parentElement.getElementsByClassName("active")).forEach(e => {
                                e.classList.remove("active")
                            })
                            e.target.classList.add("active")
                            showFormTab()
                        }

                        function showFormTab() {
                            //check active tab
                            let tab = $("#subTab").find(".active")[0].dataset.tab
                            if (tab == "PP") {
                                kategoriForm = "PP"

                            }
                            if (tab == "INV") {
                                kategoriForm = "INV"

                            }

                            let column = tblDetails.column(0);

                            // Toggle the visibility
                            column.visible(kategoriForm == "PP");
                            if (kategoriForm == "PP") {

                                $("#dvPenerima").hide()
                            }
                            else if (kategoriForm == "INV") {
                                $("#dvPenerima").show()

                            }
                            clearForm()
                        }

                        function cacheDDL(id, val) {
                            ddlCaches[id] = val
                        }
                        function checkCache(id) {
                            return ddlCaches[id]
                        }
                        function readDDLCache(id) {
                            if (!ddlCaches[id])
                                return null
                            var tmp = ddlCaches[id]
                            delete ddlCaches[id];
                            return tmp
                        }

                        function btnAddrowHandler(e) {
                            try {

                                let count = parseInt(e.target.dataset.val)
                                addInvoisRowMulti(count)

                                var scrollBody = $(".dataTables_scrollBody");

                                //scroll to bottom
                                scrollBody.scrollTop(scrollBody[0].scrollHeight);
                            } catch (e) {

                            }
                        }

                        function addInvoisRowMulti(count) {

                            for (var i = 0; i < count; i++) {
                                addInvoisDetails()
                            }
                        }

                        function initDDLPemiutang() {
                            $("#ddlPemiutang").dropdown({
                                selectOnKeydown: true,
                                fullTextSearch: true,
                                apiSettings: {
                                    url: 'Transaksi_InvoisWS.asmx/GetPemiutangList?q={query}',
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

                                        let pop = $(obj).find("input").is(":focus")

                                        let ddlid = $(obj).find('select').attr("id")
                                        if (checkCache(ddlid)) {
                                            console.log('load back')
                                            $(obj).dropdown('set selected', readDDLCache(ddlid));

                                        }
                                        if (pop) {
                                            $(obj).dropdown('show');
                                        }

                                    }
                                }
                            });

                        }


                        function invoisHdrFromForm() {
                            calculateGrandTotalFromDT()
                            return {
                                ID_Rujukan: $('#txtidinv').val(),
                                No_Invois: $('#txtnoinv').val(),
                                Jenis_Invois: $('#ddlJenInv').val(),
                                Jenis_Bayar: $('#ddlJenByr').val(),
                                Tarikh_Invois: $('#tkhInv').val(),
                                Tarikh_Terima_Invois: $('#tkhTerimaInv').val(),
                                No_DO: $('#txtnoDO').val(),
                                Tarikh_DO: $('#tkhDO').val(),
                                Tarikh_Terima_DO: $('#tkhTerimaDO').val(),
                                Tujuan: $('#txtTujuan').val(),
                                Kategori_Invois: kategoriForm,
                                Jumlah_Sebenar: cacheJumlahSebenar,
                                Jumlah_Bayar: cacheJumlahSebenar
                            }
                        }

                        function invoisDetailsFromForm() {
                            var details = []
                            var allData = tblDetails.rows().data(); // Get all data in the DataTable
                            var kp
                            if (kategoriForm == "INV") {

                                kp = $("#ddlPemiutang").dropdown("get value")
                                if (kp == "" || !kp) {
                                    notification("Sila pilih penerima")
                                    return
                                }
                            }
                            allData.each(function (rowData) {
                                rowData.Kadar_Harga = formattedPriceToFloat(rowData.Kadar_Harga)
                                rowData.Amaun_Sebenar = rowData.Jumlah_Raw
                                if (rowData.Kod_Vot) {
                                    if (!rowData.ID_Rujukan) {
                                        rowData.ID_Rujukan = $("#txtidinv").val()
                                    }
                                    if (kategoriForm == "PP" && rowData.Kod_Pemiutang) {
                                        details.push(rowData)
                                    }
                                    if (kategoriForm == "INV") {
                                        rowData.Kod_Pemiutang = kp
                                        details.push(rowData)
                                    }

                                }
                            });

                            return details
                        }
                        async function SaveInvoisData(hantar = false) {
                            let invoishdr = invoisHdrFromForm()
                            console.log(invoishdr)
                            if (hantar && !invoishdr.ID_Rujukan) {

                                notification("Maklumat invois baharu perlu disimpan terlebih dahulu")
                                return
                            }
                            if (hantar) {
                                let confirm = false
                                confirm = await show_message_async("Adakah anda pasti untuk menghantar permohonan ini?")
                                console.log(confirm)
                                if (!confirm)
                                    return
                            } else {
                                let confirm = false
                                confirm = await show_message_async("Adakah anda pasti untuk menyimpan permohonan ini?")
                                console.log(confirm)
                                if (!confirm)
                                    return
                            }
                            console.log('proceed')
                            let invoisDetails = invoisDetailsFromForm()
                            invoishdr.details = invoisDetails
                            if (invoisDetails.length == 0) {
                                notification("Tiada data transaksi.")
                                return
                            }
                            console.log(invoishdr)

                            show_loader()
                            if (editMode) {
                                let txt = "simpan"
                                if (hantar) {
                                    txt = 'hantar'
                                }
                                let res = await updateInvois(invoishdr, hantar).catch(function (err) {
                                    notification("Maklumat gagal di" + txt)

                                })
                                if (!res) {

                                    close_loader()
                                    return
                                }
                                notification("Maklumat telah berjaya di" + txt)
                                if (hantar) {
                                    toggleEdit(false)
                                    clearForm()
                                }
                                else {
                                    getInvoisDetails(invoishdr.ID_Rujukan)
                                }
                            }
                            else {
                                let res = await InsertNewInvois(invoishdr).catch(function (err) {
                                    notification("Maklumat gagal disimpan.")

                                })
                                if (!res) {
                                    close_loader()
                                    return

                                }
                                notification("Maklumat berjaya disimpan.")

                            }
                            close_loader()

                        }


                        function notification(msg) {
                            $("#notify").html(msg);
                            $("#NotifyModal").modal('show');
                        }

                        //Take the category filter drop down and append it to the datatables_filter div. 
                        //You can use this same idea to move the filter anywhere withing the datatable that you want.
                        //$("#tblDataSenarai_trans_filter.dataTables_filter").append($("#categoryFilter"));

                        var tbl = null
                        var isClicked = false;
                        var tblDetails = null

                        function addInvoisDetails(data = null) {
                            var newData = {
                                "Kod_Pemiutang": '',
                                "Kod_Vot": '',
                                "Kod_PTJ": '',
                                "Kod_Kump_Wang": '',
                                "Kod_Operasi": '',
                                "Kod_Projek": '',
                                "Butiran": '',
                                "Kuantiti_Sebenar": '',
                                "Kadar_Harga": '',
                                "Amaun_Dicukai": '',
                                "Cukai": '',
                                "Diskaun": '',
                                "Jumlah": '',
                                "Tindakan": ''
                            }
                            if (data) {
                                newData = data
                                let total = newData.Kuantiti_Sebenar * newData.Kadar_Harga
                                newData.Jumlah_Raw = total
                                let totalCukai = newData.Cukai / 100 * total
                                let totalDiskaun = newData.Diskaun / 100 * total
                                newData.Jumlah = total + totalCukai - totalDiskaun
                                newData.Jumlah_Cukai = totalCukai
                                newData.Jumlah_Diskaun = totalDiskaun
                                newData.Kadar_Harga = formatPrice(newData.Kadar_Harga)
                                newData.Amaun_Dicukai = formatPrice(newData.Amaun_Dicukai)

                            }
                            tblDetails.row.add(newData).draw()

                        }

                        function calculateHarga(row) {
                            var rowData = row.data();


                            var totalPrice = NumDefault(rowData.Kuantiti_Sebenar) * (formattedPriceToFloat(rowData.Kadar_Harga) + formattedPriceToFloat(rowData.Amaun_Dicukai))
                            var amauncukai = NumDefault(rowData.Cukai) / 100
                            var total_cukai = totalPrice * amauncukai
                            if(rowData.Amaun_Dicukai){
                                total_cukai = formattedPriceToFloat(rowData.Amaun_Dicukai) * amauncukai
                            }
                            var amaundiskaun = NumDefault(rowData.Diskaun) / 100
                            var total_diskaun = totalPrice * amaundiskaun

                            //alert(amaundiskaun)
                            rowData.Jumlah_Raw = toNearest5Cents( totalPrice)
                            totalPrice = totalPrice + total_cukai - total_diskaun
                            rowData.Jumlah = toNearest5Cents(totalPrice)
                            rowData.Jumlah_Cukai = toNearest5Cents(total_cukai)
                            rowData.Jumlah_Diskaun = toNearest5Cents( total_diskaun)
                            calculateGrandTotalFromDT()

                            return totalPrice
                        }

                        var cacheJumlahSebenar = 0

                        function calculateGrandTotalFromDT() {
                            console.log('calcl total')
                            cacheJumlahSebenar = 0
                            var allData = tblDetails.rows().data(); // Get all data in the DataTable 
                            let gJumlahRaw = 0
                            let gCukai = 0
                            let gDiskaun = 0
                            let gJumlahSebenar = 0
                            let count = 0
                            allData.each(function (rowData) { 
                                    gJumlahRaw += rowData.Jumlah_Raw ? rowData.Jumlah_Raw : 0;
                                    gCukai += rowData.Jumlah_Cukai ? rowData.Jumlah_Cukai : 0;
                                    gDiskaun += rowData.Jumlah_Diskaun ? rowData.Jumlah_Diskaun : 0;
                                    gJumlahSebenar += rowData.Jumlah ? rowData.Jumlah : 0;
                                    count++; 
                            });
                            //      console.log(gJumlahRaw)
                            //      console.log(gCukai)
                            //    console.log(gDiskaun)

                            cacheJumlahSebenar = gJumlahSebenar
                            $("#totalwoCukai").val(formatPrice(gJumlahRaw))
                            $("#TotalTax").val(formatPrice(gCukai))
                            $("#TotalDiskaun").val(formatPrice(gDiskaun))
                            $("#total").val(formatPrice(gJumlahSebenar))
                            $("#stickyJumlah").html(formatPrice(gJumlahSebenar))
                            $("#result").html(count)
                        }

                        //Modal codes
                        function loadInvoisLists(dateStart, dateEnd) {
                            console.log('newing')
                            console.log(kategoriForm)
                            if (!dateEnd) {
                                dateEnd = ""
                            }
                            if (!dateStart) {
                                dateStart = ""
                            }

                            show_loader()
                            let url = '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/getInvoisDraf") %>'
                            if (kategoriForm == "PP") {
                                url = '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/getPPDraf") %>'
                                console.log(url)
                            }
                            $.ajax({
                                url: url,
                                method: "POST",
                                data: JSON.stringify({
                                    DateStart: dateStart,
                                    DateEnd: dateEnd
                                }),
                                dataType: "json",
                                contentType: "application/json;charset=utf-8",
                                success: function (data) {
                                    data = JSON.parse(data.d)
                                    //     tbl.clear()
                                    //     tbl.rows.add(data).draw() 
                                    tbl.clear()
                                    tbl.rows.add(data.Payload).draw()
                                    close_loader()
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
                                    notification("sila pilih tarikh carian")
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

                        function getDateBeforeDays(days) {
                            let pastDate = new Date();
                            pastDate.setDate(pastDate.getDate() - days);
                            return pastDate;
                        }
                        function LoadSelectedInvois(e) {
                            var target = e.target
                            while (target.tagName != "TR") {
                                target = target.parentElement
                            }

                            getInvoisDetails(target.dataset.idrujukan)
                        }
                        function getInvoisDetails(ID_Rujukan) {

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

                                    toggleEdit(false)
                                    switchToTab(data.hdr[0].Kategori_Invois)
                                    toggleEdit(true)
                                    await clearForm()
                                    if (data.hdr[0].Kategori_Invois === "INV") {
                                        cacheDDL("ddlPemiutang", data.dtl[0].Kod_Pemiutang)
                                        $("#ddlPemiutang").dropdown("search", data.dtl[0].Kod_Pemiutang)


                                    }
                                    loadHdrIntoForm(data.hdr[0])
                                    if (data.dtl) {

                                        data.dtl.forEach(d => {
                                            console.log(d)
                                            addInvoisDetails(d)
                                        })

                                    }
                                    calculateGrandTotalFromDT()
                                    $("#permohonan").modal('hide')
                                },
                                error: function (xhr, status, error) {
                                    console.error(error);
                                }
                            })
                        }
                        function loadHdrIntoForm(invhr) {
                            console.log('full hdr')
                            console.log(invhr)
                            $("#txtidinv").val(invhr.ID_Rujukan)
                            $("#txtidinv").focus()
                            $("#txtnoinv").val(invhr.No_Invois)
                            $("#txtnoinv").focus()
                            $("#txtnoDO").val(invhr.No_DO)
                            $("#txtnoDO").focus()
                            $("#txtTujuan").val(invhr.Tujuan)
                            $("#txtTujuan").focus()
                            $("#tkhInv").val(dateStrFromSQl(invhr.Tarikh_Invois))
                            $("#tkhInv").focus()
                            $("#tkhTerimaInv").val(dateStrFromSQl(invhr.Tarikh_Terima_Invois))
                            $("#tkhTerimaInv").focus()
                            $("#tkhDO").val(dateStrFromSQl(invhr.Tarikh_DO))
                            $("#tkhDO").focus()
                            $("#tkhTerimaDO").val(dateStrFromSQl(invhr.Tarikh_Terima_DO))
                            $("#tkhTerimaDO").focus()
                            $("#ddlJenInv").dropdown('set selected', invhr.Jenis_Invois)
                            $("#ddlJenByr").dropdown('set selected', invhr.Jenis_Bayar)

                        }

                        async function clearForm(emptyTable = false) {
                            $('#PermohonanTab textarea, #PermohonanTab select,#PermohonanTab input').val('');
                            $("#ddlJenInv").dropdown('clear');
                            $("#ddlJenByr").dropdown('clear');
                            $("#ddlPemiutang").dropdown('clear');
                            await initDDLJenInvois()
                            //        await initDDLJenBayar()
                            tblDetails.rows().remove().draw();
                            $(":focus").blur();
                            if (!editMode) {
                                addInvoisRowMulti(5)
                            }
                            else {

                            }

                            initializeAllDatePickerToday()
                        }
                        // modal end
                        function waitForDrawCompletion(table) {
                            return new Promise(function (resolve) {
                                table.one('draw', function () {
                                    resolve();
                                });
                            });
                        }

                        //inits
                        $(document).ready(function () {
                            jQuery.fx.off = true
                            tblDetails = $("#tblData").on('draw.dt', async function () {
                                //on draw

                                //move column vot to leftmost when is invois tab
                                if (kategoriForm == "INV") {

                                    $('.left-200').removeClass('left-200');
                                }

                                if (document.getElementsByClassName("ddlKodVot-init").length > 0) {
                                    $(".ddlKodVot-init").dropdown({
                                        fullTextSearch: true,
                                        placeholder: '',
                                        onChange: function (value, text, $selectedItem) {
                                            let sidx = (kategoriForm == "PP") ? 0 : -1
                                            var curTR = $(this).closest("tr");
                                            var tdata = tblDetails.row(curTR).data()
                                            tdata.Kod_Vot = value
                                            tdata.Kod_PTJ = $($selectedItem).data("coltambah5")
                                            tdata.Kod_Kump_Wang = $($selectedItem).data("coltambah6")
                                            tdata.Kod_Operasi = $($selectedItem).data("coltambah7")
                                            tdata.Kod_Projek = $($selectedItem).data("coltambah8")

                                            curTR.find('td:eq(' + (sidx + 2) + ')').html('<span class="spn-dtl" title="' + $($selectedItem).data("coltambah1") + '">' + $($selectedItem).data("coltambah5") + '</span>') //ptj
                                            curTR.find('td:eq(' + (sidx + 3) + ')').html('<span class="spn-dtl"  title="' + $($selectedItem).data("coltambah2") + '">' + $($selectedItem).data("coltambah6") + '</span>') //ptj
                                            curTR.find('td:eq(' + (sidx + 4) + ')').html('<span class="spn-dtl"  title="' + $($selectedItem).data("coltambah3") + '">' + $($selectedItem).data("coltambah7") + '</span>') //ptj
                                            curTR.find('td:eq(' + (sidx + 5) + ')').html('<span class="spn-dtl"  title="' + $($selectedItem).data("coltambah4") + '">' + $($selectedItem).data("coltambah8") + '</span>') //ptj 

                                            var curtd = $(this).closest("td");
                                            curtd.find('input').blur();

                                        },
                                        apiSettings: {
                                            url: 'Transaksi_InvoisWS.asmx/GetVotCOA?q={query}',
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
                                            onSuccess: function (response, element, xhr) {
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
                                                    let parts = option.text.split(/,(.+)/);

                                                    // Replace all commas with <br>
                                                    var secondPart = parts[1].replace(/,/g, '<br>');
                                                    //$(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.text));
                                                    $(objItem).append($('<div class="item" data-value="' + option.value + '" data-coltambah1="' + option.colPTJ + '" data-coltambah5="'
                                                        + option.colhidptj + '" data-coltambah2="' + option.colKW + '" data-coltambah6="' + option.colhidkw + '" data-coltambah3="' + option.colKO
                                                        + '" data-coltambah7="' + option.colhidko + '" data-coltambah4="' + option.colKp + '" data-coltambah8="' + option.colhidkp
                                                        + '" >').html(parts[0] + "<br><span class='spn-ddl-dtl'>" + secondPart + " </span>"));
                                                });

                                                // Refresh dropdown
                                                $(obj).dropdown('refresh');
                                                $(obj).dropdown('set selected', $(obj).find("select").data("selected"))


                                                let pop = $(obj).find("input").is(":focus")

                                                let ddlid = $(obj).find('select').attr("id")
                                                let curval = $(obj).find("select").data("selected")
                                                if (curval != null && curval != "") { 
                                                    $(obj).find("select").data("selected", "")
                                                    cacheDDL(ddlid, curval)
                                                    $(obj).dropdown('search', curval);
                                                }

                                                if (checkCache(ddlid)) {
                                                    let val = readDDLCache(ddlid) 
                                                    //   $(obj).dropdown('set value', val)
                                                    $(obj).dropdown('set text', val)
                                                    $(obj).dropdown('set selected', val);

                                                }
                                                if (pop) {
                                                    $(obj).dropdown('show');
                                                }

                                            }
                                        }



                                    });
                                    $(".ddlKodVot-init").dropdown('show');
                                    $(".ddlKodVot-init").dropdown('hide');
                                    $('.ddlKodVot-init').each(function (index, element) {

                                        element.classList.remove("ddlKodVot-init")
                                        // You can log any other details or manipulate the elements as needed
                                    });
                                }
                                if (document.getElementsByClassName("ddlPemiutang-init").length > 0) {

                                    $(".ddlPemiutang-init").dropdown({
                                        selectOnKeydown: true,
                                        fullTextSearch: true,
                                        placeholder: '',
                                        onChange: function (value, text, $selectedItem) {


                                            var curTR = $(this).closest("tr");
                                            var tdata = tblDetails.row(curTR).data()
                                            tdata.Kod_Pemiutang = value
                                        },
                                        apiSettings: {
                                            url: 'Transaksi_InvoisWS.asmx/GetPemiutangList?q={query}',
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
                                            onSuccess: function (response, element, xhr) {
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

                                                let pop = $(obj).find("input").is(":focus")

                                                let ddlid = $(obj).find('select').attr("id")
                                                let curval = $(obj).find("select").data("selected")
                                                if (curval != null && curval != "") {
                                                    $(obj).find("select").data("selected", "")
                                                    cacheDDL(ddlid, curval)
                                                    $(obj).dropdown('search', curval);
                                                }

                                                if (checkCache(ddlid)) {
                                                    let val = readDDLCache(ddlid)
                                                    $(obj).dropdown('set value', val)
                                                    $(obj).dropdown('set text', val)
                                                    $(obj).dropdown('set selected', val);

                                                }
                                                if (pop) {
                                                    $(obj).dropdown('show');
                                                }
                                            }
                                        }
                                    });

                                    $(".ddlPemiutang-init").dropdown('show')
                                    $(".ddlPemiutang-init").dropdown('hide')

                                    $('.ddlPemiutang-init').each(function (index, element) {

                                        element.classList.remove("ddlPemiutang-init")
                                        // You can log any other details or manipulate the elements as needed
                                    });
                                }
                            }).DataTable({
                                "searching": false, ordering: false, paging: false,


                                paging: false,
                                scrollCollapse: true,
                                scrollX: true,
                                scrollY: 300,
                                //  "sPaginationType": "full_numbers",
                                "oLanguage": {
                                    /* "oPaginate": {
                                         "sNext": '<i class="fa fa-forward"></i>',
                                         "sPrevious": '<i class="fa fa-backward"></i>',
                                         "sFirst": '<i class="fa fa-step-backward"></i>',
                                         "sLast": '<i class="fa fa-step-forward"></i>'
                                     },*/
                                    "sLengthMenu": "",
                                    "sZeroRecords": "",
                                    "sInfo": "",
                                    "sInfoEmpty": "",
                                    "sInfoFiltered": "",
                                    "sEmptyTable": "Tiada rekod."
                                },
                                "columnDefs": [
                                    { targets: [0, 1], className: 'sticky-col-start col-ddl' },
                                    { targets: [1], className: 'left-200' },
                                    { targets: [0, 1, 6], width: 200 },
                                    { targets: [2, 3, 4, 5], width: 60 },
                                    { targets: [8, 9], width: 120 },
                                    { targets: [ 12], width: 130 },
                                    { targets: [7, 10, 11], width: 80 }, 
                                    { targets: [12, 13], className: 'sticky-col-end' },
                                    { targets: [12], className: 'right-50' },
                                ],
                                "columns":
                                    [
                                        {
                                            "data": "Kod_Pemiutang",
                                            render: function (data, type, row, meta) {

                                                let id = "ddlPemiutang" + meta.row
                                                if (kategoriForm == "INV") {
                                                    return ''
                                                }
                                                return '  <select class="ui search dropdown ddlPemiutang ddlPemiutang-init" id="' + id + '" data-selected="' + data + '"></select > '

                                            }
                                        },
                                        {
                                            "data": "Kod_Vot",
                                            render: function (data, type, row, meta) {
                                                let id = "ddlKodVot" + meta.row

                                                return '  <select class="ui search dropdown ddlKodVot ddlKodVot-init"  id="' + id + '"  data-selected="' + data + '">'
                                                    + '</select > '

                                            }
                                        },
                                        { "data": "Kod_PTJ" },
                                        { "data": "Kod_Kump_Wang" },
                                        { "data": "Kod_Operasi" },
                                        { "data": "Kod_Projek" },
                                        {
                                            "data": "Butiran",
                                            render: function (data, type, row, meta) {
                                                return ' <textarea class="form-control  details" rows="1" type="text" name="Butiran">' + data + '</textarea>'
                                            }
                                        },
                                        {
                                            "data": "Kuantiti_Sebenar",
                                            render: function (data, type, row, meta) {
                                                return ' <input type="number" class="form-control nospn underline-input multi quantity" placeholder="0"  name="Kuantiti_Sebenar" style="text-align: right" value="' + data + '"/>'
                                            }
                                        },
                                        {
                                            "data": "Kadar_Harga",
                                            render: function (data, type, row, meta) {
                                                return '   <input type="text"  style="text-align: right" class="form-control nospn underline-input multi price" placeholder="0" min="0" name="Kadar_Harga"   value="' + data + '" />'
                                            }
                                        },
                                        {
                                            "data": "Amaun_Dicukai",
                                            render: function (data, type, row, meta) {
                                                return '   <input type="text"  style="text-align: right" class="form-control nospn underline-input multi price" placeholder="0" min="0" name="Amaun_Dicukai"   value="' + data + '" />'
                                            }
                                        },
                                        {
                                            "data": "Cukai",
                                            render: function (data, type, row, meta) {
                                                return '<input type="number" class="form-control nospn underline-input multi price" placeholder="0.00" min="0"   max="100" name="Cukai" style="text-align: right" value="' + data + '" />'
                                            }
                                        },
                                        {
                                            "data": "Diskaun",
                                            render: function (data, type, row, meta) {
                                                data = to2Decimal(data)
                                                return '<input type="number" class="form-control nospn  underline-input multi price" placeholder="0.00" min="0"  max="100" name="Diskaun" style="text-align: right" value="' + data + '" />'
                                            }
                                        },
                                        {
                                            "data": "Jumlah",
                                            render: function (data, type, row, meta) {
                                                return '<input type="text" class="form-control nospn  underline-input multi price" placeholder="0.00" min="0" name="Jumlah" style="text-align: right" value="' + formatPrice(data) + '" readonly />'
                                            }
                                        },
                                        {
                                            "data": "Tindakan",
                                            render: function (data, type, row, meta) {
                                                return '<button class="btn btnDelete"> <i class="fa fa-trash" style="color: red;font-size:1.5em"   ></i> </button > '
                                            }
                                        }
                                    ],
                                createdRow: function (row, data, dataIndex) {
                                    var fixedColumnend = $(row).find('.sticky-col-end');
                                    var fixedColumnstart = $(row).find('.sticky-col-start');

                                    if ($(row).hasClass('odd')) {
                                        fixedColumnstart.css({
                                            'background-color': 'white',
                                            'box-shadow': 'inset 0 0 0 9999px rgba(var(--dt-row-stripe), 0.023)'
                                        });
                                        fixedColumnend.css({
                                            'background-color': 'white',
                                            'box-shadow': 'inset 0 0 0 9999px rgba(var(--dt-row-stripe), 0.023)'
                                        });
                                    } else {
                                        fixedColumnstart.css('background-color', 'white');
                                        fixedColumnend.css('background-color', 'white');
                                    }

                                    row.dataset.noitem = data["No_Item"];

                                    console.log(data.No_Item)
                                    calculateGrandTotalFromDT()
                                }

                            });

                            $('#tblData').parent().on('scroll', function() {
                                console.log('scrolling')
                            });


                            $('#tblData').on('keyup', 'input:not(.search)', function () {

                                var row = tblDetails.row($(this).closest('tr'));
                                var rowData = row.data();
                                var keyName = $(this).attr('name');

                                let max = readInt($(this).attr('max'))
                                if (max != 0 && $(this).val() > max) {
                                    $(this).val(max)
                                }

                                rowData[keyName] = $(this).val();
                                if (keyName != "Butiran") {
                                    let totalPrice = calculateHarga(row)

                                    $(row.node()).find('input[name="Jumlah"]').val(formatPrice(totalPrice));

                                    if (keyName == "Kadar_Harga" || keyName == "Amaun_Dicukai") {

                                        formatPriceKeyUp(this)
                                    }

                                }
                            });


                            $('#tblData').on('keyup', 'textarea', function () {

                                var row = tblDetails.row($(this).closest('tr'));
                                var rowData = row.data();
                                var keyName = $(this).attr('name');
                                rowData[keyName] = $(this).val();
                            });
                            $('#tblData').on('click', '.btnDelete', async function (e) {
                                e.preventDefault()
                                var row = tblDetails.row($(this).closest('tr'));
                                var rowData = row.data()
                                // var rowData = row.data();

                                // Remove the row data from the data source
                                if (editMode) {

                                    if (rowData.No_Item && rowData.No_Item != "") {
                                        console.log('data row')

                                        let confirm = false
                                        confirm = await show_message_async("Padam Maklumat Transaksi?")
                                        console.log(confirm)
                                        if (!confirm)
                                            return
                                        let delStatus = await deleteInvoisDtl(rowData).catch(function (e) {
                                            console.log(e)
                                        })


                                        if (delStatus) {

                                            notification("Rekod transaksi Berjaya dipadam")

                                        } else {
                                            notification("Rekod transaksi gagal dipadam")
                                            return

                                        }
                                    }
                                    else {
                                        console.log('normal row')
                                    }
                                }

                                var index = tblDetails.row($(this).closest('tr')).index();
                                tblDetails.rows().data().splice(index, 1);
                                tblDetails.row().invalidate();

                                row.remove().draw(false);
                                calculateGrandTotalFromDT()
                            });



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
                                    row.onclick = LoadSelectedInvois
                                }

                            });

                            initDDLPemiutang()
                            //   showFormTab()
                            addInvoisRowMulti(5)

                            console.log("ddl finish")

                            $('#ddlJenByr').dropdown({
                                selectOnKeydown: true,
                                fullTextSearch: true,
                                placeholder: ''
                            });



                            $('#ddlJenInv').dropdown({
                                selectOnKeydown: true,
                                fullTextSearch: true,
                                placeholder: '',
                                onChange: function (value, text, $selectedItem) {

                                    //check jenis enable amuan dicukai if relatable
                                    console.log(value)

                                },
                            });
                            initDDLJenInvois()
                            initDDLJenBayar()
                            $("#ddlJenByr").dropdown('show')

                            $("#ddlJenByr").dropdown('hide')

                            initializeAllDatePickerToday()

                            document.getElementById('docUpload').addEventListener('change', parseExcel);
                            console.log("init finish")
                        });

                        function show_message_async(msg, okfn, cancelfn) {

                            $("#MessageModal .modal-body").text(msg);
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

                        function show_message(msg, okfn, cancelfn) {

                            $("#MessageModal .modal-body").text(msg);

                            $('.btnYA').click(function () {
                                if (okfn !== null && okfn !== undefined) {
                                    okfn();
                                }
                            });

                            //$('.btnTidak').click(function () {
                            //    $("#notify").html("Maklumat Tidak Berjaya Disimpan");
                            //    $("#NotifyModal").modal('show');
                            //});

                            $("#MessageModal").modal('show');
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


                        function ShowPopup(elm) {

                            //alert("test");
                            if (elm == "1") {

                                $('#permohonan').modal('toggle');


                            }
                            else if (elm == "2") {

                                $(".modal-body div").val("");
                                $('#permohonan').modal('toggle');


                                // set datepicker to empty and hide it as default state
                                $('#txtTarikhStart').val("");
                                $('#txtTarikhEnd').val("");
                                $('#divDatePicker').removeClass("d-flex").addClass("d-none");

                                // set categoryFilter to Semua as default state  
                                console.log('ssnnii')
                                tbl.clear().draw()


                                // run click event on the .btnSearch button
                                ////$('.btnSearch').click();

                            }
                        }
                        //////////////
                        function initDDLJenBayar() {

                            return new Promise(function (resolve, reject) {
                                $.ajax({
                                    url: 'Transaksi_InvoisWS.asmx/GetJenByrList?q={query}',
                                    method: "POST",
                                    data: JSON.stringify({
                                        q: ''
                                    }),
                                    dataType: "json",
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        data = JSON.parse(data.d)
                                        let list = $('#ddlJenByr')
                                        $(list).html('');
                                        let parsed = []
                                        data.forEach(d => {
                                            $(list).append($('<option class="item" data-value="' + d.value + '" value="' + d.value + '">').html(d.text));
                                        })

                                        $("#ddlJenByr").dropdown('clear');
                                        resolve(true)
                                    },

                                    error: function (xhr, status, error) {
                                        console.error(error);
                                        reject(false)
                                    }
                                })
                            })
                        }
                        function initDDLJenInvois() {
                            return new Promise(function (resolve, reject) {
                                $.ajax({
                                    url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/getJenInv") %>',
                                    method: "POST",
                                    data: JSON.stringify({
                                        q: '',
                                        kategori: kategoriForm
                                    }),
                                    dataType: "json",
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        data = JSON.parse(data.d)
                                        let list = $('#ddlJenInv')
                                        $(list).html('');
                                        let parsed = []
                                        data.forEach(d => {
                                            $(list).append($('<option class="item" data-value="' + d.value + '" value="' + d.value + '">').html(d.text));
                                        })

                                        $("#ddlJenInv").dropdown('clear');
                                        resolve(true)
                                    },
                                    error: function (xhr, status, error) {
                                        console.error(error);
                                        reject(false)
                                    }
                                })

                            })
                        }



                        function updateInvois(invois, hantar = false) {
                            return new Promise(function (resolve, reject) {
                                let url = '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/updateInvoisData") %>'
                                if (hantar) {
                                    url = '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/submitInvoisData") %>'
                                }
                                $.ajax({
                                    url: url,
                                    method: "POST",
                                    data: JSON.stringify({
                                        "invois": invois
                                    }),
                                    dataType: "json",
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
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

                        function InsertNewInvois(invois) {
                            return new Promise(function (resolve, reject) {
                                $.ajax({
                                    url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/insertInvois") %>',
                                    method: "POST",
                                    data: JSON.stringify({
                                        "invois": invois
                                    }),
                                    dataType: "json",
                                    contentType: "application/json;charset=utf-8",
                                    success: function (data) {
                                        var jsondata = JSON.parse(data.d)
                                        if (jsondata.Code == "200") {
                                            toggleEdit(false)
                                            clearForm()
                                            resolve(true)
                                        }
                                        else {
                                            reject(jsondata.Message)
                                        }
                                    }
                                })
                            })
                        }

                        function deleteInvoisDtl(dtl) {
                            return new Promise(function (resolve, reject) {

                                $.ajax({
                                    url: '<%= ResolveUrl("~/FORMS/PEMBAYARAN/Invois_WS.asmx/delInvoisDetail") %>',
                                    method: "POST",
                                    data: JSON.stringify({
                                        invoisDtl: dtl

                                    }),
                                    dataType: "json",
                                    contentType: "application/json;charset=utf-8",
                                    success: async function (data) {
                                        resolve(true)
                                    },
                                    error: function (xhr, status, error) {
                                        resolve(false)
                                    }
                                })
                            })
                        }

                    </script>
        </contenttemplate>
    </asp:Content>