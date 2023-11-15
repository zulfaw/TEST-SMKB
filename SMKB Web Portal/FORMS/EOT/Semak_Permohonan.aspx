<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Semak_Permohonan.aspx.vb" Inherits="SMKB_Web_Portal.Semak_Permohonan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    
    <div id="PermohonanTab" class="tabcontent" style="display: block">

        <!-- Modal -->
        <div id="permohonan">
            
                <div class="modal-content" >
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Semakan Permohonan Tuntutan kerja Lebih Masa</h5>

                    </div>
                    <div class=" -body">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai_trans" class="table table-striped" style="width: 95%">
                                    <thead>
                                        <tr>

                                            <th scope="col">No. Permohonan</th>
                                            <th scope="col">No. Arahan</th>
                                            <th scope="col">No. Staf</th>
                                            <th scope="col">Nama</th>
                                            <th scope="col">Tarikh Mohon</th>
                                            <th scope="col">Status</th> 
                                            
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
 

<script type="text/javascript">
    var tbl = null;
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
                "url": "Transaksi_EOTs.asmx/LoadSemakanPermohonan",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
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
                                                <label id="lblNoArahan" name="lblNoArahan" class="lblNoArahan" value="${data}" >${data}</label>
                                                <input type ="hidden" class = "lblNoArahan" value="${data}"/>
                                            </td>`;
                        return link;
                    }
                },
                { "data": "No_Arahan" },
                { "data": "No_Staf" },
                { "data": "MS01_nama" },
                { "data": "Tkh_Mohon" },
                { "data": "Butiran" },
                
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
</script>
        </div>




</asp:Content>
