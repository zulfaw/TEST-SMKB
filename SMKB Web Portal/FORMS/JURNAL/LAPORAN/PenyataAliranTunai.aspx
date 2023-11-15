<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PenyataAliranTunai.aspx.vb" Inherits="SMKB_Web_Portal.PenyataAliranTunai" %>



<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
    <%--<script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>--%>
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

        .tblPendapatan {
            display: none;
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

        table.dataTable.no-footer {
            border-bottom: unset !important;
        }


    </style>


    <div id="PermohonanTab" class="tabcontent" style="display: block">
        <!-- Modal -->
        <div id="permohonan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Penyata Aliran Tunai</h5>

                    </div>

                    <!-- Create the dropdown filter -->
                    <div class="search-filter">

                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align: right">Syarikat :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList2" runat="server" DataTextField="Nama" DataValueField="Nama" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <br />


                    <div class="form-row justify-content-center">
                    <div class="form-group row col-md-6">
                        <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align: right">Tahun :</label>
                        <div class="col-sm-8" style="display: flex;">
                            <select id="ddlyear" runat="server" class="ui fluid search dropdown selection custom-select">
                                <option value="">-- Sila Pilih --</option>
                                <option value="2023">2023</option>
                                <option value="2022">2022</option>
                                <option value="2021">2021</option>
                                <option value="2020">2020</option>
                            </select>
                            <button id="btnSearch" runat="server" class="btn btnSearch" onclick="return beginSearch();" type="button" style="margin-left: 10px;">
                                <i class="fa fa-search"></i>
                            </button>
                        </div>
                    </div>
                        </div>

<%--                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="txtPerihal1" class="col-sm-2 col-form-label" style="text-align: right">Ptj :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Pejabat" DataValueField="KodPejabat" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />--%>


<%--                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="kodKewangan" class="col-sm-2 col-form-label" style="text-align: right">Kod KW :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="kodkw" runat="server" DataTextField="Butiran" DataValueField="Kod_Kump_Wang" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />--%>

                        <div class="container">
                            <div class="row justify-content-center">
                                <div class="col-md-9">
                                    <div class="form-row">
                                        <div class="col-md-2 order-md-1">
                                            </div>
 <%--                                       <div class="col-md-3 order-md-3">
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
                                        </div>--%>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <div class="modal-body">
                        <div class="col-md-12">
                            <%--<button class="dt-button buttons-print" tabindex="0" type="button" style="margin:10px 0px 10px 0px" onclick="ShowDetails()"><span>Print</span></button>--%>
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai_trans_rpt" class="table table-striped" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th scope="col" style="text-align: center"></th>
                                            <th scope="col" style="text-align: center">RM</th>
                                        </tr>
                                       <tr>
                                            <td scope="col">ALIRAN TUNAI DARI AKTIVITI OPERASI</td>
                                            <td scope="col"></td>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_trans">
                                      
                                        <tr>
                                             <td scope="col">Lebihan/(Kurangan) Pendapatan </td> 
                                             <td scope="col" id="row1" class="align-right"><label id="lblLebihan"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pelarasan untuk :</td> 
                                             <td scope="col" id="row2"></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pelunasan Geran</td> 
                                             <td scope="col" id="row3" class="align-right"><label id="lblLunasGeran"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Faedah, Keuntungan dan Dividen Diterima</td> 
                                             <td scope="col" id="row4" class="align-right"><label id="lblFaedahDiterima"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Manfaat Pekerja</td> 
                                             <td scope="col" id="row5" class="align-right"><label id="lblManfaatP"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Susut Nilai</td> 
                                             <td scope="col" id="row6" class="align-right"><label id="lblSusutNilai"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Belanja Pelunasan</td> 
                                             <td scope="col" id="row7" class="align-right"><label id="lblBPelunasan"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Belanja Hutang Ragu</td> 
                                             <td scope="col" id="row8" class="align-right"><label id="lblBHutangRagu"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Faedah Pajakan</td> 
                                             <td scope="col" id="row9" class="align-right"><label id="lblFaedahPajakan"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pelarasan Pengkelasan Akaun</td> 
                                             <td scope="col" id="row10" class="align-right"><label id="lblPPAkaun"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pelarasan Hartanah, Loji dan Peralatan</td> 
                                             <td scope="col" id="row11" class="align-right"><label id="lblPHartanah"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Keuntungan atas Pelupusan Hartanah, Loji dan Peralatan</td> 
                                             <td scope="col" id="row12" class="align-right"><label id="lblKeuntunganAtasPelupusan"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Kerugian atas Pelupusan Hartanah, Loji dan Peralatan</td> 
                                             <td scope="col" id="row13" class="align-right"><label id="lblKerugianAtasPelupusan"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Lebihan/(Kurangan) Operasi sebelum perubahan Modal Kerja</td> 
                                             <td scope="col" id="row14" class="align-right">
                                                 <label id="sum1" class="subtotal-label"></label>
                                             </td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pengurangan/(pertambahan) dalam Akaun Belum Terima</td> 
                                             <td scope="col" id="row15" class="align-right"><label id="lblPPAkaunBelumTerima"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pengurangan/(pertambahan) dalam Urus Niaga Bukan Pertukaran Belum Terima</td> 
                                             <td scope="col" id="row16" class="align-right"><label id="lblPPPertukaranBelumTerima"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">(Pertambahan)/pengurangan dalam Pendahuluan, Deposit dan Prabayar</td> 
                                             <td scope="col" id="row17" class="align-right"><label id="lblPPPendahuluan"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pengurangan dalam Pendapatan Terakru</td> 
                                             <td scope="col" id="row18" class="align-right"><label id="lblPPendapatanTerakru"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pengurangan/(pertambahan) dalam Akaun Belum Bayar</td> 
                                             <td scope="col" id="row19" class="align-right"><label id="lblPPAkaunBelumBayar"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Tunai (untuk)/daripada Aktiviti Operasi</td> 
                                             <td scope="col" id="row20" class="align-right"><label id="sum2" class="subtotal-label"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Manfaat Pekerja Dibayar</td> 
                                             <td scope="col" id="row21" class="align-right"><label id="lblManfaatPekerjaDibayar"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Tunai Bersih (untuk)/daripada Aktiviti Operasi</td> 
                                             <td scope="col" id="row22" class="align-right"><label id="sum3" class="subtotal-label2"></label></td>
                                        </tr>
                                         <tr>
                                             <td scope="col"></td> 
                                             <td scope="col" id="row24"></td>
                                        </tr>
                                       <tr>
                                             <td scope="col"><b>ALIRAN TUNAI DARI AKTIVITI PELABURAN</b></td> 
                                             <td scope="col" id="row25"></td>
                                        </tr>
                                       <tr>
                                             <td scope="col">Faedah dan Keuntungan Diterima</td> 
                                             <td scope="col" id="row26" class="align-right"><label id="lblFKeuntunganDiterima"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Dividen Diterima</td> 
                                             <td scope="col" id="row27" class="align-right"><label id="lblDividenDiterima"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pelaburan Lain</td> 
                                             <td scope="col" id="row28" class="align-right"><label id="lblPelaburanLain"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Penerimaan Pinjaman Kenderaan/Komputer/Peralatan</td> 
                                             <td scope="col" id="row30" class="align-right"><label id="lblPPinjamanKenderaan"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Bayaran Pinjaman Kenderaan/Komputer/Peralatan</td> 
                                             <td scope="col" id="row31" class="align-right"><label id="lblBPKenderaan"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pembelian Hartanah, Loji dan Peralatan</td> 
                                             <td scope="col" id="row32" class="align-right"><label id="lblPHartanahLoji"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pembelian Aset Tak Ketara</td> 
                                             <td scope="col" id="row33" class="align-right"><label id="lblPembelianATK"></label></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Terimaan Hasil Pelupusan Hartanah, Loji dan Peralatan</td> 
                                             <td scope="col" id="row34" class="align-right"><label id="lblTerimaanHPH"></label></td>
                                        </tr>
                                       <tr>
                                             <td scope="col">Pembayaran Pembinaan dalam Kemajuan</td> 
                                             <td scope="col" id="row35" class="align-right"><label id="lblPPembayaranPDK"></label></td>
                                        </tr>
                                       <tr>
                                             <td scope="col">Tunai Bersih untuk Aktiviti Pelaburan</td> 
                                             <td scope="col" id="row36" class="align-right"><label id="sum4" class="subtotal-label2"></label></td>
                                       </tr>
                                       <tr>
                                             <td scope="col"></td> 
                                             <td scope="col" id="row37"></td>
                                       </tr>
                                       <tr>
                                             <td scope="col"><b>ALIRAN TUNAI DARI AKTIVITI PEMBIAYAAN</b></td> 
                                             <td scope="col" id="row38"></td>
                                        </tr>
                                        <tr>
                                             <td scope="col">Pembayaran Pajakan Kewangan</td> 
                                             <td scope="col" id="row39" class="align-right"><label id="lblPPajakanKewangan"></label></td>
                                        </tr>
                                       <tr>
                                             <td scope="col">Penerimaan Pelbagai Geran Khusus</td> 
                                             <td scope="col" id="row42" class="align-right"><label id="lblPPelbagaiGK"></label></td>
                                        </tr>
                                       <tr>
                                             <td scope="col">Tunai Bersih daripada Aktiviti Pembiayaan</td> 
                                             <td scope="col" id="row43" class="align-right"><label id="sum5" class="subtotal-label2"></label></td>
                                       </tr>
                                       <tr>
                                             <td scope="col"></td> 
                                             <td scope="col" id="row44"></td>
                                       </tr>
                                       <tr>
                                             <td scope="col"> Penambahan/(Pengurangan) Bersih Tunai dan Kesetaraan Tunai</td> 
                                             <td scope="col" id="row45" class="align-right"><label id="lblSum3Sum4Sum5"></label></td>
                                        </tr>
                                       <tr>
                                             <td scope="col">Tunai dan Kesetaraan Tunai Pada Awal Tahun</td> 
                                             <td scope="col" id="row46" class="align-right"><label id="lblTDKAwalTahun"></label></td>
                                        </tr>
                                       <tr>
                                             <td scope="col">Tunai dan Kesetaraan Tunai Pada Akhir Tahun</td> 
                                             <td scope="col" id="row47" class="align-right"><label id="sum6" class="subtotal-label3"></label></td>
                                       </tr>
                                       <tr>
                                             <td scope="col"></td> 
                                             <td scope="col" id="row48"></td>
                                       </tr>
                                       <tr>
                                             <td scope="col"> Tunai dan Kesetaraan Tunai termasuk :</td> 
                                             <td scope="col" id="row49"></td>
                                        </tr>
                                       <tr>
                                             <td scope="col">Simpanan Tetap</td> 
                                             <td scope="col" id="row50" class="align-right"><label id="lblSimpananTetap"></label></td>
                                        </tr>
                                       <tr>
                                             <td scope="col">Tunai di Tangan dan di Bank</td> 
                                             <td scope="col" id="row51" class="align-right"><label id="lblTunaiTanganBank"></label></td>
                                       </tr>
                                       <tr>
                                             <td scope="col">Jumlah</td> 
                                             <td scope="col" id="row52" class="align-right"><label id="sum7" class="subtotal-label3"></label></td>
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
            $(".ui.dropdown").dropdown({
                fullTextSearch: true
            });

            var table = null;
            


            var tbl = null;
            var bulan = '';
            var thn = '';

            $(document).ready(function () {
                table = generateTable();
            });

            $(document).ready(function () {
                $(document).on("click", ".btnSearch", function (e) {

                    e.preventDefault();

                    getData($('#<%=ddlyear.ClientID%>').val());
                    return false;
                });
            });

            function getData(tahun) {

                fetch('LejarPenghutangWS.asmx/LoadDataAliran', {
                    method: 'POST',
                    headers: {
                        'Content-Type': "application/json"
                    },
                    //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
                    body: JSON.stringify({ tahun: tahun })
                })
                    .then(response => response.json())
                    .then(data => setData(data.d))

            }




            function setData(data) {
                table.destroy();
                console.log("test " + data)
                data = JSON.parse(data);

                //if (data.bulan === "") {
                //    alert("Tiada data");
                //    return false;
                //}

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

                 $('#lblLebihan').html(data[0].LebihanPendapatan.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row1*/
                $('#lblLunasGeran').html(data[0].Pelunasan_Geran.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row2*/
                $('#lblFaedahDiterima').html(data[0].Faedah_Keuntungan.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row3*/
                $('#lblManfaatP').html(data[0].Manfaat_Pekerja.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row4*/
                $('#lblSusutNilai').html(data[0].Susut_Nilai.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row5*/
                $('#lblBPelunasan').html(data[0].Belanja_Penulasan.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row6*/
                $('#lblBHutangRagu').html(data[0].Belanja_Hutang_Ragu.toLocaleString('en-US', { maximumFractionDigits: 2 }));; /*row7*/
                $('#lblFaedahPajakan').html(data[0].Faedah_Pajakan.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row8*/
                $('#lblPPAkaun').html(data[0].Pelarasan_Pengkelasan_Akaun.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row9*/
                //key in manual(data manual) Pelarasan Pengkelasan Akaun
                //key in manual(data manual)		Pelarasan Hartanah, Loji dan Peralatan
                $('#lblPHartanah').html(data[0].Pelarasan_Hartanah.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row12*/
                $('#lblKeuntunganAtasPelupusan').html(data[0].Keuntungan_atas_Pelupusan.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row13*/
                $('#lblKerugianAtasPelupusan').html(data[0].Kerugian_atas_Pelupusan.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row14*/
                $('#sum1').text(sum1.toLocaleString('en-US', { maximumFractionDigits: 2 }));  /*row15*/

                $('#lblPPAkaunBelumTerima').text(a.toLocaleString('en-US', { maximumFractionDigits: 2 }));  /*row15*/
                $('#lblPPPertukaranBelumTerima').text(b.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row16*/
                $('#lblPPPendahuluan').text(c.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row17*/
                $('#lblPPendapatanTerakru').html(data[0].Pengurangan_Terakru.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row18*/
                $('#lblPPAkaunBelumBayar').html(data[0].Pengurangan_Belum_Bayar.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row19*/
                $('#sum2').text(sum2.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row20*/

                // $('#lblManfaatPekerjaDibayar'). key in manual (data manual) A - Manfaat Pekerja Dibayar  /*row21*/
                $('#sum3').text(sum3.toLocaleString('en-US', { maximumFractionDigits: 2 }));  /*row23*/

                $('#lblFKeuntunganDiterima').text(FaedahDiterima.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row26*/
                $('#lblDividenDiterima').html(data[0].Dividen_Terima.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row27*/
                $('#lblPelaburanLain').html(data[0].Pelaburan_Lain.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row28*/
                $('#lblPPinjamanKenderaan').html(data[0].Penerimaan_Pinjaman.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row30*/
                $('#lblBPKenderaan').html(data[0].Bayaran_Pinjaman.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row31*/
                $('#lblPHartanahLoji').html(data[0].Pembelian_Hartanah.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row32*/
                $('#lblPembelianATK').html(data[0].Pembelian_Aset_Tak_Ketara.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row33*/
                $('#lblTerimaanHPH').html(data[0].Terimaan_Hasil_Pelupusan.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*key in manual(data manual) Terimaan Hasil Pelupusan Hartanah, Loji dan Peralatan*/ /*row34*/
                $('#lblPPembayaranPDK').html(data[0].Pembayaran_Pembinaan.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row35*/
                $('#sum4').text(sum4.toLocaleString('en-US', { maximumFractionDigits: 2 }));  /*row36*/

                $('#lblPPajakanKewangan').html(data[0].Pembayaran_Pajakan.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row39*/
                $('#lblPPelbagaiGK').html(data[0].Penerimaan_Geran_Khusus.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row42*/
                $('#sum5').text(sum5.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row43*/


                $('#lblSum3Sum4Sum5').text(Sum345.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row45*/
                $('#lblTDKAwalTahun').html(data[0].Baki_1_Jan.toLocaleString('en-US', { maximumFractionDigits: 2 }));  /*row46*/
                $('#sum6').text(sum6.toLocaleString('en-US', { maximumFractionDigits: 2 })); /*row47*/

                $('#lblSimpananTetap').html(data[0].Simpanan_Tetap.toLocaleString('en-US', { maximumFractionDigits: 2 }));  /*row50*/
                $('#lblTunaiTanganBank').html(data[0].Tunai_Di_Tangan.toLocaleString('en-US', { maximumFractionDigits: 2 }));  /*row51*/
                $('#sum7').text(sum7.toLocaleString('en-US', { maximumFractionDigits: 2 }));  /*row52*/

                table = generateTable();
            }

            function generateTable() {
                return $("#tblDataSenarai_trans_rpt").DataTable({
                    "responsive": true, "lengthChange": false, "autoWidth": false,
                    paging: false,
                    dom: 'Bfrt',
                    "ordering": false,
                    buttons: [
                        'csv', 'excel', {
                            // 'csv', 'excel', 'pdf', {

                            extend: 'print',
                            text: '<i class="fa fa-files-o green"></i> Print',
                            titleAttr: 'Print',
                            className: 'ui green basic button',
                            action: function (e, dt, button, config) {
<%--                                window.location.href = '<%=ResolveClientUrl("~/reportbaru4.aspx")%>';--%>
                                location.replace('<%=ResolveClientUrl("~/FORMS/JURNAL/LAPORAN/PrintReport/reportPenyataAliranTunai.aspx")%>', '_blank');

                            }
                        }
                    ],
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
                });
            }





    function beginSearch() {
        tbl.ajax.reload();
    }

    function ShowDetails() {
        location.replace('<%=ResolveClientUrl("~/FORMS/JURNAL/LAPORAN/PrintReport/reportPenyataAliranTunai.aspx")%>','_blank');

    }

        </script>

</asp:Content>
