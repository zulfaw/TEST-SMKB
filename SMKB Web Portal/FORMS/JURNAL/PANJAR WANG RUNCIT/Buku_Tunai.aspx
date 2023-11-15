<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Buku_Tunai.aspx.vb" Inherits="SMKB_Web_Portal.Buku_Tunai" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.2/moment.min.js"></script>
    <script src="//cdn.datatables.net/plug-ins/1.10.12/sorting/datetime-moment.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>


    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />

    <style>
        .pageA4 {
            size: a4 portrait;
            margin-top: 0;
            margin-bottom: 0;
        }

        .modal-body {
            border: 2px solid black;
        }
    </style>


    <div id="bukuTunaiTab" class="tabcontent" style="display: block">

        <div class="modal fade" id="cetak" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true" runat="server">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">

                    <div class="modal-body">
                        <div id="letter" class="pageA4" runat="server" style="width: 100%; background-color: white">

                            <img id="water" src="../../../Images/logo.png" style="display: block; margin: auto" />
                            <br />
                            <div class="col-md-12 text-center">
                                <p style="font-size: 14px; font-weight: bold">UNIVERSITI TEKNIKAL MALAYSIA MELAKA</p>
                                <p style="font-size: 16px; font-weight: bold">TUNTUTAN PANJAR WANG RUNCIT</p>
                                <p style="font-size: 16px; font-weight: bold" class="Sesi"></p>

                                <p style="font-size: 14px; font-weight: bold" class="text-left">BUTIR PERBELANJAAN PANJAR WANG RUNCIT</p>
                            </div>

                            <div class="col-md-12">

                                <div class="table-responsive">
                                    <table id="tblLaporan" class="table-bordered" style="width: 95%">
                                        <thead class="table-warning">
                                            <tr>
                                                <th scope="col">Bil</th>
                                                <th scope="col">Tarikh</th>
                                                <th scope="col">Butiran</th>
                                                <th scope="col">No. Resit</th>
                                                <th scope="col">Vot</th>
                                                <th scope="col">Jumlah</th>
                                                <th scope="col">Baki</th>
                                                <th scope="col">Catatan</th>
                                            </tr>
                                        </thead>
                                        <tbody class="table-info">
                                            <tr style="display: none">
                                                <td>
                                                    <label id="bil" class="Bil"></label>
                                                </td>
                                                <td>
                                                    <label class="trkMohon"></label>
                                                </td>
                                                <td>
                                                    <label class="Butiran"></label>
                                                </td>
                                                <td>
                                                    <label class="Resit"></label>
                                                </td>
                                                <td>
                                                    <label class="Vot"></label>
                                                </td>
                                                <td>
                                                    <label class="Jumlah"></label>
                                                </td>
                                                <td>
                                                    <label class="Baki"></label>
                                                </td>
                                                <td>
                                                    <label class="Catatan"></label>
                                                </td>
                                            </tr>
                                        </tbody>
                                        <tfoot class="table-secondary">
                                            <tr>
                                                <td colspan="5"></td>
                                                <td>
                                                    <label class="totalJum font-weight-bold"></label>
                                                </td>
                                                <td colspan="2"></td>
                                            </tr>
                                        </tfoot>
                                    </table>
                                </div>

                            </div>

                            <button id="btnPrint" runat="server" class="btn btnPrint" type="button" style="margin-top: 33px">
                                <i class="fa fa-print" style="font-size: 30px"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="bukutunai">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Laporan</h5>
                </div>

                <!-- Create the dropdown filter -->
                <div class="search-filter">
                    <div class="form-row justify-content-center">
                        <div class="form-group row col-md-6 ">
                            <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align: right">Tahun :</label>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <select id="categoryFilterTahun" class="custom-select">
                                        <option value="2023" selected="selected">2023</option>
                                        <option value="2022">2022</option>
                                        <option value="2021">2021</option>
                                        <option value="2020">2020</option>
                                        <option value="2019">2019</option>
                                    </select>
                                </div>
                            </div>

                            <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align: right">Bulan :</label>
                            <div class="col-sm-4">
                                <div class="input-group">
                                    <select id="categoryFilterBulan" class="custom-select">
                                        <option value="1" selected="selected">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                        <option value="6">6</option>
                                        <option value="7">7</option>
                                        <option value="8">8</option>
                                        <option value="9">9</option>
                                        <option value="10">10</option>
                                        <option value="11">11</option>
                                        <option value="12">12</option>
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

                    </div>
                </div>

                <div class=" -body">
                    <div class="col-md-12">
                        <div class="application-table table-responsive">
                            <table id="tblDataSenarai_laporan" class="table table-striped" style="width: 95%">
                                <thead>
                                    <tr>
                                        <th scope="col" style="width: 10%">Bil</th>
                                        <th scope="col" style="width: 10%">Kod</th>
                                        <th scope="col">Perkara</th>
                                    </tr>
                                </thead>
                                <tbody id="tableID"></tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <script>

            var tbl = null;
            var curNumObject = 0;
            var isClicked = false;

            function showPopup() {

                var year = $('#categoryFilterTahun').val();
                var month = $('#categoryFilterBulan').val();

                if (month == 1)
                    month = "JANUARI";
                else if (month == 2)
                    month = "FEBRUARI";
                else if (month == 3)
                    month = "MAC";
                else if (month == 4)
                    month = "APRIL";
                else if (month == 5)
                    month = "MEI";
                else if (month == 6)
                    month = "JUN";
                else if (month == 7)
                    month = "JULAI";
                else if (month == 8)
                    month = "OGOS";
                else if (month == 9)
                    month = "SEPTEMBER";
                else if (month == 10)
                    month = "OKTOBER";
                else if (month == 11)
                    month = "NOVEMBER";
                else if (month == 12)
                    month = "DISEMBER";

                $('.Sesi').html(month + " " + year);

                // Create a new window for printing
                var data = document.getElementById('<%=letter.ClientID%>').innerHTML;
                var myWindow = window.open('', 'my div', 'height=800,width=1600');
                myWindow.document.write('<html><head><title>Laporan Panjar Wang Runcit</title>');

                /*optional stylesheet*/ myWindow.document.write('<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N" crossorigin="anonymous" media="all"/>');
                /*optional stylesheet*/ myWindow.document.write('<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />');

                myWindow.document.write('</head><body >');
                myWindow.document.write(data);
                myWindow.document.write('</body></html>');
                myWindow.document.close(); // necessary for IE >= 10
                myWindow.focus();
                myWindow.document.getElementById('<%=btnPrint.ClientID%>').addEventListener('click', CreatePDFfromHTML);
                <%--myWindow.document.getElementById('<%=btnPrint.ClientID%>').addEventListener('click', function () {
                    myWindow.document.getElementById('<%=btnPrint.ClientID%>').style.display = "none";
                    myWindow.print();
                });--%>
                //myWindow.onload = function () { // necessary if the div contain images

                //    myWindow.focus(); // necessary for IE >= 10
                //    //myWindow.print();
                //    myWindow.close();
                //};
                //myWindow.focus(); // necessary for IE >= 10
                //myWindow.print();
                //myWindow.close();

            }

            $(document).ready(function () {

                tbl = $('#tblDataSenarai_laporan').DataTable({
                    responsive: true,
                    searching: true,
                    language: {
                        paginate: {
                            next: '<i class="fa fa-forward"></i>',
                            previous: '<i class="fa fa-backward"></i>',
                            first: '<i class="fa fa-step-backward"></i>',
                            last: '<i class="fa fa-step-forward"></i>'
                        },
                        lengthMenu: "Tunjuk _MENU_ rekod",
                        zeroRecords: "Tiada rekod yang sepadan ditemui",
                        info: "Menunjukkan _END_ dari _TOTAL_ rekod",
                        infoEmpty: "Menunjukkan 0 ke 0 daripada rekod",
                        infoFiltered: "(ditapis dari _MAX_ jumlah rekod)",
                        emptyTable: "Tiada rekod.",
                        search: "Carian"
                    },
                    ajax: {
                        url: "Panjar_WS.asmx/LoadOrder_SenaraiLaporan",
                        method: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        dataSrc: function (json) {
                            return JSON.parse(json.d);
                        },
                        data: function () {
                            //Filter date bermula dari sini - 20 julai 2023
                            var tahun = $('#categoryFilterTahun').val();
                            var bulan = $('#categoryFilterBulan').val();
                            return JSON.stringify({
                                //category_filter: $('#categoryFilter').val(),
                                isClicked: isClicked,
                                tahun: tahun,
                                bulan: bulan
                            })
                            //akhir sini
                        }
                    },
                    rowCallback: function (row, data) {
                        // Add hover effect
                        $(row).hover(function () {
                            $(this).addClass("hover pe-auto bg-warning");
                            $(this).css("cursor", "pointer");
                        }, function () {
                            $(this).removeClass("hover pe-auto bg-warning");
                        });

                        // Add click event
                        $(row).on("click", function () {
                            rowHoverClick(data.No_Wpr);
                        });
                    },
                    columns: [
                        {
                            "data": "Bil",
                            render: function (data, type, row, meta) {
                                return meta.row + meta.settings._iDisplayStart + 1;
                            }
                        },
                        { "data": "No_Wpr" },
                        {
                            "data": "Status_Dok",
                            render: function (data, type, row, meta) {
                                var link
                                data = "LAPORAN BUKU TUNAI";

                                link = data;
                                return link;
                            }
                        }
                    ]
                })
            });

            async function AddRow(totalClone, objOrder) {
                var counter = 1;
                var table = $('#tblLaporan');

                if (objOrder !== null && objOrder !== undefined) {
                    totalClone = objOrder.Payload.length;
                }

                while (counter <= totalClone) {
                    curNumObject += 1;

                    var newId_coa = "bil" + curNumObject;
                    var row = $('#tblLaporan tbody>tr:first').clone();
                    var dropdown5 = $(row).find(".bil").attr("id", newId_coa);

                    var val = ""

                    row.attr("style", "");
                    $('#tblLaporan tbody').append(row);

                    //$(newId_coa).api("query");

                    if (objOrder !== null && objOrder !== undefined) {
                        //await setValueToRow(row, objOrder.Payload.OrderDetails[counter - 1]);
                        if (counter <= objOrder.Payload.length) {
                            await setValueToRow(row, objOrder.Payload[counter - 1], counter);
                        }
                    }
                    counter += 1;
                }
            }

            async function clearAllRows() {
                $('#tblLaporan' + ">tbody>tr").each(function (index, obj) {
                    if (index > 0) {
                        obj.remove();
                    }
                })
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

            function NumDefault(theVal) {

                return setDefault(theVal, 0)
            }

            $('.btnSearch').click(async function () {
                // show_loader();
                isClicked = true;
                tbl.ajax.reload();
                //close_loader();
            })

            function CreatePDFfromHTML() {
                //alert("masuk");
                var HTML_Width = $('#<%=letter.ClientID%>').width();
                var HTML_Height = $('#<%=letter.ClientID%>').height();
                var top_left_margin = 80;
                var PDF_Width = HTML_Width + (top_left_margin * 2);
                var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
                var canvas_image_width = HTML_Width;
                var canvas_image_height = HTML_Height;

                var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;

                html2canvas($('#<%=letter.ClientID%>')[0]).then(function (canvas) {
                    var imgData = canvas.toDataURL("image/jpeg", 1.0);
                    var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                    pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);
                    for (var i = 1; i <= totalPDFPages; i++) {
                        pdf.addPage(PDF_Width, PDF_Height);
                        pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                    }
                    pdf.save("BorangPermohonanWPR.pdf");
                    //$('#<%=letter.ClientID%>').hide();
                });
            }

            async function rowHoverClick(id) {
                var year = $('#categoryFilterTahun').val();
                var month = $('#categoryFilterBulan').val();
                var record = await AjaxLoadRecord(id, year, month);
                console.log(record);
                await clearAllRows();
                await AddRow(null, record);
                showPopup();
            }


            async function setValueToRow(row, orderDetail, count) {

                $(row).find("td > .Bil").html(count);

                var tarikh = $(row).find("td > .trkMohon");
                tarikh.html(orderDetail.Tarikh_Mohon);

                var butiran = $(row).find("td > .Butiran");
                butiran.html(orderDetail.Butiran_Belanja);

                var resit = $(row).find("td > .Resit");
                resit.html(orderDetail.No_Resit)

                var vot = $(row).find("td > .Vot");
                vot.html(orderDetail.Kod_Vot);

                var jumlah = $(row).find("td > .Jumlah");
                jumlah.html(orderDetail.Jumlah_Butiran);

                var baki = $(row).find("td > .Baki");
                baki.html(orderDetail.Baki);

                calculateGrandTotal();
            }

            function calculateGrandTotal() {

                var total = $('.totalJum');
                var curTotal_Jumlah = 0;

                $('.Jumlah').each(function (index, obj) {
                    curTotal_Jumlah += parseFloat(NumDefault($(obj).text()));
                });

                total.html(curTotal_Jumlah.toFixed(2));

            }

            async function AjaxLoadRecord(id, year, month) {

                try {

                    const response = await fetch('Panjar_WS.asmx/LoadRecord_Laporan', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({ id: id, year: year, month: month })
                    });
                    const data = await response.json();
                    return JSON.parse(data.d);
                } catch (error) {
                    console.error('Error:', error);
                    return false;
                }
            }
        </script>
    </div>
</asp:Content>

