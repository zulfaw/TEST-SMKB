<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CetakJadualBayarBalik.aspx.vb" Inherits="SMKB_Web_Portal.CetakJadualBayarBalik" %>
<%@ Register Src="~/FORMS/PINJAMAN/LAPORAN/tableBayarBalik.ascx" TagPrefix="AC" TagName="Table" %>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <%--<link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />--%>
    <%--<link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />--%>
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>
   
    <link rel="stylesheet" href="../../../Content/datatables/buttons.dataTables.min.css">

    <script src='../../../Scripts/datatables/dataTables.buttons.min.js' ></script>
    <script src ='../../../Scripts/datatables/jszip.min.js'></script>
    <script src='../../../Scripts/datatables/pdfmake.min.js'></script>
    <script src='../../../Scripts/datatables/vfs_fonts.js'></script>
    <script src='../../../Scripts/datatables/buttons.html5.min.js'></script>


<style>

            body {
        font - family: Arial, sans - serif;
    }

</style>

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

        .plabel {
            font-size: 14px;
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
            width: 10px;
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

    .auto-style1 {
        width: 225px;
    }
    .auto-style2 {
        width: 498px;
    }
    .auto-style3 {
        width: 331px;
    }

    /*.my-div {
  margin-top: 10px;
  margin-right: 20px;
  margin-bottom: 30px;
  margin-left: 40px;
}*/

    

    .table-content .tblByrBalikPinjaman tr.bold {
  font-weight: bold;
}

     table {
            width: 75%; /* Set the table width to 75% of its container */
            border-collapse: collapse; /* Optional: This removes the spacing between table cells */
        }


        .pheader {
/*            text-align: center;
*/            font-size: 14px;
            font-weight: bold;
            margin-top:0px!important;
            margin-bottom:0px!important;
        }


        .pheader2 {
/*            text-align: left;
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

    </style>



     <div id="headerreport">
                <table>
                    <tr>
                        <td style="width: 20%;text-align:right">
                            <asp:Image ID="imgMyImage" runat="server" style="width:140px; Height:80px;text-align:right"/>
                        </td>
                        <td style="width: 50%;text-align:left">
                            <p class="pheader2"><strong><asp:label ID="lblNamaKorporat" runat="server"></asp:label></strong></p>
                            <p class="pheader2"><asp:label ID="lblAlamatKorporat" runat="server"></asp:label></p>
                            <p class="pheader2"><asp:label ID="lblNoTelFaks" runat="server"></asp:label></p>
                            <p class="pheader2"><asp:label ID="lblEmailKorporat" runat="server"></asp:label></p>

                        </td>
                        <td style="width: 30%; text-align: right">
                            <span class="ptarikh">Tarikh : <%= DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") %></span><br />
                            <span class="ptarikh">Laporan : PJM001</span><br />
                            <span class="ptarikh">Pengguna : <%= Session("ssusrID") %></span>

                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="text-align:center">
                            <br />
                          <p class="pheader">PENYATA KEWANGAN BAYARAN BALIK PEMBIAYAAN KENDERAAN UTeM</p>
                          <p class="pheader">(RUJUKAN PEMINJAM) <asp:label ID="lblNoPinjaman" runat="server"></asp:label></p>
                        </td>
                         <td style="width: 25%"></td>

                    </tr>
                </table>
            </div> <%--close header report--%>
<br /><br />

        <div id="contentreport">
            <table>
                <tr><b></b>
                    <td class="plabel"><b>Nama</b></td>
                  
                    <td class="auto-style2">&nbsp;<b>:<asp:Label ID="lblNama" runat="server"></asp:Label></b></td>
                    <td class="auto-style3"><b>No Jilid</b></td>
                    
                    <td>&nbsp;<b>:<asp:Label ID="lblNoJilid" runat="server"></asp:Label></b></td>
                </tr>
                <tr>
                    <td class="auto-style1"><b>No Pembiayaan</b></td>
                   
                    <td class="auto-style2">&nbsp;<b>:<asp:Label ID="lblNoPinj" runat="server"></asp:Label></b></td>
                    <td class="auto-style3"><b>Jumlah</b></td>
                    
                    <td>&nbsp;:<b><asp:Label ID="lblJumlah" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td class="auto-style1"><b>No Pekerja</b></td>
                    
                    <td class="auto-style2">&nbsp;:<b><asp:Label ID="lblNoStaf" runat="server"></asp:Label></b></td>
                    <td class="auto-style3"><b>Tempoh Bayaran</b> </td>
                    
                    <td>&nbsp;:<b><asp:Label ID="lblTempoh" runat="server"></asp:Label></b></td>
                </tr>
                <tr>
                    <td class="auto-style1"><b>Ptj / Fakulti</b></td>
                    
                    <td class="auto-style2">&nbsp;:<b><asp:Label ID="lblNamaPTj" runat="server"></asp:Label></b></td>
                    <td class="auto-style3"><b>Keuntungan</b></td>
                    
                    <td>&nbsp;:<b><asp:Label ID="lblKeuntungan" runat="server"></asp:Label></b></td>
                </tr>
            </table>
        </div>


<button id="Button1" runat="server" class="btn btnPrint" onclick="CreatePDFfromHTML()" type="button" style="margin-top: 33px">
                    <i class="fa fa-print"></i>
                </button>
        

        

        <div class="table-content" style ="width:80%;margin:0 auto;">
            <AC:table runat="server" id="tblByrBalikPinjaman" Tajuk ="SENARAI JADUAL BAYAR BALIK" CustomClass ="tblByrBalikPinjaman table table-bordered " />
        </div>

 




