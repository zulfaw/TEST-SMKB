<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="reportpnl.aspx.vb" Inherits="SMKB_Web_Portal.reportbaru4" %>

<%@ Register Src="~/FORMS/JURNAL/LAPORAN/PrintReport/TableVot.ascx" TagPrefix="Vot" TagName="Table" %>

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
        body {
            font-family: Arial, sans-serif;
        }

        .pheader {
            text-align: center;
            font-size: 14px;
            font-weight: bold;
        }

        .ptarikh {
            font-size: 12px;
        }

        table {
            width: 100%;
        }

        th, td {
            padding: 3px;
        }

        .headerkiri {
            text-align: center;
        }

        .valuekanan {
            text-align: right;
        }

        .bold {
            font-weight: bold;
        }


@media print {
  .col-md-2 {
    float: right;
    /* margin-top: -50px; */
  }

  a[href]:after,
  img[src]:after {
    content: none !important;
  }

  .tdbg {
    -webkit-print-color-adjust: exact;
    print-color-adjust: exact;
    background-color: gray !important;
  }

  .pemberianspacing {
    padding-top: 20px;
  }

  .printButton {
    display: none !important;
  }

  /* Hide url and datetime elements */
  .url,
  .datetime {
    display: none !important;
  }

  /* Hide auto-generated header and footer */
  @page {
      size:A4;
    margin: 0px;
  }

  body {
    margin:1cm !important;
  }

}

