<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="reportpnldetailbaru.aspx.vb" Inherits="SMKB_Web_Portal.reportpnldetailbaru" %>

<%@ Register Src="~/FORMS/JURNAL/LAPORAN/PrintReport/TableVotDetailbaru.ascx" TagPrefix="Vot" TagName="Table" %>

<!DOCTYPE html>
<html>
<head>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">

    <!-- Optional theme -->

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous">

    <!-- Bootstrap JS -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa" crossorigin="anonymous"></script>
    <script src="https://printjs-4de6.kxcdn.com/print.min.js"></script>
    <title></title>

    <style>

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
        }

        .pheader {
            text-align: center;
            font-size: 14px;
            font-weight: bold;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }

        .ptarikh {
            font-size: 12px;
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

            .printButton {
                display: none !important;
            }

            #printButton {
                display: block;
            }
        }

</style>

    
</head>
<body>
    <form id="form1" runat="server">
        <div id="masterdiv" class="container">

            <div id="headerreport">
                <table>
                    <tr>
                        <td style="width: 100%">
                            <p class="pheader">Universiti Teknikal Malaysia Melaka</p>
                            <p class="pheader">Penyata Prestasi Kewangan</p>
                            <p class="pheader">Bagi Tahun Berakhir 31 <asp:Label runat="server" ID="bulan"></asp:Label> <asp:label runat="server" id="tahun"></asp:label></p>
                            <p class="pheader">Keseluruhan</p>
                        </td>
