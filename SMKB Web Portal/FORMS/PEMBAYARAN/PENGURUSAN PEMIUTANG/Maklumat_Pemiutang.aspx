<%@ Page Title="Pengurusan Pemiutang" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master"
    CodeBehind="Maklumat_Pemiutang.aspx.vb" Inherits="SMKB_Web_Portal.DaftarMaklumatPemiutang" %>


    <asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
        <script type="text/javascript">
        </script>
        <contenttemplate>
            <style>
                .default-primary {
                    background-color: #007bff !important;
                    color: white;
                }
            </style>

            <%--<form id="form1" runat="server">--%>
                <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
                    <div id="PermohonanTab" class="tabcontent" style="display: block">
                        <%-- DIV DAFTAR pemiutang --%>
                            <div id="divMaklumatPemiutang" runat="server" visible="true">
                                <div class="modal-body">
                                    <div class="table-title">
                                        <h4 class="font-weight-bold">Senarai Pemiutang</h4>
                                        <div class="form-group row col-md-5" id="kategori">     
                                            <label for="" class="col-sm-2 col-form-label">Kategori:</label>
                                            <div class="col-sm-8">
                                                <div class="input-group">
                                                    <select id="categoryFilter" class="custom-select">
                                                        <option value="" selected>Semua</option>
                                                        <option value="OA">Orang Awam</option>
                                                        <option value="PG">Pelajar Pasca Siswazah</option>
                                                        <option value="PH">Pelajar Sepanjang Hayat</option>
                                                        <option value="PL">Pelajar Sarjana Muda</option>
                                                        <option value="ST">Staf</option>
                                                        <option value="SY">Syarikat</option>
                                                    </select>
                                                    <div class="input-group-append">
                                                        <button id="btnSearch" runat="server" class="btn btn-outline btnSearch" type="button">
                                                            <i class="fa fa-search"></i>
                                                            Cari
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="btn btn-primary btnPapar">
                                            + Pemiutang
                                        </div>
                                    </div>
                                    <hr>
                                    <div class="transaction-table table-responsive">
                                        <table id="tblDataSenarai" class="table table-striped" style="width: 99%">
                                            <thead>
                                                <tr style="width:100%">
                                                    <th scope="col" style="width: 10%">No. Akaun</th>
                                                    <th scope="col" style="width: 15%">Nama</th>
                                                    <th scope="col" style="width: 5%">Kategori</th>
                                                    <th scope="col" style="width: 10%">No. Rujukan</th>
                                                    <th scope="col" style="width: 10%">No. Telefon</th>
                                                    <th scope="col" style="width: 10%">Emel</th>
                                                    <th scope="col" style="width: 10%">Kod Bank</th>
                                                    <th scope="col" style="width: 10%">Nama Bank</th>
                                                    <th scope="col" style="width: 10%">No. Akaun Bank</th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <!-- Modal - Add New Pemiutang -->
                            <div class="modal fade" id="modalNewPemiutang" tabindex="-1" role="dialog"
                                aria-labelledby="exampleModalCenterTitle" aria-hidden="true" data-backdrop="static">
                                <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-lg"
                                    role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="addNewPemiutangModal">Tambah Pemiutang</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"
                                                id="btnCloseModal">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>

                                        <div class="modal-body">
                                            <div class="col-md-12">
                                                <div class="row w-full">
                                                    <div class="container w-full">
                                                        <h6 class="font-weight-bold mb-2">Maklumat Pemiutang</h6>
                                                        <div class="form-group d-flex align-items-center w-full">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="txtNoPemiutang" class="form-label">No.
                                                                    Akaun: </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <input type="text" class="form-control" placeholder=""
                                                                    id="txtNoPemiutang" name="txtNoPemiutang"
                                                                    readonly>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlKategoriPemiutang"
                                                                    class="form-label text-right">Kategori Pemiutang: <span class="text-danger">*</span>
                                                                </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlKategoriPemiutang"
                                                                    class="ui search dropdown"
                                                                    name="ddlKategoriPemiutang"></select>
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
                                                        <button type="button" class="btn btn-success btnHantar">
                                                            Hantar
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Modal - Update Pemiutang -->
                            <div class="modal fade" id="modalUpdatePemiutang" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle"
                                aria-hidden="true" data-backdrop="static">
                                <div class="modal-dialog modal-dialog-scrollable modal-dialog-centered modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title">Kemaskini Pemiutang</h5>
                                            <button type="button" class="close btnCloseModal" data-dismiss="modal" aria-label="Close" id="">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                            
                                        <div class="modal-body">
                                            <div class="col-md-12">
                                                <div class="row w-full">
                                                    <div class="container w-full">
                                                        <h6 class="font-weight-bold mb-2">Maklumat Pemiutang</h6>
                                                        <div class="form-group d-flex align-items-center w-full">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="txtNoPemiutangUpdate" class="form-label">No.
                                                                    Akaun: </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <input type="text" class="form-control" placeholder="" id="txtNoPemiutangUpdate"
                                                                    name="txtNoPemiutangUpdate" readonly>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlKategoriPemiutangUpdate" class="form-label text-right">Kategori
                                                                    Pemiutang: <span class="text-danger">*</span>
                                                                </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlKategoriPemiutangUpdate" class="ui search dropdown"
                                                                    name="ddlKategoriPemiutangUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center" id="sectionIdUpdate">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="txtId" class="form-label text-right" id="lblId">No. Kad Pengenalan / No.
                                                                    Syarikat:
                                                                </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <input type="text" class="form-control" placeholder="" id="txtIdUpdate" name="txtIdUpdate"
                                                                    autocomplete="off">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-none align-items-center" id="sectionStafUpdate">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlNoStafUpdate" class="form-label">No. Staf: <span
                                                                        class="text-danger">*</span>
                                                                </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlNoStafUpdate" class="ui search dropdown" name="ddlNoStafUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-none align-items-center" id="sectionPelajarUGUpdate">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlNoMatrikUGUpdate" class="form-label">No. Matrik: <span
                                                                        class="text-danger">*</span>
                                                                </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlNoMatrikUGUpdate" class="ui search dropdown" name="ddlNoMatrikUGUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-none align-items-center" id="sectionPelajarPGUpdate">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlNoMatrikPGUpdate" class="form-label">No. Matrik: <span
                                                                        class="text-danger">*</span>
                                                                </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlNoMatrikPGUpdate" class="ui search dropdown" name="ddlNoMatrikPGUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-none align-items-center" id="sectionPelajarPHUpdate">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlNoMatrikPGUpdate" class="form-label">No.
                                                                    Matrik:</label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlNoMatrikPHUpdate" class="ui search dropdown" name="ddlNoMatrikPHUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="txtNamaUpdate" class="form-label">Nama: <span
                                                                        class="text-danger">*</span></label>
                                                            </div>
                                                            <div class="col-8">
                                                                <input type="text" class="form-control" placeholder="" id="txtNamaUpdate" name="txtNamaUpdate"
                                                                    autocomplete="off">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="txtEmelUpdate" class="form-label">Emel: <span
                                                                        class="text-danger">*</span></label>
                                                            </div>
                                                            <div class="col-7">
                                                                <input type="email" class="form-control" placeholder="" id="txtEmelUpdate" name="txtEmelUpdate"
                                                                    autocomplete="off">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="txtNoTelefonUpdate" class="form-label">No. Telefon: <span
                                                                        class="text-danger">*</span></label>
                                                            </div>
                                                            <div class="col-6">
                                                                <input type="text" class="form-control" placeholder="" id="txtNoTelefonUpdate"
                                                                    name="txtNoTelefonUpdate" autocomplete="off">
                                                            </div>
                                                        </div>
                                                        <div class="dropdown-divider"></div>
                                                        <h6 class="font-weight-bold mb-2">Maklumat Alamat</h6>
                                                        <div class="form-group mb-2 d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="" class="form-label">Alamat:</label>
                                                            </div>
                                                            <div class="col-6">
                                                                <input type="text" class="form-control" placeholder="" id="txtAlamat1Update"
                                                                    name="txtAlamat1Update" autocomplete="off">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                            </div>
                                                            <div class="col-6">
                                                                <input type="text" class="form-control" placeholder="" id="txtAlamat2Update"
                                                                    name="txtAlamat2Update" autocomplete="off">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlBandarUpdate" class="form-label">Bandar:</label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlBandarUpdate" class="ui search dropdown" name="ddlBandarUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlPoskodUpdate" class="form-label">Poskod:</label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlPoskodUpdate" class="ui search dropdown" name="ddlPoskodUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlNegeriUpdate" class="form-label">Negeri:</label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlNegeriUpdate" class="ui search dropdown" name="ddlNegeriUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlNegaraUpdate" class="form-label">Negara:</label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlNegaraUpdate" class="ui search dropdown" name="ddlNegaraUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="dropdown-divider"></div>
                                                        <h6 class="font-weight-bold mb-2">Maklumat Bank</h6>
                                                        <div class="form-group d-flex align-items-center" id="sectionDefaultBankUpdate">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="ddlBankUpdate" class="form-label">Nama Bank:
                                                                </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <select id="ddlBankUpdate" class="ui search dropdown" name="ddlBankUpdate"></select>
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-none align-items-center" id="sectionStafBankUpdate">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="txtStafBankNameUpdate" class="form-label">Nama
                                                                    Bank: </label>
                                                            </div>
                                                            <div class="col-6">
                                                                <input type="text" class="form-control" placeholder="" id="txtStafBankNameUpdate"
                                                                    name="txtStafBankNameUpdate" autocomplete="off">
                                                            </div>
                                                        </div>
                                                        <div class="form-group d-flex align-items-center">
                                                            <div class="col-4 d-flex justify-content-end">
                                                                <label for="txtNoAkaunBankUpdate" class="form-label">No. Akaun
                                                                    Bank:</label>
                                                            </div>
                                                            <div class="col-6">
                                                                <input type="text" class="form-control" placeholder="" id="txtNoAkaunBankUpdate"
                                                                    name="txtNoAkaunBankUpdate" autocomplete="off">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-row">
                                                    <div class="form-group col-md-12" align="right">
                                                        <button type="button" class="btn btn-danger btnBatal">Padam</button>
                                                        <button type="button" class="btn default-primary btnKemaskini">
                                                            Simpan
                                                        </button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- Confirmation Modal -->
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
                            <!-- Makluman Modal -->
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
                    </div>

                    <script type="text/javascript">
                        var shouldPop = true;

                        var fetchKodKategoriPemiutang;
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

                        var senaraiPemiutangData = null;

                        var selectedCategory = null;

                        var tbl = null
                        var isClicked = false;
                        var isReset = false;

                        $(document).ready(function () {

                            // auto convert text to uppercase
                            $('#txtNama').on('input', function () {
                                var inputValue = $(this).val();
                                $(this).val(inputValue.toUpperCase());

                                // make REGEX to allow only alphabets and space
                                var regex = /[^a-z ]/gi;
                                this.value = this.value.replace(regex, "");
                            });

                            // set categoryFilter to Semua as default state
                            $('#categoryFilter').val("");

                            // get all kategori pemiutang and store in array
                             fetchKodKategoriPemiutang = GetKategoriValue('');
                             fetchKodKategoriPemiutang.then(function (result) {
                                 if (result && result.length > 0) {
                                     kodKategoriArray = result;
                                 }
                             }).catch(function (error) {
                                 console.error('Error:', error);
                             });

                            // // get all kod negara and store in array
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


                             // get all no staff and store in array
                            fetchNoStaff = GetNoStaffValue('');
                            fetchNoStaff.then(function (result) {
                                if (result && result.length > 0) {
                                    noStaffArray = result;
                                }
                            }).catch(function (error) {
                                console.error('Error:', error);
                            });

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
                                    "sSearch": "Carian: "
                                },
                                "ajax": {
                                    "url": "PemiutangWS.asmx/LoadList_SenaraiPemiutang",
                                    "method": 'POST',
                                    "contentType": "application/json; charset=utf-8",
                                    "dataType": "json",
                                    "dataSrc": function (json) {
                                        return JSON.parse(json.d);
                                    },
                                    "data": function () {
                                        //Filter category bermula dari sini - 20 julai 2023
                                        return JSON.stringify({
                                            category_filter: $('#categoryFilter').val(),
                                            isClicked: isClicked,
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
                                    { "data": "id" },
                                    { "data": "nama" },
                                    {
                                        "data": "kategoriPemiutang",
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
                                    { "data": "noRujukan" },
                                    {
                                        "data": "telBimbit",
                                        "render": function (data, type, row) {
                                            if (data == null) {
                                                return "-";
                                            } else {
                                                return data;
                                            }
                                        }
                                    },
                                    { "data": "emel" },
                                    {
                                        "data": "kodBank",
                                        "render": function (data, type, row) {
                                            if (data == null) {
                                                return "-";
                                            } else {
                                                return data;
                                            }
                                        }
                                    },
                                    {
                                        "data": "namaBank",
                                        "render": function (data, type, row) {
                                            if (data == null) {
                                                return "-";
                                            } else {
                                                return data;
                                            }
                                        }
                                    },
                                    {
                                        "data": "noAkaun",
                                        "render": function (data, type, row) {
                                            if (data == null) {
                                                return "-";
                                            } else {
                                                return data;
                                            }
                                        }
                                    },
                                ]
                            });

                            Promise.all([fetchKodKategoriPemiutang]).then(function (values) {
                            });
                        });

                        $('#ddlKategoriPemiutang').dropdown({
                            selectOnKeydown: true,
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'PemiutangWS.asmx/GetKodPemiutangList?q={query}',
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

                        $('#ddlKategoriPemiutangUpdate').dropdown({
                            selectOnKeydown: true,
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'PemiutangWS.asmx/GetKodPemiutangList?q={query}',
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
                                url: 'PemiutangWS.asmx/GetNegaraList?q={query}',
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
                                url: 'PemiutangWS.asmx/GetNegeriList?q={query}',
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
                                url: 'PemiutangWS.asmx/GetPoskodList?q={query}',
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

                        $('#ddlPoskodUpdate').dropdown({
                            selectOnKeydown: true,
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'PemiutangWS.asmx/GetPoskodList?q={query}',
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

                        $('#ddlBandar').dropdown({
                            selectOnKeydown: true,
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'PemiutangWS.asmx/GetBandarList?q={query}',
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
                                url: 'PemiutangWS.asmx/GetBankList?q={query}',
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
                                url: 'PemiutangWS.asmx/GetKodStaffList?q={query}',
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

                        $('#ddlNoStafUpdate').dropdown({
                            selectOnKeydown: true,
                            fullTextSearch: true,
                            apiSettings: {
                                url: 'PemiutangWS.asmx/GetKodStaffList?q={query}',
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
                                url: 'PemiutangWS.asmx/GetKodPelajarUGList?q={query}',
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
                                url: 'PemiutangWS.asmx/GetKodPelajarUGList?q={query}',
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
                                url: 'PemiutangWS.asmx/GetKodPelajarPGList?q={query}',
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

                        $('.btnSearch').click(async function () {
                            show_loader();
                            isClicked = true;
                            tbl.ajax.reload();
                        });

                        $('.btnHantar').click(async function () {
                            // check every required field
                            if ($('#ddlKategoriPemiutang').val() == "" || $('#txtNama').val() == "" || $('#txtNoTelefon').val() == "" || $('#txtEmel').val() == "") {
                                // open modal makluman and show message
                                $('#maklumanModal').modal('toggle');
                                $('#detailMakluman').html('<span>Sila isi semua ruangan yang bertanda <span class="text-danger">*</span></span>');
                            } else {
                                // open modal confirmation
                                $('#confirmationModal').modal('toggle');
                            }
                        })

                        $('.btnKemaskini').click(async function () {
                            // check every required field
                            if ($('#ddlKategoriPemiutangUpdate').val() == "" || $('#txtNamaUpdate').val() == "" || $('#txtNoTelefonUpdate').val() == "" || $('#txtEmelUpdate').val() == "") {
                                // open modal makluman and show message
                                $('#maklumanModal').modal('toggle');
                                $('#detailMakluman').html('<span>Sila isi semua ruangan yang bertanda <span class="text-danger">*</span></span>');
                            } else {
                                // open modal confirmation
                                $('#confirmationModal').modal('toggle');
                            }
                        })

                        async function ajaxSavePemiutang(category) {
                            return new Promise((resolve, reject) => {
                                $.ajax({
                                    url: 'PemiutangWS.asmx/SavePemiutang',
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

                        // confirmation button in confirmation modal
                        $('.btnYa').click(async function () {
                            var pemiutang = null;

                            //close modal confirmation
                            $('#confirmationModal').modal('toggle');

                            var id;
                            var bank;
                            var noAkaunBank;
                            var alamat1;
                            var alamat2;
                            var poskod;
                            var bandar;
                            var negeri;
                            var negara;


                            if ($('#txtNoPemiutangUpdate').val() == "") {

                                selectedCategory = $('#ddlKategoriPemiutang').val();
                                
                                if (selectedCategory == "ST") {
                                    id = $('#ddlNoStaf').val();
                                    bank = $('#txtStafBankName').val();
                                } else if (selectedCategory == "UG") {
                                    id = $('#ddlNoMatrikUG').val();
                                    bank = $('#ddlBank').val();
                                } else if (selectedCategory == "PG") {
                                    id = $('#ddlNoMatrikPG').val();
                                    bank = $('#ddlBank').val();
                                } else if (selectedCategory == "PH") {
                                    id = $('#ddlNoMatrikPH').val();
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

                                // new pemiutang
                                pemiutang = {
                                    pemiutang: {
                                        Nama: $('#txtNama').val(),
                                        Id: id,
                                        IdPemiutang: '',
                                        NoTelefon: $('#txtNoTelefon').val(),
                                        Email: $('#txtEmel').val(),
                                        KategoriPemiutang: $('#ddlKategoriPemiutang').val(),
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

                                var result = JSON.parse(await ajaxSavePemiutang(pemiutang));

                                if (result.Status !== "Failed") {
                                    $('#modalNewPemiutang').modal('toggle');
                                    // open modal makluman and show message
                                    $('#maklumanModal').modal('toggle');
                                    $('#detailMakluman').html(result.Message);
                                    clearAllFields();
                                    tbl.ajax.reload();
                                } else {
                                    // open modal makluman and show message
                                    $('#maklumanModal').modal('toggle');
                                    $('#detailMakluman').html(result.Message);
                                }

                            } else {

                                selectedCategory = $('#ddlKategoriPemiutangUpdate').val();

                                if (selectedCategory == "ST") {
                                    id = $('#ddlNoStafUpdate').val();
                                    bank = $('#txtStafBankNameUpdate').val();
                                } else if (selectedCategory == "UG") {
                                    id = $('#ddlNoMatrikUGUpdate').val();
                                    bank = $('#ddlBankUpdate').val();
                                } else if (selectedCategory == "PG") {
                                    id = $('#ddlNoMatrikPGUpdate').val();
                                    bank = $('#ddlBankUpdate').val();
                                } else if (selectedCategory == "PH") {
                                    id = $('#ddlNoMatrikPHUpdate').val();
                                    bank = $('#ddlBankUpdate').val();
                                } else if (selectedCategory == "OA" || selectedCategory == "SY") {
                                    id = $('#txtIdUpdate').val();
                                    bank = $('#ddlBankUpdate').val();
                                }

                                if (bank == '' || bank == null) {
                                    bank = '-';
                                }

                                if ($('#txtAlamat1Update').val() == '' || $('#txtAlamat1Update').val() == null) {
                                    alamat1 = '-';
                                } else {
                                    alamat1 = $('#txtAlamat1Update').val();
                                }

                                if ($('#txtAlamat2Update').val() == '' || $('#txtAlamat2Update').val() == null) {
                                    alamat2 = '-';
                                } else {
                                    alamat2 = $('#txtAlamat2Update').val();
                                }

                                if ($('#ddlPoskodUpdate').val() == '' || $('#ddlPoskodUpdate').val() == null) {
                                    poskod = '-';
                                } else {
                                    poskod = $('#ddlPoskodUpdate').val();
                                }

                                if ($('#ddlBandarUpdate').val() == '' || $('#ddlBandarUpdate').val() == null) {
                                    bandar = '-';
                                } else {
                                    bandar = $('#ddlBandarUpdate').val();
                                }

                                if ($('#ddlNegeriUpdate').val() == '' || $('#ddlNegeriUpdate').val() == null) {
                                    negeri = '-';
                                } else {
                                    negeri = $('#ddlNegeriUpdate').val();
                                }

                                if ($('#ddlNegaraUpdate').val() == '' || $('#ddlNegaraUpdate').val() == null) {
                                    negara = '-';
                                } else {
                                    negara = $('#ddlNegaraUpdate').val();
                                }

                                if ($('#txtNoAkaunBankUpdate').val() == '' || $('#txtNoAkaunBankUpdate').val() == null) {
                                    noAkaunBank = '-';
                                } else {
                                    noAkaunBank = $('#txtNoAkaunBankUpdate').val();
                                }

                                // update pemiutang
                                pemiutang = {
                                    pemiutang: {
                                        Nama: $('#txtNamaUpdate').val(),
                                        Id: id,
                                        IdPemiutang: $('#txtNoPemiutangUpdate').val(),
                                        NoTelefon: $('#txtNoTelefonUpdate').val(),
                                        Email: $('#txtEmelUpdate').val(),
                                        KategoriPemiutang: $('#ddlKategoriPemiutangUpdate').val(),
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
    
                                var result = JSON.parse(await ajaxSavePemiutang(pemiutang));
    
                                if (result.Status !== "Failed") {
                                    $('#modalUpdatePemiutang').modal('toggle');
                                    // open modal makluman and show message
                                    $('#maklumanModal').modal('toggle');
                                    $('#detailMakluman').html(result.Message);
                                    clearAllFields();
                                    tbl.ajax.reload();
                                } else {
                                    // open modal makluman and show message
                                    $('#maklumanModal').modal('toggle');
                                    $('#detailMakluman').html(result.Message);
                                }
                            }
                        });

                        // button Batal / Modal closed / default state
                        function clearAllFields() {
                            $('#txtNoPemiutang').val('');
                            $('#ddlKategoriPemiutang').dropdown('clear');
                            $('#ddlKategoriPemiutang').empty();
                            $('#ddlKategoriPemiutang').dropdown('refresh');
                            isReset = false
                            $('#ddlKategoriPemiutang').trigger('change');
                            isReset = true;
                            $('#txtNama').val('');
                            $('#txtId').val('');
                            $('#txtNoTelefon').val('');
                            $('#txtEmel').val('');

                            $('#ddlNoStaf').dropdown('clear');
                            $('#ddlNoStaf').empty();

                            $('#ddlNoMatrikUG').dropdown('clear');
                            $('#ddlNoMatrikUG').empty();

                            $('#ddlNoMatrikPG').dropdown('clear');
                            $('#ddlNoMatrikPG').empty();

                            $('#ddlNoMatrikPH').dropdown('clear');
                            $('#ddlNoMatrikPH').empty();

                            $('#txtAlamat1').val('');
                            $('#txtAlamat2').val('');

                            $('#ddlBandar').dropdown('clear');
                            $('#ddlBandar').empty();
                            $('#ddlPoskod').dropdown('clear');
                            $('#ddlPoskod').empty();
                            $('#ddlNegeri').dropdown('clear');
                            $('#ddlNegeri').empty();
                            $('#ddlNegara').dropdown('clear');
                            $('#ddlNegara').empty();

                            $('#ddlBank').dropdown('clear');
                            $('#ddlBank').empty();
                            $('#txtStafBankName').val('');
                            $('#txtNoAkaunBank').val('');
                            
                            // clear in update modal
                            $('#txtNoPemiutangUpdate').val('');
                            $('#txtNamaUpdate').val('');
                            $('#txtNoTelefonUpdate').val('');
                            $('#txtEmelUpdate').val('');

                            $('#ddlKategoriPemiutangUpdate').dropdown('clear');
                            $('#ddlKategoriPemiutangUpdate').dropdown('refresh');
                            $('#ddlKategoriPemiutangUpdate').trigger('change');

                            $('#ddlNoMatrikUGUpdate').dropdown('clear');
                            $('#ddlNoMatrikUGUpdate').dropdown('refresh');
                            $('#ddlNoMatrikPGUpdate').dropdown('clear');
                            $('#ddlNoMatrikPGUpdate').dropdown('refresh');
                            $('#ddlNoMatrikPHUpdate').dropdown('clear');
                            $('#ddlNoMatrikPHUpdate').dropdown('refresh');
                            $('#ddlNoStafUpdate').dropdown('clear');
                            $('#ddlNoStafUpdate').dropdown('refresh');

                            $('#txtAlamat1Update').val('');
                            $('#txtAlamat2Update').val('');

                            $('#ddlBandarUpdate').dropdown('clear');
                            $('#ddlBandarUpdate').dropdown('refresh');
                            $('#ddlPoskodUpdate').dropdown('clear');
                            $('#ddlPoskodUpdate').dropdown('refresh');
                            $('#ddlNegeriUpdate').dropdown('clear');
                            $('#ddlNegeriUpdate').dropdown('refresh');
                            $('#ddlNegaraUpdate').dropdown('clear');
                            $('#ddlNegaraUpdate').dropdown('refresh');

                            $('#ddlBankUpdate').dropdown('clear');
                            $('#ddlBankUpdate').dropdown('refresh');
                            $('#txtNoAkaunBankUpdate').val('');

                        }

                        function clearFilter() {
                            $('.data-row').show();
                        }

                        // handling the row click event
                        function rowClickHandler(item) {

                            // setTimeout(function () {
                            // }, 500);
                            
                            isReset = true;
                            clearAllFields();
                            isReset = false;

                            $('#modalUpdatePemiutang').modal('toggle');

                            // append $('#ddlKategoriPemiutang') with data from server
                            $('#ddlKategoriPemiutangUpdate').dropdown('clear');
                            $('#ddlKategoriPemiutangUpdate').empty();
                            $('#ddlKategoriPemiutangUpdate').dropdown('refresh');

                            $('#txtNoPemiutangUpdate').val(item.id);

                            var kategoriPromise = GetKategoriValue(item.kategoriPemiutang);
                            kategoriPromise.then(function (result) {
                                if (result && result.length > 0) {

                                    var text = result[0].text;
                                    var option = $('<option>').attr('value', item.kategoriPemiutang).text(text);
                                    $('#ddlKategoriPemiutangUpdate').append(option);

                                    // click event for ddlKategoriPemiutang
                                    isReset = false;
                                    $('#ddlKategoriPemiutangUpdate').trigger('change');
                                    isReset = true;

                                    $('#txtNamaUpdate').val(item.nama);
                                    $('#txtNoTelefonUpdate').val(item.telBimbit);
                                    $('#txtEmelUpdate').val(item.emel);

                                    // if kategori == UG, PG, PH
                                    if (item.kategoriPemiutang == 'PL' || item.kategoriPemiutang == 'PG' || item.kategoriPemiutang == 'PH') {

                                        if (item.kategoriPemiutang == 'PG') {
                                            $('#ddlNoMatrikPGUpdate').dropdown('clear');
                                            $('#ddlNoMatrikPGUpdate').empty();
                                            var option = $('<option>').attr('value', item.noRujukan).text(item.noRujukan);
                                            $('#ddlNoMatrikPGUpdate').append(option);      
                                        } else if (item.kategoriPemiutang == 'PH') {
                                            $('#ddlNoMatrikPHUpdate').dropdown('clear');
                                            $('#ddlNoMatrikPHUpdate').empty();
                                            var option = $('<option>').attr('value', item.noRujukan).text(item.noRujukan);
                                            $('#ddlNoMatrikPHUpdate').append(option);
                                        } else {
                                            $('#ddlNoMatrikUGUpdate').dropdown('clear');
                                            $('#ddlNoMatrikUGUpdate').empty();
                                            var option = $('<option>').attr('value', item.noRujukan).text(item.noRujukan);
                                            $('#ddlNoMatrikUGUpdate').append(option);
                                        }

                                        GetPemiutangAllDetailValue(item.id).then(function (result) {

                                            setTimeout(function () {
                                                // alamat
                                                $('#txtAlamat1Update').val(result[0].alamat1)
                                                $('#txtAlamat1Update').val(result[0].alamat2)

                                                $('#ddlBandarUpdate').dropdown('clear');
                                                if (result[0].bandar !== null) {
                                                    $('#ddlBandarUpdate').empty();
                                                    var bandarPromise = GetBandarValue(result[0].bandar);
                                                    bandarPromise.then(function (result) {
                                                        if (result && result.length > 0) {
                                                            var text = result[0].text;
                                                            var value = result[0].value;
                                                            var option = $('<option>').attr('value', value).text(text);
                                                            $('#ddlBandarUpdate').append(option);

                                                            updateNegeriNegaraByBandar(value);
                                                        }
                                                    }).catch(function (error) {
                                                        console.error('Error:', error);
                                                    });
                                                } else {
                                                    $('#ddlBandar').dropdown('clear');
                                                }

                                                $('#ddlPoskodUpdate').dropdown('clear');
                                                if (result[0].poskod !== null) {
                                                    $('#ddlPoskodUpdate').empty();
                                                    var poskodPromise = GetPoskodValue(result[0].poskod);
                                                    poskodPromise.then(function (result) {
                                                        if (result && result.length > 0) {
                                                            var text = result[0].text;
                                                            var option = $('<option>').attr('value', result[0].poskod).text(text);
                                                            $('#ddlPoskodUpdate').append(option);
                                                        }
                                                    }).catch(function (error) {
                                                        console.error('Error:', error);
                                                    });
                                                } else {
                                                    $('#ddlPoskod').dropdown('clear');
                                                }

                                                $('#ddlBankUpdate').dropdown('clear');
                                                if (result[0].kodBank !== null) {
                                                    $('#ddlBank').empty();
                                                    var bankPromise = GetBankValue(result[0].kodBank);
                                                    bankPromise.then(function (result) {
                                                        if (result && result.length > 0) {
                                                            var text = result[0].value + " - " + result[0].text;
                                                            var option = $('<option>').attr('value', result[0].value).text(text);
                                                            $('#ddlBankUpdate').append(option);
                                                        }
                                                    }).catch(function (error) {
                                                        console.error('Error:', error);
                                                    });
                                                } else {
                                                    $('#ddlBankUpdate').dropdown('clear');
                                                }

                                                $('#txtNoAkaunBankUpdate').val(result[0].noAkaun);
                                                
                                            }, 500);
                                        })

                                    } else if (item.kategoriPemiutang == 'ST') {
                                                                                
                                        GetPemiutangAllDetailValue(item.id).then(function (result) {
    
                                            $('#ddlNoStafUpdate').dropdown('clear');
                                            $('#ddlNoStafUpdate').empty();
                                            var staffPromise = GetStaffValue(item.noRujukan);
                                            staffPromise.then(function (result) {
                                                if (result && result.length > 0) {
                                                    var option = $('<option>').attr('value', item.noRujukan).text(item.noRujukan);
                                                    $('#ddlNoStafUpdate').append(option);
                                                }
                                            }).catch(function (error) {
                                                console.error('Error:', error);
                                            });

                                            setTimeout(function () {
                                                // alamat
                                                $('#txtAlamat1Update').val(result[0].alamat1)
                                                $('#txtAlamat2Update').val(result[0].alamat2)

                                                if (result[0].bandar !== null) {
                                                    $('#ddlBandarUpdate').empty();
                                                    var bandarPromise = GetBandarValue(result[0].bandar);
                                                    bandarPromise.then(function (result) {
                                                        if (result && result.length > 0) {
                                                            var text = result[0].text;
                                                            var value = result[0].value;
                                                            var option = $('<option>').attr('value', value).text(text);
                                                            $('#ddlBandarUpdate').append(option);

                                                            updateNegeriNegaraByBandar(value);
                                                        }
                                                    }).catch(function (error) {
                                                        console.error('Error:', error);
                                                    });
                                                } else {
                                                    $('#ddlBandarUpdate').dropdown('clear');
                                                }

                                                $('#ddlPoskodUpdate').empty();
                                                var option = $('<option>').attr('value', result[0].poskod).text(result[0].poskod);
                                                $('#ddlPoskodUpdate').append(option);

                                                $('#txtStafBankNameUpdate').val(result[0].kodBank);
                                                $('#txtNoAkaunBankUpdate').val(result[0].noAkaun);
                                            }, 500);
                                        })

                                    } else if (item.kategoriPemiutang == 'OA' || item.kategoriPemiutang == 'SY') {
                                        $('#txtIdUpdate').val(item.noRujukan);

                                        GetPemiutangAllDetailValue(item.id).then(function (result) {
                                            // alamat
                                            $('#txtAlamat1').val(result[0].alamat1)
                                            $('#txtAlamat2').val(result[0].alamat2)

                                            if (result[0].bandar !== null) {
                                                $('#ddlBandar').empty();
                                                var bandarPromise = GetBandarValue(result[0].bandar);
                                                bandarPromise.then(function (result) {
                                                    if (result && result.length > 0) {
                                                        var text = result[0].text;
                                                        var value = result[0].value;
                                                        var option = $('<option>').attr('value', value).text(text);
                                                        $('#ddlBandar').append(option);

                                                        updateNegeriNegaraByBandar(value);
                                                    }
                                                }).catch(function (error) {
                                                    console.error('Error:', error);
                                                });
                                            } else {
                                                $('#ddlBandar').dropdown('clear');
                                            }

                                            $('#ddlPoskod').dropdown('clear');
                                            if (result[0].poskod !== null) {
                                                $('#ddlPoskod').empty();
                                                var poskodPromise = GetPoskodValue(result[0].poskod);
                                                poskodPromise.then(function (pos) {
                                                    if (pos && pos.length > 0) {
                                                        var text = pos[0].text;
                                                        var option = $('<option>').attr('value', result[0].poskod).text(text);
                                                        $('#ddlPoskod').append(option);
                                                    }
                                                }).catch(function (error) {
                                                    console.error('Error:', error);
                                                });
                                            } else {
                                                $('#ddlPoskod').dropdown('clear');
                                            }

                                            $('#ddlBank').dropdown('clear');
                                            if (result[0].kodBank !== null) {
                                                $('#ddlBank').empty();
                                                var bankPromise = GetBankValue(result[0].kodBank);
                                                bankPromise.then(function (result) {
                                                    if (result && result.length > 0) {
                                                        var text = result[0].value + " - " + result[0].text;
                                                        var option = $('<option>').attr('value', result[0].value).text(text);
                                                        $('#ddlBank').append(option);
                                                    }
                                                }).catch(function (error) {
                                                    console.error('Error:', error);
                                                });
                                            } else {
                                                $('#ddlBank').dropdown('clear');
                                            }

                                            $('#txtNoAkaunBank').val(result[0].noAkaun);
                                        })
                                    }
                                }
                            }).catch(function (error) {
                                console.error('Error:', error);
                            });
                        }

                        $('.btnBatal').click(function () {
                            // put timeout to prevent dropdown from showing (reset state)
                            isReset = true;
                            setTimeout(function () {
                                isReset = false;
                            }, 500);
                            clearAllFields();
                        });

                        // display modal - add new pemiutang
                        $('.btnPapar').click(function () {         
                            // put timeout to prevent dropdown from showing (reset state)
                            isReset = true;
                            clearAllFields();
                            isReset = false;
                            $('#modalNewPemiutang').modal('show');
                        });

                        // get value from ddlKategoriPemiutang to display section based on selection
                        $('#ddlKategoriPemiutang').on('change', async function () {
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

                                } else if (selectedValue == "" || selectedValue == null) { // default state
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

                        // get value from ddlKategoriPemiutang to display section based on selection
                        $('#ddlKategoriPemiutangUpdate').on('change', async function () {
                            if (!isReset) {
                                // kategori
                                var selectedValue = $(this).val();

                                if (selectedValue === "ST") { // if Staf
                                    // hide not necessary section
                                    $('#sectionIdUpdate').removeClass('d-flex');
                                    $('#sectionIdUpdate').addClass('d-none');
                                    $('#sectionPelajarUGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarUGUpdate').addClass('d-none');
                                    $('#sectionPelajarPGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPGUpdate').addClass('d-none');
                                    $('#sectionPelajarPHUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPHUpdate').addClass('d-none');

                                    $('#sectionDefaultBankUpdate').removeClass('d-flex');
                                    $('#sectionDefaultBankUpdate').addClass('d-none');

                                    // show necessary section
                                    $('#sectionStafUpdate').removeClass('d-none');
                                    $('#sectionStafUpdate').addClass('d-flex');

                                    $('#ddlNoStafUpdate').dropdown('clear');
                                    $('#ddlNoStafUpdate').dropdown('refresh');

                                    $('#sectionStafBankUpdate').removeClass('d-none');
                                    $('#sectionStafBankUpdate').addClass('d-flex');

                                } else if (selectedValue === "PL") { // if Pelajar UG
                                    // hide not necessary section
                                    $('#sectionIdUpdate').removeClass('d-flex');
                                    $('#sectionIdUpdate').addClass('d-none');
                                    $('#sectionStafUpdate').removeClass('d-flex');
                                    $('#sectionStafUpdate').addClass('d-none');
                                    $('#sectionPelajarPGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPGUpdate').addClass('d-none');
                                    $('#sectionPelajarPHUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPHUpdate').addClass('d-none');

                                    $('#sectionStafBankUpdate').removeClass('d-flex');
                                    $('#sectionStafBankUpdate').addClass('d-none');

                                    // show necessary section
                                    $('#sectionPelajarUGUpdate').removeClass('d-none');
                                    $('#sectionPelajarUGUpdate').addClass('d-flex');

                                    $('#ddlNoMatrikUGUpdate').dropdown('clear');
                                    $('#ddlNoMatrikUGUpdate').dropdown('refresh');

                                    $('#sectionDefaultBankUpdate').removeClass('d-none');
                                    $('#sectionDefaultBankUpdate').addClass('d-flex');

                                } else if (selectedValue == "PG") { // if Pelajar PG
                                    // hide not necessary section
                                    $('#sectionIdUpdate').removeClass('d-flex');
                                    $('#sectionIdUpdate').addClass('d-none');
                                    $('#sectionStafUpdate').removeClass('d-flex');
                                    $('#sectionStafUpdate').addClass('d-none');
                                    $('#sectionPelajarUGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarUGUpdate').addClass('d-none');
                                    $('#sectionPelajarPHUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPHUpdate').addClass('d-none');

                                    $('#sectionStafBankUpdate').removeClass('d-flex');
                                    $('#sectionStafBankUpdate').addClass('d-none');

                                    // show necessary section
                                    $('#sectionPelajarPGUpdate').removeClass('d-none');
                                    $('#sectionPelajarPGUpdate').addClass('d-flex');

                                    $('#ddlNoMatrikPGUpdate').dropdown('clear');
                                    $('#ddlNoMatrikPGUpdate').dropdown('refresh');

                                    $('#sectionDefaultBankUpdate').removeClass('d-none');
                                    $('#sectionDefaultBankUpdate').addClass('d-flex');

                                } else if (selectedValue == "PH") { // if Pelajar PH / same with UG
                                    // hide not necessary section
                                    $('#sectionIdUpdate').removeClass('d-flex');
                                    $('#sectionIdUpdate').addClass('d-none');
                                    $('#sectionStafUpdate').removeClass('d-flex');
                                    $('#sectionStafUpdate').addClass('d-none');
                                    $('#sectionPelajarPGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPGUpdate').addClass('d-none');
                                    $('#sectionPelajarPHUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPHUpdate').addClass('d-none');

                                    $('#sectionStafBankUpdate').removeClass('d-flex');
                                    $('#sectionStafBankUpdate').addClass('d-none');

                                    // show necessary section
                                    $('#sectionPelajarUGUpdate').removeClass('d-none');
                                    $('#sectionPelajarUGUpdate').addClass('d-flex');

                                    $('#ddlNoMatrikUGUpdate').dropdown('clear');
                                    $('#ddlNoMatrikUGUpdate').dropdown('refresh');

                                    $('#sectionDefaultBankUpdate').removeClass('d-none');
                                    $('#sectionDefaultBankUpdate').addClass('d-flex');

                                } else if (selectedValue === "OA") { // if Orang Awam
                                    // hide not necessary section
                                    $('#sectionNoStaf').removeClass('d-flex');
                                    $('#sectionNoStaf').addClass('d-none');
                                    $('#sectionStafUpdate').removeClass('d-flex');
                                    $('#sectionStafUpdate').addClass('d-none');
                                    $('#sectionPelajarUGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarUGUpdate').addClass('d-none');
                                    $('#sectionPelajarPGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPGUpdate').addClass('d-none');
                                    $('#sectionPelajarPHUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPHUpdate').addClass('d-none');

                                    $('#sectionStafBankUpdate').removeClass('d-flex');
                                    $('#sectionStafBankUpdate').addClass('d-none');

                                    // show necessary section
                                    $('#sectionIdUpdate').removeClass('d-none');
                                    $('#sectionIdUpdate').addClass('d-flex');
                                    $('#lblId').html("No. Kad Pengenalan");

                                    $('#sectionDefaultBankUpdate').removeClass('d-none');
                                    $('#sectionDefaultBankUpdate').addClass('d-flex');

                                } else if (selectedValue == "SY") { // if Syarikat
                                    // hide not necessary section
                                    $('#sectionNoStaf').removeClass('d-flex');
                                    $('#sectionNoStaf').addClass('d-none');
                                    $('#sectionStafUpdate').removeClass('d-flex');
                                    $('#sectionStafUpdate').addClass('d-none');
                                    $('#sectionPelajarUGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarUGUpdate').addClass('d-none');
                                    $('#sectionPelajarPGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPGUpdate').addClass('d-none');
                                    $('#sectionPelajarPHUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPHUpdate').addClass('d-none');

                                    $('#sectionStafBankUpdate').removeClass('d-flex');
                                    $('#sectionStafBankUpdate').addClass('d-none');

                                    // show necessary section
                                    $('#sectionIdUpdate').removeClass('d-none');
                                    $('#sectionIdUpdate').addClass('d-flex');
                                    $('#lblId').html("No. Syarikat");

                                    $('#sectionDefaultBankUpdate').removeClass('d-none');
                                    $('#sectionDefaultBankUpdate').addClass('d-flex');

                                } else if (selectedValue == "") { // default state
                                    // hide not necessary section
                                    $('#sectionNoStaf').removeClass('d-flex');
                                    $('#sectionNoStaf').addClass('d-none');
                                    $('#sectionStafUpdate').removeClass('d-flex');
                                    $('#sectionStafUpdate').addClass('d-none');
                                    $('#sectionPelajarUGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarUGUpdate').addClass('d-none');
                                    $('#sectionPelajarPGUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPGUpdate').addClass('d-none');
                                    $('#sectionPelajarPHUpdate').removeClass('d-flex');
                                    $('#sectionPelajarPHUpdate').addClass('d-none');

                                    $('#sectionStafBankUpdate').removeClass('d-flex');
                                    $('#sectionStafBankUpdate').addClass('d-none');

                                    // show necessary section
                                    $('#sectionIdUpdate').removeClass('d-none');
                                    $('#sectionIdUpdate').addClass('d-flex');
                                    $('#lblId').html("No. Kad Pengenalan / No. Syarikat");

                                    $('#sectionDefaultBankUpdate').removeClass('d-none');
                                    $('#sectionDefaultBankUpdate').addClass('d-flex');
                                }
                            }
                        });

                        // get value from ddlNoStaf for auto-select No Staff
                        $('#ddlNoStaf').on('change', async function () {
                            if (!isReset) {
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
                            isReset = false;
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

                                $('#ddlNegeriUpdate').empty();
                                for (var i = 0; i < kodNegeriArray.length; i++) {
                                    if (kodNegeriArray[i].value === idNegeri) {
                                        var text = kodNegeriArray[i].text;
                                        var value = kodNegeriArray[i].value;
                                        var option = $('<option>').attr('value', value).text(text);
                                        $('#ddlNegeriUpdate').append(option);
                                        isExist = true;

                                        $('#ddlNegaraUpdate').empty();
                                        var textNegara = "Malaysia";
                                        var valueNegara = "MY";
                                        var optionNegara = $('<option>').attr('value', valueNegara).text(textNegara);
                                        $('#ddlNegaraUpdate').append(optionNegara);
                                    }
                                }

                                if (!isExist) {
                                    $('#ddlNegeri').dropdown('clear');
                                    $('#ddlNegara').dropdown('clear');
                                }
                            }
                            isReset = true;
                        }

                        // get data from server for $('#ddlKategoriPemiutang') dropdown when row is selected
                        function GetKategoriValue(kod, callback) {
                            return new Promise(function (resolve, reject) {
                                $.ajax({
                                    url: 'PemiutangWS.asmx/GetKategoriValue',
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
                                    url: 'PemiutangWS.asmx/GetNegaraValue',
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
                                    url: 'PemiutangWS.asmx/GetNegeriValue',
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
                                    url: 'PemiutangWS.asmx/GetPoskodValue',
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
                                    url: 'PemiutangWS.asmx/GetBandarValue',
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
                                    url: 'PemiutangWS.asmx/GetBandarValueMap',
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
                                    url: 'PemiutangWS.asmx/GetBankValue',
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
                                    url: 'PemiutangWS.asmx/GetKodStaffList',
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
                        function GetPemiutangAllDetailValue(kod, callback) {
                            return new Promise(function (resolve, reject) {
                                $.ajax({
                                    url: 'PemiutangWS.asmx/GetPemiutangAllDetailValue',
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
                                    url: 'PemiutangWS.asmx/GetStafValue',
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
                                    url: 'PemiutangWS.asmx/GetPelajarUGValue',
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
                                    url: 'PemiutangWS.asmx/GetPelajarPGValue',
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