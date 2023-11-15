<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Pengesahan_Penyelia.aspx.vb" Inherits="SMKB_Web_Portal.Pengesahan_Penyelia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
 <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js" crossorigin="anonymous"></script>--%>
    <style>
        .checkbox-spacing {
    margin-right: 50px; /* Adjust the margin value as needed */
}

    </style>
    <div id="PermohonanTab" class="tabcontent" style="display: block">

        <!-- Modal -->
        <div id="permohonan">
            
                <div class="modal-content" >
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Pengesahan Penyelia Untuk Tuntutan kerja Lebih Masa</h5>

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


         <div class="modal fade" id="Pengesahan" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-xl modal-dialog-scrollable" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle1">Pengesahan EOT</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">

                            <h6><u>Butir - Butir Borang Tuntutan</u></h6>
                              <%-- <asp:Panel ID="Panel2" runat="server" >--%>
                                 <div class="form-row">  
                                    
                                    <div class="form-group col-sm-6">
                                        <label for="NoPermohonan">No. Permohonan</label><br />
                                       <%-- <input type="text" id="txtNoMohon"  class="form-control" style="width: 20%" />--%>
                                        <asp:TextBox ID="txtNoMohon" runat="server" Width="20%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        
                                    </div>  
                                </div>
                             <%-- </asp:Panel>--%>
                            <hr>                           
                               <div class="col-md-12">                                   
                                <table id="tblDataSenaraiEOT_trans" class="table table-striped" style="width: 99%">
                                    <thead>                                                                             
                                        <tr>
                                            <th scope="col">Sah</th>
                                            <th scope="col">Tidak Sah</th>
                                            <th scope="col">No Mohon</th>
                                            <th scope="col">Tarikh Tuntut</th>                                         
                                            <th scope="col">Jam Tuntut</th> 
                                            <th scope="col">Kadar</th> 
                                            <th scope="col">Amaun (RM)</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_SenaraiEOT_trans">
                                    </tbody>
                                </table>
                            </div>
                             <div class="col-md-12">
                                <div class="form-row">  
                                      <div class="form-group col-md-11"> <h6 style="color:#FF0000;font-style:italic; align-content:center">** Sila klik rekod untuk kemaskini tuntutan </h6> </div>
                                      <div class="form-group col-md-1">
                                       <label for="ButSah">&nbsp</label>                          
                                            <button type="button" class="btn btn-secondary btnSah"  style="margin-top:32px" data-toggle="tooltip">Pengesahan</button>     
                                      </div>
                                </div>
                            </div>
                           <hr />
                            <div class="col-md-12">
                                <div class="form-row"> 
                                    <div class="form-group col-md-2">
                                     <label for="lbltajuk"><u>Kemaskini Tuntutan</u></label>
                                     </div>
                                </div>

                            </div>
                             <div class="col-md-12">                    
                                <div class="form-row">
                                   <div class="form-group col-md-2">
                                        <label for="TarikhTuntut">Tarikh Tuntut</label>
                                            <input type="text" class="form-control" id="txtTkhTuntut" runat="server" style="width:150px" readonly/> 
                                      
                                    </div>                   
                                    <div class="form-group col-md-1">
                                        <label for="JamMula">Jam Mula</label>
                                            <input type="text" class="form-control" id="txtJamMula"  runat="server"  style="text-align: right; width:100px" />   
                                            <h9 style="color:#FF0000;font-size:12px"> (Nota : 2400 -> 0000)</h9>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtJamMula" CssClass="text-danger" ErrorMessage="*Sila Masukkan Jam Mula" ValidationGroup="Semak" Display="Dynamic"/>
                                    </div> 
                                    <div class="form-group col-md-1">
                                        <label for="JamTamat">Jam Tamat</label>
                                            <input type="text" class="form-control" id="txtJamTamat" runat="server" style="text-align: right; width:100px" />   
                        
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtJamTamat" CssClass="text-danger" ErrorMessage="*Sila Masukkan Jam Tamat" ValidationGroup="Semak" Display="Dynamic"/>
                                    </div>
                                    <div class="form-group col-md-1">  
                                        <label for="ButKira"></label>
                    
                                        <button type="button" class="btn btn-info btnKira" style="margin-top:32px" data-toggle="tooltip"  data-validation-group="Semak">Kira</button>                
                                    </div>
              
                                    <div class="form-group col-md-1">
                                        <label for="JamTuntut">Jam Tuntut</label>
                                            <input type="text" class="form-control" id="lblJamTuntut" name="lblJamTuntut" runat="server" style="text-align: right; width:100px" readonly />   
                        
                                    </div>
                                    <div class="form-group col-md-1">
                                        <label for="AmaunTuntut">Amaun Tuntut</label>
                                        <input type="text" class="form-control" id="lblAmnTuntut" runat="server" style="text-align: right; width:100px" readonly />   
                                    </div>

                                     <div class="form-group col-md-4">
                                        <label for="Ulasan">Ulasan</label>
                                        <input type="text" class="form-control" id="txtUlasan" runat="server" style="text-align: left; width:360px"  Rows="2" TextMode="MultiLine" />   
                                    </div>
                                     <div class="form-group col-md-1">
                                            <label for="ButSimpan">&nbsp</label>                          
                                            <button type="button" class="btn btn-secondary btnSimpan"  style="margin-top:32px" data-toggle="tooltip" data-validation-group="Semak">Simpan</button>                
                                    </div>
                                </div>

                                 <div class="row">
                                        <div class="col-md-12">
                                            <h6><u>Transaksi</u></h6>
                                            <br />
                                            <div class="transaction-table table-responsive">
                                                <table id="tblData" class="table table-striped" style="width: 100%">
                                                    <thead>
                                                        <tr>
                                                            <%--<th scope="col">Turutan</th>--%>
                                                            <th scope="col">Tarikh Tuntut</th>
                                                            <th scope="col">Jam Mula</th>
                                                            <th scope="col">Jam Tamat</th>
                                                            <th scope="col">Kadar Tuntut</th>
                                                            <th scope="col">Amaun Tuntut</th> 
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tableID">
                                                                </tbody>               
                                                    <tfoot>
                                                        <tr>
                                                            <td colspan ="2"></td>
                                                            <td><strong>Jumlah Keseluruhan</strong></td>
                                                            <td>
                                                                <input  type="text" class="form-control" id="totalKadar" runat="server"  style="text-align: right; font-weight: bold" width="10%" value="0.000" readonly />
                                                            </td>
                                                            <td>
                                                                <input class="form-control" id="totalAmaun" runat="server"  style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly />
                                                            </td>
                                                        </tr>
                                                    </tfoot>                
                                            </table>                             
                                            </div>
                                        </div>
                                </div>
                             </div>                            
                          </div>                           
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>                           
                        </div>
                    </div>
                </div>
            </div>

 

