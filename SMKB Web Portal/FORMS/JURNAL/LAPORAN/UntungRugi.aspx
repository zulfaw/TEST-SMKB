<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="UntungRugi.aspx.vb" Inherits="SMKB_Web_Portal.UntungRugi" %>
<%@ Register Src="~/FORMS/JURNAL/LAPORAN/PrintReport/TableVot.ascx" TagPrefix="Vot" TagName="Table" %>

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


    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
    <%--        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>--%>
<%--    <script type="text/javascript" src="<%=ResolveClientUrl("~/Scripts/select2custom.min.js")%>"></script>--%>

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
    </style>





    <div id="PermohonanTab" class="tabcontent" style="display: block">

        <!-- Modal -->
        <div id="permohonan">
            <div>
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Laporan Untung Rugi</h5>

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
                                <label for="txtPerihal1" class="col-sm-2 col-form-label" style="text-align: right">Ptj :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="DropDownList1" runat="server" DataTextField="Pejabat" DataValueField="KodPejabat" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                       <%-- <br />--%>


                        <div class="form-row justify-content-center" style="display:none">
                            <div class="form-group row col-md-6">
                                <label for="kodKewangan" class="col-sm-2 col-form-label" style="text-align: right">Kod KW :</label>
                                <div class="col-sm-7">
                                    <div class="input-group">
                                        <asp:DropDownList ID="kodkw" runat="server" DataTextField="Butiran" DataValueField="Kod_Kump_Wang" class="ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="container">
                            <div class="row justify-content-center">
                                <div class="col-md-9">
                                    <div class="form-row">
                                        <div class="col-md-2 order-md-1">
                                            </div>
                                        <div class="col-md-3 order-md-3">
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

                                        <div class="col-md-3 order-md-2">
                                            <div class="form-group">
                                                <label for="txtNoRujukan1" style="display: block; text-align: center; width: 100%;">Tahun</label>
                                                <select id="ddlyear" runat="server" class="ui fluid search dropdown selection custom-select">
                                                    <option value="">-- Sila Pilih --</option>
                                                    <option value="2023">2023</option>
                                                    <option value="2022">2022</option>
                                                    <option value="2021">2021</option>
                                                    <option value="2020">2020</option>                                                   
                                                </select>
                                            </div>
                                        </div>



                                        <div class="col-md-3 order-md-4">
                                            <div class="form-group">
                                                <label></label>
                                                <button id="btnSearch" runat="server" class="btn btnSearch" onclick="return beginSearch();" type="button" style="margin-top: 33px">
                                                    <i class="fa fa-search"></i>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>



                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai_trans_rpt" class="table table-striped" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th scope="col">Vot</th>
                                            <th scope="col">Butiran</th>
                                            <th scope="col" >Jumlah Terkumpul (RM)</th>
                                            <th scope="col">Bulan Semasa (RM)</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_trans">
                                    </tbody>
                                   
                                </table>
                                <Vot:Table runat="server" id="tblPendapatan" KodVotFrom="90000" KodVotTo ="99999" CustomClass ="tblPendapatan"/>
                                <%--calculation below datatable--%>
                                <div style="margin-top: 20px;display:none;" id="displaybawahDT">  
                                    <table style="width: 30%; margin-right: 0; margin-left: auto;">
                                        <tr>
                                            <th style="width: 10%"></th>
                                            <th style="width: 5%">Terkumpul (RM)</th>
                                            <th style="width: 5%">Semasa (RM)</th>
                                        </tr>
                                        <tr>
                                            <td><strong>Pendapatan</strong></td>
                                            <td>
                                                <asp:Label runat="server" ID="pendapatan1"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="pendapatan2"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><strong>Emolumen</strong></td>
                                            <td>
                                                <asp:Label runat="server" ID="emo1"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="emo2"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><strong>Perkhidmatan dan Bekalan</strong></td>
                                            <td>
                                                <asp:Label runat="server" ID="pb1"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="pb2"></asp:Label></td>
                                        </tr>
                                       <tr>
                                            <td><strong>Aset</strong></td>
                                            <td>
                                                <asp:Label runat="server" ID="aset1"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="aset2"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><strong>Pemberian Dan Bayaran Tetap</strong></td>
                                            <td>
                                                <asp:Label runat="server" ID="pdbt1"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="pdbt2"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><strong>Perbelanjaan Lain</strong></td>
                                            <td>
                                                <asp:Label runat="server" ID="pl1"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="pl2"></asp:Label></td>
                                        </tr>

                                        <tr>
                                            <td><strong>Untung / Rugi Bersih</strong></td>
                                            <td>
                                                <asp:Label runat="server" ID="ub1"></asp:Label></td>
                                            <td>
                                                <asp:Label runat="server" ID="ub2"></asp:Label></td>
                                        </tr>
                                    </table>
                                </div>
                                 

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

