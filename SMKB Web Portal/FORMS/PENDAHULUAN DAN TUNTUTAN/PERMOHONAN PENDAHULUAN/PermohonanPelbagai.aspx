<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PermohonanPelbagai.aspx.vb" Inherits="SMKB_Web_Portal.Pelbagai" %>

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
            padding: 0 10px;
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
  
    <br />
    <br />
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
    <li class="nav-item" role="presentation"><a class="nav-link" id="tab-Keperluan" data-toggle="tab" href="#menu2">Senarai Keperluan</a></li>
    <li class="nav-item" role="presentation"><a class="nav-link" id="tab-Pengesahan" data-toggle="tab" href="#menu3">Perakuan</a></li>
  </ul>
  

  <div class="tab-content">
      
 <%--content Info Pemohon--%>
  
 <%--content Info Permohonan--%>
      <br />
    <div id="menu1" class="tab-pane fade show active" role="tabpanel" aria-labelledby ="tab-permohonan">     
       <asp:Panel ID="Panel2" runat="server" > 
           <div class="col-md-12"> 
                <div class="form-row">
                    <div class="form-group col-md-3" style="left: -1px; top: 0px">
                        <input type="text"  id="noPermohonan" class="input-group__input form-control input-md" style="background-color: #f0f0f0"  readonly>
                        <label for="No.Permohonan"  class="input-group__label">No.Permohonan:</label>   
                    </div>                    
                    <div class="form-group col-md-3">
                         <input type="date" id="tkhMohon" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly />                                   
                        <label for="TarikhMohon"  class="input-group__label">Tarikh Mohon:</label>                       
                    </div>                
                </div>
               </div>
                <div class="col-md-12">
                
                    <table id="tblData" class="table table-striped" >
                        <thead>
                            <tr>
                                <th scope="col"  style="text-align: left;width: 30%;">Kumpulan Wang</th>
                                <th scope="col"  style="text-align: left;width: 25%;">Kod Operasi</th>
                                <th scope="col"  style="text-align: left;width: 15%;">Kod Ptj</th>
                                <th scope="col"  style="text-align: left;width: 15%;">Kod Projek</th>
                                <th scope="col"  style="text-align: left;width: 15%;">Kod Vot</th>
                            </tr>
                        </thead>
                        <tbody id="tableID">
                             <tr  class="table-list">
                                <td>
                                    <select class="ui search dropdown COA-list" name="ddlCOA" id="ddlCOA"></select>
                                    <input type="hidden" class="data-id" value="" />                                     
                                     <label id="lblKw" name="lblKw" class="label-kw-list" style="text-align: center;visibility: hidden"></label>
                                    <label id="HidlblKw" name="HidlblKw" class="Hid-kw-list" style="visibility: hidden"></label>                                  
                                </td>
                                <td>
                                    <label id="lblKo" name="lblKo" class="label-ko-list" style="text-align: center"></label>
                                    <label id="HidlblKo" name="HidlblKo" class="Hid-ko-list" style="visibility: hidden"></label>
                                </td>

                                <td>                                    
                                    <label id="lblPTj" name="lblPTj" class="label-ptj-list" style="text-align: left"></label>  
                                    <label id="HidlblPTj" name="HidlblPTj" class="Hid-ptj-list" style="visibility: hidden"></label>
                                </td>

                                <td>
                                    <label id="lblKp" name="lblKp" class="label-kp-list" style="text-align: center"></label>
                                    <label id="HidlblKp" name="HidlblKp" class="Hid-kp-list" style="visibility: hidden"></label>
                                </td>

                                <td>                                    
                                    <label id="lblVot" name="lblVot" class="label-vot-list" style="text-align: left"></label>
                                    <label id="hidVot" name="hidVot" class="Hid-vot-list" style="visibility: hidden"></label>                                   
                                </td>  
                            </tr>
                        </tbody>                        
                    </table>
                    </div>

                <div class="col-md-12">             
                    <div class="form-row">
                        <div class="form-group col-md-4">  
                            <input type="date" id="tkh_Adv" class="input-group__input form-control"  placeholder="&nbsp;">
                            <label class="input-group__label" for="kodModul">Tarikh Pendahuluan Dikehendaki</label>                                    
                        </div>   
                        <div class="form-group col-md-4">   
                            <select class="input-group__select ui search dropdown bayar-list" name="ddlBayar" id="ddlBayar"  placeholder="&nbsp;"></select>
                            <label class="input-group__label" for="Kaedah Pembayaran">Kaedah Pembayaran</label>                             
                        </div>  
                        <div class="form-group col-md-4">
                            <input type="text" id="txtPeruntukan" class="input-group__input form-control input-sm" style="background-color: #f0f0f0"  readonly>
                            <label class="input-group__label" for="kodModul">Peruntukan Program</label>                            
                        </div>                              
                    </div>                       
                </div>
           
                <div class="col-md-12">                 
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <input type="date" id="tkhMula" class="input-group__input form-control"  placeholder="&nbsp;" onchange="updatedate();">                                       
                            <label class="input-group__label" for="Tarikh Mula">Tarikh Mula</label>                                   
                        </div>
                        <div class="form-group col-md-4">
                            <input type="date" id="tkhTamat"  class="input-group__input form-control"  placeholder="&nbsp;">
                            <label class="input-group__label" for="Tarikh Tamat">Tarikh Tamat</label>                              
                       </div>                                    
                    </div>   
                </div> 

                <div class="col-md-12">                 
                    <div class="form-row">
                         <div class="form-group col-md-4">
                             <textarea rows="2" cols="30" ID="txtTujuan" class="input-group__input form-control" placeholder="&nbsp;" MaxLength="500"></textarea>
                            <label class="input-group__label" for="Tujuan">Tujuan Pendahuluan/Nama Program</label>
                         </div> 
                        <div class="form-group col-md-4">
                            <textarea rows="2" cols="30" ID="txtTempat"  class="input-group__input form-control" placeholder="&nbsp;" MaxLength="500"></textarea>                                     
                            <label class="input-group__label" for="Tempat">Tempat Program Diadakan</label>   
                        </div>               
                    </div>   
                </div> 

             <div class="col-md-12">                 
                    <div class="form-row">
                        <div class="form-group col-md-8">
                            <textarea rows="2" cols="30" ID="txtSebab"class="input-group__input form-control" placeholder="&nbsp;" MaxLength="500"></textarea>
                            <label class="input-group__label" for="date-input2">Sila Nyatakan Mengapa Pembelian Barang/Perkhidmatan Tersebut 
                                Tidak Boleh Dibuat Melalui Pesanan Tempatan/Pembekal Tidak Dapat Mengeluarkan Invois</label> 
                         </div>                                 
                    </div>   
                </div> 


    </asp:Panel>
        
    <div class="form-row">
                <div class="form-group col-md-12" align="right">                   
                    <button type="button" class="btn btn-secondary btnSimpan">Simpan</button>
                </div>
            </div>
    </div>


 <%--content Senarai Keperluan--%>
  
    <div id="menu2" class="tab-pane fade" aria-labelledby ="tab-Keperluan" role="tabpanel">
             <div class="col-md-12">        
            <div class="form-row">
                    <div class="form-group col-md-3" style="left: -1px; top: 0px">
                         <input type="text"  id="txtMohonID"  class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3">
                        <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                       
                    </div>                    
                    <div class="form-group col-md-3">
                        <input type="date" id="tkhMohon2" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly>                                   
                        <label class="input-group__label" for="TarikhMohon">Tarikh Mohon:</label>                        
                    </div>                
                </div>
                 </div>
       <asp:Panel ID="keperluan" runat="server">  
          <p>Peringatan : 
            <br />&nbsp; 1) Anggaran Perbelanjaan Adalah Berdasarkan Kertas Kerja Yang Telah Diluluskan
            <br />&nbsp; 2) Perbelanjaan Luar Jangka Tidak Dibenarkan Didalam Pendahuluan Pelbagai 
              </p>
         

               <div class="col-md-12">
              
                    <table  class="table table-striped" id="tblData2" style="width: 100%">
                        <thead>
                            <tr>
                                <th style="width: 30%" scope="col">Senarai Keperluan</th>
                                <th style="width: 20%" scope="col">Kuantiti</th>                                
                                <th style="width: 20%" scope="col">Kadar Harga (RM)</th>
                                <th style="width: 20%" scope="col">Anggaran (RM)</th>
                                <th style="width: 10%" scope="col">Tindakan</th>

                            </tr>
                        </thead>
                        <tbody id="tableID2">
                            <tr style="display: none; width: 100%" class="table-list">
                                <td style="width: 30%">                             
                                    <input type="text"  ID="txtKeperluan"  class="txtKeperluan form-control"/><%-- //style="visibility: hidden"--%>
                                    <label id="hidItem" name="hidItem" class="hidItemNO" ></label>
                                </td>
                                <td style="width: 20%">
                                    <input id="Kuantiti" name="Kuantiti"  type="number" class="list-kuantiti" style="text-align: right"/>
                                </td>

                                <td style="width: 20%">
                                    <input id="txtKadarHarga" name="txtKadarHarga" type="text" class="list-kadarHarga" style="text-align: right" />
                                </td>
                                <td style="width: 20%">
                                    <input id="txtAnggaran" name="txtAnggaran"  type="text" class="list-anggaran" style="text-align: right" />
                                </td>                          
                                <td style="width: 10%">
                                    <button id="lbtnCari" runat="server" class="btn btnDelete" type="button" style="color: red">
                                        <i class="fa fa-trash"></i>
                                    </button>
                                </td>

                            </tr>
                        </tbody>
                        <tfoot>
                            <tr>
                                <td colspan="2"></td>
                                <td>
                                    <%--<input class="form-control underline-input" id="totalDbt" name="totalDbt" style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly />--%>
                                    <label style="font-size:medium; align-items:end"  > Jumlah (RM) </label>
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

                                <%--<th colspan="3" style="text-align: right; font-size: large">JUMLAH BEZA <font style="color: grey">(DEBIT - KREDIT)</font></th>
                                <td style="text-align:center">
                                    <input class="form-control underline-input" id="totalBeza" name="totalBeza" style="text-align: right; font-size: large; font-weight: bold" value="0.00" readonly />
                                </td>
                                <td colspan="2"></td>--%>
                            </tr>
                        </tfoot>
                    </table>
            </div>


               <div class="form-row">
                    <div class="form-group col-md-12" align="right">
                         <button type="button" class="btn btn-secondary btnSimpan2" id="btnSimpanTblData2">Simpan</button>
                    </div>
                </div>         
        </asp:Panel> 
      
    </div>

 <%--content Pengesahan--%>
 
    <div id="menu3" class="tab-pane fade" aria-labelledby="tab-Pengesahan" role="tabpanel">
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
         <asp:Panel ID="Panel3" runat="server">           
             <input type="checkbox" ID="chckSah" value="chckSah" />&nbsp;&nbsp;<b>Saya mengaku bahawa :</b> <br /><br />
                 <ul>
                     <li>Pelarasan Pendahuluan akan dikemukan segera <b>TIDAK LEWAT DARIPADA 21 HARI </b> selepas selesai program/aktiviti.</li>
                     <li>Jika wang pendahuluan tidak dijelaskan dalam tempoh yang ditetapkan, saya bersetuju membenarkan Bendahari memotong gaji saya sehingga selesai berserta denda 10% setahun tanpa apa-apa peringatan</li>
                     <li>Jika saya tidak sempat menyelesaikan wang pendahuluan itu sebelum bersara, saya bersetuju wang pendahuluan tersebut diselesaikan melalui potongan dari ganjaran atau pencen atau apa-apa juga wang yang berhak kepada saya ditangan UTeM </li>
                     <li>Jika program tersebut ditangguhkan atau dipinda melebihi 21 hari daripada tarikh asal atau dibatalkan, wang pendahuluan itu akan dikembalikan dengan serta merta</li>
                 </ul> 
               <br />
            <br />
             <p><br/><u>Senarai Semak</u> <br />
                 Sila pastikan dokumen sokongan tersebut dihantar bersama borang permohonan Pendahuluan Pelbagai. <br /> 
             </p>
            <div class="form-row">
            <div class="form-group col-md-12">     
             <label for="kodModul">1) Surat Kelulusan program dari Pihak Berkuasa Meluluskan (tertakluk kepada peraturan semasa)</label> <br /> 
             <div class="input-group col-md-10">    
             <div class="form-inline">
                <input type="file" id="fileInputSurat" style="width:350px" /><br />
                <input type="button" class="btn btn-primary" id="uploadBtnSurat" value="Upload Surat" onclick="uploadFileSurat()" />
                    <span id=""><br /></span>
                <span id="uploadSurat" style="display: inline;"></span>
                <span id="">&nbsp</span>
                <span id="progressContainer"></span>
                <input type ="hidden" id="txtNamaFile" />
                <input type="hidden" class="form-control"  id="hidFolder" style="width:300px" readonly="readonly" /> 
                <input type="hidden" class="form-control"  id="hidFileName" style="width:300px" readonly="readonly" /> 
                </div>         
            </div>
            <br />
             <br />
             <label for="kodModul">2)Perincian bajet (berdasarkan kertas kerja)</label><br />   
              <div class="input-group col-md-10">    
                  <div class="input-group-prepend">
                       
                      <div class="form-inline">
                        <input type="file" id="fileBajet" style="width:350px" />
                        <input type="button" class="btn btn-primary" id="uploadBtnBajet" value="Upload Dokumen" onclick="uploadFileBajet()" />
                          <span id="uploadBajet" style="display: inline;"></span>
                        <span id="">&nbsp</span>
                        <span id="progressContainer2"></span>
                        <input type ="hidden" id="txtFileBajet" />
                        <input type="hidden" class="form-control"  id="hidFolderBajet" style="width:300px" readonly="readonly" /> 
                        <input type="hidden" class="form-control"  id="hidFileBajet" style="width:300px" readonly="readonly" /> 
                        </div>      
                    </div>   
                </div>
            </div>
            </div>
            <br />
            <br />
             <div class="form-row">
            <div class="form-group col-md-12" align="right">                 
                <button type="button" class="btn btn-secondary btnHantar" id="btnHantar">Hantar</button>
            </div>
        </div>
             
        </asp:Panel>
    </div>
  
