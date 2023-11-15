<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PelarasanPP.aspx.vb" Inherits="SMKB_Web_Portal.PelarasanPP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
<style>
          #tblDataSenarai td:hover {
            cursor: pointer;
          }

        .ui.search.dropdown {
            height: 40px;
        }

        .tabcontent {
            padding: 0px 20px 20px 20px !important;
        }

        .table-title {
            padding-top: 0px !important;
            padding-bottom: 0px !important;
        }

        .custom-table > tbody > tr:hover {
            background-color: #ffc83d !important;
        }

        #tblDataSenarai_trans td:hover {
            cursor: pointer;
        }


        .default-primary {
            background-color: #007bff !important;
            color: white;
        }


        /*start sticky table tbody tfoot*/
        table {
            overflow: scroll;
            border-collapse: collapse;
            color: white;
        }

        .secondaryContainer {
            overflow: scroll;
            border-collapse: collapse;
            height: 500px;
            border-radius: 10px;
        }

        .sticky-footer {
            position: sticky;
            bottom: 0;
            background-color: white;
            z-index: 2; 
        }

        .sticky-footer th,
        .sticky-footer td {
            text-align: center; /* Center-align the content in footer cells */
            border-top: 1px solid #ddd; /* Add a border at the top to separate from data rows */
            padding: 10px; /* Adjust padding as needed */
        }

        #showModalButton:hover {
            background-color: #ffc107; /* Change background color on hover */
            color: #fff; /* Change text color on hover */
            border-color: #ffc107; /* Change border color on hover */
            cursor: pointer; /* Change cursor to indicate interactivity */
        }

        /*input CSS */
        .input-group {
            margin-bottom: 20px;
            position: relative;
        }

        .input-group__label {
            display: block;
            position: absolute;
            top: 0;
            line-height: 40px;
            color: #aaa;
            left: 5px;
            padding: 0 5px;
            transition: line-height 200ms ease-in-out, font-size 200ms ease-in-out, top 200ms ease-in-out;
            pointer-events: none;
        }

        .input-group__input {
            width: 100%;
            height: 40px;
            border: 1px solid #dddddd;
            border-radius: 5px;
            padding: 0 10px;btnPapar
        }

        .input-group__input_RM {
            width: 100%;
            text-align :right;
            height: 40px;
            border: 1px solid #dddddd;
            border-radius: 5px;
            padding: 0 10px;
        }

        .input-group__input:not(:-moz-placeholder-shown) + label {
            background-color: white;
            line-height: 10px;
            opacity: 1;
            font-size: 10px;
            top: -5px;
        }

        .input-group__input:not(:-ms-input-placeholder) + label {
            background-color: white;
            line-height: 10px;
            opacity: 1;
            font-size: 10px;
            top: -5px;
        }

        .input-group__input:not(:placeholder-shown) + label, .input-group__input:focus + label {
            background-color: white;
            line-height: 10px;
            opacity: 1;
            font-size: 10px;
            top: -5px;
        }

        .input-group__input:focus {
            outline: none;
            border: 1px solid #01080D;
        }

        .input-group__input:focus + label {
            color: #01080D;
        }


        .input-group__select + label {
            background-color: white;
            line-height: 10px;
            opacity: 1;
            font-size: 10px;
            top: -5px;
        }

        .input-group__select:focus + label {
            color: #01080D;
        }

        /* Styles for the focused dropdown */
        .input-group__select:focus {
            outline: none;
            border: 1px solid #01080D;
        }


        .input-group__label-floated {
            /* Apply styles for the floating label */
            /* For example: */
            top: -5px;
            font-size: 10px;
            line-height: 10px;
            color: #01080D;
            opacity: 1;
        }

        .input-group__subTitle_lable {
            background-color: white;
            line-height: 10px;
            opacity: 1;
            font-size: 15px;
            top: -5px;
        }
         .btn-change1{
            height: 50px;
            width: 150px;
            background: #FFC83D;
            margin: 20px;
            float: left;
            border: 0px;
            color: #000;
            box-shadow: 0 0 1px #ccc;
            -webkit-transition-duration: 0.5s;
            -webkit-box-shadow: 0px 0px 0 0 #31708f inset , 0px 0px 0 0 #31708f inset;
        }
        .btn-change1:hover{
            -webkit-box-shadow: 50px 0px 0 0 #31708f inset , -50px 0px 0 0 #31708f inset;
        }

        .btn-change{
            height: 35px;
            width: 170px;
            background: #FFC83D;
            margin: 20px;
            float: left;
            box-shadow: 0 0 1px #ccc;
            -webkit-transition: all 0.5s ease-in-out;
            border: 0px;
            border-radius: 8px;
            color: #000;
        }
        .btn-change:hover{
            -webkit-transform: scale(1.1);
            background: #FFC83D;
             color: white;
            
        }
        .nav-tabs .nav-item.show .nav-link, .nav-tabs .nav-link.active {
            color: #000;
            font-weight:normal;
            background-color: #FFC83D;
        }

        .tab-pane {
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-bottom: 1px solid #ddd;
            border-radius: 0px 0px 5px 5px;
            padding: 10px;
        }

        .nav-tabs .nav-link {
            padding: 0.25rem 0.5rem;
        }       
    </style>

     <!-- Modal PermohonanList -->
        <div class="modal fade" id="SenaraiPermohonan" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Senarai Permohonan</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="panel-body" style="overflow-x: auto">
                        <br />
                     <div class="col-md-12">            
                       <div class="form-group row">                        
                            <div class="col-md-6">
                              <input type="text" id="txtNama" name="txtNama"  class="input-group__input" placeholder="&nbsp;"  readonly style="background-color: #f0f0f0" />
                              <Label class="input-group__label" for="Nama">Nama</Label>                  
                            </div>
                            <div class="col-md-6">
                              <input type="text" id="txtNoStaf" name="txtNoStaf" class="input-group__input" placeholder="&nbsp;"  readonly style="background-color: #f0f0f0" />
                                    <Label class="input-group__label" for="NoStaf">NoStaf</Label>                  
                            </div>
                       </div>  
                </div>
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
                                <div class="col-md-12">
                                    <div class="form-row">
                                        <div class="form-group col-md-6"> 
                                            <input type="date" id="txtTarikhStart" name="txtTarikhStart" style="display:none;" class="input-group__input form-control input-sm">
                                            <label class="input-group__label" id="lblMula" for="Mula" style="display:none;">Mula: </label>
                                        </div>                                        
                                                                             
                                        <div class="form-group col-md-6">
                                            <input type="date" id="txtTarikhEnd" name="txtTarikhEnd" style="display:none;" class="input-group__input form-control input-sm">
                                            <label class="input-group__label" id="lblTamat" for="Tamat" style="display:none;">Tamat: </label>
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                  <%-- tutup filtering--%>

                    <div class="modal-body">
                        <div class="col-md-12">
                            <div class="transaction-table table-responsive">
                                <table id="tblDataSenarai" class="table table-striped" style="width:100%">
                                    <thead>
                                        <tr>
                                            <th scope="col">No. Permohonan</th>
                                            <th scope="col">Tarikh Mohon</th>
                                            <th scope="col">Nama Pemohon</th>
                                            <th scope="col">Tujuan</th>
                                            <th scope="col">Jumlah Mohon (RM)</th>
                                            <th scope="col">Status Terkini </th>                                            

                                        </tr>
                                    </thead>
                                    <tbody id="tableID_SenaraiPermohonan">
                                        
                                    </tbody>

                                </table>                                

                            </div>
                        </div>                  
                    </div>
                </div>
            </div>
        </div>

    <%--body--%>

    <div class="col-md-10">
       <div class="form-row">
       <div class="form-group col-md-6">                        
        <label class="input-group__subTitle_lable"><b>Sila Pilih Jenis Permohonan</b></label> &nbsp;
        <input type="radio" id="Sendiri" name="JnsMohon" value="SENDIRI" checked>
        <label for="html">Sendiri</label>&nbsp;&nbsp; 
        <input type="radio" id="StafLain" name="JnsMohon" value="STAFLAIN">
                                     <label for="html">Staf Lain</label> &nbsp;&nbsp;&nbsp;
    
        </div>
           <div class="form-group col-md-4">
            <div id="cariStaf" style="display:none">
                <select class="input-group__select ui search dropdown cr-staf" name="ddlStaf" id="ddlStaf" placeholder="&nbsp;" ></select>
                <label class="input-group__label" for="Pilih Staf">Pilih Staf</label> 
           </div>
         </div>  
       </div>
    </div>       

     <!-- Trigger the modal with a button -->
         <div class="modal-body">
            <div class="table-title">
                <%--<h6>Permohonan Pelbagai  </h6>--%>
                <button type="button"  class="btn-change" data-toggle="modal" data-target="#myPersonal">Maklumat Pegawai</button> 
                <div class="btn btn-primary btnPapar" onclick="ShowPopup('2')">
                    Senarai Permohonan 
                </div>
            </div>
              <div class="form-row">
            
            </div>
             </div>

 

