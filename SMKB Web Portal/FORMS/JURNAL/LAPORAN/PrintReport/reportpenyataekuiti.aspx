<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="reportpenyataekuiti.aspx.vb" Inherits="SMKB_Web_Portal.reportpenyataekuiti" %>

<!DOCTYPE html>
<html>
<head>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.html5.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.print.min.js" crossorigin="anonymous"></script>



    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

    <!-- Bootstrap JS -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <script src="https://printjs-4de6.kxcdn.com/print.min.js"></script>
    <title></title>

    <style>
        .align-right {
            text-align: right;
        }

        .subtotal-label {
            border-top: 2px solid #000;
            /*        display: block; */
            padding-top: 2px;
        }

        .subtotal-label2 {
            border-top: 2px solid #000;
            border-bottom: 2px solid #000;
            /*        display: block; */
            padding-top: 2px;
            padding-bottom: 2px;
        }

        .subtotal-label3 {
            border-top: 2px solid #000;
            border-bottom: 4px double #000;
            /*        display: block; 
*/ padding-top: 2px;
            padding-bottom: 2px;
        }

        table {
            border-collapse: separate;
        }

        .top-border {
            border-top: 1px solid black;
            min-width: 250px;
        }

        .topbottom-border {
            border-top: 1px solid black;
            border-bottom: 3px double black;
            min-width: 250px;
        }

        body {
            font-family: Arial, sans-serif;
            /*font-size: 12px!important;*/
        }

        .pheader {
            /*            text-align: center;
*/ font-size: 14px;
            font-weight: bold;
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }

        .pheader2 {
            /*            text-align: center;
*/ font-size: 14px;
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }

        .pheader3 {
            text-align: center;
            font-size: 14px;
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }



        .ptarikh {
            font-size: 12px;
            margin-top: 0px !important;
            margin-bottom: 0px !important;
        }

        table {
            width: 100%;
        }

        th, td {
            padding: 1px;
        }

        .headerkiri {
            text-align: center;
        }

        .valuekanan {
            text-align: right;
        }

        .valuetengah {
            text-align: center;
        }

        .bold {
            font-weight: bold;
        }

        #tableID_Senarai_trans tr {
        height: 30px; /* Adjust the height as needed */
    }

        @media print {
            body {
                margin: 20px;
                padding: 0;
            }

            .col-md-2 {
                float: right;
                /* margin-top: -50px; */
            }

            .print-div {
                break-after: page;
            }

                .print-div:last-child {
                    page-break-after: auto;
                }

            .printButton {
                display: none !important;
            }

            #printButton {
                display: block;
            }

            #header, #nav, .noprint {
                display: none;
            }

            .header, .header-space,
            .footer, .footer-space {
                height: 180px;
            }

            .header {
                position: fixed;
                top: 0;
            }

            .footer {
                position: fixed;
                bottom: 0;
            }

            .container {
                display: initial; /* or any other desired style */
            }

            table {
                font-size: 10px;
                table-layout: fixed;
                 width: 100%; 
            }
            th, td {
           width: 9%; /* Set a fixed width for each table cell (adjust as needed) */
           text-align: right;
            }
        }

        @page {
            size: landscape; /* Set the page orientation to landscape */
            margin: 0; /* adjust margins as needed */
        }

        .auto-style1 {
            width: 25%;
        }

        .auto-style2 {
            width: 48%;
        }
    </style>

    