</div>
    

        <script type="text/javascript">
 

            var tbl = null;
            var bulan = '';
            var thn = '' ;
            $(document).ready(function () {
                //Take the category filter drop down and append it to the datatables_filter div. 
                //You can use this same idea to move the filter anywhere withing the datatable that you want.
                //$("#tblDataSenarai_trans_rpt_filter.dataTables_filter").append($("#DropDownList2"));
                $(".ui.dropdown").dropdown({
                    fullTextSearch: true
                });

                tbl = $("#tblDataSenarai_trans_rpt").DataTable({
                    "responsive": true,
                    "searching": true,
                    cache: true,
                    dom: 'Bfrtip',
                    buttons: [
                        'csv', 'excel', {
                       // 'csv', 'excel', 'pdf', {

                            extend: 'print',
                            text: '<i class="fa fa-files-o green"></i> Print',
                            titleAttr: 'Print',
                            className: 'ui green basic button',
                            action: function (e, dt, button, config) {
<%--                                window.location.href = '<%=ResolveClientUrl("~/reportbaru4.aspx")%>';--%>
                                window.open('<%=ResolveClientUrl("~/FORMS/JURNAL/LAPORAN/PrintReport/reportpnl.aspx")%>', '_blank');

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
                    "ajax":
                    {
                        "url": "LejarPenghutangWS.asmx/LoadOrderRecord_SenaraiLulusTransaksiPNL",
                        type: 'POST',
                        data: function (d) {
                            return "{ bulan: '" + $('#<%=ddlMonths.ClientID%>').val() + "', tahun: '" + $('#<%=ddlyear.ClientID%>').val() + "',syarikat: '" + $('#<%=DropDownList2.ClientID%>').val() + "', ptj: '" + $('#<%=DropDownList1.ClientID%>').val() + "'}"
                        },
                        "contentType": "application/json; charset=utf-8",
                        "dataType": "json",
                        "dataSrc": function (json) {
                            return JSON.parse(json.d);
                        }

                    },
                    //for value below dataTables
                    "footerCallback": function (row, data, start, end, display) {
                        var totals = {
                            pendapatan: 0,
                            pendapatan2: 0,
                            emo: 0,
                            emo2: 0,
                            perkhidmatan: 0,
                            perkhidmatan2: 0,
                            aset: 0,
                            aset2: 0,
                            pemberian: 0,
                            pemberian2: 0,
                            pl: 0,
                            pl2: 0
                        };

                        for (var i = 0; i < data.length; i++) {
                            var kodvot = parseInt(data[i].kodvot);
                            var amaun = parseFloat(data[i].amaun);
                            var amaun2 = parseFloat(data[i].amaun2);

                            if (kodvot > 90000) {
                                totals.pendapatan += amaun;
                                totals.pendapatan2 += amaun2;
                            } else if (kodvot >= 10000 && kodvot <= 19999) {
                                totals.emo += amaun;
                                totals.emo2 += amaun2;
                            } else if (kodvot >= 20000 && kodvot <= 29999) {
                                totals.perkhidmatan += amaun;
                                totals.perkhidmatan2 += amaun2;
                            } else if (kodvot >= 30000 && kodvot <= 39999) {
                                totals.aset += amaun;
                                totals.aset2 += amaun2;
                            } else if (kodvot >= 40000 && kodvot <= 49999) {
                                totals.pemberian += amaun;
                                totals.pemberian2 += amaun2;
                            } else if (kodvot >= 50000 && kodvot <= 59999) {
                                totals.pl += amaun;
                                totals.pl2 += amaun2;
                            }
                        }

                        // Calculate the net profit
                        var netProfit = totals.pendapatan - (totals.emo + totals.perkhidmatan + totals.aset + totals.pemberian + totals.pl);
                        var netProfit2 = totals.pendapatan2 - (totals.emo2 + totals.perkhidmatan2 + totals.aset2 + totals.pemberian2 + totals.pl2);

                        // Format the totals with commas
                        for (var key in totals) {
                            totals[key] = totals[key].toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                        }
                        netProfit = netProfit.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                        netProfit2 = netProfit2.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });

                        // Update the HTML elements
                        document.getElementById("MainContent_FormContents_pendapatan1").innerHTML = totals.pendapatan;
                        document.getElementById("MainContent_FormContents_pendapatan2").innerHTML = totals.pendapatan2;
                        document.getElementById("MainContent_FormContents_emo1").innerHTML = totals.emo;
                        document.getElementById("MainContent_FormContents_emo2").innerHTML = totals.emo2;
                        document.getElementById("MainContent_FormContents_pb1").innerHTML = totals.perkhidmatan;
                        document.getElementById("MainContent_FormContents_pb2").innerHTML = totals.perkhidmatan2;
                        document.getElementById("MainContent_FormContents_aset1").innerHTML = totals.aset;
                        document.getElementById("MainContent_FormContents_aset2").innerHTML = totals.aset2;
                        document.getElementById("MainContent_FormContents_pdbt1").innerHTML = totals.pemberian;
                        document.getElementById("MainContent_FormContents_pdbt2").innerHTML = totals.pemberian2;
                        document.getElementById("MainContent_FormContents_pl1").innerHTML = totals.pl;
                        document.getElementById("MainContent_FormContents_pl2").innerHTML = totals.pl2;
                        document.getElementById("MainContent_FormContents_ub1").innerHTML = netProfit;
                        document.getElementById("MainContent_FormContents_ub2").innerHTML = netProfit2;
                    },

                    "columns": [
                      { "data": "kodvot", "width": "20%" },
                      { "data": "ButiranDetail", "width": "20%" },
                      { 
                        "data": "amaun", 
                        "width": "30%",
                        "render": function (data) {
                          return parseFloat(data).toLocaleString();
                        },
                        "className": "align-right" // Add CSS class for right alignment
                      },
                      { 
                        "data": "amaun2", 
                        "width": "30%",
                        "render": function (data) {
                          
                          return parseFloat(data).toLocaleString();
                        },
                        "className": "align-right" // Add CSS class for right alignment
                      }                    
                    ]
                });                

            });


            function beginSearch() {
                tbl.ajax.reload();

                document.getElementById("displaybawahDT").style.display = '';               
            }          
        </script>

</asp:Content>
