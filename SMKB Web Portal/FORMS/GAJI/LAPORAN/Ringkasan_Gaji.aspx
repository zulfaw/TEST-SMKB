<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Ringkasan_Gaji.aspx.vb" Inherits="SMKB_Web_Portal.Ringkasan_Gaji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
      <script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.html5.min.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.print.min.js" crossorigin="anonymous"></script>

<div id="PermohonanTab" class="tabcontent" style="display: block">
        <div class="table-title">
            <h6>Laporan Ringkasan Gaji</h6>
            <hr>
        </div>
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
        <div class="form-row">
             
            <div class="col-md-12">
                
                <div class="transaction-table table-responsive">
                    
                    <table id="tblListROC" class="table table-striped" style="width:100%">
                        <thead>
                            <tr>
                                <th scope="col" style="width:30px">No. Staf</th>
                                <th scope="col" style="width:100px">Nama</th>
                                <th scope="col" style="width:100px">Kod Transaksi</th>
                                <th scope="col" style="width:50px">Amaun Lama (RM)</th>
                                <th scope="col" style="width:50px">Amaun Baru (RM)</th>
                                <th scope="col" style="width:200px">Keterangan</th>
                                <th scope="col" style="width:50px">No. ROC</th>
                            </tr>
                        </thead>
                        <tbody id="tableID_ListROC">
                                        
                        </tbody>

                    </table>
                </div>
            </div>                  
        </div>


</div>
    <script type="text/javascript">
        

        var tbl = null

        $(document).ready(function () {
            show_loader();
            getBlnThn();
            getRumusROC();
            tbl = $("#tblListROC").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copy', 'csv', 'excel', 'pdf', 'print'
                ],
                "responsive": true,
                "searching": true,
                "bLengthChange": false,
                "sPaginationType": "full_numbers",
                "pageLength": 10,
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
                    "sInfoEmpty": "Menunjukkan 0 ke 0 dari rekod",
                    "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
                    "sEmptyTable": "Tiada rekod.",
                    "sSearch": "Carian"
                },
                "ajax": {
                    "url": "ROC_WS.asmx/LoadListUbahROC",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: function () {
                    return JSON.stringify()
                },
                "dataSrc": function (json) {
                    //var data = JSON.parse(json.d);
                    //console.log(data.Payload);
                    return JSON.parse(json.d);
                }
                },
                drawCallback: function (settings) {
                    // Your function to be called after loading data
                    close_loader();
                },
            "columns": [
                //{
                //    "data": "no_staf",
                //    render: function (data, type, row, meta) {

                //        if (type !== "display") {

                //            return data;

                //        }

                //        var link = `<td style="width: 10%" >
                //                            <label id="lblNo" name="lblNo"  class="lblNo" value="${data}" ><a id="myLink" class="yourButton" href="#" onclick="ShowPopup(this);">${data}</a></label>
                //                            <input type ="hidden" class = "lblNo" value="${data}"/>
                //                        </td>`;
                //        return link;
                //    } },
                { "data": "no_staf" },
                { "data": "ms01_nama" },
                { "data": "kod_trans" },
                { "data": "jumlama", className: "text-right", render: $.fn.dataTable.render.number(',', '.', 2, '') },
                { "data": "jumbaru", className: "text-right", render: $.fn.dataTable.render.number(',', '.', 2, '') },
                { "data": "MS15_Keterangan" },
                { "data": "MS15_NoRoc", className: "text-center" }
                
            ]

             });
       
        });
       





    </script>
</asp:Content>
