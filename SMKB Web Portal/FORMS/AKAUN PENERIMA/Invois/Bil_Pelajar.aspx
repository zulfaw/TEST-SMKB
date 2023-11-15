<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Bil_Pelajar.aspx.vb" Inherits="SMKB_Web_Portal.Bil_Pelajar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>


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
            max-height: 70vh; /* Adjust height as needed to fit your layout */
            min-height: 70vh;
            overflow-y: scroll;
            scrollbar-width: thin;
        }
        #subTab a{
            cursor:pointer;
        }
   /*    #subTab .nav-link {
            font-size: 1.3em;
        }

        #subTab .active {
            background-color: #FFC83D;
            font-weight: bold;
        }

        #subTab {
            overflow: hidden;
        }*/ 
        .default-primary {
            background-color: #007bff !important;
            color: white;
        }


       /*start sticky table tbody tfoot*/
        table {
            
        }
        .secondaryContainer {
            overflow: scroll;
            border-collapse: collapse;
            height: 500px;
            border-radius: 10px;
        }

        .sticky-footer {
            position: sticky;
            bottom: 0;
            background-color: white;
            z-index: 2;
        }

        #showModalButton:hover {
            /* Add your hover styles here */
            background-color: #ffc107; /* Change background color on hover */
            color: #fff; /* Change text color on hover */
            border-color: #ffc107; /* Change border color on hover */
            cursor: pointer; /* Change cursor to indicate interactivity */
        }


         /*input CSS */
        .input-group {
            margin-bottom: 20px;
            position: relative;
        }

        .input-group__label {
            display: block;
            position: absolute;
            top: 0;
            line-height: 40px;
            color: #aaa;
            left: 5px;
            padding: 0 5px;
            transition: line-height 200ms ease-in-out, font-size 200ms ease-in-out, top 200ms ease-in-out;
            pointer-events: none;
        }

        .input-group__input {
            width: 100%;
            height: 40px;
            border: 1px solid #dddddd;
            border-radius: 5px;
            padding: 0 10px;
        }

        .input-group__input:not(:-moz-placeholder-shown) + label {
            background-color: white;
            line-height: 10px;
            opacity: 1;
            font-size: 10px;
            top: -5px;
        }

        .input-group__input:not(:-ms-input-placeholder) + label {
            background-color: white;
            line-height: 10px;
            opacity: 1;
            font-size: 10px;
            top: -5px;
        }

        .input-group__input:not(:placeholder-shown) + label, .input-group__input:focus + label {
            background-color: white;
            line-height: 10px;
            opacity: 1;
            font-size: 10px;
            top: -5px;
        }

        .input-group__input:focus {
            outline: none;
            border: 1px solid #01080D;
        }

        .input-group__input:focus + label {
            color: #01080D;
        }


        .input-group__select + label {
            background-color: white;
            line-height: 10px;
            opacity: 1;
            font-size: 10px;
            top: -5px;
        }

        .input-group__select:focus + label {
            color: #01080D;
        }

        /* Styles for the focused dropdown */
        .input-group__select:focus {
            outline: none;
            border: 1px solid #01080D;
        }


        .input-group__label-floated {
            /* Apply styles for the floating label */
            /* For example: */
            top: -5px;
            font-size: 10px;
            line-height: 10px;
            color: #01080D;
            opacity: 1;
        }
    </style>
    <contenttemplate> 
  
        <div class="container-fluid mt-3">
            <ul class="nav nav-tabs" id="subTab" role="tablist" >
                <li class="nav-item" role="presentation"  onclick="subTabChange(event)">
                    <a class="nav-link active" aria-current="page" data-tab="K"   >Berkelompok</a>
                </li> 
                <li class="nav-item" role="presentation"  onclick="subTabChange(event)">
                      <a class="nav-link " tabindex="-1"  data-tab="I" aria-disabled="true"  >Individu</a>
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
                        <h5>Maklumat Bil</h5>
                        <hr>
                        <div class="btn btn-primary btnPapar" onclick="ShowPopup('2')">
                            Senarai Bil  
                        </div>
                    </div>
                    <div id="divbilindividu" class="" style="display:none">
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <select id="ddlKategoriPenghutang" class="ui search dropdown input-group__input" name="ddlKategoriPenghutang"></select>
                                <label class="input-group__label" for="ddlKategoriPenghutang">Kategori Penghutang<i style="color: red">*</i></label>
                            </div>
                            <div class="form-group col-md-3">
                                <select class="ui search dropdown input-group__input" name="ddlPenghutang" id="ddlPenghutang"></select>
                                <label class="input-group__label" for="ddlPenghutang">Penghutang <i style="color: red">*</i></label>
                            </div>
                            <div class="form-group col-md-3">
                                <select id="ddlUrusniaga" class="ui search dropdown input-group__input" name="ddlUrusniaga"></select>
                                <label class="input-group__label" for="ddlUrusniaga">Jenis Urusniaga <i style="color: red">*</i></label>
                            </div>
                            <div class="form-group col-md-3">
                                <textarea class="input-group__input" name="txtTujuanPelajar" id="txtTujuanPelajar" maxlength="350"></textarea>
                                <label class="input-group__label" for="txtTujuanPelajar">Tujuan <i style="color: red">*</i></label>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <input type="date" class="form-control input-group__input" name="tkhBil" id="tkhBil" >
                                <label class="input-group__label" for="tkhBil">Tarikh Bil <i style="color: red">*</i> </label>
                            </div>
                            <div class="form-group col-md-3 form-inline">
                                <label class="" for="rdKontrak">Berkontrak <i style="color: red">*</i> :</label>
                                <div class="radio-btn-form d-flex " id="rdKontrak" name="rdKontrak">
                                    <div class="form-check form-check-inline">
                                        <input type="radio" id="rdYa" name="inlineRadioOptions" value="1" class="w-100" />
                                        <label class="form-check-label" for="rdYa">Ya</label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <input type="radio" id="rdTidak" name="inlineRadioOptions" value="0" class="w-100" />
                                        <label class="form-check-label" for="rdTidak">Tidak</label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-md-1">
                                <input type="number" class="form-control input-group__input" id="biltempoh" name="biltempoh" >
                                <label class="input-group__label" for="biltempoh">Tempoh</label>
                            </div>
                            <div class="form-group col-md-2">

                                <select class="ui search dropdown input-group__input" name="ddlTermBayar" id="ddlTermBayar" ></select>
                                <label class="input-group__label" for="ddlTermBayar">Jenis Tempoh</label>
                            </div>
                            <div class="form-group col-md-3" style="display: none">
                                <input type="date" class="form-control input-group__input" name="tkhMula" id="tkhMula">
                                <label class="input-group__label" for="tkhMula">Tarikh Mula </label>
                            </div>
                            <div class="form-group col-md-3">
                                <input type="date" class="form-control input-group__input" id="tkhTamat" name="tkhTamat" >
                                <label class="input-group__label" for="tkhTamat">Tarikh Tamat </label>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <input type="text" class="form-control input-group__input" id="txtnorujukan" name="txtnoinv" />
                                <label class="input-group__label" for="txtnorujukan">No. Rujukan </label>
                            </div>
                            <div class="form-group col-md-3">
                                <input type="text" class="form-control input-group__input" id="txtnobil" name="txtnobil" readonly />
                                <label class="input-group__label" for="txtnobil">No. Bil </label>
                            </div>
                            <div class="form-group col-md-3" style="display: none">
                                <textarea class="input-group__input" name="txtAlmt1" id="txtAlmt1" readonly></textarea>
                                <label class="input-group__label" for="txtAlmt1">Alamat </label>
                            </div>
                        </div>
                    </div>
                    <div id="divbilkelompok" class="">
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <select id="ddlKategoriPenghutangKelompok" class="ui search dropdown input-group__input" name="ddlKategoriPenghutangKelompok" onchange="handleInputChange(this.value,'','')"></select>
                                <label class="input-group__label" for="ddlKategoriPenghutangKelompok">Kategori Pelajar<i style="color: red">*</i></label>
                            </div>
                            <div class="form-group col-md-3">
                                <select id="ddlSesi" class="ui search dropdown input-group__input" name="ddlSesi" onchange="handleInputChange('',this.value,'')"></select>
                                <label class="input-group__label" for="ddlSesi">Sesi<i style="color: red">*</i></label>
                            </div>
                            <div class="form-group col-md-3">
                                <select id="ddlFakulti" class="ui search dropdown input-group__input" name="ddlFakulti" onchange="handleInputChange('','',this.value)"></select>
                                <label class="input-group__label" for="ddlFakulti">Fakulti<i style="color: red">*</i></label>
                            </div>
                            <div class="form-group col-md-3">
                                <select id="ddlJnsUrusniaga" class="ui search dropdown input-group__input" name="ddlJnsUrusniaga"></select>
                                <label class="input-group__label" for="ddlJnsUrusniaga">Jenis Urusniaga<i style="color: red">*</i></label>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <input type="date" class="form-control input-group__input" name="tkhBilKelompok" id="tkhBilKelompok" >
                                <label class="input-group__label" for="tkhBilKelompok">Tarikh Bil <i style="color: red">*</i> </label>
                            </div>
                            <div class="form-group col-md-3">
                                <textarea class="input-group__input" name="txtTujuanBerkelompok" id="txtTujuanBerkelompok" maxlength="350"></textarea>
                                <label class="input-group__label" for="txtTujuanBerkelompok">Tujuan <i style="color: red">*</i></label>
                            </div>
                            <div class="form-group col-md-3">
                                <input type="text" class="form-control input-group__input" id="txtnobilKelompok" name="txtnobilKelompok" readonly />
                                <label class="input-group__label" for="txtnobilKelompok">No. Bil </label>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="table-title">
                        <h5>Maklumat Transaksi</h5>
                    </div>
                    <div class="row">
                        <div id="transaksi_individu" class="col-md-12" style="display:none">
                            <div class="transaction-table table-responsive">
                                <div class="col-md-12">
                                    <table class="table table-striped" id="tblData" style="width: 100%;">
                                        <thead>
                                            <tr style="width: 100%; text-align: center">
                                                <%--<th scope="col">Penerima</th>--%>
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
                                                <th scope="col">Tindakan</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableID">
                                        </tbody>
                                    </table>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="btn-group">
                                                <button type="button" class="btn btn-warning btnAddRow font-weight-bold" data-val="1" value="1" onclick="btnAddrowHandler(event)">+ Tambah</button>
                                                <button type="button" class="btn btn-warning dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <span class="sr-only">Toggle Dropdown</span>
                                                </button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item btnAddRow five" value="5" data-val="5" onclick="btnAddrowHandler(event)" id="btnAdd5">Tambah 5</a>
                                                    <a class="dropdown-item btnAddRow" value="10" data-val="10" onclick="btnAddrowHandler(event)">Tambah 10</a>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row justify-content-end mt-2">
                                        <div class="col-md-2 text-right">
                                            <label class="col-form-label ">Jumlah (RM)</label>

                                        </div>
                                        <div class="col-md-2">
                                            <input class="form-control  underline-input" id="totalwoCukai" name="totalwoCukai" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly />

                                        </div>
                                    </div>
                                    <div class="row justify-content-end mt-2">
                                        <div class="col-md-2 text-right">
                                            <label class="col-form-label ">Jumlah Cukai (RM)</label>

                                        </div>
                                        <div class="col-md-2">
                                            <input class="form-control  underline-input" id="TotalTax" name="TotalTax" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly />

                                        </div>
                                    </div>
                                    <div class="row justify-content-end mt-2">
                                        <div class="col-md-2 text-right">
                                            <label class="col-form-label ">Jumlah Diskaun (RM)</label>

                                        </div>
                                        <div class="col-md-2">
                                            <input class="form-control  underline-input" id="TotalDiskaun" name="TotalDiskaun" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly />


                                        </div>
                                    </div>
                                    <div class="row justify-content-end mt-2">
                                        <div class="col-md-2 text-right">
                                            <label class="col-form-label ">Jumlah (RM)</label>

                                        </div>
                                        <div class="col-md-2">
                                            <input class="form-control underline-input" id="total" name="total" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly />

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div id="transaksi_kelompok" class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <div class="col-md-12">
                                    <table class="table table-striped" id="tblDataKelompok" style="width: 100%;">
                                        <thead>
                                            <tr style="width: 100%; text-align: center">
                                                <th scope="col" style="width:3%"><input type="checkbox" name="selectAll" id="selectAll" /></th>
                                                <th scope="col" style="width:20%">Nama </th>
                                                <th scope="col" style="width:20%">No. Matrik</th>
                                                <th scope="col" style="width:20%">Fakulti</th>
                                                <th scope="col" style="width:20%">Kursus</th>
                                                <th scope="col" style="width:17%">Biasiswa</th>
                                            </tr>
                                        </thead>
                                        <tbody id="tableIDKelompok">
                                            <%--<tr>
                                                <td><input type="checkbox" name="select1" id="select1" /></td>
                                                <td style="width:3%">Aina Farhanah</td>
                                                <td style="width:20%">B031910024</td>
                                                <td style="width:20%">BITS</td>
                                            </tr>--%>
                                        </tbody>
                                    </table>
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
                        <button type="button" class="btn btn-setsemula btnReset" onclick="btnReset()">Rekod Baru</button>
                        <button id="btnShow" type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Draft" onclick="SaveInvoisData()">Simpan</button>
                        <button type="button" class="btn btn-success btnHantar" data-toggle="tooltip" data-placement="bottom" title="Simpan dan Hantar" onclick="SaveInvoisData(true)">Hantar</button>
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
                        <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Bil</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">

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
                        <div class="col-md-12 ">

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
        </div>

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
                        <button type="button" class="btn btn-danger btnTidak" data-dismiss="modal">Tidak</button>
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
        <script type="text/javascript"> 
            var kategoriForm = "PP"
            var editMode = false
            var ddlCaches = {}
            var shouldPop = true;
            
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

            function handleInputChange(var1, var2, var3) {
                var var1 
                var var2
                var var3
                var KatPel = $('#ddlKategoriPenghutangKelompok').val();
                var Sesi = $('#ddlSesi').val();
                var Fakulti = $('#ddlFakulti').val();

                if (var1 !== null && var2 !== null && var3 !== null) {

                    $("#tblDataKelompok").DataTable({
                        "destroy":true,
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
                            "sSearch": "Carian: "
                        },
                        "ajax": {
                            "url": "Transaksi_PelajarWS.asmx/LoadSenaraiPelajar",
                            "method": 'POST',
                            "contentType": "application/json; charset=utf-8",
                            "dataType": "json",
                            "dataSrc": function (json) {
                                return JSON.parse(json.d);
                            },
                            "data": function () {
                                //Filter category bermula dari sini - 20 julai 2023
                                return JSON.stringify({
                                    KatPel: KatPel,
                                    Sesi: Sesi,
                                    Fakulti: Fakulti
                                })
                                //akhir sini
                            }
                        },
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
                        "drawCallback": function (settings) {
                            // Your function to be called after loading data
                            close_loader();
                        },
                        "columns": [
                            {
                                "data": "SMP01_Nomatrik",
                                render: function (data, type, row, meta) {

                                    if (type !== "display") {
                                        return data;
                                    }
                                    var link = `<input type="checkbox" id="${data}" name="${data}" id="select1" />`;
                                    return link;
                                }
                            },
                            { "data": "SMP01_Nama" },
                            { "data": "SMP01_Nomatrik" },
                            { "data": "SMP01_Fakulti" },
                            { "data": "SMP01_Kursus" },
                            { "data": "SMP01_Biasiswa" },

                        ]
                    });

                    //$("#tblDataKelompok").DataTable.destroy()
                }
            }

            function showFormTab() {
                //check active tab
                let tab = $("#subTab").find(".active")[0].dataset.tab
                if (tab == "K") {
                    kategoriForm = "K"
                    //hide single penerima
                    /*$("#dvPenerima").hide()*/
                    //show column penerima
                    //$("#tblData").find("tr").each(function () {
                    //    $(this).children().eq(0).show();
                    //});
                    //$('#tblData thead th:eq(0)').show();
                    $("#divbilkelompok").show()
                    $("#transaksi_kelompok").show()
                    $("#divbilindividu").hide()
                    $("#transaksi_individu").hide()


                }
                if (tab == "I") {
                    kategoriForm = "I"
                    //show single penerima
                    //$("#dvPenerima").show()
                    //hide column penerima
                    //$("#tblData").find("tr").each(function () {
                    //    $(this).children().eq(0).hide();
                    //});
                    //$('#tblData thead th:eq(0)').hide();
                    $("#divbilkelompok").hide()
                    $("#transaksi_kelompok").hide()
                    $("#divbilindividu").show()
                    $("#transaksi_individu").show()

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
                } catch (e) {

                }
            }

            function addInvoisRowMulti(count) {

                for (var i = 0; i < count; i++) {
                    addInvoisDetails()
                }
            }

            $('#ddlKategoriPenghutang').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                apiSettings: {
                    url: 'Transaksi_PelajarWS.asmx/GetKategoriPenghutangList?q={query}',
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

            $('#ddlKategoriPenghutangKelompok').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                apiSettings: {
                    url: 'Transaksi_PelajarWS.asmx/GetKategoriPenghutangList?q={query}',
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

            $('#ddlSesi').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                apiSettings: {
                    url: 'Transaksi_PelajarWS.asmx/GetSesiList?q={query}',
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

            $('#ddlFakulti').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                apiSettings: {
                    url: 'Transaksi_PelajarWS.asmx/GetfakultiList?q={query}',
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
            
            

            $('#ddlPenghutang').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                apiSettings: {
                    url: 'Transaksi_PelajarWS.asmx/GetPenghutangList',
                    method: 'POST',
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    cache: false,
                    beforeSend: function (settings) {
                        // Replace {query} placeholder in data with user-entered search term
                        settings.data = JSON.stringify({
                            kod: settings.urlData.query,
                            category: $('#ddlKategoriPenghutang').val()
                        });
                        //searchQuery = settings.urlData.query;
                        return settings;
                    },
                    onchange: function (settings) {
                        // Replace {query} placeholder in data with user-entered search term
                        settings.data = JSON.stringify({
                            kod: settings.urlData.query,
                            category: $('#ddlKategoriPenghutang').val()
                        });
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

            $('#ddlUrusniaga').dropdown({
                fullTextSearch: true,
                apiSettings: {
                    url: 'Transaksi_PelajarWS.asmx/GetUrusniagaList?q={query}',
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

            $('#ddlJnsUrusniaga').dropdown({
                fullTextSearch: true,
                apiSettings: {
                    url: 'Transaksi_PelajarWS.asmx/GetUrusniagaList?q={query}',
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
            

            function initDDLPemiutang() {
                $("#ddlPemiutang").dropdown({
                    selectOnKeydown: true,
                    fullTextSearch: true,
                    apiSettings: {
                        url: 'Transaksi_PelajarWS.asmx/GetPemiutangList?q={query}',
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
                if (editMode) {
                    let txt = "simpan"
                    if (hantar) {
                        txt = 'hantar'
                    }
                    let res = await updateInvois(invoishdr, hantar).catch(function (err) {
                        notification("Maklumat gagal di" + txt)

                    })
                    if (!res) {
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
                        return

                    }
                    notification("Maklumat berjaya disimpan.")

                }

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


                }
                tblDetails.row.add(newData).draw()
                console.log('add')
            }

            function calculateHarga(row) {
                var rowData = row.data();


                var totalPrice = NumDefault(rowData.Kuantiti_Sebenar) * formattedPriceToFloat (rowData.Kadar_Harga)
                var amauncukai = NumDefault(rowData.Cukai) / 100
                var total_cukai = totalPrice * amauncukai
                var amaundiskaun = NumDefault(rowData.Diskaun) / 100
                var total_diskaun = totalPrice * amaundiskaun

                //alert(amaundiskaun)
                rowData.Jumlah_Raw = totalPrice
                totalPrice = totalPrice + total_cukai - total_diskaun
                rowData.Jumlah = totalPrice
                rowData.Jumlah_Cukai = total_cukai
                rowData.Jumlah_Diskaun = total_diskaun
                calculateGrandTotalFromDT()

                return totalPrice
            }

            var cacheJumlahSebenar = 0

            function calculateGrandTotalFromDT() {
                cacheJumlahSebenar = 0
                var allData = tblDetails.rows().data(); // Get all data in the DataTable 
                let gJumlahRaw = 0
                let gCukai = 0
                let gDiskaun = 0
                let gJumlahSebenar = 0
                allData.each(function (rowData) {
                    if (rowData.Kod_Vot) {
                        gJumlahRaw += rowData.Jumlah_Raw ? rowData.Jumlah_Raw : 0;
                        gCukai += rowData.Jumlah_Cukai ? rowData.Jumlah_Cukai : 0;
                        gDiskaun += rowData.Jumlah_Diskaun ? rowData.Jumlah_Diskaun : 0;
                        gJumlahSebenar += rowData.Jumlah ? rowData.Jumlah : 0;

                    }
                });
                //      console.log(gJumlahRaw)
                //      console.log(gCukai)
                //    console.log(gDiskaun)
                cacheJumlahSebenar = gJumlahSebenar
                $("#totalwoCukai").val(formatPrice(gJumlahRaw))
                $("#TotalTax").val(formatPrice(gCukai))
                $("#TotalDiskaun").val(formatPrice(gDiskaun))
                $("#total").val(formatPrice(gJumlahSebenar))
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
                    },
                    error: function (xhr, status, error) {
                        console.error(error);
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
                $("#txtnoinv").val(invhr.No_Invois)
                $("#txtnoDO").val(invhr.No_DO)
                $("#txtTujuan").val(invhr.Tujuan)
                $("#tkhInv").val(dateStrFromSQl(invhr.Tarikh_Invois))
                $("#tkhTerimaInv").val(dateStrFromSQl(invhr.Tarikh_Terima_Invois))
                $("#tkhDO").val(dateStrFromSQl(invhr.Tarikh_DO))
                $("#tkhTerimaDO").val(dateStrFromSQl(invhr.Tarikh_Terima_DO))  
                $("#ddlJenInv").dropdown('set selected', invhr.Jenis_Invois)  
                $("#ddlJenByr").dropdown('set selected', invhr.Jenis_Bayar) 

            }
            async function clearForm(emptyTable = false) {
                $('#PermohonanTab textarea, #PermohonanTab select,#PermohonanTab input').val('');
                $("#ddlJenInv").dropdown('clear');
                $("#ddlJenByr").dropdown('clear');
                $("#ddlPemiutang").dropdown('clear'); 
               await  initDDLJenInvois()
                tblDetails.rows().remove().draw();
                $(":focus").blur();
                if (!editMode) {
                    addInvoisRowMulti(5)
                }
                else {

                }
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
                jQuery.fx.off = true;
                tblDetails = $("#tblData").on('draw.dt', function () {
                    //on draw
                    if (document.getElementsByClassName("ddlKodVot-init").length > 0) {
                        $(".ddlKodVot-init").dropdown({
                            fullTextSearch: true,
                            placeholder: '',
                            onChange: function (value, text, $selectedItem) {

                                var curTR = $(this).closest("tr");
                                var tdata = tblDetails.row(curTR).data()
                                tdata.Kod_Vot = value
                                tdata.Kod_PTJ = $($selectedItem).data("coltambah5")
                                tdata.Kod_Kump_Wang = $($selectedItem).data("coltambah6")
                                tdata.Kod_Operasi = $($selectedItem).data("coltambah7")
                                tdata.Kod_Projek = $($selectedItem).data("coltambah8")

                                curTR.find('td:eq(1)').html($($selectedItem).data("coltambah5") + " - " + $($selectedItem).data("coltambah1")) //ptj
                                curTR.find('td:eq(2)').html($($selectedItem).data("coltambah6") + " - " + $($selectedItem).data("coltambah2"))
                                curTR.find('td:eq(3)').html($($selectedItem).data("coltambah7") + " - " + $($selectedItem).data("coltambah3"))
                                curTR.find('td:eq(4)').html($($selectedItem).data("coltambah8") + " - " + $($selectedItem).data("coltambah4"))


                            },
                            apiSettings: {
                                url: 'Transaksi_PelajarWS.asmx/GetVotCOA?q={query}',
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
                                        //$(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.text));
                                        $(objItem).append($('<div class="item" data-value="' + option.value + '" data-coltambah1="' + option.colPTJ + '" data-coltambah5="'
                                            + option.colhidptj + '" data-coltambah2="' + option.colKW + '" data-coltambah6="' + option.colhidkw + '" data-coltambah3="' + option.colKO
                                            + '" data-coltambah7="' + option.colhidko + '" data-coltambah4="' + option.colKp + '" data-coltambah8="' + option.colhidkp
                                            + '" >').html(option.text));
                                    });

                                    // Refresh dropdown
                                    $(obj).dropdown('refresh');
                                    $(obj).dropdown('set selected', $(obj).find("select").data("selected"))


                                    let pop = $(obj).find("input").is(":focus")

                                    let ddlid = $(obj).find('select').attr("id")
                                    let curval = $(obj).find("select").data("selected")
                                    if (curval != null && curval != "") {
                                        console.log('first search')
                                        $(obj).find("select").data("selected", "")
                                        cacheDDL(ddlid, curval)
                                        $(obj).dropdown('search', curval);
                                    }

                                    if (checkCache(ddlid)) {
                                        let val = readDDLCache(ddlid)
                                        console.log('set ' + val)
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
                                url: 'Transaksi_PelajarWS.asmx/GetPemiutangList?q={query}',
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
                    "responsive": true,
                    "searching": false, ordering: false, paging: false,
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
                    /*  "rowCallback": function (row, data) {
                          // Add hover effect
                          $(row).hover(function () {
                              $(this).addClass("hover pe-auto bg-warning");
                          }, function () {
                              $(this).removeClass("hover pe-auto bg-warning");
                          });
                      },*/
                    "columns":
                        [
                            //{
                            //    "data": "Kod_Pemiutang",
                            //    render: function (data, type, row, meta) {

                            //        let id = "ddlPemiutang" + meta.row
                            //        if (kategoriForm == "K") {
                            //            return ''
                            //        }
                            //        return '  <select class="ui search dropdown ddlPemiutang ddlPemiutang-init" id="' + id + '" data-selected="' + data + '"></select > '

                            //    }
                            //},
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
                                    return ' <input class="form-control  details" type="text" name="Butiran" value="' + data + '" /></td>'
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
                                    return '<input type="text" class="form-control nospn  underline-input multi price" placeholder="0.00" min="0" name="Jumlah" style="text-align: right" value="' + formatPrice( data) + '" readonly />'
                                }
                            },
                            {
                                "data": "Tindakan",
                                render: function (data, type, row, meta) {
                                    return '<button class="btn btnDelete"> <i class="fa fa-trash" style="color: red;font-size:1.5em"   ></i> </button > '
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
                        { targets: 10, width: '7%' },
                        { targets: 11, width: '10%' },
                        /*{ targets: 12, width: '1%' }*/
                    ],
                    //createdRow: function (row, data, dataIndex) {
                    //    row.dataset.noitem = data["No_Item"];
                    //    if (kategoriForm == "INV") {
                    //        row.children[0].style.display = "none"
                    //    }
                    //    console.log(data.No_Item)
                    //    calculateGrandTotalFromDT()
                    //}

                });
                
                var tbl = null
                var isClicked = false;
                var isReset = false;

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
                        
                        if (keyName == "Kadar_Harga") {

                            formatPriceKeyUp(this)
                        }

                    }
                });
                $('#tblData').on('click', '.btnDelete', async  function (e) {
                    e.preventDefault()
                    var row = tblDetails.row($(this).closest('tr'));
                    var rowData  =  row.data()
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
                        { targets: [  8], className: 'text-right' }
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
                    placeholder: ''});



                $('#ddlJenInv').dropdown({ 
                    selectOnKeydown: true,
                    fullTextSearch: true,
                    placeholder: '' 
                }); 
                initDDLJenInvois()
                initDDLJenBayar()
                $("#ddlJenByr").dropdown('show')

                $("#ddlJenByr").dropdown('hide')
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
            function  initDDLJenBayar(){
                $.ajax({
                    url:'Transaksi_PelajarWS.asmx/GetJenByrList?q={query}',
                    method: "POST",
                    data: JSON.stringify({
                        q: ''
                    }),
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",
                    success: function (data) {
                        data = JSON.parse(data.d)
                        let  list  =  $('#ddlJenByr') 
                        $(list).html('');
                        let parsed = [] 
                        data.forEach(d=>{
                            $(list).append($('<option class="item" data-value="' + d.value + '" value="' + d.value + '">').html(d.text));  
                        }) 
                        
                        $("#ddlJenByr").dropdown('clear');
                    }
                }) 
            }
            function initDDLJenInvois() { 
                return new Promise(function(resolve,reject){
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
                            let  list  =  $('#ddlJenInv') 
                            $(list).html('');
                            let parsed = [] 
                            data.forEach(d=>{
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
            $('#ddlTermBayar').dropdown({
                fullTextSearch: true,
                apiSettings: {
                    url: 'InvoisWS.asmx/GetTempohBayaran?q={query}',
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
        </script>
    </contenttemplate>
</asp:Content>