<%--                        <td style="width: 20%; text-align: right">
                            <p class="ptarikh">Tarikh : 26/05/2021 5:14:40</p>
                            <p class="ptarikh">Pengguna : 02064</p>

                        </td>--%>
                    </tr>
                </table>
            </div> <%--close header report--%>


            <div class="vot-table-content">
                 <div id="headerbutiranheader">
                    <table>
                        <tr>
                            <th style="width: 10%"></th>
                            <td style="width: 39%"></td>
                            <td class="valuekanan" style="width: 17%"><strong>RM</strong></td>
                            <td class="valuekanan" style="width: 17%"><strong>RM</strong></td>
                            <td class="valuekanan" style="width: 17%"><strong>RM</strong></td>
                        </tr>
                        <tr>
                            <td colspan="2"><strong><u>PENDAPATAN</u></strong></td>
                            <td class="valuekanan" style="width: 17%"></td>
                            <td class="valuekanan" style="width: 17%"></td>
                            <td class="valuekanan" style="width: 17%"></td>
                        </tr>
                    </table>
                </div> <%--close header report--%>


                <br />
                <strong>Pendapatan daripada Urus Niaga Bukan Pertukaran</strong>
                <Vot:Table runat="server" id="tblPendapatan" KategoriVot="1" CustomClass ="tblPendapatan"/>

                <br />
                <strong>Pendapatan daripada Urus Niaga Pertukaran</strong>
                <Vot:Table runat="server" id="tblPendapatan2" KategoriVot = "2" CustomClass ="tblPendapatan"/>

                <br />
                <strong>Lain-lain Pendapatan</strong>
                <Vot:Table runat="server" id="tblPendapatan3" KategoriVot = "3" CustomClass ="tblPendapatan"/>

                <br />
                <table>
                    <tr>
                        <th style="width: 40%">JUMLAH PENDAPATAN</th>
                        <td style="width: 9%"></td>
                        <td class="valuekanan" style="width: 17%"></td>
                        <td class="valuekanan" style="width: 17%"></td>
                        <td class="valuekanan" style="width: 17%">
                            <span class="top-border lblamaun bold" id="totalAmaunMax"></span>
                        </td> 
                    </tr>
                </table>
                <script>
                    var totalAmaun = parseFloat(getObj("tblPendapatan_lbl2AmauntableData").innerText.replace(/[,]+/g, ""));
                    totalAmaun += parseFloat(getObj("tblPendapatan2_lbl2AmauntableData").innerText.replace(/[,]+/g, ""));
                    totalAmaun += parseFloat(getObj("tblPendapatan3_lbl2AmauntableData").innerText.replace(/[,]+/g, ""));
                    getObj("totalAmaunMax").innerText = (totalAmaun.toLocaleString("en-US"));

                    function getObj(id) {
                        return document.getElementById(id);
                    }
                    </script>

            </div>
            <%--close section pendapatan--%>


            <br /><br />
            <div class="vot-table-content">
                <div id="headerbutiranheader2">
                    <table>
                        <tr>
                            <td><strong><u>PERBELANJAAN</u></strong></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                 <br />
                </div> <%--close section perbelanjaan--%>
                <Vot:Table runat="server" id="votBt" KodVotFrom="00000" KodVotTo ="09999" Tajuk ="Perbelanjaan Bukan Tunai" CustomClass ="tblPerbelanjaan"/>
                
            </div>
            <br />
            <div class="vot-table-content">
            <Vot:Table runat="server" id="votEmolumen" KodVotFrom="10000" KodVotTo ="19000" Tajuk ="Emolumen" CustomClass ="tblPerbelanjaan"/>
            </div>
            <br />

            <div class="vot-table-content">
                <Vot:Table runat="server" id="votPerkhidmatan" KodVotFrom="20000" KodVotTo ="29999" Tajuk ="Perkhidmatan Dan Bekalan" CustomClass ="tblPerbelanjaan" />
            </div>
            <br />

            <div class="vot-table-content">
                <Vot:Table runat="server" id="Table1" KodVotFrom="30000" KodVotTo ="39999" Tajuk ="Aset" CustomClass ="tblPerbelanjaan" />
            </div>
            <br />

            <div class="vot-table-content">
                <Vot:Table runat="server" id="Table2" KodVotFrom="40000" KodVotTo ="49999" Tajuk ="Pemberian Dan Bayaran Tetap" CustomClass ="tblPerbelanjaan" />
            </div>
            <br />

            <div class="vot-table-content">
                <Vot:Table runat="server" id="Table3" KodVotFrom="50000" KodVotTo ="59999" Tajuk ="Perbelanjaan Lain" CustomClass ="tblPerbelanjaan" />
            </div>
            <br />

                <table>
                    <tr>
                        <th style="width: 40%">JUMLAH PERBELANJAAN</th>
                        <td style="width: 9%"></td>
                        <td class="valuekanan" style="width: 17%"></td>
                        <td class="valuekanan" style="width: 17%"></td>
                        <td class="valuekanan" style="width: 17%">
                            <span class="top-border lblamaun bold" id="totalPerbelanjaan"></span>
                        </td> 
                    </tr>
              </table>

            <br />
            <table>
                     <tr>
                        <th style="width: 40%">LEBIHAN/(KURANGAN)</th>
                        <td style="width: 9%"></td>
                        <td class="valuekanan" style="width: 17%"></td>
                        <td class="valuekanan" style="width: 17%"></td>
                        <td class="valuekanan" style="width: 17%">
                            <span class="topbottom-border lblamaun bold" id="lebihankurangan"></span>
                        </td> 
                    </tr>
   
            </table>
            <br /><br /><br />
            
            <div style="text-align:center">
                <span><strong>***** Tamat Laporan *****</strong></span>
            </div>
            


                <script>
                    var totalBelanja = parseFloat(getObj("votBt_lbl2AmauntableData").innerText.replace(/[,]+/g, ""));
                    totalBelanja += parseFloat(getObj("votEmolumen_lbl2AmauntableData").innerText.replace(/[,]+/g, ""));
                    totalBelanja += parseFloat(getObj("votPerkhidmatan_lbl2AmauntableData").innerText.replace(/[,]+/g, ""));
                    totalBelanja += parseFloat(getObj("Table1_lbl2AmauntableData").innerText.replace(/[,]+/g, ""));
                    totalBelanja += parseFloat(getObj("Table2_lbl2AmauntableData").innerText.replace(/[,]+/g, ""));
                    totalBelanja += parseFloat(getObj("Table3_lbl2AmauntableData").innerText.replace(/[,]+/g, ""));

                    getObj("totalPerbelanjaan").innerText = (totalBelanja.toLocaleString("en-US"));


                    var totalLebihan = parseFloat(getObj("totalAmaunMax").innerText.replace(/[,]+/g, ""));
                    totalLebihan -= parseFloat(getObj("totalPerbelanjaan").innerText.replace(/[,]+/g, ""));
                    getObj("lebihankurangan").innerText = (totalLebihan.toLocaleString("en-US"));


                    function getObj(id) {
                        return document.getElementById(id);
                    }
            </script>


        
            <!-- Add the print button -->
            <br /><br />
            <%--<button onclick="return printContent()">print</button>--%>
                <asp:Button ID="printButton" runat="server" Text="Print" CssClass="print-button printButton" OnClick="Button1_Click"  OnClientClick=" printContent();" AutoPostBack="False" />
            <br /><br />
            <script>
                function printContent() {
                    window.print();
                    return false;
                }

                if (window.addEventListener) {
                    window.print();
                } else {
                    window.print();
                }
            </script>
        </div> <%--close master div--%>

    </form>
</body>
</html>