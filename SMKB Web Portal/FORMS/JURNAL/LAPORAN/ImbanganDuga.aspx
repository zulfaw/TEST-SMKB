<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="ImbanganDuga.aspx.vb" Inherits="SMKB_Web_Portal.ImbanganDuga" %>
<%@ Register Src="~/FORMS/JURNAL/LAPORAN/PrintReport/TableImbanganDuga.ascx" TagPrefix="Vot" TagName="Table" %>

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
                        <h5 class="modal-title" id="exampleModalCenterTitle">Imbangan Duga</h5>

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
                        <br />


                        <div class="form-row justify-content-center">
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
                                            <th scope="col">Terkumpul Debit (RM)</th>
                                            <th scope="col">Terkumpul Kredit (RM)</th>
                                            <th scope="col">Semasa Debit (RM)</th>
                                            <th scope="col">Semasa Kredit (RM)</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_trans">
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


            var tbl = null;
            var bulan = '';
            var thn = '';
            $(document).ready(function () {

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
                                window.open('<%=ResolveClientUrl("~/FORMS/JURNAL/LAPORAN/PrintReport/reportImbanganDuga.aspx")%>', '_blank');

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
                        "url": "LejarPenghutangWS.asmx/LoadRecord_ImbanganDuga",
                        type: 'POST',
                        data: function (d) {
                            return "{ bulan: '" + $('#MainContent_FormContents_ddlMonths').val() + "', tahun: '" + $('#MainContent_FormContents_ddlyear').val() + "', kodkw: '" + $('#MainContent_FormContents_kodkw').val() + "', syarikat: '" + $('#MainContent_FormContents_DropDownList2').val() + "', ptj: '" + $('#MainContent_FormContents_DropDownList1').val() + "'}";
                        },
                        "contentType": "application/json; charset=utf-8",
                        "dataType": "json",
                        "dataSrc": function (json) {
                            return JSON.parse(json.d);
                        }

                    },

                    "columns": [
                        { "data": "kodvot", "width": "20%" },
                        { "data": "ButiranDetail", "width": "20%" },

                        {
                            "data": "amaunTerkumpulDebit",
                            "width": "15%",
                            "render": function (data) {
                                return parseFloat(data).toLocaleString();
                            },
                            "className": "align-right" 
                        },

                        {
                            "data": "amaunTerkumpulKredit",
                            "width": "15%",
                            "render": function (data) {
                                return parseFloat(data).toLocaleString();
                            },
                            "className": "align-right"
                        },

                        {
                            "data": "amaunSemasaDebit",
                            "width": "15%",
                            "render": function (data) {
                                return parseFloat(data).toLocaleString();
                            },
                            "className": "align-right" 
                        },


                        {
                            "data": "amaunSemasaKredit",
                            "width": "15%",
                            "render": function (data) {

                                return parseFloat(data).toLocaleString();
                            },


                            "className": "align-right" 
                        }


                    ]
                });

            });


            function beginSearch() {
                tbl.ajax.reload();
            }
        </script>

</asp:Content>
