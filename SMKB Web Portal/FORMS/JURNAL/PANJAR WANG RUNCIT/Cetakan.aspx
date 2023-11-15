<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Cetakan.aspx.vb" Inherits="SMKB_Web_Portal.Cetakan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>


    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/moment.js/2.11.2/moment.min.js"></script>
    <script src="//cdn.datatables.net/plug-ins/1.10.12/sorting/datetime-moment.js"></script>

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

    <div id="CetakanTab" class="tabcontent" style="display: block">

        <div class="modal fade" id="cetak" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true" runat="server">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">

                    <div class="modal-body">
                        <div id="letter" class="pageA4" runat="server" style="width: 100%; background-color: white">
                            <div class="border border-dark">
                                <table style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th scope="col"></th>
                                            <th scope="col"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <p>BORANG PERMOHONAN PANJAR WANG RUNCIT</p>
                                            </td>
                                            <td class="border-left" style="width: 0">
                                                <img id="water" src="../../../Images/logo.png" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div class="border border-dark">
                                <table style="font-size: 12px; width: 95%">
                                    <thead style="display: none">
                                        <tr>
                                            <th scope="col"></th>
                                            <th scope="col"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <label>NAMA:&nbsp;</label>
                                                <label class="namaPekerja" style="font-weight: bold"></label>
                                            </td>
                                            <td style="width: 30%">
                                                <label>FAKULTI/JABATAN/UNIT:&nbsp;</label>
                                                <label class="pejabat" style="font-weight: bold"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>NO. PEKERJA:&nbsp;</label>
                                                <label class="noPekerja" style="font-weight: bold"></label>
                                            </td>
                                            <td>
                                                <label>NO. AKAUN BANK:&nbsp;</label>
                                                <label class="noAkaun" style="font-weight: bold"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>NO. TEL PEJABAT & NOMBOR HP:&nbsp;</label>
                                                <label class="noHp"></label>
                                            </td>
                                            <td>
                                                <label>EMEL:&nbsp;</label>
                                                <label class="emel" style="font-weight: bold"></label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>TUJUAN:&nbsp;</label>
                                                <label class="tujuan"></label>
                                            </td>
                                            <td>
                                                <label>NO. RUJUKAN:&nbsp;</label>
                                                <label class="no-wpr" style="font-weight: bold"></label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <table class="table-bordered" style="width: 95%; margin:auto">
                                    <thead>
                                        <tr>
                                            <th scope="col" style="text-align: center">Bil</th>
                                            <th scope="col" style="text-align: center">TUJUAN</th>
                                            <th scope="col">JUMLAH (RM)</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblHdr">
                                        <tr>
                                            <td class="bil" style="text-align: center">1</td>
                                            <td class="tujuan">KEGUNAAN PEJABAT</td>
                                            <td class="jumlah" style="width: 15%; text-align: right"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <br />
                                <div class="col-md-12">
                                    <div class="form-row">
                                        <div class="form-group col-md-9">
                                            <label>Tandatangan Pemohon:</label>
                                        </div>
                                        <div class="form-group col-md-3">
                                            <label>Tarikh:&nbsp;</label>
                                            <label class="tarikh-mohon"></label>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <p style="margin-left: 15%">
                                            Saya mengesahkan tidak terlibat dengan sebarang amalan rasuah dengan mana-mana pihak 
                                    berkaitan perolehan secara langsung atau tidak langsung.
                                        </p>
                                        <br />
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12 border border-dark">
                                <div class="form-row">
                                    <div class="col-md-6 border-right border-dark">
                                        <h6 class="text-center">1. PENGESAHAN KETUA JABATAN:<br />
                                            LULUS/TIDAK DILULUSKAN</h6>
                                        <br />
                                        <br />
                                        <%--Signature of head of department--%>
                                        <hr class="text-center" style="width: 50%; border-top: 1px solid black" />
                                        <p class="text-center"></p>
                                        <br />
                                        <label class="tarikh text-left"></label>
                                        <hr style="float: right; width: 30%; border-bottom: 1px solid black;" />
                                        <br />
                                    </div>
                                    <div class="col-md-6 border-left border-dark">
                                        <h6 class="text-center">3. KEGUNAAN PEJABAT BENDAHARI SAHAJA<br />
                                            (KELULUSAN PEMBAYARAN)</h6>
                                        <br />
                                        <br />
                                        <hr class="text-center" style="width: 50%; border-top: 1px solid black" />
                                        <br />
                                        <br />
                                        <label class="tarikhN text-left">Tarikh:</label>
                                        <hr style="float: right; width: 30%; border-bottom: 1px solid black;" />
                                        <br />
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12 border border-dark">
                                <div class="form-row">
                                    <div class="col-md-6 border-right border-dark">
                                        <h6 class="text-center">2. KEGUNAAN PEJABAT BENDAHARI SAHAJA</h6>
                                        <br />
                                        <br />
                                        <hr class="text-center" style="width: 50%; border-top: 1px solid black" />
                                        <p class="text-center"></p>
                                        <br />
                                        <label class="tarikh text-left"></label>
                                        <hr style="float: right; width: 30%; border-bottom: 1px solid black;" />
                                        <br />
                                    </div>
                                    <div class="col-md-6 border-left border-dark">
                                        <h6 class="text-center">4. KEGUNAAN PEJABAT BENDAHARI SAHAJA<br />
                                            (UNIT PEMBAYARAN)</h6>
                                        <br />
                                        <p></p>
                                        <hr class="text-center" style="width: 50%; border-top: 1px solid black" />
                                        <p class="text-center">Nama Staf & Tandatangan</p>
                                        <br />
                                        <label class="tarikhN text-left">Tarikh:</label>
                                        <hr style="float: right; width: 30%; border-bottom: 1px solid black;" />
                                        <br />
                                    </div>
                                </div>
                            </div>
                            <button id="btnPrint" runat="server" class="btn btnPrint" type="button" style="margin-top: 33px">
                                <i class="fa fa-print" style="font-size:30px"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="form-row">

            <div class="col-md-12">
                <div class="list-table table-responsive cetak">
                    <table class="table table-striped" style="width: 95%" id="tblSenarai">
                        <thead>
                            <tr>
                                <th scope="col">Tarikh Mohon</th>
                                <th scope="col">No. WPR</th>
                                <th scope="col">Jabatan/Fakulti</th>
                                <th scope="col">Amaun (RM)</th>
                            </tr>
                        </thead>

                        <tbody id="tableID">
                        </tbody>
                    </table>
                </div>
            </div>

        </div>

    </div>

    <script>
        /*let table = new DataTable(tblSenarai);*/

        function showPrint(elm) {
            if (elm == "1") {

                $('#cetak').modal('toggle');


            }
            else if (elm == "2") {

                //$(".modal-body div").val("");
                //$('#cetak').modal('toggle');
              

                // Create a new window for printing
                var data = document.getElementById('<%=letter.ClientID%>').innerHTML;
                var myWindow = window.open('', 'my div', 'height=800,width=1600');
                myWindow.document.write('<html><head><title>Borang Permohonan Wang Runcit</title>');

                /*optional stylesheet*/ myWindow.document.write('<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N" crossorigin="anonymous"/>');
                /*optional stylesheet*/ myWindow.document.write('<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.2/css/all.min.css" />');

                myWindow.document.write('</head><body >');
                myWindow.document.write(data);
                myWindow.document.write('</body></html>');
                myWindow.document.close(); // necessary for IE >= 10
                myWindow.focus();
                //myWindow.document.getElementById('<%=btnPrint.ClientID%>').addEventListener('click', CreatePDFfromHTML);
                myWindow.document.getElementById('<%=btnPrint.ClientID%>').addEventListener('click', function () {
                    myWindow.document.getElementById('<%=btnPrint.ClientID%>').style.display = "none";
                    myWindow.print();
                });
                //myWindow.onload = function () { // necessary if the div contain images

                    //    myWindow.focus(); // necessary for IE >= 10
                    //    //myWindow.print();
                    //    myWindow.close();
                    //};
                    //myWindow.focus(); // necessary for IE >= 10
                    //myWindow.print();
                    //myWindow.close();
                }
            }

            $(document).ready(function () {
                $.fn.dataTable.moment('D-MM-YYYY');
                $('#tblSenarai').DataTable({
                    responsive: true,
                    searching: true,
                    pagingType: "full_numbers",
                    language: {
                        paginate: {
                            next: '<i class="fa fa-forward"></i>',
                            previous: '<i class="fa fa-backward"></i>',
                            first: '<i class="fa fa-step-backward"></i>',
                            last: '<i class="fa fa-step-forward"></i>'
                        },
                        lengthMenu: "Tunjuk _MENU_ rekod",
                        info: "Menunjukkan _END_ dari _TOTAL_ rekod",
                        infoEmpty: "Menunjukan 0 ke 0 dari rekod",
                        emptyTable: "Tiada rekod",
                        loadingRecords: "Memuatkan",
                        infoFiltered: "(ditapis dari _MAX_ jumlah rekod)",
                        zeroRecords: "Tiada rekod dijumpai",
                        search: "Carian"
                    },
                    ajax: {
                        url: "Panjar_WS.asmx/LoadOrderRecord_SenaraiCetakan",
                        method: 'POST',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        dataSrc: function (json) {
                            return JSON.parse(json.d);
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
                        { "data": "Tarikh_Mohon" },
                        {
                            "data": "No_Wpr",
                            render: function (data, type, row, meta) {
                                if (type !== "display") {

                                    return data;

                                }

                                var link = `<td style="width: 10%" >
                                                <label id="lblNoWpr" name="lblNoWpr" class="lblNoWpr" value="${data}" >${data}</label>
                                                <input type ="hidden" class = "lblNoWpr" value="${data}"/>
                                            </td>`;
                                return link;
                            }
                        },
                        { "data": "Kod_PTJ" },
                        { "data": "Jumlah_Belanja" }
                    ]
                });
            });

            $('.print-modal').click(function () {
                CreatePDFfromHTML();
            });

            var tableID = '#tblSenarai tbody';

            function setDefault(theVal, defVal) {

                if (defVal === null || defVal === undefined) {
                    defVal = "";
                }

                if (theVal === "" || theVal === undefined || theVal === null) {
                    theVal = defVal;
                }
                return theVal;
            }

            // add clickable event in DataTable row
            async function rowHoverClick(id) {
                if (id !== "") {
                    if (id !== "") {
                        var record = await AjaxGetRecord(id);
                        await AddRowHeader(null, record);
                        showPrint(2);
                        //CreatePDFfromHTML();
                    }
                }
            }

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
                    var imgData = canvas.toDataURL("image/jpg", 1.0);
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

            async function AddRowHeader(totalClone, objOrder) {
                var counter = 1;
                if (objOrder !== null && objOrder !== undefined) {
                    totalClone = objOrder.Payload.length;
                }


                if (counter <= objOrder.Payload.length) {
                    await setValueToCetak(objOrder.Payload[counter - 1]);
                }

            }

            async function setValueToCetak(order) {
                $('.no-wpr').html(order.No_Wpr);
                //$('.namaPekerja').html(order.Nama);
                $('.noPekerja').html(<%= Session("ssusrID")%>);
                //$('.emel').html(order.Email);
                //$('.noAkaun').html(order.Akaun);
                $('.pejabat').html(order.Pejabat);
                $('.kumpulan').html(order.Kod_Kump_Wang);
                $('.jumlah').html(parseFloat(order.Jumlah_Belanja).toFixed(2));
                $('.tarikh-mohon').html(order.Tarikh_Mohon);

                //Get current date
                var d = new Date();
                var date = "Tarikh: " + d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();
                $('.tarikh').html(date);
            }

            async function AjaxGetRecord(id) {

                try {

                    const response = await fetch('Panjar_WS.asmx/LoadCetakan', {
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
    </script>
</asp:Content>
