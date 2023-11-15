<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Sokong_EOT.aspx.vb" Inherits="SMKB_Web_Portal.Sokong_EOT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

   
   <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>--%>
    

    <div id="PermohonanTab" class="tabcontent" style="display: block">

        <!-- Modal -->
        <div id="permohonan">
            
                <div class="modal-content" >
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Permohonan Tuntutan Elaun Lebih Masa Yang Belum Diluluskan</h5>

                    </div>
                    <div class=" -body">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai_trans" class="table table-striped" style="width: 95%">
                                    <thead>
                                        <tr>
                                            <th scope="col">No. Permohonan</th>
                                            <th scope="col">No. Staf</th>
                                            <th scope="col">Nama</th>
                                            <th scope="col">Tarikh Mohon</th>
                                            <th scope="col">Jumlah Jam Tuntut</th>
                                            <th scope="col">Jumlah (RM)</th> 
                                          <%--  <th scope="col">Tindakan</th>--%>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_trans">

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle2">Senarai Permohonan Tuntutan Elaun Lebih Masa Yang Telah Diluluskan</h5>

                    </div>
                    <div class=" -body">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai_trans_Lulus" class="table table-striped" style="width: 95%">
                                    <thead>
                                        <tr>
                                            <th scope="col">No. Permohonan</th>
                                            <th scope="col">No. Staf</th>
                                            <th scope="col">Nama</th>
                                            <th scope="col">Tarikh Mohon</th>
                                            <th scope="col">Jumlah Jam Tuntut</th>
                                            <th scope="col">Jumlah (RM)</th> 
                                            
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_trans_Lulus">

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
        
    </div>
 

<script type="text/javascript">
    var tbl = null;
    var tbl1 = null;
    $(document).ready(function () {
        show_loader();
        tbl = $("#tblDataSenarai_trans").DataTable({
            "responsive": true,
            "searching": true,
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
                "sInfoEmpty": "Menunjukkan 0 ke 0 dari rekod",
                "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
                "sEmptyTable": "Tiada rekod.",
                "sSearch": "Carian"
            },
            "ajax": {
                "url": "Transaksi_EOTs.asmx/LoadSenEOTSokongKJ",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: function () {
                    return JSON.stringify({ NoPegSah: '<%=Session("ssusrID")%>' });
                },
                "dataSrc": function (json) {
                    return JSON.parse(json.d);
                }

            },
            "rowCallback": function (row, data) {
                close_loader();
                // Add hover effect
                $(row).hover(function () {
                    $(this).addClass("hover pe-auto bg-warning");
                }, function () {
                    $(this).removeClass("hover pe-auto bg-warning");
                });

                // Add click event
                $(row).on("click", function () {
                    console.log(data);
                    rowClickHandler(data);
                });

            },
            "columns": [
                {
                    "data": "No_Mohon",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }
                        var link = `<td style="width: 10%" >
                                                <label id="lblNoArahan" name="lblNoArahan" class="lblNoArahan" value="${data}" >${data}</label>
                                                <input type ="hidden" class = "lblNoArahan" value="${data}"/>
                                            </td>`;
                        return link;
                    }
                },
                { "data": "No_Staf" },
                { "data": "Nama" },
                { "data": "Tkh_Mohon" },
                { "data": "Jam" },
                { "data": "AmaunTuntut" }
                //{
                //    className: "btnView",
                //    "data": "No_Mohon",
                //    render: function (data, type, row, meta) {

                //        if (type !== "display") {

                //            return data;

                //        }

                //        var link = `<button id="btnView" runat="server" data-id = "${data}" class="btn btnView" type="button" style="color: blue">
                //                                    <i class="fa fa-edit"></i>
                //                        </button>`;
                //        return link;
                //    }
                //}
            ]
        });

        tbl1 = $("#tblDataSenarai_trans_Lulus").DataTable({
            "responsive": true,
            "searching": true,
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
                "sInfoEmpty": "Menunjukkan 0 ke 0 dari rekod",
                "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
                "sEmptyTable": "Tiada rekod.",
                "sSearch": "Carian"
            },
            "ajax": {
                "url": "Transaksi_EOTs.asmx/LoadSenEOTlulusKJ",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: function () {
                    return JSON.stringify({ NoPegSah: '<%=Session("ssusrID")%>' });
                },
                "dataSrc": function (json) {
                    return JSON.parse(json.d);
                }

            },
           
            "columns": [
                {
                    "data": "No_Mohon",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }
                        var link = `<td style="width: 10%" >
                                                <label id="lblNoArahan1" name="lblNoArahan1" class="lblNoArahan1" value="${data}" >${data}</label>
                                                <input type ="hidden" class = "lblNoArahan1" value="${data}"/>
                                            </td>`;
                        return link;
                    }
                },
                { "data": "No_Staf" },
                { "data": "Nama" },
                { "data": "Tkh_Mohon" },
                { "data": "Jam" },
                { "data": "AmaunTuntut" }
             
            ]
        });
    });
    var tableID_Senarai = "#tblDataSenarai_trans";
    var tableID_SenaraiLulus = "#tblDataSenarai_trans_Lulus";
    var searchQuery = "";
    var oldSearchQuery = "";
    var objMetadata = [{
        "obj1": {
            "id": "",
            "oldSearchQurey": "",
            "searchQuery": ""
        }
    }, {
        "obj2": {
            "id": "",
            "oldSearchQurey": "",
            "searchQuery": ""
        }
        }]

    function rowClickHandler(orderDetail) {
        event.preventDefault();

        var id1 = orderDetail.No_Mohon;

        msg = "Anda pasti ingin menyokong  permohonan " + id1 + " ini?"

        if (!confirm(msg)) {
            return false;
        }

        AjaxSokongEot(id1);


    }

    async function AjaxSokongEot(id1) {

        try {

            const response = await fetch('Transaksi_EOTs.asmx/LoadSokongEOT', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: id1 })
            });
            const data = await response.json();
            //return JSON.parse(data.d);
            var result = JSON.parse(data.d);
            alert(result.Message)
            tbl.ajax.reload();
            tbl1.ajax.reload();

        } catch (error) {
            console.error('Error:', error);
            return false;
        }

    }
</script>
        </div>
</asp:Content>