<script type="text/javascript">
    var tbl = null;
    var tbl2 = null;
    var selectedData1 = [];
    var selectedData2 = [];
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
                "url": "Transaksi_EOTs.asmx/LoadSenEOTPengesahanPenyelia",
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

            drawCallback: function (settings) {
                // Your function to be called after loading data
                close_loader();
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




        tbl2 = $("#tblData").DataTable({
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
                "url": "Transaksi_EOTs.asmx/LoadRecordEOTbyNoMohonPenyelia",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: function (d) {
                    return JSON.stringify({ id: $('#<%=txtNoMohon.ClientID%>').val(), Tarikhtuntut: formattedDate });

                 },
                 "dataSrc": function (json) {
                     return JSON.parse(json.d);
                 }
             },
             "columns": [
                 //  { "data": "No_Turutan" },
                 { "data": "Tkh_Tuntut" },
                 { "data": "Jam_Mula" },
                 { "data": "Jam_Tamat" },
                 {
                     "data": "Kadar_Tuntut",
                     "render": function (data, type, row) {
                         // Format the Kadar_Tuntut column with three decimal places
                         if (type === 'display' || type === 'filter') {
                             return parseFloat(data).toFixed(3);
                         }
                         return data;
                     }


                 },
                 {
                     "data": "Amaun_Tuntut",
                     "render": function (data, type, row) {
                         // Format the Kadar_Tuntut column with three decimal places
                         if (type === 'display' || type === 'filter') {
                             return parseFloat(data).toFixed(2);
                         }
                         return data;
                     }


                 }
             ],

             "columnDefs": [
                 { "targets": [1, 2], "className": "middle-align" },
                 { "targets": [3, 4], "className": "right-align" }
             ],
             "drawCallback": function (settings) {
                 var api = this.api();
                 var counter = 0;
                 var listofdata = api.rows().data();
                 var totaldata = listofdata.length;
                 var jumlahAmaun = 0.00;
                 var jumlahKadar = 0.000;
                 while (counter < totaldata) {
                     jumlahAmaun += parseFloat(listofdata[counter].Amaun_Tuntut)
                     jumlahKadar += parseFloat(listofdata[counter].Kadar_Tuntut)
                     counter += 1;
                 }


                 var fixedAmaun = parseFloat(jumlahAmaun).toFixed(2)
                  <%--  var amountContainer = $('#<%=totalAmaun.ClientID%>');
                    amountContainer.text(jumlahAmaun.toFixed(2));
                    amountContainer.css('text-align', 'right');--%>

                <%--$('#<%=totalAmaun.ClientID%>').val(jumlahAmaun);--%>
                $('#<%=totalAmaun.ClientID%>').val(fixedAmaun);
                 $('#<%=totalKadar.ClientID%>').val(jumlahKadar);

                 // Output the data for the visible rows to the browser's console
             }
         });





    });
        var tableID_Senarai = "#tblDataSenarai_trans";
        var searchQuery = "";
        var oldSearchQuery = "";
        var tableID = "#tblData";
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


    // andling the row click event
    function rowClickHandler(orderDetail) {
       // clearAllRowsHdr();
        $('#Pengesahan').modal('toggle');
        var id = orderDetail.No_Mohon;
        var TkhTuntut = orderDetail.TkhTuntut;
        $('#<%=txtNoMohon.ClientID%>').val(id);
        var tbl2 = null;
        var tbl1 = null;
        tbl1 = $("#tblDataSenaraiEOT_trans").DataTable({
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
                "url": "Transaksi_EOTs.asmx/LoadSenEOTPengesahanPenyeliaDtl",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: function () {
                    return JSON.stringify({ id: id });
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
                    rowClickHandler1(data);
                });

            },
          

            "columns": [
                {
                    className: "btnView1",
                    "data": "Tkh_Tuntut",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }

                        var checkboxes = `<input type="checkbox" class="checkbox-spacing checkbox-single column1-checkbox" name="check1" data-id="${data}">`;

                        return checkboxes;

                    }
                },
                {
                    className: "btnView2",
                    "data": "Tkh_Tuntut",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }

                        var checkboxes = `<input type="checkbox" class="checkbox-spacing checkbox-single column2-checkbox" name="check2" data-id="${data}">`;

                        return checkboxes;

                    }
                },
                {

                    "data": "No_Mohon",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }
                        var link = `<td style="width: 10%" >
                                                <label id="lblNoMohon1" name="lblNoMohon1" class="lblNoMohon1" value="${data}" >${data}</label>
                                            
                                            </td>`;
                        return link;
                    }
                },
                {
                    "data": "Tkh_Tuntut",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }
                        var link = `<td style="width: 10%" >
                                                <label id="lblTkh" name="lblTkh" class="lblTkh" value="${data}" >${data}</label>
                                            
                                            </td>`;
                        return link;

                    }
                },
                { "data": "Jum_Jam_Tuntut" },
                { "data": "Kadar_Tuntut" },
                { "data": "Amaun_Tuntut" }

            ]
  
        });      
        
    }


    //$('#tblDataSenaraiEOT_trans').on('click', '._check', function () {
    //    var checkbox = $(this);
    //    var checked = checkbox.prop('checked');
    //    var dataId = checkbox.data('data-id');


    //});

    //$(document).ready(function () {
    //    $('.checkbox-single').change(function () {
    //        alert("here")
    //        if ($(this).prop('checked')) {
    //            $('.checkbox-single').not(this).prop('checked', false);
    //        }
    //    });
    //});


    // Add event listeners to synchronize checkbox states
    $('#tblDataSenaraiEOT_trans').on('change', '.column1-checkbox', function () {
        var dataId = $(this).data('id');
        var column2Checkbox = $('.column2-checkbox[data-id="' + dataId + '"]');

        if ($(this).is(':checked')) {
            column2Checkbox.prop('checked', false);
        }
    });

    $('#tblDataSenaraiEOT_trans').on('change', '.column2-checkbox', function () {
        var dataId = $(this).data('id');
        var column1Checkbox = $('.column1-checkbox[data-id="' + dataId + '"]');

        if ($(this).is(':checked')) {
            column1Checkbox.prop('checked', false);
        }
    });


    $(document).on('change', '.column1-checkbox', function () {
        var checkedCheckboxes1 = $('.column1-checkbox:checked');

        if (checkedCheckboxes1.length > 0) {
            selectedData = [];

            checkedCheckboxes1.each(function () {
                var data = $(this).closest('tr').find('td:first-child input').val();
                selectedData1.push(data);
            });

        }
    });


    $(document).on('change', '.column2-checkbox', function () {
        var checkedCheckboxes2 = $('.column2-checkbox:checked');

        if (checkedCheckboxes2.length > 0) {
            selectedData2 = [];

            checkedCheckboxes2.each(function () {
                var data = $(this).closest('tr').find('td:first-child input').val();
                selectedData2.push(data);
            });

        }
    });

    function rowClickHandler1(tranEOT) {
       
        var tkhtuntut = tranEOT.Tkh_Tuntut
        $('#<%=txtTkhTuntut.ClientID%>').val(tranEOT.Tkh_Tuntut)

        var dateObject = new Date(tkhtuntut);

        // Format the date as "yyyy-DD-mm"

        var formattedDate = dateObject.getFullYear() + "-" + ("0" + (dateObject.getMonth() + 1)).slice(-2) + "-" + ("0" + dateObject.getDate()).slice(-2);


        // Set the formatted date as the value of the HTML element
        //$('#<%'=txtTkhTuntut.ClientID%>').val(formattedDate);


        tbl2.ajax.reload();


    }

    $('.btnKira').click(async function () {
        var jumRecord = 0;
        var acceptedRecord = 0;
        var msg = "";
        var result = await performCheck("Semak");
        var bezaJam;
        var JamTuntut;
        var Jam;
        var Minit;
        alert("sini")
        if (result === false) {
            return false;
        }

        var mula = $('#<%=txtJamMula.ClientID%>').val();
         var tamat = $('#<%=txtJamTamat.ClientID%>').val();


         var JMula = parseInt(mula.substring(0, 2), 10);
         var JTamat = parseInt(tamat.substring(0, 2), 10);
         var MMula = parseInt(mula.substring(2, 4), 10);
         var MTamat = parseInt(tamat.substring(2, 4), 10);

         if (tamat !== "0000") {
             if (JTamat >= JMula) {
                 var bezaJam = ((JTamat * 60) + MTamat) - ((JMula * 60) + MMula);
                 var JamTuntut = "";


                 if (parseInt(bezaJam / 60) === 0) {
                     var Minit = parseInt(bezaJam % 60);
                     Minit = Minit < 10 ? "000" + Minit : "00" + Minit;
                     JamTuntut = Minit;

                 } else {
                     if (parseInt(bezaJam % 60) === 0) {
                         var Jam = parseInt(bezaJam / 60);
                         if (Jam >= 8) {
                             Jam = Jam - 1;
                         }
                         Jam = Jam < 10 ? "0" + Jam + "00" : Jam + "00";
                         JamTuntut = Jam;

                     } else {
                         var Jam = parseInt(bezaJam / 60);
                         var Minit = parseInt(bezaJam % 60);
                         if (Jam >= 8) {
                             Jam = Jam - 1;
                         }
                         Jam = Jam < 10 ? "0" + Jam : Jam;
                         Minit = Minit < 10 ? "0" + Minit : Minit;
                         JamTuntut = Jam + "" + Minit;

                     }
                 }
                 alert(JamTuntut)
                 /* $("#lblJamTuntut").val(JamTuntut);*/
                 $("#MainContent_FormContents_lblJamTuntut").val(JamTuntut);

             }
         }


         var newArahan = {
             MohonEOT: {

                
                 Tkh_Tuntut: $('#<%= txtTkhTuntut.ClientID%>').val(),
                     Jam_Mula: $('#<%=txtJamMula.ClientID%>').val(),
                 Jam_Tamat: $('#<%=txtJamTamat.ClientID%>').val(),

             }

         }

         var result = JSON.parse(await ajaxKiraAmtEOT(newArahan));
         alert(result.Message)
     });


    async function ajaxKiraAmtEOT(MohonEOT) {

        $.ajax({

            url: 'Transaksi_EOTs.asmx/KiraAmtEOT',
            method: 'POST',
            data: JSON.stringify(MohonEOT),

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var response = JSON.parse(data.d)
                console.log(response);
                var payload = response.Payload;

                $("#MainContent_FormContents_lblAmnTuntut").val(payload);

            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error:', errorThrown);
                reject(false);
            }

        });

    }

    async function performCheck(e) {
        if (e === undefined || e === null) {
            e = "groupone";
        }
        if (await checkValidationGroup(e)) {
            //alert('it is valid');
            return true;
        }
        else {
            //alert("not valid");
            return false;
        }
    }


    async function checkValidationGroup(valGrp) {
        var rtnVal = true;
        var errCheck = 0;
        //$('#ContentPlaceHolder1_msg2').html("");

        for (i = 0; i < Page_Validators.length; i++) {
            if (Page_Validators[i].validationGroup == valGrp) {
                if (document.getElementById(Page_Validators[i].controltovalidate) != null) {
                    ValidatorValidate(Page_Validators[i]);
                    //console.log(+ "---" + Page_Validators[i].isValid);
                    if (!Page_Validators[i].isvalid) {
                        errCheck = 1;
                        rtnVal = false;
                        //break; //exit for-loop, we are done.
                    }
                }
            }
        }

        return rtnVal;
    }



    $('.btnSimpan').click(async function () {
        var jumRecord = 0;
        var acceptedRecord = 0;
        var msg = "";
        var result = await performCheck("Semak");

       

        if (result === false) {
            return false;
        }
        var newArahan = {
            MohonEOT: {

               
                    Tkh_Tuntut: $('#<%= txtTkhTuntut.ClientID%>').val(),
                    Jam_Mula: $('#<%=txtJamMula.ClientID%>').val(),
                    Jam_Tamat: $('#<%=txtJamTamat.ClientID%>').val(),                 
                    Amaun: $("#MainContent_FormContents_lblAmnTuntut").val(),
                    Kadar: $("#MainContent_FormContents_lblJamTuntut").val(),
                    Ulasan:$('#<%=txtUlasan.ClientID%>').val(),
                    }, q: $('#<%=txtNoMohon.ClientID%>').val()
           }

          
            console.log(newArahan);
           
            acceptedRecord += 1;
          

            msg = "Anda pasti ingin menyimpan tuntutan rekod ini?"

            if (!confirm(msg)) {
                return false;
            }

            (await ajaxSaveEOT(newArahan));
           

    });

    $('.btnSah').click(async function () {

        alert(selectedData1)
        alert(selectedData2)

        if (selectedData1.length > 0) {
            $.ajax({
                url: 'Transaksi_EOTs.asmx/SimpanStafAK',
                method: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ selectedData: selectedData1, noarahan: noArahan }),
                success: function (data) {
                    // Handle the success response
                    console.log('Success:', data.d);
                    //var response = JSON.parse(data.d)
                    //alert(response.Message);
                    //tbl1.ajax.reload();
                    //closeModal();

                    //$('#TransaksiStaf').modal('toggle');


                },
                error: function (xhr, status, error) {
                    // Handle the error response
                    console.error('Error:', error);
                }
            });
        }
    });




        async function ajaxSaveEOT(MohonEOT) {

            $.ajax({
              
                url: 'Transaksi_EOTs.asmx/SimpanEOTPenyelia',
                method: 'POST',
                //data: JSON.stringify(MohonEOT),
                data: JSON.stringify(MohonEOT),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    var response = JSON.parse(data.d)
                    alert(response.Message)                   
                    tbl1.ajax.reload();
                    tbl2.ajax.reload();


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

