<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Cetak_EOT.aspx.vb" Inherits="SMKB_Web_Portal.Cetak_EOT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">


    

    <div id="PermohonanTab" class="tabcontent" style="display: block">

        <!-- Modal -->
        <div id="permohonan">
            
                <div class="modal-content" >
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Permohonan Tuntutan Elaun Lebih Masa </h5>

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
                                            <th scope="col">Hantar &nbsp&nbsp&nbsp Cetak</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_Senarai_trans">

                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <hr />
                   
                </div>
        
    </div>
 

<script type="text/javascript">
    var tbl = null;
    var tbl1 = null;
    $(document).ready(function () {
       
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
                "url": "Transaksi_EOTs.asmx/LoadSenEOTHantaCetak",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: function () {
                    return JSON.stringify({ NoStaf: '<%=Session("ssusrID")%>' });
                },
                "dataSrc": function (json) {
                    return JSON.parse(json.d);
                }

            },
            "rowCallback": function (row, data) {

                // Add hover effect
                $(row).hover(function () {
                    $(this).addClass("hover pe-auto bg-warning");
                }, function () {
                    $(this).removeClass("hover pe-auto bg-warning");
                });

                // Add click event
                $(row).on("click", function () {
                    console.log(data);
                   /* rowClickHandler(data);*/
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
                { "data": "AmaunTuntut" },
                {
                    className: "btnView",
                    "data": "No_Mohon",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }

                        var link = `<button id="btnView1"  data-id = "${data}" class="btn btnView" type="button" style="color: blue">
                                                    <i class="fa fa-edit"></i>

                                        </button>
                                     <button id="btnView2"  data-id = "${data}" class="btn btnView" type="button" style="color: blue">
                                                    <i class="fa fa-print"></i>

                                        </button>`;
                        return link;
                    }
                }
            ]
        });

       
    });
    var tableID_Senarai = "#tblDataSenarai_trans";
  
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


    $(tblDataSenarai_trans).on('click', '#btnView1', async function () {

        event.preventDefault();
       
        var bool = true;
      
        var id2 = $(this).attr("data-id")
      
        if (id2 !== "") {

            var newEot = {
                MohonEOT: {
                    No_Mohon: id2,

                }
            }

            var recordHdr = await AjaxHantarEot(newEot);
            console.log(recordHdr);
        
        }

        return false;
    })

    async function AjaxHantarEot(MohonEOT) {

        $.ajax({

            url: 'Transaksi_EOTs.asmx/HantarEOT',
            method: 'POST',
            data: JSON.stringify(MohonEOT),

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var response = JSON.parse(data.d)
                alert(response.Message)


            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error:', errorThrown);
                reject(false);
            }

        });
    }


    $(tblDataSenarai_trans).on('click', '#btnView2', async function () {

        event.preventDefault();

        var bool = true;

        var id2 = $(this).attr("data-id")

        if (id2 !== "") {

            var newEot = {
                MohonEOT: {
                    No_Mohon: id2,

                }
            }

            var recordHdr = await AjaxCetakEot(newEot);
            console.log(recordHdr);

        }

        return false;
    })

    async function AjaxCetakEot(MohonEOT) {

        $.ajax({

            url: 'Transaksi_EOTs.asmx/CetakEOT',
            method: 'POST',
            data: JSON.stringify(MohonEOT),

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var response = JSON.parse(data.d)
                alert(response.Message)


            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error:', errorThrown);
                reject(false);
            }

        });
    }

</script>
        </div>







</asp:Content>
