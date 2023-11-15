<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Transaksi_Semasa.aspx.vb" Inherits="SMKB_Web_Portal.Transaksi_Semasa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
<div id="PermohonanTab" class="tabcontent" style="display: block">
    Bulan/Tahun Gaji : <label id="lblBlnThnGaji"></label>
    <label id="hidBulan" style="visibility:hidden"></label>
                    <label id="hidTahun" style="visibility:hidden"></label>

        <div class="table-title">
            <h6>Senarai Staf</h6>
            <hr>
        </div>
       
        <div class="form-row">
             
            <div class="col-md-12">
                
                <div class="transaction-table table-responsive">
                    
                    <table id="tblListStaf" class="table table-striped" style="width:100%">
                        <thead>
                            <tr>
                                <th scope="col" style="width:100px">No. Staf</th>
                                <th scope="col" style="width:100px">Nama</th>
                                <th scope="col" style="width:250px">Pejabat</th>
                                <th scope="col" style="width:100px">Tindakan</th>
                            </tr>
                        </thead>
                        <tbody id="tableID_ListStaf">
                                        
                        </tbody>

                    </table>
                </div>
            </div>                  
        </div>
    <div class="table-title">

        <div class="col-sm-6 col-md-8">
            <div class="card border-white">
                <div class="card-header" style="text-align: left">
                    <label id="lbl1"></label>
                    <label id="lbl2"></label>
                     <label id="hidNostaf" style="visibility:hidden"></label>
                    <label id="hidSave" style="visibility:hidden"></label>
                </div>

            </div>
        </div>
 
    </div>
       
        <div class="form-row">
             
            <div class="col-md-12">
                
                <div class="transaction-table table-responsive">
                    
                    <table id="tblListHdr" class="table table-striped" style="width:100%">
                        <thead>
                            <tr style="background-color:gray;text-align:center;">
                                <th colspan="3" style="border-right:1px solid white;">Pendapatan</th>
                                <th colspan="3">Potongan</th>
                            </tr>
                            <tr style="background-color:gray;">
                                <th scope="col" style="width:50px">Kod</th>
                                <th scope="col" style="width:100px">Butiran</th>
                                <th scope="col" style="width:100px;border-right:1px solid white;text-align:right">Amaun (RM)</th>
                                <th scope="col" style="width:100px">Kod</th>
                                <th scope="col" style="width:100px">Butiran</th>
                                <th scope="col" style="width:100px;text-align:right">Amaun (RM)</th>
                            </tr>
                        </thead>
                        <tbody id="tableID_ListLejar">
                                        
                        </tbody>
                        <tfoot style="display:none">
                             
                            <tr style="background-color:beige;" rowspan="2"> 
                                <td colspan="2" ><b>PENDAPATAN KASAR (RM) </b></td>
                                <td colspan="1" id="lblAmaunPendapatan" style="border-right:1px solid grey;text-align:right;font-weight:bold"></td>
                                <td colspan="2" ><b>JUMLAH POTONGAN (RM) </b></td>
                                <td colspan="1" id="lblAmaunPotongan" style="text-align:right;font-weight:bold"></td>
                            </tr>
                            <tr style="background-color:beige;" >
       
                                <td colspan="2" ></td>
                                <td colspan="1" style="border-right:1px solid grey;text-align:right"></td>
                                <td colspan="2" ><b>PENDAPATAN BERSIH (RM) </b></td>
                                <td colspan="1" id="lblAmaunbersih" style="text-align:right; font-weight:bold" ></td>
                            </tr>
                        </tfoot>

                    </table>
                </div>
            </div>                  
        </div>

        

