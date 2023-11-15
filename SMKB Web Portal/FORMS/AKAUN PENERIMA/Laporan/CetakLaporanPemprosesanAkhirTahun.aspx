<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CetakLaporanPemprosesanAkhirTahun.aspx.vb" Inherits="SMKB_Web_Portal.CetakLaporanPemprosesanAkhirTahun" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Laporan Pemprosesan Akhir Tahun</title>
    <script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
    <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" integrity="sha384-xOolHFLEh07PJGoPkLv1IbcEPTNtaed2xpHsD9ESMhqIYd0nLMwNLD69Npy4HI+N" crossorigin="anonymous">
</head>
<body class="d-flex flex-column justify-content-center align-items-center w-auto">
    <style>
        * {
            font-family: 'Times New Roman', Times, serif;
        }
        /* Custom CSS for GridView styling */
        .table th {
            text-align: center;
            font-weight: bold;
        }

        body {
            /* Set page size to A4 (21cm x 29.7cm) and add margins */
            size: A4 portrait;
            margin: 2cm;
            max-width: 100%;
        }

        /* Hide the print button when printing */
        @media print {
            body {
                /* Reset body styles for printing */
                size: Letter portrait;
                margin: 0;
                max-width: 100%;
            }

            @page {
                /* Define page size and margins for printing */
                size: Letter portrait;
                margin: 1cm; /* Adjust margin to fit Letter size */
            }

            .container {
                /* Adjust container styles for printing */
                width: 100%;
                margin: 0;
                padding: 0;
            }

            #lblTarikhLaporan {
                /* Adjust label styles for printing */
                width: 15%;
            }

            #gvPenghutangTahun {
                /* Add page break for the GridView */
                page-break-inside: auto;
            }

            /* Hide unnecessary elements */
            #btnPrint, hr {
                display: none;
            }
        }
    </style>
    <form id="form1" runat="server" class="p-5 pt-2 m-auto">
        <div style="text-align: center;">
            <img src="../../../Images/logo.png" /> 
        </div>
        <div class="justify-content-center">
            <hr />
            <h2 class="text-center font-weight-bold">Laporan Penyata Kewangan Penghutang&nbsp;(<span id="txtTahun" runat="server"></span>)</h2>
        </div>
        <br />
        <div>
            <table class="container" style="width: 100%;text-align:left">
                <tr>
                    <td id="lblTarikhLaporan" class="font-weight-bolder" style="width: 25%;">Tarikh Laporan</td>
                    <td style="width: 3%;">:</td>
                    <td id="txtTarikhLaporan" name="txtTarikhLaporan" runat="server"></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="container mt-4">
            <asp:GridView ID="gvPenghutangTahun" runat="server" CssClass="table table-bordered w-100" AutoGenerateColumns="false"
                OnRowDataBound="gvPenghutang_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="NoPenghutang" HeaderText="No. Penghutang" ItemStyle-Width="24%" />
                    <asp:BoundField DataField="Nama" HeaderText="Nama" ItemStyle-Width="36%" />
                    <asp:BoundField DataField="Debit" HeaderText="Debit (RM)" DataFormatString="{0:N2}" ItemStyle-Width="20%" />
                    <asp:BoundField DataField="Credit" HeaderText="Credit (RM)" DataFormatString="{0:N2}" ItemStyle-Width="20%" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="container d-flex mt-4 justify-content-between">
            <label class="font-weight-bolder">Jumlah Credit (RM)</label>
            <label class="fa-bold" id="txtJumlahCredit" name="txtJumlahCredit" runat="server"></label>
        </div>
        <div class="container d-flex mt-2 justify-content-between">
            <label class="font-weight-bolder">Jumlah Debit (RM)</label>
            <label class="fa-bold" id="txtJumlahDebit" name="txtJumlahDebit" runat="server"></label>
        </div>
        <div class="container d-flex mt-4 justify-content-between">
            <label class="font-weight-bolder">Jumlah Baki (RM)</label>
            <label class="fa-bold" id="txtJumlahBaki" name="txtJumlahBaki" runat="server"></label>
        </div>
    </form>
</body>
<button id="btnPrint" class="btn btn-primary btnPrint" type="button">
    Cetak
</button>
<script type="text/javascript">
    $(document).ready(function () {

        // set #tahunNow to current year
        var tahunNow = new Date().getFullYear();
        $('#tahunNow').text(tahunNow);

        // set #txtTarikhLaporan to current date (dd/mm/yyyy)
        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; // January is 0!
        var yyyy = today.getFullYear();

        if (dd < 10) {
            dd = '0' + dd
        }

        if (mm < 10) {
            mm = '0' + mm
        }

        today = dd + '/' + mm + '/' + yyyy;
        $('#txtTarikhLaporan').text(today);

        window.print();
    });

    // load window.print() when #btnPrint is clicked
    $('#btnPrint').click(function () {
        location.reload();
    });
</script>
</html>
