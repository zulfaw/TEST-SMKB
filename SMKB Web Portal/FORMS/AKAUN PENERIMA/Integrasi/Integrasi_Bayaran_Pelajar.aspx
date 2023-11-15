<%@ Page Title="" Language="vb" Async="true" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Integrasi_Bayaran_Pelajar.aspx.vb" Inherits="SMKB_Web_Portal.Integrasi_Bayaran_Pelajar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
    <contenttemplate>
        <style>
            .default-primary {
                background-color: #007bff !important;
                color: white;
            }
            .icon {
                width: 24px; /* Set the width and height according to your preference */
                height: 24px;
                fill: currentColor; /* Allows the SVG to inherit the button's text color */
                margin-right: 10px; /* Adjust spacing between the SVG and text */
            }
        </style>
        <div class="container-fluid mt-3">
            <ul class="nav nav-tabs" id="subTab" role="tablist" >
                <li class="nav-item" role="presentation"  onclick="subTabChange(event)" style="cursor:pointer">
                    <a class="nav-link active" aria-current="page" data-tab="I">Individu</a>
                </li> 
                <li class="nav-item" role="presentation"  onclick="subTabChange(event)" style="cursor:pointer">
                      <a class="nav-link " tabindex="-1"  data-tab="P" aria-disabled="true"  >Penaja</a>
                </li>
            </ul>
        </div>
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <div id="SenaraiKelulusan">
                <div>
                    <div class="modal-body">
                        <div class="modal-header">
                            <h5 class="modal-title">Senarai Penghutang</h5>
                        </div>
                        <!-- Create the dropdown filter -->
                        <div class="search-filter">
                            <div id="search_penaja" class="justify-content-center search_penaja" style="display:none">
                                <div class="form-row form-group row col-sm-12">
                                    <div class="col-sm-3"></div>
                                    <label for="ddlSesiPenaja" class="col-sm-2 col-form-label" style="text-align:right">Sesi:</label>
                                    <div class="col-sm-3">
                                        <select id="ddlSesiPenaja" class="ui search dropdown" name="ddlSesiPenaja"></select>
                                    </div>
                                </div>
                                <br />
                                 <div class="form-row form-group row col-sm-12">
                                    <div class="col-sm-3"></div>
                                    <label for="ddlKategoriPenghutang" class="col-sm-2 col-form-label" style="text-align:right">Penaja:</label>
                                    <div class="col-sm-3">
                                        <select id="ddlKategoriPenghutang" class="ui search dropdown form-control" name="ddlKategoriPenghutang"></select>
                                    </div>
                                     <button id="btnSearchPenaja" runat="server" class="btn btnSearchPenaja" onclick="return beginSearch();"
                                        type="button">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </div>
                            </div>
                            <div id="search_individu" class="justify-content-center search_individu">
                                <div class="form-row form-group row col-sm-12">
                                    <div class="col-sm-3"></div>
                                    <label for="ddlKategoriPelajar" class="col-sm-2 col-form-label" style="text-align:right">Kategori Pelajar:</label>
                                    <div class="col-sm-3">
                                        <select id="ddlKategoriPelajar" class="ui search dropdown" name="ddlKategoriPelajar"></select>
                                    </div>
                                </div>
                                <br />
                                <div class="form-row form-group row col-sm-12">
                                    <div class="col-sm-3"></div>
                                    <label for="ddlSesi" class="col-sm-2 col-form-label" style="text-align:right">Sesi:</label>
                                    <div class="col-sm-3">
                                        <select id="ddlSesi" class="ui search dropdown" name="ddlSesi"></select>
                                    </div>
                                </div>
                                <br />
                                <div class="form-row form-group row col-sm-12">
                                    <div class="col-sm-3"></div>
                                    <label for="ddlFakulti" class="col-sm-2 col-form-label" style="text-align:right">Fakulti:</label>
                                    <div class="col-sm-3">
                                        <select id="ddlFakulti" class="ui search dropdown" name="ddlFakulti"></select>
                                    </div>
                                    <button id="btnSearch" runat="server" class="btn btnSearch" onclick="return beginSearch();"
                                        type="button">
                                        <i class="fa fa-search"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <div id="divIndividu" class="row">
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive ">
                                        <table id="tblDataSenarai" class="table table-striped ">
                                            <thead>
                                                <tr style="width: 100%">
                                                    <th scope="col" style="width: 15%">No. Akaun</th>
                                                    <th scope="col" style="width: 25%">Nama</th>
                                                    <th scope="col" style="width: 10%">No. Rujukan</th>
                                                    <th scope="col" style="width: 28%">Alamat</th>
                                                    <th scope="col" style="width: 10%">No. Telefon</th>
                                                    <th scope="col" style="width: 12%">Tindakan</th>
                                                </tr>
                                            </thead>
                                            <tbody id=" ">
                                            </tbody>
                                        </table>
                                </div>
                            </div>
                        </div>
                        <div id="divPenaja"  class="row" style="display:none"> 
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive ">
                                        <table id="tblDataSenaraiPenaja" class="table table-striped " style="width: 100%!important">
                                            <thead >
                                                <tr style="width: 100%!important">
                                                    <th style="width:5%"><input type="checkbox" id="selectAll"  ><label for="selectAll"></label></th>
                                                    <th scope="col" style="width: 15%">No. Akaun</th>
                                                    <th scope="col" style="width: 25%">Nama</th>
                                                    <th scope="col" style="width: 10%">No. Rujukan</th>
                                                    <th scope="col" style="width: 35%">Tujuan</th>
                                                    <th scope="col" style="width: 10%">Jumlah (RM)</th>
                                                </tr>
                                            </thead>
                                            <tbody id=" ">
                                            </tbody>
                                        </table>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="modal-footer modal-footer--sticky" id="footerpenaja" style="display:none">
                            <div class="form-group col-md-12" align="right">
                                <%--<button type="button" class="btn btn-danger btnTidakLulus">Tidak Lulus</button>--%>
                                <span><b>Jumlah (<span id="stickyJumlahItem" style="margin-right:5px">0</span> item) :RM <span id="stickyJumlah"
                                            style="margin-right:5px">0.00</span></b></span>
                                <button type="button" class="btn" id="showModalButton"><i class="fas fa-angle-up"></i></button>
                                <button type="button" class="btn btn-secondary btnCetakResit" data-toggle="tooltip" data-placement="bottom" title="Cetak Resit" style="display:none"><i class="fa fa-print" aria-hidden="true"></i>Cetak Resit</button>
                                <button type="button" class="btn btn-secondary btnCetak" data-toggle="tooltip" data-placement="bottom" title="Cetak Bil"><i class="fa fa-print" aria-hidden="true"></i>Cetak Bil</button>
                                <button type="button" class="btn btn-success btnBayar" data-toggle="tooltip" data-placement="bottom" title="Buat Bayaran">Bayar</button>
                                <%--<div class="form-row justify-content-end" >
                                    <div class="btn btn-primary btnBayarPenaja" onclick="ShowPopup('2')" >
                                        Bayar
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--modal--%>
        <div class="modal fade" id="Senarai" tabindex="-1" role="dialog"
            aria-labelledby="ModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-dialog-scrollable">
                <div class="modal-content">
                    <div class="modal-header modal-header--sticky">
                        <h5 class="modal-title" id="ModalCenterTitle">Terimaan Bayaran</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-row">
                                    <div class="form-group col-md-3">
                                        <label>No. Bil</label>
                                        <input type="text" class="form-control" placeholder="No. Bil" id="txtnoinv" name="txtnoinv" readonly />
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Kepada</label>
                                        <br />
                                        <div style="display:none">
                                            <select class="form-control ui search dropdown" name="ddlPenghutang" id="ddlPenghutang" >
                                            </select>
                                        </div>
                                        <input type="text" class="form-control" id="txtNamaPenghutang" name="txtNamaPenghutang" readonly/>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Tarikh Mula</label>
                                        <input type="text" class="form-control" name="tkhMula" id="tkhMula" readonly>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Tarikh Tamat</label>
                                        <input type="text" class="form-control" id="tkhTamat" name="tkhTamat" readonly>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-row">
                                    <div class="form-group col-md-3">
                                        <label>Berkontrak</label>
                                        <div class="radio-btn-form" id="rdKontrak" name="rdKontrak" >
                                            <div class="form-check form-check-inline radio-size">
                                                <input type="radio" id="rdYa" name="inlineRadioOptions" value="1" />&nbsp;  Ya
                                            </div>
                                            <div class="form-check form-check-inline radio-size">
                                                <input type="radio" id="rdTidak" name="inlineRadioOptions" value="0" />&nbsp;  Tidak
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Jenis Urusniaga</label>
                                        <div style="display:none"><select id="ddlUrusniaga" class="ui search dropdown" name="ddlUrusniaga" ></select></div>
                                        <input type="text" class="form-control" id="txtUrusniaga" name="txtUrusniaga" readonly/>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Tujuan</label>
                                        <textarea class="form-control" rows="1" name="txtTujuan" id="txtTujuan" readonly></textarea>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <h6>Transaksi</h6>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="transaction-table table-responsive">
                                        <table class="table table-striped" id="tblData" style="width: 100%;">
                                            <thead>
                                                <tr style="width: 100%; text-align: center;">
                                                    <th scope="col" style="width: 10%;vertical-align:middle">Vot</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle">Kod PTJ</th>
                                                    <th scope="col" style="width: 5%;vertical-align:middle">Kumpulan Wang</th>
                                                    <th scope="col" style="width: 5%;vertical-align:middle">Kod Operasi</th>
                                                    <th scope="col" style="width: 5%;vertical-align:middle">Kod Projek</th>
                                                    <th scope="col" style="width: 15%;vertical-align:middle">Perkara</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle;display:none">Kuantiti</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle;display:none">Harga Seunit (RM)</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle;display:none">Cukai (%)</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle;display:none">Diskaun (%)</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle">Jumlah (RM)</th>
                                                    <%--<th scope="col" style="width: 3%">Tindakan</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody id="tableID">
                                                <tr style="display: none; width: 100%">
                                                    <td>
                                                        <%-- kalau nak paparkan vot dropdown semula, kena buang div dibawah --%>
                                                        <div style="display:none">
                                                            <select class="ui search dropdown vot-carian-list" name="ddlVotCarian" id="ddlVotCarian">
                                                            </select>
                                                        </div>
                                                        <label id="lblvot" name="lblvot" class="label-vot-list"></label>
                                                        <label id="hidlblvot" name="hidlblvot" class="hid-vot-list" style="display:none"></label>
                                                        <input type="hidden" class="data-id" value="" />
                                                    </td>
                                                    <td>
                                                        <label id="lblPTj" name="lblPTj" class="label-ptj-list"></label>
                                                        <label id="HidlblPTj" name="HidlblPTj" class="Hid-ptj-list" style="visibility: hidden; display: none"></label>
                                                    </td>
                                                    <td>
                                                        <label id="lblKw" name="lblKw" class="label-kw-list"></label>
                                                        <label id="HidlblKw" name="HidlblKw" class="Hid-kw-list" style="visibility: hidden; display: none"></label>
                                                    </td>
                                                    <td>
                                                        <label id="lblKo" name="lblKo" class="label-ko-list"></label>
                                                        <label id="HidlblKo" name="HidlblKo" class="Hid-ko-list" style="visibility: hidden; display: none"></label>
                                                    </td>
                                                    <td>
                                                        <label id="lblKp" name="lblKp" class="label-kp-list"></label>
                                                        <label id="HidlblKp" name="HidlblKp" class="Hid-kp-list" style="visibility: hidden; display: none"></label>
                                                    </td>
                                                    <td>
                                                        <input class="form-control details" type="text" id="txtPerkara" name="txtPerkara" style="display:none" />
                                                        <label class="lblPerkaraDetails" id="lblPerkara" name="lblPerkara"></label>
                                                    </td>
                                                    <td style="display:none">
                                                        <input type="number" class="form-control underline-input multi quantity" placeholder="0" id="quantity" name="quantity" style="text-align: center;display:none" />
                                                        <label class="lblKuantiti_" id="lblKuantiti" name="lblKuantiti" style="display:block;text-align:center"></label>
                                                    </td>
                                                    <td style="display:none">
                                                        <input type="number" class="form-control underline-input multi price" placeholder="0.00" id="price" name="price" style="text-align: right;display:none" />
                                                        <label class="lblHarga_" id="lblHarga" name="lblHarga" style="display:block;text-align:right"></label>
                                                    </td>
                                                    <td style="display:none">
                                                        <input type="number" class="form-control underline-input multi cukai" placeholder="0.00" id="cukai" name="cukai" style="text-align: center;display:none" />
                                                        <label class="lblCukai_" id="lblCukai" name="lblCukai" style="display:block;text-align:center"></label>
                                                        <input type="number" class="form-control underline-input multi JUMcukai" placeholder="0.00" id="JUMcukai" name="JUMcukai" style="text-align: center;display: none" />
                                                    </td>
                                                    <td style="display:none">
                                                        <input type="number" class="form-control underline-input multi diskaun" placeholder="0.00" id="diskaun" name="diskaun" style="text-align: center;display:none" />
                                                        <label class="lblDiskaun_" id="lblDiskaun" name="lblDiskaun" style="display:block;text-align:center"></label>
                                                        <input type="number" class="form-control underline-input multi JUMdiskaun" placeholder="0.00" id="JUMdiskaun" name="JUMdiskaun" style="text-align: center;display: none" />
                                                    </td>
                                                    <td>
                                                        <input class="form-control underline-input amount" id="amount" name="amount" style="text-align: right;display:none" placeholder="0.00" />
                                                        <label class="lblAmount_" id="lblAmount" name="lblAmount" style="display:block;text-align:right"></label>
                                                        <input class="form-control underline-input amountwocukai" id="amountwocukai" name="amountwocukai" style="text-align: right; display: none" placeholder="0.00" /></td>
                                                    <%--<td class="tindakan">
                                                    <button class="btn btnDelete">
                                                        <i class="fa fa-trash" style="color: red"></i>
                                                    </button>
                                                    <button class="btn"><i class="fa fa-trash"></i> Trash</button>
                                                </td>--%>
                                                </tr>

                                            </tbody>
                                            <%--<tfoot >
                                            <tr>
                                                
                                                <td colspan="8" style="text-align: right; font-size: medium;border-right:hidden"><i>Jumlah Cukai (RM)</i></td>
                                                <td colspan="1" style="text-align:center;border-right:hidden">
                                                    <input class="form-control underline-input" id="TotalTax" name="TotalTax" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly/>
                                                </td>
                                                <td colspan="1"></td>
                                            </tr>
                                            <tr>
                                                <td colspan="10" style="text-align: right; font-size: medium;border-right:hidden"><i>Jumlah Diskaun (RM)</i></td>
                                                <td colspan="1" style="text-align:center;border-right:hidden">
                                                    <input class="form-control underline-input" id="TotalDiskaun" name="TotalDiskaun" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly/>
                                                </td>
                                                <td colspan="1"></td>
                                            </tr>
                                            <tr>
                                                <th colspan="10" style="text-align: right; font-size: large;border-right:hidden" class="">JUMLAH (RM)</th>
                                                <td colspan="1" style="text-align:center;border-right:hidden">
                                                    <input class="form-control underline-input" id="total" name="total" style="text-align: right; font-size: large; font-weight: bold" placeholder="0.00" readonly/>
                                                </td>
                                                <td colspan="1"></td>
                                            </tr>
                                        </tfoot>--%>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer modal-footer--sticky">
                        <table class="" style="width: 100%; border: none">
                            <tr style="border-top: none">
                                <td style="width: 1%; border-top: none"></td>
                                <td style="width: 20%; border-top: none"></td>
                                <td style="width: 50%; border-top: none"></td>
                                <td style="width: 15%; border-top: none"></td>
                                <td style="width: 2%; border-top: none"></td>
                                <td style="width: 10%; border-top: none"></td>
                                <td style="width: 2%; border-top: none"></td>
                            </tr>
                            <tr style="border-top: none">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align: right; font-size: medium;">Jumlah<br />
                                    <%--<i>( Tolak Diskaun RM
                                        <input class="underline-input" id="TotalDiskaun" name="TotalDiskaun" style="border: none; width: 20%; font-style: italic" placeholder="0.00" />
                                                    )</i>--%></td>
                                <td></td>
                                <td style="text-align: right">
                                    <input class="form-control underline-input" id="totalwoCukai" name="totalwoCukai" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly /></td>
                                <td></td>
                            </tr>
                            <tr style="border-top: none">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align: right; font-size: medium;">Jumlah Cukai</td>
                                <td></td>
                                <td style="text-align: right">
                                    <input class="form-control underline-input" id="TotalTax" name="TotalTax" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly /></td>
                                <td></td>
                            </tr>
                            <tr style="border-top: none">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align: right; font-size: medium;">Jumlah Diskaun</td>
                                <td></td>
                                <td style="text-align: right">
                                    <input class="form-control underline-input" id="TotalDiskaun" name="TotalDiskaun" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly /></td>
                                <td></td>
                            </tr>
                            <tr style="border-top: none">
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align: right; font-size: large">JUMLAH (RM)</td>
                                <td></td>
                                <td style="text-align: right">
                                    <input class="form-control underline-input" id="total" name="total" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly /></td>
                                <td></td>
                            </tr>
                        </table>
                        <div class="form-group col-md-12" align="right">
                            <button type="button" class="btn btn-danger btnTidakLulus">Tidak Lulus</button>
                            <button type="button" class="btn btn-success btnLulus" data-toggle="tooltip" data-placement="bottom" title="Lulus">Lulus</button>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>
        <!-- Confirmation Modal-->
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
                        Anda pasti ingin meluluskan rekod ini?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger"
                            data-dismiss="modal">Tidak</button>
                        <button type="button" class="btn default-primary btnYaLulus">Ya</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Confirmation Modal Tidak Lulus-->
        <div class="modal fade" id="confirmationModalxLulus" tabindex="-1" role="dialog"
            aria-labelledby="confirmationModalLabelxLulus" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="confirmationModalLabelxLulus">Pengesahan</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        Anda pasti untuk tidak meluluskan rekod ini?
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger"
                            data-dismiss="modal">Tidak</button>
                        <button type="button" class="btn default-primary btnYaxLulus">Ya</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Makluman Modal-->
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
    </contenttemplate>
    <script type="text/javascript">
        $('#ddlKategoriPenghutang').dropdown({
            selectOnKeydown: true,
            fullTextSearch: true,
            apiSettings: {
                url: 'Integrasi.asmx/GetPenajaList?q={query}',
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

        $('#ddlKategoriPelajar').dropdown({
            selectOnKeydown: true,
            fullTextSearch: true,
            apiSettings: {
                url: '<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Invois/Transaksi_PelajarWS.asmx/GetKategoriPenghutangList?q={query}")%>',
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

        
        $('#ddlSesiPenaja').dropdown({
            selectOnKeydown: true,
            fullTextSearch: true,
            apiSettings: {
                url: '<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Invois/Transaksi_PelajarWS.asmx/GetSesiList?q={query}'")%>,
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
                url: '<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Invois/Transaksi_PelajarWS.asmx/GetSesiList?q={query}'")%>,
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
                url: '<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Invois/Transaksi_PelajarWS.asmx/GetfakultiList?q={query}'")%>,
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
        var editMode = false
        function ShowDetails(id, status) {
            if (id !== "") {
                if (status !== "") {
                    if (status == "NOTDONE") {
                        location.replace('<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Integrasi/Integrasi_Pelajar_Terperinci.aspx")%>?nomatrik=' + id + '&status=03', '_blank');
                    } else if (status == "DONE") {
                        location.replace('<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Terimaan/Integrasi_Pelajar_Terperinci.aspx")%>?nomatrik=' + id + '&status=04', '_blank');
                    }
                    
                }
            }

        }
        function ShowPopup(elm) {

            //alert("test");
            if (elm == "1") {

                $('#Senarai').modal('toggle');


            }
            else if (elm == "2") {

                $(".modal-body div").val("");
                $('#Senarai').modal('toggle');

            }
            
        }

        function toggleEdit(mode) {
            editMode = mode
        }

        function switchToTab(tab) {
            console.log("swtich" + tab)
            //const secondTab = document.querySelector('[data-tab="' + id + '"] > a');
            $("#subTab").find("[data-tab='" + tab + "'] > a").parent().click()
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
            //RefreshTable();
            console.log(e)

            Array.from(e.target.parentElement.parentElement.getElementsByClassName("active")).forEach(e => {
                e.classList.remove("active")
            })
            e.target.classList.add("active")
            showFormTab()
        }
        //function RefreshTable() {
        //    $("#tblDataSenarai").load("Integrasi_Bayaran_Pelajat.aspx #tblDataSenarai");
        //}
        const searchParams = new URLSearchParams(window.location.search);
        var kategori = searchParams.get('kategori')
        var KatPelajar = searchParams.get('startDate')
        var Sesi = searchParams.get('endDate')
        var Fakulti = searchParams.get('categoryFilter')
        if (Fakulti !== null) {
            $('#ddlFakulti').val(Fakulti)
           // var text = result[0].text;
            var value = Fakulti;
            var label = Fakulti;
            var option = $('<option>').attr('value', value).text(label);
            $('#ddlFakulti').append(option);
        }
        if (Sesi !== null) {
            $('#ddlSesi').val(Sesi)
            // var text = result[0].text;
            var value = Sesi;
            var label = Sesi;
            var option = $('<option>').attr('value', value).text(label);
            $('#ddlSesi').append(option);
        }
        if (KatPelajar !== null) {
            $('#ddlKategoriPelajar').val(KatPelajar)
            // var text = result[0].text;
            var label
            var value = KatPelajar;
            if (KatPelajar == "PG") {
                label = "PELAJAR PASCA SISWAZAH";
            } else if (KatPelajar == "PL") {
                label = "PELAJAR SARJANA MUDA";
            } else if (KatPelajar == "PH") {
                label = "PELAJAR SEPANJANG HAYAT";
            }
             
            var option = $('<option>').attr('value', value).text(label);
            $('#ddlKategoriPelajar').append(option);
        }

        //$('#ddlKategoriPelajar').val(KatPelajar)
        //$('#ddlSesi').val(Sesi)
        
        console.log("kategori" + kategori)
        if (kategori == 'IDN') {
            $("#divIndividu").show()
            $("#divPenaja").hide()
            $("#search_penaja").hide()
            $("#search_individu").show()
            $('.btnBayarPenaja').hide()
            $('#footerpenaja').hide()
            const secondTab = document.querySelector('[data-tab="I"]');
            secondTab.click();
            //switchToTab("I")
        } else if (kategori == 'PN'){
            $("#divIndividu").hide()
            $("#divPenaja").show()
            $("#search_penaja").show()
            $("#search_individu").hide()
            $('.btnBayarPenaja').show()
            $('#footerpenaja').show()
            const secondTab = document.querySelector('[data-tab="P"]');
            secondTab.click();
            //switchToTab("P")
        }

        //function activateTab(id) {
        //    alert(id)
        //    if (id !== 'null') {
        //        const secondTab = document.querySelector('[data-tab="' + id + '"] > a');
        //        secondTab.click();
        //    }
        //}
        function showFormTab() {
            //check active tab
            
            //if (kategori=) { }
            
            let tab = $("#subTab").find(".active")[0].dataset.tab
            //alert(tab)
            if (tab == "I" ) {
                kategoriForm = "I"
                $("#divIndividu").show()
                $("#divPenaja").hide()
                $("#search_penaja").hide()
                $("#search_individu").show()
                $('.btnBayarPenaja').hide()
                $('#footerpenaja').hide()
               // activateTab("I")
                clearAllRows_senarai()
            }
            if (tab == "P" ) {
                kategoriForm = "P"
                $("#divIndividu").hide()
                $("#divPenaja").show()
                $("#search_penaja").show()
                $("#search_individu").hide()
                clearAllRows_senarai()
                $('.btnBayarPenaja').show()
                $('#footerpenaja').show()
               // activateTab("P")
            }
            //clearForm()
        }
        var tbl = null
        var isClicked = false;
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
                    "url": "Integrasi.asmx/LoadOrderRecord_SenaraiLulusInvois",
                    "method": 'POST',
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    },
                    "data": function () {
                        //Filter date bermula dari sini - 20 julai 2023
                        var startDate = $('#ddlKategoriPelajar').val()
                        var endDate = $('#ddlSesi').val()
                        return JSON.stringify({
                            category_filter: $('#ddlFakulti').val(),
                            isClicked: isClicked,
                            tkhMula: startDate,
                            tkhTamat: endDate
                        })
                        //akhir sini
                    }

                },
                "rowCallback": function (row, data) {
                    // Add hover effect
                    //$(row).hover(function () {
                    //    $(this).addClass("hover pe-auto bg-warning");
                    //}, function () {
                    //    $(this).removeClass("hover pe-auto bg-warning");
                    //});

                },
                "drawCallback": function (settings) {
                    // Your function to be called after loading data
                    close_loader();
                },
                "columns": [
                    {
                        "data": "SMP01_KP",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {

                                return data;

                            }

                            var link = `<td >
                                            <label id="lblNoAkaun" name="lblNoAkaun" class="lblNoAkaun" value="${data}" >${data}</label>
                                        </td>`;
                            return link;
                        }
                    },
                    { "data": "SMP01_Nama" },
                    { "data": "SMP01_Nomatrik" },
                    { "data": "ALAMAT" },
                    { "data": "SMP01_NoTelBimBit" },
                    {
                        "data": "SMP01_Nomatrik",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {
                                return data;
                            }

                            var link = `<button id="btnPaid" runat="server" class="btn btnPaid" value="${data}" type="button" data-toggle="tooltip" data-placement="top" title="Bil Berbayar" onclick="ShowDetails('${data}','DONE')">
                                                    <img src="../../../Content/icon/money-check-dollar-svgrepo-com.svg" style="width:30px;height:30px"/>
                                        </button>
                                        <button id="btnUnPaid" runat="server" class="btn btnUnPaid" value="${data}" type="button" data-toggle="tooltip" data-placement="top" title="Bil Belum Terima" onclick="ShowDetails('${data}','NOTDONE')">
                                                    <img src="../../../Content/icon/money-check-dollar-pen-svgrepo-com.svg" style="width:30px;height:30px"/>
                                        </button>`;
                            return link;
                        }
                    }

                ],
            });

            $('.btnSearch').click(async function () {
                //show_loader();
                //isClicked = true;
                //tbl.ajax.reload();
                // get the categoryFilter value and append it in the url
                var categoryFilter = $('#ddlFakulti').val();

                if (categoryFilter !== " ") {
                    // get the startDate value and append it in the url
                    var startDate = $('#ddlKategoriPelajar').val()

                    // get the endDate value and append it in the url
                    var endDate = $('#ddlSesi').val()
                    var kategori = 'IND'

                    const url = `<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Integrasi/Integrasi_Bayaran_Pelajar.aspx?kodsubmenu=btnMenu121801")%>&categoryFilter=${categoryFilter}&startDate=${startDate}&endDate=${endDate}&kategori=${kategori}`;

                    localStorage.setItem('redirectIntegrasiPelajarUrl', url);
                    location.replace(url, '_blank');
                } else {

                    const url = `<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Integrasi/Integrasi_Bayaran_Pelajar.aspx?kodsubmenu=btnMenu121801")%>&categoryFilter=${categoryFilter}`;
                    localStorage.setItem('redirectIntegrasiPelajarUrl', url);
                    location.replace(url, '_blank');
                }
            })

            $('.btnSearchPenaja').click(async function () {
                //show_loader();
                //isClicked = true;
                //tbl.ajax.reload();
                // get the categoryFilter value and append it in the url
                var categoryFilter = $('#ddlKategoriPenghutang').val();
                var sesi = $('#ddlSesiPenaja').val();
                if (categoryFilter !== " ") {
                    // get the startDate value and append it in the url
                   // var startDate = $('#ddlKategoriPelajar').val()

                    // get the endDate value and append it in the url
                    //var endDate = $('#ddlSesi').val()
                    var kategori = 'PN'

                    const url = `<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Integrasi/Integrasi_Bayaran_Pelajar.aspx?kodsubmenu=btnMenu121801")%>&categoryFilter=${categoryFilter}&kategori=${kategori}&sesi=${sesi}`;

                    localStorage.setItem('redirectIntegrasiPelajarUrl', url);
                    location.replace(url, '_blank');
                } else {

                    const url = `<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Integrasi/Integrasi_Bayaran_Pelajar.aspx?kodsubmenu=btnMenu121801")%>&categoryFilter=${categoryFilter}`;
                    localStorage.setItem('redirectIntegrasiPelajarUrl', url);
                    location.replace(url, '_blank');
                }
            })


            const searchParams = new URLSearchParams(window.location.search);
            if (searchParams.has('categoryFilter')) {
                loadTable();
                //alert(searchParams.get('categoryFilter'))
                $('#ddlFakulti').val(searchParams.get('categoryFilter'));
                $('#ddlKategoriPenghutang').val(searchParams.get('categoryFilter'));

                if (searchParams.get('categoryFilter') !== " ") {
                    // trigger on change event
                    $('#ddlFakulti').trigger('change');
                    $('#ddlKategoriPenghutang').trigger('change');
                    $('#ddlKategoriPelajar').val(searchParams.get('startDate'));
                    $('#ddlSesi').val(searchParams.get('endDate'));

                }
            }

            function loadTable() {
                show_loader();
                isClicked = true;
                tbl.ajax.reload();
            }

            tbl_Penaja = $("#tblDataSenaraiPenaja").DataTable({
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
                    "url": "Integrasi.asmx/LoadOrderRecord_SenaraiPelajarPenaja",
                    "method": 'POST',
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    },
                    "data": function () {
                        //Filter date bermula dari sini - 20 julai 2023
                        var startDate = $('#ddlKategoriPelajar').val()
                        var endDate = $('#ddlSesiPenaja').val()
                        return JSON.stringify({
                            category_filter: endDate,
                            isClicked: isClicked,
                            tkhMula: startDate,
                            tkhTamat: endDate
                        })
                        //akhir sini
                    }

                },
                "rowCallback": function (row, data) {
                    // Add hover effect
                    //$(row).hover(function () {
                    //    $(this).addClass("hover pe-auto bg-warning");
                    //}, function () {
                    //    $(this).removeClass("hover pe-auto bg-warning");
                    //});

                },
                "drawCallback": function (settings) {
                    // Your function to be called after loading data
                    close_loader();
                },
                "columns": [
                    {
                        "data": "Kod_Penghutang",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {

                                return data;

                            }

                            var link = `<td >
                                            <input type="checkbox" id="select${data}" value="${data}" class="rowCheckbox">
                                            <label for="select"></label>
                                        </td>`;
                            return link;
                        }
                    },
                    
                    {
                        "data": "Kod_Penghutang",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {

                                return data;

                            }

                            var link = `<td >
                                <label id="lblNoAkaun" name="lblNoAkaun" class="lblNoAkaun" value="${data}" >${data}</label>
                            </td>`;
                            return link;
                        }
                    },
                    { "data": "Nama_Penghutang" },
                    { "data": "No_Rujukan" },
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
                    }

                ],
            });
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

        $(document).ready(function () {
            

            $('#ddlJenTransaksi').dropdown({
                fullTextSearch: false,
                apiSettings: {
                    url: 'Transaksi_WS.asmx/GetJenisTransaksi?q={query}',
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

        async function clearAllRows_senarai() {
            $('#tblDataSenarai' + " > tbody > tr ").each(function (index, obj) {
                if (index > 0) {
                    obj.remove();
                }
            })
            $('#tblDataSenaraiPenaja' + " > tbody > tr ").each(function (index, obj) {
                if (index > 0) {
                    obj.remove();
                }
            })
            
        }

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

        async function paparSenarai(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblDataSenarai');
            //alert("hai")
            if (objOrder !== null && objOrder !== undefined) {
                totalClone = objOrder.length;

            }
            console.log(objOrder)

            while (counter <= totalClone) {

                var row = $('#tblDataSenarai tbody>tr:first').clone();
                row.attr("style", "");
                var val = "";

                $('#tblDataSenarai tbody').append(row);

                if (objOrder !== null && objOrder !== undefined) {

                    if (counter <= objOrder.length) {
                        await setValueToRow(row, objOrder[counter - 1]);
                    }
                }

                counter += 1;
            }
        }

        async function setValueToRow(row, orderDetail) {
            var no = $(row).find("td > .lblNo");
            var Penghutang = $(row).find("td > .lblPenghutang");
            var TkhMohon = $(row).find("td > .lblTkhMohon");
            var TkhMula = $(row).find("td > .lblTkhMula");
            var TkhTamat = $(row).find("td > .lblTkhTamat");
            var JnsUrusNiaga = $(row).find("td > .lblJnsUrus");
            var Tujuan = $(row).find("td > .lblTujuan");
            var Jumlah = $(row).find("td > .lblJumlah");
            var jumlah_ = orderDetail.Jumlah;
            //console.log(jumlah_)
            if (jumlah_ !== null) {
                Jumlah.html(orderDetail.Jumlah.toFixed(2));
            } else {
                Jumlah.html(orderDetail.Jumlah);
            }
            no.html(orderDetail.No_Invois);
            Penghutang.html(orderDetail.Nama_Penghutang);
            TkhMula.html(orderDetail.Tkh_Mula);
            TkhTamat.html(orderDetail.Tkh_Tamat);
            JnsUrusNiaga.html(orderDetail.UrusNiaga);
            Tujuan.html(orderDetail.Tujuan);
            TkhMohon.html(orderDetail.TKHMOHON);

        }

        // add clickable event in DataTable row
        async function rowClickHandler(id) {
            event.preventDefault();

            if (id !== "") {
                // open modal
                $('#Senarai').modal('toggle');

                //BACA HEADER JURNAL
                var recordHdr = await AjaxGetRecordHdrJurnal(id);
                await AddRowHeader(null, recordHdr);

                //BACA DETAIL JURNAL
                var record = await AjaxGetRecordJurnal(id);
                await clearAllRows();
                await AddRow(null, record);
            }
        }

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
                await AddRowHeader(null, recordHdr);

                //BACA DETAIL JURNAL
                var record = await AjaxGetRecordJurnal(id);
                await clearAllRows();
                await AddRow(null, record);
            }

            return false;
        })

        $('#selectAll').on('click', async function () {
            
            var checkboxes = $('.rowCheckbox');
            //alert("check" + checkboxes)
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = this.checked;
            }
        })

        $('#tblDataSenaraiPenaja tbody').on('change', 'input[type="checkbox"]', function () {
            //alert("hello")
            if (!this.checked) {
                $('#selectAll').prop('checked', false);
            } else {
                var allChecked = $('td input[type="checkbox"]', table.rows().nodes()).length ===
                    $('td input[type="checkbox"]:checked', table.rows().nodes()).length;
                $('#selectAll').prop('checked', allChecked);
            }
        });
        //$('#tblDataSenaraiPenaja thead').on('change', '#selectAll', function () {
        //    alert("haii")
        //    var checked = this.checked;
        //    $('td input[type="checkbox"]', table.rows().nodes()).prop('checked', checked);
        //});

        async function AjaxGetRecordHdrJurnal(id) {

            try {

                const response = await fetch('../Invois/SenaraiKelulusanWS.asmx/LoadHdrInvois', {
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

                const response = await fetch('../Invois/SenaraiKelulusanWS.asmx/LoadRecordInvois', {
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
                console.log(totalClone)
                //if (totalClone < 5) {
                //    totalClone = 5;
                //}
            }

            while (counter <= totalClone) {
                curNumObject += 1;
                var newCarianVot = "ddlVotCarian" + curNumObject;

                var row = $('#tblData tbody>tr:first').clone();
                var votcarianlist = $(row).find(".vot-carian-list").attr("id", newCarianVot);
                console.log(row)
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

                    }
                }
                counter += 1;
            }
        }

        async function setValueToRow_Transaksi(row, orderDetail) {

            //console.log(orderDetail)
            var ddl = $(row).find("td > .vot-carian-list");
            var ddlSearch = $(row).find("td > .vot-carian-list > .search");
            var ddlText = $(row).find("td > .vot-carian-list > .text");
            var selectObj = $(row).find("td > .vot-carian-list > select");
            $(ddl).dropdown('set selected', orderDetail.Kod_Vot);
            selectObj.append("<option value = '" + orderDetail.Kod_Vot + "'>" + orderDetail.ButiranVot + "</option>")

            var butirvot = $(row).find("td > .label-vot-list");
            butirvot.html(orderDetail.ButiranVot);
            
            var hidlblvot = $(row).find("td > .hid-vot-list");
            hidlblvot.html(orderDetail.Kod_Vot);

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

            var lblPerkara = $(row).find("td > .lblPerkaraDetails");
            lblPerkara.html(orderDetail.Perkara);

            var details = $(row).find("td > .details");
            details.val(orderDetail.Perkara);

            var lblKuantiti = $(row).find("td > .lblKuantiti_");
            lblKuantiti.html(orderDetail.Kuantiti);

            var quantity = $(row).find("td > .quantity");
            quantity.val(orderDetail.Kuantiti);

            var lblCukai = $(row).find("td > .lblCukai_");
            lblCukai.html(orderDetail.Cukai.toFixed(2));

            var cukai = $(row).find("td > .cukai");
            cukai.val(orderDetail.Cukai.toFixed(2));

            var lblHarga = $(row).find("td > .lblHarga_");
            lblHarga.html(orderDetail.Kadar_Harga.toFixed(2));

            var kdr_hrga = $(row).find("td > .price");
            kdr_hrga.val(orderDetail.Kadar_Harga.toFixed(2));

            var lblDiskaun = $(row).find("td > .lblDiskaun_");
            lblDiskaun.html(orderDetail.Diskaun.toFixed(2));

            var diskaun = $(row).find("td > .diskaun");
            diskaun.val(orderDetail.Diskaun.toFixed(2));

            var lblAmount = $(row).find("td > .lblAmount_");
            lblAmount.html(orderDetail.Jumlah.toFixed(2));

            var amount = $(row).find("td > .amount");
            amount.val(orderDetail.Jumlah.toFixed(2));

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

        async function setValueToRow_HdrJurnal(orderDetail) {

            $('#txtnoinv').val(orderDetail.No_Bil)
            //$('#txtNoRujukan').val(orderDetail.No_Rujukan)
            $('#txtNamaPenghutang').val(orderDetail.Nama_Penghutang)

            $('#tkhMula').val(orderDetail.Tkh_Mula)
            $('#tkhTamat').val(orderDetail.Tkh_Tamat)
            //$('#rdKontrak').val(orderDetail.Kontrak)
            $('#txtUrusniaga').val(orderDetail.Butiran)
            $('#txtTujuan').val(orderDetail.Tujuan)

            var newId = $('#ddlJenTransaksi')

            //await initDropdownPtj(newId)
            //$(newId).api("query");

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
            $(ddlUrusniaga).dropdown('set selected', orderDetail.Jenis_Urusniaga);
            selectObj_JenisTransaksi.append("<option value = '" + orderDetail.Jenis_Urusniaga + "'>" + orderDetail.Butiran + "</option>")

            if (orderDetail.Kontrak == '0') {
                //$('[id=rdTIDAK]')[0].checked = true
                $(':radio[name=inlineRadioOptions][value="0"]').prop('checked', true);
                $(':radio[name=inlineRadioOptions][value="1"]').prop('checked', false);
                //$('#rdTIDAK').val(':checked');
                //$('#rdTIDAK').is('checked:checked');
            } else {
                //$('#rdYA').is('checked:checked');
                $(':radio[name=inlineRadioOptions][value="1"]').prop('checked', true);
                $(':radio[name=inlineRadioOptions][value="0"]').prop('checked', false);
            }
        }

        async function clearAllRows() {
            $(tableID + " > tbody > tr ").each(function (index, obj) {
                if (index > 0) {
                    obj.remove();
                }
            })
            $(totalID).val("0.00");

        }

        async function initCarianVot(id) {
            $('#' + id).dropdown({
                fullTextSearch: true,
                apiSettings: {
                    url: '../Invois/SenaraiKelulusanWS.asmx/GetCarianVotList?q={query}',
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
            grandTotal.val(curTotal.toFixed(2));
        }

        $('.btnLulus').click(async function () {
            // check every required field
            //if ($('#ddlKategoriPenghutang').val() == "" || $('#ddlUrusniaga').val() == "" || $('#ddlPenghutang').val() == "" || $("input[name='inlineRadioOptions']:checked").val() == "" || $('#txtTujuan').val() == "") {
                // open modal makluman and show message
                //$('#maklumanModalBil').modal('toggle');
                //$('#detailMaklumanBil').html("Sila isi semua ruangan yang bertanda *");
            //} else {
                // open modal confirmation
                $('#confirmationModal').modal('toggle');
            //}
        })

        $('.btnYaLulus').click(async function () {

            //close modal confirmation
            $('#confirmationModal').modal('toggle');

            var jumRecord = 0;
            var acceptedRecord = 0;
            var msg = "";
            var newOrder = {
                order: {
                    OrderID: $('#txtnoinv').val(),
                    PenghutangID: $('#ddlPenghutang').val(),
                    //TkhMohon: $('#lblTkhMohon').val(),
                    TkhMula: $('#tkhMula').val(),
                    TkhTamat: $('#tkhTamat').val(),
                    Kontrak: $("input[name='inlineRadioOptions']:checked").val(),
                    JenisUrusniaga: $('#ddlUrusniaga').val(),
                    Tujuan: $('#txtTujuan').val(),
                    Jumlah: $('#total').val(),
                    OrderDetails: []
                }
            }
            $('.vot-carian-list').each(function (index, obj) {
                if (index > 0) {
                    var tcell = $(obj).closest("td");
                    var orderDetail = {
                        OrderID: $('#txtnoinv').val(),
                        PenghutangID: $('#ddlPenghutang').val(),
                        ddlVot: $('.hid-vot-list').eq(index).html(),
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

            console.log(newOrder)
            var result = await ajaxSaveOrder(newOrder);
            //console.log(result)
            if (result.Status !== "5") {
                //$('#modalPenghutang').modal('toggle');
                // open modal makluman and show message
                $('#maklumanModal').modal('toggle');
                $('#detailMakluman').html("Transaksi berjaya.");
                //clearAllFields();
                await clearAllRows();
                // refresh page after 2 seconds
                setTimeout(function () {
                    window.location.reload();
                }, 2000);
            } else {
                // open modal makluman and show message
                $('#maklumanModal').modal('toggle');
                $('#detailMakluman').html("Transaksi disimpan.");
            }
            //alert("Rekod Berjaya Disimpan");
            //$('#orderid').val(result.Payload.OrderID)
            //loadExistingRecords();
            
            //AddRow(5);
            
        });
        $('.btnYaxLulus').click(async function () {

            //close modal confirmation
            $('#confirmationModalxLulus').modal('toggle');

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
                    OrderDetails: []
                }
            }
            $('.vot-carian-list').each(function (index, obj) {
                if (index > 0) {
                    var tcell = $(obj).closest("td");
                    var orderDetail = {
                        OrderID: $('#txtnoinv').val(),
                        PenghutangID: $('#ddlPenghutang').val(),
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
            var result = JSON.parse(await ajaxRejectOrder(newOrder));
            //alert(result.Message);
            if (result.Status !== "Failed") {
                //$('#modalPenghutang').modal('toggle');
                // open modal makluman and show message
                $('#maklumanModal').modal('toggle');
                $('#detailMakluman').html(result.Message);
                await clearAllRows();
                // refresh page after 2 seconds
                setTimeout(function () {
                    window.location.reload();
                }, 2000);
            } else {
                // open modal makluman and show message
                $('#maklumanModal').modal('toggle');
                $('#detailMakluman').html(result.Message);
            }
            //$('#orderid').val(result.Payload.OrderID)
            //loadExistingRecords();
            //await clearAllRows();
            //AddRow(5);
            //window.location.reload();
        });


        async function ajaxSaveOrder(id) {

            return new Promise((resolve, reject) => {
                $.ajax({

                    url: '../Invois/SenaraiKelulusanWS.asmx/SaveLulus',
                    method: 'POST',
                    data: JSON.stringify(id),
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
            console.log("tst")
        }
        async function ajaxRejectOrder(id) {

            return new Promise((resolve, reject) => {
                $.ajax({

                    url: '../Invois/SenaraiKelulusanWS.asmx/RejectLulus',
                    method: 'POST',
                    data: JSON.stringify(id),
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
        function beginSearch() {
            tbl.ajax.reload();
        }
    </script>
</asp:Content>
