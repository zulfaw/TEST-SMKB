<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Perubahan_ROC.aspx.vb" Inherits="SMKB_Web_Portal.Perubahan_ROC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
     <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>
        <script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/buttons/2.3.6/js/dataTables.buttons.min.js" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.html5.min.js" crossorigin="anonymous"></script>
        <script src="https://cdn.datatables.net/buttons/2.3.6/js/buttons.print.min.js" crossorigin="anonymous"></script>
    <script type="text/javascript">

        $(function () {
            $(".grid").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
                "searching": false,
                dom: 'Bfrtip',
                buttons: [
                    'csv', 'excel', 'pdf', 'print'
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
                    "sInfoEmpty": "Menunjukkan 0 ke 0 dari rekod",
                    "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
                    "sEmptyTable": "Tiada rekod.",
                    "sSearch": "Carian"
                }
            });

        })

        function Search_Gridview(strKey, strGV) {
            var strData = strKey.value.toLowerCase().split(" ");        
            var tblData = document.getElementById(strGV);

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

        function SaveSucces() {
             $('#MessageModal').modal('toggle');

        }

        function ShowPopup(elm) {    
          
            if (elm == "1") {
                $('#permohonan').modal('toggle');

            }
            else if (elm == "2") {   
               $(".modal-body input").val("");   
              
                $('#permohonan').modal('toggle');
            }
           
          
        }
    </script>
    <style type="text/css">
        .hideGridColumn {
            display: none;
        }
    </style>

            <div id="PermohonanTab" class="tabcontent" style="display:block">
			        <div class="search-filter">
                        <div class="form-row">
                            <div class="form-group row col-md-4">
                                <label for="inputEmail3" class="col-sm-2 col-form-label">Bulan/Tahun:</label>
                                <div class="col-sm-6">
                                    <div class="input-group">
                                        <asp:DropDownList ID="ddlBulan" runat="server" AutoPostBack="True" CssClass="form-control" Width="100px">
                                        </asp:DropDownList>
                                        <div class="input-group-append">
                                            <button class="btn btn-outline" type="button"><i
                                                    class="fa fa-search"></i>Cari</button>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-title">
                        <h6>Laporan Perubahan ROC Gaji\Elaun</h6>

                        <%--<div class="btn btn-primary" data-toggle="modal" data-target="#permohonaninvois"><i class="fa fa-plus"></i> Tambah
                            Permohonan
                        </div>--%>
                    </div>

            <div class="box-body" >               
                <asp:GridView ID="gvKod"  CssClass="table table-bordered table-striped grid" Width="100%" runat="server"
                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true"  >
                    <Columns>
                       <asp:BoundField DataField="no_staf" HeaderText="No. Staf">
                            <ItemStyle Width="3%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ms01_nama" HeaderText="Nama">
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:BoundField> 
                        <asp:BoundField DataField="kod_trans" HeaderText="Kod Transaksi">
                            <ItemStyle Width="15%" HorizontalAlign="Left" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="jumlama" HeaderText="Amaun Lama (RM)">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:BoundField>  
                       <asp:BoundField DataField="jumbaru" HeaderText="Amaun Baru (RM)">
                            <ItemStyle Width="8%" HorizontalAlign="Right" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="MS15_Keterangan" HeaderText="Keterangan">
                            <ItemStyle Width="30%" HorizontalAlign="Left" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="MS15_NoRoc" HeaderText="No. ROC">
                            <ItemStyle Width="5%" HorizontalAlign="Left" />
                        </asp:BoundField>  

                    </Columns>
                </asp:GridView>                    
            </div>


</div>
                    
</asp:Content>
