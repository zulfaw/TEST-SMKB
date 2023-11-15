<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="reportPenyataAliranTunai.aspx.vb" Inherits="SMKB_Web_Portal.reportPenyataAliranTunai" %>

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
        .align-right{
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
*/      padding-top: 2px; 
        padding-bottom: 2px; 
        }

        table {
            border-collapse:separate;
        }
        .top-border {
            border-top: 1px solid black;
            min-width: 250px;
        }

        .topbottom-border{
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
*/            font-size: 14px;
            font-weight: bold;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }

        .pheader2 {
/*            text-align: center;
*/            font-size: 14px;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }

        .pheader3 {
            text-align: center;
            font-size: 14px;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }



        .ptarikh {
            font-size: 12px;
            margin-top:0px!important;
            margin-bottom:0px!important;

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
            text-align:center;
        }

        .bold {
            font-weight: bold;
        }


        @media print {
            .col-md-2 {
                float: right;
                /* margin-top: -50px; */
            }

            .print-div {
                break-after:page;
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

        }

        @page {
        size: A4; /* or letter, legal, etc. */
        margin: 1cm; /* adjust margins as needed */
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

            <div id="headerreport" class="header">
                <table style="width: 100%"> 
                    <thead >
                     <tr>
                        <td style="width: 20%;text-align:right">
                            <asp:Image ID="imgMyImage" runat="server" style="width:140px; Height:80px;text-align:right"/>
                        </td>
                        <td style="width: 50%;text-align:left">
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
                            <p class="pheader3">Penyata Aliran Tunai</p>
                            <p class="pheader3">Bagi Tahun Berakhir 31 Disember 2022 <asp:label runat="server" id="tahun"></asp:label></p>
                            <p class="pheader3">Keseluruhan</p>
                        </td>
                         <td style="width: 25%"></td>

                    </tr>
                </table>
            </div> <%--close header report--%>
            <br />

            <div id="printreportcashflow">
                <table style="width: 100%">
                    <thead class="header-space">
                    </thead>
                    <tbody id="tableID_Senarai_trans">
                        <tr>
                            <th scope="col" style="text-align: right"></th>
                            <th scope="col" style="text-align: right"><b>RM</b></th>
                        </tr>
                        <tr>
                            <th scope="col" class="auto-style3"><b>ALIRAN TUNAI DARI AKTIVITI OPERASI</b></th>
                            <th scope="col" class="auto-style3"></th>
                        </tr>
                        <tr>
                            <td scope="col">Lebihan/(Kurangan) Pendapatan </td>
                            <td scope="col" id="row1" class="align-right">
                                <label id="lblLebihan"  name="lblLebihan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pelarasan untuk :</td>
                            <td scope="col" id="row2"></td>
                        </tr>
                        <tr>
                            <td scope="col">Pelunasan Geran</td>
                            <td scope="col" id="row3" class="align-right">
                                <label id="lblLunasGeran"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Faedah, Keuntungan dan Dividen Diterima</td>
                            <td scope="col" id="row4" class="align-right">
                                <label id="lblFaedahDiterima"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Manfaat Pekerja</td>
                            <td scope="col" id="row5" class="align-right">
                                <label id="lblManfaatP"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Susut Nilai</td>
                            <td scope="col" id="row6" class="align-right">
                                <label id="lblSusutNilai"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Belanja Pelunasan</td>
                            <td scope="col" id="row7" class="align-right">
                                <label id="lblBPelunasan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Belanja Hutang Ragu</td>
                            <td scope="col" id="row8" class="align-right">
                                <label id="lblBHutangRagu"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Faedah Pajakan</td>
                            <td scope="col" id="row9" class="align-right">
                                <label id="lblFaedahPajakan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pelarasan Pengkelasan Akaun</td>
                            <td scope="col" id="row10" class="align-right">
                                <label id="lblPPAkaun"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pelarasan Hartanah, Loji dan Peralatan</td>
                            <td scope="col" id="row11" class="align-right">
                                <label id="lblPHartanah"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Keuntungan atas Pelupusan Hartanah, Loji dan Peralatan</td>
                            <td scope="col" id="row12" class="align-right">
                                <label id="lblKeuntunganAtasPelupusan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Kerugian atas Pelupusan Hartanah, Loji dan Peralatan</td>
                            <td scope="col" id="row13" class="align-right">
                                <label id="lblKerugianAtasPelupusan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Lebihan/(Kurangan) Operasi sebelum perubahan Modal Kerja</td>
                            <td scope="col" id="row14" class="align-right">
                                <label id="sum1" class="subtotal-label"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pengurangan/(pertambahan) dalam Akaun Belum Terima</td>
                            <td scope="col" id="row15" class="align-right">
                                <label id="lblPPAkaunBelumTerima"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pengurangan/(pertambahan) dalam Urus Niaga Bukan Pertukaran Belum Terima</td>
                            <td scope="col" id="row16" class="align-right">
                                <label id="lblPPPertukaranBelumTerima"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">(Pertambahan)/pengurangan dalam Pendahuluan, Deposit dan Prabayar</td>
                            <td scope="col" id="row17" class="align-right">
                                <label id="lblPPPendahuluan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pengurangan dalam Pendapatan Terakru</td>
                            <td scope="col" id="row18" class="align-right">
                                <label id="lblPPendapatanTerakru"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pengurangan/(pertambahan) dalam Akaun Belum Bayar</td>
                            <td scope="col" id="row19" class="align-right">
                                <label id="lblPPAkaunBelumBayar"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Tunai (untuk)/daripada Aktiviti Operasi</td>
                            <td scope="col" id="row20" class="align-right">
                                <label id="sum2" class="subtotal-label"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Manfaat Pekerja Dibayar</td>
                            <td scope="col" id="row21" class="align-right">
                                <label id="lblManfaatPekerjaDibayar"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Tunai Bersih (untuk)/daripada Aktiviti Operasi</td>
                            <td scope="col" id="row22" class="align-right">
                                <label id="sum3" class="subtotal-label2"></label>
                            </td>
                        </tr>
                        <tr style="margin-bottom: 80px;">
                            <td scope="col"></td>
                            <td scope="col" id="row24"></td>
                        </tr>
                        <tr>
                            <td scope="col"><div style="margin-top: 30px;"><b>ALIRAN TUNAI DARI AKTIVITI PELABURAN</b></div></td>
                            <td scope="col" id="row25"></td>
                        </tr>
                        <tr>
                            <td scope="col">Faedah dan Keuntungan Diterima</td>
                            <td scope="col" id="row26" class="align-right">
                                <label id="lblFKeuntunganDiterima"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Dividen Diterima</td>
                            <td scope="col" id="row27" class="align-right">
                                <label id="lblDividenDiterima"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pelaburan Lain</td>
                            <td scope="col" id="row28" class="align-right">
                                <label id="lblPelaburanLain"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Penerimaan Pinjaman Kenderaan/Komputer/Peralatan</td>
                            <td scope="col" id="row30" class="align-right">
                                <label id="lblPPinjamanKenderaan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Bayaran Pinjaman Kenderaan/Komputer/Peralatan</td>
                            <td scope="col" id="row31" class="align-right">
                                <label id="lblBPKenderaan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pembelian Hartanah, Loji dan Peralatan</td>
                            <td scope="col" id="row32" class="align-right">
                                <label id="lblPHartanahLoji"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pembelian Aset Tak Ketara</td>
                            <td scope="col" id="row33" class="align-right">
                                <label id="lblPembelianATK"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Terimaan Hasil Pelupusan Hartanah, Loji dan Peralatan</td>
                            <td scope="col" id="row34" class="align-right">
                                <label id="lblTerimaanHPH"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Pembayaran Pembinaan dalam Kemajuan</td>
                            <td scope="col" id="row35" class="align-right">
                                <label id="lblPPembayaranPDK"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Tunai Bersih untuk Aktiviti Pelaburan</td>
                            <td scope="col" id="row36" class="align-right">
                                <label id="sum4" class="subtotal-label2"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col"></td>
                            <td scope="col" id="row37"></td>
                        </tr>
                        <tr>
                            <td scope="col"><div style="margin-top: 30px;"><b>ALIRAN TUNAI DARI AKTIVITI PEMBIAYAAN</b></div></td>
                            <td scope="col" id="row38"></td>
                        </tr>
                        <tr>
                            <td scope="col">Pembayaran Pajakan Kewangan</td>
                            <td scope="col" id="row39" class="align-right">
                                <label id="lblPPajakanKewangan"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Penerimaan Pelbagai Geran Khusus</td>
                            <td scope="col" id="row42" class="align-right">
                                <label id="lblPPelbagaiGK"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Tunai Bersih daripada Aktiviti Pembiayaan</td>
                            <td scope="col" id="row43" class="align-right">
                                <label id="sum5" class="subtotal-label2"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col"></td>
                            <td scope="col" id="row44"></td>
                        </tr>
                        <tr>
                            <td scope="col">Penambahan/(Pengurangan) Bersih Tunai dan Kesetaraan Tunai</td>
                            <td scope="col" id="row45" class="align-right">
                                <label id="lblSum3Sum4Sum5"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Tunai dan Kesetaraan Tunai Pada Awal Tahun</td>
                            <td scope="col" id="row46" class="align-right">
                                <label id="lblTDKAwalTahun"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Tunai dan Kesetaraan Tunai Pada Akhir Tahun</td>
                            <td scope="col" id="row47" class="align-right">
                                <label id="sum6" class="subtotal-label3"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col"></td>
                            <td scope="col" id="row48"></td>
                        </tr>
                        <tr>
                            <td scope="col">Tunai dan Kesetaraan Tunai termasuk :</td>
                            <td scope="col" id="row49"></td>
                        </tr>
                        <tr>
                            <td scope="col">Simpanan Tetap</td>
                            <td scope="col" id="row50" class="align-right">
                                <label id="lblSimpananTetap"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Tunai di Tangan dan di Bank</td>
                            <td scope="col" id="row51" class="align-right">
                                <label id="lblTunaiTanganBank"></label>
                            </td>
                        </tr>
                        <tr>
                            <td scope="col">Jumlah</td>
                            <td scope="col" id="row52" class="align-right">
                                <label id="sum7" class="subtotal-label3"></label>
                            </td>
                        </tr>
                    </tbody>
                </table>
               <br /><br /><br />
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

                    fetch('<%= ResolveUrl("~/FORMS/JURNAL/LAPORAN/LejarPenghutangWS.asmx/LoadData") %>', {
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

                    var mPekerja = 0.00;
                    var sum1 = data[0].LebihanPendapatan + data[0].Pelunasan_Geran + data[0].Faedah_Keuntungan +
                        data[0].Manfaat_Pekerja + data[0].Susut_Nilai + data[0].Belanja_Penulasan +
                        data[0].Belanja_Hutang_Ragu + data[0].Faedah_Pajakan + data[0].Pelarasan_Pengkelasan_Akaun +
                        data[0].Pelarasan_Hartanah + data[0].Keuntungan_atas_Pelupusan + data[0].Kerugian_atas_Pelupusan;

                    var a = data[0].Pengurangan_AkaunBelumTerima_1 - data[0].Pengurangan_AkaunBelumTerima_2 - data[0].Pengurangan_AkaunBelumTerima_3;/*- b - c*/
                    var b = data[0].Pengurangan_UrusNiaga_1 - data[0].Pengurangan_UrusNiaga_2 - data[0].Pengurangan_AkaunBelumTerima_3;
                    var c = data[0].Pertambahan_Prabayar_1 - data[0].Pertambahan_Prabayar_2 - data[0].Pertambahan_Prabayar_3;
                    var sum2 = a + b + c + data[0].Pengurangan_Terakru + data[0].Pengurangan_Belum_Bayar;
                    var sum3 = sum1 + sum2 + mPekerja;

                    var FaedahDiterima = data[0].Faedah_Keuntungan_Terima_1 - data[0].Faedah_Keuntungan_Terima_2;
                    var sum4 = FaedahDiterima + data[0].Dividen_Terima + data[0].Pelaburan_Lain + data[0].Penerimaan_Pinjaman +
                        data[0].Bayaran_Pinjaman + data[0].Pembelian_Hartanah + data[0].Pembelian_Aset_Tak_Ketara +
                        data[0].Terimaan_Hasil_Pelupusan + data[0].Pembayaran_Pembinaan;

                    var sum5 = data[0].Pembayaran_Pajakan + data[0].Penerimaan_Geran_Khusus;
                    var Sum345 = sum3 + sum4 + sum5;
                    var sum6 = Sum345 + data[0].Baki_1_Jan;
                    var sum7 = data[0].Simpanan_Tetap + data[0].Tunai_Di_Tangan;

                    document.getElementById("lblLebihan").innerHTML = formatNumber(data[0].LebihanPendapatan); /*row2*/
                    document.getElementById("lblLunasGeran").innerHTML = formatNumber(data[0].Pelunasan_Geran);   /*row2*/
                    document.getElementById("lblFaedahDiterima").innerHTML = formatNumber(data[0].Faedah_Keuntungan); /*row3*/
                    document.getElementById("lblManfaatP").innerHTML = formatNumber(data[0].Manfaat_Pekerja); /*row4*/
                    document.getElementById("lblSusutNilai").innerHTML = formatNumber(data[0].Susut_Nilai); /*row5*/
                    document.getElementById("lblBPelunasan").innerHTML = formatNumber(data[0].Belanja_Penulasan); /*row6*/
                    document.getElementById("lblBHutangRagu").innerHTML = formatNumber(data[0].Belanja_Hutang_Ragu); /*row7*/
                    document.getElementById("lblFaedahPajakan").innerHTML = formatNumber(data[0].Faedah_Pajakan); /*row8*/
                    document.getElementById("lblPPAkaun").innerHTML = formatNumber(data[0].Pelarasan_Pengkelasan_Akaun); /*row9*/

                    document.getElementById("lblPHartanah").innerHTML = formatNumber(data[0].Pelarasan_Hartanah); /*row12*/
                    document.getElementById("lblKeuntunganAtasPelupusan").innerHTML = formatNumber(data[0].Keuntungan_atas_Pelupusan); /*row13*/
                    document.getElementById("lblKerugianAtasPelupusan").innerHTML = formatNumber(data[0].Kerugian_atas_Pelupusan); /*row14*/

                    document.getElementById("sum1").innerHTML = formatNumber(sum1); /*row15*/
                    document.getElementById("lblPPAkaunBelumTerima").innerHTML = formatNumber(a); /*row15*/
                    document.getElementById("lblPPPertukaranBelumTerima").innerHTML = formatNumber(b); /*row16*/
                    document.getElementById("lblPPPendahuluan").innerHTML = formatNumber(c); /*row17*/
                    document.getElementById("lblPPendapatanTerakru").innerHTML = formatNumber(data[0].Pengurangan_Terakru); /*row18*/
                    document.getElementById("lblPPAkaunBelumBayar").innerHTML = formatNumber(data[0].Pengurangan_Belum_Bayar); /*row19*/

                    document.getElementById("sum2").innerHTML = formatNumber(sum2); /*row20*/

                    document.getElementById("sum3").innerHTML = formatNumber(sum3); /*row23*/

                    document.getElementById("lblFKeuntunganDiterima").innerHTML = formatNumber(FaedahDiterima); /*row26*/
                    document.getElementById("lblDividenDiterima").innerHTML = formatNumber(data[0].Dividen_Terima); /*row27*/
                    document.getElementById("lblPelaburanLain").innerHTML = formatNumber(data[0].Pelaburan_Lain); /*row28*/
                    document.getElementById("lblPPinjamanKenderaan").innerHTML = formatNumber(data[0].Penerimaan_Pinjaman); /*row30*/
                    document.getElementById("lblBPKenderaan").innerHTML = formatNumber(data[0].Bayaran_Pinjaman); /*row31*/
                    document.getElementById("lblPHartanahLoji").innerHTML = formatNumber(data[0].Pembelian_Hartanah); /*row32*/
                    document.getElementById("lblPembelianATK").innerHTML = formatNumber(data[0].Pembelian_Aset_Tak_Ketara); /*row33*/
                    document.getElementById("lblTerimaanHPH").innerHTML = formatNumber(data[0].Terimaan_Hasil_Pelupusan); /*row34*/
                    document.getElementById("lblPPembayaranPDK").innerHTML = formatNumber(data[0].Pembayaran_Pembinaan); /*row35*/
                    document.getElementById("sum4").innerHTML = formatNumber(sum4); /*row36*/

                    document.getElementById("lblPPajakanKewangan").innerHTML = formatNumber(data[0].Pembayaran_Pajakan); /*row39*/
                    document.getElementById("lblPPelbagaiGK").innerHTML = formatNumber(data[0].Penerimaan_Geran_Khusus); /*row42*/
                    document.getElementById("sum5").innerHTML = formatNumber(sum5); /*row43*/

                    document.getElementById("lblSum3Sum4Sum5").innerHTML = formatNumber(Sum345); /*row45*/

                    document.getElementById("lblTDKAwalTahun").innerHTML = formatNumber(data[0].Baki_1_Jan); /*row46*/
                    document.getElementById("sum6").innerHTML = formatNumber(sum6); /*row47*/

                    document.getElementById("lblSimpananTetap").innerHTML = formatNumber(data[0].Simpanan_Tetap); /*row50*/
                    document.getElementById("lblTunaiTanganBank").innerHTML = formatNumber(data[0].Tunai_Di_Tangan); /*row51*/
                    document.getElementById("sum7").innerHTML = formatNumber(sum7); /*row52*/

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