</div>
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
         const dateInput = document.getElementById('tkhMohon');
         document.getElementById("tkhMohon").disabled = true;
         var isClicked = false;

         // ✅ Using the visitor's timezone
         dateInput.value = formatDate();

         function padTo2Digits(num) {
             return num.toString().padStart(2, '0');
         }

         function formatDate(date = new Date()) {
             return [

                 date.getFullYear(),
                 padTo2Digits(date.getMonth() + 1),
                 padTo2Digits(date.getDate()),
             ].join('-');
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
             $('.btnAddRow.five').click();
         });

         function updatedate() {
             var firstdate = $('#tkhMula').val();
             $('#tkhTamat').value = "";
             document.getElementById("tkhTamat").setAttribute("min", firstdate);
         }

         $(document).ready(function () {
             
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
                     "url": "Pelbagai_WS.asmx/LoadOrderRecord_PermohonanSendiri",
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
                     { "data": "Tarikh_Mohon" },
                     { "data": "NamaPemohon" },
                     { "data": "Tujuan" },
                     {
                         "data": "Jum_Mohon",
                         render: function (data, type, full) {
                             return parseFloat(data).toFixed(2);
                         }

                     },
                     { "data": "Butiran" }
                     //{
                     //    className: "btnView",
                     //    "data": "No_Pendahuluan",
                     //    render: function (data, type, row, meta) {

                     //        if (type !== "display") {

                     //            return data;

                     //        }

                     //        var link = `<button id="btnView" runat="server" class="btn btnView" type="button" style="color: blue" data-dismiss="modal">
                     //                           <i class="fa fa-edit"></i>
                     //               </button>`;
                     //        return link;
                     //    }
                     //}
                 ],
                 "columnDefs": [
                      { "targets": [4], "className": "right-align" }
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

         //tuk radioButton        
         function showSelected(e) {
            
             if (this.checked) {
                 var x = document.getElementById("cariStaf");
                 ///document.querySelector('#output').innerText = `You selected ${this.value}`;
                 if (this.value == "STAFLAIN") {
                     x.style.display = "inline";

                 }
                 else {
                     x.style.display = "none";
                 }
             }
         }

         var searchQuery = "";
         var oldSearchQuery = "";
         var curNumObject = 0;
         var tableID = "#tblData";
         var tableID2 = "#tableID2"
         var tableID_Senarai = "#tblDataSenarai_trans";
         var shouldPop = true;
         var totalID = "#totalBeza";

         var totalDebit = "#totalDbt";
         var totalKredit = "#totalKt";

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
             clearAllRows();
             console.log(orderDetail.Nopemohon);
             getDataPeribadiPemohon(orderDetail.Nopemohon)
             $('#SenaraiPermohonan').modal('toggle');

             console.log(orderDetail.No_Pendahuluan)
             // change .btnSimpan text to Simpan
             
             $('#noPermohonan').val(orderDetail.No_Pendahuluan)
             $('#txtTujuan').val(orderDetail.Tujuan)
             $('#tkhMula').val(orderDetail.Tarikh_Mula)
             $('#tkhTamat').val(orderDetail.Tarikh_Tamat)
             $('#tkh_Adv').val(orderDetail.Tkh_Adv_Perlu)
             $('#tkhMohon').val(orderDetail.Tarikh_Mohon)
             $('#txtLokasi').val(orderDetail.Tempat_Perjalanan)
             $('#txtTempat').val(orderDetail.Tujuan)
             $('#txtPeruntukan').val(orderDetail.Peruntukan_Prgm)
             $('#txtNoPekerja').val(orderDetail.Nopemohon)
             $('#txtSebab').val(orderDetail.Justifikasi_Prgm)
             $('#txtTel').val(orderDetail.Samb_Telefon)

             var newId = $('#ddlBayar')
             var ddlJnsPnginap = $('#ddlBayar')
             var ddlSearch = $('#ddlBayar')
             var ddlText = $('#ddlBayar')
             var selectObj_JenisBayar = $('#ddlBayar')
             $(ddlBayar).dropdown('set selected', orderDetail.CaraBayar);
             selectObj_JenisBayar.append("<option value = '" + orderDetail.CaraBayar + "'>" + orderDetail.ButiranJenisBayar + "</option>")


             //console.log(orderDetail)
             //ni digunakan tuk bind data pada ddlCOA 
             //ni xperlu guna .row() sebab dlm design hanya ade satu sahaja row dia..xde tambah row. perlu baca object terus
             var ddl = $(".COA-list");    //ddl tu adalah parent bagi class COA_list..perlu baca parent tuk dapat object
             var ddlSearch = ddl.find("td > .search");
             var ddlText = ddl.find(".text");
             var selectObj = ddl.find("select");
             $(ddl).dropdown('set selected', orderDetail.colKW);
             selectObj.append("<option value = '" + orderDetail.Kod_Kump_Wang + "'>" + orderDetail.colKW + "</option>")

             var lblVot = $(".label-vot-list");
             lblVot.html(orderDetail.Kod_Vot);

             var hidVot = $(".Hid-vot-list");
             hidVot.html(orderDetail.Kod_Vot);

             var butirptj = $(".label-ptj-list");
             butirptj.html(orderDetail.ButiranPTJ);

             var hidbutirptj = $(".Hid-ptj-list");
             hidbutirptj.html(orderDetail.Kod_PTJ);

             var butirKW = $(".label-kw-list");
             butirKW.html(orderDetail.colKW);

             var hidbutirkw = $(".Hid-kw-list");
             hidbutirkw.html(orderDetail.Kod_Kump_Wang);

             var butirKo = $(".label-ko-list");
             butirKo.html(orderDetail.colKO);

             var hidbutirko = $(".Hid-ko-list");
             hidbutirko.html(orderDetail.colhidko);

             var butirKp = $(".label-kp-list");
             butirKp.html(orderDetail.colKp);

             var hidbutirkp = $(".Hid-kp-list");
             hidbutirkp.html(orderDetail.colhidkp);
             $('#txtPeruntukan').val(butirptj.html());
         }


         function getDataPeribadiPemohon(pemohon) {
             //Cara Pertama

             var nostaf = pemohon
             //alert(pemohon)

             fetch('Pendahuluan_WS.asmx/GetUserInfo', {
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

             $('.btnPapar').click(function () {
                 tbl.ajax.reload();
             });

             function getDataPeribadi() {
                 //Cara Pertama

                 var nostaf = $('#ddlStaf').val()

                 if (nostaf === null) {
                     nostaf = '<%=Session("ssusrID")%>'
                }

                else {

                    nostaf = $('#ddlStaf').val();
                }


                fetch('Pelbagai_WS.asmx/GetUserInfo', {
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
                 var newId_Keperluan = "txtKeperluan" + curNumObject; //create new object pada tble
                 var newId_Kuantiti = "Kuantiti" + curNumObject;
                 var newId_KdrHarga = "txtKadarHarga" + curNumObject;
                 var newId_Anggaran = "txtAnggaran" + curNumObject;
                 var newId_hidItem = "hidItem" + curNumObject;
                 

                 var row = $('#tblData2 tbody>tr:first').clone();

                // var dropdown5 = $(row).find(".COA-list").attr("id", newId_coa);
                 var keperluanTextbox = $(row).find(".txtKeperluan").attr("id", newId_Keperluan);
                 var kuantitiTextbox = $(row).find(".list-kuantiti").attr("id", newId_Kuantiti);
                 var kadarHargaTextbox = $(row).find(".list-kadarHarga").attr("id", newId_KdrHarga);
                 var anggaranTextbox = $(row).find(".list-anggaran").attr("id", newId_Anggaran);
                 var hidItemNO = $(row).find(".hidItemNO").attr("id", newId_hidItem);

               
                 row.attr("style", "");
                 var val = "";

                 $('#tblData2 tbody').append(row);
                
                 counter += 1;
             }
         }

         

         $(tableID2).on('click', '.btnDelete', async function () {
             event.preventDefault();
             var mohonID =  $('#noPermohonan').val()
             console.log("masuk delete");
             console.log(mohonID);
             
             var curTR = $(this).closest("tr");
             var recordID = curTR.find("td > .hidItemNO");
             var bool = true;
             var id = setDefault(recordID.val());
             console.log("masuk2222");
             console.log(id);
             if (id !== "") {
                 bool = await DelRecord(id, mohonID);
             }

             if (bool === true) {
                 curTR.remove();
             }

             //calculateGrandTotal();
             return false;
         });

         async function DelRecord(id, mohonID) {
             var bool = false;
             var result = JSON.parse(await AjaxDelete(id));

             if (result.Code === "00") {
                 bool = true;
             }

             return bool;
         }

         async function AjaxDelete(id, mohonID) {
             var mohonID = $('#noPermohonan').val()
             return new Promise((resolve, reject) => {
                 $.ajax({
                     url: 'Pelbagai_WS.asmx/DeleteOrder',
                     method: 'POST',
                     data: JSON.stringify({ id: id, mohonID: mohonID }),
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
             });
         }

         function NumDefault(theVal) {

             return setDefault(theVal, 0)
         }

         function setDefault(theVal, defVal) {

             if (defVal === null || defVal === undefined) {
                 defVal = "";
             }

             if (theVal === "" || theVal === undefined || theVal === null) {
                 theVal = defVal;
             }
             return theVal;
         }

         async function clearAllRows() {
             $('#tblData2' + " > tbody > tr ").each(function (index, obj) {
                 if (index > 0) {
                     obj.remove();
                 }
             })
             $(totalKt).val("0.00");
           
         }

         $('#txtKadarHarga').on('keypress', function () {
             
             var total
             total = parseFloat($('#txtKadarHarga').val()) * parseFloat($('#Kuantiti').val())
             $('#txtAnggaran').val(parseFloat(total).toFixed(2));
             console.log(total);
         });
        

         async function ajaxSaveOrder(order) {

             return new Promise((resolve, reject) => {
                 $.ajax({
                     url: 'Pelbagai_WS.asmx/SaveOrders',
                     method: 'POST',
                     data: JSON.stringify(order),
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

         async function initDropdownCOA(id) {

             $('#' + id).dropdown({
                 fullTextSearch: true,
                 onChange: function (value, text, $selectedItem) {

                     //console.log($selectedItem);

                     var curTR = $(this).closest("tr");

                   var recordIDkwHd = curTR.find("td > .Hid-kw-list");
                     recordIDkwHd.html($($selectedItem).data("coltambah6"));

                     //var selectObj = $($selectedItem).find("td > .COA-list > select");
                     //selectObj.val($($selectedItem).data("coltambah5"));



                     var recordIDPtj = curTR.find("td > .label-ptj-list");
                     recordIDPtj.html($($selectedItem).data("coltambah1"));

                     var recordIDPtjHd = curTR.find("td > .Hid-ptj-list");
                     recordIDPtjHd.html($($selectedItem).data("coltambah5"));

                     console.log(recordIDPtjHd.html());

                     var recordIDVotHd = curTR.find("td > .Hid-vot-list");
                     recordIDVotHd.html("74102");

                     var recordID_Vot = curTR.find("td > .label-vot-list");
                     recordID_Vot.html("74102");

                     var recordID_ko = curTR.find("td > .label-ko-list");
                     recordID_ko.html($($selectedItem).data("coltambah3"));

                     var recordIDkoHd = curTR.find("td > .Hid-ko-list");
                     recordIDkoHd.html($($selectedItem).data("coltambah7"));

                     var recordID_kp = curTR.find("td > .label-kp-list");
                     recordID_kp.html($($selectedItem).data("coltambah4"));

                     var recordIDkpHd = curTR.find("td > .Hid-kp-list");
                     recordIDkpHd.html($($selectedItem).data("coltambah8"));

                     $('#txtPeruntukan').val(recordIDPtj.html());

                    


                 },
                 
                 apiSettings: {
                     url: 'Pelbagai_WS.asmx/GetVotCOA?q={query}',
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

                         settings.data = JSON.stringify({ q: settings.urlData.query });

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


         function generateDropdown(id, url, param, fn) {

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

         $(document).ready(function () {            
            
             generateDropdown("ddlBayar", "Pendahuluan_WS.asmx/GetKaedahBayar")            
             initDropdownCOA("ddlCOA");

             $('#ddlStaf').dropdown({
                 fullTextSearch: false,
                 onChange: function () {   //function bila klik ddlstaf.pilih nama staf then auto load maklumat staf.
                     getDataPeribadi($(this).val())  //baca value bila pilih nama pada ddlStaf selection
                 },
                 apiSettings: {
                     url: 'Pendahuluan_WS.asmx/fnCariStaf?q={query}',
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


         //event bila klik button simpan  
         $('.btnSimpan').click(async function () {
             var jumRecord = 0;
             var acceptedRecord = 0; 
             var pemohon = $('#txtNoStaf').val()
             var statusPemohon

             if ('<%=Session("ssusrID")%>' !== pemohon) {
                 statusPemohon = "0"  //mohon tuk org lain
             } else {
                 statusPemohon = "N"  //mohon tuk sendiri
             }

             var msg = "";
             var newPelbagai = {
                 OtherList: {
                     mohonID: $('#noPermohonan').val(),
                     stafID: '<%=Session("ssusrID")%>',
                     NoTel: $('#txtTel').val(),
                     PtjMohon: $('#hidPtjPemohon').val(),
                     TkhMula: $('#tkhMula').val(),
                     TkhTamat: $('#tkhTamat').val(),
                     TujuanProgram: $('#txtTujuan').val(),
                     TempatProgram: $('#txtTempat').val(),
                     JnsBayar: $('#ddlBayar').val(),
                     TunjukSebab: $('#txtSebab').val(),
                     TkhMohon: $('#tkhMohon').val(),
                     Peruntukan: $('#txtPeruntukan').val(),
                     NoPemohon: $('#txtNoPekerja').val(),
                     TkhAdvPerlu: $('#tkh_Adv').val(),
                     StatusPemohon: statusPemohon,
                     KodVot: $('.Hid-vot-list').eq(0).html(),
                     kodPTj: $('.Hid-ptj-list').eq(0).html(),
                     kodKW: $('.Hid-kw-list').eq(0).html(),
                     kodKO: $('.Hid-ko-list').eq(0).html(),
                     kodKP: $('.Hid-kp-list').eq(0).html(),                    
                 }              
             }


             //1`ShowPopup("msg")
             msg = "Anda pasti ingin menyimpan rekod ini?"

             if (!confirm(msg)) {
                 return false;
             }
             show_loader();
             var result = JSON.parse(await ajaxSaveRecord(newPelbagai));
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

        

         async function AjaxGetRecordListItem(id) {

             try {

                 const response = await fetch('Pelbagai_WS.asmx/LoadListingKeperluan', {
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

         $('#btnSimpanTblData2').click(async function () {
             var item
             id = $('#noPermohonan').val();

             var item = {
                 keperluan: {
                     mohonID: $('#noPermohonan').val(),
                     Jumlah: $('#totalKt').val(),
                     GroupItem: []
                 }

             } 

             $('#tableID2 tr').each(function (index, obj) {
                 if (index > 0) {
                     console.log($(obj));
                     //alert("ce;; "+tcell)
                     console.log($(obj).find('#Kuantiti'));
                     if ($(obj).find('.txtKeperluan').val() !== "") {
                         var orderItem = {
                             mohonID: $('#noPermohonan').val(),
                             id: $(obj).find('.hidItemNO').val(),
                             txtKeperluan: $(obj).find('.txtKeperluan').val(),
                             Kuantiti: $(obj).find('.list-kuantiti').val(),
                             txtKadarHarga: $(obj).find('.list-kadarHarga').val(),
                             txtAnggaran: $(obj).find('.list-anggaran').val(),
                         };
                         item.keperluan.GroupItem.push(orderItem);
                     }
                 }

             });


             //1`ShowPopup("msg")
             msg = "Anda pasti ingin menyimpan rekod ini?"

             if (!confirm(msg)) {
                 return false;
             }

             show_loader();

             var result = JSON.parse(await saveRecordItem(item));
             alert(result.Message)
             $('#totalKt').val(parseFloat(result.Payload.Jumlah).toFixed(2));
             console.log($('#totalKt').val(result.Payload.Jumlah))
             close_loader();
         });

         async function saveRecordItem(keperluan) {

             return new Promise((resolve, reject) => {
                 $.ajax({
                     url: 'Pelbagai_WS.asmx/SaveRecordItem',
                     method: 'POST',
                     data: JSON.stringify(keperluan),
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

         async function ajaxSaveRecord(pelbagai) {
             
             return new Promise((resolve, reject) => {
                 $.ajax({
                     url: 'Pelbagai_WS.asmx/SaveRecordPelbagai',
                     method: 'POST',
                     data: JSON.stringify(pelbagai),
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

         //async function AjaxGetRecordHdrPermohonan(id) {

         //    try {

         //        const response = await fetch('Pendahuluan_WS.asmx/LoadHdrPermohonan', {
         //            method: 'POST',
         //            headers: {
         //                'Content-Type': 'application/json'
         //            },
         //            body: JSON.stringify({ id: id })
         //        });
         //        const data = await response.json();
         //        return JSON.parse(data.d);
         //    } catch (error) {
         //        console.error('Error:', error);
         //        return false;
         //    }
         //}

         async function setValueToRow_HdrPermohonan(orderDetail) {  //ni tuk set value bila klik pada gridview permohonan

             $('#noPermohonan').val(orderDetail[0].No_Pendahuluan)            
             $('#txtTujuan').val(orderDetail[0].Tujuan)             
             $('#tkhMula').val(orderDetail[0].Tarikh_Mula)
             $('#tkhTamat').val(orderDetail[0].Tarikh_Tamat)
             $('#tkh_Adv').val(orderDetail[0].Tkh_Adv_Perlu)             
             $('#tkhMohon').val(orderDetail[0].Tarikh_Mohon)
             $('#txtLokasi').val(orderDetail[0].Tempat_Perjalanan)
             $('#txtTempat').val(orderDetail[0].Tujuan)
             $('#txtPeruntukan').val(orderDetail[0].Tujuan)
             $('#txtNoPekerja').val(orderDetail[0].Nopemohon)             
             $('#txtSebab').val(orderDetail[0].Justifikasi_Prgm)
             $('#txtTel').val(orderDetail[0].Samb_Telefon)
             
             //alert($('#tkh_Adv').val(orderDetail[0].Tkh_Adv_Perlu));          

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


             //var butirptj = $(".label-ptj-list");
             //butirptj.html(orderDetail[0].Kod_PTJ);


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

         $('#tab-Pengesahan').click(async function () {
             var id = $('#noPermohonan').val()
             var tkhMohon = $('#tkhMohon').val()
             $('#txtMohonID3').val(id)
             $('#tkhMohon3').val(tkhMohon)

             console.log("masuk tab")
             console.log(id)
             if (id !== "") {
                 console.log("masuk data")
                 //BACA Data Surat
                 var recordDataSurat = await AjaxGetDataSurat(id);  //Baca data pada table Keperluan   
                 console.log(recordDataSurat);
                 //await clearAllRows();
                 await SetDataSurat(recordDataSurat.Payload); //setData Surat

                 //BACA Data Bajet
                 var recordDataBajet = await AjaxGetDataBajet(id);  //Baca data Bajet             
                 console.log(recordDataBajet);
                 await SetDataBajet(recordDataBajet.Payload); //setData Bajet
             }

             return false;            
         })

         

         async function AjaxGetDataSurat(id) {

             try {

                 const response = await fetch('Pelbagai_WS.asmx/LoadDataSurat', {
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

         async function SetDataSurat(setSurat) {  //ni tuk set value pada lampiran surat
            
             $('#chckSah').prop('checked', true) 
             //$('#chckSah').prop('checked',true)
             $('#hidFolder').val(setSurat[0].Path)
            // $('#hidFileName').attr('type', 'text')  //setting input hidden to display 
            // $('#hidFileName').val(setSurat[0].Nama_Fail)
             $('#txtNamaFile').val(setSurat[0].Nama_Fail)

             var fileName = setSurat[0].Nama_Fail            
             var fileLink = document.createElement("a");
             fileLink.href = setSurat[0].url;
             fileLink.textContent = fileName;
             console.log(fileLink.href)
             var uploadLabel = document.getElementById("uploadSurat");
             uploadLabel.appendChild(fileLink);
             $("#uploadSurat").show();
             // Clear the file input
             $("#fileInputSurat").val("");


         }

         async function SetDataBajet(setBajet) {  //ni tuk set value pada lampiran Bajet
            // $('#hidFolderBajet').attr('type', 'text')        
             $('#hidFolderBajet').val(setBajet[0].Path)
             console.log(setBajet[0].Path)
            // $('#hidFileBajet').attr('type', 'text') 
             $('#hidFileBajet').val(setBajet[0].Nama_Fail) 

           
             var fileName = setBajet[0].Nama_Fail
             var fileLink = document.createElement("a");
             fileLink.href = setBajet[0].url;
             fileLink.textContent = fileName;
             console.log(fileLink.href)
             var uploadLabel = document.getElementById("uploadBajet");
             uploadLabel.appendChild(fileLink);
             $("#uploadBajet").show();
             // Clear the file input
             $("#fileInputBajet").val("");
         }
            

         async function AjaxGetDataBajet(id) {

             try {

                 const response = await fetch('Pelbagai_WS.asmx/LoadDataBajet', {
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

         $('#tab-Keperluan').click(async function () {
             var id = $('#noPermohonan').val()
             var tkhMohon = $('#tkhMohon').val()
             $('#txtMohonID').val(id)
             $('#tkhMohon2').val(tkhMohon)
            
             if (id !== "") {

                 //BACA DETAIL JURNAL
                 var recordDataKeperluan = await AjaxGetDataKeperluan(id);  //Baca data pada table Keperluan             
                 //await clearAllRows();
                 await SetDataKeperluanKepadaRows(null, recordDataKeperluan); //setData pada table
             }

             return false;            

         })

         async function AjaxGetDataKeperluan(id) {

             try {

                 const response = await fetch('Pelbagai_WS.asmx/LoadListingKeperluan', {
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
         

         async function SetDataKeperluanKepadaRows(totalClone, objOrder) {
             clearAllRows()
             var counter = 1;
             var table = $('#tblData2');
             var total = 0.00;

             if (objOrder !== null && objOrder !== undefined) {   //semak berapa object yang ada
                 totalClone = objOrder.Payload.length;
             }
             var obj = objOrder.Payload;
             while (counter <= totalClone) {

                 curNumObject += 1;

                 var newId_Keperluan = "txtKeperluan" + curNumObject; //create new object pada tble
                 var newId_Kuantiti = "Kuantiti" + curNumObject;
                 var newId_KdrHarga = "txtKadarHarga" + curNumObject;
                 var newId_Anggaran = "txtAnggaran" + curNumObject;
                 var newId_hidItem = "hidItem" + curNumObject;


                 var row = $('#tblData2 tbody>tr:first').clone(); // create dummy object pada tble
                 var keperluanTextbox = $(row).find(".txtKeperluan").attr("id", newId_Keperluan);
                 var kuantitiTextbox = $(row).find(".list-kuantiti").attr("id", newId_Kuantiti);
                 var kadarHargaTextbox = $(row).find(".list-kadarHarga").attr("id", newId_KdrHarga);
                 var anggaranTextbox = $(row).find(".list-anggaran").attr("id", newId_Anggaran);
                 var hidItemNO = $(row).find(".hidItemNO").attr("id", newId_hidItem);

                 keperluanTextbox.val(obj[counter - 1].Butiran);   //bind value setiap object dlm tbl
                 kuantitiTextbox.val(obj[counter - 1].Kuantiti);
                 kadarHargaTextbox.val(obj[counter - 1].Kadar_Harga);
                 anggaranTextbox.val(obj[counter - 1].Jumlah_anggaran);
                 hidItemNO.val(obj[counter - 1].No_Item);
                 console.log(hidItemNO.val(obj[counter - 1].No_Item))
                 total += parseFloat(obj[counter - 1].Jumlah_anggaran);

                 row.attr("style", "");  //style pada row

                 $('#tblData2 tbody').append(row);  //bind data start pada row yang first pada tblData2

                 counter += 1;
             }

             $('#totalKt').val(total)  //total harga
         }

         function uploadFileSurat() {
             alert("masuk upload surat")
             var fileInputSurat = document.getElementById("fileInputSurat");
             var file = fileInputSurat.files[0];

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
                                 resolvedUrl: resolveAppUrl("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/")
                             };

                             var frmData = new FormData();

                             frmData.append("fileSurat", $('input[id="fileInputSurat"]').get(0).files[0]);
                             frmData.append("fileName", fileName);
                             frmData.append("fileSize", fileSize);

                             $('#hidFolder').val(fileExtension);
                             $('#hidFileName').val(fileName);


                             $.ajax({
                                 url: "Pelbagai_WS.asmx/UploadFileSurat",
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
                 url: "Pelbagai_ws.asmx/GetBaseUrl",
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


         function uploadFileBajet() {
             var fileInputBajet = document.getElementById("fileBajet");
             var file = fileInputBajet.files[0];

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
                                 resolvedUrl: resolveAppUrl("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/")
                             };

                             var frmData = new FormData();

                             frmData.append("fileSurat", $('input[id="fileBajet"]').get(0).files[0]);
                             frmData.append("fileName", fileName);
                             frmData.append("fileSize", fileSize);

                             $('#hidFolderBajet').val(fileExtension);
                             $('#hidFileBajet').val(fileName);


                             $.ajax({
                                 url: "Pelbagai_WS.asmx/UploadFileBajet",
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

                                     var uploadedFileNameLabel = document.getElementById("uploadBajet");
                                     uploadedFileNameLabel.appendChild(fileLink);


                                     $("#uploadBajet").show();
                                     // Clear the file input
                                     $("#fileBajet").val("");

                                     $("#progressContainer2").text("File uploaded successfully.");
                                 },
                                 error: function () {
                                     $("#progressContainer2").text("Error uploading file.");
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


         $('.btnHantar').click(async function () {
             
             var msg = "";
             var mohonID = $('#noPermohonan').val()
             var check_surat = $('#uploadSurat').text();
             var check_suratBajet = $('#uploadBajet').text();
             var statusHantar = 1
             var idSurat = "4"
             var idBajet = "5"
             console.log("masuk simpan")

             console.log(mohonID);
             console.log(check_surat);
             console.log(check_suratBajet); 
             if (check_surat === "") {
                 alert("Sila upload Surat Kelulusan Program")
                 return false;
             }

             if (check_suratBajet === "") {
                 alert("Sila upload Perincian Bajet")
                 return false;
             }


             let isChecked = $('#chckSah').is(':checked')
             //console.log("semak"); 
             //console.log(isChecked); 
             if (isChecked !== true) {
                 alert("Sila tick pada kotak perakuan")
                  return false;
             }

             
          
             var newLampiran = {
                 checkList: {
                     mohonID: $('#noPermohonan').val(),                     
                     idSurat: idSurat,
                     namaSurat: check_surat,
                     folderSurat: $('#hidFolder').val(),
                     staHantarSurat: statusHantar,
                     idBajet: idBajet,
                     namaBajet: check_suratBajet,
                     folderBajet: $('#hidFolderBajet').val(),
                     staHantarBajet: statusHantar,

                }
            }

             

             ////1`ShowPopup("msg")
            msg = "Anda pasti ingin menyimpan rekod ini?"

             if (!confirm(msg)) {
                 return false;
             }
             show_loader();
             var recordSimpan = JSON.parse(await ajaxSaveRecordLampiran(newLampiran));
             alert(recordSimpan.Message)   
             close_loader();
                                   

         }); 

         async function ajaxSaveRecordLampiran(lampiran) { 
            
             return new Promise((resolve, reject) => {
                 $.ajax({
                     url: 'Pelbagai_WS.asmx/SaveRecordChecklist',
                     method: 'POST',
                     data: JSON.stringify(lampiran),
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