<div class="container-fluid"> 
  <ul class="nav nav-tabs" id="myTab" role="tablist">  
    <li class="nav-item" role="presentation"><a class="nav-link active"  data-toggle="tab" id="tab-permohonan" href="#menu1">Info Permohonan</a></li>
    <li class="nav-item" role="presentation"><a class="nav-link" id="tab-Perbelanjaan" data-toggle="tab" href="#menu2">Ringkasan Perbelanjaan</a></li>
    <li class="nav-item" role="presentation"><a class="nav-link" id="tab-Rumusan" data-toggle="tab" href="#menu3">Rumusan </a></li>
  </ul>
  

  <div class="tab-content">
   <div id="menu1" class="tab-pane fade show active" role="tabpanel" aria-labelledby ="tab-permohonan">
        <div class="panel panel-default">
        <div class="panel-heading"></div>
        <div class="panel-body"> 
      <br />                                     

       <div class="col-md-12"> 
           <div class="form-row">
               <div class="form-group col-md-3" style="left: -1px; top: 0px">                    
                   <input type="text" id="noPermohonan"  class="input-group__input form-control input-md" style="background-color: #f0f0f0"  readonly>
                   <label class="input-group__label" for="No.Permohonan" >No.Permohonan</label>
               </div>                
               <div class="form-group col-md-3">
                   <input type="date"  id="tkhMohon" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" />  
                   <label class="input-group__label" for="Tarikh Mohon"> Tarikh Mohon:</label>                                      
               </div>  
               <div class="form-group col-md-3">
                    <select name="ddlBulan" id="ddlBulan"  class="input-group__select ui search dropdown bln-tuntut" placeholder="&nbsp;"></select> 
                    <label class="input-group__label" for="Bulan Tuntut"> Bulan Tuntut</label> 
                </div>
               <div class="form-group col-md-3">
                 <select name="ddlTahun" id="ddlTahun"  class="input-group__select ui search dropdown tahun-tuntut" placeholder="&nbsp;"></select> 
                 <label class="input-group__label" for="Tahun Tuntut"> Tahun Tuntut</label> 
             </div>

               </div>
           </div>

           <div class="col-md-12">                   
               <div class="form-row">
                   <div class="form-group col-md-3">
                        <select name="ddlKumpWang" id="ddlKumpWang"  class="input-group__select ui search dropdown jnsKump_Wang-list" placeholder="&nbsp;"></select>
                       <label class="input-group__label" for="Kumpulan Wang">Kumpulan Wang</label>              
                   </div>
                   <div class="form-group col-md-3">
                        <select name="ddlKO" id="ddlKO"  class="input-group__select ui search dropdown jnsKO-list" placeholder="&nbsp;"></select> 
                       <label class="input-group__label" for="Kod Operasi"> Kod Operasi</label> 
                    </div>
                    <div class="form-group col-md-3">
                        <select name="ddlKPtj" id="ddlKPtj"  class="input-group__select ui search dropdown jnsKPtj-list" placeholder="&nbsp;"></select>
                       <label class="input-group__label" for="Kod Projek">Kod PTj</label>                 
                   </div>
                   <div class="form-group col-md-3">
                        <select name="ddlKP" id="ddlKP"  class="input-group__select ui search dropdown jnsKP-list" placeholder="&nbsp;"></select> 
                       <label class="input-group__label" for="Kod Projek"> Kod Projek</label>                                                                          
                   </div>
              
               </div>         
           </div>
             
           <div class="col-md-12">
               <div class="form-row">  
                 <div class="form-group col-md-6">                                         
                   <textarea rows="2" cols="45" ID="txtTujuan" name="Tujuan Tuntutan" class="input-group__input form-control" placeholder="&nbsp;" MaxLength="500"></textarea>
                   <label class="input-group__label" for="Tujuan Perjalanan">Tujuan Tuntutan</label>  
                </div>
               </div>   
           </div>
            <br /> 
                 

             <div class="table-title">
             <br />
                    <h6> &nbsp;&nbsp;Senarai Pendahuluan Pelbagai Yang Telah Diterima</h6>   
             <hr />
               <%--<button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Proses">Proses</button>--%>
             <br /> 
         </div>
                <div class="form-row">
               <div class="col-md-12">
                   <div class="transaction-table table-responsive">
                       <table id="tblListPend" class="table table-striped" style="width:98%">
                           <thead>
                               <tr>
                                  <th><input type="checkbox" name="select_all" value="1" id="example-select-all"></th>
                                   <th scope="col" style="width:25%">No Pendahuluan</th>
                                    <th scope="col" style="width:35%">Program</th>
                                   <th scope="col" style="width:15%">Jumlah Cek (RM)</th>
                                   <th scope="col" style="width:20%">No Baucer</th>                                        
                               </tr>
                           </thead>
                           <tbody id="tableID_ListPend">                                        
                           </tbody>
                       </table>
                   </div>
               </div> 
                    <br />
                <div class="col-md-8"> 
                    <div class="form-row">                                        
                    <textarea rows="2" cols="45" ID="txtsebab" name="txtsebab" class="input-group__input form-control" placeholder="&nbsp;" MaxLength="500"></textarea>
                    <label class="input-group__label" for="Sebab">Sebab-sebab kelewatan menghantar permohonan (jika ada)</label>  
                    </div>
                </div>   
       </div>


    </div>   <%--div panel_body--%>
</div>  <%--div panel--%>
      
<div class="form-row">
    <div class="form-group col-md-12" align="right">                   
        <button type="button" class="btn btn-secondary btnSimpan">Simpan</button>
    </div>
</div>

    </div>
    <br />
   <div id="menu2" class="tab-pane fade" aria-labelledby ="tab-Perbelanjaan" role="tabpanel">

       <div class="col-md-12"> 
    <div class="form-row">
        <div class="form-group col-md-3" style="left: -1px; top: 0px">
            <input type="text" id="txtMohonID2" class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3" >
            <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                
        </div>                    
        <div class="form-group col-md-3">
             <input type="date" id="tkhMohon2" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly />                                   
            <label for="TarikhMohon" class="input-group__label">Tarikh Mohon:</label>               
        </div>      <div class="col-md-12">        
        <div class="form-row">
        <input type ="hidden" id="selectedNoPendahuluan"/>
            <input type ="hidden" id="selectedJumlah"/>
            <input type ="hidden" id="monthInt"/>
        </div>
        </div>
    </div>
