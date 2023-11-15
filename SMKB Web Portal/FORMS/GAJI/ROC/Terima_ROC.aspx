<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Terima_ROC.aspx.vb" Inherits="SMKB_Web_Portal.Terima_ROC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <style>
        labels {
    
    height: 20px;
    width: 200px;
    margin-top: 10px;
    margin-left: 10px;
    text-align: right;
    margin-right:15px;
    float:left;
}
inputs {
    height: 20px;
    width: 300px;
    border: 1px solid #000;
    margin-top: 10px;
}
        </style>
<div id="PermohonanTab" class="tabcontent" style="display: block">
  <div class="search-filter">
        <div class="form-row">
            <div class="form-group col-md-3">
                <label>Bulan/Tahun Gaji</label>
                <asp:TextBox runat="server" ID="txtBlnThn" CssClass="form-control" Enabled="false" Style="width: 50%;" ></asp:TextBox>
            </div>

        </div>
    </div>
            <label id="hidBulan" style="visibility:hidden" ></label>
        <label id="hidTahun" style="visibility:hidden"></label>
    <div class="card w-2" style="width: 30rem;align-content:center;">
              <h6 class="card-header">Rumusan</h6>
              <div class="card-body">
                  Bil. ROC : <label id="jumroc"></label>&nbsp;&nbsp;&nbsp;&nbsp; Bil. Gaji : <label id="jumgaji">&nbsp;&nbsp;&nbsp;&nbsp;</label> Bil. Elaun : <label id="jumelaun"></label>
              </div>
            </div>
    <div class="table-title">
        <h6>Paparan ROC Belum Diproses</h6>
        <hr>
        <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Proses">Proses</button>

    </div>
    
    <div class="form-row">
                <div class="col-md-12">
                    <div class="transaction-table table-responsive">
                        <table id="tblListROC" class="table table-striped" style="width:100%">
                            <thead>
                                <tr>
                                    <th scope="col"><input type="checkbox" name="selectAll" id="selectAll" /></th>
                                    <th scope="col" style="width:100px">No. ROC</th>
                                    <th scope="col" style="width:250px">No. Staf | Nama</th>
                                    <th scope="col" style="width:100px">Tarikh Disahkan</th>
                                        <th scope="col" style="width:200px">No Ruj Surat</th>
                                    <th scope="col">Keterangan</th>
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
                                            <label >No. Staf</label>
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
                                                     <th scope="col" style="width:10px">Bil</th>
                                                    <th scope="col" style="width:200px">Butiran</th>
                                                    <th scope="col" style="width:50px">Tarikh Mula</th>
                                                    <th scope="col" style="width:50px">Tarikh Tamat</th>
                                                     <th scope="col" style="width:50px">Jumlah (RM)</th>
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
    var tbldt = null
    var noroc_ = "";
    var bln = "";
    var thn = "";


    $(document).ready(function () {
        show_loader();
        
        getBlnThn();
        getRumusROC();


            tbl = $("#tblListROC").DataTable({
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
                    
                    "url": "ROC_WS.asmx/LoadListROC",
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
                    {
                        "data": "MS15_NoRoc",
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        render: function (data, type, row, meta) {
                            if (type !== "display") {
                                return data;

                            }

                            var link = ` <input type="checkbox" name="checkROC" class = "checkROC" id="checkROC" class="checkSingle"  />`;
                            return link;
                        }
                    },
                    {
                        "data": "MS15_NoRoc",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {

                                return data;

                            }

                            var link = `<td style="width: 10%" >
                                            <label id="lblNo" name="lblNo"  class="lblNo" value="${data}" ><a id="myLink" class="yourButton" href="#" onclick="ShowPopup(this);">${data}</a></label>
                                            <input type ="hidden" class = "lblNo" value="${data}"/>
                                        </td>`;
                            return link;
                        }
                    },
                    { "data": "nama" },
                    { "data": "MS15_TkhDisahkan" },
                    { "data": "MS15_NoRujSurat" },
                    { "data": "MS15_Keterangan" }
                ]

            });

        tbldt = $("#tblListDtROC").DataTable({
            "responsive": true,
            "searching": false,
            "bLengthChange": false,
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

                "url": "ROC_WS.asmx/LoadDtROC",
                    method: 'POST',
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    data: function () {
                        return JSON.stringify({ noroc: noroc_ })
                    },
                    "dataSrc": function (json) {
                        //var data = JSON.parse(json.d);
                        //console.log(data.Payload);
                        return JSON.parse(json.d);
                    }
                },
                "columns": [
                    {
                        "render": function (data, type, full, meta) {
                            return meta.row + 1;
                        }
                    },
                    { "data": "ROC01_Butiran" },
                    {
                        "data": "ROC01_TkhMulaB", className: "text-center"
                    },
                    {
                        "data": "ROC01_TkhTamatB", className: "text-center"
                    },
                    { "data": "roc01_amaunakandibayar", className: "text-right", render: $.fn.dataTable.render.number(',', '.', 2, '') }
                ]

            });
            ////delegation concept
            //$('#tableA').on('click', '#btnView1', function () {
            //})
        
    });
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
        $('#hidBulan').text(data[0].bulan);
        $('#hidTahun').text(data[0].tahun);


    }
    $("#selectAll").on("change", function () {
        //tbl.$("input[type='checkbox']").attr('checked', $(this.checked));
        if (this.checked) {
                $(".checkSingle").each(function () {
                    this.checked = true;
                });
            } else {
                $(".checkSingle").each(function () {
                    this.checked = false;
                });
            }

    });
    //$('#selectAll').click(function (e) {
    //    if ($(this).hasClass('checkedAll')) {
    //        $('input').prop('checked', false);
    //        $(this).removeClass('checkedAll');
    //    } else {
    //        $('input').prop('checked', true);
    //        $(this).addClass('checkedAll');
    //    }
    //});

    $('#tblListROC').on('click', '.yourButton', function () {
        var val = $(this).closest('tr').find('td:eq(1)').text().trim(); // amend the index as needed
        var nostaf = $(this).closest('tr').find('td:eq(2)').text().trim().substring(0, 5);;
        noroc_ = val;
        //alert(nostaf)
        tbldt.ajax.reload();

        getInfoStaf(nostaf);
        getInfoROC(val);

        
    });
    function ShowPopup(obj) {
        $('#infostaf').modal('toggle');
    }
    function getRumusROC() {

        fetch('ROC_WS.asmx/LoadRumusROC', {
                method: 'POST',
                headers: {
                    'Content-Type': "application/json"
                },

            body: JSON.stringify()
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
    function getInfoROC(noroc) {
        //Cara Pertama

        fetch('ROC_WS.asmx/LoadRekodROC', {
                method: 'POST',
                headers: {
                    'Content-Type': "application/json"
                },
            //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
            body: JSON.stringify({ noroc: noroc })
        })
                .then(response => response.json())
            .then(data => setInfoROC(data.d))

    }
    function setInfoROC(data) {
        data = JSON.parse(data);
        if (data.R1NoROC === "") {
            alert("Tiada data");
            return false;
        }

        $('#<%=txtNoRoc.ClientID%>').val(data[0].R1NoROC);
        $('#<%=txtTkhRoc.ClientID%>').val(data[0].R1TkhRoc);
        $('#<%=txtNoRuj.ClientID%>').val(data[0].R1NoRujSurat);
        $('#<%=txtKeterangan.ClientID%>').val(data[0].R1Keterangan);
        $('#<%=txtJenis.ClientID%>').val(data[0].NamaROC);
    }

    function getInfoStaf(nostaf) {
        //Cara Pertama
        
        fetch('ROC_WS.asmx/LoadRekodStaf', {
                method: 'POST',
                headers: {
                    'Content-Type': "application/json"
                },
            //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
            body: JSON.stringify({ nostaf:nostaf  })
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
        $('#<%=txtSkim.ClientID%>').val(data[0].skim); 
        $('#<%=txtNoGaji.ClientID%>').val(data[0].MS01_NoStaf); 

    }

   
    //$(document).ready(function () {
    //    $("#checkedAll").change(function () {
    //        if (this.checked) {
    //            $(".checkSingle").each(function () {
    //                this.checked = true;
    //            });
    //        } else {
    //            $(".checkSingle").each(function () {
    //                this.checked = false;
    //            });
    //        }
    //    });

    //    $(".checkSingle").click(function () {
    //        if ($(this).is(":checked")) {
    //            var isAllChecked = 0;

    //            $(".checkSingle").each(function () {
    //                if (!this.checked)
    //                    isAllChecked = 1;
    //            });

    //            if (isAllChecked == 0) {
    //                $("#checkedAll").prop("checked", true);
    //            }
    //        }
    //        else {
    //            $("#checkedAll").prop("checked", false);
    //        }
    //    });
    //});


    //// Handle form submission event
    //$('#frm-example').on('submit', function (e) {
    //    var form = this;

    //    // Iterate over all checkboxes in the table
    //    table.$('input[type="checkbox"]').each(function () {
    //        // If checkbox doesn't exist in DOM
    //        if (!$.contains(document, this)) {
    //            // If checkbox is checked
    //            if (this.checked) {
    //                // Create a hidden element
    //                $(form).append(
    //                    $('<input>')
    //                        .attr('type', 'hidden')
    //                        .attr('name', this.name)
    //                        .val(this.value)
    //                );
    //            }
    //        }
    //    });
    //});

//});
    $('.btnSimpan').click(function () {
        var data = {
            data: []
        };
        
        $('.checkROC:checked').each(function () {
            var tr = $(this).closest("tr");
            data.data.push({ NoStaf: tr.find("td:eq(2)").text(), NoROC: tr.find(".lblNo").text() })
        });   
  
        if (data.data.length === 0) {
            alert('Sila buat pilihan untuk diproses.');
            return false;
        }
        show_loader();
        $.ajax({
            "url": "ROC_WS.asmx/SimpanROC",
            method: 'POST',
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            data: JSON.stringify(data),
            success: function (response) {
                console.log('Success', response);
                var response = JSON.parse(response.d);
                console.log(response);
                alert(response.Message);
                window.location.reload(1);
            }

        });
    });
</script>
</asp:Content>
