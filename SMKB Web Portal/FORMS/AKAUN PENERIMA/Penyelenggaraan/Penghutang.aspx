<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Penghutang.aspx.vb" Inherits="SMKB_Web_Portal.Penghutang" %>


<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script type="text/javascript">
        function ShowPopup(elm) {

            //alert("test");
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
            <%-- DIV PENDAFTARAN INVOIS --%>
            <div id="divpendaftaraninv" runat="server" visible="true">
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
                        <%--<div class="row">
                            <div class="col-md-6">
                                <div class="form-row">
                                    <div class="form-group col-md-3">
                                        <label>No. Resit</label>
                                        <input type="text" class="form-control" placeholder="No. Resit" id="txtnoResit" name="txtnoResit" readonly />
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>No. Rujukan</label>
                                        <input type="text" class="form-control" placeholder="No. Rujukan" id="txtnoinv" name="txtnoinv" readonly />
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Jenis Urusniaga <i style="color:red">*</i></label>
                                        <select id="ddlUrusniaga" class="ui search dropdown" name="ddlUrusniaga"></select>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Mod Terimaan <i style="color:red">*</i></label>
                                        <select id="ddlModTerima" class="ui search dropdown" name="ddlModTerima"></select>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-row">
                                    
                                    
                                    <div class="form-group col-md-6">
                                        <label>Pembayar</label>
                                        <br />
                                        <div class="responsive">
                                            <select class="form-control ui search dropdown" name="ddlPenghutang" id="ddlPenghutang">
                                            </select>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-6">
                                        <label>Tujuan</label>
                                        <textarea class="form-control" rows="1" name="txtTujuan" id="txtTujuan"></textarea>
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Tarikh Mula</label>
                                        <input type="date" class="form-control" placeholder="No.Invois" name="tkhMula" id="tkhMula">
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label>Tarikh Tamat</label>
                                        <input type="date" class="form-control" placeholder="date" id="tkhTamat" name="tkhTamat">
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <div class="row">
                            <div class="col-md">
                                <div class="form-col">
                                    <div class="form-group d-flex align-items-center">
                                        <div class="col-2">
                                            <label for="ddlUrusniaga" class="form-label">Kategori Penghutang</label>
                                        </div>
                                        <div class="col-3">
                                            <select id="ddlUrusniaga" class="ui search dropdown" name="ddlUrusniaga"></select>
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
                    <%--<div>
                        <h6>Transaksi</h6>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">--%>
                                    <%--<table class="table table-bordered" id="tblData" style="width: 100%;">--%>
                                        <%--<thead>
                                            <tr style="width: 100%; text-align: center">
                                                <th scope="col" style="width: 20%">Vot</th>
                                                <th scope="col" style="width: 10%">Kod PTJ</th>
                                                <th scope="col" style="width: 5%">Kumpulan Wang</th>
                                                <th scope="col" style="width: 5%">Kod Operasi</th>
                                                <th scope="col" style="width: 5%">Kod Projek</th>
                                                <th scope="col" style="width: 10%">Perkara</th>
                                                <th scope="col" style="width: 5%">Kuantiti</th>
                                                <th scope="col" style="width: 7%">Harga Seunit (RM)</th>
                                                <th scope="col" style="width: 5%">Cukai (%)</th>
                                                <th scope="col" style="width: 5%">Diskaun (%)</th>
                                                <th scope="col" style="width: 10%">Debit (RM)</th>
                                                <th scope="col" style="width: 10%">Kredit (RM)</th>
                                                <th scope="col" style="width: 3%">Tindakan</th>
                                            </tr>
                                        </thead>--%>
                                        <%--<tbody id="tableID">
                                            <tr style="display: none; width: 100%">
                                                <td>
                                                    <select class="ui search dropdown vot-carian-list" name="ddlVotCarian" id="ddlVotCarian">
                                                    </select>

                                                    <input type="hidden" class="data-id" name="hdid" id="hdid" value="" />
                                                </td>
                                                <td>
                                                    <label id="lblPTj" name="lblPTj" class="label-ptj-list"></label>
                                                    <label id="HidlblPTj" name="HidlblPTj" class="Hid-ptj-list" style="visibility:hidden"></label>
                                                </td>
                                                <td>
                                                    <label id="lblKw" name="lblKw" class="label-kw-list"></label>
                                                    <label id="HidlblKw" name="HidlblKw" class="Hid-kw-list" style="visibility:hidden"></label>
                                                </td>
                                                <td>
                                                    <label id="lblKo" name="lblKo" class="label-ko-list"></label>
                                                    <label id="HidlblKo" name="HidlblKo" class="Hid-ko-list" style="visibility:hidden"></label>
                                                </td>
                                                <td>
                                                    <label id="lblKp" name="lblKp" class="label-kp-list"></label>
                                                    <label id="HidlblKp" name="HidlblKp" class="Hid-kp-list" style="visibility:hidden"></label>
                                                </td>
                                                <td>
                                                    <input class="form-control details" type="text" id="txtPerkara" name="txtPerkara" /></td>
                                                <td>
                                                    <input type="number" class="form-control underline-input multi quantity" placeholder="0" id="quantity" name="quantity" style="text-align: center" /></td>
                                                <td>
                                                    <input type="number" class="form-control underline-input multi price" placeholder="0.00" id="price" name="price" style="text-align: right" /></td>
                                                <td>
                                                    <input type="number" class="form-control underline-input multi cukai" placeholder="0.00" id="cukai" name="cukai" style="text-align: center" />
                                                    <input type="number" class="form-control underline-input multi JUMcukai" placeholder="0.00" id="JUMcukai" name="JUMcukai" style="text-align: center; visibility: hidden" />
                                                </td>
                                                <td>
                                                    <input type="number" class="form-control underline-input multi diskaun" placeholder="0.00" id="diskaun" name="diskaun" style="text-align: center" />
                                                    <input type="number" class="form-control underline-input multi JUMdiskaun" placeholder="0.00" id="JUMdiskaun" name="JUMdiskaun" style="text-align: center; visibility: hidden" />
                                                </td>
                                                <td>
                                                    <input class="form-control underline-input amount" id="amountDebit" name="amount" style="text-align: right" placeholder="0.00" /></td>
                                                <td>
                                                    <input class="form-control underline-input amount" id="amount" name="amount" style="text-align: right" placeholder="0.00" />
                                                    <input class="form-control underline-input amountwocukai" id="amountwocukai" name="amountwocukai" style="text-align: right; visibility: hidden" placeholder="0.00" /></td>
                                                <td class="tindakan">
                                                    <button class="btn btnDelete">
                                                        <i class="fa fa-trash" style="color: red"></i>
                                                    </button>
                                                    <button class="btn"><i class="fa fa-trash"></i> Trash</button>
                                                </td>
                                            </tr>

                                        </tbody>--%>
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
                                    <%--</table>--%>
                                    <%--<table class="table" style="width: 100%; border: none">
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
                                            <td style="border-right: hidden">
                                                <div class="btn-group">
                                                    <button type="button" class="btn btn-warning btnAddRow" data-val="1" value="1"><b>+ Tambah</b></button>
                                                    <button type="button" class="btn btn-warning dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                        <span class="sr-only">Toggle Dropdown</span>
                                                    </button>
                                                    <div class="dropdown-menu">
                                                        <a class="dropdown-item btnAddRow five" value="5" data-val="5" id="btnAdd5">Tambah 5</a>
                                                        <a class="dropdown-item btnAddRow" value="10" data-val="10">Tambah 10</a>

                                                    </div>
                                                </div>
                                            </td>
                                            <td></td>
                                            <td style="text-align: right; font-size: medium;">Jumlah<br />
                                                <i>( Tolak Diskaun RM
                                        <input class="underline-input" id="TotalDiskaun" name="TotalDiskaun" style="border: none; width: 20%; font-style: italic" placeholder="0.00" />
                                                    )</i></td>
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
                                            <td style="text-align: right; font-size: large">JUMLAH (RM)</td>
                                            <td></td>
                                            <td style="text-align: right">
                                                <input class="form-control underline-input" id="total" name="total" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly /></td>
                                            <td></td>
                                        </tr>
                                    </table>--%>
                                <%--</div>
                            </div>
                        </div>
                    </div>--%>
                    <%--<div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:LinkButton id="lbtnKembali" class="btn btn-primary" runat="server" onclick="lbtnKembali_Click"><i class="las la-angle-left"></i>Kembali</asp:LinkButton>
                        </div>
                        <div class="form-group col-md-12" align="right">
                            <button type="button" class="btn btn-danger" >Padam</button>
                            <button type="button" class="btn btn-secondary btnSimpan" >Simpan</button>
                            <input type ="text" id="orderid" value=""/> ORDER ID
                            <button type="button" class="btn btn-secondary btnLoad" >Load Order Records</button>
                            <button type="button" class="btn btn-danger btnPadam">Padam</button>
                            <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Draft">Simpan</button>
                            <button type="button" class="btn btn-success btnHantar" data-toggle="tooltip" data-placement="bottom" title="Simpan dan Hantar">Hantar</button>
                        </div>
                    </div>--%>
                </div>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Invois</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <!-- Create the dropdown filter -->
                        <div class="search-filter">
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-6">
                                    <label for="inputEmail3" class="col-sm-4 col-form-label">Jenis Urusniaga:</label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <select id="categoryFilter" class="custom-select">
                                                <option value="">SEMUA</option>
                                                <option value="PINJAMAN">PINJAMAN</option>
                                                <option value="PELBAGAI">PELBAGAI</option>
                                            </select>
                                            <!-- <div class="input-group-append">
                                            <button class="btn btn-outline" type="button"><i
                                                    class="fa fa-search"></i>Cari</button>
                                        </div> -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- <div class="form-row justify-content-center">
                        <div class="form-row justify-content-center">
                            <div class="col-sm-8">
                                <div class="category-filter">
                                    <select id="categoryFilter" class="form-control">
                                        <option value="">SEMUA</option>
                                        <option value="PINJAMAN">PINJAMAN</option>
                                        <option value="PELBAGAI">PELBAGAI</option>
                                    </select>
                                    <%--  <select class="form-control" name="categoryFilter" id="categoryFilter"></select>--%>
                                </div>
                            </div>
                        </div>
                    </div> -->

                        <div class="modal-body">

                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table id="tblDataSenarai" class="table table-striped" style="width: 99%">
                                        <thead>
                                            <tr style="width:100%">
                                                <th scope="col" style="width: 15%">No. Invois</th>
                                                <th scope="col" style="width: 15%">ID Penghutang</th>
                                                <th scope="col" style="width: 10%">Tarikh Mula</th>
                                                <th scope="col" style="width: 10%">Tarikh Tamat</th>
                                                <th scope="col" style="width: 15%">Jenis Urusniaga</th>
                                                <th scope="col" style="width: 20%">Tujuan</th>
                                                <%--<th scope="col" style="width: 10%">Jumlah (RM)</th>--%>
                                                <th scope="col" style="width: 5%">Tindakan</th>

                                            </tr>
                                        </thead>
                                        <tbody id="tableID_Senarai">
                                            <tr style="display: none" class="table-list">
                                            
                                            <td >
                                                <label id="lblNo" name="lblNo" class="lblNo"></label>
                                            </td>
                                            <td >
                                                <label id="lblPenghutang" name="lblPenghutang" class="lblPenghutang"></label>
                                            </td>
                                            <td >
                                                <label id="lblTkhMula" name="lblTkhMula" class="lblTkhMula"></label>
                                            </td>
                                            <td >
                                                <label id="lblTkhTamat" name="lblTkhTamat" class="lblTkhTamat" ></label>
                                            </td>
                                            <td >
                                                <label id="lblJnsUrus" name="lblJnsUrus" class="lblJnsUrus"></label>
                                            </td>
                                            <td >
                                                <label id="lblTujuan" name="lblTujuan" class="lblTujuan"></label>
                                            </td>
                                            <%--<td >
                                                <label id="lblJumlah" name="lblJumlah" class="lblJumlah"></label>
                                            </td>--%>
                                            <td >
                                                <button id="btnView_Terima" runat="server" class="btn btnView_Terima" type="button" style="color: blue" data-dismiss="modal">
                                                    <i class="fa fa-edit"></i>
                                                </button>
                                            </td>
                                        </tr>
                                        </tbody>

                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal -->
            <div class="modal fade" id="JenisUrusniaga" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle_"><label id="tajuk" name="tajuk"></label></h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <!-- Create the dropdown filter -->
                        <%--<div class="search-filter">
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-6">
                                    <label for="inputEmail3" class="col-sm-4 col-form-label">Jenis Urusniaga:</label>
                                    <div class="col-sm-8">
                                        <div class="input-group">
                                            <select id="categoryFilter" class="custom-select">
                                                <option value="">SEMUA</option>
                                                <option value="PINJAMAN">PINJAMAN</option>
                                                <option value="PELBAGAI">PELBAGAI</option>
                                            </select>
                                            <!-- <div class="input-group-append">
                                            <button class="btn btn-outline" type="button"><i
                                                    class="fa fa-search"></i>Cari</button>
                                        </div> -->
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                        <!-- <div class="form-row justify-content-center">
                        <div class="form-row justify-content-center">
                            <div class="col-sm-8">
                                <div class="category-filter">
                                    <select id="categoryFilter" class="form-control">
                                        <option value="">SEMUA</option>
                                        <option value="PINJAMAN">PINJAMAN</option>
                                        <option value="PELBAGAI">PELBAGAI</option>
                                    </select>
                                    <%--  <select class="form-control" name="categoryFilter" id="categoryFilter"></select>--%>
                                </div>
                            </div>
                        </div>
                    </div> -->

                        <div class="modal-body">

                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table id="tblDataSenarai_terima" class="table table-striped" style="width: 99%">
                                        <thead>
                                            <tr style="width:100%">
                                                <th scope="col" style="width: 15%"><label id="no_rujukan" name="no_rujukan"></label></th>
                                                <th scope="col" style="width: 15%"><label id="id_penghutang" name="id_penghutang"></label></th>
                                                <%--<th scope="col" style="width: 10%">Tarikh Transaksi</th>
                                                <th scope="col" style="width: 20%">Tujuan</th>
                                                <th scope="col" style="width: 10%">Jumlah (RM)</th>
                                                <th scope="col" style="width: 5%">Tindakan</th>--%>

                                            </tr>
                                        </thead>
                                        <tbody id="tableID_Senarai_terima">
                                            <tr style="display: none" class="table-list">
                                            
                                            <td >
                                                <label id="lblNo_Urusniaga" name="lblNo_Urusniaga" class="lblNo_Urusniaga"></label>
                                            </td>
                                            <td >
                                                <label id="lblPenghutang_Urusniaga" name="lblPenghutang_Urusniaga" class="lblPenghutang_Urusniaga"></label>
                                            </td>
                                            <%--<td >
                                                <label id="lblTkh_Urusniaga" name="lblTkh_Urusniaga" class="lblTkh_Urusniaga"></label>
                                            </td>
                                            <td >
                                                <label id="lblTujuan_Urusniaga" name="lblTujuan_Urusniaga" class="lblTujuan_Urusniaga"></label>
                                            </td>
                                            <td >
                                                <label id="lblJumlah_Urusniaga" name="lblJumlah" class="lblJumlah_Urusniaga"></label>
                                            </td>
                                            <td >
                                                <button id="btnView_Urusniaga" runat="server" class="btn btnView_Urusniaga" type="button" style="color: blue" data-dismiss="modal">
                                                    <i class="fa fa-edit"></i>
                                                </button>
                                            </td>--%>
                                        </tr>
                                        </tbody>

                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">

            //Take the category filter drop down and append it to the datatables_filter div. 
            //You can use this same idea to move the filter anywhere withing the datatable that you want.
            $("#tblDataSenarai_transaction_filter.dataTables_filter").append($("#categoryFilter"));

            var tbl = null
            $(document).ready(function () {
                var categoryIndex = 0;
                $("#tblDataSenarai_transaction th").each(function (i) {
                    if ($($(this)).html() == "Jenis Urusniaga") {
                        categoryIndex = i; return false;
                    }
                });

                //Use the built in datatables API to filter the existing rows by the Category column
                $.fn.dataTable.ext.search.push(
                    function (settings, data, dataIndex) {
                        var selectedItem = $('#categoryFilter').val()
                        var category = data[categoryIndex];
                        if (selectedItem === "" || category.includes(selectedItem)) {
                            return true;
                        }
                        return false;
                    }
                );

                //Set the change event for the Category Filter dropdown to redraw the datatable each time
                //a user selects a new filter.
                $("#categoryFilter").change(function (e) {
                    tbl.draw();
                });

                tbl.draw();

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
            var tableSenarai = '#tblDataSenarai_terima';

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


            $('.btnPapar').click(async function () {
                var record = await AjaxLoadOrderRecord_Senarai("");
                //$('#lblNoJurnal').val("")
                await clearAllRows_senarai();
                await paparSenarai(null, record);
            });

            async function AjaxLoadOrderRecord_Senarai(id) {

                try {
                    const response = await fetch('Penghutang_WS.asmx/LoadOrderRecord_SenaraiTransaksiInvois', {
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

            async function clearAllRows_senaraiurus() {
                $(tableSenarai + " > tbody > tr ").each(function (index, obj) {
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
            async function papar_SenaraiUrusniaga(totalClone, objOrder) {
                var counter = 1;
                var table = $('#tblDataSenarai_terima');
                if (objOrder !== null && objOrder !== undefined) {

                    totalClone = objOrder.Payload.length;

                }
                console.log(totalClone)
                //alert("HAI")
                while (counter <= totalClone) {


                    var row = $('#tblDataSenarai_terima tbody>tr:first').clone();
                    row.attr("style", "");
                    var val = "";

                    $('#tblDataSenarai_terima tbody').append(row);
                    ;
                    if (objOrder !== null && objOrder !== undefined) {

                        if (counter <= objOrder.Payload.length) {
                            await setValueToRow_URUSNIAGA(row, objOrder.Payload[counter - 1]);
                        }
                    }

                    counter += 1;
                }
            }
            
            async function setValueToRow_URUSNIAGA(row, orderDetail) {
                //console.log("3");
                var no = $(row).find("td > .lblNo_Urusniaga");
                var Penghutang = $(row).find("td > .lblPenghutang_Urusniaga");
                //var Tkh = $(row).find("td > .lblTkh_Urusniaga");
                //var Tujuan = $(row).find("td > .lblTujuan_Urusniaga");
                //var Jumlah = $(row).find("td > .lblJumlah_Urusniaga");

                no.html(orderDetail.NO_RUJUKAN);
                Penghutang.html(orderDetail.NAMA);
                //Tkh.html(orderDetail.Tkh_Lulus);
                //Tujuan.html(orderDetail.Tujuan);
                //Jumlah.html(orderDetail.Jumlah);
            }


            //$(document).ready(function () {
            //    $("#tblDataSenarai").DataTable({
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
            //            "sEmptyTable": "Tiada rekod."
            //        }
            //    });

            //});



            $(function () {
                $('.btnAddRow.five').click();
            });


            $('#ddlPenghutang').dropdown({
                selectOnKeydown: true,
                fullTextSearch: true,
                onChange: function (value, text, $selectedItem) {

                    //console.log($selectedItem);
                    //$(".modal-body div").val("");
                    //$('#permohonan').modal('toggle');

                },
                apiSettings: {
                    url: 'Penghutang_WS.asmx/GetPenghutangList?q={query}',
                    method: 'POST',
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    cache: false,
                    fields: {

                        value: "value",      // specify which column is for data
                        name: "text",      // specify which column is for text
                    },
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
                onChange: function (value, text, $selectedItem) {
                    //console.log(value,text);
                    $(".modal-body div").val("");
                    //if (value === '10') {
                    //    $('#no_rujukan').html("No.Bil");
                    //    $('#id_penghutang').html("ID Penghutang");
                    //    loadurusniaga(value);
                    //    $('#tajuk').html(text);
                    //    $('#JenisUrusniaga').modal('toggle');
                    //} else {
                        
                    //}

                    if (value === 'PL') {
                        $('#no_rujukan').html("No. Matrik");
                        $('#id_penghutang').html("Nama Pelajar");
                        loadurusniaga(value);
                        $('#tajuk').html(text);
                    } if (value === 'ST') {
                        $('#no_rujukan').html("No. Staf");
                        $('#id_penghutang').html("Nama Staf");
                        loadurusniaga(value);
                        $('#tajuk').html(text);
                    } 
                    $('#JenisUrusniaga').modal('toggle');
                },
                apiSettings: {
                    url: 'Penghutang_WS.asmx/GetUrusniagaList?q={query}',
                    method: 'POST',
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    cache: false,
                    fields: {

                        value: "value",      // specify which column is for data
                        name: "text",      // specify which column is for text
                    },
                   
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
            $('#ddlModTerima').dropdown({
                fullTextSearch: true,
                apiSettings: {
                    url: 'Penghutang_WS.asmx/GetModTerimaList?q={query}',
                    method: 'POST',
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    cache: false,
                    fields: {

                        value: "value",      // specify which column is for data
                        name: "text",      // specify which column is for text
                    },

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
            

            async function loadurusniaga(id) {
                //console.log("URUSS")
                var record = await AjaxLoad_SenaraiUrusniaga(id);
                await clearAllRows_senaraiurus();
                await papar_SenaraiUrusniaga(null, record);
            }

            $('.btnSimpan').click(async function () {
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
                a
                msg = "Anda pasti ingin menyimpan " + acceptedRecord + " rekod ini?"

                if (!confirm(msg)) {
                    return false;
                }
                //console.log(newOrder)
                var result = JSON.parse(await ajaxSaveOrder(newOrder));
                alert(result.Message);
                //$('#orderid').val(result.Payload.OrderID)
                //loadExistingRecords();
                await clearAllRows();
                //AddRow(5);

            });

            $('.btn-danger').click(async function () {
                var NoInvois = $('#txtnoinv').val();
                if (NoInvois !== "") {
                    await de
                } else {
                    $('#txtnoinv').val("")
                    await clearAllRows();
                    await clearAllRowsHdr();
                    await clearHiddenButton();
                    AddRow(5);
                }
                
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

                $('#txtnoinv').val("");
                $('#ddlPenghutang').empty();
                $('#tkhMula').val("");
                $('#tkhTamat').val("");
                $("#ddlUrusniaga").empty();
                $('#txtTujuan').val("");

            }

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

                calculateGrandTotal();
                return false;
            })

            async function ajaxSaveOrder(order) {

                return new Promise((resolve, reject) => {
                    $.ajax({

                        url: 'Penghutang_WS.asmx/SaveOrders',
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
                        url: 'Penghutang_WS.asmx/DeleteOrder',
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
                        url: 'Penghutang_WS.asmx/DeleteRecord',
                        method: 'POST',
                        data: JSON.stringify({ id:id }),
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
                    const response = await fetch('Penghutang_WS.asmx/LoadOrderRecord', {
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

            async function AjaxLoad_SenaraiUrusniaga(id) {
                try {
                    console.log("hai")
                    const response = await fetch('Penghutang_WS.asmx/LoadRecord_SenaraiUrusniaga', {
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
                grandTotal.val(curTotal.toFixed(2));
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
                        url: 'Penghutang_WS.asmx/GetCarianVotList?q={query}',
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
                        url: 'Penghutang_WS.asmx/GetPTJList?q={query}',
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
                        url: 'Penghutang_WS.asmx/GetVotList?q={query}&kodptj={kodptj}',
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
                        url: 'Penghutang_WS.asmx/GetKWList?q={query}&kodvot={kodvot}',
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
                        url: 'Penghutang_WS.asmx/GetKOList?q={query}&kodkw={kodkw}',
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
                        url: 'Penghutang_WS.asmx/GetProjekList?q={query}&kodptj={kodptj}&kodvot={kodvot}',
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

            $(tableID_Senarai).on('click', '.btnView_Terima', async function () {
                console.log("Hai")
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

            $(tableID_Senarai_terima).on('click', '.btnView_Urusniaga', async function () {
                //console.log("Hai")
                event.preventDefault();
                var curTR = $(this).closest("tr");
                var recordID = curTR.find("td > .lblNo_Urusniaga");

                //var bool = true;
                var id = recordID.html();
                //alert("hai");
                //console.log(id)
                if (id !== "") {

                    //BACA HEADER JURNAL
                    var recordHdr = await AjaxGetRecordHdrBil(id);
                    await AddRowHeader_Bil(null, recordHdr);

                    //BACA DETAIL JURNAL
                    var record = await AjaxGetRecordBil(id);
                    await clearAllRows();
                    await AddRow_Bil(null, record);
                }

                return false;
            })
            

            async function AddRowHeader_Bil(totalClone, objOrder) {
                var counter = 1;
                //var table = $('#tblDataSenarai');

                if (objOrder !== null && objOrder !== undefined) {
                    totalClone = objOrder.Payload.length;
                }


                if (counter <= objOrder.Payload.length) {
                    await setValueToRow_HdrBil(objOrder.Payload[counter - 1]);
                }
                // console.log(objOrder)
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
            

            async function setValueToRow_HdrBil(orderDetail) {

                $('#txtnoinv').val(orderDetail.No_Bil)
                //$('#txtNoRujukan').val(orderDetail.No_Rujukan)
                //$('#ddlPenghutang').val(orderDetail.Nama_Penghutang)

                //$('#tkhMula').val(orderDetail.Tkh_Mula)
                //$('#tkhTamat').val(orderDetail.Tkh_Tamat)
                //console.log($('#rdTIDAK').is(':checked'))
                //if (orderDetail.Kontrak == '0') {
                //    //$('[id=rdTIDAK]')[0].checked = true
                //    $(':radio[name=inlineRadioOptions][value="0"]').prop('checked', true);
                //    $(':radio[name=inlineRadioOptions][value="1"]').prop('checked', false);
                //    //$('#rdTIDAK').val(':checked');
                //    //$('#rdTIDAK').is('checked:checked');
                //} else {
                //    //$('#rdYA').is('checked:checked');
                //    $(':radio[name=inlineRadioOptions][value="1"]').prop('checked', true);
                //    $(':radio[name=inlineRadioOptions][value="0"]').prop('checked', false);
                //}
                //$("input[name='inlineRadioOptions']:checked").val(orderDetail.Kontrak)
                //$('#ddlUrusniaga').val(orderDetail.JenisUrusniaga)
                $('#txtTujuan').val(orderDetail.Tujuan)

                var newId = $('#ddlJenTransaksi')

                //await initDropdownPtj(newId)
                //$(newId).api("query");

                var ddlPenghutang = $('#ddlPenghutang')
                var ddlSearchP = $('#ddlPenghutang')
                var ddlTextP = $('#ddlPenghutang')
                var selectObj_JenisTransaksiP = $('#ddlPenghutang')
                $(ddlPenghutang).dropdown('set selected', orderDetail.Kod_Pelanggan);
                selectObj_JenisTransaksiP.append("<option value = '" + orderDetail.Kod_Pelanggan + "'>" + orderDetail.Nama_Penghutang + "</option>")

                //var ddlUrusniaga = $('#ddlUrusniaga')
                //var ddlSearch = $('#ddlUrusniaga')
                //var ddlText = $('#ddlUrusniaga')
                //var selectObj_JenisTransaksi = $('#ddlUrusniaga')
                //$(ddlUrusniaga).dropdown('set selected', orderDetail.Jenis_Urusniaga);
                //selectObj_JenisTransaksi.append("<option value = '" + orderDetail.Jenis_Urusniaga + "'>" + orderDetail.Butiran + "</option>")
            }

            async function setValueToRow_HdrJurnal(orderDetail) {

                $('#txtnoinv').val(orderDetail.No_Bil)
                //$('#txtNoRujukan').val(orderDetail.No_Rujukan)
                //$('#ddlPenghutang').val(orderDetail.Nama_Penghutang)
                
                $('#tkhMula').val(orderDetail.Tkh_Mula)
                $('#tkhTamat').val(orderDetail.Tkh_Tamat)
                //console.log($('#rdTIDAK').is(':checked'))
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
                //$("input[name='inlineRadioOptions']:checked").val(orderDetail.Kontrak)
                //$('#ddlUrusniaga').val(orderDetail.JenisUrusniaga)
                $('#txtTujuan').val(orderDetail.Tujuan)

                var newId = $('#ddlJenTransaksi')

                //await initDropdownPtj(newId)
                //$(newId).api("query");

                var ddlPenghutang = $('#ddlPenghutang')
                var ddlSearchP = $('#ddlPenghutang')
                var ddlTextP = $('#ddlPenghutang')
                var selectObj_JenisTransaksiP = $('#ddlPenghutang')
                $(ddlPenghutang).dropdown('set selected', orderDetail.Kod_Pelanggan);
                selectObj_JenisTransaksiP.append("<option value = '" + orderDetail.Kod_Pelanggan + "'>" + orderDetail.Nama_Penghutang + "</option>")

                var ddlUrusniaga = $('#ddlUrusniaga')
                var ddlSearch = $('#ddlUrusniaga')
                var ddlText = $('#ddlUrusniaga')
                var selectObj_JenisTransaksi = $('#ddlUrusniaga')
                $(ddlUrusniaga).dropdown('set selected', orderDetail.Jenis_Urusniaga);
                selectObj_JenisTransaksi.append("<option value = '" + orderDetail.Jenis_Urusniaga + "'>" + orderDetail.Butiran + "</option>")
            }


            async function AjaxGetRecordHdrJurnal(id) {

                try {

                    const response = await fetch('Penghutang_WS.asmx/LoadHdrInvois', {
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

            async function AjaxGetRecordHdrBil(id) {
                console.log("Hai")
                try {

                    const response = await fetch('Penghutang_WS.asmx/LoadHdrBil', {
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

                    const response = await fetch('Penghutang_WS.asmx/LoadRecordInvois', {
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
            async function AjaxGetRecordBil(id) {

                try {

                    const response = await fetch('Penghutang_WS.asmx/LoadRecordBil', {
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

                        }
                    }
                    counter += 1;
                }
            }

            async function AddRow_Bil(totalClone, objOrder) {
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
                            await setValueToRow_Bil(row, objOrder.Payload[counter - 1]);

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
            async function setValueToRow_Bil(row, orderDetail) {

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
                        url: 'Penghutang_WS.asmx/GetVotCOA?q={query}',
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
        </script>
    </contenttemplate>
</asp:Content>