#printButton {
  display: block;
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
                            <p class="pheader">Penyata Pendapatan Komprehensif</p>
                            <p class="pheader">Universiti Teknikal Malaysia Melaka</p>
                            <p class="pheader">Bagi Bulan <asp:Label runat="server" ID="bulan"></asp:Label> <asp:label runat="server" id="tahun"></asp:label></p>
                            <p class="pheader">(Keseluruhan)</p>
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
                            <td><strong><u>PENDAPATAN</u></strong></td>
                            <td style="width: 40%"></td>
                            <td class="valuekanan" style="width: 25%"><strong>TERKUMPUL (RM) (<asp:label runat="server" id="Label1"></asp:label>)</strong></td>
                            <td class="valuekanan" style="width: 25%"><strong>BULAN SEMASA (RM) (<asp:label runat="server" id="Label2"></asp:label>)</strong></td>
                        </tr>
                    </table>
                </div> <%--close header report--%>

                <Vot:Table runat="server" id="tblPendapatan" KodVotFrom="90000" KodVotTo ="99999" Tajuk ="PENDAPATAN" CustomClass ="tblPendapatan"/>
                <table>
                    <tr>
                        <td style="background-color:gray;width: 25%" class="tdbg"><strong>Untung / (Rugi) Kasar RM</strong></td>
                        <td style ="width: 25%"></td>
                        <td class="valuekanan bold" style="width: 25%" runat="server" id="Td1"></td>
                        <td class="valuekanan bold" style="width: 25%" runat="server" id="Td2"></td>
                    </tr>
                </table>
            </div>
            <%--close section pendapatan--%>


            <br />
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
                </div> <%--close section perbelanjaan--%>
  
                <Vot:Table runat="server" id="votEmolumen" KodVotFrom="10000" KodVotTo ="19000" Tajuk ="EMOLUMEN" CustomClass ="tblPerbelanjaan"/>
            </div>
            
            <br />
            <div class="vot-table-content">
                <Vot:Table runat="server" 
                id="votPerkhidmatan" 
                KodVotFrom="20000" 
                KodVotTo ="29999" 
                Tajuk ="PERKHIDMATAN DAN BEKALAN" 
                CustomClass ="tblPerbelanjaan" />
            </div>
            <br />

            <div class="vot-table-content">
                <Vot:Table runat="server" id="Table1" KodVotFrom="30000" KodVotTo ="39999" Tajuk ="ASET" CustomClass ="tblPerbelanjaan" />
            </div>

            <br />
            <div class="vot-table-content">
                <Vot:Table runat="server" id="Table2" KodVotFrom="40000" KodVotTo ="49999" Tajuk ="PEMBERIAN DAN BAYARAN TETAP" CustomClass ="tblPerbelanjaan" />
            </div>
            
            <br />
            <div class="vot-table-content">
                <Vot:Table runat="server" id="Table3" KodVotFrom="50000" KodVotTo ="59999" Tajuk ="PERBELANJAAN LAIN" CustomClass ="tblPerbelanjaan" />
                <table>
                    <tr>
                        <td style="width: 25%" class="tdbg"></td>
                        <td style ="width: 25%"></td>
                        <td class="valuekanan bold" style="width: 25%" runat="server" id="Td3"></td>
                        <td class="valuekanan bold" style="width: 25%" runat="server" id="Td4"></td>
                    </tr>
                </table>
            </div>

            <div class="vot-table-content">
                <table>
                    <tr>
                        <td style="background-color:gray;width: 25%" class="tdbg"><strong>Untung / (Rugi) Bersih RM</strong></td>
                        <td style ="width: 25%"></td>
                        <td class="valuekanan bold" style="width: 25%" runat="server" id="untungbersih1"></td>
                        <td class="valuekanan bold" style="width: 25%" runat="server" id="untungbersih2"></td>
                    </tr>
                </table>
            </div>
            
            <!-- Add the print button -->
            <br /><br />
            <%--<button onclick="return printContent()">print</button>--%>
                <asp:Button ID="printButton" runat="server" Text="Print" CssClass="print-button printButton" OnClick="Button1_Click"  OnClientClick=" printContent();" AutoPostBack="False" />
            <br /><br />
            <script>
                function printContent() {
                    // Hide the print button
                    //document.getElementById("printButton").style.display = "none";

                    // Print the contents of the masterdiv
                    window.print();
                    //printJS('masterdiv', 'html', "<link rel='stylesheet' type='text/css' href='StyleSheet1.css'>");
                    // Trigger autopostback
                    //__doPostBack('<'%= printButton.UniqueID %>', '');
                    return false;
                }
            </script>
        </div> <%--close master div--%>
    </form>

    <script type="text/javascript">
        var counter = 0;
        var totalAmaun = 0.00;
        var totalAmaun2 = 0.00;
        var totalPendapatan = document.querySelector(".tblPendapatan #totalAmauntableData span").innerHTML;
        var totalPendapatan2 = document.querySelector(".tblPendapatan #totalAmaun2tableData span").innerHTML;
        document.getElementById("Td1").innerHTML = totalPendapatan;
        document.getElementById("Td2").innerHTML = totalPendapatan2;

        //Kira total
        var obj = document.querySelectorAll(".tblPerbelanjaan #totalAmauntableData span");
        var obj2 = document.querySelectorAll(".tblPerbelanjaan #totalAmaun2tableData span");

        while (counter < obj.length) {
            totalAmaun += parseFloat(obj[counter].innerHTML.replaceAll(",", ""));
            totalAmaun2 += parseFloat(obj2[counter].innerHTML.replaceAll(",", ""));
            counter += 1;
        }

        document.getElementById("Td3").innerHTML = totalAmaun.toLocaleString("en-US")
        document.getElementById("Td4").innerHTML = totalAmaun2.toLocaleString("en-US")
        var untungBersih = parseFloat(totalPendapatan.replaceAll(",", "")) - totalAmaun;
        var untungBersih2 = parseFloat(totalPendapatan2.replaceAll(",", "")) - totalAmaun2;

        document.getElementById("untungbersih1").innerHTML = (untungBersih.toLocaleString("en-US"))
        document.getElementById("untungbersih2").innerHTML = untungBersih2.toLocaleString("en-US")


        if (window.addEventListener) {
            window.print();
        } else {
            window.print();
        }
    </script>
</body>
</html>