</div>
       
  
        <table  class="table table-striped" id="tblData2" style="width: 100%">
            <thead>
                <tr>                    
                    <th style="width: 20%" scope="col">Senarai Item</th>                                
                    <th style="width: 10%" scope="col">Kuantiti</th>
                    <th style="width: 10%" scope="col">Harga</th>
                    <th style="width: 20%" scope="col">No. Resit</th>
                    <th style="width: 40%" scope="col">Upload Resit</th>
                </tr>
            </thead>
            <tbody id="tableID2">
                <tr style="display: none; width: 100%" class="table-list">
                    <td style="width: 20%">                             
                        <input type="text"  ID="txtNamaItem"  class="list-NamaItem form-control"/><%-- //style="visibility: hidden"--%>
                        <label id="hidItem" name="hidItem" class="hidItemNO" ></label>
                    </td>
                    <td style="width: 10%">
                        <input id="txtKuantiti" name="txtKuantiti" type="text" class="list-kuantiti" style="text-align: right"  />
                    </td>
                    <td style="width: 10%">
                        <input id="txtHarga" name="txtHarga" type="text" class="list-harga" style="text-align: right" />
                    </td>
                    <td style="width: 20%">
                        <input id="txtResit" name="txtResit"  type="text" class="list-resit"  />
                    </td>                     
                    <td style="width: 40%">                        
                     <div class="input-group col-md-10">    
                     <div class="form-inline">
                        <input type="file" id="fileInputSurat" class="fileInputSurat" style="width:250px" />
                         <a href ="#" class="tempFile" target="_blank"></a>
                        <input type="button" class="btn btn-primary uploadBtnSurat" onclick="return uploadSurat(this);" id="uploadBtnSurat" value="Upload" />
                        <span id="uploadSurat" style="display: inline;"></span>
                        <span id="">&nbsp</span>
                        <span id="progressContainer"></span>
                        <input type ="hidden" id="txtNamaFile" />
                        <input type="hidden" class="form-control"  id="hidFolder" style="width:200px" readonly="readonly" /> 
                        <input type="hidden" class="form-control"  id="hidFileName" style="width:200px" readonly="readonly" /> 
                        </div>         
                    </div>
                    </td>

                </tr>
            </tbody>
            
            <tfoot>  
                <tr>
                    
                    <td colspan="2">
                        <%--<input class="form-control underline-input" id="totalDbt" name="totalDbt" style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly />--%>
                        <label style="font-size:medium; align-items:center"> Jumlah (RM) </label>
                    </td>
                    <td>
                        <input class="form-control underline-input" id="totalKt" name="totalKt" style="text-align: right; font-weight: bold" width="8%" value="0.00" readonly /></td>
                    <td></td>
                </tr>
                  <tr>
                    <td colspan="2">
                        <div class="btn-group">
                            <button type="button" class="btn btn-warning btnAddRow One" data-val="1" value="1"><b>+ Tambah</b></button>
                            <button type="button" class="btn btn-warning btnAddRow dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <div class="dropdown-menu">
                                <a class="dropdown-item btnAddRow five" value="5" data-val="5">Tambah 5</a>
                                <a class="dropdown-item btnAddRow" value="10" data-val="10">Tambah 10</a>

                            </div>
                        </div>
                    </td>                   
                </tr>
            </tfoot>
        </table>
       <br />
       <div class="form-row">
     <div class="form-group col-md-12" align="right">
          <button type="button" class="btn btn-secondary btnSimpan2" id="btnSimpanTblData2">Simpan</button>
     </div>
 </div>  

</div>
<br />
<div id="menu3" class="tab-pane fade" aria-labelledby="tab-Rumusan" role="tabpanel">
    <div class="col-md-12"> 
        <div class="form-row">
            <div class="form-group col-md-3" style="left: -1px; top: 0px">
                <input type="text" id="txtMohonID3" class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3" >
                <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                
            </div>                    
            <div class="form-group col-md-3">
                 <input type="date" id="tkhMohon3" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly />                                   
                <label for="TarikhMohon" class="input-group__label">Tarikh Mohon:</label>               
            </div>     
        </div>
    </div>
    <br />
    <asp:Panel ID="Panel3" runat="server"> 
        <div class="col-md-12"> 
            <div class="form-row">
                <u>Rumusan Tuntutan</u>
            </div>
        </div>
        <div class="col-md-12"> 
            <div class="">
                <table class="table">
                    <tr>
                        <td>Pendahuluan Telah Diberi Sebelum Ini</td>
                        <td>
                             <div class="form-group col-md-10">
                              <input type="text" ID="txtPendDiberi" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;"  readonly />
                              <label class="input-group__label" for="RM">RM</label>  
                             </div>
                        </td>
                    </tr> 
                    <tr>
                        <td>Tolak Pemulangan</td>
                        <td>
                            <div class="form-group col-md-10">
                             <input type="text" ID="txtTolakPulang" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;"  readonly />
                             <label class="input-group__label" for="RM">RM</label>  
                            </div>
                        </td>
                    </tr> 
                    <tr>
                        <td>Tolak Jumlah Tuntutan Pelbagai Sekarang</td>
                        <td>
                             <div class="form-group col-md-10">
                             <input type="text" ID="txtTolakJumTunt" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;"  readonly />
                             <label class="input-group__label" for="RM">RM</label>  
                            </div>
                        </td>
                    </tr> 
                    <tr>
                        <td>JUMLAH BESAR BAKI TUNTUTAN/BAKI PEMBAYARAN BALIK</td>
                        <td>    
                            <div class="form-group col-md-10">
                            <input type="text" ID="txtBakiBesar" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;"  readonly />
                            <label class="input-group__label" for="RM">RM</label>  
                           </div>
                        </td>
                    </tr>                    

                </table>
            </div>
        </div>
    </asp:Panel>

    <div class="form-row">
     <div class="form-group col-md-12" align="right">
          <button type="button" class="btn btn-secondary btnSimpanFinal" id="btnSimpanFinal">Simpan</button>
     </div>
 </div>  
</div>

     

</div>



      <%-- <div class="modal-footer">
       <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
       </div>--%>
   

