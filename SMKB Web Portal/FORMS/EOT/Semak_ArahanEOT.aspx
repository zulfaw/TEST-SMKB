<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Semak_ArahanEOT.aspx.vb" Inherits="SMKB_Web_Portal.Semak_ArahanEOT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
<style>
     /*    #KodPTJ {
    font-weight: bold;
     
  }*/
     .upload-button {
        background-color:coral;
        color: white;
        border: none;
        padding: 10px 10px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 14px;
        border-radius: 5px;
        cursor: pointer;
     }

     .textALign {
         float: right;
     }
        .choose-button {
        background-color:beige;
        color: black;
        border: none;
        padding: 10px 10px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 14px;
        border-radius: 5px;
        cursor: pointer;
     }
     
    #TransaksiStaf .modal-body {
            max-height: 70vh; /* Adjust height as needed to fit your layout */
            min-height: 70vh;
            overflow-y: scroll;
            scrollbar-width: thin;
        }
    #subTab a{
        cursor:pointer;
    }
    
    </style>

    <%@ Register Src="~/FORMS/WebUserControl1.ascx" TagName="PilihStaf" TagPrefix="Arahan" %>

     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="PermohonanTab" class="tabcontent" style="display: block">

    <div id="PilihStaf">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Arahan Kerja</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    
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
                                <div class="col-md-11">
                                    <div class="form-row">
                                        <div class="form-group col-md-1">
                                            <label id="lblMula" style="text-align: right;display:none;"  >Mula: </label>
                                        </div>
                                        
                                        <div class="form-group col-md-4">
                                            <input type="date" id="txtTarikhStart" name="txtTarikhStart" style="display:none;" class="form-control date-range-filter">
                                        </div>
                                         <div class="form-group col-md-1">
                                     
                                        </div>
                                        <div class="form-group col-md-1">
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
                   
                    <div class="modal-body">
                        <div class="col-md-12">
                                 <div class="transaction-table table-responsive">   
                                    <table id="tblDataSenarai_trans" class="table table-striped" style="width: 99%">
                                        <thead>
                                            <tr>
                                                <th scope="col">No. Arahan</th>
                                                <th scope="col">No. Rujukan Surat</th> 
                                                <th scope="col">No. Staf Sah</th> 
                                                <th scope="col">Nama Staf Sah</th>
                                                <th scope="col">No. Staf Lulus</th> 
                                                <th scope="col">Nama Staf Lulus</th>
                                                <th scope="col">Kod PTJ</th> 
                                                <th scope="col">Pejabat</th> 
                                                <th scope="col">Tkh Mula</th> 
                                                <th scope="col">Tkh Tamat</th> 
                                                <th scope="col">Lokasi</th> 
                                                <th scope="col">PeneranganK</th> 
                                                <th scope="col">Nama Fail</th> 
                                               <%-- <th scope="col">Tindakan</th>  --%>                                           
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
            </div>
 <Arahan:PilihStaf runat="server" ID="PilihanStafContainer" />
   
 <div class="modal fade" id="TransaksiStaf" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered modal-xl"  style="min-width: 80%" role="document">
   <div class="modal-content">    
     <div class="modal-header modal-header--sticky" style="border-bottom:none !important">      
       <div class="container-fluid mt-3"> 
            <ul class="nav nav-tabs" id="subTab" role="tablist">          
             <li class="nav-item" role="presentation" onclick="subTabChange(event)" data-tab="ARH">
              <a class="nav-link active" tabindex="-1" data-tab="ARH" aria-disabled="true">Arahan Kerja</a>
             </li>
             <li class="nav-item" role="presentation" onclick="subTabChange(event)" data-tab="SEN">
                <a class="nav-link" tabindex="-1" data-tab="SEN" aria-disabled="true">Senarai Staf</a>
             </li>        
           </ul>
       </div>    
       <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                   <span aria-hidden="true">&times;</span>
       </button>
       </div>
       <div class="modal-body">
        
            <div id="ARH" class="modal-sub-tab">
          
          
            <div class="form-row">
            <h6>Transaksi</h6>
            <div class="col-md-12">
                <div class="">
                    <table  id="tblData"  class="table table-striped"><%-- class="table table-bordered"--%>
                        <thead>
                            <tr>
                                <th scope="col" style="width: 30%;">Vot</th>
                                <th scope="col"  style="width: 20%;">PTj</th>
                                <th scope="col"  style="width: 20%;">Kumpulan Wang</th>
                                <th scope="col"  style="width: 15%;">Operasi</th>
                                <th scope="col"  style="width: 15%;">Projek</th>
                            </tr>
                        </thead>
                        <tbody id="tableID">
                            <tr  class="table-list">
                                <td>
                                   <select class="ui search dropdown COA-list" name="ddlCOA" id="ddlCOA" style="width:300px"></select>
                                    <input type="hidden" class="data-id" value="" />
                                    <label id="lblvot" name="lblvot" class="label-vot-list"></label>
                                    <label id="hidVot" name="hidVot" class="Hid-vot-list" style="visibility: hidden"></label>
                                </td>
                                <td>
                                    <label id="lblPTj" name="lblPTj" class="label-ptj-list" style="justify-items:center" ></label>
                                    <label id="HidlblPTj" name="HidlblPTj" class="Hid-ptj-list" style="visibility: hidden"></label>
                                </td>

                                <td>
                                    <label id="lblKw" name="lblKw" class="label-kw-list" style="justify-items:center"></label>
                                    <label id="HidlblKw" name="HidlblKw" class="Hid-kw-list" style="visibility: hidden"></label>
                                </td>
                                <td>
                                    <label id="lblKo" name="lblKo" class="label-ko-list" style="justify-items:center"></label>
                                    <label id="HidlblKo" name="HidlblKo" class="Hid-ko-list" style="visibility: hidden"></label>
                                </td>
                                <td>
                                    <label id="lblKp" name="lblKp" class="label-kp-list" style="justify-items:center"></label>
                                    <label id="HidlblKp" name="HidlblKp" class="Hid-kp-list" style="visibility: hidden"></label>
                                </td>

                            </tr>
                        </tbody>
                    </table>

                </div>
            </div>

        </div>
    
            <div class="table-title">
                <h6>Maklumat Arahan Kerja  </h6>`
             </div> 
           
             <div class="row">
                <div class="col-md-12">                    
                                <div class="form-row">
                                      <%--<div class="form-group col-md-4" style="left: 0px; top: 0px">
                                        <label for="PTJ" class="col-form-label">PTj</label>
                                        <input type="text" class="form-control" id="lblPTJ2" readonly="readonly" style="width:450px" > 
                                           <input type="hidden" class="form-control"  id="hPTJ2" style="width:300px" readonly="readonly" /> 
                                      </div>--%>
                                      <div class="form-group col-md-4">
                                        <label for="NoSurat" class="col-form-label">No Surat</label>
                                          <input type="text" class="form-control" id="txtNoSurat" runat="server" style="width:350px" maxlength="30" readonly/> 
                                          <asp:RequiredFieldValidator ID="RqrNoSurat" runat="server" ControlToValidate="txtNoSurat" CssClass="text-danger" ErrorMessage="*Sila Masukkan No Surat" ValidationGroup="Semak" Display="Dynamic"/>
                                          
                                      </div>
                                                                 
                                    <div class="form-group col-md-4">
                                        <label for="TarikhMula">Tarikh Mula Kerja</label>
                                          <input type="date" class="form-control" id="txtTkhMula"  runat="server" style="width:200px"/>   
                                        <asp:LinkButton ID="lbtntxtTkhMula" runat="server" ToolTip="Klik untuk papar kalendar">
                                        
                                </asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTkhMula" CssClass="text-danger" ErrorMessage="*Sila pilih Tarikh Mula" ValidationGroup="Semak" Display="Dynamic"/>
                                    </div>                                    
                                      <div class="form-group col-md-4">
                                        <label for="TarikhTamat">Tarikh Tamat Kerja</label>
                                          <input type="date" class="form-control" id="txtTkhTamat"  runat="server" style="width:200px" /> 
                                           <asp:LinkButton ID="lbtntxtTkhTamat" runat="server" ToolTip="Klik untuk papar kalendar">
                                                   
                                           </asp:LinkButton>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTkhTamat" CssClass="text-danger" ErrorMessage="*Sila pilih Tarikh Tamat" ValidationGroup="Semak" Display="Dynamic"/>
                                    </div>                                
                               </div>

                                <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="Lokasi">Lokasi</label>
                                         <asp:TextBox ID="txtLokasi" runat="server"   CssClass="form-control" style="width:360px" Rows="2" TextMode="MultiLine" MaxLength="50"></asp:TextBox>                                      
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLokasi" CssClass="text-danger" ErrorMessage="*Sila masukkan Lokasi!" ValidationGroup="Semak" Display="Dynamic"/>
                                    </div>
                                
                                    <div class="form-group col-md-4">
                                        <label for="Penerangan">Penerangan Kerja</label>
                                       <asp:TextBox ID="txtButirKerja" runat="server"   style="width:360px" CssClass="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtButirKerja" CssClass="text-danger" ErrorMessage="*Sila masukkan Keterangan Kerja" ValidationGroup="Semak" Display="Dynamic"/>
                                    </div>                                
                                    <div class="form-group col-md-4">
                                        <label for="Pengesah">Pegawai Pengesah</label>                                      
                                            <select class="ui search dropdown Pengesah" name="ddlPengesah" id="ddlPengesah" style="width:360px; left: 0px; top: 0px;"></select>                                                                                 
                                        <%--<input type="checkbox" id ="chkAllPengesah" value ="1"  AutoPostBack = true/><label for="CatatPengesah">Senarai semua penyelia</label> --%>
                                    </div>
                                
                                    
                                </div>

                                <div class="form-row">
                                    <div class="form-group col-md-4">
                                        <label for="kodModul">Ketua Jabatan</label>                                    
                                            <%--<input type="text" class="form-control" id="lblKetuaPej" style="width:300px" readonly="readonly" />
                                            <%-- <asp:RequiredFieldValidator ID="RqrKetuaPej" runat="server" ControlToValidate="lblKetuaPej" CssClass="text-danger" ErrorMessage="*Sila masukkan maklumat Ketua PTJ" ValidationGroup="Semak" Display="Dynamic"/>--%>

                                            <%--<input type="hidden" class="form-control"  id="hidKetuaPej" style="width:300px" readonly="readonly" />--%>
                                        
                                           <label for="KetuaJabatan">Ketua Jabatan</label>                                      
                                            <select class="ui search dropdown KetuaJabatan" name="ddlKetuaJabatan" id="ddlKetuaJabatan" style="width:100px"></select> 
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label for="UploadSurat">Upload surat</label>
                                         <div class="form-inline">

                                           <input type="file" id="fileInput" class="choose-button" />
                                            <input type="button" id="uploadButton" class="upload-button" value="Upload" onclick="uploadFile()" />
                                                <span id="uploadedFileNameLabel" style="display: inline;"></span>
                                             <span id="">&nbsp</span>   
                                             <span id="progressContainer"></span>
                                             <input type="hidden" class="form-control"  id="hidJenDok" style="width:300px"/> 
                                              <input type="hidden" class="form-control"  id="hidFileName" style="width:300px" /> 
                                                                                   
                                         </div>                                                                              
                                   </div>

                                    <div class="form-group col-md-1">
                                        <asp:Label ID="lblMessageDokumen" runat="server" />
                                    </div>
                               
                                    <div class="form-group col-md-4">
                                        <label for="NoArahan">No Arahan</label>
                                          <asp:TextBox ID="txtNoArahan" runat="server" Width="50%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RqrNoArahan" runat="server" ControlToValidate="txtNoArahan" CssClass="text-danger" ErrorMessage="*Sila masukkan No Arahan" ValidationGroup="Semak" Display="Dynamic"/>
                                         
                                    </div>                                  
                                </div>                                                                                          
                    </div>
                </div>            
            
                 <div class="form-row">
                    <div class="form-group col-md-11" align="right">
                        <button type="button" class="btn btn-secondary">Padam</button>
                        <button type="button" class="btn btn-danger btnSimpan" data-toggle="tooltip" data-validation-group="Semak">Simpan</button>                
                    </div>
                     <div class="form-group col-md-1" style="left: 0px; top: 0px">
                           
                     </div>
                </div>
            </div>

            <div id="SEN" class="modal-sub-tab">   
                 <div class="row">
                            <div class="col-md-12">
                                <div align="right">                            
                                    <button id="btnOpenPilihStaf" data-dismiss="modal" data-toggle="modal" href="#PilihStafModal" class="btn btn-primary"><i class="fa fa-plus"></i>Tambah Staf</button>
                                </div>
                                <br />
                           </div>
                            
                            <div class="col-md-12">
                            
                                <div class="transaction-table table-responsive">
                                    <table id="tblDataSenAK_trans" CssClass="table table-striped" style="width: 99%">
                                        <thead>                                       
                                            <tr>
                                                <th scope="col">No. Arahan</th>
                                                <th scope="col">No. Staf</th>
                                                <th scope="col">Nama</th>                                          
                                                <th scope="col">Tindakan</th> 
                                            </tr>
                                        </thead>
                                        <tbody id="tableID_SenAK_trans">                                     
                                        </tbody>

                                    </table>

                                </div>
                            </div>                  
                        </div>        
              </div>
        </div>
       
      </div>
     </div> 
    </div>
   </div>


<script type="text/javascript">
    var isClicked = false;
    function ShowPopup(elm) {
        alert(elm)

        if (elm == "1") {

            $('#PilihStaf').modal('toggle');


        }
        else if (elm == "2") {
           
            /* $(".modal-body div").val("");*/
            $('#TransaksiStaf').modal('toggle');

        }
    }

    function subTabChange(e) {
        e.preventDefault()
        Array.from(e.target.parentElement.parentElement.getElementsByClassName("active")).forEach(e => {
            e.classList.remove("active")
        })
        e.target.classList.add("active")
        console.log(e.target)
        showTab(e.target.dataset.tab)
    }

    function showTab(id) {
        alert(id)
        $(".modal-sub-tab").hide()
        
        $("#" + id).show()

    }

    var tbl = null
    var tbl1 = null
    $(document).ready(function () {
        show_loader();
        // $("#tblDataSenarai_trans.dataTables_filter").append($("#categoryFilter"));

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
                "url": "Transaksi_EOTs.asmx/LoadRecordArahan",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                "dataSrc": function (json) {
                    return JSON.parse(json.d);
                },

                data: function () {
                    //Filter date bermula dari sini - 20 julai 2023
                    var startDate = $('#txtTarikhStart').val()
                    var endDate = $('#txtTarikhEnd').val()
                    return JSON.stringify({
                        category_filter: $('#categoryFilter').val(),
                        isClicked: isClicked,
                        tkhMula: startDate,
                        tkhTamat: endDate
                    })
                    //akhir sini
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
                    rowClickHandler(data.No_Arahan);
                });

            },

            "columns": [
                {
                    "data": "No_Arahan",
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
                { "data": "No_Surat" },
                { "data": "No_Staf_SahB" },
                { "data": "Nama_Staf_Sah" },
                { "data": "No_Staf_LulusB" },
                { "data": "Nama_Staf_LulusB" },
                { "data": "Kod_PTJ" },
                { "data": "pejabat" },
                { "data": "Tkh_Mula" },
                { "data": "Tkh_Tamat" },
                { "data": "Lokasi" },
                { "data": "PeneranganK" },
                { "data": "File_name" }
                //{
                //    className: "btnView",
                //    "data": "No_Arahan",
                //    render: function (data, type, row, meta) {

                //        if (type !== "display") {

                //            return data;

                //        }

                //        var link = `<button id="btnView" runat="server" data-id = "${data}" class="btn btnView" type="button" style="color: blue" data-dismiss="modal">
                //                                    <i class="fa fa-edit"></i>
                //                        </button>`;
                //        return link;
                //    }
                //}
            ]
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

        $('#lblPTJ').val('<%= Session("ssusrPTj")%>');
            $('#KodPTJ').val('<%= Session("ssusrKodPTj")%>');
            $('#hPTJ').html('<%= Session("ssusrKodPTj")%>');
            $('#lblPTJ').text('<%= Session("ssusrPTj")%>');
            $('#KodPTJ').text('<%= Session("ssusrKodPTj")%>');
            initDropdownCOA("ddlCOA");



            tbl1 = $("#tblDataSenAK_trans").DataTable({
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
                    "url": "Transaksi_EOTs.asmx/LoadRecordStafArahan",
                    method: 'POST',
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    data: function () {
                        return JSON.stringify({ idarahan: $('#<%=txtNoArahan.ClientID%>').val() });
                    },
                    "dataSrc": function (json) {
                        return JSON.parse(json.d);
                    }

                },

                "columns": [
                    {
                        "data": "No_Arahan",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {

                                return data;

                            }
                            var link = `<td style="width: 10%" >
                                                <label id="lblNoArahan3" name="lblNoArahan3" class="lblNoArahan3" value="${data}" >${data}</label>
                                                <input type ="hidden" class = "lblNoArahan3" value="${data}"/>
                                            </td>`;
                            return link;
                        }
                    },
                    { "data": "No_Staf" },
                    { "data": "MS01_Nama" },
                    {
                        className: "btnView3",
                        "data": "No_Staf",
                        render: function (data, type, row, meta) {

                            if (type !== "display") {

                                return data;

                            }

                            var link = `<button id="btnView3" runat="server"  data-id = "${data}" class="btn btnView3" type="button" style="color: blue" data-dismiss="modal">
                                                    <i class="fa fa-trash"></i>
                                        </button>`;
                            return link;
                        }
                    }
                ]
            });

        });




    var selectedData = [];

    $(document).on('change', 'input[name="check"]', function () {
        var checkedCheckboxes = $('input[name="check"]:checked');

        if (checkedCheckboxes.length > 0) {
            selectedData = [];

            checkedCheckboxes.each(function () {
                var data = $(this).closest('tr').find('td:first-child input').val();
                selectedData.push(data);
            });

            console.log("Selected data:", selectedData);
        }
    });

    $("#categoryFilter").click(async function () {
        isClicked = true;
        tbl.ajax.reload();

    })

    var searchQuery = "";
    var oldSearchQuery = "";
    var curNumObject = 0;
    var tableID = "#tblData";
    var tableID_Senarai = "#tblDataSenarai_trans";
    var tableID_SenAK = "#tblDataSenAK_trans";

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

    var shouldPop = true;
    //var totalID = "#totalBeza";

    var totalDebit = "#totalDbt";
    var totalKredit = "#totalKt";
    

    $('.btnSimpanST').on('click', function () {
        if (selectedData.length > 0) {
            alert(selectedData);
            $.ajax({
                url: 'Transaksi_EOTs.asmx/SimpanStafAK',
                method: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify({ selectedData: selectedData }),
                success: function (data) {
                    // Handle the success response
                    console.log('Success:', data.d);
                },
                error: function (xhr, status, error) {
                    // Handle the error response
                    console.error('Error:', error);
                }
            });
        }
    });

    $("#tab-permohonan").click(async function () {
        isClicked = true;     
        tbl1.ajax.reload();

    })


    $('.btnSearch').click(async function () {
        isClicked = true;
        tbl1.ajax.reload();

    })

    function OnSuccess(data, status) {
        $("#lblKetuaPej").html(data.d);

    }

    function OnError(request, status, error) {
        $("#lblKetuaPej").html(request.textStatus);

    }

    $(document).ready(function () {

        // alert("test")
        $('#ddlPengesah').dropdown({
            fullTextSearch: false,
            apiSettings: {
                url: 'Transaksi_EOTs.asmx/GetPegPengesah?q={query}&chk={chk}&ptj={ptj}',
                method: 'POST',
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                cache: false,
                fields: {
                    value: "MS01_NoStaf",      // specify which column is for data
                    name: "MS01_Nama"      // specify which column is for text
                },

                beforeSend: function (settings) {
                    // Replace {query} placeholder in data with user-entered search term
                    //var isChecked = document.getElementById("chkAllPengesah").checked;

                    //settings.urlData.chk = isChecked;
                    settings.urlData.chk = false;
                    settings.urlData.ptj = '<%=Session("ssusrKodPTj")%>'
                        settings.data = JSON.stringify({ q: settings.urlData.query, chk: settings.urlData.chk, ptj: settings.urlData.ptj });
                        searchQuery = settings.urlData.query;

                        return settings;
                    },
                    onSuccess: function (response) {
                        // Clear existing dropdown options
                        var obj = $(this);

                        var objItem = $(this).find('.menu');
                        $(objItem).html('');

                        // Add new options to dropdown
                        if (response.d.length === 0) {
                            $(obj.dropdown("clear"));
                            return false;
                        }

                        var listOptions = JSON.parse(response.d);

                        $.each(listOptions, function (index, option) {
                            $(objItem).append($('<div class="item" data-value="' + option.MS01_NoStaf + '">').html(option.MS01_NoStaf + ' - ' + option.MS01_Nama));
                        });

                        //if (searchQuery !== oldSearchQuery) {
                        // $(obj).dropdown('set selected', $(this).find('.menu .item').first().data('value'));
                        //}

                        //oldSearchQuery = searchQuery;

                        // Refresh dropdown
                        $(obj).dropdown('refresh');

                        if (shouldPop === true) {
                            $(obj).dropdown('show');
                        }
                    }
                }
            });

            $('.ui.Pengesah .dropdown').on('click', function () {
                $('.Pengesah .menu').html("");
            })
        });


    $(document).ready(function () {

        // alert("test")
        $('#ddlKetuaJabatan').dropdown({
            fullTextSearch: false,
            apiSettings: {
                url: 'Transaksi_EOTs.asmx/GetKetuaJabatan?q={query}&chk={chk}&ptj={ptj}',
                method: 'POST',
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                cache: false,
                fields: {
                    value: "MS01_NoStaf",      // specify which column is for data
                    name: "MS01_Nama"      // specify which column is for text
                },

                beforeSend: function (settings) {
                    // Replace {query} placeholder in data with user-entered search term
                    //var isChecked = document.getElementById("chkAllKetuaJabatan").checked;

                    settings.urlData.chk = false;
                    settings.urlData.ptj = '<%=Session("ssusrKodPTj")%>';
                        settings.data = JSON.stringify({ q: settings.urlData.query, chk: settings.urlData.chk, ptj: settings.urlData.ptj });
                        searchQuery = settings.urlData.query;

                        return settings;
                    },
                    onSuccess: function (response) {
                        // Clear existing dropdown options
                        var obj = $(this);

                        var objItem = $(this).find('.menu');
                        $(objItem).html('');

                        // Add new options to dropdown
                        if (response.d.length === 0) {
                            $(obj.dropdown("clear"));
                            return false;
                        }

                        var listOptions = JSON.parse(response.d);

                        $.each(listOptions, function (index, option) {
                            $(objItem).append($('<div class="item" data-value="' + option.MS01_NoStaf + '">').html(option.MS01_NoStaf + ' - ' + option.MS01_Nama));
                        });

                        //if (searchQuery !== oldSearchQuery) {
                        // $(obj).dropdown('set selected', $(this).find('.menu .item').first().data('value'));
                        //}

                        //oldSearchQuery = searchQuery;

                        // Refresh dropdown
                        $(obj).dropdown('refresh');

                        if (shouldPop === true) {
                            $(obj).dropdown('show');
                        }
                    }
                }
            });

            $('.ui.KetuaJabatan .dropdown').on('click', function () {
                $('.KetuaJabatan .menu').html("");
            })
        });

    $('#btnOpenPilihStaf').click(function () {
        
        event.preventDefault();
        $("#TransaksiStaf").val("");
        PilihanStafModule.NoArahan($('#<%=txtNoArahan.ClientID%>').val(), function () {
                ShowPopup(2);
            });
        });

    $('.btnSimpan').click(async function () {
        var jumRecord = 0;
        var acceptedRecord = 0;
        var msg = "";
        var result = await performCheck("Semak");

        if (result === false) {
            return false;
        }
        var newArahan = {
            SemakArahan: {
                   // Kod_PTJ: $('#<%'=hPTJ.ClientID%>').val(),

                    No_Arahan: $('#<%=txtNoArahan.ClientID%>').val(),
                    /* Kod_PTJ: $('#hPTJ').val(),*/
                    kod_PTj: $('.Hid-ptj-list').eq(0).html(),
                    No_Surat: $('#<%=txtNoSurat.ClientID%>').val(),
                    Tkh_Mula: $('#<%=txtTkhMula.ClientID%>').val(),
                    Tkh_Tamat: $('#<%=txtTkhTamat.ClientID%>').val(),
                    Lokasi: $('#<%=txtLokasi.ClientID%>').val(),
                    PeneranganK: $('#<%=txtButirKerja.ClientID%>').val(),
                    No_Staf_Sah: $('#ddlPengesah').val(),
                   // No_Staf_Peg_AK: $('#hidNo_Staf_Peg_ak').val(),
                    /*Ketua_PTJ: $('#hidKetuaPej').val(),*/
                    No_Staf_Lulus: $('#ddlKetuaJabatan').val(),
                    //Ketua_PTJ: $('#lblKetuaPej').val(),
                    Jen_Dok: $('#hidJenDok').val(),
                    File_Name: $('#hidFileName').val(),

                 }
             }

          
            console.log(newArahan);
            // ----
            acceptedRecord += 1;
            //console.log(newOrder.order);

            msg = "Anda pasti ingin menyimpan " + acceptedRecord + " rekod ini?"

            if (!confirm(msg)) {
                return false;
            }

            var result = JSON.parse(await ajaxSaveEOT(newArahan));
            alert(result.Message)
            tbl.ajax.reload();
            //$('#orderid').val(result.Payload.OrderID)
          
            //loadExistingRecords();
            //await clearAllRows();
            // AddRow(5);
            

        });

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

        async function ajaxSaveEOT(SemakArahan) {

            $.ajax({

                url: 'Transaksi_EOTs.asmx/KemaskiniAK',
                method: 'POST',
                data: JSON.stringify(SemakArahan),

                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    var response = JSON.parse(data.d)
                    console.log(response);
                    alert(response.Message);
                    

                  //  var payload = response.Payload;
                 //   $("#<%=txtNoArahan.ClientID%>").val(payload.No_Arahan);

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        console.error('Error:', errorThrown);
                        reject(false);
                    }

                });

             //})

         }

        $('.btn-danger').click(async function () {
            //alert("test");
            //var result = JSON.parse(await ajaxDeleteOrder($('#lblNoJurnal').val()))
            $('#txtNoArahan').val("")
            await clearAllRows();
            await clearAllRowsHdr();
            AddRow(5);
        });

        //$('.btnPapar').click(async function () {
        //    var record = await AjaxLoadOrderRecord_Senarai("");
        //    $('#txtNoArahan').val("")
        //    await clearAllRows_senarai();
        //    await paparSenarai(null, record);
        //});
        var tbl1 = null;
        $('.btnPaparSen').on('click', async function () {
           tbl1.ajax.reload();


          <%--  event.preventDefault();
            PilihanStafModule.NoArahan($('#<%=txtNoArahan.ClientID%>').val(), function () {
                ShowPopup(2);
            });--%>

          
        });


        async function initDropdownCOA(id) {

            $('#' + id).dropdown({
                fullTextSearch: true,
                onChange: function (value, text, $selectedItem) {

                    console.log($selectedItem);

                    var curTR = $(this).closest("tr");

                    var recordIDVotHd = curTR.find("td > .Hid-vot-list");
                    recordIDVotHd.html($($selectedItem).data("coltambah5"));

                   //var selectObj = $($selectedItem).find("td > .COA-list > select");
                    //selectObj.val($($selectedItem).data("coltambah5"));



                    var recordIDPtj = curTR.find("td > .label-ptj-list");
                    recordIDPtj.html($($selectedItem).data("coltambah1"));

                    var recordIDPtjHd = curTR.find("td > .Hid-ptj-list");
                    recordIDPtjHd.html($($selectedItem).data("coltambah5"));

                    var recordID_ = curTR.find("td > .label-kw-list");
                    recordID_.html($($selectedItem).data("coltambah2"));

                    var recordIDkwHd = curTR.find("td > .Hid-kw-list");
                    recordIDkwHd.html($($selectedItem).data("coltambah6"));

                    var recordID_ko = curTR.find("td > .label-ko-list");
                    recordID_ko.html($($selectedItem).data("coltambah3"));

                    var recordIDkoHd = curTR.find("td > .Hid-ko-list");
                    recordIDkoHd.html($($selectedItem).data("coltambah7"));

                    var recordID_kp = curTR.find("td > .label-kp-list");
                    recordID_kp.html($($selectedItem).data("coltambah4"));

                    var recordIDkpHd = curTR.find("td > .Hid-kp-list");
                    recordIDkpHd.html($($selectedItem).data("coltambah8"));


                },
                apiSettings: {
                    url: 'Transaksi_EOTs.asmx/GetVotCOA?q={query}&ptj={ptj}',
                    method: 'POST',
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    cache: false,
                    fields: {

                        value: "value",      // specify which column is for data
                        name: "text",      // specify which column is for text
                        colPTJ: "colPTJ",
                        colhidptj: "colhidptj",
                        colKW: "colKW",
                        colhidkw: "colhidkw",
                        colKO: "colKO",
                        colhidko: "colhidko",
                        colKp: "colKp",
                        colhidkp: "colhidkp",

                    },
                    beforeSend: function (settings) {
                        // Replace {query} placeholder in data with user-entered search term

                        //settings.urlData.param2 = "secondvalue";

                        settings.urlData.ptj = '<%=Session("ssusrKodPTj")%>';
                        settings.data = JSON.stringify({ q: settings.urlData.query, ptj: settings.urlData.ptj });

                        //searchQuery = settings.urlData.query;

                        return settings;
                    },
                    onSuccess: function (response) {
                        // Clear existing dropdown options
                        var obj = $(this);

                        var objItem = $(this).find('.menu');
                        $(objItem).html('');

                        // Add new options to dropdown
                        if (response.d.length === 0) {
                            $(obj.dropdown("clear"));
                            return false;
                        }

                        var listOptions = JSON.parse(response.d);

                        $.each(listOptions, function (index, option) {
                            //$(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.text));
                            $(objItem).append($('<div class="item" data-value="' + option.value + '" data-coltambah1="' + option.colPTJ + '" data-coltambah5="' + option.colhidptj + '" data-coltambah2="' + option.colKW + '" data-coltambah6="' + option.colhidkw + '" data-coltambah3="' + option.colKO + '" data-coltambah7="' + option.colhidko + '" data-coltambah4="' + option.colKp + '" data-coltambah8="' + option.colhidkp + '" >').html(option.text));
                        });

                        // Refresh dropdown
                        $(obj).dropdown('refresh');

                        if (shouldPop === true) {
                            $(obj).dropdown('show');
                        }
                    }
                }

            });

        }


     

        $('.btnPaparStaf').click(async function () {
            tbl.ajax.reload();
        });


        async function loadExistingRecordSen() {
            var record = await AjaxLoadOrderRecordSen();
           // await clearAllRows();
          //  await AddRow(null, record);
        }


        //$('.btnPaparStaf').on('click', async function () {
        //    loadExistingRecordsStaf();
        //});

    async function loadExistingRecordStaf() {
           
            var record = await AjaxLoadOrderRecordStaf();
            //await clearAllRows();
            //await AddRow(null, record);
        }

      


        function uploadFile() {
            var fileInput = document.getElementById("fileInput");
            var file = fileInput.files[0];

            if (file) {
                var fileSize = file.size; // File size in bytes
                var maxSize = 3 * 1024 * 1024; // Maximum size in bytes (3MB)

                if (fileSize <= maxSize) {
                    // File size is within the allowed limit

                    var fileName = file.name;
                    var fileExtension = fileName.split('.').pop().toLowerCase();

                    // Check if the file extension is PDF or Excel
                    if (fileExtension === 'pdf' || fileExtension === 'xlsx' || fileExtension === 'xls') {
                        var reader = new FileReader();
                        reader.onload = function (e) {



                            var fileData = e.target.result; // Base64 string representation of the file data
                            var fileName = file.name;

                            var requestData = {
                                fileData: "test",
                                fileName: fileName,
                                resolvedUrl: resolveAppUrl("~/UPLOAD/DOCUMENT/EOT/AR")
                            };

                            var frmData = new FormData();

                            frmData.append("fileSurat", $('input[id="fileInput"]').get(0).files[0]);
                            frmData.append("fileName", fileName);
                            frmData.append("fileSize", fileSize);

                            $("#hidJenDok").val(fileExtension);
                            $("#hidFileName").val(fileName);

                            $.ajax({
                                url: "Transaksi_EOTs.asmx/UploadFile",
                                type: 'POST',
                                data: frmData,
                                cache: false,
                                contentType: false,
                                processData: false,
                                success: function (response) {

                                    // Clear the file input
                                    $("#fileInput").val("");
                                    $('#uploadedFileNameLabel').empty();
                                   // $("#uploadedFileNameLabel").text(fileName);
                                    var fileLink = document.createElement("a");
                                    fileLink.href = requestData.resolvedUrl + fileName;
                                    fileLink.textContent = fileName;

                                    var uploadedFileNameLabel = document.getElementById("uploadedFileNameLabel");
                                    uploadedFileNameLabel.appendChild(fileLink);

                                    // Show the uploaded file name on the screen
                                    $("#uploadedFileNameLabel").show();

                                  

                                    $("#progressContainer").text("   File uploaded successfully.");



                                },
                                error: function () {
                                    $("#progressContainer").text("Error uploading file.");
                                }
                            });
                        };

                        reader.readAsArrayBuffer(file);
                    } else {
                        // Invalid file type
                        alert("Only PDF and Excel files are allowed.");
                    }
                } else {
                    // File size exceeds the allowed limit
                    alert("File size exceeds the maximum limit of 3MB");
                }
            } else {
                // No file selected
                alert("Please select a file to upload");
            }
        }


        function resolveAppUrl(relativeUrl) {
            // Make a separate AJAX request to the server to resolve the URL
            var resolvedUrl = "";
            $.ajax({
                type: "POST",
                url: "Transaksi_EOTs.asmx/GetBaseUrl",
                data: JSON.stringify({ relativeUrl: relativeUrl }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false, // Ensure synchronous execution for simplicity
                success: function (response) {
                    resolvedUrl = response.d;
                }
            });
            return resolvedUrl;
        }
       
        async function paparSenaraiAK(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblDataSenAK');

            if (objOrder !== null && objOrder !== undefined) {
                totalClone = objOrder.Payload.length;

            }
            // console.log(objOrder)

            while (counter <= totalClone) {


                var row = $('#tblDataSenAK tbody>tr:first').clone();
                row.attr("style", "");
                var val = "";

                $('#tblDataSenAK tbody').append(row);

                if (objOrder !== null && objOrder !== undefined) {

                    if (counter <= objOrder.Payload.length) {
                        await setValueToRow(row, objOrder.Payload[counter - 1]);
                    }
                }

                counter += 1;
            }
        }

        //async function clearAllRowsHdr() {

        //    $('#lblNoJurnal').val("");
        //    $('#txtNoRujukan').val("");
        //    $('#txtTarikh').val("");
        //    $('#txtPerihal').val("");
        //    $('#ddlJenTransaksi').empty();


        //}

        async function clearAllRows_senarai() {
            $(tableID_Senarai + " > tbody > tr ").each(function (index, obj) {
                if (index > 0) {
                    obj.remove();
                }
            })
        }

        async function clearAllRows_senarai() {
            $(tableID_SenAK + " > tbody > tr ").each(function (index, obj) {
                if (index > 0) {
                    obj.remove();
                }
            })
        }

        $(tableID).on('click', '.btnDelete', async function () {
            event.preventDefault();
            var curTR = $(this).closest("tr");
            var recordID = curTR.find("td > .data-id");
            var bool = true;
            var id = setDefault(recordID.val());

            if (id !== "") {
                bool = await DelRecord(id);
            }

            if (bool === true) {
                curTR.remove();
            }

           
            return false;
        })

     

        async function AjaxLoadOrderRecordSen() {

            try {

                const response = await fetch('Transaksi_EOTs.asmx/LoadRecordArahan', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                   // body: JSON.stringify({ id: id })
                });
                const data = await response.json();
                return JSON.parse(data.d);
              //  alert(result.Message)

            } catch (error) {
                console.error('Error:', error);
                return false;
            }
        }

        async function AjaxLoadOrderRecordStaf() {

            try {

                const response = await fetch('Transaksi_EOTs.asmx/LoadRecordStaf', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    //body: JSON.stringify({ id: id })
                    body: JSON.stringify({ })
                });
                const data = await response.json();
                return JSON.parse(data.d);
            } catch (error) {
                console.error('Error:', error);
                return false;
            }
        }




    var tbl1 = null;

    async function rowClickHandler(id) {
      
        $('#TransaksiStaf').modal('toggle');
        tbl1.ajax.reload();
       
        var bool = true;
     
        if (id !== "") {
            //BACA HEADER ARAHAN
            var recordHdr = await AjaxGetRecordHdrEot(id);
            console.log(recordHdr);
            await AddRowHeader(null, recordHdr);
        }

        return false;
    }

        //$(tableID_Senarai).on('click', '.btnView', async function () {
        //    $('#TransaksiStaf').modal('toggle');
        //    tbl1.ajax.reload();

        //    event.preventDefault();
        //    //var curTR = $(this).closest("tr");
        //    //var recordID = curTR.find("td > .lblNoArahan");
        //    //var cellData = curTR.eq(0).eq(0).html();
        //    var bool = true;
        //    //var id = recordID[0].val();
        //    var id = $(this).data("id")
        //    var id2 = $(this).attr("data-id")
            
        //    if (id !== "") {

        //        //BACA HEADER ARAHAN
               
        //        var recordHdr = await AjaxGetRecordHdrEot(id);               
        //        console.log(recordHdr);
        //        await AddRowHeader(null, recordHdr);


        //        //BACA DETAIL ARAHAN
        //       // await clearAllRows_senarai();
        //       // var record = await AjaxGetRecordDtlEot(id);
        //       // await  clearAllRows_senarai();
        //       // await AddRow(null, record);
        //    }

        //    return false;
        //})

        async function AjaxGetRecordHdrEot(id) {

            try {

                const response = await fetch('Transaksi_EOTs.asmx/LoadRecordByNoArahan', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ id: id })
                });
                const data = await response.json();
                return JSON.parse(data.d);
            } catch (error) {
                console.error('Error:', error);
                return false;
            }
        }

        async function clearAllRowsHdr() {
                      
            $('#<%=txtNoSurat.ClientID%>').val("")
            $('#<%=txtNoArahan.ClientID%>').val("")
            $('#lblPTJ').val("")
            $('#<%=txtTkhMula.ClientID%>').val("")
            $('#<%=txtTkhTamat.ClientID%>').val("")
            $('#<%=txtLokasi.ClientID%>').val("")
            $('#<%=txtButirKerja.ClientID%>').val("")
            $('#ddlPengesah').empty()
            /*$('#lblKetuaPej').val("")*/
            $('#ddlKetuaJabatan').val("")
            $('#uploadedFileNameLabel').val("")

        }

        async function AddRowHeader(totalClone, objOrder) {
            var counter = 1;
            //var table = $('#tblDataSenarai');

            if (objOrder === null || objOrder === undefined) {
                return false;
            }

            await setValueToRow_HdrArahan(objOrder[0]);
            // console.log(objOrder)
        }

        
        async function setValueToRow_HdrArahan(orderDetail) {
            $('#<%=txtNoSurat.ClientID%>').val(orderDetail.No_Surat)
            $('#<%=txtNoArahan.ClientID%>').val(orderDetail.No_Arahan)
            $('#txtNoArahanPapar').val(orderDetail.No_Arahan)           
            $('#lblPTJ').val(orderDetail.pejabat)            
            $('#<%=txtTkhMula.ClientID%>').val(orderDetail.Tkh_Mula)
            $('#<%=txtTkhTamat.ClientID%>').val(orderDetail.Tkh_Tamat)
            $('#<%=txtLokasi.ClientID%>').val(orderDetail.Lokasi)
            $('#<%=txtButirKerja.ClientID%>').val(orderDetail.PeneranganK)

        //$('#lblKetuaPej').val(orderDetail.Staf_lulusB)
        //$('#hidKetuaPej').val(orderDetail.No_Staf_SahB)
        var strFileName = orderDetail.File_name
        var fileUrl = "~/UPLOAD/DOCUMENT/EOT/AR/" + strFileName; // Replace with the actual file URL

        var fileLink = $('<a></a>');
        fileLink.attr('href', orderDetail.url);
        fileLink.text(strFileName);

        $('#uploadedFileNameLabel').empty(); // Clear any existing content
        $('#uploadedFileNameLabel').append(fileLink);

        $('#hidFileName').val(orderDetail.File_name)

        var newId = $('#ddlPengesah')

        //await initDropdownPtj(newId)
        //$(newId).api("query");

        var ddlJenTransaksi = $('#ddlPengesah')
        var ddlSearch = $('#ddlPengesah')
        var ddlText = $('#ddlPengesah')
        var selectObj_JenisTransaksi = $('#ddlPengesah')
        $(ddlPengesah).dropdown('set selected', orderDetail.No_Staf_SahB);
        selectObj_JenisTransaksi.append("<option value = '" + orderDetail.No_Staf_SahB + "'>" + orderDetail.Nama_Staf_Sah + "</option>")


        var newId = $('#ddlKetuaJabatan')

        //await initDropdownPtj(newId)
        //$(newId).api("query");

        var ddlJenTransaksi = $('#ddlKetuaJabatan')
        var ddlSearch = $('#ddlKetuaJabatan')
        var ddlText = $('#ddlKetuaJabatan')
        var selectObj_JenisTransaksi = $('#ddlKetuaJabatan')
        $(ddlKetuaJabatan).dropdown('set selected', orderDetail.No_Staf_SahB);
        selectObj_JenisTransaksi.append("<option value = '" + orderDetail.No_Staf_LulusB + "'>" + orderDetail.Nama_Staf_LulusB + "</option>")

        var ddl = $(".COA-list");    //ddl tu adalah parent bagi class COA_list..perlu baca parent tuk dapat object
        var ddlSearch = ddl.find("td > .search");
        var ddlText = ddl.find(".text");
        var selectObj = ddl.find("select");
        $(ddl).dropdown('set selected', orderDetail.Kod_Vot);
        selectObj.append("<option value = '" + orderDetail.Kod_Vot + "'>" + orderDetail.ButiranVot + "</option>")

        var butirvot = $(".label-vot-list");
        butirvot.html(orderDetail.ButiranVot);

        var hidlblvot = $(".hid-vot-list");
        hidlblvot.html(orderDetail.Kod_Vot);

        var butirptj = $(".label-ptj-list");
        butirptj.html(orderDetail.pejabat);

        var hidbutirptj = $(".Hid-ptj-list");
        hidbutirptj.html(orderDetail.colhidptj);



        var butirKW = $(".label-kw-list");
        butirKW.html(orderDetail.colKW);

        var hidbutirkw = $(".Hid-kw-list");
        hidbutirkw.html(orderDetail.colhidkw);

        var butirKo = $(".label-ko-list");
        butirKo.html(orderDetail.colKO);

        var hidbutirko = $(".Hid-ko-list");
        hidbutirko.html(orderDetail.colhidko);

        var butirKp = $(".label-kp-list");
        butirKp.html(orderDetail.colKp);

        var hidbutirkp = $(".Hid-kp-list");
        hidbutirkp.html(orderDetail.colhidkp);

        loadExistingRecordStaf()

    }



    $(tableID_SenAK).on('click', '.btnView2', async function () {
        event.preventDefault();
        var curTR = $(this).closest("tr");
        var recordID = curTR.find("td > .lblNo2");


        return false;
    })



    $(tblDataSenAK_trans).on('click', '.btnView3', async function () {

        event.preventDefault();

        var bool = true;
        //var id = recordID[0].val();
        var id = $(this).data("id")
        var id2 = $(this).attr("data-id")
        var id1 = $(this).closest('tr').find('td:first-child input').val();
        if (id !== "") {


            //var recordHdr = await AjaxGetRecDelDtlEot(id, id1);
            bool = await DelRecord(id, id1);

        }

        if (bool === true) {
            id.remove();
        }

        return false;
    })

    async function DelRecord(id, id1) {
        var bool = false;

        //var result_ = id.split('|');
        //var nobil = result_[0];
        //var noitem = result_[1];
        //console.log(nobil, noitem)

        var result = JSON.parse(await AjaxGetRecDelDtlEot(id, id1));

        if (result.Code === "00") {
            bool = true;
        }

        return bool;
    }

    async function AjaxGetRecDelDtlEot(id, id1) {

        try {

            const response = await fetch('Transaksi_EOTs.asmx/LoadDeleteStafAK', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: id, id1: id1 })
            });
            const data = await response.json();
            // return JSON.parse(data.d);

            var result = JSON.parse(data.d);
            alert(result.Message)


        } catch (error) {
            console.error('Error:', error);
            return false;
        }
    }

    async function AjaxGetRecordDtlEot(id) {

        try {

            const response = await fetch('Transaksi_EOTs.asmx/LoadRecordStafArahan', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: id })
            });
            const data = await response.json();
            return JSON.parse(data.d);
        } catch (error) {
            console.error('Error:', error);
            return false;
        }
    }

</script>

</asp:Content>
