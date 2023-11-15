<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="WebUserControl1.ascx.vb" Inherits="SMKB_Web_Portal.WebUserControl1" %>

<div class="modal fade" id="PilihStafModal" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h5 class="modal-title" id="exampleModalCenterTitle">Pilih Staf Untuk Arahan Kerja</h5>
               <%-- <button type="button" class="close-modal-pilihstafmodal" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>--%>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                 <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                 </button>--%>
            </div>
            <div class="modal-body">
                <div class="col-md-12">
                    <div class="transaction-table table-responsive">
                        <table id="tblDataSenaraiStaf" class="table table-striped" style="width: 99%">
                            <thead>
                                <tr>
                                    <th scope="col">No. Staf</th>
                                    <th scope="col">Nama</th>
                                    <th scope="col">PTJ</th> 
                                    <th scope="col">Tindakan</th> 
                                </tr>
                            </thead>
                            <tbody id="tableID_Senarai_trans">
                            </tbody>                                                                         
                            </table>
                                <div class="modal-footer">
                                    <%-- <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>--%>
                                    <button type="button" runat="server" id="lbtnSimStafAK" class="btn btn-secondary btnSimpanSTPilihanStaf" > Simpan</button>
                                </div>
                    </div>
                </div>                  
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    var PilihanStafModule = (function () {
        var noArahan = "";
        var tblStaf = null;
        var selectedData = [];
        var fnclose = null;
        var fnopen = null;
      
        function setNoArahan(val, closefn) {
            noArahan = val;
          //  alert(noArahan);
            fnclose = closefn;
            $('#PilihStafModal').modal("show");
            refreshTable();
        }

        function refreshTable() {
            tblStaf.ajax.reload();
        }

        $('.close-modal-pilihstafmodal').click(function () {
            if (fnclose !== null) {
                fnclose();
            }
        });


        function closeModal() {
            //var modal = document.getElementById("tblDataSenaraiStaf");
            //modal.style.display = "none";
            $('#PilihStafModal').modal("hide");
        }


        $(document).on('change', '._check', function () {
            var checkedCheckboxes = $('._check:checked');

            if (checkedCheckboxes.length > 0) {
                selectedData = [];

                checkedCheckboxes.each(function () {
                    var data = $(this).closest('tr').find('td:first-child input').val();
                    selectedData.push(data);
                });

            }
        });

        $('.btnSimpanSTPilihanStaf').on('click', function () {
            if (selectedData.length > 0) {
                $.ajax({
                    url: 'Transaksi_EOTs.asmx/SimpanStafAK',
                    method: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify({ selectedData: selectedData, noarahan: noArahan}),
                    success: function (data) {
                        // Handle the success response
                        console.log('Success:', data.d);
                        var response = JSON.parse(data.d)                       
                        alert(response.Message);
                        tbl1.ajax.reload();
                        closeModal();
                     
                        $('#TransaksiStaf').modal('toggle');
                      
                        
                      
                       
                       
                    },
                    error: function (xhr, status, error) {
                        // Handle the error response
                        console.error('Error:', error);
                    }
                });
            }
        });

        tblStaf = $("#tblDataSenaraiStaf").DataTable({
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
                "url": "Transaksi_EOTs.asmx/LoadRecordStaf",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: function (d) {
                    return JSON.stringify(d)
                },
                "dataSrc": function (json) {
                    return JSON.parse(json.d);
                }

            },
            "columns": [
                {
                    "data": "MS01_NOSTAF",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }
                        var link = `<td style="width: 10%" >
                                                <label id="lblNo" name="lblNo" class="lblNo" value="${data}" >${data}</label>
                                                <input type ="hidden" class = "lblNo" value="${data}"/>
                                            </td>`;
                        return link;
                    }
                },
                { "data": "MS01_NAMA" },
                { "data": "Singkatan" },
                {
                    className: "text-center",
                    "data": "MS01_NOSTAF",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }

                        return '<input type="checkbox"  class="_check" name="check" value="' + data.id + '">';
                        this.width = "5%";
                    }

                }
            ]
        });

        return {
            NoArahan: setNoArahan
        }
    })();
</script>