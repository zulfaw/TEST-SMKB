<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Slip_Gaji.aspx.vb" Inherits="SMKB_Web_Portal.Slip_Gaji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
     <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.html5.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.print.min.js" crossorigin="anonymous"></script>

    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
<script type="text/javascript" src="https://html2canvas.hertzen.com/dist/html2canvas.js"></script>


    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />


    <style>
        @page {
            size: a4 portrait;
            margin-top: 0;
            margin-bottom: 0;
        }


        .dropdown-list {
            width: 100%; /* You can adjust the width as needed */
        }

        .align-right {
            text-align: right;
        }

        .center-align {
            text-align: center;
        }

        .tblPendapatan {
            display: none;
        }

       .top_rw{ background-color:#f4f4f4; }
	.td_w{ }
	button{ padding:5px 10px; font-size:14px;}
    .invoice-box {
        max-width: 100%;
        margin: auto;
        padding:10px;
        border: 1px solid #eee;
        box-shadow: 0 0 10px rgba(0, 0, 0, .15);
        font-size: 14px;
        line-height: 24px;
        font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
        color: #555;
    }
    .invoice-box table {
        width: 100%;
        margin: auto;
        /*margin-left: 10px;*/
        line-height: inherit;
        text-align: left;
		border-bottom: solid 1px #ccc;
    }
    .invoice-box table td {
        padding: 5px;
        vertical-align:middle;
    }
    .invoice-box table tr td:nth-child(2) {
        text-align: right;
    }
    .invoice-box table tr.top table td {
        padding-bottom: 20px;
    }
    .invoice-box table tr.top table td.title {
        font-size: 45px;
        line-height: 45px;
        color: #333;
    }
    .invoice-box table tr.information table td {
        padding-bottom: 40px;
    }
    .invoice-box table tr.heading td {
        background: #eee;
        border-bottom: 1px solid #ddd;
        font-weight: bold;
		font-size:12px;
    }
    .invoice-box table tr.details td {
        padding-bottom: 20px;
    }
    .invoice-box table tr.item td{
        border-bottom: 1px solid #eee;
    }
    .invoice-box table tr.item.last td {
        border-bottom: none;
    }
    .invoice-box table tr.total td:nth-child(2) {
        border-top: 2px solid #eee;
        font-weight: bold;
    }
    /*@media only screen and (max-width: 600px) {
        .invoice-box table tr.top table td {
            width: 100%;
            display: block;
            text-align: center;
        }
        .invoice-box table tr.information table td {
            width: 100%;
            display: block;
            text-align: center;
        }
    }*/
    /** RTL **/
    /*.rtl {
        direction: rtl;
        font-family: Tahoma, 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;
    }
    .rtl table {
        text-align: right;
    }
    .rtl table tr td:nth-child(2) {
        text-align: left;
    }*/
    .styled-table {
    border-collapse: collapse;
    margin: 25px 0;
    font-size: 0.9em;
    font-family: sans-serif;
    min-width: 400px;
    box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
}
.styled-table thead tr {
    background-color: #009879;
    color: #ffffff;
    text-align: left;
}
.styled-table th,
.styled-table td {
    padding: 12px 15px;
    
}
.styled-table tbody tr {
    border-bottom: 1px solid #dddddd;
}
.styled-table tbody tr:nth-of-type(even) {
    background-color: #f3f3f3;
}
.styled-table tbody tr:last-of-type {
    border-bottom: 2px solid #009879;
}
.styled-table tbody tr.active-row {
    font-weight: bold;
    color: #009879;
}
    </style>

    <div id="PermohonanTab" class="tabcontent" style="display: block">

        <!-- Modal -->
        <div id="permohonan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Slip Gaji</h5>

                    </div>
                           <!-- Create the dropdown filter -->
                    <div class="search-filter">

                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="txtPerihal1" class="col-sm-2 col-form-label" style="text-align: right">No. Staf :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="descnm" DataValueField="MS01_NOSTAF" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />


                        <div class="container">
                            
                            <div class="row justify-content-center">
                                <div class="col-md-9">
                                    <div class="form-row">
                                        <div class="col-md-3 order-md-1">
                                            </div>
                                        <div class="col-md-2 order-md-1">
                                            <div class="form-group">
                                                <label for="txtTarikh1" style="display: block; text-align: center; width: 100%;">Bulan</label>
                                               <select id="ddlMonths" runat="server" class="ui fluid search dropdown selection custom-select">
                                                    <option value="">-- Sila Pilih --</option>
                                                    <option value="1">Januari</option>
                                                    <option value="2">Februari</option>
                                                    <option value="3">Mac</option>
                                                    <option value="4">April</option>
                                                    <option value="5">Mei</option>
                                                    <option value="6">Jun</option>
                                                    <option value="7">Julai</option>
                                                    <option value="8">Ogos</option>
                                                    <option value="9">September</option>
                                                    <option value="10">Oktober</option>
                                                    <option value="11">November</option>
                                                    <option value="12">Disember</option>
                                                </select>
                                            </div>
                                            </div>

                                        <div class="col-md-2 order-md-1">
                                            <div class="form-group">
                                                <label for="txtNoRujukan1" style="display: block; text-align: center; width: 100%;">Tahun</label>
                                                <select id="ddlyear" runat="server" class="ui fluid search dropdown selection custom-select">
                                                    <option value="">-- Sila Pilih --</option>
                                                    <option value="2019">2019</option>
                                                    <option value="2020">2020</option>
                                                    <option value="2021">2021</option>
                                                    <option value="2022">2022</option>
                                                    <option value="2023">2023</option>
                                                </select>
                                            </div>
                                        </div>
                    
                                        <div class="col-md-4 order-md-1">
                                            <div class="form-group">
                                                <label></label>
                                                <button runat="server" class="btn btnSearch" type="button" style="margin-top: 33px">
                                                    <i class="fa fa-search"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                                    <button id="btnPrint" runat="server" class="btn btnPrint" onclick="CreatePDFfromHTML()" type="button" style="margin-top: 33px">
                    <i class="fa fa-print"></i>
                </button>
               
                    <div class="modal-body" style="background-color:white">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                
                   <%--<div id="letter" class="pageA4"  runat="server" style="width:100%;"> 
                        
                      

                   <div id ="letter-watermark" style="z-index:-1;position:absolute;top:300px;left:180px;height:500px;width:550px;">
                                <img style="height:20%;width:20%;"
                                    id="water"  
                                    src="../../../Images/watermark.jpg"
                                   />
                            </div>    
                    
                       </div>--%>
              
                 <div class="invoice-box" id="invoice-box" style="width:80%;position:relative;height:100%;" >
                      <div id="invoice-box2">
                            <div style =" position: absolute;
                                top: 50%;
                                transform:translateY(-50%);
                                left: 50%;
                                transform:translateX(-50%);
                                opacity: 0.1;
                                z-index: 2;height:200px;">
                                <img src="../../../Images/logo.png" style="height:100%;width:100%;">
                            </div>
                      <div id="letter" class="pageA4"  runat="server" style="width:100%;position:relative;z-index:5;"> 
                                  
                    <table cellpadding="0" cellspacing="0" style="width:100%">
		                <tr style="text-align:center" >
                            <td  style="width:40%;">
		                        <img style="height:20%;width:20%;"
                                    id="water"  
                                    src="../../../Images/logo.png"
                                   />
		                   </td>
<%--		                   <td colspan="3" >
		                      <h2 style="margin-bottom: 0px;text-align:right;"> SLIP GAJI </h2>
		                   </td>--%>

  
		                </tr>
                    </table>
                     <br />

     
                          <div class="form-row">
           <%--                 <div class="form-group col-md-6" >
                               <p class="mb-1" id="pnama" style="font-size:12px;font-weight:bold"></p> 
                                <p class="mb-1" id="pnokp" style="font-size:12px;font-weight:bold">  </p>  
                                <p class="mb-1" id="pjabatan" style="font-size:12px;font-weight:bold"></p>
                            </div>
                            <div class="form-group col-md-6">
                                <p class="mb-1" id="pnostaf" style="font-size:12px;font-weight:bold"></p>
                                <p class="mb-1" id="pbank" style="font-size:12px;font-weight:bold"></p>
                                <p class="mb-1" id="ptarikh" style="font-size:12px;font-weight:bold"></p>
                            </div>--%>
                        <table id="tblListstaf" style="width:97%;padding:5px"  cellspacing="0px" cellpadding="0px" border="0" >
                            <tr style="padding:5px" > 
                                <td style="width:8%;font-size:12px;padding:5px"><b>NAMA </b></td>
                                <td id="lblNama"  style="text-align:left;font-weight:bold;width:33%;font-size:12px;"></td>
                                <td style="width:8%;font-size:12px;"><b>NO. STAF </b></td>
                                <td id="lblNoStaf" style="text-align:left;font-weight:bold;width:33%;font-size:12px;"></td>
                            </tr>
                            <tr > 
                                <td style="width:8%;font-size:12px;"><b>NO. KP </b></td>
                                <td    id="lblnokp"  style="text-align:left;font-weight:bold;width:33%;font-size:12px;"></td>
                                <td   style="width:8%;font-size:12px;"><b>BAYAR </b></td>
                                <td   style="text-align:left;font-weight:bold;width:33%;font-size:12px;">: BANK</td>
                            </tr>
                            <tr > 
                                <td style="width:8%;font-size:12px;"><b>JABATAN </b></td>
                                <td    id="lbljabatan"  style="text-align:left;font-weight:bold;width:33%;font-size:12px;"></td>
                                <td   style="width:8%;font-size:12px;"><b>TARIKH </b></td>
                                <td   id="lbltarikh" style="text-align:left;font-weight:bold;width:33%;font-size:12px;"></td>
                            </tr>

                        </table>
                        <table id="tblListHdr" style="width:97%"  cellspacing="0px" cellpadding="0px">

                           <thead>
                                <tr class="heading">
                                <td colspan="3" style="border-right:1px solid gainsboro;text-align:center">PENDAPATAN</td>
                                <td colspan="3" style="border-right:1px solid gainsboro;text-align:center">POTONGAN</td>
                            </tr>
                            <tr class="heading">
                                <td scope="col" style="width:50px">KOD</td>
                                <td scope="col" style="width:100px;text-align:left">BUTIRAN</td>
                                <td scope="col" style="width:100px;border-right:1px solid gainsboro;text-align:right">AMAUN (RM)</td>
                                <td scope="col" style="width:50px">KOD</td>
                                <td scope="col" style="width:100px;text-align:left">BUTIRAN</td>
                                <td scope="col" style="width:100px;text-align:right">AMAUN (RM)</td>
                            </tr>
                           </thead>

                            <tbody id="tbllistbody">

                            </tbody>

                        <tfoot >
                             
                            <tr style="background-color:beige;" rowspan="2"> 
                                <td colspan="2" ><b>PENDAPATAN KASAR (RM) </b></td>
                                <td colspan="1" id="lblAmaunPendapatan" style="border-right:1px solid gainsboro;text-align:right;font-weight:bold"></td>
                                <td colspan="2" ><b>JUMLAH POTONGAN (RM) </b></td>
                                <td colspan="1" id="lblAmaunPotongan" style="text-align:right;font-weight:bold"></td>
                            </tr>
                            <tr style="background-color:beige;" >
       
                                <td colspan="2" ></td>
                                <td colspan="1" style="border-right:1px solid gainsboro;text-align:right"></td>
                                <td colspan="2" ><b>PENDAPATAN BERSIH (RM) </b></td>
                                <td colspan="1" id="lblAmaunbersih" style="text-align:right; font-weight:bold" ></td>
                            </tr>
                            <tr >
                                <td colspan="6" >Nota : Slip ini adalah cetakan berkomputer dan tidak memerlukan tandatangan </td>
                            </tr>
                        </tfoot>
                            
                    </table>
                        
                       
                         </div>   
                                         
 
                        </div>
                                </div>
				    
                </div>
                             </div>
                        </div>
                    </div>
                </div>


                </div>
    </div>
 </div>


        <script type="text/javascript">
            $(".ui.dropdown").dropdown({
                fullTextSearch: true
            });


            var tbl = null;
            var bulan = '';
            var thn = '';
            $(document).ready(function () {
                $(document).on("click", ".btnSearch", function (e) {

                    //alert('Boem');
                    //alert("You have been clicked")
                    e.preventDefault();
                    
                    getInfoStaf($('#<%=DropDownList1.ClientID%>').val());
                    getInfoGaji();
                    getAllData();
                    return false;
                });
            });
         /*   $(document).ready(function () {*/
                //Take the category filter drop down and append it to the datatables_filter div. 
                //You can use this same idea to move the filter anywhere withing the datatable that you want.
                //$("#tblDataSenarai_trans_rpt_filter.dataTables_filter").append($("#DropDownList2"));


                //tbl = $("#tblListHdr").DataTable({
                //    "responsive": true,
                //    "searching": false,
                //    cache: true,
                //    dom: 'Bfrtip',
                //    buttons: [
                //        'csv', 'excel', {
                //            // 'csv', 'excel', 'pdf', {

                //            extend: 'print',
                //            text: '<i class="fa fa-files-o green"></i> Print',
                //            titleAttr: 'Print',
                //            className: 'ui green basic button',
                //            action: function (e, dt, button, config) {
<%--                                window.location.href = '<%=ResolveClientUrl("~/reportbaru4.aspx")%>';--%>
                                //PrintElem()
                               // CreatePDFfromHTML();

                    //        }
                    //    }
                    //],
                    //"sPaginationType": "full_numbers",
                    //"oLanguage": {
                    //    "oPaginate": {
                    //        "sNext": '<i class="fa fa-forward"></i>',
                    //        "sPrevious": '<i class="fa fa-backward"></i>',
                    //        "sFirst": '<i class="fa fa-step-backward"></i>',
                    //        "sLast": '<i class="fa fa-step-forward"></i>'
                    //    },
                    //    "sLengthMenu": "Tunjuk _MENU_ rekod",
                    //    "sZeroRecords": "Tiada rekod yang sepadan ditemui",
                    //    "sInfo": "Menunjukkan _END_ dari _TOTAL_ rekod",
                    //    "sInfoEmpty": "Menunjukkan 0 ke 0 daripada rekod",
                    //    "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
                    //    "sEmptyTable": "Tiada rekod.",
                    //    "sSearch": "Carian"
            //        }

            //    });

            //});

            async function getAllData() {
               

                var dataIncome = await getIncome();
                var dataPotongan = await getPotongan();
                var counter = 0;
                dataIncome = JSON.parse(dataIncome)
                dataPotongan = JSON.parse(dataPotongan)
                $('#tbllistbody').html("");

                var totalData = dataIncome.length;

                if (dataPotongan.length > totalData) {
                    totalData = dataPotongan.length;
                }
                var totalIncome = 0.00;
                var totalPotongan = 0.00;
                var totalBersih = 0.00;
                console.log(totalData);
                console.log(dataIncome);
                console.log(dataPotongan);
               
                while (counter < totalData) {

                    var potongan1 = ""
                    var potongan2 = ""
                    var potongan3 = ""
                   

                    var income1 = ""
                    var income2 = ""
                    var income3 = ""
                    

                    if (dataIncome[counter] !== null && dataIncome[counter] !== undefined) {
                        income1 = dataIncome[counter].Kod_Trans;
                        income2 = dataIncome[counter].Butiran;
                        income3 = parseFloat(dataIncome[counter].Amaun).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                        totalIncome += parseFloat(dataIncome[counter].Amaun);
                        
                        //console.log("masuk");
                    }

                   
                    if (dataPotongan[counter] !== null && dataPotongan[counter] !== undefined) {
                        potongan1 = dataPotongan[counter].Kod_Trans;
                        potongan2 = dataPotongan[counter].Butiran;
                        potongan3 = parseFloat(dataPotongan[counter].Amaun).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                        totalPotongan += parseFloat(dataPotongan[counter].Amaun);
                    }
                    
                    var tr = null;
             
                     tr = $('<tr class="item" >')
                        .append($('<td>').html(income1))
                        .append($('<td style = "text-align:left;">').html(income2))
                         .append($('<td style = "border-right:1px solid gainsboro;text-align:right;">').html(income3))
                        .append($('<td>').html(potongan1))
                        .append($('<td style = "text-align:left;">').html(potongan2))
                         .append($('<td style = "text-align:right">').html(potongan3))
                   
                    $('#tbllistbody').append(tr);
                    counter += 1;
                }
                totalBersih = totalIncome - totalPotongan;
   
                $('#lblAmaunPendapatan').html(parseFloat(totalIncome).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })); 
                $('#lblAmaunPotongan').html(parseFloat(totalPotongan).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })); 
                $('#lblAmaunbersih').html(parseFloat(totalBersih).toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));

                return false

            }

            async function getIncome() {
                var vBln = $('#<%=ddlMonths.ClientID%>').val();
                var vThn = $('#<%=ddlyear.ClientID%>').val();
                var vNostaf = $('#<%=DropDownList1.ClientID%>').val();

                return new Promise((resolve, reject) => {
                    $.ajax({
                        
                        url: '<%=ResolveClientUrl("~/FORMS/GAJI/Transaksi/Transaksi_WS.asmx/LoadListIncome")%>',
                        method: 'POST',
                        data: JSON.stringify({
                            nostaf: vNostaf,
                            tahun: vThn,
                            bulan: vBln
                        }),
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

            async function getPotongan() {
                var vBln = $('#<%=ddlMonths.ClientID%>').val();
                var vThn = $('#<%=ddlyear.ClientID%>').val();
                var vNostaf = $('#<%=DropDownList1.ClientID%>').val();

                return new Promise((resolve, reject) => {
                    $.ajax({
                        url: '<%=ResolveClientUrl("~/FORMS/GAJI/Transaksi/Transaksi_WS.asmx/LoadListPotonganSlip")%>',
                        method: 'POST',
                        data: JSON.stringify({
                            nostaf: vNostaf,
                            tahun: vThn,
                            bulan: vBln
                        }),
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

            
            //$("#btnSearch").on("click", function (e) {
            //    alert('Button Clicked');
            //    e.preventDefault();
            //    getAllData();
            //});
            //function beginSearch() {
            //    getAllData();
            //   // tbl.ajax.reload();


            //}
            function PrintElem() {
                var data = document.getElementById('<%=letter.ClientID%>').innerHTML;
                Popup(data);
               // CreatePDFfromHTML()
               <%-- window.jsPDF = window.jspdf.jsPDF;

                var doc = new jsPDF();

                // Source HTMLElement or a string containing HTML.
                var elementHTML = document.querySelector('<%=letter.ClientID%>');

                doc.html(elementHTML, {
                    callback: function (doc) {
                        // Save the PDF
                        doc.save('sample-document.pdf');
                    },
                    x: 15,
                    y: 15,
                    width: 170, //target width in the PDF document
                    windowWidth: 650 //window width in CSS pixels
                });--%>
            }

            function Popup(data) {
                var myWindow = window.open('', 'my div', 'height=800,width=800');
                myWindow.document.write('<html><head><title>Slip Gaji</title>');
                /*optional stylesheet*/ //myWindow.document.write('<link rel="stylesheet" href="main.css" type="text/css" />');
                myWindow.document.write('</head><body >');
                myWindow.document.write(data);
                myWindow.document.write('</body></html>');
                myWindow.document.close(); // necessary for IE >= 10
                myWindow.focus();

                myWindow.onload = function () { // necessary if the div contain images

                    myWindow.focus(); // necessary for IE >= 10
                    myWindow.print();
                    myWindow.close();
                };
                myWindow.focus(); // necessary for IE >= 10
                myWindow.print();
                myWindow.close();
            }
            function CreatePDFfromHTML() {
                var HTML_Width = $('#invoice-box2').width(); //$('#<%'=letter.ClientID%>').width();
                var HTML_Height = $('#invoice-box2').height();//$('#<%'=letter.ClientID%>').height();
                var top_left_margin = 80;
                var PDF_Width = HTML_Width + (top_left_margin * 2);
                var PDF_Height = (PDF_Width * 1.5) + (top_left_margin * 2);
                var canvas_image_width = HTML_Width;
                var canvas_image_height = HTML_Height;

                var totalPDFPages = Math.ceil(HTML_Height / PDF_Height) - 1;
                html2canvas($('#invoice-box2')[0]).then(function (canvas) {
                //html2canvas($('#<%'=letter.ClientID%>')[0]).then(function (canvas) {
                    var imgData = canvas.toDataURL("image/jpeg", 1.0);
                    var pdf = new jsPDF('p', 'pt', [PDF_Width, PDF_Height]);
                    pdf.addImage(imgData, 'JPG', top_left_margin, top_left_margin, canvas_image_width, canvas_image_height);
                    for (var i = 1; i <= totalPDFPages; i++) {
                        pdf.addPage(PDF_Width, PDF_Height);
                        pdf.addImage(imgData, 'JPG', top_left_margin, -(PDF_Height * i) + (top_left_margin * 4), canvas_image_width, canvas_image_height);
                    }
                    pdf.save("SlipGaji.pdf");
                    //$('#<%=letter.ClientID%>').hide();
                });
            }
            function getInfoStaf(nostaf) {
                //Cara Pertama
                //alert(nostaf);

                    fetch('<%=ResolveClientUrl("~/FORMS/GAJI/Transaksi/Transaksi_WS.asmx/LoadRekodStaf")%>', {
                    method: 'POST',
                    headers: {
                        'Content-Type': "application/json"
                    },
                        //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
                        body: JSON.stringify({ nostaf: nostaf })
                    })
                    .then(response => response.json())
                    .then(data => setInfoStaf(data.d))

            }
            function setInfoStaf(data) {
                data = JSON.parse(data);
                if (data.MS01_NoStaf === "") {
                    alert("Tiada data");
                    return false;
                }

                //document.getElementById('pnostaf').innerHTML = 'NO. STAF : ' + data[0].MS01_NoStaf;
                //$('#lblNoStaf').html(data[0].MS01_NoStaf);
                $('#lblNama').html(data[0].MS01_Nama);
                $('#lbljabatan').html(data[0].PejabatS);
                $('#lblnokp').html(data[0].MS01_KpB);

                document.getElementById('lblNoStaf').innerHTML = ': ' + data[0].MS01_NoStaf;
                document.getElementById('lblNama').innerHTML = ': ' + data[0].MS01_Nama;
                document.getElementById('lbljabatan').innerHTML = ': ' + data[0].PejabatS;
                document.getElementById('lblnokp').innerHTML = ': ' + data[0].MS01_KpB;
              
                //document.getElementById('pnama').innerHTML = 'NAMA : ' + data[0].MS01_Nama;
                //document.getElementById('pnokp').innerHTML = 'NO. KP : ' + data[0].MS01_KpB;
                //document.getElementById('pjabatan').innerHTML = 'JABATAN : ' + data[0].PejabatS;
                //document.getElementById('pbank').innerHTML = 'BAYAR : BANK';
               // document.getElementById('ptarikh').innerHTML = 'TARIKH : ';


            }
            function getInfoGaji() {
                //Cara Pertama
                //alert(nostaf);
                var vBln = $('#<%=ddlMonths.ClientID%>').val();
                var vThn = $('#<%=ddlyear.ClientID%>').val();

                fetch('<%=ResolveClientUrl("~/FORMS/GAJI/Transaksi/Transaksi_WS.asmx/LoadTkhGaji")%>', {
                    method: 'POST',
                    headers: {
                        'Content-Type': "application/json"
                    },
                        //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
                    body: JSON.stringify({ bln: vBln, thn: vThn })
                    })
                                .then(response => response.json())
                    .then(data => setInfoGaji(data.d))

            }
            function setInfoGaji(data) {
                data = JSON.parse(data);
                if (data.tarikh_byr_gaji === "") {
                    alert("Tiada data");
                    return false;
                }

               // alert(data[0].tarikh_byr_gaji);
                //document.getElementById('ptarikh').innerHTML = 'TARIKH : ' + data[0].tarikh_byr_gaji;
                //$('#lbltarikh').html(data[0].tarikh_byr_gaji);
                document.getElementById('lbltarikh').innerHTML = ': ' + data[0].tarikh_byr_gaji;

            }
        </script>
</asp:Content>