</head>
<body>
    <form id="form1" runat="server">
        <div id="masterdiv" class="container">

            <div id="headerreport">
                <table style="width: 100%"> 
                    <thead >
                     <tr>
                        <td style="width: 40%;text-align:right; padding-right:20px">
                            <asp:Image ID="imgMyImage" runat="server" style="width:140px; Height:80px;text-align:right"/>
                        </td>
                        <td style="width: 30%;text-align:left">
                            <p class="pheader"><strong><asp:label ID="lblNamaKorporat" runat="server"></asp:label></strong></p>
                            <p class="pheader2" style="font-size:12px!important;text-transform: capitalize"><asp:label ID="lblAlamatKorporat" runat="server"></asp:label></p>
                            <p class="pheader2" style="font-size:12px!important"><asp:label ID="lblNoTelFaks" runat="server"></asp:label></p>
                            <p class="pheader2" style="font-size:12px!important"><asp:label ID="lblEmailKorporat" runat="server"></asp:label></p>

                        </td>
                        <td style="width: 30%; text-align: right">
                            <span class="ptarikh">Tarikh : <%= DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") %></span><br />
                            <span class="ptarikh">Laporan : CLS006</span><br />
                            <span class="ptarikh">Pengguna : <%= Session("ssusrID") %></span>

                        </td>
                    </tr>
                        </thead>
                    <tr>
                        <td></td>
                        <td style="text-align:center">
                            <br />
                            <p class="pheader3">Penyata Perubahan Aset Bersih/Ekuiti</p>
                            <p class="pheader3">Bagi Tahun Berakhir 31 Disember  <%= Session("tahun") %> <asp:label runat="server" id="tahun"></asp:label></p>
                            <p class="pheader3">Keseluruhan</p>
                        </td>
                         <td style="width: 25%"></td>

                    </tr>
                </table>
            </div> <%--close header report--%>
            <br />

            <div id="printreportcashflow" class="header-space">
                <table style="width: 100%" border="1">
                    <thead>
                      <tr>
                            <th scope="col" style="text-align: left"><b>Universiti</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Mengurus <br>(KW01)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Pembangunan <br>(KW02)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Penyelidikan <br>(KW03)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Inisiatif Pembiayaan Swasta (PFI) <br>(KW04)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Pembiayaan Kenderaan <br>(KW05)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Pembiayaan Komputer <br>(KW06)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Pendapatan <br>(KW07)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Wang Amanah <br>(KW08)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Peruntukan Khas <br>(KW09)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Perundingan <br>(KW10)</b></th>
                            <th scope="col" style="text-align: center"><b>Kumpulan Wang Pembiayaan Peralatan Sukan dan Rekreasi <br>(KW11)</b></th>
                            <th scope="col" style="text-align: center"><b>Jumlah</b></th>

                        </tr>
                    </thead>
                    <tbody id="tableID_Senarai_trans">


                        <tr>
                            <th scope="col" style="text-align: center"><b><%= Session("tahun") %></b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                            <th scope="col" style="text-align: center"><b>RM</b></th>
                        </tr>

                        <tr>
                            <td><span id="butiran1"></span></td>
                            <td class="rightAlign"><span id="r1"></span></td>
                            <td><span id="r2"></span></td>
                            <td><span id="r3"></span></td>
                            <td><span id="r4"></span></td>
                            <td><span id="r5"></span></td>
                            <td><span id="r6"></span></td>
                            <td><span id="r7"></span></td>
                            <td><span id="r8"></span></td>
                            <td><span id="r9"></span></td>
                            <td><span id="r10"></span></td>
                            <td><span id="r11"></span></td>
                            <td><span id="total1"></span></td>
                        </tr>

                         <tr>
                            <td><span id="butiran2"></span></td>
                            <td><span id="a1"></span></td>
                            <td><span id="a2"></span></td>
                            <td><span id="a3"></span></td>
                            <td><span id="a4"></span></td>
                            <td><span id="a5"></span></td>
                            <td><span id="a6"></span></td>
                            <td><span id="a7"></span></td>
                            <td><span id="a8"></span></td>
                            <td><span id="a9"></span></td>
                            <td><span id="a10"></span></td>
                            <td><span id="a11"></span></td>
                            <td><span id="total2"></span></td>
                        </tr>

                        <tr>
                           <td><span id="butiran3"></span></td>
                           <td><span id="b1"></span></td>
                           <td><span id="b2"></span></td>
                           <td><span id="b3"></span></td>
                           <td><span id="b4"></span></td>
                           <td><span id="b5"></span></td>
                           <td><span id="b6"></span></td>
                           <td><span id="b7"></span></td>
                           <td><span id="b8"></span></td>
                           <td><span id="b9"></span></td>
                           <td><span id="b10"></span></td>
                           <td><span id="b11"></span></td>
                           <td><span id="total3"></span></td>
                       </tr>

                        <tr>
                           <td><span id="butiran4"></span></td>
                           <td><span id="c1"></span></td>
                           <td><span id="c2"></span></td>
                           <td><span id="c3"></span></td>
                           <td><span id="c4"></span></td>
                           <td><span id="c5"></span></td>
                           <td><span id="c6"></span></td>
                           <td><span id="c7"></span></td>
                           <td><span id="c8"></span></td>
                           <td><span id="c9"></span></td>
                           <td><span id="c10"></span></td>
                           <td><span id="c11"></span></td>
                           <td><span id="total4"></span></td>
                       </tr>


                        <tr>
                           <td></td>
                           <td><span id="d1"></span></td>
                           <td><span id="d2"></span></td>
                           <td><span id="d3"></span></td>
                           <td><span id="d4"></span></td>
                           <td><span id="d5"></span></td>
                           <td><span id="d6"></span></td>
                           <td><span id="d7"></span></td>
                           <td><span id="d8"></span></td>
                           <td><span id="d9"></span></td>
                           <td><span id="d10"></span></td>
                           <td><span id="d11"></span></td>
                           <td><span id="total5"></span></td>
                       </tr>

                         <tr>
                           <td><span id="butiran6"></span></td>
                           <td><span id="e1"></span></td>
                           <td><span id="e2"></span></td>
                           <td><span id="e3"></span></td>
                           <td><span id="e4"></span></td>
                           <td><span id="e5"></span></td>
                           <td><span id="e6"></span></td>
                           <td><span id="e7"></span></td>
                           <td><span id="e8"></span></td>
                           <td><span id="e9"></span></td>
                           <td><span id="e10"></span></td>
                           <td><span id="e11"></span></td>
                           <td><span id="total6"></span></td>
                       </tr> 

                       <tr>
                          <td></td>
                          <td><span id="f1"></span></td>
                          <td><span id="f2"></span></td>
                          <td><span id="f3"></span></td>
                          <td><span id="f4"></span></td>
                          <td><span id="f5"></span></td>
                          <td><span id="f6"></span></td>
                          <td><span id="f7"></span></td>
                          <td><span id="f8"></span></td>
                          <td><span id="f9"></span></td>
                          <td><span id="f10"></span></td>
                          <td><span id="f11"></span></td>
                          <td><span id="total7"></span></td>
                      </tr>

                      <tr>
                           <td><span id="butiran8"></span></td>
                           <td><span id="g1"></span></td>
                           <td><span id="g2"></span></td>
                           <td><span id="g3"></span></td>
                           <td><span id="g4"></span></td>
                           <td><span id="g5"></span></td>
                           <td><span id="g6"></span></td>
                           <td><span id="g7"></span></td>
                           <td><span id="g8"></span></td>
                           <td><span id="g9"></span></td>
                           <td><span id="g10"></span></td>
                           <td><span id="g11"></span></td>
                           <td><span id="total8"></span></td>
                       </tr> 


                      <tr>
                          <td><span id="butiran9">Baki pada 31 Disember <%= Session("tahun") %> </span></td>
                          <td><span id="h1"></span></td>
                          <td><span id="h2"></span></td>
                          <td><span id="h3"></span></td>
                          <td><span id="h4"></span></td>
                          <td><span id="h5"></span></td>
                          <td><span id="h6"></span></td>
                          <td><span id="h7"></span></td>
                          <td><span id="h8"></span></td>
                          <td><span id="h9"></span></td>
                          <td><span id="h10"></span></td>
                          <td><span id="h11"></span></td>
                          <td><span id="total9"></span></td>
                      </tr> 


                    </tbody>
                </table>
               <br /><br /><br /><br />
             <div style="text-align:center">
                <span><strong>*** Laporan Tamat ***</strong></span>
            </div>
            </div>


            <script type="text/javascript">
                var tbl = null;
                var bulan = '';
                var thn = '';
                      
                $(document).ready(function () {
                    getData();
                });

                function formatNumber(number) {
                    return new Intl.NumberFormat('en-US', {
                        style: 'decimal',
                        minimumFractionDigits: 2,
                        maximumFractionDigits: 2
                    }).format(number);
                }

                function getData() {                
                   
                    fetch('<%= ResolveUrl("~/FORMS/JURNAL/LAPORAN/LejarPenghutangWS.asmx/Get_PenyataEkuiti") %>', {                   
                        method: 'POST',
                        headers: {
                            'Content-Type': "application/json"
                        },
                        body: JSON.stringify()
                    })
                        .then(response => response.json())
                        .then(data => setData(data.d))
                }

                function setData(data) {
                    data = JSON.parse(data);
                    console.log(data)



                    var total1 = 0;
                    var total1 = parseFloat(data[0].Total_Kod_Kw_1) + parseFloat(data[0].Total_Kod_Kw_2) + parseFloat(data[0].Total_Kod_Kw_3) + parseFloat(data[0].Total_Kod_Kw_4) +
                                 parseFloat(data[0].Total_Kod_Kw_5) + parseFloat(data[0].Total_Kod_Kw_6) + parseFloat(data[0].Total_Kod_Kw_7) + parseFloat(data[0].Total_Kod_Kw_8) +
                                 parseFloat(data[0].Total_Kod_Kw_9) + parseFloat(data[0].Total_Kod_Kw_10) + parseFloat(data[0].Total_Kod_Kw_11);

                    document.getElementById("butiran1").innerHTML = data[0].vot_name; 
                    document.getElementById("r1").innerHTML = formatNumber(data[0].Total_Kod_Kw_1);
                    document.getElementById("r2").innerHTML = formatNumber(data[0].Total_Kod_Kw_2); 
                    document.getElementById("r3").innerHTML = formatNumber(data[0].Total_Kod_Kw_3); 
                    document.getElementById("r4").innerHTML = formatNumber(data[0].Total_Kod_Kw_4); 
                    document.getElementById("r5").innerHTML = formatNumber(data[0].Total_Kod_Kw_5); 
                    document.getElementById("r6").innerHTML = formatNumber(data[0].Total_Kod_Kw_6); 
                    document.getElementById("r7").innerHTML = formatNumber(data[0].Total_Kod_Kw_7); 
                    document.getElementById("r8").innerHTML = formatNumber(data[0].Total_Kod_Kw_8); 
                    document.getElementById("r9").innerHTML = formatNumber(data[0].Total_Kod_Kw_9); 
                    document.getElementById("r10").innerHTML = formatNumber(data[0].Total_Kod_Kw_10); 
                    document.getElementById("r11").innerHTML = formatNumber(data[0].Total_Kod_Kw_11); 
                    document.getElementById("total1").innerHTML = formatNumber(total1); 


                    var total2 = 0;
                    var total2 = parseFloat(data[1].Total_Kod_Kw_1) + parseFloat(data[1].Total_Kod_Kw_2) + parseFloat(data[1].Total_Kod_Kw_3) + parseFloat(data[1].Total_Kod_Kw_4) +
                        parseFloat(data[1].Total_Kod_Kw_5) + parseFloat(data[1].Total_Kod_Kw_6) + parseFloat(data[1].Total_Kod_Kw_7) + parseFloat(data[1].Total_Kod_Kw_8) +
                        parseFloat(data[1].Total_Kod_Kw_9) + parseFloat(data[1].Total_Kod_Kw_10) + parseFloat(data[1].Total_Kod_Kw_11);

                    document.getElementById("butiran2").innerHTML = data[1].vot_name;
                    document.getElementById("a1").innerHTML = formatNumber(data[1].Total_Kod_Kw_1);
                    document.getElementById("a2").innerHTML = formatNumber(data[1].Total_Kod_Kw_2);
                    document.getElementById("a3").innerHTML = formatNumber(data[1].Total_Kod_Kw_3);
                    document.getElementById("a4").innerHTML = formatNumber(data[1].Total_Kod_Kw_4);
                    document.getElementById("a5").innerHTML = formatNumber(data[1].Total_Kod_Kw_5);
                    document.getElementById("a6").innerHTML = formatNumber(data[1].Total_Kod_Kw_6);
                    document.getElementById("a7").innerHTML = formatNumber(data[1].Total_Kod_Kw_7);
                    document.getElementById("a8").innerHTML = formatNumber(data[1].Total_Kod_Kw_8);
                    document.getElementById("a9").innerHTML = formatNumber(data[1].Total_Kod_Kw_9);
                    document.getElementById("a10").innerHTML = formatNumber(data[1].Total_Kod_Kw_10);
                    document.getElementById("a11").innerHTML = formatNumber(data[1].Total_Kod_Kw_11);
                    document.getElementById("total2").innerHTML = formatNumber(total2);


                    var total3 = 0;
                    var total3 = parseFloat(data[2].Total_Kod_Kw_1) + parseFloat(data[2].Total_Kod_Kw_2) + parseFloat(data[2].Total_Kod_Kw_3) + parseFloat(data[2].Total_Kod_Kw_4) +
                        parseFloat(data[2].Total_Kod_Kw_5) + parseFloat(data[2].Total_Kod_Kw_6) + parseFloat(data[2].Total_Kod_Kw_7) + parseFloat(data[2].Total_Kod_Kw_8) +
                        parseFloat(data[2].Total_Kod_Kw_9) + parseFloat(data[2].Total_Kod_Kw_10) + parseFloat(data[2].Total_Kod_Kw_11);

                    document.getElementById("butiran3").innerHTML = data[2].vot_name;
                    document.getElementById("b1").innerHTML = formatNumber(data[2].Total_Kod_Kw_1);
                    document.getElementById("b2").innerHTML = formatNumber(data[2].Total_Kod_Kw_2);
                    document.getElementById("b3").innerHTML = formatNumber(data[2].Total_Kod_Kw_3);
                    document.getElementById("b4").innerHTML = formatNumber(data[2].Total_Kod_Kw_4);
                    document.getElementById("b5").innerHTML = formatNumber(data[2].Total_Kod_Kw_5);
                    document.getElementById("b6").innerHTML = formatNumber(data[2].Total_Kod_Kw_6);
                    document.getElementById("b7").innerHTML = formatNumber(data[2].Total_Kod_Kw_7);
                    document.getElementById("b8").innerHTML = formatNumber(data[2].Total_Kod_Kw_8);
                    document.getElementById("b9").innerHTML = formatNumber(data[2].Total_Kod_Kw_9);
                    document.getElementById("b10").innerHTML = formatNumber(data[2].Total_Kod_Kw_10);
                    document.getElementById("b11").innerHTML = formatNumber(data[2].Total_Kod_Kw_11);
                    document.getElementById("total3").innerHTML = formatNumber(total3);


                    var total4 = 0;
                    var total4 = parseFloat(data[3].Total_Kod_Kw_1) + parseFloat(data[3].Total_Kod_Kw_2) + parseFloat(data[3].Total_Kod_Kw_3) + parseFloat(data[3].Total_Kod_Kw_4) +
                        parseFloat(data[3].Total_Kod_Kw_5) + parseFloat(data[3].Total_Kod_Kw_6) + parseFloat(data[3].Total_Kod_Kw_7) + parseFloat(data[3].Total_Kod_Kw_8) +
                        parseFloat(data[3].Total_Kod_Kw_9) + parseFloat(data[3].Total_Kod_Kw_10) + parseFloat(data[3].Total_Kod_Kw_11);

                    document.getElementById("butiran4").innerHTML = data[3].vot_name;
                    document.getElementById("c1").innerHTML = formatNumber(data[3].Total_Kod_Kw_1);
                    document.getElementById("c2").innerHTML = formatNumber(data[3].Total_Kod_Kw_2);
                    document.getElementById("c3").innerHTML = formatNumber(data[3].Total_Kod_Kw_3);
                    document.getElementById("c4").innerHTML = formatNumber(data[3].Total_Kod_Kw_4);
                    document.getElementById("c5").innerHTML = formatNumber(data[3].Total_Kod_Kw_5);
                    document.getElementById("c6").innerHTML = formatNumber(data[3].Total_Kod_Kw_6);
                    document.getElementById("c7").innerHTML = formatNumber(data[3].Total_Kod_Kw_7);
                    document.getElementById("c8").innerHTML = formatNumber(data[3].Total_Kod_Kw_8);
                    document.getElementById("c9").innerHTML = formatNumber(data[3].Total_Kod_Kw_9);
                    document.getElementById("c10").innerHTML = formatNumber(data[3].Total_Kod_Kw_10);
                    document.getElementById("c11").innerHTML = formatNumber(data[3].Total_Kod_Kw_11);
                    document.getElementById("total4").innerHTML = formatNumber(total4);


                    var total5a = parseFloat(data[0].Total_Kod_Kw_1) + parseFloat(data[1].Total_Kod_Kw_1) + parseFloat(data[2].Total_Kod_Kw_1) + parseFloat(data[3].Total_Kod_Kw_1) 
                    var total5b = parseFloat(data[0].Total_Kod_Kw_2) + parseFloat(data[1].Total_Kod_Kw_2) + parseFloat(data[2].Total_Kod_Kw_2) + parseFloat(data[3].Total_Kod_Kw_2) 
                    var total5c = parseFloat(data[0].Total_Kod_Kw_3) + parseFloat(data[1].Total_Kod_Kw_3) + parseFloat(data[2].Total_Kod_Kw_3) + parseFloat(data[3].Total_Kod_Kw_3) 
                    var total5d = parseFloat(data[0].Total_Kod_Kw_4) + parseFloat(data[1].Total_Kod_Kw_4) + parseFloat(data[2].Total_Kod_Kw_4) + parseFloat(data[3].Total_Kod_Kw_4) 
                    var total5e = parseFloat(data[0].Total_Kod_Kw_5) + parseFloat(data[1].Total_Kod_Kw_5) + parseFloat(data[2].Total_Kod_Kw_5) + parseFloat(data[3].Total_Kod_Kw_5)
                    var total5f = parseFloat(data[0].Total_Kod_Kw_6) + parseFloat(data[1].Total_Kod_Kw_6) + parseFloat(data[2].Total_Kod_Kw_6) + parseFloat(data[3].Total_Kod_Kw_6)
                    var total5g = parseFloat(data[0].Total_Kod_Kw_7) + parseFloat(data[1].Total_Kod_Kw_7) + parseFloat(data[2].Total_Kod_Kw_7) + parseFloat(data[3].Total_Kod_Kw_7)
                    var total5h = parseFloat(data[0].Total_Kod_Kw_8) + parseFloat(data[1].Total_Kod_Kw_8) + parseFloat(data[2].Total_Kod_Kw_8) + parseFloat(data[3].Total_Kod_Kw_8)
                    var total5i = parseFloat(data[0].Total_Kod_Kw_9) + parseFloat(data[1].Total_Kod_Kw_9) + parseFloat(data[2].Total_Kod_Kw_9) + parseFloat(data[3].Total_Kod_Kw_9)
                    var total5j = parseFloat(data[0].Total_Kod_Kw_10) + parseFloat(data[1].Total_Kod_Kw_10) + parseFloat(data[2].Total_Kod_Kw_10) + parseFloat(data[3].Total_Kod_Kw_10) 
                    var total5k = parseFloat(data[0].Total_Kod_Kw_11) + parseFloat(data[1].Total_Kod_Kw_11) + parseFloat(data[2].Total_Kod_Kw_11) + parseFloat(data[3].Total_Kod_Kw_11) 
                    var total5 = total5a + total5b + total5c + total5d + total5e + total5f + total5g + total5h + total5i + total5j + total5j + total5k;


                    document.getElementById("d1").innerHTML = formatNumber(total5a);
                    document.getElementById("d2").innerHTML = formatNumber(total5b);
                    document.getElementById("d3").innerHTML = formatNumber(total5c);
                    document.getElementById("d4").innerHTML = formatNumber(total5d);
                    document.getElementById("d5").innerHTML = formatNumber(total5e);
                    document.getElementById("d6").innerHTML = formatNumber(total5f);
                    document.getElementById("d7").innerHTML = formatNumber(total5g);
                    document.getElementById("d8").innerHTML = formatNumber(total5h);
                    document.getElementById("d9").innerHTML = formatNumber(total5i);
                    document.getElementById("d10").innerHTML = formatNumber(total5j);
                    document.getElementById("d11").innerHTML = formatNumber(total5k);
                    document.getElementById("total5").innerHTML = formatNumber(total5);



                    var total6 = parseFloat(data[4].Total_Kod_Kw_1) + parseFloat(data[4].Total_Kod_Kw_2) + parseFloat(data[4].Total_Kod_Kw_3) + parseFloat(data[4].Total_Kod_Kw_4) +
                        parseFloat(data[4].Total_Kod_Kw_5) + parseFloat(data[4].Total_Kod_Kw_6) + parseFloat(data[4].Total_Kod_Kw_7) + parseFloat(data[4].Total_Kod_Kw_8) +
                        parseFloat(data[4].Total_Kod_Kw_9) + parseFloat(data[4].Total_Kod_Kw_10) + parseFloat(data[4].Total_Kod_Kw_11);

                    document.getElementById("butiran6").innerHTML = data[4].vot_name;
                    document.getElementById("e1").innerHTML = formatNumber(data[4].Total_Kod_Kw_1);
                    document.getElementById("e2").innerHTML = formatNumber(data[4].Total_Kod_Kw_2);
                    document.getElementById("e3").innerHTML = formatNumber(data[4].Total_Kod_Kw_3);
                    document.getElementById("e4").innerHTML = formatNumber(data[4].Total_Kod_Kw_4);
                    document.getElementById("e5").innerHTML = formatNumber(data[4].Total_Kod_Kw_5);
                    document.getElementById("e6").innerHTML = formatNumber(data[4].Total_Kod_Kw_6);
                    document.getElementById("e7").innerHTML = formatNumber(data[4].Total_Kod_Kw_7);
                    document.getElementById("e8").innerHTML = formatNumber(data[4].Total_Kod_Kw_8);
                    document.getElementById("e9").innerHTML = formatNumber(data[4].Total_Kod_Kw_9);
                    document.getElementById("e10").innerHTML = formatNumber(data[4].Total_Kod_Kw_10);
                    document.getElementById("e11").innerHTML = formatNumber(data[4].Total_Kod_Kw_11);
                    document.getElementById("total6").innerHTML = formatNumber(total6);



                    var total7a = total5a - parseFloat(data[4].Total_Kod_Kw_1);
                    var total7b = total5b - parseFloat(data[4].Total_Kod_Kw_2);
                    var total7c = total5c - parseFloat(data[4].Total_Kod_Kw_3);
                    var total7d = total5d - parseFloat(data[4].Total_Kod_Kw_4);
                    var total7e = total5e - parseFloat(data[4].Total_Kod_Kw_5);
                    var total7f = total5f - parseFloat(data[4].Total_Kod_Kw_6);
                    var total7g = total5g - parseFloat(data[4].Total_Kod_Kw_7);
                    var total7h = total5h - parseFloat(data[4].Total_Kod_Kw_8);
                    var total7i = total5i - parseFloat(data[4].Total_Kod_Kw_9);
                    var total7j = total5j - parseFloat(data[4].Total_Kod_Kw_10);
                    var total7k = total5k - parseFloat(data[4].Total_Kod_Kw_11);
                    var total7 = total7a + total7b + total7c + total7d + total7e + total7f + total7g + total7h + total7i + total7j + total7j + total7k;


                    document.getElementById("f1").innerHTML = formatNumber(total7a);
                    document.getElementById("f2").innerHTML = formatNumber(total7b);
                    document.getElementById("f3").innerHTML = formatNumber(total7c);
                    document.getElementById("f4").innerHTML = formatNumber(total7d);
                    document.getElementById("f5").innerHTML = formatNumber(total7e);
                    document.getElementById("f6").innerHTML = formatNumber(total7f);
                    document.getElementById("f7").innerHTML = formatNumber(total7g);
                    document.getElementById("f8").innerHTML = formatNumber(total7h);
                    document.getElementById("f9").innerHTML = formatNumber(total7i);
                    document.getElementById("f10").innerHTML = formatNumber(total7j);
                    document.getElementById("f11").innerHTML = formatNumber(total7k);
                    document.getElementById("total7").innerHTML = formatNumber(total7);



                    var total8 = parseFloat(data[5].Total_Kod_Kw_1) + parseFloat(data[5].Total_Kod_Kw_2) + parseFloat(data[5].Total_Kod_Kw_3) + parseFloat(data[5].Total_Kod_Kw_4) +
                        parseFloat(data[5].Total_Kod_Kw_5) + parseFloat(data[5].Total_Kod_Kw_6) + parseFloat(data[5].Total_Kod_Kw_7) + parseFloat(data[5].Total_Kod_Kw_8) +
                        parseFloat(data[5].Total_Kod_Kw_9) + parseFloat(data[5].Total_Kod_Kw_10) + parseFloat(data[5].Total_Kod_Kw_11);

                    document.getElementById("butiran8").innerHTML = data[5].vot_name;
                    document.getElementById("g1").innerHTML = formatNumber(data[5].Total_Kod_Kw_1);
                    document.getElementById("g2").innerHTML = formatNumber(data[5].Total_Kod_Kw_2);
                    document.getElementById("g3").innerHTML = formatNumber(data[5].Total_Kod_Kw_3);
                    document.getElementById("g4").innerHTML = formatNumber(data[5].Total_Kod_Kw_4);
                    document.getElementById("g5").innerHTML = formatNumber(data[5].Total_Kod_Kw_5);
                    document.getElementById("g6").innerHTML = formatNumber(data[5].Total_Kod_Kw_6);
                    document.getElementById("g7").innerHTML = formatNumber(data[5].Total_Kod_Kw_7);
                    document.getElementById("g8").innerHTML = formatNumber(data[5].Total_Kod_Kw_8);
                    document.getElementById("g9").innerHTML = formatNumber(data[5].Total_Kod_Kw_9);
                    document.getElementById("g10").innerHTML = formatNumber(data[5].Total_Kod_Kw_10);
                    document.getElementById("g11").innerHTML = formatNumber(data[5].Total_Kod_Kw_11);
                    document.getElementById("total8").innerHTML = formatNumber(total8);


                    var total9a = total7a - parseFloat(data[5].Total_Kod_Kw_1);
                    var total9b = total7b - parseFloat(data[5].Total_Kod_Kw_2);
                    var total9c = total7c - parseFloat(data[5].Total_Kod_Kw_3);
                    var total9d = total7d - parseFloat(data[5].Total_Kod_Kw_4);
                    var total9e = total7e - parseFloat(data[5].Total_Kod_Kw_5);
                    var total9f = total7f - parseFloat(data[5].Total_Kod_Kw_6);
                    var total9g = total7g - parseFloat(data[5].Total_Kod_Kw_7);
                    var total9h = total7h - parseFloat(data[5].Total_Kod_Kw_8);
                    var total9i = total7i - parseFloat(data[5].Total_Kod_Kw_9);
                    var total9j = total7j - parseFloat(data[5].Total_Kod_Kw_10);
                    var total9k = total7k - parseFloat(data[5].Total_Kod_Kw_11);
                    var total9 = total9a + total9b + total9c + total9d + total9e + total9f + total9g + total9h + total9i + total9j + total9j + total9k;

                    document.getElementById("h1").innerHTML = formatNumber(total9a);
                    document.getElementById("h2").innerHTML = formatNumber(total9b);
                    document.getElementById("h3").innerHTML = formatNumber(total9c);
                    document.getElementById("h4").innerHTML = formatNumber(total9d);
                    document.getElementById("h5").innerHTML = formatNumber(total9e);
                    document.getElementById("h6").innerHTML = formatNumber(total9f);
                    document.getElementById("h7").innerHTML = formatNumber(total9g);
                    document.getElementById("h8").innerHTML = formatNumber(total9h);
                    document.getElementById("h9").innerHTML = formatNumber(total9i);
                    document.getElementById("h10").innerHTML = formatNumber(total9j);
                    document.getElementById("h11").innerHTML = formatNumber(total9k);
                    document.getElementById("total9").innerHTML = formatNumber(total9);


                    callPrint();

                }
                         
                function callPrint() {
                if (window.addEventListener) {
                    //getData();
                    window.print();
                } else {
                    window.print();


                }
                }
            </script>
        </div> <%--close master div--%>

    </form>
</body>
</html>
