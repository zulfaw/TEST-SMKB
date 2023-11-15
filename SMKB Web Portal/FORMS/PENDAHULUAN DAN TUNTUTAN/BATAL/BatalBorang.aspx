<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="BatalBorang.aspx.vb" Inherits="SMKB_Web_Portal.BatalBorang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

 <!-- Create the dropdown filter -->
                    <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
                                <label for="inputEmail3" class="col-sm-2 col-form-label" style="text-align:right">Carian :</label>
                                <div class="col-sm-8">
                                    <div class="input-group">
                                        <select id="categoryFilter" class="custom-select" >
                                            <option value="">SEMUA</option>
                                            <option value="1" selected="selected">Hari Ini</option>
                                            <option value="2">Semalam</option>
                                            <option value="3">7 Hari Lepas</option>
                                            <option value="4">30 Hari Lepas</option>
                                            <option value="5">60 Hari Lepas</option>
                                            <option value="6">Pilih Tarikh</option>
                                        </select>
                                         <button id="btnSearch" runat="server" class="btn btnSearch" type="button">
                                                <i class="fa fa-search"></i>
                                            </button>
                                    </div>
                                </div>
                           <div class="col-md-5">
                                    <div class="form-row">
                                         <div class="form-group col-md-5">
                                           <br />
                                        </div>
                                        </div>
                               </div>
                                <div class="col-md-12">
                                    <div class="form-row">
                                        <div class="form-group col-md-2">
                                            <label id="lblMula" style="text-align: right;display:none;"  >Mula: </label>
                                        </div>                                        
                                        <div class="form-group col-md-4">
                                            <input type="date" id="txtTarikhStart" name="txtTarikhStart" style="display:none;" class="form-control date-range-filter">
                                        </div>                                        
                                        <div class="form-group col-md-2">
                                            <label id="lblTamat" style="text-align: right;display:none;" >Tamat: </label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <input type="date" id="txtTarikhEnd" name="txtTarikhEnd" style="display:none;" class="form-control date-range-filter">
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
  <%-- tutup filtering--%>

     <div class="modal-body">
                        <div class="col-sm-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai" class="table table-striped" style="width:100%">
                                    <thead>
                                        <tr>
                                            <th scope="col" width="10%">No. Permohonan</th>
                                            <th scope="col" width="10%">Tarikh Mohon</th>
                                            <th scope="col" width="20%">Jenis Permohonan</th>                                            
                                            <th scope="col" width="30%">Tujuan</th>
                                            <th scope="col" width="10%">Tarikh Mula</th>
                                            <th scope="col" width="10%">Tarikh Tamat</th>
                                            <th scope="col" width="10%">Jumlah Mohon (RM)</th>

                                        </tr>
                                    </thead>
                                    <tbody id="tableID_SenaraiPermohonan">
                                        
                                    </tbody>
                                </table>                                

                            </div>
                        </div>                  
                    </div>