</div>





    <div id="myPersonal" class="modal fade" role="dialog">
       <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
          
       <!-- Modal content-->
       <div class="modal-content">
       <div class="modal-header"><h4>Maklumat Pegawai</h4>
       <button type="button" class="close" data-dismiss="modal"></button>
       <h4 class="modal-title"></h4> 
       </div>
       <div class="modal-body">
          
                <asp:Panel ID="Panel4" runat="server" >
                            <div class="form-row">                                     
                               <div class="form-group col-sm-6">
                                   <input type="text" id="txtNamaP"  class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"  />
                                   <label class="input-group__label" for="Nama">Nama</label>                                        
                                    <%--<asp:TextBox ID="txtNamaP" runat="server" Width="100%" class="form-control input-sm" style="background-color:#f3f3f3"></asp:TextBox>--%>
                               </div>                                    
                               <div class="form-group col-sm-6">
                                   <input type="text" ID="txtNoPekerja"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"/>                                       
                                   <label class="input-group__label" for="No.Pekerja">No.Pekerja</label>                                        
                                    <input type="hidden" ID="hidPtjPemohon" />
                               </div>                               
                          </div>

                           <div class="form-row">
                               <div class="form-group col-sm-6">
                                   <input type="text" ID="txtJawatan"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"   />                                        
                                   <label class="input-group__label" for="Jawatan">Jawatan</label>                                        
                               </div>
                               <div class="form-group col-sm-6">
                                   <input type="text" ID="txtGredGaji" Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />                                       
                                   <label class="input-group__label" for="Gred Gaji">Gred Gaji</label>                                        
                               </div>
                           </div>

                            <div class="form-row">
                               <div class="form-group col-sm-6">
                                   <input type="text" ID="txtPejabat"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"/> 
                                   <label class="input-group__label" for="Pejabat">Pejabat/Jabatan/Fakulti</label>                                        
                                   <%-- <asp:TextBox ID="txtPejabat" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                               </div>
                               <div class="form-group col-sm-6">
                                   <input type="text"  ID="txtKump"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"/>
                                   <label class="input-group__label" for="Kumpulan">Kumpulan</label>                                        
                                  <%--<asp:TextBox ID="txtKump" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                               </div>
                           </div>

                          <div class="form-row">
                               <div class="form-group col-sm-6">
                                   <input type="text"  ID="txtMemangku"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"/>
                                   <label class="input-group__label" for="Memangku Jawatan">Memangku Jawatan</label>                                        
                                   <%-- <asp:TextBox ID="txtMemangku" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                               </div>
                               <div class="form-group col-sm-6">
                                   <input type="text"  ID="txtTel"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"/>
                                   <label class="input-group__label" for="Samb. Te">Samb. Tel</label>                                       
                                  <%--<asp:TextBox ID="txtTel" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                               </div>
                           </div>
               </asp:Panel>
       </div>
       <div class="modal-footer">
       <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
       </div>
       </div>

       </div>
   </div>

    


     <script type="text/javascript">
         var tbl = null
         var tbl2 = null
         var shouldPop = true;
         const dateInput = document.getElementById('tkhMohon');
         document.getElementById("tkhMohon").disabled = true;
         var isClicked = false;
         var curNumObject = 0;

         // ✅ Using the visitor's timezone
         dateInput.value = formatDate();

         function formatDate(date = new Date()) {
             return [

                 date.getFullYear(),
                 padTo2Digits(date.getMonth() + 1),
                 padTo2Digits(date.getDate()),
             ].join('-');
         }

         function padTo2Digits(num) {
             return num.toString().padStart(2, '0');
         }

         function ShowPopup(elm) {

             if (elm == "1") {
                 $('#permohonan').modal('toggle');


             }
             else if (elm == "2") {
                 $(".modal-body div").val("");
                 $('#SenaraiPermohonan').modal('toggle');

             }
         }
         
         $(function () {
             //Reference the DropDownList.
             var ddlYears = $("#ddlTahun");

             //Determine the Current Year.
             var currentYear = (new Date()).getFullYear();
             var tempTahunS = currentYear - 1
             var tempTahun = currentYear + 1 

             //Loop and add the Year values to DropDownList.
             for (var i = tempTahunS; i <= tempTahun; i++) {
                 var option = $("<option />");
                 option.html(i);
                 option.val(i);
                 ddlYears.append(option);
             }
         });

         $(function () {
             //Reference the DropDownList.
             var ddlBulan = $("#ddlBulan")
             //var monthInt

             //Determine the Current Year.
             var currentMonth = new Date().getMonth() - 1
             var totalMonths = 11            
             var monthNames = ["Januari", "Februari", "Mac", "April", "Mei", "Jun", "Julai", "Ogos", "September", "Oktober", "November", "Disember"]

             // Loop and add the Month values to DropDownList.
             for (var month = 0; month <= totalMonths; month++) {
                 $(ddlBulan).append('<option value="' + (month + 1) + '">' + monthNames[month] + '</option>');
             }
             // Event handler for when the selection changes.
             $(ddlBulan).change(function () {
                 // Get the selected value as an integer.
                 var selectedMonthInt = parseInt($(this).val());

                 // You now have the selected month as an integer in selectedMonthInt.
                 console.log("Selected Month as Integer: " + selectedMonthInt);
                 $('#ddlBulan').val(selectedMonthInt);
                 console.log($('#ddlBulan').val())
             });

             // Set the initial selected value based on the current month.
             $(ddlBulan).val(currentMonth + 1); // Adding 1 because months are 1-based.

             
         });

  


         $(document).ready(function () {
             console.log("818")
             
             getDataPeribadi();
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
                     "url": "PelarasanPP_WS.asmx/LoadOrderRecord_PermohonanSendiri",
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
                             staffP: $('#txtNoPekerja').val()
                             //staffP: '<%'=Session("ssusrID")%>'
                            
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
                         "data": "No_Tuntutan",
                         render: function (data, type, row, meta) {

                             if (type !== "display") {
                                 return data;
                             }

                             var link = `<td style="width: 10%" >
                                    <label  name="noTuntutan"  value="${data}" >${data}</label>
                                                <input type ="hidden" class = "noTuntutan" value="${data}"/>
                                            </td>`;
                             return link;
                         }
                     },
                     { "data": "Tarikh_Mohon" },
                     { "data": "NamaPemohon" },
                     { "data": "Tujuan_Tuntutan" },
                     {
                         "data": "Jum_Pendahuluan",
                         render: function (data, type, full) {
                             return parseFloat(data).toFixed(2);
                         }

                     },
                     { "data": "Butiran" }
                    
                 ],
                 "columnDefs": [
                     { "targets": [4], "className": "right-align" }
                 ],
             });            
         });

         $(document).ready(function () {
             console.log("525")
             tbl2 = $("#tblListPend").DataTable({   //tbl load senarai permohonan PP
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
                     "url": "PelarasanPP_WS.asmx/LoadRecord_PermohonanPP",
                     type: 'POST',
                     "contentType": "application/json; charset=utf-8",
                     "dataType": "json",
                     "dataSrc": function (json) {
                         //var data = JSON.parse(json.d);
                         //console.log(data.Payload);
                         return JSON.parse(json.d);
                     },

                     data: function () {
                            return JSON.stringify({                     
                                //staffP: '<%=Session("ssusrID")%>'
                              staffP: $('#txtNoPekerja').val()
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
                         console.log("data1");

                         rowClickHandlerKeperluan(data);
                     });

                 },
                 "columns": [
                     {
                         "data": "No_Pendahuluan",
                         'targets': 0,
                         'searchable': false,
                         'orderable': false,
                         render: function (data, type, row, meta) {
                             if (type !== "display") {
                                 return data;

                             }
                             var checked = "";

                             var link = ` <input type="checkbox" data-nomohon="${data}" onclick="return setActiveNoPendahuluan(this);" name="checkPP" class = "checkPP" id="checkPP" class="checkSingle" ${checked} />`;
                             return link;
                         }
                     },
                     {
                         "data": "No_Pendahuluan",
                         render: function (data, type, row, meta) {

                             if (type !== "display") {
                                 return data;
                             }

                             var link = `<td style="width: 10%" >
                      <label  name="noPermohonan1"  value="${data}" >${data}</label>
                        <input type ="hidden" class = "noPermohonan1" value="${data}"/>
                        </td>`;
                             return link;
                         }
                     },
                     {
                         "data": "Tujuan"
                     },
                     {
                         "data": "Jum_Lulus",
                         render: function (data, type, full) {
                             return parseFloat(data).toFixed(2);
                         }
                     },
                     { "data": "No_Baucar" }
                 

                 ],
                 "columnDefs": [
                     { "targets": [3], "className": "right-align" }
                 ],
             });
         });

         function setActiveNoPendahuluan(obj) {
             $('#selectedNoPendahuluan').val($(obj).data("nomohon")); 
            
            
         }

         function rowClickHandlerKeperluan(orderDetail) {
             $('#selectedNoPendahuluan').val(orderDetail.No_Pendahuluan)
             $('#selectedJumlah').val(orderDetail.Jum_Lulus)
            
         }

         //var tbl2 = "#tblListPend"
         ////Ni digunakan untuk papar senarai permohonan, kemudian klik event pada gridview untuk papar data pada form
         //$('#tbl2').on('click', '.btnView', async function () {
         //    //event.preventDefault();
         //    alert("masuk");
         //    var curTR = $(this).closest("tr");
         //    var recordID = curTR.find("td > .btnView");
         //    //var bool = true;
         //    var id = recordID.val();
         //    alert(id)
         //    //if (id !== "") {

         //    //    //BACA HEADER JURNAL
         //    //    var recordHdr = await AjaxGetRecordHdrPermohonan(id);
         //    //    console.log(recordHdr);
         //    //    await setValueToRow_HdrPermohonan(recordHdr.Payload);
         //    //    setValueToRow_Transaksi
         //    //    //await AddRowHeader(null, recordHdr);                     
         //    //}

         //    return false;
         //})


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


         $('.btnPapar').click(function () {
             console.log("1085");
             tbl.ajax.reload();
             tbl2.ajax.reload();
         });

         $('.btnSearch').click(async function () {
             isClicked = true;
             tbl.ajax.reload();
         })


         
         function rowClickHandler(orderDetail) {
             //clearAllRows();
             console.log("rowclick")
             console.log(orderDetail.Nopemohon);
             $('#SenaraiPermohonan').modal('toggle') 
             getDataPeribadiPemohon(orderDetail.Nopemohon)
               
             
             $('#noPermohonan').val(orderDetail.No_Tuntutan)                
             $('#hidPtjPemohon').val(orderDetail.PTj)
             $('#ddlBulan').val(orderDetail.Bulan_Tuntut)
             $('#ddlTahun').val(orderDetail.Tahun_Tuntut)
             $('#txtTujuan').val(orderDetail.Tujuan_Tuntutan)
             $('#tkhMohon').val(orderDetail.Tarikh_Mohon)               
             //$('#ddlKPtj').val(orderDetail.ButiranPTJ)
             //$('#ddlKumpWang').val(orderDetail.colKW)
             //$('#ddlKO').val(orderDetail.colKO)
             //$('#ddlKP').val(orderDetail.colKp)
             $('#selectedNoPendahuluan').val(orderDetail.No_Pendahuluan)
             $('#selectedJumlah').val(orderDetail.Jum_Pendahuluan)

             //ni digunakan untuk reload semula datatable  tblListPend n check nopendahuluan yang telah disimpan
             $('#tblListPend > tbody > tr').each(function (ind, obj) {  
                 $row = $(obj);
                 $checkBox = $row.eq("0").find("input");
                 if ($checkBox.data("nomohon") === $('#selectedNoPendahuluan').val()) {
                     $checkBox.prop("checked", true);
                 } else {
                     $checkBox.prop("checked", false);
                 }
             })

             var newId = $('#ddlKumpWang')
             var ddlKumpWang = $('#ddlKumpWang')
             var ddlSearch = $('#ddlKumpWang')
             var ddlText = $('#ddlKumpWang')
             var selectObj_KumpWang = $('#ddlKumpWang')
             $(ddlKumpWang).dropdown('set selected', orderDetail.Kod_Kump_Wang);
             selectObj_KumpWang.append("<option value = '" + orderDetail.Kod_Kump_Wang + "'>" + orderDetail.colKW + "</option>")

             var newId = $('#ddlKO')
             var ddlKO = $('#ddlKO')
             var ddlSearch = $('#ddlKO')
             var ddlText = $('#ddlKO')
             var selectObj_ddlKO = $('#ddlKO')
             $(ddlKO).dropdown('set selected', orderDetail.Kod_Operasi);
             selectObj_ddlKO.append("<option value = '" + orderDetail.Kod_Operasi + "'>" + orderDetail.colKO + "</option>")

             var newId = $('#ddlKP')
             var ddlKP = $('#ddlKP')
             var ddlSearch = $('#ddlKP')
             var ddlText = $('#ddlKP')
             var selectObj_ddlKP = $('#ddlKP')
             $(ddlKP).dropdown('set selected', orderDetail.Kod_Projek);
             selectObj_ddlKP.append("<option value = '" + orderDetail.Kod_Projek + "'>" + orderDetail.colKp + "</option>")

             var newId = $('#ddlKPtj')
             var ddlKPtj = $('#ddlKPtj')
             var ddlSearch = $('#ddlKPtj')
             var ddlText = $('#ddlKPtj')
             var selectObj_ddlKPtj = $('#ddlKPtj')
             $(ddlKPtj).dropdown('set selected', orderDetail.Kod_PTJ);
             selectObj_ddlKPtj.append("<option value = '" + orderDetail.Kod_PTJ + "'>" + orderDetail.ButiranPTJ + "</option>")
         }

         $(function () {
             $('.btnAddRow.five').click();
         });

         $('.btnAddRow').click(async function () {
             //alert("test");
             var totalClone = $(this).data("val");

             await AddRow(totalClone);
         });

         async function AddRow(totalClone, objOrder) {
             var counter = 1;
             var table = $('#tblData2');

             if (objOrder !== null && objOrder !== undefined) {
                 //totalClone = objOrder.Payload.OrderDetails.length;
                 totalClone = objOrder.Payload.length;

             }

             while (counter <= totalClone) {

                 curNumObject += 1;

                 //var newId_coa = "ddlCOA" + curNumObject;
                 var newId_NamaItem = "txtNamaItem" + curNumObject; //create new object pada tble
                 var newId_Kuantiti = "txtKuantiti" + curNumObject;
                 var newId_Harga = "txtHarga" + curNumObject;
                 var newId_Resit = "txtResit" + curNumObject;
                 var newId_Bukti = "txtUpload" + curNumObject;
                 var newId_hidBukti = "hidItem" + curNumObject;


                 var row = $('#tblData2 tbody>tr:first').clone();

                 var NamaItemInput = $(row).find(".list-NamaItem").attr("id", newId_NamaItem);
                 var KuantitiInput = $(row).find(".list-kuantiti").attr("id", newId_Kuantiti);
                 var HargaInput = $(row).find(".list-harga").attr("id", newId_Harga);
                 var resitInput = $(row).find(".list-resit").attr("id", newId_Resit);
                 var UploadBukti = $(row).find(".fileInputSurat").attr("id", newId_Bukti);
                 var hidItemNO = $(row).find(".hidItemNO").attr("id", newId_hidBukti);


                 row.attr("style", "");
                 var val = "";

                 $('#tblData2 tbody').append(row);

                 counter += 1;
             }
         }


         function getDataPeribadiPemohon(pemohon) {
             //Cara Pertama
             console.log("668");
             var nostaf = pemohon
             //alert(pemohon)

             fetch('PelarasanPP_WS.asmx/GetUserInfo', {
                 method: 'POST',
                 headers: {

                     'Content-Type': "application/json"
                 },
                 //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
                 body: JSON.stringify({ nostaf: pemohon })
             })
                 .then(response => response.json())
                 .then(data => setDataPeribadi(data.d))
         }

         function setDataPeribadi(data) {
             data = JSON.parse(data);
             if (data.Nostaf === "") {
                 alert("Tiada data");
                 return false;
             }

             $('#txtNamaP').val(data[0].Param1);
             $('#txtNama').val(data[0].Param1);
             $('#txtNoPekerja').val(data[0].StafNo);
             $('#txtNoStaf').val(data[0].StafNo);
             $('#txtJawatan').val(data[0].Param3);
             $('#txtGredGaji').val(data[0].Param6);
             $('#txtPejabat').val(data[0].Param5);
             $('#txtKump').val(data[0].Param4);
             $('#txtTel').val(data[0].Param7);
             $('#hidPtjPemohon').val(data[0].KodPejPemohon)
              //$('#<%'=txtMemangku.ClientID%>').val(data[0].Param3);

         }

         function generateDropdown(id, url, param, fn) {
             console.log("masuk sini 707")
             var inParam = "";

             if (param !== null && param !== undefined) {
                 inParam = param;
             }
             $('#' + id).dropdown({
                 fullTextSearch: false,
                 onChange: function () {
                     if (fn !== null && fn !== undefined) {
                         return fn();
                     }
                 },
                 apiSettings: {
                     url: url + '?q={query}&' + inParam,
                     method: 'POST',
                     dataType: "json",
                     contentType: 'application/json; charset=utf-8',
                     cache: false,
                     beforeSend: function (settings) {
                         // Replace {query} placeholder in data with user-entered search term
                         settings.data = JSON.stringify({ q: settings.urlData.query });
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
                         console.log(objItem)
                         $.each(listOptions, function (index, option) {
                             $(objItem).append($('<div class="item" data-value="' + option.value + '">').html(option.text));
                         });

                         $(obj).dropdown('refresh');

                         if (shouldPop === true) {
                             $(obj).dropdown('show');
                         }
                     }
                 }
             });
         }

         function getDataPeribadi() {
             //Cara Pertama
             console.log("1312")
             var nostaf = $('#ddlStaf').val()

             if (nostaf === null) {
                 nostaf = '<%=Session("ssusrID")%>'
                 //$("#tblListPend").ajax.ready();
                 //$('#tblListPend').DataTable().ajax.reload();
                 console.log("1322")
              }

              else {

                 nostaf = $('#ddlStaf').val();
                // $("#tblListPend").ajax.ready();
                 //$('#tblListPend').DataTable().ajax.reload();
                 console.log("1329")
              }


              fetch('PelarasanPP_WS.asmx/GetUserInfo', {
                  method: 'POST',
                  headers: {
                      'Content-Type': "application/json"
                  },
         //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
         body: JSON.stringify({ nostaf:nostaf  })
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

         

         $(document).ready(function () {
             generateDropdown("ddlKumpWang", "PelarasanPP_WS.asmx/GetDataKumpWang", null, null)
             generateDropdown("ddlKO", "PelarasanPP_WS.asmx/GetDataOperasi", null, null)
             generateDropdown("ddlKPtj", "PelarasanPP_WS.asmx/GetKodPtj", null, null)
             generateDropdown("ddlKP", "PelarasanPP_WS.asmx/GetDataProjek", null, null)
             $('#ddlStaf').dropdown({
                 fullTextSearch: false,
                 onChange: function () {   //function bila klik ddlstaf.pilih nama staf then auto load maklumat staf.
                     getDataPeribadi($(this).val())  //baca value bila pilih nama pada ddlStaf selection
                    
                 },
                 apiSettings: {
                     url: 'PelarasanPP_WS.asmx/fnCariStaf?q={query}',
                     method: 'POST',
                     dataType: "json",
                     contentType: 'application/json; charset=utf-8',
                     cache: false,
                     beforeSend: function (settings) {
                         // Replace {query} placeholder in data with user-entered search term
                         settings.data = JSON.stringify({ q: settings.urlData.query });
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
                             $(objItem).append($('<div class="item" data-value="' + option.StafNo + '">').html(option.MS01_Nama));
                         });

                         // Refresh dropdown
                         $(obj).dropdown('refresh');

                         if (shouldPop === true) {
                             $(obj).dropdown('show');

                         }

                     }
                 }
             });

         });

        

         function setDataPeribadi(data) {
             data = JSON.parse(data);
             if (data.Nostaf === "") {
                 alert("Tiada data");
                 return false;
             }
             $('#txtNamaP').val(data[0].Param1);
             $('#txtNama').val(data[0].Param1);
             $('#txtNoPekerja').val(data[0].StafNo);
             $('#txtNoStaf').val(data[0].StafNo);
             $('#txtJawatan').val(data[0].Param3);
             $('#txtGredGaji').val(data[0].Param6);
             $('#txtPejabat').val(data[0].Param5);
             $('#txtKump').val(data[0].Param4);
             $('#txtTel').val(data[0].Param7);
             $('#hidPtjPemohon').val(data[0].KodPejPemohon)
             //$('#<%'=txtMemangku.ClientID%>').val(data[0].Param3);
             $('#tblListPend').DataTable().ajax.reload();
         };

         

         $(document).ready(function () {
             getDataPeribadi();
             //initDropdownCOA("ddlCOA");
             const radioButtons = document.querySelectorAll('input[name="JnsMohon"]');
             for (const radioButton of radioButtons) {
                 radioButton.addEventListener('change', showSelected);
             }

             function showSelected(e) {
                 if (this.checked) {
                     var x = document.getElementById("cariStaf");
                     ///document.querySelector('#output').innerText = `You selected ${this.value}`;
                     if (this.value == "STAFLAIN") {
                         x.style.display = "inline";
                         getDataPeribadi('<%=Session("ssusrID")%>')
                     }
                     else {
                         x.style.display = "none";
                     }
                 }
             }

         });

         function uploadSurat(obj) {
             
             var $curBtnUpload = $(obj);
             var $row = $curBtnUpload.closest("tr");
             var $namaItem = $row.find(".list-NamaItem");
             var $kuantiti = $row.find(".list-kuantiti");
             var $jumlah = $row.find(".list-harga");
             var $resit = $row.find(".list-resit");
             var $itemID = $row.find(".hidItemNO");
             //alert($('#selectedNoPendahuluan').val());

             //var newUploadResit = {
             //    checkList: {
             //        mohonID: $('#noPermohonan').val(),
             //        idItem: $itemID,
             //        namaItem: $namaItem,
             //        kuantiti: $kuantiti,
             //        jumlah: $jumlah,

             //    }
             //}  
             //return;

             var frmData = new FormData();
             var $file = $row.find('.fileInputSurat').get(0).files[0];

             frmData.append("fileSurat", $file);
             frmData.append("fileName", $file.name);
             frmData.append("fileSize", $file.size);
             frmData.append("idItem", $itemID.val());
             frmData.append("mohonID", $('#noPermohonan').val());
             frmData.append("namaItem", $namaItem.val());
             frmData.append("kuantiti", $kuantiti.val());
             frmData.append("jumlah", $jumlah.val());
             //$('#hidFolder').val(fileExtension);
             //$('#hidFileName').val(fileName);

             console.log(frmData);

             $.ajax({
                 url: "PelarasanPP_WS.asmx/UploadFileSurat",
                 type: 'POST',
                 data: frmData,
                 cache: false,
                 contentType: false,
                 processData: false,
                 success: function (response) {
                     // Show the uploaded file name on the screen
                     // $("#uploadedFileNameLabel").text(fileName);

                     var fileLink = document.createElement("a");
                     fileLink.href = requestData.resolvedUrl + fileName;
                     fileLink.textContent = fileName;

                     var uploadedFileNameLabel = document.getElementById("uploadSurat");
                     uploadedFileNameLabel.appendChild(fileLink);

                     
                     $("#uploadSurat").show();
                     // Clear the file input
                     $("#fileInputSurat").val("");

                     $("#progressContainer").text("File uploaded successfully.");
                    

                 },
                 error: function () {
                     $("#progressContainer").text("Error uploading file.");
                 }

                 
             });
             console.log("nama file");
             $('#tab-Perbelanjaan').click();
             //$('#tab-Perbelanjaan').reload();
            
             //console.log("1456");
         }

         $('#tab-Perbelanjaan').click(async function () {
            // $('#noPermohonan').val(orderDetail.No_Tuntutan) 
             console.log("masuk tab Perbelanjaan")
             //$('.btnSimpan').style.visible = true;
             var mohonID = $('#noPermohonan').val();
             console.log(mohonID);
             var id = $('#selectedNoPendahuluan').val();
             //alert($('#selectedNoPendahuluan').val());
             var id = $('#noPermohonan').val();
             var tkhMohon1 = $('#tkhMohon').val();
             $('#txtMohonID2').val(id);
             $('#tkhMohon2').val(tkhMohon1);


             if (id !== "") {

                 //BACA DETAIL JURNAL
                 var recordDataKeperluan = await AjaxGetDataKeperluan(id, mohonID);  //Baca data pada table Keperluan             
                 //await clearAllRows();
                 await SetDataKeperluanKepadaRows(null, recordDataKeperluan); //setData pada table
             }

             return false;            
            
         })

         async function AjaxGetDataKeperluan(id, mohonID) {

             try {

                 const response = await fetch('PelarasanPP_WS.asmx/LoadListingKeperluan', {
                     method: 'POST',
                     headers: {
                         'Content-Type': 'application/json'
                     },
                     body: JSON.stringify({ id: id, mohonID:mohonID })
                 });
                 const data = await response.json();
                 return JSON.parse(data.d);
             } catch (error) {
                 console.error('Error:', error);
                 return false;
             }
         }

         async function clearAllRows() {
             $('#tblData2' + " > tbody > tr ").each(function (index, obj) {
                 if (index > 0) {
                     obj.remove();
                 }
             })
             $(totalKt).val("0.00");

         }

         async function SetDataKeperluanKepadaRows(totalClone, objOrder) {
             clearAllRows()
             var counter = 1;
             var table = $('#tblData2');
             var total = 0.00;

             if (objOrder !== null && objOrder !== undefined) {   //semak berapa object yang ada
                 totalClone = objOrder.Payload.length;
             }

             console.log(objOrder);
             var obj = objOrder.Payload;
             while (counter <= totalClone) {

                 curNumObject += 1;

                 var newId_Keperluan = "Keperluan" + curNumObject; //create new object pada tble
                 var newId_Kuantiti = "Kuantiti" + curNumObject;
                 var newId_Harga = "Harga" + curNumObject;
                 var newId_Resit = "Resit" + curNumObject;
                 var newId_Bukti = "Bukti" + curNumObject;
                 var newId_hidItem = "hidItem" + curNumObject;


                 var row = $('#tblData2 tbody>tr:first').clone(); // create dummy object pada tble
                 var keperluanTextbox = $(row).find(".list-NamaItem").attr("id", newId_Keperluan);
                 var kuantitiTextbox = $(row).find(".list-kuantiti").attr("id", newId_Kuantiti);
                 var hargaTextbox = $(row).find(".list-harga").attr("id", newId_Harga);
                 var resitTextbox = $(row).find(".list-resit").attr("id", newId_Resit);
                 var buktiTextbox = $(row).find(".fileInputSurat").attr("id", newId_Bukti);

                 var $tempFilePreview = $(row).find(".tempFile");
                 var $link1 = obj[counter - 1].Path + "" + (obj[counter - 1].Nama_Fail).toString()
                 console.log($link1);
                 var hidItemNO = $(row).find(".hidItemNO").attr("id", newId_hidItem);

                 keperluanTextbox.val(obj[counter - 1].Butiran);   //bind value setiap object dlm tbl
                 kuantitiTextbox.val(obj[counter - 1].Kuantiti);
                 hargaTextbox.val(obj[counter - 1].Jumlah_anggaran);
                 resitTextbox.val(obj[counter - 1].Nama_Fail);
                 //buktiTextbox.val(obj[counter - 1].Path);
                 //$tempFilePreview.attr("href", obj[counter - 1].Path);
                 $tempFilePreview.attr("href", $link1);
                 $tempFilePreview.html(obj[counter - 1].Nama_Fail);
                 hidItemNO.val(obj[counter - 1].No_Item);
                 console.log(hidItemNO.val(obj[counter - 1].No_Item))
                 total += parseFloat(obj[counter - 1].Jumlah_anggaran);

                 row.attr("style", "");  //style pada row

                 $('#tblData2 tbody').append(row);  //bind data start pada row yang first pada tblData2

                 counter += 1;
             }

             $('#totalKt').val(total)  //total harga
         }


         //event bila klik button simpan  
         $('.btnSimpan').click(async function () {
             var jumRecord = 0;
             var acceptedRecord = 0;
             //alert("masuk simpan");
             var pemohon = $('#txtNoStaf').val();
             var statusPemohon
             //alert(pemohon);
             //alert($('#selectedNoPendahuluan').val());
             

             if ('<%=Session("ssusrID")%>' !== pemohon) {
                    statusPemohon = "0"  //mohon tuk org lain
                } else {
                    statusPemohon = "N"  //mohon tuk sendiri
                }

                var msg = "";
                var newTuntutan = {
                    listDetail: {
                        mohonID: $('#noPermohonan').val(),
                        stafID: '<%=Session("ssusrID")%>',                        
                        PtjMohon: $('#hidPtjPemohon').val(),
                        blnTuntut: $('#ddlBulan').val(),
                        thnTuntut: $('#ddlTahun').val(),
                        TujuanMohon: $('#txtTujuan').val(),
                        TkhMohon: $('#tkhMohon').val(),
                        NoPemohon: $('#txtNoPekerja').val(),
                        StatusPemohon: statusPemohon,                        
                        kodPTj: $('#ddlKPtj').val(),
                        kodKW: $('#ddlKumpWang').val(),
                        kodKO: $('#ddlKO').val(),
                        kodKP: $('#ddlKP').val(),
                        noPendahuluan: $('#selectedNoPendahuluan').val(),
                        jumlahPendahuluan: $('#selectedJumlah').val(),

                    }
                }


                //1`ShowPopup("msg")
                msg = "Anda pasti ingin menyimpan rekod ini?"

                if (!confirm(msg)) {
                    return false;
                }
                show_loader();
             var result = JSON.parse(await ajaxSaveRecord(newTuntutan));
                close_loader();
                $('#noPermohonan').val(result.Payload.mohonID)
                console.log(result.Payload.mohonID)
                //$('#orderid').val(result.Payload.OrderID)
                if (id !== "") {
                    //BACA HEADER JURNAL
                    console.log("BACA HEADER JURNAL")
                    var recordHdr = await AjaxGetRecordHdrPermohonan(id);
                    await setValueToRow_HdrPermohonan(recordHdr.Payload);
                    setValueToRow_Transaksi

                }
                //await clearAllRowsHdr();
         }); 

         async function ajaxSaveRecord(tuntutan) {

             return new Promise((resolve, reject) => {
                 $.ajax({
                     url: 'PelarasanPP_WS.asmx/SaveRecordTuntutan',
                     method: 'POST',
                     data: JSON.stringify(tuntutan),
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

         async function AjaxGetRecordHdrPermohonan(id) {

             try {

                 const response = await fetch('PelarasanPP_WS.asmx/LoadHdrPermohonan', {
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

         async function setValueToRow_HdrPermohonan(orderDetail) {  //ni tuk set value bila klik pada gridview permohonan

             $('#noPermohonan').val(orderDetail[0].No_Pendahuluan)
             $('#hidPtjPemohon').val(orderDetail[0].Kod_PTJ)
             $('#ddlBulan').val(orderDetail[0].Tarikh_Mula)
             $('#ddlTahun').val(orderDetail[0].Tarikh_Tamat)
             $('#txtTujuan').val(orderDetail[0].Tkh_Adv_Perlu)
             $('#tkhMohon').val(orderDetail[0].Tarikh_Mohon)
             $('#ddlKPtj').val(orderDetail[0].Tempat_Perjalanan)
             $('#ddlKumpWang').val(orderDetail[0].Tujuan)
             $('#ddlKO').val(orderDetail[0].Tujuan)
             $('#ddlKP').val(orderDetail[0].Nopemohon)
             $('#selectedNoPendahuluan').val(orderDetail[0].Justifikasi_Prgm)
             $('#selectedJumlah').val(orderDetail[0].Samb_Telefon)       

             var newId = $('#ddlBayar')
             var ddlJnsPnginap = $('#ddlBayar')
             var ddlSearch = $('#ddlBayar')
             var ddlText = $('#ddlBayar')
             var selectObj_JenisBayar = $('#ddlBayar')
             $(ddlBayar).dropdown('set selected', orderDetail[0].CaraBayar);
             selectObj_JenisBayar.append("<option value = '" + orderDetail[0].CaraBayar + "'>" + orderDetail[0].ButiranJenisBayar + "</option>")

             //console.log(orderDetail)
             //ni digunakan tuk bind data pada ddlCOA 
             //ni xperlu guna .row() sebab dlm design hanya ade satu sahaja row dia..xde tambah row. perlu baca object terus
             var ddl = $(".COA-list");    //ddl tu adalah parent bagi class COA_list..perlu baca parent tuk dapat object
             var ddlSearch = ddl.find("td > .search");
             var ddlText = ddl.find(".text");
             var selectObj = ddl.find("select");
             $(ddl).dropdown('set selected', orderDetail[0].KodVot);
             selectObj.append("<option value = '" + orderDetail[0].Kod_Vot + "'>" + orderDetail[0].ButiranVot + "</option>")

             $(".Hid-vot-list").html(orderDetail[0].Kod_Vot);

             var hidbutirptj = $(".Hid-ptj-list");
             hidbutirptj.html(orderDetail[0].Kod_PTJ);

             var butirKW = $(".label-kw-list");
             butirKW.html(orderDetail[0].colKW);

             var hidbutirkw = $(".Hid-kw-list");
             hidbutirkw.html(orderDetail[0].colhidkw);

             var butirKo = $(".label-ko-list");
             butirKo.html(orderDetail[0].colKO);

             var hidbutirko = $(".Hid-ko-list");
             hidbutirko.html(orderDetail[0].colhidko);

             var butirKp = $(".label-kp-list");
             butirKp.html(orderDetail[0].colKp);

             var hidbutirkp = $(".Hid-kp-list");
             hidbutirkp.html(orderDetail[0].colhidkp);

         };

         $('#tab-Rumusan').click(async function () {
             // $('#noPermohonan').val(orderDetail.No_Tuntutan) 
             console.log("masuk tab")
             console.log(mohonID);
             //$('.btnSimpan').style.visible = true;
             var mohonID = $('#noPermohonan').val();            
             var id = $('#selectedNoPendahuluan').val();
             var tkhMohon1 = $('#tkhMohon').val();
             var jumPilih = $('#selectedJumlah').val();
             var jumTuntut = $('#totalKt').val();
             var tolakPulang = 0.00;
             var jumBesar = 0.00;
             $('#txtMohonID3').val(id);
             $('#tkhMohon3').val(tkhMohon1);
             $('#txtPendDiberi').val(parseFloat(jumPilih).toFixed(2));
             $('#txtTolakPulang').val(parseFloat(tolakPulang).toFixed(2));
             $('#txtTolakJumTunt').val(parseFloat(jumTuntut).toFixed(2));   
             //alert($('#selectedNoPendahuluan').val());
             console.log(jumPilih);
             console.log(jumTuntut);
             console.log(tolakPulang);
             jumBesar = jumPilih - tolakPulang - jumTuntut
             console.log(jumBesar);
             $('#txtBakiBesar').val(parseFloat(jumBesar).toFixed(2));


             //if (id !== "") {

             //    //BACA DETAIL JURNAL
             //    var recordDataKeperluan = await AjaxGetDataKeperluan(id, mohonID);  //Baca data pada table Keperluan             
             //    //await clearAllRows();
             //    await SetDataKeperluanKepadaRows(null, recordDataKeperluan); //setData pada table
             //}

             return false;

         })

         $('.btnSimpanFinal').click(async function () {
             var jumRecord = 0;
             var id = $('#noPermohonan').val();
             var jumlahPendahuluan
             var jumlahTuntut
             var bakiTuntut
            
            var msg = "";
            var newKira = {
                    kiraBaki: {
                            mohonID: $('#noPermohonan').val(),                   
                            jumlahPendahuluan: $('#txtPendDiberi').val(),
                            jumlahTuntut: $('#txtTolakJumTunt').val(),
                            bakiTuntut: $('#txtBakiBesar').val(),                    
                        }
                    }


              //1`ShowPopup("msg")
              msg = "Anda pasti ingin menyimpan rekod ini?"

              if (!confirm(msg)) {
                  return false;
              }
              show_loader();
             var result = JSON.parse(await ajaxSaveRecordBakiKira(newKira));
              close_loader();
              $('#noPermohonan').val(result.Payload.mohonID)
              console.log(result.Payload.mohonID)
              //$('#orderid').val(result.Payload.OrderID)
              if (id !== "") {
                  //BACA HEADER JURNAL
                  console.log("BACA HEADER JURNAL")
                  var recordHdr = await AjaxGetRecordBakiTuntut(id);
                  await setValueToRow_BakiRecord(recordHdr.Payload);
                  //setValueToRow_kiraBaki
              }
         
         }); 

         async function ajaxSaveRecordBakiKira(baki) {

             return new Promise((resolve, reject) => {
                 $.ajax({
                     url: 'PelarasanPP_WS.asmx/SaveRecordBaki',
                     method: 'POST',
                     data: JSON.stringify(baki),
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

         };

         async function AjaxGetRecordBakiTuntut(id) {

             try {

                 const response = await fetch('PelarasanPP_WS.asmx/LoadDataBakiTuntut', {
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


         async function setValueToRow_BakiRecord(baki) {  //ni tuk set value bila klik pada gridview permohonan
             console.log("setValueToRow_BakiRecord")
             $('#txtMohonID3').val(baki[0].No_Tuntutan)
             $('#txtPendDiberi').val(parseFloat((baki[0].Jum_Pendahuluan).toFixed(2)));
            // $('#txtTolakPulang').val(parseFloat((baki[0].No_Pendahuluan).toFixed(2)));
             $('#txtTolakJumTunt').val(parseFloat((baki[0].Jum_Tuntut).toFixed(2)));
             $('#txtBakiBesar').val(parseFloat((baki[0].Jum_Baki_Tuntut).toFixed(2)));
             
             
         }


     </script>


</asp:Content>