</div>
<script type="text/javascript">
    var tbl = null

    $(document).ready(function () {
        getBlnThn();

        tbl = $("#tblListStaf").DataTable({
            dom: '<"toolbar">frtip',
            "responsive": true,
            "searching": true,
            "bLengthChange": false,
            "sPaginationType": "full_numbers",
            "pageLength": 5,
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
                "url": "Transaksi_WS.asmx/LoadListStaf",
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
            "columns": [
                {
                    "data": "MS01_NoStaf",
                    
                },
                { "data": "MS01_Nama" },
                { "data": "PejabatS" },
                {
                    className: "btnView",
                    "data": "MS01_NoStaf",
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }

                        var link = `<button runat="server" class="btn" type="button" style="color: blue" data-dismiss="modal">
                                                    <i class="fa fa-edit"></i>
                                        </button>`;
                        return link;
                    }
                }
            ]

        });

    });
   
    function getBlnThn() {
        //Cara Pertama

        fetch('Transaksi_WS.asmx/LoadBlnThnGaji', {
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
        var sBulan = "";
        data = JSON.parse(data);

        if (data.bulan === "") {
            alert("Tiada data");
            return false;
        }
        $('#lblBlnThnGaji').text(data[0].butir);
        if (data[0].bulan < 10) {
            sBulan = '0' + data[0].bulan;
        }
        else {
            sBulan = data[0].bulan;
        }
        $('#hidBulan').text(data[0].bulan);
        $('#hidTahun').text(data[0].tahun);

    }

    function getInfoDet(obj) {
        alert($(obj).text());


    }
    function formatDate(tkh) {
        var date1 = tkh.split('/')
        var newDate = date1[2] + '-' + date1[1] + '-' + date1[0];
        return newDate;
       // var date = new Date(newDate);
       // alert(newDate);
    }



    $('#tblListStaf').on('click', '.btnView', function (e) {
        e.preventDefault();
        var recordID = $(this).closest('tr').find('td:eq(0)').text(); // amend the index as needed
       // var curTR = $(this).closest("tr");
        //var recordID = curTR.find("td > .noStaf");
        //var bool = true;
        //var id = recordID.val();
        var bln = $('#hidBulan').text();
        var thn = $('#hidTahun').text();
        $('#lbl1').text(recordID);
        $('#hidNostaf').text(recordID);
        $('#lbl2').text($(this).closest('tr').find('td:eq(1)').text());
        
        //ListMaster(recordID, thn, bln);
        $('#tblListHdr > tbody').empty();

        getAllData();
        //var data = table.row($(this).parents('tr')).data();

        //alert(data[0] + "'s salary is: " + data[5]);


        
        //$("div.header").html('Charges list');
    });


    ///////////////////////////////////
    //1st
 // getAllData();

    async function getAllData() {
        
        var dataIncome = await getIncome();
        var dataPotongan = await getPotongan();
        var counter = 0;
        dataIncome = JSON.parse(dataIncome)
        dataPotongan = JSON.parse(dataPotongan)
        
        var totalData = dataIncome.length;

        if (dataPotongan.length > totalData) {
            totalData = dataPotongan.length;
        }
        var totalIncome = 0.00;
        var totalPotongan = 0.00;
        var totalBersih = 0.00;

        while (counter < totalData) {

            var potongan1 = ""
            var potongan2 = ""
            var potongan3 = ""
           

            var income1 = ""
            var income2 = ""
            var income3 = ""
           

            if (dataIncome[counter] !== null && dataIncome[counter] !== undefined) {
                income1 = dataIncome[counter].Kod_Trans
                income2 = dataIncome[counter].Butiran
                income3 = dataIncome[counter].Amaun
                totalIncome += parseFloat(dataIncome[counter].Amaun);
            }

            if (dataPotongan[counter] !== null && dataPotongan[counter] !== undefined) {
                potongan1 = dataPotongan[counter].Kod_Trans
                potongan2 = dataPotongan[counter].Butiran
                potongan3 = dataPotongan[counter].Amaun
                totalPotongan += parseFloat(dataPotongan[counter].Amaun);
            }

            
            var tr = $('<tr>')
                .append($('<td>').html(income1))
                .append($('<td>').html(income2))
                .append($('<td style = "border-right:1px solid grey;text-align:right;">').html(parseFloat(income3).toFixed(2))) 
                .append($('<td>').html(potongan1))
                .append($('<td>').html(potongan2))
                .append($('<td style = "text-align:right">').html(parseFloat(potongan3).toFixed(2)))

            $('#tblListHdr tbody').append(tr);
            counter += 1;
        }
        totalBersih = totalIncome - totalPotongan;
        $('#lblAmaunPendapatan').html(totalIncome);
        $('#lblAmaunPotongan').html(totalPotongan);
        $('#lblAmaunbersih').html(parseFloat(totalBersih).toFixed(2)); 
        


    }

    async function getIncome() {
        var vBln = $('#hidBulan').text();
        var vThn = $('#hidTahun').text();
        var vNostaf = $('#lbl1').text();

        return new Promise((resolve, reject) => {
            $.ajax({
                url: "Transaksi_WS.asmx/LoadListIncome",
                method: 'POST',
                data: JSON.stringify({
                    nostaf: vNostaf,
                    tahun: vThn,
                    bulan: vBln
                }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {

                    resolve(data.d);
                },
                error: function (xhr, textStatus, errorThrown) {
                    console.error('Error:', errorThrown);
                    reject(false);
                }
            });
        })
    } 

    async function getPotongan() {
        var vBln = $('#hidBulan').text();
        var vThn = $('#hidTahun').text();
        var vNostaf = $('#lbl1').text();

        return new Promise((resolve, reject) => {
            $.ajax({
                url: "Transaksi_WS.asmx/LoadListPotongan",
                method: 'POST',
                data: JSON.stringify({
                    nostaf: vNostaf,
                    tahun: vThn,
                    bulan: vBln
                }),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    resolve(data.d);
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
