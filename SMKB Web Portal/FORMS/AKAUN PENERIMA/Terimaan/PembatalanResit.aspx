<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PembatalanResit.aspx.vb" Inherits="SMKB_Web_Portal.PembatalanResit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
    <contenttemplate>
        <style>
            .default-primary {
                background-color: #007bff !important;
                color: white;
            }
            .default-primary {
                background-color: #007bff !important;
                color: white;
            }

            /*input CSS */
            .input-group {
                margin-bottom: 20px;
                position: relative;
            }

            .input-group__input {
                width: 100%;
                height: 40px;
                border: 1px solid #dddddd;
                border-radius: 5px;
                padding: 0 10px;
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
        </style>
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <div id="SenaraiKelulusan">
                <div>
                    <div class="modal-body">
                        <div class="modal-header">
                            <h5 class="modal-title">Senarai Resit</h5>
                        </div>
                        <!-- Create the dropdown filter -->
                        <div class="search-filter">
                             <div class="form-row justify-content-center">
                                  <div class="form-group row col-md-6">
                                     <label for="" class="col-sm-1 col-form-label">Carian:</label>
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
                                    <div class="col-sm-10 mt-4 d-none" id="divDatePicker">
                                        <div class="d-flex flex-row justify-content-around align-items-center">
                                            <div class="form-group row">
                                                <label class="col-sm-3 col-form-label text-nowrap">Mula:</label>
                                                <div class="col-sm-9">
                                                    <input type="date" id="txtTarikhStart" name="txtTarikhStart" class="form-control date-range-filter">
                                                </div>
                                            </div>

                                            <div class="form-group row ml-3">
                                                <label class="col-sm-3 col-form-label text-nowrap">Tamat:</label>
                                                <div class="col-sm-9">
                                                    <input type="date" id="txtTarikhEnd" name="txtTarikhEnd" class="form-control date-range-filter">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table id="tblDataSenarai" class="table table-striped">
                                        <thead>
                                            <tr style="width: 100%">
                                                <th scope="col" style="width: 10%">No. Resit</th>
                                                <th scope="col" style="width: 5%">Bil. Item</th>
                                                <th scope="col" style="width: 15%">Pembayar</th>
                                                <th scope="col" style="width: 10%">Jenis Urusniaga</th>
                                                <th scope="col" style="width: 10%">Mod Terima</th>
                                                <th scope="col" style="width: 10%">Tarikh Resit</th>
                                                <th scope="col" style="width: 15%">Jumlah Terima</th>
                                                <th scope="col" style="width: 15%">Nama Penyedia</th>
                                                <th scope="col" style="width: 10%">Status</th>
                                            </tr>
                                        </thead>
                                        <tbody id=" ">
                                        </tbody>
                                    </table>
                                </div>
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
                        <h5 class="modal-title" id="ModalCenterTitle">Maklumat Invois</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                         <div class="row">
     <div class="col-md-12">
         <div class="form-row">
             <div class="form-group col-md-3">
                 <input type="text" class="form-control input-group__input" placeholder="No. Resit" id="txtnoinv" name="txtnoinv" readonly />
                 <label class="input-group__label" for="txtnoinv">No. Resit</label>
             </div>
             <div class="form-group col-md-3">
                 <input type="text" class="form-control input-group__input" id="txtNamaPenghutang" name="txtNamaPenghutang" readonly/>
                 <label class="input-group__label" for="txtNamaPenghutang">Nama Pembayar</label>
                 <br />
                 <div style="display:none">
                     <select class="form-control ui search dropdown" name="ddlPenghutang" id="ddlPenghutang" >
                     </select>
                 </div>
             </div>
             <div class="form-group col-md-3">
                 <input type="text" class="form-control input-group__input" id="txtModTerima" name="txtModTerima" readonly />
                 <label Class="input-group__label" for="txtModTerima">Mod Terima</label>
             </div>
             <div class="form-group col-md-3" >
                 <input type="text" class="form-control input-group__input" name="tkhResit" id="tkhResit" readonly>
                 <label Class="input-group__label" for="tkhResit">Tarikh Resit</label>
             </div>
         </div>
     </div>
 </div>
 <div class="row">
     <div class="col-md-12">
         <div class="form-row">
            <%-- <div class="form-group col-md-3" style="display:none">
                 <label>Bank UTem</label>
                 <input type="text" class="form-control" id="ddlBankUTeM" name="ddlBankUTeM" readonly/>
             </div>--%>
             <div class="form-group col-md-3">
                 <input type="text" class="form-control input-group__input" id="txtUrusniaga" name="txtUrusniaga" readonly/>
                 <label Class="input-group__label" for="txtUrusniaga">Jenis Urusniaga</label>
                 <div style="display:none"><select id="ddlUrusniaga" class="ui search dropdown" name="ddlUrusniaga" ></select></div>
             </div>
             <div class="form-group col-md-6">
                 <textarea class="form-control input-group__input" name="txtTujuan" id="txtTujuan" readonly></textarea>
                 <label Class="input-group__label" for="txtTujuan">Tujuan</label>
             </div>
         </div>
     </div>
 </div>
                        <div>
                            <h6>Transaksi</h6>
                            <div class="row">
                                <div class="col-md-12">
                                    <div id="tblContainer" class="transaction-table table-responsive">
                                        <table class="table table-striped" id="tblData" style="width: 100%;">
                                            <thead>
                                                <tr style="width: 100%; text-align: center;">
                                                    <th scope="col" style="width: 10%;vertical-align:middle">Vot</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle">Kod PTJ</th>
                                                    <th scope="col" style="width: 5%;vertical-align:middle">Kumpulan Wang</th>
                                                    <th scope="col" style="width: 5%;vertical-align:middle">Kod Operasi</th>
                                                    <th scope="col" style="width: 5%;vertical-align:middle">Kod Projek</th>
                                                    <th scope="col" style="width: 15%;vertical-align:middle">Perkara</th>
                                                  <%--  <th scope="col" style="width: 10%;vertical-align:middle">Kuantiti</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle">Harga Seunit (RM)</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle">Cukai (%)</th>
                                                    <th scope="col" style="width: 10%;vertical-align:middle">Diskaun (%)</th>--%>
                                                    <th scope="col" style="width: 10%;vertical-align:middle">Jumlah (RM)</th>
                                                    <%--<th scope="col" style="width: 3%">Tindakan</th>--%>
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
                    <div class="modal-footer modal-footer--sticky">
                        <div class="form-group col-md-12" align="right">
                            <span><b>Jumlah (<span id="stickyJumlahItem"
                                        style="margin-right:5px">0</span>item) :RM <span id="stickyJumlah"
                                        style="margin-right:5px">0.00</span></b></span>
                            <button type="button" class="btn" id="showModalButton"><i class="fas fa-angle-up"></i></button>
                            <button type="button" class="btn btn-warning btnBatal">Batal</button>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        </div>
          <!-- Jumlah Detail Sticky Modal -->
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
                              <input class="form-control underline-input" id="totalwoCukai" name="totalwoCukai"
                                  style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00"
                                  readonly />
                          </td>
                      </tr>
  
                     <%-- <tr style="border-top: none">
                          <td class="pr-2" style="text-align: right; font-size: medium;">Jumlah
                              Cukai</td>
                          <td style="text-align: right">
                              <input class="form-control underline-input" id="TotalTax" name="TotalTax"
                                  style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00"
                                  readonly />
                          </td>
                      </tr>
  
                      <tr style="border-top: none">
                          <td class="pr-2" style="text-align: right; font-size: medium;">Jumlah
                              Diskaun</td>
                          <td style="text-align: right">
                              <input class="form-control underline-input" id="TotalDiskaun" name="TotalDiskaun"
                                  style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00"
                                  readonly />
                          </td>
                      </tr>--%>
  
                      <tr style="border-top: none">
                          <td class="pr-2" style="text-align: right; font-size: large">JUMLAH (RM)
                          </td>
                          <td style="text-align: right">
                              <input class="form-control underline-input" id="total" name="total"
                                  style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00"
                                  readonly />
                          </td>
                      </tr>
                  </table>
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
                Anda pasti ingin membatalkan rekod ini?
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger"
                    data-dismiss="modal">Tidak</button>
                <button type="button" class="btn default-primary btnYaLulus">Ya</button>
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
                console.log('Mouse left the button');
                detailTotalModal.hide();
            });
