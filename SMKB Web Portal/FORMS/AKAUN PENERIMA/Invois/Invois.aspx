<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master"
CodeBehind="Invois.aspx.vb" Inherits="SMKB_Web_Portal.Invois" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>

    <contenttemplate>
    <style>
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
        <%--<form id="form1" runat="server">--%>
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
                <div id="PermohonanTab" class="tabcontent" style="display: block">
                    <%-- DIV PENDAFTARAN INVOIS --%>
                        <div id="divpendaftaraninv" runat="server" visible="true">
                            <div class="modal-body ">
                                <div class="table-title form-row">
                                    <div class="justify-content-around">
                                        <h5>Maklumat Bil</h5>
                                    </div>
                                    <div class="form-row justify-content-end" >
                                        <div class="btn btn-secondary btnTambahPenghutang">
                                            + Penghutang
                                        </div>
                                        &nbsp;
                                        <div>
                                            <button type="button" runat="server" class="btn btn-secondary btnEmel" data-placement="bottom" title="Emel" data-toggle="modal" data-target="#modalEmail">Emel</button>
                                        </div>
                                        &nbsp;
                                        <div class="btn btn-secondary btnCetak">
                                            <i class="fa fa-print" aria-hidden="true"></i>
                                            Cetak
                                        </div>
                                        &nbsp;
                                        <div class="btn btn-primary btnPapar" onclick="ShowPopup('2')">
                                            Senarai Bil
                                        </div>
                                    </div>
                                </div>
                                <hr>
                                <div class="">
                                    <div class="form-row">
                                        <div class="form-group col-md-3">
                                            <select id="ddlKategoriPenghutang" class="ui search dropdown input-group__input" name="ddlKategoriPenghutang"></select>
                                            <label class="input-group__label" for="ddlKategoriPenghutang">Kategori Penghutang<i style="color:red">*</i></label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <select class="ui search dropdown input-group__input" name="ddlPenghutang" id="ddlPenghutang"></select>
                                            <label class="input-group__label" for="ddlPenghutang">Penghutang <i style="color:red">*</i></label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <select id="ddlUrusniaga" class="ui search dropdown input-group__input" name="ddlUrusniaga"></select>
                                            <label class="input-group__label" for="ddlUrusniaga">Jenis Urusniaga <i style="color:red">*</i></label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <textarea class="input-group__input" name="txtTujuan" id="txtTujuan" maxlength="350"></textarea>
                                            <label class="input-group__label" for="txtTujuan">Tujuan <i style="color:red">*</i></label>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group col-md-3">
                                            <input type="date" class="form-control input-group__input" name="tkhBil" id="tkhBil" onchange="handleInputChange(this.value,'','','')">
                                            <label class="input-group__label" for="tkhBil">Tarikh Bil <i style="color:red">*</i> </label>
                                        </div>
                                        <div class="form-group col-md-3 form-inline">
                                            <label class="" for="rdKontrak">Berkontrak <i style="color:red">*</i> :</label>
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
                                            <input type="number" class="form-control input-group__input" id="biltempoh" name="biltempoh" onchange="handleInputChange('',this.value,'','')">
                                             <label class="input-group__label" for="biltempoh">Tempoh</label>
                                        </div>
                                        <div class="form-group col-md-2">
                                            
                                            <select class="ui search dropdown input-group__input" name="ddlTermBayar" id="ddlTermBayar" onchange="handleInputChange('','',this.value,'')"></select>
                                            <label class="input-group__label" for="ddlTermBayar">Jenis Tempoh</label>
                                        </div>
                                        <div class="form-group col-md-3" style="display:none">
                                            <input type="date" class="form-control input-group__input" name="tkhMula" id="tkhMula">
                                            <label class="input-group__label" for="tkhMula">Tarikh Mula </label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <input type="date" class="form-control input-group__input" id="tkhTamat" name="tkhTamat"  onchange="handleInputChange('','','',this.value)">
                                            <label class="input-group__label" for="tkhTamat">Tarikh Tamat </label>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group col-md-3">
                                            <input type="text" class="form-control input-group__input" id="txtnorujukan" name="txtnoinv"/>
                                            <label class="input-group__label" for="txtnorujukan">No. Rujukan </label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <input type="text" class="form-control input-group__input" id="txtnoinv" name="txtnoinv" readonly />
                                            <label class="input-group__label" for="txtnoinv">No. Bil </label>
                                        </div>
                                        <div class="form-group col-md-3" style="display:none" >
                                            <textarea class="input-group__input" name="txtAlmt1" id="txtAlmt1" readonly></textarea>
                                            <label class="input-group__label" for="txtAlmt1">Alamat </label>
                                        </div>
                                    </div>
                                </div>
                                
                                    <h5>Transaksi</h5>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="transaction-table table-responsive">
                                                <table class="table table-striped" id="tblData" style="width: 100%;">
                                                    <thead>
                                                        <tr style="width: 100%; text-align: center">
                                                            <th scope="col" style="width: 20%">Vot</th>
                                                            <th scope="col" style="width: 5%">Kumpulan Wang</th>
                                                            <th scope="col" style="width: 5%">Kod Operasi</th>
                                                            <th scope="col" style="width: 10%">Kod PTJ</th>
                                                            <th scope="col" style="width: 5%">Kod Projek</th>
                                                            <th scope="col" style="width: 15%">Perkara</th>
                                                            <th scope="col" style="width: 6%">Kuantiti</th>
                                                            <th scope="col" style="width: 8%">Harga Seunit (RM)</th>
                                                            <th scope="col" style="width: 8%">Cukai (%)</th>
                                                            <th scope="col" style="width: 8%">Diskaun (%)</th>
                                                            <th scope="col" style="width: 7%">Jumlah (RM)</th>
                                                            <th scope="col" style="width: 3%">Tindakan</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tableID">
                                                        <tr style="display: none; width: 100%">
                                                            <td>
                                                                <select class="ui search dropdown vot-carian-list"
                                                                    name="ddlVotCarian" id="ddlVotCarian">
                                                                </select>

                                                                <input type="hidden" class="data-id" name="hdid"
                                                                    id="hdid" value="" />
                                                            </td>
                                                            <td>
                                                                <label id="lblKw" name="lblKw"
                                                                    class="label-kw-list"></label>
                                                                <label id="HidlblKw" name="HidlblKw"
                                                                    class="Hid-kw-list"
                                                                    style="visibility:hidden;display: none;"></label>
                                                            </td>
                                                            <td>
                                                                <label id="lblKo" name="lblKo"
                                                                    class="label-ko-list"></label>
                                                                <label id="HidlblKo" name="HidlblKo"
                                                                    class="Hid-ko-list"
                                                                    style="visibility:hidden;display: none;"></label>
                                                            </td>
                                                            <td>
                                                                <label id="lblPTj" name="lblPTj"
                                                                    class="label-ptj-list"></label>
                                                                <label id="HidlblPTj" name="HidlblPTj"
                                                                    class="Hid-ptj-list"
                                                                    style="visibility:hidden;display: none;"></label>
                                                            </td>
                                                            <td>
                                                                <label id="lblKp" name="lblKp"
                                                                    class="label-kp-list"></label>
                                                                <label id="HidlblKp" name="HidlblKp"
                                                                    class="Hid-kp-list"
                                                                    style="visibility:hidden;display: none;"></label>
                                                            </td>
                                                            <td>
                                                                <%--<input/>--%>
                                                                <textarea class="form-control details" type="text"
                                                                    id="txtPerkara" name="txtPerkara" rows="1"></textarea>
                                                            </td>
                                                            <td>
                                                                <input type="number"
                                                                    class="form-control underline-input multi quantity"
                                                                    placeholder="0" id="quantity" name="quantity"
                                                                    style="text-align: center" />
                                                            </td>
                                                            <td>
                                                                <input type="number"
                                                                    class="form-control underline-input multi price"
                                                                    placeholder="0.00" id="price" name="price"
                                                                    style="text-align: right" />
                                                            </td>
                                                            <td>
                                                                <input type="number"
                                                                    class="form-control underline-input multi cukai"
                                                                    placeholder="0.00" id="cukai" name="cukai"
                                                                    style="text-align: center" />
                                                                <input type="number"
                                                                    class="form-control underline-input multi JUMcukai"
                                                                    placeholder="0.00" id="JUMcukai" name="JUMcukai"
                                                                    style="text-align: center; visibility: hidden;display: none;" />
                                                            </td>
                                                            <td>
                                                                <input type="number"
                                                                    class="form-control underline-input multi diskaun"
                                                                    placeholder="0.00" id="diskaun" name="diskaun"
                                                                    style="text-align: center" />
                                                                <input type="number"
                                                                    class="form-control underline-input multi JUMdiskaun"
                                                                    placeholder="0.00" id="JUMdiskaun"
                                                                    name="JUMdiskaun"
                                                                    style="text-align: center; visibility: hidden;display: none;" />
                                                            </td>
                                                            <td>
                                                                <input class="form-control underline-input amount"
                                                                    id="amount" name="amount"
                                                                    style="text-align: right" placeholder="0.00" />
                                                                <input
                                                                    class="form-control underline-input amountwocukai"
                                                                    id="amountwocukai" name="amountwocukai"
                                                                    style="text-align: right; visibility: hidden;display: none;"
                                                                    placeholder="0.00" />
                                                            </td>
                                                            <td class="tindakan">
                                                                <button class="btn btnDelete">
                                                                    <i class="fa fa-trash" style="color: red"></i>
                                                                </button>
                                                                <%--<button class="btn"><i class="fa fa-trash"></i>
                                                                    Trash</button>--%>
                                                            </td>
                                                        </tr>

                                                    </tbody>
                                                </table>

                                        <div class="sticky-footer">
                                            <br />
                                            <div class="form-row">
                                                <div class="form-group col-md-12">
                                                    <div class="btn-group float-left">
                                                        <button type="button"
                                                            class="btn btn-warning btnAddRow" data-val="1"
                                                            value="1">
                                                            <b>+ Tambah</b></button>
                                                        <button type="button"
                                                            class="btn btn-warning dropdown-toggle dropdown-toggle-split"
                                                            data-toggle="dropdown" aria-haspopup="true"
                                                            aria-expanded="false">
                                                            <span class="sr-only">Toggle Dropdown</span>
                                                        </button>
                                                        <div class="dropdown-menu">
                                                            <a class="dropdown-item btnAddRow five"
                                                                value="5" data-val="5" id="btnAdd5">Tambah
                                                                        5</a>
                                                            <a class="dropdown-item btnAddRow" value="10"
                                                                data-val="10">Tambah 10</a>
                                                        </div>

                                                    </div>
                                                    <div class="float-right">
                                                        <span style="font-family: roboto!important; font-size: 18px!important"><b>Jumlah (<span id="stickyJumlahItem" style="margin-right:5px" >0</span> item) :RM <span id="stickyJumlah" style="margin-right:5px" >0.00</span></b></span>
                                                        <button type="button" class="btn" id="showModalButton"><i class="fas fa-angle-up"></i></button>
                                                        <button type="button" class="btn btn-setsemula btnPadam ">Rekod Baru</button>
                                                        <button type="button" class="btn btn-secondary btnSimpan">Simpan</button>
                                                        <button type="button" class="btn btn-success btnHantar">Hantar</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                <!-- Modal -->
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
                                <table class="" style="width: 100%; border: none">
                                    <tr>
                                        <td class="pr-2" style="text-align: right; font-size: medium;">Jumlah<br />
                                        </td>
                                        <td style="text-align: right">
                                            <input class="form-control underline-input"
                                                id="totalwoCukai" name="totalwoCukai"
                                                style="text-align: right; font-size: medium; font-weight: bold"
                                                placeholder="0.00" readonly />
                                        </td>
                                    </tr>

                                    <tr style="border-top: none">
                                        <td class="pr-2" style="text-align: right; font-size: medium;">Jumlah
                                                Cukai</td>
                                        <td style="text-align: right">
                                            <input class="form-control underline-input"
                                                id="TotalTax" name="TotalTax"
                                                style="text-align: right; font-size: medium; font-weight: bold"
                                                placeholder="0.00" readonly />
                                        </td>
                                    </tr>

                                    <tr style="border-top: none">
                                        <td class="pr-2" style="text-align: right; font-size: medium;">Jumlah
                                                Diskaun</td>
                                        <td style="text-align: right">
                                            <input class="form-control underline-input"
                                                id="TotalDiskaun" name="TotalDiskaun"
                                                style="text-align: right; font-size: medium; font-weight: bold"
                                                placeholder="0.00" readonly />
                                        </td>
                                    </tr>
                                    
                                    <tr style="border-top: none">
                                        <td class="pr-2" style="text-align: right; font-size: medium;">Pelarasan Bundaran</td>
                                        <td style="text-align: right">
                                            <input class="form-control underline-input"
                                                id="rounding" name="rounding"
                                                style="text-align: right; font-size: medium; font-weight: bold"
                                                placeholder="0.00" readonly />
                                        </td>
                                    </tr>


                                    <tr style="border-top: none">
                                        <td class="pr-2" style="text-align: right; font-size: large">JUMLAH (RM)
                                            </td>
                                        <td style="text-align: right">
                                            <input class="form-control underline-input" id="total"
                                                name="total"
                                                style="text-align: right; font-size: medium; font-weight: bold"
                                                placeholder="0.00" readonly />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Modal -->
                <div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
                    aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Bil</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <!-- Create the dropdown filter -->
                            <div class="search-filter">
                                <div class="form-row justify-content-center">
                                    <div class="form-group row col-md-6">
                                        <label for="" class="col-sm-4 col-form-label">Tempoh Urusniaga:</label>
                                        <div class="col-sm-8">
                                            <div class="input-group">
                                                <select id="categoryFilter" class="custom-select">
                                                    <option value="" selected>Semua</option>
                                                    <option value="1">Hari Ini</option>
                                                    <option value="2">Semalam</option>
                                                    <option value="3">7 Hari Lepas</option>
                                                    <option value="4">30 Hari Lepas</option>
                                                    <option value="5">60 Hari Lepas</option>
                                                    <option value="6">Pilih Tarikh</option>
                                                </select>
                                                <div class="input-group-append">
                                                    <button id="btnSearch" runat="server" class="btn btn-outline btnSearch" type="button">
                                                        <i class="fa fa-search"></i>
                                                        Cari
                                                    </button>
                                                </div> 
                                            </div>
                                        </div>
                                        <div class="mt-4 d-none" id="divDatePicker">
                                            <div class="d-flex flex-row justify-content-around align-items-center">
                                                <div class="form-group row">
                                                    <label class="col-sm-3 col-form-label text-nowrap">Mula :</label>
                                                    <div class="col-sm-9">
                                                        <input type="date" id="txtTarikhStart" name="txtTarikhStart" class="form-control date-range-filter">
                                                    </div>
                                                </div>

                                                <div class="form-group row ml-3">
                                                    <label class="col-sm-3 col-form-label text-nowrap">Tamat :</label>
                                                    <div class="col-sm-9">
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
                                            <table id="tblDataSenarai" class="table table-striped" style="width: 99%">
                                                <thead>
                                                    <tr style="width:100%">
                                                        <th scope="col" style="width: 10%">No. Bil</th>
                                                        <th scope="col" style="width: 10%">Nama Penghutang</th>
                                                        <th scope="col" style="width: 10%">Tarikh Mohon</th>
                                                        <th scope="col" style="width: 10%">Jenis Urusniaga</th>
                                                        <th scope="col" style="width: 10%">Tujuan</th>
                                                        <th scope="col" style="width: 10%">Jumlah (RM)</th>
                                                        <th scope="col" style="width: 10%">Bil. Item</th>
                                                        <th scope="col" style="width: 9%">Status</th>
                                                        <th scope="col" style="width: 10%">Nama Penyedia</th>
                                                        <th scope="col" style="width: 10%">Tarikh Lulus</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tableID_Senarai" style="cursor:pointer;overflow:auto; ">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal -->
                <div class="modal fade" id="modalPenghutang" tabindex="-1" role="dialog"
                    aria-labelledby="exampleModalCenterTitle" aria-hidden="true" data-backdrop="static">
                    <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-lg"
                        role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="addNewPenghutangModal">Tambah Penghutang</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"
                                    id="btnCloseModal">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div class="modal-body">
                                <div class="col-md-12">
                                    <div class="row w-full">
                                        <div class="container w-full">
                                            <h6 class="font-weight-bold mb-2">Maklumat Penghutang</h6>
                                            <div class="form-group d-flex align-items-center w-full">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="txtNoPenghutang" class="form-label">No.
                                                        Akaun: </label>
                                                </div>
                                                <div class="col-6">
                                                    <input type="text" class="form-control" placeholder=""
                                                        id="txtNoPenghutang" name="txtNoPenghutang"
                                                        readonly>
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="ddlAddKategoriPenghutang"
                                                        class="form-label text-right">Kategori Penghutang: <span class="text-danger">*</span>
                                                    </label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlAddKategoriPenghutang"
                                                        class="ui search dropdown"
                                                        name="ddlAddKategoriPenghutang"></select>
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center"
                                                id="sectionId">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="txtId" class="form-label text-right"
                                                        id="lblId">No. Kad Pengenalan / No. Syarikat: 
                                                    </label>
                                                </div>
                                                <div class="col-6">
                                                    <input type="text" class="form-control" placeholder=""
                                                        id="txtId" name="txtId" autocomplete="off">
                                                </div>
                                            </div>
                                            <div class="form-group d-none align-items-center"
                                                id="sectionStaf">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="ddlNoStaf" class="form-label">No. Staf: <span class="text-danger">*</span>
                                                    </label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlNoStaf" class="ui search dropdown"
                                                        name="ddlNoStaf"></select>
                                                </div>
                                            </div>
                                            <div class="form-group d-none align-items-center"
                                                id="sectionPelajarUG">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="ddlNoMatrikUG" class="form-label">No. Matrik: <span class="text-danger">*</span>
                                                    </label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlNoMatrikUG" class="ui search dropdown"
                                                        name="ddlNoMatrikUG"></select>
                                                </div>
                                            </div>
                                            <div class="form-group d-none align-items-center"
                                                id="sectionPelajarPG">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="ddlNoMatrikPG" class="form-label">No. Matrik: <span class="text-danger">*</span>
                                                    </label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlNoMatrikPG" class="ui search dropdown"
                                                        name="ddlNoMatrikPG"></select>
                                                </div>
                                            </div>
                                            <div class="form-group d-none align-items-center"
                                                id="sectionPelajarPH">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="ddlNoMatrikPH" class="form-label">No.
                                                        Matrik:</label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlNoMatrikPH" class="ui search dropdown"
                                                        name="ddlNoMatrikPH"></select>
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="txtNama" class="form-label">Nama: <span class="text-danger">*</span></label>
                                                </div>
                                                <div class="col-8">
                                                    <input type="text" class="form-control" placeholder=""
                                                        id="txtNama" name="txtNama" autocomplete="off">
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="txtEmel" class="form-label">Emel: <span class="text-danger">*</span></label>
                                                </div>
                                                <div class="col-7">
                                                    <input type="email" class="form-control" placeholder=""
                                                        id="txtEmel" name="txtEmel" autocomplete="off">
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="txtNoTelefon" class="form-label">No. Telefon: <span class="text-danger">*</span></label>
                                                </div>
                                                <div class="col-6">
                                                    <input type="text" class="form-control" placeholder=""
                                                        id="txtNoTelefon" name="txtNoTelefon"
                                                        autocomplete="off">
                                                </div>
                                            </div>
                                            <div class="dropdown-divider"></div>
                                            <h6 class="font-weight-bold mb-2">Maklumat Alamat</h6>
                                            <div class="form-group mb-2 d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="" class="form-label">Alamat:</label>
                                                </div>
                                                <div class="col-6">
                                                    <input type="text" class="form-control" placeholder=""
                                                        id="txtAlamat1" name="txtAlamat1"
                                                        autocomplete="off">
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                </div>
                                                <div class="col-6">
                                                    <input type="text" class="form-control" placeholder=""
                                                        id="txtAlamat2" name="txtAlamat2"
                                                        autocomplete="off">
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="ddlBandar"
                                                        class="form-label">Bandar:</label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlBandar" class="ui search dropdown"
                                                        name="ddlBandar"></select>
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="txtPoskod"
                                                        class="form-label">Poskod:</label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlPoskod" class="ui search dropdown"
                                                        name="ddlPoskod"></select>
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="ddlNegeri"
                                                        class="form-label">Negeri:</label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlNegeri" class="ui search dropdown"
                                                        name="ddlNegeri"></select>
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="ddlNegara"
                                                        class="form-label">Negara:</label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlNegara" class="ui search dropdown"
                                                        name="ddlNegara"></select>
                                                </div>
                                            </div>
                                            <div class="dropdown-divider"></div>
                                            <h6 class="font-weight-bold mb-2">Maklumat Bank</h6>
                                            <div class="form-group d-flex align-items-center"
                                                id="sectionDefaultBank">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="ddlBank" class="form-label">Nama Bank:
                                                    </label>
                                                </div>
                                                <div class="col-6">
                                                    <select id="ddlBank" class="ui search dropdown"
                                                        name="ddlBank"></select>
                                                </div>
                                            </div>
                                            <div class="form-group d-none align-items-center"
                                                id="sectionStafBank">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="txtStafBankName" class="form-label">Nama
                                                        Bank: </label>
                                                </div>
                                                <div class="col-6">
                                                    <input type="text" class="form-control" placeholder=""
                                                        id="txtStafBankName" name="txtStafBankName"
                                                        autocomplete="off">
                                                </div>
                                            </div>
                                            <div class="form-group d-flex align-items-center">
                                                <div class="col-4 d-flex justify-content-end">
                                                    <label for="txtNoAkaunBank" class="form-label">No. Akaun
                                                        Bank:</label>
                                                </div>
                                                <div class="col-6">
                                                    <input type="text" class="form-control" placeholder=""
                                                        id="txtNoAkaunBank" name="txtNoAkaunBank"
                                                        autocomplete="off">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group col-md-12" align="right">
                                            <button type="button"
                                                class="btn btn-danger btnBatal">Padam</button>
                                            <button type="button" class="btn default-primary btnSimpanPenghutang">
                                                Simpan
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Confirmation Modal Tambah Penghutang -->
                <div class="modal fade" id="confirmationModal" tabindex="-1" role="dialog"
                    aria-labelledby="confirmationModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="confirmationModalLabel">Pengesahan</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                Anda pasti ingin menyimpan rekod ini?
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger"
                                    data-dismiss="modal">Tidak</button>
                                <button type="button" class="btn default-primary btnYa">Ya</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Confirmation Modal Bil -->
                <div class="modal fade" id="confirmationModalBil" tabindex="-1" role="dialog"
                    aria-labelledby="confirmationModalLabelBil" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="confirmationModalLabelBil">Pengesahan</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                Anda pasti ingin menyimpan rekod ini?
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger"
                                    data-dismiss="modal">Tidak</button>
                                <button type="button" class="btn default-primary btnYaBil">Ya</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Confirmation Modal Submit Bil -->
                <div class="modal fade" id="confirmationModalSubmitBil" tabindex="-1" role="dialog"
                    aria-labelledby="confirmationModalLabelSubmitBil" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="confirmationModalLabelSubmitBil">Pengesahan</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                Anda pasti ingin menghantar rekod ini?
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger"
                                    data-dismiss="modal">Tidak</button>
                                <button type="button" class="btn default-primary btnYaSubmitBil">Ya</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Confirmation Modal Hantar Emel -->
                <div class="modal fade" id="confirmationModalHantarEmel" tabindex="-1" role="dialog"
                    aria-labelledby="confirmationModalLabelHantarEmel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="confirmationModalLabelHantarEmel">Pengesahan</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                Anda pasti ingin menghantar emel ini?
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger"
                                    data-dismiss="modal">Tidak</button>
                                <button type="button" class="btn default-primary btnYaHantarEmel" runat="server" id="btnYaHantarEmel" name="btnYaHantarEmel">Ya</button>
                            </div>
                        </div>
                    </div>
                </div>
                
                <!-- Makluman Modal Tambah Penghutang-->
                <div class="modal fade" id="maklumanModal" tabindex="-1" role="dialog"
                    aria-labelledby="maklumanModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="maklumanModalLabel">Makluman</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <span id="detailMakluman"></span>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn default-primary" id="tutupMakluman"
                                    data-dismiss="modal">Tutup</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Makluman Modal Bil -->
                <div class="modal fade" id="maklumanModalBil" tabindex="-1" role="dialog"
                    aria-labelledby="maklumanModalLabelBil" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="maklumanModalLabelBil">Makluman</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <span id="detailMaklumanBil"></span>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn default-primary" id="tutupMaklumanBil"
                                    data-dismiss="modal">Tutup</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Makluman Modal Emel -->
                <div class="modal fade" id="maklumanModalEmel" tabindex="-1" role="dialog"
                    aria-labelledby="maklumanModalLabelEmel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="maklumanModalLabelEmel">Makluman</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <span id="detailMaklumanEmel"></span>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn default-primary" id="tutupMaklumanEmel"
                                    data-dismiss="modal">Tutup</button>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- Modal Hantar Email (Haziq 17/8/23) -->
                <div class="modal fade" id="modalEmail" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle"
                    aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title">Emel</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="form-group d-flex w-full">
                                    <div class="col-2 d-flex justify-content-end">
                                        <label for="emailBilTuntutan" class="form-label">No. Bil: </label>
                                    </div>
                                    <div class="col-8">
                                        <input type="text" class="form-control" id="emailBilTuntutan" 
                                            name="emailBilTuntutan" readonly>
                                    </div>
                                </div>
                                <div class="form-group d-flex w-full">
                                    <div class="col-2 d-flex justify-content-end">
                                        <label for="emailName" class="form-label">Kepada: </label>
                                    </div>
                                    <div class="col-8">
                                        <input type="text" class="form-control" id="emailName" name="emailName">
                                    </div>
                                </div>
                                <div class="form-group d-flex w-full">
                                    <div class="col-2 d-flex justify-content-end">
                                        <label for="emailEmail" class="form-label">Email: </label>
                                    </div>
                                    <div class="col-8">
                                        <input type="text" class="form-control" id="emailEmail" name="emailEmail">
                                    </div>
                                </div>
                                <div class="form-group d-flex w-full">
                                    <div class="col-2 d-flex justify-content-end">
                                        <label for="txtMesej" class="form-label">Mesej: </label>
                                    </div>
                                    <div class="border rounded col-8" role="textbox" contenteditable="true" id="txtMesej">
                                        <p>Assalamualaikum dan Salam Sejahtera&nbsp;<span id="lblEmailName" class="font-weight-bold"></span></p>
                                        <br>
                                        <p>Pelanggan yang dihargai,</p>
                                        <br>
                                        <p>E-mel ini adalah pemakluman mengenai Bil Tuntutan yang didaftarkan atas nama Encik / Puan
                                            (<span id="lblEmailBilTuntutan" class="font-weight-bold"></span>),</p>
                                        <br>
                                        <p>Sila jelaskan bayaran sebelum tarikh tamat. Sila abaikan e-emel ini jika bayaran telah
                                            dibuat.</p>
                                        <br>
                                        <p>Sekiranya terdapat sebarang pertanyaan, sila hubungi kami di talian +606-3316871 [Waktu
                                            Operasi 8:00 pagi hingga 5:00 petang, Isnin hingga Jumaat]</p>
                                        <br>
                                        <br><br>
                                        <p>Email dijanakan secara automatik daripada UTeM - Sistem Maklumat Kewangan Bersepadu.</p>
                                        <br>
                                        <p>Email ini tidak perlu dibalas</p>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="form-row">
                                    <div class="form-group col-md-12" align="right">
                                        <button type="button" class="btn btn-danger btnBatal" data-dismiss="modal">Batal</button>
                                        <button type="button" class="btn default-primary btnHantarEmail" id="btnHantarEmail" name="btnHantarEmail" runat="server">Hantar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


        <script>
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
                </script>

                <script type="text/javascript">
                    var fetchKodKategoriPenghutang;
                    var kodKategoriArray = [];

                    var fetchKodNegara;
                    var kodNegaraArray = [];

                    var fetchKodNegeri;
                    var kodNegeriArray = [];

                    var fetchKodPoskod;
                    var kodPoskodArray = [];

                    var fetchKodBank;
                    var kodBankArray = [];

                    var fetchNoStaff;
                    var noStaffArray = [];

                    var senaraiPenghutangData = null;

                    var totalRows = 0;
                    var totalItems = 0;

                    var selectedCategory = null;
                    function ShowPopup(elm) {

                        //alert("test" + elm);
                        if (elm == "1") {
                            $('#permohonan').modal('toggle');
                        }
                        else if (elm == "2") { // open modal and load data

                            $(".modal-body div").val("");
                            $('#permohonan').modal('toggle');

                            // set datepicker to empty and hide it as default state
                            $('#txtTarikhStart').val("");
                            $('#txtTarikhEnd').val("");
                            $('#divDatePicker').removeClass("d-flex").addClass("d-none");

                            // set categoryFilter to Semua as default state
                            $('#categoryFilter').val("");

                            // run click event on the .btnSearch button
                            $('.btnSearch').click();

                        }
                    }
                    function showDisplay(elm) {
                        //close modal
                        $('#modalEmail').modal('hide');

                        //alert("test" + elm);
                        if (elm == "Berjaya") {
                            $('#maklumanModalBil').modal('toggle');
                            $('#detailMaklumanBil').html("Emel telah dihantar.");
                        }
                        else if (elm == "Tidak Berjaya") { // open modal and load data

                            $('#maklumanModalBil').modal('toggle');
                            $('#detailMaklumanBil').html("Maaf, emel tidak berjaya dihantar.");

                        }
                    }

                    $('.btnBatal').click(function () {
                        // put timeout to prevent dropdown from showing (reset state)
                        isReset = true;
                        setTimeout(function () {
                            isReset = false;
                        }, 500);
                        clearAllFields();
                    });

                    $('.btnTambahPenghutang').click(function () {
                        // change .btnSimpan text to Simpan
                        //$('.btnSimpan').text('Hantar')
                        //$('.btnSimpan').removeClass('default-primary');
                        //$('.btnSimpan').addClass('btn-success');

                        $('#addNewPenghutangModal').text('Tambah Penghutang');

                        // show modal
                        $('#modalPenghutang').modal('show');

                        // put timeout to prevent dropdown from showing (reset state)
                        isReset = true;
                        setTimeout(function () {
                            isReset = false;
                        }, 500);

                        clearAllFields();
                    });
                    // button Batal / Modal closed / default state
                    function clearAllFields() {
                        $('#txtNoPenghutang').val('');
                        $('#ddlAddKategoriPenghutang').dropdown('clear');
                        $('#ddlAddKategoriPenghutang').dropdown('refresh');
                        $('#txtNama').val('');
                        $('#txtId').val('');
                        $('#txtNoTelefon').val('');
                        $('#txtEmel').val('');

                        $('#ddlNoStaf').dropdown('clear');
                        $('#ddlNoStaf').dropdown('refresh');
                        // $('#ddlNoStaf').empty();
                        $('#ddlNoMatrikUG').dropdown('clear');
                        $('#ddlNoMatrikUG').dropdown('refresh');
                        // $('#ddlNoMatrikUG').empty();
                        $('#ddlNoMatrikPG').dropdown('clear');
                        $('#ddlNoMatrikPG').dropdown('refresh');
                        // $('#ddlNoMatrikPG').empty();
                        $('#ddlNoMatrikPH').dropdown('clear');
                        $('#ddlNoMatrikPH').dropdown('refresh');
                        // $('#ddlNoMatrikPH').empty();

                        $('#txtAlamat1').val('');
                        $('#txtAlamat2').val('');

                        $('#ddlBandar').dropdown('clear');
                        $('#ddlBandar').empty();
                        $('#ddlPoskod').dropdown('clear');
                        $('#ddlNegeri').dropdown('clear');
                        $('#ddlNegara').dropdown('clear');

                        $('#ddlBank').dropdown('clear');
                        $('#ddlBank').empty();
                        $('#txtStafBankName').val('');
                        $('#txtNoAkaunBank').val('');
                    }

                    var tbl = null
                    var isClicked = false;
                    var isReset = false;
                    $(document).ready(function () {
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
                            "ajax": {
                                "url": "InvoisWS.asmx/LoadOrderRecord_SenaraiTransaksiInvois",
                                "method": 'POST',
                                "contentType": "application/json; charset=utf-8",
                                "dataType": "json",
                                "dataSrc": function (json) {
                                    return JSON.parse(json.d);
                                },
                                "data": function () {
                                    //Filter date bermula dari sini - 20 julai 2023
                                    var startDate = $('#txtTarikhStart').val()
                                    var endDate = $('#txtTarikhEnd').val()
                                    return JSON.stringify({
                                        category_filter: $('#categoryFilter').val(),
                                        isClicked: isClicked,
                                        tkhMula: startDate,
                                        tkhTamat: endDate
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
                                    rowClickHandler(data.No_Bil);
                                });
                            },
                            "drawCallback": function (settings) {
                                // Your function to be called after loading data
                                //close_loader();
                            },
                            "columns": [
                                {
                                    "data": "No_Bil",
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
                                { "data": "Nama_Penghutang" },
                                { "data": "Tkh_Mohon" },
                                { "data": "UrusNiaga" },
                                { "data": "Tujuan" },
                                {
                                    "data": "Jumlah",
                                    render: function (data, type, row, meta) {
                                        if (type !== "display") {

                                            return data;

                                        }
                                        var Jumlah = data.toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 });
                                        return Jumlah;
                                    }
                                },
                                { "data": "NO_ITEM" },
                                { "data": "STATUS_BIL" },
                                { "data": "Penyedia" },
                                { "data": "Tkh_Lulus" }
                            ]

                        });

                        // set btnEmel and btnCetak to hidden in first load
                        $('.btnEmel').hide();
                        $('.btnCetak').hide();
                    });

                    var searchQuery = "";
                    var oldSearchQuery = "";
                    var curNumObject = 0;
                    var tableID = "#tblData";
                    var shouldPop = true;
                    var totalID = "#total";
                    var totalCukai = "#TotalTax";
                    var totalDiskaun = "#TotalDiskaun";
                    var totalwoCukai = "#totalwoCukai";
                    var tableID_Senarai = "#tblDataSenarai";

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

                    $('.btnCetak').click(async function () {
                        var params = `scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=0,height=0,left=-1000,top=-1000`;
                        var txtbil = $('#txtnoinv').val();
                        window.open('<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Invois/CetakBil.aspx")%>?bilid=' + txtbil, '_blank', params);

                    });

                    async function AjaxLoadOrderRecord_Senarai(id) {

                        try {
                            const response = await fetch('InvoisWS.asmx/LoadOrderRecord_SenaraiTransaksiInvois', {
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

                    async function clearAllRows_senarai() {
                        $(tableID_Senarai + " > tbody > tr ").each(function (index, obj) {
                            if (index > 0) {
                                obj.remove();
                            }
                        })
                    }

                    async function paparSenarai(totalClone, objOrder) {
                        var counter = 1;
                        var table = $('#tblDataSenarai');
                        if (objOrder !== null && objOrder !== undefined) {

                            totalClone = objOrder.length;

                        }
                        //console.log(totalClone)
                        //alert("HAI")
                        while (counter <= totalClone) {


                            var row = $('#tblDataSenarai tbody>tr:first').clone();
                            row.attr("style", "");
                            var val = "";

                            $('#tblDataSenarai tbody').append(row);
                            ;
                            if (objOrder !== null && objOrder !== undefined) {

                                if (counter <= objOrder.length) {
                                    await setValueToRow(row, objOrder[counter - 1]);
                                }
                            }

                            counter += 1;
                        }
                    }

                    $('.btnSearch').click(async function () {
                        //show_loader();
                        isClicked = true;
                        tbl.ajax.reload();
                    })

                    $(function () {
                        $('.btnAddRow.five').click();
                    });

                    $('#ddlPenghutang').dropdown({
                        selectOnKeydown: true,
                        fullTextSearch: true,
                        apiSettings: {
                            url: 'InvoisWS.asmx/GetPenghutangList',
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

                    $('#ddlKategoriPenghutang').dropdown({
                        selectOnKeydown: true,
                        fullTextSearch: true,
                        apiSettings: {
                            url: 'InvoisWS.asmx/GetKategoriPenghutangList?q={query}',
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

                    $('#ddlAddKategoriPenghutang').dropdown({
                        selectOnKeydown: true,
                        fullTextSearch: true,
                        apiSettings: {
                            url: 'InvoisWS.asmx/GetKategoriPenghutangList?q={query}',
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

                    // get value from ddlKategoriPenghutang to display section based on selection
                    $('#ddlAddKategoriPenghutang').on('change', async function () {
                        if (!isReset) {
                            // kategori
                            var selectedValue = $(this).val();

                            if (selectedValue === "ST") { // if Staf
                                // hide not necessary section
                                $('#sectionId').removeClass('d-flex');
                                $('#sectionId').addClass('d-none');
                                $('#sectionPelajarUG').removeClass('d-flex');
                                $('#sectionPelajarUG').addClass('d-none');
                                $('#sectionPelajarPG').removeClass('d-flex');
                                $('#sectionPelajarPG').addClass('d-none');
                                $('#sectionPelajarPH').removeClass('d-flex');
                                $('#sectionPelajarPH').addClass('d-none');

                                $('#sectionDefaultBank').removeClass('d-flex');
                                $('#sectionDefaultBank').addClass('d-none');

                                // show necessary section
                                $('#sectionStaf').removeClass('d-none');
                                $('#sectionStaf').addClass('d-flex');

                                $('#ddlNoStaf').dropdown('clear');
                                $('#ddlNoStaf').dropdown('refresh');

                                $('#sectionStafBank').removeClass('d-none');
                                $('#sectionStafBank').addClass('d-flex');

                            } else if (selectedValue === "PL") { // if Pelajar UG
                                // hide not necessary section
                                $('#sectionId').removeClass('d-flex');
                                $('#sectionId').addClass('d-none');
                                $('#sectionStaf').removeClass('d-flex');
                                $('#sectionStaf').addClass('d-none');
                                $('#sectionPelajarPG').removeClass('d-flex');
                                $('#sectionPelajarPG').addClass('d-none');
                                $('#sectionPelajarPH').removeClass('d-flex');
                                $('#sectionPelajarPH').addClass('d-none');

                                $('#sectionStafBank').removeClass('d-flex');
                                $('#sectionStafBank').addClass('d-none');

                                // show necessary section
                                $('#sectionPelajarUG').removeClass('d-none');
                                $('#sectionPelajarUG').addClass('d-flex');

                                $('#ddlNoMatrikUG').dropdown('clear');
                                $('#ddlNoMatrikUG').dropdown('refresh');

                                $('#sectionDefaultBank').removeClass('d-none');
                                $('#sectionDefaultBank').addClass('d-flex');

                            } else if (selectedValue == "PG") { // if Pelajar PG
                                // hide not necessary section
                                $('#sectionId').removeClass('d-flex');
                                $('#sectionId').addClass('d-none');
                                $('#sectionStaf').removeClass('d-flex');
                                $('#sectionStaf').addClass('d-none');
                                $('#sectionPelajarUG').removeClass('d-flex');
                                $('#sectionPelajarUG').addClass('d-none');
                                $('#sectionPelajarPH').removeClass('d-flex');
                                $('#sectionPelajarPH').addClass('d-none');

                                $('#sectionStafBank').removeClass('d-flex');
                                $('#sectionStafBank').addClass('d-none');

                                // show necessary section
                                $('#sectionPelajarPG').removeClass('d-none');
                                $('#sectionPelajarPG').addClass('d-flex');

                                $('#ddlNoMatrikPG').dropdown('clear');
                                $('#ddlNoMatrikPG').dropdown('refresh');

                                $('#sectionDefaultBank').removeClass('d-none');
                                $('#sectionDefaultBank').addClass('d-flex');

                            } else if (selectedValue == "PH") { // if Pelajar PH / same with UG
                                // hide not necessary section
                                $('#sectionId').removeClass('d-flex');
                                $('#sectionId').addClass('d-none');
                                $('#sectionStaf').removeClass('d-flex');
                                $('#sectionStaf').addClass('d-none');
                                $('#sectionPelajarPG').removeClass('d-flex');
                                $('#sectionPelajarPG').addClass('d-none');
                                $('#sectionPelajarPH').removeClass('d-flex');
                                $('#sectionPelajarPH').addClass('d-none');

                                $('#sectionStafBank').removeClass('d-flex');
                                $('#sectionStafBank').addClass('d-none');

                                // show necessary section
                                $('#sectionPelajarUG').removeClass('d-none');
                                $('#sectionPelajarUG').addClass('d-flex');

                                $('#ddlNoMatrikUG').dropdown('clear');
                                $('#ddlNoMatrikUG').dropdown('refresh');

                                $('#sectionDefaultBank').removeClass('d-none');
                                $('#sectionDefaultBank').addClass('d-flex');

                            } else if (selectedValue === "OA") { // if Orang Awam
                                // hide not necessary section
                                $('#sectionNoStaf').removeClass('d-flex');
                                $('#sectionNoStaf').addClass('d-none');
                                $('#sectionStaf').removeClass('d-flex');
                                $('#sectionStaf').addClass('d-none');
                                $('#sectionPelajarUG').removeClass('d-flex');
                                $('#sectionPelajarUG').addClass('d-none');
                                $('#sectionPelajarPG').removeClass('d-flex');
                                $('#sectionPelajarPG').addClass('d-none');
                                $('#sectionPelajarPH').removeClass('d-flex');
                                $('#sectionPelajarPH').addClass('d-none');

                                $('#sectionStafBank').removeClass('d-flex');
                                $('#sectionStafBank').addClass('d-none');

                                // show necessary section
                                $('#sectionId').removeClass('d-none');
                                $('#sectionId').addClass('d-flex');
                                $('#lblId').html("No. Kad Pengenalan");

                                $('#sectionDefaultBank').removeClass('d-none');
                                $('#sectionDefaultBank').addClass('d-flex');

                            } else if (selectedValue == "SY") { // if Syarikat
                                // hide not necessary section
                                $('#sectionNoStaf').removeClass('d-flex');
                                $('#sectionNoStaf').addClass('d-none');
                                $('#sectionStaf').removeClass('d-flex');
                                $('#sectionStaf').addClass('d-none');
                                $('#sectionPelajarUG').removeClass('d-flex');
                                $('#sectionPelajarUG').addClass('d-none');
                                $('#sectionPelajarPG').removeClass('d-flex');
                                $('#sectionPelajarPG').addClass('d-none');
                                $('#sectionPelajarPH').removeClass('d-flex');
                                $('#sectionPelajarPH').addClass('d-none');

                                $('#sectionStafBank').removeClass('d-flex');
                                $('#sectionStafBank').addClass('d-none');

                                // show necessary section
                                $('#sectionId').removeClass('d-none');
                                $('#sectionId').addClass('d-flex');
                                $('#lblId').html("No. Syarikat");

                                $('#sectionDefaultBank').removeClass('d-none');
                                $('#sectionDefaultBank').addClass('d-flex');

                            } else if (selectedValue == "") { // default state
                                // hide not necessary section
                                $('#sectionNoStaf').removeClass('d-flex');
                                $('#sectionNoStaf').addClass('d-none');
                                $('#sectionStaf').removeClass('d-flex');
                                $('#sectionStaf').addClass('d-none');
                                $('#sectionPelajarUG').removeClass('d-flex');
                                $('#sectionPelajarUG').addClass('d-none');
                                $('#sectionPelajarPG').removeClass('d-flex');
                                $('#sectionPelajarPG').addClass('d-none');
                                $('#sectionPelajarPH').removeClass('d-flex');
                                $('#sectionPelajarPH').addClass('d-none');

                                $('#sectionStafBank').removeClass('d-flex');
                                $('#sectionStafBank').addClass('d-none');

                                // show necessary section
                                $('#sectionId').removeClass('d-none');
                                $('#sectionId').addClass('d-flex');
                                $('#lblId').html("No. Kad Pengenalan / No. Syarikat");

                                $('#sectionDefaultBank').removeClass('d-none');
                                $('#sectionDefaultBank').addClass('d-flex');
                            }
                        }
                    });

                    // get value from ddlNoStaf for auto-select No Staff
                    $('#ddlNoStaf').on('change', async function () {
                        if (!isReset) {
                            // kategori id format XXYYZZ
                            var selectedValue = $(this).val();

                            // make ajax call to get data from server
                            var staffPromise = GetStaffValue(selectedValue);

                            staffPromise.then(function (staf) {
                                if (staf && staf.length > 0) {
                                    $('#txtNama').val(staf[0].MS01_Nama);
                                    $('#txtNoTelefon').val(staf[0].MS01_NoTelBimbit);
                                    $('#txtEmel').val(staf[0].MS01_Email);
                                    $('#txtAlamat1').val(staf[0].MS01_AlamatSurat1);
                                    $('#txtAlamat2').val(staf[0].MS01_AlamatSurat2);

                                    $('#ddlBandar').dropdown('clear');
                                    if (staf[0].MS01_BandarSurat !== null) {
                                        $('#ddlBandar').empty();
                                        var bandarPromise = GetBandarValueMapping(staf[0].MS01_BandarSurat);
                                        bandarPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var value = result[0].value;
                                                var option = $('<option>').attr('value', value).text(text);
                                                $('#ddlBandar').append(option);
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlBandar').dropdown('clear');
                                    }

                                    $('#ddlPoskod').dropdown('clear');
                                    if (staf[0].MS01_PoskodSurat !== null) {
                                        $('#ddlPoskod').empty();
                                        var poskodPromise = GetPoskodValue(staf[0].MS01_PoskodSurat);
                                        poskodPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var option = $('<option>').attr('value', staf[0].MS01_PoskodSurat).text(text);
                                                $('#ddlPoskod').append(option);
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlPoskod').dropdown('clear');
                                    }

                                    $('#ddlNegeri').dropdown('clear');
                                    if (staf[0].MS01_NegeriSurat !== null) {
                                        $('#ddlNegeri').empty();
                                        var negeriPromise = GetNegeriValue(staf[0].MS01_NegeriSurat);
                                        negeriPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var option = $('<option>').attr('value', staf[0].MS01_NegeriSurat).text(text);
                                                $('#ddlNegeri').append(option);

                                                $('#ddlNegara').empty();
                                                var textNegara = "Malaysia"
                                                var valueNegara = "MY"
                                                var option = $('<option>').attr('value', valueNegara).text(textNegara);
                                                $('#ddlNegara').append(option);
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlNegeri').dropdown('clear');
                                    }

                                    $('#txtStafBankName').val(staf[0].NamaBank);
                                    $('#txtNoAkaunBank').val(staf[0].MS01_NoAkaun);
                                }
                            }).catch(function (error) {
                                console.error('Error:', error);
                            });
                        }
                    });

                    // get value from ddlNoMatrikUG for auto-select No Matrik UG
                    $('#ddlNoMatrikUG').on('change', async function () {
                        if (!isReset) {
                            // kategori id format XXYYZZ
                            var selectedValue = $(this).val();

                            // make ajax call to get data from server
                            var pelajarUG = GetPelajarUGValue(selectedValue);

                            pelajarUG.then(function (pelajar) {
                                if (pelajar && pelajar.length > 0) {
                                    $('#txtNama').val(pelajar[0].SMP01_Nama);
                                    $('#txtNoTelefon').val(pelajar[0].SMP01_NoTelBimBit);
                                    $('#txtEmel').val(pelajar[0].SMP01_Emel);
                                    $('#txtAlamat1').val(pelajar[0].SMP01_Alamat1);
                                    $('#txtAlamat2').val(pelajar[0].SMP01_Alamat2);

                                    $('#ddlBandar').dropdown('clear');
                                    if (pelajar[0].SMP01_Bandar !== null) {
                                        $('#ddlBandar').empty();
                                        var bandarPromise = GetBandarValue(pelajar[0].SMP01_Bandar);
                                        bandarPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var value = result[0].value;
                                                var option = $('<option>').attr('value', value).text(text);
                                                $('#ddlBandar').append(option);

                                                // pass value of selected bandar to update negeri and negara
                                                updateNegeriNegaraByBandar(value);
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlBandar').dropdown('clear');
                                    }

                                    $('#ddlPoskod').dropdown('clear');
                                    if (pelajar[0].SMP01_Poskod !== null) {
                                        $('#ddlPoskod').empty();
                                        var poskodPromise = GetPoskodValue(pelajar[0].SMP01_Poskod);
                                        poskodPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var option = $('<option>').attr('value', pelajar[0].SMP01_Poskod).text(text);
                                                $('#ddlPoskod').append(option);
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlPoskod').dropdown('clear');
                                    }

                                    // bank
                                    if (pelajar[0].SMP01_Bank !== null) {
                                        $('#ddlBank').empty()

                                        var bankPromise = GetBankValue(pelajar[0].SMP01_Bank);
                                        bankPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var value = result[0].value;
                                                var label = value + " - " + text;
                                                var option = $('<option>').attr('value', value).text(label);
                                                $('#ddlBank').append(option);
                                            } else {
                                                $('#ddlBank').dropdown('clear');
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlBank').dropdown('clear');
                                    }

                                    $('#txtNoAkaunBank').val(pelajar[0].SMP01_NoAkaun)
                                }
                            }).catch(function (error) {
                                console.error('Error:', error);
                            });
                        }
                    });

                    // get value from ddlNoMatrikUG for auto-select No Matrik PH (same with UG)
                    $('#ddlNoMatrikPH').on('change', async function () {
                        if (!isReset) {
                            // kategori id format XXYYZZ
                            var selectedValue = $(this).val();

                            // make ajax call to get data from server
                            var pelajarUG = GetPelajarUGValue(selectedValue);

                            pelajarUG.then(function (pelajar) {
                                if (pelajar && pelajar.length > 0) {
                                    $('#txtNama').val(pelajar[0].SMP01_Nama);
                                    $('#txtNoTelefon').val(pelajar[0].SMP01_NoTelBimBit);
                                    $('#txtEmel').val(pelajar[0].SMP01_Emel);
                                    $('#txtAlamat1').val(pelajar[0].SMP01_Alamat1);
                                    $('#txtAlamat2').val(pelajar[0].SMP01_Alamat2);

                                    $('#ddlBandar').dropdown('clear');
                                    if (pelajar[0].SMP01_Bandar !== null) {
                                        $('#ddlBandar').empty();
                                        var bandarPromise = GetBandarValue(pelajar[0].SMP01_Bandar);
                                        bandarPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var value = result[0].value;
                                                var option = $('<option>').attr('value', value).text(text);
                                                $('#ddlBandar').append(option);

                                                // pass value of selected bandar to update negeri and negara
                                                updateNegeriNegaraByBandar(value);
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlBandar').dropdown('clear');
                                    }

                                    $('#ddlPoskod').dropdown('clear');
                                    if (pelajar[0].SMP01_Poskod !== null) {
                                        $('#ddlPoskod').empty();
                                        var poskodPromise = GetPoskodValue(pelajar[0].SMP01_Poskod);
                                        poskodPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var option = $('<option>').attr('value', pelajar[0].SMP01_Poskod).text(text);
                                                $('#ddlPoskod').append(option);
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlPoskod').dropdown('clear');
                                    }

                                    // bank
                                    if (pelajar[0].SMP01_Bank !== null) {
                                        $('#ddlBank').empty()

                                        var bankPromise = GetBankValue(pelajar[0].SMP01_Bank);
                                        bankPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var value = result[0].value;
                                                var label = value + " - " + text;
                                                var option = $('<option>').attr('value', value).text(label);
                                                $('#ddlBank').append(option);
                                            } else {
                                                $('#ddlBank').dropdown('clear');
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlBank').dropdown('clear');
                                    }

                                    $('#txtNoAkaunBank').val(pelajar[0].SMP01_NoAkaun)
                                }
                            }).catch(function (error) {
                                console.error('Error:', error);
                            });
                        }
                    });

                    // get value from ddlNoMatrikUG for auto-select No Matrik UG
                    $('#ddlNoMatrikPG').on('change', async function () {
                        if (!isReset) {
                            // kategori id format XXYYZZ
                            var selectedValue = $(this).val();

                            // make ajax call to get data from server
                            var pelajarPG = GetPelajarPGValue(selectedValue);

                            pelajarPG.then(function (pelajar) {
                                if (pelajar && pelajar.length > 0) {
                                    $('#txtNama').val(pelajar[0].SMG02_NAMA);
                                    $('#txtNoTelefon').val(pelajar[0].SMG02_NOTEL);
                                    $('#txtEmel').val(pelajar[0].SMP01_Emel);
                                    $('#txtAlamat1').val(pelajar[0].SMG01_Alamat1);
                                    $('#txtAlamat2').val(pelajar[0].SMG01_Alamat2);

                                    $('#ddlBandar').dropdown('clear');

                                    if (pelajar[0].SMG01_Bandar !== null) {
                                        $('#ddlBandar').empty();
                                        var bandarPromise = GetBandarValue(pelajar[0].SMG01_Bandar);
                                        bandarPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var value = result[0].value;
                                                var option = $('<option>').attr('value', value).text(text);
                                                $('#ddlBandar').append(option);

                                                // pass value of selected bandar to update negeri and negara
                                                updateNegeriNegaraByBandar(value);
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlBandar').dropdown('clear');
                                    }

                                    $('#ddlPoskod').dropdown('clear');
                                    if (pelajar[0].SMG01_Poskod !== null) {
                                        $('#ddlPoskod').empty();
                                        var poskodPromise = GetPoskodValue(pelajar[0].SMG01_Poskod);
                                        poskodPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var option = $('<option>').attr('value', pelajar[0].SMG01_Poskod).text(text);
                                                $('#ddlPoskod').append(option);
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlPoskod').dropdown('clear');
                                    }

                                    // bank
                                    if (pelajar[0].SMP01_Bank !== null) {
                                        $('#ddlBank').empty()

                                        var bankPromise = GetBankValue(pelajar[0].SMP01_Bank);
                                        bankPromise.then(function (result) {
                                            if (result && result.length > 0) {
                                                var text = result[0].text;
                                                var value = result[0].value;
                                                var label = value + " - " + text;
                                                var option = $('<option>').attr('value', value).text(label);
                                                $('#ddlBank').append(option);
                                            } else {
                                                $('#ddlBank').dropdown('clear');
                                            }
                                        }).catch(function (error) {
                                            console.error('Error:', error);
                                        });
                                    } else {
                                        $('#ddlBank').dropdown('clear');
                                    }

                                    // $('#txtNoAkaunBank').val(pelajar[0].SMP01_NoAkaun)
                                    $('#txtNoAkaunBank').val('') // make empty for now
                                }
                            }).catch(function (error) {
                                console.error('Error:', error);
                            });
                        }
                    });

                    $('#ddlBandar').dropdown({
                        selectOnKeydown: true,
                        fullTextSearch: true,
                        apiSettings: {
                            url: 'InvoisWS.asmx/GetBandarList?q={query}',
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
                            url: 'InvoisWS.asmx/GetPoskodList?q={query}',
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
                            url: 'InvoisWS.asmx/GetNegaraList?q={query}',
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
                            url: 'InvoisWS.asmx/GetNegeriList?q={query}',
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

                    $('#ddlBank').dropdown({
                        selectOnKeydown: true,
                        fullTextSearch: true,
                        apiSettings: {
                            url: 'InvoisWS.asmx/GetBankList?q={query}',
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
                                    var text = option.value + " - " + option.text;
                                    $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(text));
                                });

                                // Refresh dropdown
                                $(obj).dropdown('refresh');

                                if (shouldPop === true) {
                                    $(obj).dropdown('show');
                                }
                            }
                        }
                    });

                    $('#ddlNoStaf').dropdown({
                        selectOnKeydown: true,
                        fullTextSearch: true,
                        apiSettings: {
                            url: 'InvoisWS.asmx/GetKodStaffList?q={query}',
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
                                    // var text = option.MS01_NoStaf + " - " + option.MS01_Nama;
                                    // var text = option.value + " - " + option.text;
                                    $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.value));
                                });

                                // Refresh dropdown
                                $(obj).dropdown('refresh');

                                if (shouldPop === true) {
                                    $(obj).dropdown('show');
                                }
                            }
                        }
                    });

                    $('#ddlNoMatrikUG').dropdown({
                        selectOnKeydown: true,
                        fullTextSearch: true,
                        apiSettings: {
                            url: 'InvoisWS.asmx/GetKodPelajarUGList?q={query}',
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
                                    $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.value));
                                });

                                // Refresh dropdown
                                $(obj).dropdown('refresh');

                                if (shouldPop === true) {
                                    $(obj).dropdown('show');
                                }
                            }
                        }
                    });

                    $('#ddlNoMatrikPH').dropdown({
                        selectOnKeydown: true,
                        fullTextSearch: true,
                        apiSettings: {
                            url: 'InvoisWS.asmx/GetKodPelajarUGList?q={query}',
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
                                    $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.value));
                                });

                                // Refresh dropdown
                                $(obj).dropdown('refresh');

                                if (shouldPop === true) {
                                    $(obj).dropdown('show');
                                }
                            }
                        }
                    });

                    $('#ddlNoMatrikPG').dropdown({
                        selectOnKeydown: true,
                        fullTextSearch: true,
                        apiSettings: {
                            url: 'InvoisWS.asmx/GetKodPelajarPGList?q={query}',
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
                                    $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.value));
                                });

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
                            url: 'InvoisWS.asmx/GetUrusniagaList?q={query}',
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

                    $("#categoryFilter").change(function (e) {
                        var selectedItem = $('#categoryFilter').val()
                        if (selectedItem == "6" && selectedItem !== "") {
                            $('#divDatePicker').addClass("d-flex").removeClass("d-none");
                            $('#txtTarikhStart').val("")
                            $('#txtTarikhEnd').val("")
                        }
                        else {
                            $('#divDatePicker').removeClass("d-flex").addClass("d-none");
                            $('#txtTarikhStart').val("")
                            $('#txtTarikhEnd').val("")
                        }
                    });

                    $('.btnYaBil').click(async function () {
                        //close modal confirmation
                        $('#confirmationModalBil').modal('toggle');

                        var jumRecord = 0;
                        var acceptedRecord = 0;
                        var msg = "";
                        var newOrder = {
                            order: {
                                OrderID: $('#txtnoinv').val(),
                                PenghutangID: $('#ddlPenghutang').val(),
                                TkhMula: $('#tkhMula').val(),
                                TkhTamat: $('#tkhTamat').val(),
                                Kontrak: $("input[name='inlineRadioOptions']:checked").val(),
                                JenisUrusniaga: $('#ddlUrusniaga').val(),
                                Tujuan: $('#txtTujuan').val(),
                                Jumlah: $('#total').val(),
                                tkhBil: $('#tkhBil').val(),
                                tempoh: $('#biltempoh').val(),
                                tempohbyrn: $('#ddlTermBayar').val(),
                                norujukan: $('#txtnorujukan').val(),
                                OrderDetails: []
                            }
                        }
                        $('.vot-carian-list').each(function (index, obj) {
                            if (index > 0) {
                                var tcell = $(obj).closest("td");
                                var orderDetail = {
                                    OrderID: $('#orderid').val(),
                                    ddlVot: $(obj).dropdown("get value"),
                                    ddlPTJ: $('.Hid-ptj-list').eq(index).html(),
                                    ddlKw: $('.Hid-kw-list').eq(index).html(),
                                    ddlKo: $('.Hid-ko-list').eq(index).html(),
                                    ddlKp: $('.Hid-kp-list').eq(index).html(),
                                    details: $('.details').eq(index).val(),
                                    quantity: $('.quantity').eq(index).val(),
                                    price: $('.price').eq(index).val(),
                                    Diskaun: $('.diskaun').eq(index).val(),
                                    Cukai: $('.cukai').eq(index).val(),
                                    amount: $('.amount').eq(index).val(),
                                    id: $(tcell).find(".data-id").val()
                                };


                                if (orderDetail.ddlVot === "" || orderDetail.details === "" ||
                                    orderDetail.quantity === "" || orderDetail.price === "") {
                                    return;
                                }

                                acceptedRecord += 1;
                                newOrder.order.OrderDetails.push(orderDetail);
                            }
                        });

                        msg = "Anda pasti ingin menyimpan " + acceptedRecord + " rekod ini?"

                        //if (!confirm(msg)) {
                        //    return false;
                        //}
                        //console.log(newOrder)
                        var result = JSON.parse(await ajaxSaveOrder(newOrder));
                        //alert(result.Message);
                        if (result.Status !== "Failed") {
                            //$('#modalPenghutang').modal('toggle');
                            // open modal makluman and show message
                            $('#maklumanModalBil').modal('toggle');
                            $('#detailMaklumanBil').html(result.Message);
                            clearAllFields();
                            // refresh page after 2 seconds
                            setTimeout(function () {
                                tbl.ajax.reload();
                            }, 2000);
                        } else {
                            // open modal makluman and show message
                            $('#maklumanModalBil').modal('toggle');
                            $('#detailMaklumanBil').html(result.Message);
                        }
                        //$('#orderid').val(result.Payload.OrderID)
                        //loadExistingRecords();
                        await clearAllRows();
                        await clearAllRowsHdr();
                        AddRow(5);
                        tbl.ajax.reload();

                    });

                    //btnHantar
                    $('.btnYaSubmitBil').click(async function () {

                        //close modal confirmation
                        $('#confirmationModalSubmitBil').modal('toggle');

                        var jumRecord = 0;
                        var acceptedRecord = 0;
                        var msg = "";
                        var newOrder = {
                            order: {
                                OrderID: $('#txtnoinv').val(),
                                PenghutangID: $('#ddlPenghutang').val(),
                                TkhMula: $('#tkhMula').val(),
                                TkhTamat: $('#tkhTamat').val(),
                                Kontrak: $("input[name='inlineRadioOptions']:checked").val(),
                                JenisUrusniaga: $('#ddlUrusniaga').val(),
                                Tujuan: $('#txtTujuan').val(),
                                Jumlah: $('#total').val(),
                                tkhBil: $('#tkhBil').val(),
                                tempoh: $('#biltempoh').val(),
                                tempohbyrn: $('#ddlTermBayar').val(),
                                norujukan: $('#txtnorujukan').val(),
                                OrderDetails: []
                            }
                        }
                        $('.vot-carian-list').each(function (index, obj) {
                            if (index > 0) {
                                var tcell = $(obj).closest("td");
                                var orderDetail = {
                                    OrderID: $('#orderid').val(),
                                    ddlVot: $(obj).dropdown("get value"),
                                    ddlPTJ: $('.Hid-ptj-list').eq(index).html(),
                                    ddlKw: $('.Hid-kw-list').eq(index).html(),
                                    ddlKo: $('.Hid-ko-list').eq(index).html(),
                                    ddlKp: $('.Hid-kp-list').eq(index).html(),
                                    details: $('.details').eq(index).val(),
                                    quantity: $('.quantity').eq(index).val(),
                                    price: $('.price').eq(index).val(),
                                    Diskaun: $('.diskaun').eq(index).val(),
                                    Cukai: $('.cukai').eq(index).val(),
                                    amount: $('.amount').eq(index).val(),
                                    id: $(tcell).find(".data-id").val()
                                };


                                if (orderDetail.ddlVot === "" || orderDetail.details === "" ||
                                    orderDetail.quantity === "" || orderDetail.price === "") {
                                    return;
                                }

                                acceptedRecord += 1;
                                newOrder.order.OrderDetails.push(orderDetail);
                            }
                        });

                        msg = "Anda pasti ingin menyimpan " + acceptedRecord + " rekod ini?"

                        //if (!confirm(msg)) {
                        //    return false;
                        //}
                        //console.log(newOrder)
                        var result = JSON.parse(await ajaxSubmitOrder(newOrder));
                        //alert(result.Message);
                        if (result.Status !== "Failed") {
                            //$('#modalPenghutang').modal('toggle');
                            // open modal makluman and show message
                            $('#maklumanModalBil').modal('toggle');
                            $('#detailMaklumanBil').html(result.Message);
                            clearAllFields();
                            // refresh page after 2 seconds
                            setTimeout(function () {
                                tbl.ajax.reload();
                            }, 2000);
                        } else {
                            // open modal makluman and show message
                            $('#maklumanModalBil').modal('toggle');
                            $('#detailMaklumanBil').html(result.Message);
                        }
                        //$('#orderid').val(result.Payload.OrderID)
                        //loadExistingRecords();
                        await clearAllRows();
                        await clearAllRowsHdr();
                        AddRow(5);
                        tbl.ajax.reload();

                    });

                    $('.btnPadam').click(async function () {
                        //var NoInvois = $('#txtnoinv').val();

                        $('#txtnoinv').val("")
                        await clearAllRows();
                        await clearAllRowsHdr();
                        await clearHiddenButton();
                        $('.btnCetak').hide();
                        $('.btnEmel').hide();
                        $('.btnTambahPenghutang').show();
                        AddRow(5);

                    });

                    $('.btnLoad').on('click', async function () {
                        loadExistingRecords();
                    });

                    async function loadExistingRecords() {
                        var record = await AjaxLoadOrderRecord($('#orderid').val());
                        await clearAllRows();
                        await AddRow(null, record);
                    }

                    async function clearAllRows() {
                        $(tableID + " > tbody > tr ").each(function (index, obj) {
                            if (index > 0) {
                                obj.remove();
                            }
                        })
                        $(totalID).val("0.00");
                        $(totalCukai).val("0.00");
                        $(totalDiskaun).val("0.00");
                        $(totalwoCukai).val("0.00");

                    }

                    async function clearAllRowsHdr() {
                        //alert("hai")
                        $('#txtnoinv').val("");
                        $('#ddlPenghutang').dropdown('clear');
                        $('#ddlPenghutang').empty();
                        $('#ddlKategoriPenghutang').dropdown('clear');
                        $('#ddlKategoriPenghutang').empty();
                        $('#ddlTermBayar').dropdown('clear');
                        $('#ddlTermBayar').empty();
                        $('#tkhMula').val("");
                        $('#tkhTamat').val("");
                        $('#tkhBil').val("");
                        $('#biltempoh').val("");
                        $('#txtnorujukan').val("");
                        $("#ddlUrusniaga").dropdown('clear');
                        $("#ddlUrusniaga").empty();
                        $('#txtTujuan').val("");

                        // #rdKontrak radio button set to default, rdTidak is checked
                        $('#rdTidak').prop('checked', true);

                        // trigger change event on #rdKontrak to effect the #tkhMula and #tkhTamat to disabled
                        $('#rdKontrak').trigger('change');

                        //var selectObj_JenisTransaksiP = $('#ddlPenghutang')
                        //selectObj_JenisTransaksiP.append("<option value = '' ></option>")

                    }

                    // when #rdKontrak changes, effect the #tkhMula and #tkhTamat to disabled or enabled
                    $('input[type=radio][name=inlineRadioOptions]').change(function () {
                        var tkhbil = $('#tkhBil').val();
                        if (this.value == '1') {

                            $('#tkhMula').val("");
                            $('#tkhTamat').val("");
                            $('#tkhMula').prop('disabled', false);
                            $('#tkhTamat').prop('disabled', false);
                        }
                        else if (this.value == '0') {
                            if (tkhbil == null) {
                                const today = new Date().toISOString().substr(0, 10);
                                $('#tkhMula').val(today);
                                $('#tkhTamat').val(today);
                                $('#tkhMula').prop('disabled', true);
                                //$('#tkhTamat').prop('disabled', true);
                            } else {
                                $('#tkhTamat').val(tkhbil);
                            }
                            
                        }
                    });

                    async function clearHiddenButton() {

                        $('.btnSimpan').show();
                        $('.btnHantar').show();
                        $('.btnAddRow').show();

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

                        // count total items
                        totalRows = $(tableID + " > tbody > tr ").length - 1;
                        if (totalRows === 0) {
                            AddRow(5);
                        }

                        if (totalItems > 0) {
                            totalItems -= 1;
                        }

                        $('#stickyJumlahItem').text(totalItems);
                        // end count total items

                        calculateGrandTotal();
                        return false;
                    })

                    async function ajaxSaveOrder(order) {

                        return new Promise((resolve, reject) => {
                            $.ajax({

                                url: 'InvoisWS.asmx/SaveOrders',
                                method: 'POST',
                                data: JSON.stringify(order),
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
                        //console.log("tst")
                    }
                    //ajaxSubmitOrder
                    async function ajaxSubmitOrder(order) {

                        return new Promise((resolve, reject) => {
                            $.ajax({

                                url: 'InvoisWS.asmx/SubmitOrders',
                                method: 'POST',
                                data: JSON.stringify(order),
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
                        //console.log("tst")
                    }
                    async function ajaxDeleteOrder(id) {
                        return new Promise((resolve, reject) => {
                            $.ajax({
                                url: 'InvoisWS.asmx/DeleteOrder',
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
                                url: 'InvoisWS.asmx/DeleteRecord',
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
                            const response = await fetch('InvoisWS.asmx/LoadOrderRecord', {
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

                        //var result_ = id.split('|');
                        //var nobil = result_[0];
                        //var noitem = result_[1];
                        //console.log(nobil, noitem)
                        var result = JSON.parse(await AjaxDelete(id));

                        if (result.Code === "00") {
                            bool = true;
                        }

                        return bool;
                    }

                    $(tableID).on('keyup', '.quantity, .price, .amount, .diskaun, .cukai', async function () {
                        var curTR = $(this).closest("tr");
                        var quantity = $(curTR).find("td > .quantity");
                        var price = $(curTR).find("td > .price");
                        var amount = $(curTR).find("td > .amount");
                        var cukai = $(curTR).find("td > .cukai");
                        var JUMcukai = $(curTR).find("td > .JUMcukai");
                        var diskaun = $(curTR).find("td > .diskaun");
                        var JUMdiskaun = $(curTR).find("td > .JUMdiskaun");
                        var amountwocukai = $(curTR).find("td > .amountwocukai");

                        var totalPrice = NumDefault(quantity.val()) * NumDefault(price.val())
                        var amauncukai = NumDefault(cukai.val()) / 100
                        var total_cukai = totalPrice * amauncukai
                        var amaundiskaun = NumDefault(diskaun.val()) / 100
                        var total_diskaun = totalPrice * amaundiskaun
                        var amountxcukai = totalPrice - total_diskaun
                        //alert(amaundiskaun)
                        totalPrice = totalPrice + total_cukai - total_diskaun
                        amount.val(totalPrice.toFixed(2));
                        JUMcukai.val(total_cukai.toFixed(2));
                        JUMdiskaun.val(total_diskaun.toFixed(2));
                        amountwocukai.val(amountxcukai.toFixed(2));
                        calculateGrandTotal();

                        //START BIL COUNT DATATABLE...
                        var columnIndexToCount = 0; // Change this to the desired column index (0-based)
                        var rowCount = 0;

                        $("#tableID").find("tr").each(function () {
                            var cellValue = $(this).find("td:eq(" + columnIndexToCount + ")").text();

                            // Check if the cell has data
                            if (cellValue.trim() !== "") {
                                rowCount++;
                            }
                        });

                        totalItems = rowCount;

                        $('#stickyJumlahItem').text(totalItems);
                        //END BIL COUNT
                    });


                    $(tableID).ready(function () {
                        $(".price").change(function () {
                            $(this).val(parseFloat($(this).val()).toFixed(2));
                        });
                        $(".cukai").change(function () {
                            $(this).val(parseFloat($(this).val()).toFixed(2));
                        });
                        $(".diskaun").change(function () {
                            $(this).val(parseFloat($(this).val()).toFixed(2));
                        });
                    });
                    async function calculateGrandTotal() {
                        var grandTotal = $(totalID);
                        var totalCukai_ = $(totalCukai);
                        var totalDiskaun_ = $(totalDiskaun);
                        var totalwoCukai_ = $(totalwoCukai);
                        var curTotal = 0;
                        var curCukai = 0;
                        var curDiskaun = 0;
                        var curwoCukai = 0;

                        $('.amount').each(function (index, obj) {
                            curTotal += parseFloat(NumDefault($(obj).val()));
                        });
                        $('.JUMcukai').each(function (index, obj) {
                            curCukai += parseFloat(NumDefault($(obj).val()));
                        });

                        $('.JUMdiskaun').each(function (index, obj) {
                            curDiskaun += parseFloat(NumDefault($(obj).val()));
                        });

                        $('.amountwocukai').each(function (index, obj) {
                            curwoCukai += parseFloat(NumDefault($(obj).val()));
                        });

                        //$("[id*=TotalCoreProd]").html(TotalCoreProd.toFixed(2));
                        totalCukai_.val(curCukai.toFixed(2));
                        totalDiskaun_.val(curDiskaun.toFixed(2));
                        totalwoCukai_.val(curwoCukai.toFixed(2));

                        curTotal = roundCurrency(curTotal);

                        grandTotal.val(curTotal.toFixed(2));

                        //STICKYJUMLAH
                        document.getElementById('stickyJumlah').textContent = curTotal.toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 });
                    }

                    function roundCurrency(value) {
                        const cents = Math.round((value - Math.floor(value)) * 100) / 100;
                        let roundedValue;

                        if (cents >= 1.01 && cents <= 1.02) {
                            roundedValue = Math.floor(value);
                        } else if (cents >= 1.03 && cents <= 1.04) {
                            roundedValue = Math.floor(value) + 0.05;
                        } else if (cents === 1.05) {
                            roundedValue = value;
                        } else if (cents >= 1.06 && cents <= 1.08) {
                            roundedValue = Math.ceil(value) + 0.02;
                        } else {
                            roundedValue = Math.round(value * 20) / 20; // Default rounding to the nearest 0.05 if not in specified ranges
                        }

                        const difference = roundedValue - value;
                        const differenceText = difference.toFixed(2);

                        $('#rounding').val(differenceText);

                        return roundedValue;
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

                    async function initCarianVot(id) {
                        $('#' + id).dropdown({
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'InvoisWS.asmx/GetCarianVotList?q={query}',
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
                    }
                    async function initDropdown(id) {
                        $('#' + id).dropdown({
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'InvoisWS.asmx/GetPTJList?q={query}',
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
                    }

                    async function initVot(id, idptj) {
                        $('#' + id).dropdown({
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'InvoisWS.asmx/GetVotList?q={query}&kodptj={kodptj}',
                                method: 'POST',
                                dataType: "json",
                                contentType: 'application/json; charset=utf-8',
                                cache: false,
                                beforeSend: function (settings) {
                                    // Replace {query} placeholder in data with user-entered search term
                                    var ptjID = $('#' + idptj).dropdown("get value");
                                    //console.log(votID);
                                    settings.urlData.kodptj = ptjID;
                                    settings.data = JSON.stringify({ q: settings.urlData.query, kodptj: settings.urlData.kodptj });

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
                    }

                    async function initKW(id, idVot) {
                        $('#' + id).dropdown({
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'InvoisWS.asmx/GetKWList?q={query}&kodvot={kodvot}',
                                method: 'POST',
                                dataType: "json",
                                contentType: 'application/json; charset=utf-8',
                                cache: false,
                                beforeSend: function (settings) {
                                    // Replace {query} placeholder in data with user-entered search term
                                    var votID = $('#' + idVot).dropdown("get value");
                                    settings.urlData.kodvot = votID;
                                    settings.data = JSON.stringify({ q: settings.urlData.query, kodvot: settings.urlData.kodvot });

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
                    }

                    async function initKO(id, idKW) {
                        $('#' + id).dropdown({
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'InvoisWS.asmx/GetKOList?q={query}&kodkw={kodkw}',
                                method: 'POST',
                                dataType: "json",
                                contentType: 'application/json; charset=utf-8',
                                cache: false,
                                beforeSend: function (settings) {
                                    // Replace {query} placeholder in data with user-entered search term
                                    var KOID = $('#' + idKW).dropdown("get value");
                                    settings.urlData.kodkw = KOID;
                                    settings.data = JSON.stringify({ q: settings.urlData.query, kodkw: settings.urlData.kodkw });

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
                    }

                    async function initKP(id, idPTJ, idVot) {
                        $('#' + id).dropdown({
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'InvoisWS.asmx/GetProjekList?q={query}&kodptj={kodptj}&kodvot={kodvot}',
                                method: 'POST',
                                dataType: "json",
                                contentType: 'application/json; charset=utf-8',
                                cache: false,
                                beforeSend: function (settings) {
                                    // Replace {query} placeholder in data with user-entered search term
                                    var ptjID = $('#' + idPTJ).dropdown("get value");
                                    var VotID = $('#' + idVot).dropdown("get value");
                                    settings.urlData.kodptj = ptjID;
                                    settings.urlData.kodvot = VotID;
                                    settings.data = JSON.stringify({ q: settings.urlData.query, kodptj: settings.urlData.kodptj, kodvot: settings.urlData.kodvot });

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
                    }

                    $('.btnAddRow').click(async function () {
                        var totalClone = $(this).data("val");

                        await AddRow(totalClone);
                    });

                    $(tableID_Senarai).on('click', '.btnView', async function () {

                        event.preventDefault();
                        var curTR = $(this).closest("tr");
                        var recordID = curTR.find("td > .lblNo");

                        //var bool = true;
                        var id = recordID.html();
                        //alert("hai");
                        //console.log(id)
                        if (id !== "") {

                            //BACA HEADER JURNAL
                            var recordHdr = await AjaxGetRecordHdrJurnal(id);
                            //alert("hello")
                            await clearAllRowsHdr();
                            await AddRowHeader(null, recordHdr);

                            //BACA DETAIL JURNAL
                            var record = await AjaxGetRecordJurnal(id);
                            await clearAllRows();
                            await AddRow(null, record);
                        }

                        return false;
                    })

                    // add clickable event in DataTable row
                    async function rowClickHandler(id) {

                        if (id !== "") {
                            // modal dismiss
                            $('#permohonan').modal('toggle');
                            // set btnEmel and btnCetak to show when modal is open
                            $('.btnEmel').show();
                            $('.btnCetak').show();
                            $('.btnTambahPenghutang').hide();
                            //BACA HEADER JURNAL
                            var recordHdr = await AjaxGetRecordHdrJurnal(id);
                            await clearAllRowsHdr();
                            await AddRowHeader(null, recordHdr);

                            //BACA DETAIL JURNAL
                            var record = await AjaxGetRecordJurnal(id);
                            await clearAllRows();
                            await AddRow(null, record);
                        }
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

                    async function setValueToRow_HdrJurnal(orderDetail) {
                        await setValueToEmailModal(orderDetail); // Haziq - 17/8/2023

                        $('#txtnoinv').val(orderDetail.No_Bil)
                        //$('#txtNoRujukan').val(orderDetail.No_Rujukan)
                        //$('#ddlPenghutang').val(orderDetail.Nama_Penghutang)
                        var T_Mula = orderDetail.Tkh_Mula.substr(0, 10);
                        var T_Tamat = orderDetail.Tkh_Tamat.substr(0, 10);
                        if (orderDetail.Tkh_Bil !== null) {
                            var T_Bil = orderDetail.Tkh_Bil.substr(0, 10);
                            $('#tkhBil').val(T_Bil)
                        }
                        
                        //alert("tarikhmula" + tt);

                        //console.log($('#rdTIDAK').is(':checked'))
                        if (orderDetail.Kontrak == '0') {
                            //$('[id=rdTIDAK]')[0].checked = true
                            $(':radio[name=inlineRadioOptions][value="0"]').prop('checked', true);
                            $(':radio[name=inlineRadioOptions][value="1"]').prop('checked', false);
                            $('#tkhMula').val(T_Mula)
                            $('#tkhTamat').val(T_Tamat)
                            //$('#tkhMula').prop('disabled', true);
                            //$('#tkhTamat').prop('disabled', true);
                        } else {
                            //$('#rdYA').is('checked:checked');
                            $(':radio[name=inlineRadioOptions][value="1"]').prop('checked', true);
                            $(':radio[name=inlineRadioOptions][value="0"]').prop('checked', false);
                            $('#tkhMula').val(T_Mula)
                            $('#tkhTamat').val(T_Tamat)
                        }
                        //$("input[name='inlineRadioOptions']:checked").val(orderDetail.Kontrak)
                        //$('#ddlUrusniaga').val(orderDetail.JenisUrusniaga)
                        $('#txtTujuan').val(orderDetail.Tujuan)
                        
                        $('#txtnorujukan').val(orderDetail.No_Rujukan)
                        $('#biltempoh').val(orderDetail.Tempoh_Kontrak)

                        var newId = $('#ddlJenTransaksi')

                        //await initDropdownPtj(newId)
                        //$(newId).api("query");

                        // append ddlKategoriPenghutang data 
                        $('#ddlKategoriPenghutang').dropdown('set selected', orderDetail.Kategori_Penghutang);
                        $('#ddlKategoriPenghutang').append("<option value = '" + orderDetail.Kategori_Penghutang + "'>" + orderDetail.Butiran_Kategori + "</option>")

                        // append ddlTermBayaran
                        if (orderDetail.Jenis_Tempoh !== null) {
                            var ddlTermByrn = $('#ddlTermBayar')
                            var ddlSearchP = $('#ddlTermBayar')
                            var ddlTextP = $('#ddlTermBayar')
                            var selectObj_JenisBayar = $('#ddlTermBayar')
                            $(ddlPenghutang).dropdown('set selected', orderDetail.Jenis_Tempoh);
                            selectObj_JenisBayar.append("<option value = '" + orderDetail.Jenis_Tempoh + "'>" + orderDetail.JenisTempoh + "</option>")
                        }
                        

                        var ddlPenghutang = $('#ddlPenghutang')
                        var ddlSearchP = $('#ddlPenghutang')
                        var ddlTextP = $('#ddlPenghutang')
                        var selectObj_JenisTransaksiP = $('#ddlPenghutang')
                        $(ddlPenghutang).dropdown('set selected', orderDetail.Kod_Penghutang);
                        selectObj_JenisTransaksiP.append("<option value = '" + orderDetail.Kod_Penghutang + "'>" + orderDetail.Nama_Penghutang + "</option>")

                        var ddlUrusniaga = $('#ddlUrusniaga')
                        var ddlSearch = $('#ddlUrusniaga')
                        var ddlText = $('#ddlUrusniaga')
                        var selectObj_JenisTransaksi = $('#ddlUrusniaga')
                        $(ddlUrusniaga).dropdown('set selected', orderDetail.Kod_Urusniaga);
                        selectObj_JenisTransaksi.append("<option value = '" + orderDetail.Kod_Urusniaga + "'>" + orderDetail.Butiran + "</option>")
                        var status_kod_dok = orderDetail.Kod_Status_Dok
                        console.log(status_kod_dok)
                        if (status_kod_dok == '01') {
                            $('.btnSimpan').show();
                            $('.btnHantar').show();
                            $('.btnAddRow').show();
                            $('.btn-warning').show();

                        }
                        else {
                            $('.btnSimpan').hide();
                            $('.btnHantar').hide();
                            $('.btnAddRow').hide();
                            $('.btn-warning').hide();

                        }
                    }

                    // set value to email modal (Haziq - 17/8/2023) 
                    async function setValueToEmailModal(orderDetail) {
                        $('#emailBilTuntutan').val(orderDetail.No_Bil)
                        $('#emailName').val(orderDetail.Nama_Penghutang)
                        $('#emailEmail').val(orderDetail.Emel)

                        $('#lblEmailName').html($('#emailName').val())
                        $('#lblEmailBilTuntutan').html($('#emailBilTuntutan').val())
                    }

                    $('#emailName').on('keyup', function () {
                        $('#lblEmailName').html($('#emailName').val())
                    })

                    
                    

                    //alert(jenistempoh)

                    function handleInputChange(var1, var2, var3, var4) {
                        // Define your variables
                        const startDate = new Date(tkhBil);
                        //const daysToAdd = 5; // Number of working days to add
                        const holidays = [new Date('2023-09-16')]; // Optional holidays to exclude
                        var tkhmula = $('#tkhBil').val();
                        var tkhtamat = $('#tkhTamat').val();
                        var tempoh = $('#biltempoh').val();
                        var jenistempoh = $('#ddlTermBayar').val();
                        var var1 = "0";
                        var var2 = "0";
                        var var3 = "0";
                        var var4 = "0";
                        var daysToAdd = "0";
                       
                        if (var1 !== null) {
                            if (tkhmula !== null || tempoh !== null || jenistempoh !== null) {
                                if (jenistempoh == "01")
                                {
                                    daysToAdd = 1 * tempoh;
                                }
                                else if (jenistempoh == "02")
                                {
                                    daysToAdd = 7 * tempoh;
                                }
                                else if (jenistempoh == "03")
                                {
                                    daysToAdd = 30 * tempoh;
                                }
                                else if (jenistempoh == "04")
                                {
                                    daysToAdd = 365 * tempoh;
                                }
                            }
                            const resultDate = addWorkingDays(tkhmula, daysToAdd, holidays);
                            //tkhtamat.val(resultDate.toDateString())
                            //alert(tkhmula)
                            if (tkhmula !== null || tkhmula !== "" || tempoh !== null || jenistempoh !== null) {
                                $('#tkhTamat').val(resultDate.toISOString().substr(0, 10))
                            }
                            
                        }

                    }

                    //$('#tkhBil').on('change', function () {
                        
                    //    //$('#tkhTamat').val($('#tkhBil').val())
                    //    // Example usage:
                    //    //if $('#tkhBil').val()
                    //    const tkhBil = $('#tkhBil').val();
                    //    const startDate = new Date(tkhBil);
                    //    const daysToAdd = 5; // Number of working days to add
                    //    const holidays = [new Date('2023-09-16')]; // Optional holidays to exclude
                    //    alert(tkhBil)
                    //    const resultDate = addWorkingDays(startDate, daysToAdd, holidays);
                    //    alert(resultDate.toDateString()); // Output the result date
                    //})

                    function addWorkingDays(startDate, daysToAdd, holidays = []) {
                        const ONE_DAY = 24 * 60 * 60 * 1000; // One day in milliseconds
                        const weekendDays = [0, 6]; // Sunday (0) and Saturday (6)

                        let currentDate = new Date(startDate); // Clone the start date
                        let addedDays = 0;

                        while (addedDays < daysToAdd) {
                            // Move to the next day
                            currentDate.setTime(currentDate.getTime() + ONE_DAY);

                            // Check if the current day is a weekend day or a holiday
                            const currentDayOfWeek = currentDate.getDay();
                            const isWeekend = weekendDays.includes(currentDayOfWeek);
                            const isHoliday = holidays.some(holiday => {
                                return holiday.getTime() === currentDate.getTime();
                            });

                            if (!isWeekend && !isHoliday) {
                                // If it's a working day and not a holiday, increment the added days
                                addedDays++;
                            }
                        }

                        return currentDate;
                    }

                    

                    async function AjaxGetRecordHdrJurnal(id) {

                        try {

                            const response = await fetch('InvoisWS.asmx/LoadHdrInvois', {
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

                    async function AjaxGetRecordJurnal(id) {

                        try {

                            const response = await fetch('InvoisWS.asmx/LoadRecordInvois', {
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

                    async function AddRow(totalClone, objOrder) {
                        var counter = 1;
                        var table = $('#tblData');

                        if (objOrder !== null && objOrder !== undefined) {
                            //totalClone = objOrder.Payload.OrderDetails.length;
                            totalClone = objOrder.Payload.length;
                            //console.log(totalClone)
                            //if (totalClone < 5) {
                            //    totalClone = 5;
                            //}
                        }

                        totalItems = 0;

                        while (counter <= totalClone) {
                            curNumObject += 1;
                            var newCarianVot = "ddlVotCarian" + curNumObject;

                            var row = $('#tblData tbody>tr:first').clone();
                            var votcarianlist = $(row).find(".vot-carian-list").attr("id", newCarianVot);

                            row.attr("style", "");
                            var val = "";

                            $('#tblData tbody').append(row);

                            
                            await initCarianVot(newCarianVot)

                            //$(newId).api("query");
                            //(newVotID).api("query");
                            //$(newKwID).api("query");
                            //(newKOID).api("query");
                            //$(newKPID).api("query");
                            $(newCarianVot).api("query");
                            
                            if (objOrder !== null && objOrder !== undefined) {
                                //await setValueToRow(row, objOrder.Payload.OrderDetails[counter - 1]);
                                if (counter <= objOrder.Payload.length) {
                                    await setValueToRow_Transaksi(row, objOrder.Payload[counter - 1]);

                                    totalItems += 1;

                                }
                            }
                            counter += 1;
                        }

                        $('#stickyJumlahItem').text(totalItems);
                    }

                    async function setValueToRow_Transaksi(row, orderDetail) {

                        //console.log(orderDetail)
                        var ddl = $(row).find("td > .vot-carian-list");
                        var ddlSearch = $(row).find("td > .vot-carian-list > .search");
                        var ddlText = $(row).find("td > .vot-carian-list > .text");
                        var selectObj = $(row).find("td > .vot-carian-list > select");
                        $(ddl).dropdown('set selected', orderDetail.Kod_Vot);
                        selectObj.append("<option value = '" + orderDetail.Kod_Vot + "'>" + orderDetail.ButiranVot + "</option>")


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

                        var details = $(row).find("td > .details");
                        details.val(orderDetail.Perkara);

                        var quantity = $(row).find("td > .quantity");
                        quantity.val(orderDetail.Kuantiti);
                        //console.log(orderDetail)
                        var cukai = $(row).find("td > .cukai");
                        cukai.val(orderDetail.Cukai.toFixed(2));

                        var kdr_hrga = $(row).find("td > .price");
                        kdr_hrga.val(orderDetail.Kadar_Harga.toFixed(2));

                        var diskaun = $(row).find("td > .diskaun");
                        diskaun.val(orderDetail.Diskaun.toFixed(2));

                        var amount = $(row).find("td > .amount");
                        amount.val(orderDetail.Jumlah);

                        var hddataid = $(row).find("td > .data-id");
                        hddataid.val(orderDetail.dataid)
                        //var quantity = $(curTR).find("td > .quantity");
                        //var price = $(curTR).find("td > .price");
                        //var amount = $(curTR).find("td > .amount");
                        //var cukai = $(curTR).find("td > .cukai");
                        var JUMcukai = $(row).find("td > .JUMcukai");
                        //var diskaun = $(curTR).find("td > .diskaun");
                        var JUMdiskaun = $(row).find("td > .JUMdiskaun");
                        var amountwocukai = $(row).find("td > .amountwocukai");

                        var totalPrice = NumDefault(quantity.val()) * NumDefault(kdr_hrga.val())
                        var amauncukai = NumDefault(cukai.val()) / 100
                        var total_cukai = totalPrice * amauncukai
                        var amaundiskaun = NumDefault(diskaun.val()) / 100
                        var total_diskaun = totalPrice * amaundiskaun
                        var amountxcukai = totalPrice - total_diskaun
                        //alert(amaundiskaun)

                        totalPrice = totalPrice + total_cukai - total_diskaun
                        amount.val(totalPrice.toFixed(2));
                        JUMcukai.val(total_cukai.toFixed(2));
                        JUMdiskaun.val(total_diskaun.toFixed(2));
                        amountwocukai.val(amountxcukai.toFixed(2));
                        calculateGrandTotal();

                    }

                    async function initCarianVot(id) {

                        $('#' + id).dropdown({
                            fullTextSearch: true,
                            onChange: function (value, text, $selectedItem) {

                                console.log($selectedItem);

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
                                url: 'InvoisWS.asmx/GetVotCOA?q={query}',
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

                    async function setValueToRow(row, orderDetail) {
                        var no = $(row).find("td > .lblNo");
                        var Penghutang = $(row).find("td > .lblPenghutang");
                        var TkhMula = $(row).find("td > .lblTkhMula");
                        var TkhTamat = $(row).find("td > .lblTkhTamat");
                        var JnsUrusNiaga = $(row).find("td > .lblJnsUrus");
                        var Tujuan = $(row).find("td > .lblTujuan");
                        //var Jumlah = $(row).find("td > .lblJumlah"); 

                        no.html(orderDetail.No_Invois);
                        Penghutang.html(orderDetail.Nama_Penghutang);
                        TkhMula.html(orderDetail.Tkh_Mula);
                        TkhTamat.html(orderDetail.Tkh_Tamat);
                        JnsUrusNiaga.html(orderDetail.UrusNiaga);
                        Tujuan.html(orderDetail.Tujuan);
                        //Jumlah.html(orderDetail.Jumlah);
                    }

                    // get value from ddl for auto-select Negeri and Negara
                    $('#ddlBandar').on('change', async function () {
                        if (!isReset) {
                            // bandar id format XXYYZZ
                            var selectedValue = $(this).val();

                            // get XX from selectedValue
                            var idNegeri = selectedValue.substring(0, 2);

                            // get YY from selectedValue
                            var idParlimen = selectedValue.substring(2, 4);

                            var isExist = false;

                            $('#ddlNegeri').empty();
                            for (var i = 0; i < kodNegeriArray.length; i++) {
                                if (kodNegeriArray[i].value === idNegeri) {
                                    var text = kodNegeriArray[i].text;
                                    var value = kodNegeriArray[i].value;
                                    var option = $('<option>').attr('value', value).text(text);
                                    $('#ddlNegeri').append(option);
                                    isExist = true;

                                    $('#ddlNegara').empty();
                                    var textNegara = "Malaysia"
                                    var valueNegara = "MY"
                                    var option = $('<option>').attr('value', valueNegara).text(textNegara);
                                    $('#ddlNegara').append(option);
                                }
                            }

                            if (!isExist) {
                                $('#ddlNegeri').dropdown('clear');
                                $('#ddlNegara').dropdown('clear');
                            }
                        }
                    });

                    // update negeri and negara based on Bandar selection
                    function updateNegeriNegaraByBandar(selectedValue) {
                        if (!isReset) {
                            // bandar id format XXYYZZ
                            var idNegeri = selectedValue.substring(0, 2);

                            // get YY from selectedValue
                            var idParlimen = selectedValue.substring(2, 4);

                            var isExist = false;

                            $('#ddlNegeri').empty();
                            for (var i = 0; i < kodNegeriArray.length; i++) {
                                if (kodNegeriArray[i].value === idNegeri) {
                                    var text = kodNegeriArray[i].text;
                                    var value = kodNegeriArray[i].value;
                                    var option = $('<option>').attr('value', value).text(text);
                                    $('#ddlNegeri').append(option);
                                    isExist = true;

                                    $('#ddlNegara').empty();
                                    var textNegara = "Malaysia";
                                    var valueNegara = "MY";
                                    var optionNegara = $('<option>').attr('value', valueNegara).text(textNegara);
                                    $('#ddlNegara').append(optionNegara);
                                }
                            }

                            if (!isExist) {
                                $('#ddlNegeri').dropdown('clear');
                                $('#ddlNegara').dropdown('clear');
                            }
                        }
                    }

                    // get data from server for $('#ddlKategoriPenghutang') dropdown when row is selected
                    function GetKategoriValue(kod, callback) {
                        return new Promise(function (resolve, reject) {
                            $.ajax({
                                url: 'InvoisWS.asmx/GetKategoriValue',
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
                                url: 'InvoisWS.asmx/GetNegaraValue',
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
                                url: 'InvoisWS.asmx/GetNegeriValue',
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
                                url: 'InvoisWS.asmx/GetPoskodValue',
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

                    // get data from server for $('#ddlBandar') dropdown when row is selected
                    function GetBandarValue(kod, callback) {
                        return new Promise(function (resolve, reject) {
                            $.ajax({
                                url: 'InvoisWS.asmx/GetBandarValue',
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

                    // get data from server for $('#ddlBandar') dropdown when row is selected // map using NAME
                    function GetBandarValueMapping(kod, callback) {
                        return new Promise(function (resolve, reject) {
                            $.ajax({
                                url: 'InvoisWS.asmx/GetBandarValueMap',
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

                    // get data from server for $('#ddlBandar') dropdown when row is selected
                    function GetBankValue(kod, callback) {
                        return new Promise(function (resolve, reject) {
                            $.ajax({
                                url: 'InvoisWS.asmx/GetBankValue',
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

                    // get data from server for $('#ddlNoStaf') dropdown when row is selected for noStaf
                    function GetNoStaffValue(kod, callback) {
                        return new Promise(function (resolve, reject) {
                            $.ajax({
                                url: 'InvoisWS.asmx/GetKodStaffList',
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
                    // get data from server for $('#ddlNoStaf') dropdown when row is selected for noStaf
                    function GetPenghutangAllDetailValue(kod, callback) {
                        return new Promise(function (resolve, reject) {
                            $.ajax({
                                url: 'InvoisWS.asmx/GetPenghutangAllDetailValue',
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

                    // get data from server for $('#ddlNoStaf') dropdown when row is selected for all staf data
                    function GetStaffValue(kod, callback) {
                        return new Promise(function (resolve, reject) {
                            $.ajax({
                                url: 'InvoisWS.asmx/GetStafValue',
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

                    // get data from server for $('#ddlNoMatrikUG') dropdown when row is selected
                    function GetPelajarUGValue(kod, callback) {
                        return new Promise(function (resolve, reject) {
                            $.ajax({
                                url: 'InvoisWS.asmx/GetPelajarUGValue',
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

                    // get data from server for $('#ddlNoMatrikUG') dropdown when row is selected
                    function GetPelajarPGValue(kod, callback) {
                        return new Promise(function (resolve, reject) {
                            $.ajax({
                                url: 'InvoisWS.asmx/GetPelajarPGValue',
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

                    $('.btnHantar').click(async function () {
                        // check every required field
                        if ($('#ddlKategoriPenghutang').val() == "" || $('#ddlUrusniaga').val() == "" || $('#ddlPenghutang').val() == "" || $("input[name='inlineRadioOptions']:checked").val() == "" || $('#txtTujuan').val() == "") {
                            // open modal makluman and show message
                            $('#maklumanModalBil').modal('toggle');
                            $('#detailMaklumanBil').html("Sila isi semua ruangan yang bertanda *");
                        } else {
                            // open modal confirmation
                            $('#confirmationModalSubmitBil').modal('toggle');
                        }
                    })

                    //$('.btnHantarEmail').click(async function () {
                    //    // check every required field
                    //    //if ($('#emailEmail').val() == "" ) {
                    //        // open modal makluman and show message
                    //    $('#modalEmail').modal('toggle');
                    //        //$('#detailMaklumanBil').html("Sila isi emel !");
                    //    //} else {
                    //        // open modal confirmation
                    //        //$('#confirmationModalHantarEmel').modal('toggle');
                    //    //}
                    //})


                    $('.btnSimpan').click(async function () {
                        // check every required field
                        if ($('#ddlKategoriPenghutang').val() == "" || $('#ddlUrusniaga').val() == "" || $('#ddlPenghutang').val() == "" || $("input[name='inlineRadioOptions']:checked").val() == "" || $('#txtTujuan').val() == "") {
                            // open modal makluman and show message
                            $('#maklumanModalBil').modal('toggle');
                            $('#detailMaklumanBil').html("Sila isi semua ruangan yang bertanda *");
                        } else {
                            // open modal confirmation
                            $('#confirmationModalBil').modal('toggle');
                        }
                    })

                    $('.btnSimpanPenghutang').click(async function () {
                        // check every required field
                        if ($('#ddlAddKategoriPenghutang').val() == "" || $('#txtNama').val() == "" || $('#txtNoTelefon').val() == "" || $('#txtEmel').val() == "") {
                            // open modal makluman and show message
                            $('#maklumanModal').modal('toggle');
                            $('#detailMakluman').html("Sila isi semua ruangan yang bertanda *");
                        } else {
                            // open modal confirmation
                            $('#confirmationModal').modal('toggle');
                        }
                    })

                    // confirmation button in confirmation modal
                    $('.btnYa').click(async function () {
                        var penghutang = null;

                        //close modal confirmation
                        $('#confirmationModal').modal('toggle');

                        selectedCategory = $('#ddlAddKategoriPenghutang').val();

                        var id;
                        var bank;
                        var noAkaunBank;
                        var alamat1;
                        var alamat2;
                        var poskod;
                        var bandar;
                        var negeri;
                        var negara;
                        console.log("selectedCategory:"+selectedCategory)
                        if (selectedCategory == "ST") {
                            id = $('#ddlNoStaf').val();
                            bank = $('#txtStafBankName').val();
                        } else if (selectedCategory == "PL") {
                            id = $('#ddlNoMatrikUG').val();
                            bank = $('#ddlBank').val();
                        } else if (selectedCategory == "PG") {
                            id = $('#ddlNoMatrikPG').val();
                            bank = $('#ddlBank').val();
                        } else if (selectedCategory == "PH") {
                            
                            console.log("id" + id)
                            id = $('#ddlNoMatrikPH').val();
                            //console.log("id" + id)
                            bank = $('#ddlBank').val();
                        } else if (selectedCategory == "OA" || selectedCategory == "SY") {
                            id = $('#txtId').val();
                            bank = $('#ddlBank').val();
                        }

                        if (bank == '' || bank == null) {
                            bank = '-';
                        }

                        if ($('#txtAlamat1').val() == '' || $('#txtAlamat1').val() == null) {
                            alamat1 = '-';
                        } else {
                            alamat1 = $('#txtAlamat1').val();
                        }

                        if ($('#txtAlamat2').val() == '' || $('#txtAlamat2').val() == null) {
                            alamat2 = '-';
                        } else {
                            alamat2 = $('#txtAlamat2').val();
                        }

                        if ($('#ddlPoskod').val() == '' || $('#ddlPoskod').val() == null) {
                            poskod = '-';
                        } else {
                            poskod = $('#ddlPoskod').val();
                        }

                        if ($('#ddlBandar').val() == '' || $('#ddlBandar').val() == null) {
                            bandar = '-';
                        } else {
                            bandar = $('#ddlBandar').val();
                        }

                        if ($('#ddlNegeri').val() == '' || $('#ddlNegeri').val() == null) {
                            negeri = '-';
                        } else {
                            negeri = $('#ddlNegeri').val();
                        }

                        if ($('#ddlNegara').val() == '' || $('#ddlNegara').val() == null) {
                            negara = '-';
                        } else {
                            negara = $('#ddlNegara').val();
                        }

                        if ($('#txtNoAkaunBank').val() == '' || $('#txtNoAkaunBank').val() == null) {
                            noAkaunBank = '-';
                        } else {
                            noAkaunBank = $('#txtNoAkaunBank').val();
                        }

                        if ($('#txtNoPenghutang').val() == "") {
                            penghutang = {
                                penghutang: {
                                    Nama: $('#txtNama').val(),
                                    Id: id,
                                    IdPenghutang: '',
                                    NoTelefon: $('#txtNoTelefon').val(),
                                    Email: $('#txtEmel').val(),
                                    KategoriPenghutang: $('#ddlAddKategoriPenghutang').val(),
                                    Alamat1: alamat1,
                                    Alamat2: alamat2,
                                    KodNegara: negara,
                                    KodNegeri: negeri,
                                    Poskod: poskod,
                                    Bandar: bandar,
                                    KodBank: bank,
                                    NoAkaun: noAkaunBank,
                                }
                            }
                        } else {
                            penghutang = {
                                penghutang: {
                                    Nama: $('#txtNama').val(),
                                    Id: id,
                                    IdPenghutang: $('#txtNoPenghutang').val(),
                                    NoTelefon: $('#txtNoTelefon').val(),
                                    Email: $('#txtEmel').val(),
                                    KategoriPenghutang: $('#ddlAddKategoriPenghutang').val(),
                                    Alamat1: alamat1,
                                    Alamat2: alamat2,
                                    KodNegara: negara,
                                    KodNegeri: negeri,
                                    Poskod: poskod,
                                    Bandar: bandar,
                                    KodBank: bank,
                                    NoAkaun: noAkaunBank,
                                }
                            }
                        }

                        var result = JSON.parse(await ajaxSavePenghutang(penghutang));

                        if (result.Status !== "Failed") {
                            $('#modalPenghutang').modal('toggle');
                            // open modal makluman and show message
                            $('#maklumanModal').modal('toggle');
                            $('#detailMakluman').html(result.Message);
                            clearAllFields();
                            // refresh page after 2 seconds
                            setTimeout(function () {
                                tbl.ajax.reload();
                            }, 2000);
                        } else {
                            // open modal makluman and show message
                            $('#maklumanModal').modal('toggle');
                            $('#detailMakluman').html(result.Message);
                        }
                    });
                    async function ajaxSavePenghutang(category) {
                        return new Promise((resolve, reject) => {
                            $.ajax({
                                url: 'InvoisWS.asmx/SavePenghutang',
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
                </script>
    </contenttemplate>
</asp:Content>