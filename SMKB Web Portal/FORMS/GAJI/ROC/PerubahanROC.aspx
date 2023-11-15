<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PerubahanROC.aspx.vb" Inherits="SMKB_Web_Portal.PerubahanROC" %>
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

       <div class="form-row">
            <div class="form-group col-md-3">
                <label>Bulan/Tahun Gaji</label>
                <asp:TextBox runat="server" ID="txtBlnThn" CssClass="form-control" Enabled="false" Style="width: 50%;" ></asp:TextBox>
            </div>

        </div>
                <div class="card w-20" style="width: 30rem;">
              <h6 class="card-header">Rumusan</h6>
              <div class="card-body">
                  Bil. ROC : <label id="jumroc"></label>&nbsp;&nbsp;&nbsp;&nbsp; Bil. Gaji : <label id="jumgaji">&nbsp;&nbsp;&nbsp;&nbsp;</label> Bil. Elaun : <label id="jumelaun"></label>
               
              </div>
            </div>

        <div class="form-row">
                     <div class="table-title">
            <h6>Paparan Perubahan ROC Telah Diproses</h6>
            <hr>
        </div>
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

    <!-- Modal -->
                <div class="modal fade" id="infostaf" tabindex="-1" role="dialog"
                    aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="eMCTitle">Maklumat ROC</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div class="modal-body">
                                <h6 style="color:blue">Maklumat Butiran Staf</h6>
                 <hr>
        
                                    <div class="form-row">
                                        <div class="form-group col-md-4">
