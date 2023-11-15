<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="TransaksiLejarAm.aspx.vb" Inherits="SMKB_Web_Portal.TransaksiLejarAm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.html5.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.print.min.js" crossorigin="anonymous"></script>


    <style>
        .dropdown-list {
            width: 100%; /* You can adjust the width as needed */
        }

        .align-right {
            text-align: right;
        }

        .center-align {
            text-align: center;
        }

        .custom-table > tbody > tr:hover {
            background-color:#ffc83d !important;
        }

        #tblDataSenarai_LejarAm td:hover {
            cursor: pointer;
          }

       /* .custom-table > tbody > tr.active {

        }*/

        .modal-body {
            padding: 0px 20px!important;
        }

        .modal-header {
            padding: 10px 10px!important;
        }


        .modal-header--sticky {
            position: sticky;
            top: 0;
            background-color: inherit;
            z-index: 9999;
        }

        .modal-footer--sticky {
            position: sticky;
            bottom: 0;
            background-color: inherit;
            z-index: 9999;
        }

         .table th, .table td {
            padding: 5px!important;
        }
 
    </style>


    <div id="PermohonanTab" class="tabcontent" style="display: block">

        <!-- Modal -->
        <div id="permohonan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Laporan Transaksi Lejar Am</h5>
                    </div>

                    <!-- Create the dropdown filter -->
                    <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="syarikat" class="col-sm-2 col-form-label" style="text-align: right">Syarikat :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="syarikat" runat="server" DataTextField="Nama" DataValueField="Nama" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="ptj" class="col-sm-2 col-form-label" style="text-align: right">Ptj :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ptj" runat="server" DataTextField="Pejabat" DataValueField="KodPejabat" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                                                 <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="ptj" class="col-sm-2 col-form-label" style="text-align: right">Kod KW :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="kodkwlist" runat="server" DataTextField="Kod_Kw" DataValueField="Kod_Kump_Wang" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="ptj" class="col-sm-2 col-form-label" style="text-align: right">Vot :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="votlist" runat="server" DataTextField="butiran" DataValueField="kod" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                              <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="tahun" class="col-sm-2 col-form-label" style="text-align: right">Tahun :</label>
                                <div class="col-sm-7">
                                    <select id="tahun" runat="server" class="ui fluid search dropdown selection custom-select">
                                        <option value="">-- Sila Pilih --</option>

                                        <option value="2023">2023</option>
                                        <option value="2022">2022</option>
                                        <option value="2021">2021</option>
                                        <option value="2020">2020</option>
                                      
                                    </select>
                                </div>
                                <button id="btnSearch" runat="server" class="btn btnSearch" onclick="return beginSearch();" type="button">
                                    <i class="fa fa-search"></i>
                                </button>
                            </div>
                        </div>                    
                        <br />
                    </div>

                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai_LejarAm" class="table table-striped custom-table" style="width: 100%">
                                 <thead>
                                        <tr>
                                            <th colspan="5" style="text-align:center;border-right:1px solid lightgrey">Carta Akaun</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Jan</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Feb</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Mac</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Apr</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Mei</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Jun</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">July</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Ogos</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Sep</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Okt</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Nov</th>
                                            <th scope="col" colspan ="2" style="text-align:center;border-right:1px solid lightgrey">Dis</th>
                                        </tr>
                                        <tr>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">KW</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Operasi</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">PTJ</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Vot</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Projek</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Dt</th>
                                            <th scope="col" style="text-align:center;border-right:1px solid lightgrey">Kt</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_LejarAm">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-show-vote" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog">
         <div class="modal-dialog-scrollable">

        <div class="modal-content modal-xl">
           <div class="modal-header modal-header--sticky">
            <h5 class="modal-title" id="exampleModalLabel">Laporan Transaksi Lejar Am</h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <div style="margin:8px 0px">
            <strong>Kod KW :</strong> <span id="kodkw"></span>&nbsp;&nbsp;
            <strong>Kod Operasi :</strong> <span id="kodko"></span>&nbsp;&nbsp;<br />
            <strong>Kod PTJ :</strong> <span id="kodptj"></span>&nbsp;&nbsp;
            <strong>Kod Vot :</strong> <span id="kodvot"></span>&nbsp;&nbsp;
            <strong>Kod Projek :</strong> <span id="kodprojek"></span><br>
            </div>



           <div class="table-responsive">
            <table id="namatable" class="table table-bordered table-striped w-100">
                <thead>
                    <tr>
                        <th scope="col">Bulan</th>
                        <th scope="col">Debit</th>
                        <th scope="col">Kredit</th>
                    </tr>

                </thead>
                <tbody>
                </tbody>
            </table>
         </div>
       </div>
 
          <div class="modal-footer modal-footer--sticky" style="padding:0px!important">
            <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
          </div>
        </div>
      </div>
    </div>
  </div>
  


  <div class="modal" id="testmodal" tabindex="-1" role="dialog">
  <div class="modal-dialog  modal-dialog-centered modal-xl" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Modal title</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body style="padding: "1px;">
        <p>Modal body text goes here.</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>


    <script type="text/javascript">
        show_loader();
        var tbl = null;
        var thn = '';

        $(document).ready(function () {

            $(".ui.dropdown").dropdown({
                fullTextSearch: true
            });

            tbl = $("#tblDataSenarai_LejarAm").DataTable({
                "responsive": true,
                "searching": true,
                cache: true,
                dom: 'Bfrtip',

                buttons: [
                    //    'csv', 'excel', 'pdf'
                ],

 <%--               buttons: [
                    'csv', 'excel', 'pdf', {

                        extend: 'print',
                        text: '<i class="fa fa-files-o green"></i> Print',
                        titleAttr: 'Print',
                        className: 'ui green basic button',
                        action: function (e, dt, button, config) {
                            window.open('<%=ResolveClientUrl("~/index.aspx")%>', '_blank');

                            }
                        }
                    ],--%>

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
                "ajax":
                {
                    "url": "LejarPenghutangWS.asmx/LoadOrderRecord_TransaksiLejarAm",
                    type: 'POST',
                    data: function (d) {
                        return "{ tahun: '" + $('#<%=tahun.ClientID%>').val() + "',syarikat: '" + $('#<%=syarikat.ClientID%>').val() + "', ptj: '" + "',kodkw: '" + $('#<%=kodkwlist.ClientID%>').val() + "', ptj: '" + $('#<%=ptj.ClientID%>').val() + "', kodjenis: '" + $('#<%=votlist.ClientID%>').val() + "'}"
                        },
                        "contentType": "application/json; charset=utf-8",
                        "dataType": "json",
                        "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    }
                },
                "columns": [
                    { "data": "Kod_Kump_Wang"},
                    { "data": "Kod_Operasi"},
                    { "data": "Kod_PTJ" },
                    { "data": "Kod_Vot" },
                    { "data": "Kod_Projek" },
                    { "data": "Dr_1", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_1", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_2", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_2", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_3", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_3", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_4", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_4", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_5", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_5", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_6", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_6", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_7", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_7", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_8", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_8", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_9", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_9", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_10", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_10", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_11", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_11", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Dr_12", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },
                    { "data": "Cr_12", "render": function (data) { return parseFloat(data).toLocaleString('en-US', { valute: 'USD', minimumFractionDigits: 2 }); }, "className": "align-right" },

            //code asal guna rowcallback        
            //    ], rowCallback: function (row, data) {
            //        $(row).click(function () {
            //            GetVotDetails(data);
            //        });
            //    }
                ]
            });
            close_loader();

            //method ganti rowcallback
            $('#tblDataSenarai_LejarAm').on('click', 'tr', function () {
                var row = tbl.row(this);
                GetVotDetails(row.data());
            });
        });


        $('.close-popup').click(function () {
            $('#tblDataSenara   i_LejarAm tbody tr.active').removeClass("active");
        })

        function beginSearch() {
            show_loader();
            tbl.ajax.reload();
            close_loader();
        }

        function GetVotDetails(data) {
            console.log(data);
            $('#kodkw').text(data.Kod_Kump_Wang);
            $('#kodko').text(data.Kod_Operasi);
            $('#kodptj').text(data.Kod_PTJ);
            $('#kodvot').text(data.Kod_Vot);
            $('#kodprojek').text(data.Kod_Projek);

            $('#namatable tbody').html("");
            $('#jumTotalDebit').val(0);
            $('#jumTotalKredit').val(0);
            $('#modal-show-vote').modal('toggle')
            $.ajax({
                url: 'LejarPenghutangWS.asmx/LoadVotDetails',
                method: 'POST',
                data: JSON.stringify({
                    KodVot: data.Kod_Vot,
                    KodPtj: data.Kod_PTJ,
                    Tahun: $('#<%=tahun.ClientID%>').val(),
                    KodKW: data.Kod_Kump_Wang,
                    KodOperasi: data.Kod_Operasi,
                    KodProjek: data.Kod_Projek
                }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    data = JSON.parse(data.d);
                    console.log(data);
                    var counter = 0;
                    var totalDebits = 0;
                    var totalKredits = 0;

                    while (counter < data.length) {
                        totalDebits = totalDebits + data[counter].Debit;
                        totalKredits = totalKredits + data[counter].Kredit;

                        var formattedDebit = parseFloat(data[counter].Debit).toLocaleString();
                        var formattedKredit = parseFloat(data[counter].Kredit).toLocaleString();

                        $('<tr>')
                            .append($('<td>').html(data[counter].namaBulan))
                            .append($('<td>').html(formattedDebit).addClass("align-right"))
                            .append($('<td>').html(formattedKredit).addClass("align-right"))
                            .appendTo($('#namatable tbody'));
                        counter += 1;
                    }
                    $('#jumTotalDebit').val(formatNumber(totalDebits));
                    $('#jumTotalKredit').val(formatNumber(totalKredits));

                    $('<tr>')
                        .append('<td><strong>Jumlah</strong></td>')
                        .append('<td class="align-right"><strong>' + formatNumber(totalDebits) + '</strong></td>')
                        .append('<td class="align-right"><strong>' + formatNumber(totalKredits) + '</strong></td>')
                        .appendTo($('#namatable tbody'));

                    function formatNumber(number) {
                        return parseFloat(number).toLocaleString();
                    }


                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Error:', errorThrown);
                }
            });
            //$.ajax({});
        }
    </script>

</asp:Content>