</script>
    <script type="text/javascript">
        var tbl = null
        var isClicked = false;
        var category_filter, startDate, endDate;
        $(document).ready(function () {
            tbl = $("#tblDataSenarai").DataTable({
                "responsive": true,
                "searching": true,
                "ajax":
                {
                    "url": "TerimaWS.asmx/LoadOrderRecord_PembatalanResit",
                    type: 'POST',
                    data: function (d) {
                        return "{ category_filter: '" + category_filter + "',isClicked: '" + isClicked + "',tkhMula: '" + startDate + "',tkhTamat: '" + endDate + "'}";
                    },
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    }
                },
                "drawCallback": function (settings) {
                    // Your function to be called after loading data
                    close_loader();
                },
                "columns": [
                    { "data": "No_Dok" },
                    { "data": "BILITEM" },
                    { "data": "Nama_Penghutang" },
                    { "data": "UrusNiaga" },
                    { "data": "MOD_TERIMA" },
                    { "data": "Tkh_Daftar" },
                    {
                        "data": "Jumlah_Bayar",
                        "render": function (data, type, row, meta) {
                            if (type === "display") {
                                return data.toLocaleString('en-MY', { valute: 'MYR', minimumFractionDigits: 2 });
                            }
                            return data;
                        }
                    },
                    { "data": "MS01_Nama" },
                    { "data": "Status" }
                ],
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
                },
            });
            $('.btnSearch').click(async function () {
                show_loader();
                isClicked = true;
                category_filter = $('#categoryFilter').val();

                if (category_filter == "6") {
                    startDate = $('#txtTarikhStart').val();
                    endDate = $('#txtTarikhEnd').val();
                } else {
                    startDate = "";
                    endDate = "";
                }
                tbl.ajax.reload();
            })

            $('#tblDataSenarai tbody').on('click', 'tr', function () {
                var data = tbl.row(this).data(); // Get the data of the clicked row
                id = data.No_Dok; // Assuming No_Invois is the ID you want to retrieve

                // Now you can fetch the details using the ID
                // and populate the modal with the details
                fetchDetailsAndPopulateModal(id);
                populateTable(id);
                $('#Senarai').modal('show');
            });

            function fetchDetailsAndPopulateModal(id) {
                $.ajax({
                    url: 'TerimaWS.asmx/LoadHdrResit',
                    type: 'POST',
                    data: { id: id },
                    success: function (response) {
                        var payload = JSON.parse($(response).text());

                        // Check if the payload contains data
                        if (payload && payload.Payload && payload.Payload.length > 0) {
                            var data = payload.Payload[0]; // Assuming you only expect one record
                            $('#txtnoinv').val(data.No_Dok);
                            $('#txtNamaPenghutang').val(data.Nama_Penghutang);
                            $('#txtModTerima').val(data.MODTERIMA);
                            $('#tkhResit').val(data.Tkh_Daftar);
                            $('#txtUrusniaga').val(data.URUSNIAGA);
                            $('#txtTujuan').val(data.Tujuan);
                        }
                    },
                    error: function (xhr, status, error) {
                        console.error('Error fetching details:', error);
                    }
                });
            }
            function populateTable(id) {
                $.ajax({
                    url: 'TerimaWS.asmx/LoadRecordResit',
                    type: 'POST',
                    data: { id: id },
                    success: function (response) {
                        $('#tableID').empty();

                        var jsonString = $(response).find('string').text();
                        var jsonData = JSON.parse(jsonString);

                        var tableBody = document.getElementById('tableID');
                        var totalKredit = 0;
                        var totalItem = 0;

                        jsonData.Payload.forEach(function (payload) {
                            var row = document.createElement('tr');
                            totalItem++;
                            var columns = [
                                'ButiranVot', 'ButiranPTJ', 'colKW', 'colKO', 'colKp', 'Butiran',
                                'Kredit'
                            ];
                            
                            columns.forEach(function (column) {
                                var cell = document.createElement('td');
                                cell.style.textAlign = 'center';

                                if (column === 'Kredit') {
                                    var value = parseFloat(payload[column]).toFixed(2);
                                    cell.innerText = value.replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    totalKredit += parseFloat(payload[column]); 
                                }
                                else {
                                    cell.innerText = payload[column];
                                }

                                row.appendChild(cell);
                            });

                            tableBody.appendChild(row);
                            $('#stickyJumlahItem').text(totalItem);
                            document.getElementById('stickyJumlah').textContent = totalKredit.toLocaleString('en-MY', { valute: 'MYR', minimumFractionDigits: 2 });
                            //document.getElementById('TotalTax').value = totalCukai.toLocaleString('en-MY', { valute: 'MYR', minimumFractionDigits: 2 });
                            //document.getElementById('TotalDiskaun').value = totalDiskaun.toLocaleString('en-MY', { valute: 'MYR', minimumFractionDigits: 2 });
                            document.getElementById('totalwoCukai').value = totalKredit.toLocaleString('en-MY', { valute: 'MYR', minimumFractionDigits: 2 });
                            document.getElementById('total').value = totalKredit.toLocaleString('en-MY', { valute: 'MYR', minimumFractionDigits: 2 });
                        });
                    },

                    error: function (error) {
                        console.error('Error retrieving data:', error);
                    }
                });
            }
        });
        $("#categoryFilter").change(function () {
            var selectedValue = $('#categoryFilter').val()

            if (selectedValue === "6") {
                // Show the date inputs
                $('#divDatePicker').removeClass("d-none").addClass("d-flex");
            } else {
                // Hide the date inputs
                $('#divDatePicker').removeClass("d-flex").addClass("d-none");

            }
        });
        $('.btnBatal').click(async function () {
            // open modal confirmation
            $('#confirmationModal').modal('toggle');
        })


        $('.btnYaLulus').click(async function () {


            // Close modal confirmation
            $('#confirmationModal').modal('toggle');
            $.ajax({

                url: 'TerimaWS.asmx/SaveUpdate',
                method: 'POST',
                data: JSON.stringify({ id: id }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $('#detailMakluman').text('Resit telah dibatalkan.');
                    $('#maklumanModal').modal('show');
                    $('#Senarai').modal('hide');
                    tbl.ajax.reload();
                },
                error: function (xhr, textStatus, errorThrown) {
                    // Display the maklumanModal with the error message
                    $('#detailMakluman').text('An error occurred: ' + errorThrown);
                    $('#maklumanModal').modal('show');
                    console.error('Error:', errorThrown);
                    reject(false);
                }
            });
        });
    </script>
</asp:Content>