<%--                                            <label for="idStaf" class="col-form-label">No.Permohonan:</label>
                                            <input type="text" class="form-control input-md" id="idStaf" style="background-color:#f3f3f3" >--%>
                                            <label>No. Staf</label>
                                            <asp:TextBox runat="server" ID="txtNoStaf" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                             
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>No. KP</label>
                                           <asp:TextBox runat="server" ID="txtNoKp" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Gaji Pokok</label>
                                            <asp:TextBox runat="server" ID="txtGjPokok" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group col-md-4">
                                            <label>Nama</label>
                                            <asp:TextBox runat="server" ID="txtNama" TextMode="MultiLine" Rows="2" Enabled="false"  CssClass="form-control" Style="width: 100%;" ></asp:TextBox>
                                             
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>Jawatan</label>
                                           <asp:TextBox runat="server" ID="txtJwtn" TextMode="MultiLine" Rows="2" Enabled="false"  CssClass="form-control" Style="width: 100%;" ></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Pejabat</label>
                                            <asp:TextBox runat="server" ID="txtPjbt" TextMode="MultiLine" Rows="2" Enabled="false" CssClass="form-control" Style="width: 100%;" ></asp:TextBox>
                                             
                                        </div>

                                    </div>
                                    <div class="form-row">
                                       <div class="form-group col-md-4">
                                            <label>Gred Gaji</label>
                                            <asp:TextBox runat="server" ID="txtGred" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>No. Gaji</label>
                                           <asp:TextBox runat="server" ID="txtNoGaji" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>Skim</label>
                                           <asp:TextBox runat="server" ID="txtSkim" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                        </div>

                                    </div>

                                    <h6 style="color:blue">Maklumat Butiran ROC</h6>
                                    <hr>
                                        <div class="form-row">
                                            <div class="form-group col-md-4">
                                                <label>No. ROC</label>
                                                <asp:TextBox runat="server" ID="txtNoRoc" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                             
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label>Tarikh ROC</label>
                                               <asp:TextBox runat="server" ID="txtTkhRoc" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                            </div>
                                            <div class="form-group col-md-4">
                                                <label>No. Ruj. Surat</label>
                                                <asp:TextBox runat="server" ID="txtNoRuj" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                            </div>
                                        </div>
                                       <div class="form-row">
                                            <div class="form-group col-md-4">
                                                <label>Jenis Perubahan</label>
                                                <asp:TextBox runat="server" ID="txtJenis" TextMode="MultiLine" Rows="2" Enabled="false" CssClass="form-control" Style="width: 100%;" ></asp:TextBox>
                                             
                                            </div>

                                            <div class="form-group col-md-8">
                                                <label>Keterangan</label>
                                               <asp:TextBox runat="server" ID="txtKeterangan" TextMode="MultiLine" Rows="2" Enabled="false" CssClass="form-control" Style="width: 100%;" ></asp:TextBox>
                                            </div>
        
                                        </div>
 
                                    <hr>  

                                        <div class="row">
                                        <div class="col-md-12">
                                            <h6 style="color:blue">Senarai Butiran Terperinci ROC</h6>
                                        <div class="transaction-table table-responsive">
                                        <table id="tblListDtROC" class="table table-striped" style="width:100%">
                                            <thead>
                                                <tr>
                                                     <th scope="col" style="width:5px">Bil</th>
                                                    <th scope="col" style="width:100px">Butiran</th>
                                                    <th scope="col" style="width:50px">Tarikh Mula</th>
                                                    <th scope="col" style="width:50px">Tarikh Tamat</th>
                                                     <th scope="col" style="width:30px">Jumlah (RM)</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tableID_ListDtROC">
                                        
                                            </tbody>

                                        </table>
                                        </div>
                                        </div>
                                        </div> 
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="$('#infostaf').modal('hide'); return false;">Tutup</button>
                                
                            </div>
                        </div>
                    </div>
         </div>
            <!-- End Modal -->

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
       
        function ShowPopup(obj) {
            $('#infostaf').modal('toggle');

            getInfoStaf($(obj).text())


        }
        function getRumusROC() {
            //Cara Pertama

            fetch('ROC_WS.asmx/LoadRumusSahROC', {
                method: 'POST',
                headers: {
                    'Content-Type': "application/json"
                },
            //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
            body: JSON.stringify({
                tahun: 2023,
                bulan: 6
            })
        })
                .then(response => response.json())
                .then(data => setRumusROC(data.d))

        }
        function setRumusROC(data) {
            data = JSON.parse(data);

            if (data.totroc === "") {
                alert("Tiada data");
                return false;
            }
            $('#jumroc').html(data[0].totroc);
            $('#jumgaji').html(data[0].totgaji);
            $('#jumelaun').html(data[0].totelaun);


        }
        function getInfoStaf(nostaf) {
            //Cara Pertama

            fetch('ROC_WS.asmx/LoadRekodStaf', {
                method: 'POST',
                headers: {
                    'Content-Type': "application/json"
                },
            //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
            body: JSON.stringify({ nostaf: nostaf })
        })
                .then(response => response.json())
                .then(data => setInfoStaf(data.d))

        }

        function setInfoStaf(data) {
            data = JSON.parse(data);
            if (data.MS01_NoStaf === "") {
                alert("Tiada data");
                return false;
            }

        $('#<%=txtNoStaf.ClientID%>').val(data[0].MS01_NoStaf);
        $('#<%=txtNoKp.ClientID%>').val(data[0].MS01_KpB); 
        $('#<%=txtNama.ClientID%>').val(data[0].MS01_Nama); 
        $('#<%=txtJwtn.ClientID%>').val(data[0].JawatanS); 
        $('#<%=txtGred.ClientID%>').val(data[0].gredgajis); 
        $('#<%=txtPjbt.ClientID%>').val(data[0].PejabatS);

        }
        function getBlnThn() {
            //Cara Pertama

            fetch('ROC_WS.asmx/LoadBlnThnGaji', {
                method: 'POST',
                headers: {
                    'Content-Type': "application/json"
                },
            //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
            body: JSON.stringify()
        })
            .then(response => response.json())
            .then(data => setBlnThn(data.d))

    }
    function setBlnThn(data) {
        
        data = JSON.parse(data);
      
        if (data.bulan === "") {
            alert("Tiada data");
            return false;
        }
        
        $('#<%=txtBlnThn.ClientID%>').val(data[0].butir);

    }
    </script>
</asp:Content>