<!-- Modal Penerimaan -->
<div id="Batal" class="modal fade hide" role="dialog">
<div class="modal-dialog modal-lg" role="dialog">
               
        <!-- Modal content-->
        <div class="modal-content">
        <div class="modal-header"><h4>Batal Permohonan</h4>
        <button type="button" class="close" data-dismiss="modal"></button>
        <h4 class="modal-title"></h4> 
        </div>
        <div class="modal-body">
               
        <asp:Panel ID="Panel1" runat="server" >
                    <div class="form-row">  
                                    
                    <div class="form-group col-sm-6">
                        <label for="kodModul">Dibatalkan Oleh</label><br />
                        <input type="text" id="namaBatal"  class="form-control"  />
                            <%--<asp:TextBox ID="txtNamaP" runat="server" Width="100%" class="form-control input-sm" style="background-color:#f3f3f3"></asp:TextBox>--%>
                    </div>                                    
                    <div class="form-group col-sm-6">
                        <label for="kodModul">Jawatan </label><br />
                        <input type="text" ID="jwtnBatal"  Width="100%" class="form-control"  />                                       
                    </div>                               
                </div>

                <fieldset>
                    <h6>Maklumat Pembatalan Permohonan</h6>

                    <div>
                    <input type="radio"  class="radioBtnClass" name="status" value="12" checked />
                    <label for="huey">Batal</label>
                    </div>                    

                    <div class="form-group col-sm-6"> 
                        <label for="dewey">Alasan Batal</label>
                        <input type="text" ID="txtCatatan"  Width="100%" class="form-control"  />                                       
                    </div>   

                </fieldset>

                          
                           
                                 
        </asp:Panel>
            </div>
            <div class="modal-footer">
            <button type="button" runat="server" class="btn btn-secondary btnSavePelulus"  data-dismiss="modal">Simpan</button>
            </div>
            </div>

            </div>
        </div>

 <style>
       

     .middle-align {
            text-align: center;
        }
      .right-align {
            text-align: right;
        }
    
    </style>
   

    <script type="text/javascript">
        var isClicked = false;
        var mohon = "";
        function ShowPopup(elm) {
            if (elm == "1") {
                //alert("masuk 1");
                $('#modalSenarai').modal('toggle');
            }
            else if (elm == "2") {
               // alert("masuk 2");
                $(".modal-body div").val("");
                $('#modalSenarai').modal('toggle');
            }
        }

        $(document).ready(function () {
                 <%--$('#<%=btnKira.CLIENTID%>').click(function (evt) {
                     evt.preventDefault();
                     console.log("masuk")
                     calculateDifference();

                 });--%>

            $('#tkhTamat').change(function (evt) {
                evt.preventDefault();
                calculateDifference();

            });
        });

        $(document).ready(function () {

            //getDataPeribadi();
            //getHadMinPendahuluan();
            tbl = $("#tblDataSenarai").DataTable({
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
                    "url": "Batal_WS.asmx/LoadOrderRecord_PermohonanSendiri",
                    type: 'POST',
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "dataSrc": function (json) {
                        //var data = JSON.parse(json.d);
                        //console.log(data.Payload);
                        return JSON.parse(json.d);
                    },
                    data: function () {
                        var startDate = $('#txtTarikhStart').val()
                        var endDate = $('#txtTarikhEnd').val()
                        return JSON.stringify({
                            category_filter: $('#categoryFilter').val(),
                            isClicked: isClicked,
                            tkhMula: startDate,
                            tkhTamat: endDate,
                            staffP: '<%=Session("ssusrID")%>'
                        })
                    },

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
                        rowClickHandler(data);
                    });

                },
                "columns": [
                    {
                        "data": "No_Pendahuluan",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {
                                return data;
                            }

                            var link = `<td style="width: 10%" >
                                    <label  name="noPermohonan"  value="${data}" >${data}</label>
                                                <input type ="hidden" class = "noPermohonan" value="${data}"/>
                                            </td>`;
                            return link;
                        }
                    },
                    { "data": "Tarikh_MohonP" },
                    { "data": "jenis" },
                    { "data": "Tujuan" },
                    { "data": "Tarikh_Mula" },
                    { "data": "Tarikh_Tamat" },
                    {
                        "data": "Jum_Mohon",
                        render: function (data, type, full) {
                            return parseFloat(data).toFixed(2);
                        }

                    }
                    
                   
                ],
                "columnDefs": [                    
                    { "targets": [6], "className": "right-align" }
                ],
            });
         });


        $("#categoryFilter").change(function (e) {

            var selectedItem = $('#categoryFilter').val()
            if (selectedItem == "6") {
                $('#txtTarikhStart').show();
                $('#txtTarikhEnd').show();

                $('#lblMula').show();
                $('#lblTamat').show();

                $('#txtTarikhStart').val("")
                $('#txtTarikhEnd').val("")
            }
            else {
                $('#txtTarikhStart').hide();
                $('#txtTarikhEnd').hide();

                $('#txtTarikhStart').val("")
                $('#txtTarikhEnd').val("")

                $('#lblMula').hide();
                $('#lblTamat').hide();

            }

        });

        $('.btnSearch').click(async function () {
            isClicked = true;
            tbl.ajax.reload();
        })

        function rowClickHandler(orderDetail) {
            
            // change .btnSimpan text to Simpan
            $('.btnSimpan').text('Simpan')
            $('.btnSimpan').removeClass('btn-success');
            $('.btnSimpan').addClass('default-primary');
            $('#Batal').modal('toggle');
            loadInfoBatal(orderDetail.No_Staf);
            mohon = orderDetail.No_Pendahuluan
            console.log(mohon)
        }

        function loadInfoBatal(nostaf) {
            //Cara Pertama
            //var nostaf = '<'%=Session("ssusrID")%>'
            console.log("masuk sini")
            console.log(nostaf)
             fetch('Batal_WS.asmx/GetUserInfo', {
                 method: 'POST',
                 headers: {
                     'Content-Type': "application/json"
                 },
                 //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
                     body: JSON.stringify({ nostaf: nostaf })
                 })
                     .then(response => response.json())
                     .then(data => setDataPeribadi(data.d))


                 ////Cara Kedua
                 <%--var param = {
                     nostaf: '<%=Session("ssusrID")%>'
                 }

                 $.ajax({
                     url: 'Pendahuluan_WS.asmx/GetUserInfo',
                     method: 'POST',
                     data: JSON.stringify(param),
                     dataType: 'json',
                     contentType: 'application/json; charset=utf-8',
                     success: function (data) {
                         setDataPeribadi(data.d);
                         //alert(resolve(data.d));
                     },
                     error: function (xhr, textStatus, errorThrown) {
                         console.error('Error:', errorThrown);
                         reject(false);
                     }

                 });--%>
        }

        function setDataPeribadi(data) {
            data = JSON.parse(data);
            if (data.Nostaf === "") {
                alert("Tiada data");
                return false;
            }

            $('#jwtnBatal').val(data[0].Param3);
            $('#namaBatal').val(data[0].Param1);
             
           
            //getHadMinPendahuluan();
        }

        $('.btnSavePelulus').click(async function () {           
           // $('#modalSenarai').modal('hide');
            //$('#penerimaan').modal('hide');

            if ($("input[type='radio'].radioBtnClass").is(':checked')) {
                var card_type = $("input[type='radio'].radioBtnClass:checked").val();
                //alert(card_type);
            }

            console.log("status")
            console.log(card_type)

            var UpdateData = {
                Batal: {
                    mohonID: mohon,
                    stafID: $('#txtNoStaf').val(),
                    catatan: $('#txtCatatan').val(),                    
                    statusDok: card_type,

                }
            }

            msg = "Anda pasti ingin menyimpan rekod ini?"
            if (!confirm(msg)) {
                return false;
            }
            show_loader();
            var result = JSON.parse(await ajaxBatal(UpdateData));
            alert(result.Message)
            close_loader();
            tbl.ajax.reload();

        });

        async function ajaxBatal(id) {

            return new Promise((resolve, reject) => {
                $.ajax({
                    url: 'Batal_WS.asmx/SaveBatalPermohonan',
                    method: 'POST',
                    data: JSON.stringify(id),
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        resolve(data.d);
                        //alert(resolve(data.d));
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.error('Error:', errorThrown);
                        reject(false);
                    }

                });
            })

        }



    </script>

</asp:Content>
