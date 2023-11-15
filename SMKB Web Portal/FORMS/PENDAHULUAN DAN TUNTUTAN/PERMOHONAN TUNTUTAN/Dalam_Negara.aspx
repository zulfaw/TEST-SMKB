<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Dalam_Negara.aspx.vb" Inherits="SMKB_Web_Portal.Dalam_Negara" %>
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

            .input-group__label_td {
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

            .input-group__input_td {
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
                height: 40px;
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

            #Num{
                width: 50px;
            }
            .custom-select{
                background-color: #FFC83D;
            }

        .form-control-checkbox-tbl {
                display: block;
                width: 100%;
                /* height: calc(1.5em + .75rem + 2px); */
                padding: .375rem .75rem;
                font-size: 1rem;
                font-weight: 400;
                line-height: 1.5;
                color: #495057;
                background-color: #fff;
                background-clip: padding-box;
                border: 1px solid #ced4da;
                border-radius: .25rem;
                transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }
        .form-control-input-tbl{
            display: block;
            /* width: 100%; */
            height: calc(1.5em + .75rem + 2px);
            padding: .375rem .75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }
            
     

    </style>

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


<div class="tab-content" id="PermohonanTab">
   
         <!-- Modal -->
        <div id="myPersonal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
               

            <!-- Modal content-->
            <div class="modal-content">
            <div class="modal-header"><h4>Maklumat Pegawai</h4> 
            <button type="button" class="close" data-dismiss="modal"></button>
            <h4 class="modal-title"></h4>
            </div>
            <div class="modal-body">
               
            <asp:Panel ID="Panel2" runat="server" >
             <div class="form-row">                                      
                <div class="form-group col-sm-6">
                     <input type="text" id="txtNamaP" name="Nama" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"/> 
                    <label class="input-group__label" for="Nama">Nama</label>                                       
                     <%--<asp:TextBox ID="txtNamaP" runat="server" Width="100%" class="form-control input-sm" style="background-color:#f3f3f3"></asp:TextBox>--%>
                </div>                                    
                <div class="form-group col-sm-6">
                     <input type="text" ID="txtNoPekerja"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />  
                    <label class="input-group__label"  for="No.Pekerja">No.Pekerja</label>
                     <input type="hidden" ID="hidPtjPemohon" />
                                                        
                </div>                               
            </div>

            <div class="form-row">
                <div class="form-group col-sm-6">
                     <input type="text" ID="txtJawatan"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"  />  
                    <label class="input-group__label" for="kodModul">Jawatan</label>                        
                </div>
                <div class="form-group col-sm-6">
                     <input type="text" ID="txtGredGaji" Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" /> 
                    <label class="input-group__label" for="kodModul">Gred Gaji</label>                                     
                </div>
            </div>

             <div class="form-row">
                <div class="form-group col-sm-6">
                    <input type="text" ID="txtPejabat"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" /> 
                    <label class="input-group__label" for="kodModul">Pejabat/Jabatan/Fakulti</label>                                        
                     <input type="hidden" ID="hidPtjPemohon" />
                    
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
                    <input type="text"  ID="txtMemangku"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />
                     <label class="input-group__label" for="Memangku Jawatan">Memangku Jawatan</label>
                    <%-- <asp:TextBox ID="txtMemangku" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                </div>
                <div class="form-group col-sm-6">
                    <input type="text"  ID="txtTel"  Width="100%"  class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"  />
                    <label class="input-group__label" for="Samb. Tel">Samb. Tel</label>                                      
                   <%--<asp:TextBox ID="txtTel" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                </div>
            </div>
        </asp:Panel>
            </div>
            <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
            </div>  <%--Tutup class="modal-content">--%>

            </div>
        </div>  <%--Tutup modal myPersonal--%>
        <asp:Panel ID="pnlJenisPermohonan" runat="server">                         
             <br />
                              
         </asp:Panel>


       
 </div>



<div class="container-fluid"> 
    <br />    
        <ul class="nav nav-tabs" id="myTab" role="tablist">            
            <li class="nav-item" role="presentation"><a class="nav-link active" id="Permohonan" data-toggle="tab"  href="#menu1">Permohonan</a></li>            
            <li class="nav-item" role="presentation"><a class="nav-link" id="tab-Kenyataan" data-toggle="tab" href="#menu2" aria-selected="false">Kenyataan</a></li>
            <li class="nav-item" role="presentation"><a class="nav-link" id="tab-elaunPjln" data-toggle="tab"  href="#elaunPjln">Elaun Perjalanan </a></li>            
            <li class="nav-item" role="presentation"><a class="nav-link" id="tab-pengangkutan" data-toggle="tab" href="#pengangkutan" aria-selected="false">Tambang Kenderaan Awam</a></li>
            <li class="nav-item" role="presentation"><a class="nav-link" id="tab-elaunMakan" data-toggle="tab" href="#elaunMakan" aria-selected="false">Elaun Makan/Harian</a></li>
            <li class="nav-item" role="presentation"><a class="nav-link" id="tab-sewaHotel" data-toggle="tab" href="#sewaHotel" aria-selected="false">Sewa Hotel</a></li>
            <li class="nav-item" role="presentation"><a class="nav-link" id="tab-pelbagai" data-toggle="tab" href="#pelbagai" aria-selected="false">Pelbagai</a></li>
            <li class="nav-item" role="presentation"><a class="nav-link" id="tab-pengesahan" data-toggle="tab" href="#pengesahan" aria-selected="false">Pengesahan</a></li>   
        </ul>  
    
 <div class="tab-content">  

  <%--Permohonan--%>
   <div id="menu1" class="tab-pane fade show active" role="tabpanel" aria-labelledby ="Permohonan">   <%--menu1--%>
    <asp:Panel ID="Panel1" runat="server" >                   
        <div class="col-md-12"> 
        <div class="form-row" >
            <div class="form-group col-md-3" style="left: -1px; top: 0px">                    
                <input type="text" id="noPermohonan"  class="input-group__input form-control input-md" style="background-color: #f0f0f0"  readonly>
                <label class="input-group__label" for="No.Permohonan" >No.Permohonan</label>
            </div>                
            <div class="form-group col-md-3">
                <select name="ddlTahun" id="ddlTahun"  class="input-group__select ui search dropdown tahun-list" placeholder="&nbsp;"></select>
                <label class="input-group__label" for="Tahun">Tahun</label>                   
            </div>                
            <div class="form-group col-md-3">
                <select name="ddlBulan" id="ddlBulan"  class="input-group__select ui search dropdown bulan-list" placeholder="&nbsp;"></select>
                <label class="input-group__label" for="Bulan">Bulan</label>                   
            </div>
        </div>
            <div class="col-md-12">        
        <div class="form-row">
        <input type ="hidden" id="selectedNoPendahuluan"/>
            <input type ="hidden" id="selectedJumlah"/>
            <input type ="hidden" id="monthInt"/>
            <input type ="hidden" id="tkhMohonCL"/>
            
        </div>
        </div>

        <div class="form-row">                              
            <div class="form-group col-md-3">
                    <select name="ddlKumpWang" id="ddlKumpWang"  class="input-group__select ui search dropdown KumWang-list" placeholder="&nbsp;"></select>
                <label class="input-group__label" for="Kum_wang">Kumpulan Wang</label>              
            </div>               
            <div class="form-group col-md-3">
                <select name="ddlOperasi" id="ddlOperasi"  class="input-group__select ui search dropdown KodOperasi-list" placeholder="&nbsp;"></select> 
                <label class="input-group__label" for="Kod Operasi"> Kod Operasi</label> 
            </div>               
            <div class="form-group col-md-3">
                <select name="ddlPTJ" id="ddlPTJ"  class="input-group__select ui search dropdown KodPTJ-list" placeholder="&nbsp;"></select>
                <label class="input-group__label" for="Kod Projek">Kod PTj</label>                 
            </div>             
            <div class="form-group col-md-3">
                <select name="ddlProjek" id="ddlProjek"  class="input-group__select ui search dropdown KodProjek-list" placeholder="&nbsp;"></select> 
                <label class="input-group__label" for="Kod Projek"> Kod Projek</label>                                                                          
            </div>
        </div>
    </div>
    <div class="form-row">
        <h6>Kontra Pendahuluan (jika ada):</h6><br /><br>
    </div>
    <div class="table-title">
        <br />
        <h6> &nbsp;&nbsp;Senarai Pendahuluan Yang Telah Diterima</h6>   
        <hr />   
    </div>

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
            
   <div class="form-group col-md-12" align="right">   
    <button id="btnsimpanInfo" type="button" class="btn btn-secondary btnSimpanInfo">Simpan</button>
</div>
  </asp:Panel>
    
    
    </div>  <%-- Tutup menu1"--%>

     <div id="menu2" class="tab-pane fade" aria-labelledby="tab-Kenyataan" role="tabpanel">  <%--menu2--%>         
          <asp:Panel ID="pnlKenyataan" runat="server">  
               <div class="col-md-10">        
                   <div class="form-row">
                   <div class="form-group col-md-5">
                   <input type="text"  id="txtMohonID"  class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3">
                   <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                       
                   </div>                    
                   <div class="form-group col-md-3">
                   <input type="date" id="tkhMohon2" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly>                                   
                   <label class="input-group__label" for="TarikhMohon">Tarikh Mohon:</label>                        
                   </div>                
                   </div>
              </div>              
              
              <div class="modal-body">
                <div>  
                    <h7>Kenyataan Tuntutan </h7>             
                </div>
                  <br />
                <div class="col-sm-12">
                <div class="transaction-table table-responsive">
                    <table id="tblKenyataan" class="table table-striped" style="width:100%">
                        <thead>
                            <tr>
                                <th width="3%" rowspan="2"><input type="checkbox" name="checkbox" class="list_checkbox form-control-checkbox-tbl" value="1" /></th>
                                <th width="9%" rowspan="2"><div align="center">Bil</div></th>
                                <th width="3%" rowspan="2"><div align="center">Mula</div></th>
                                <th width="7%" rowspan="2"><div align="center">Tarikh</div></th>
                                <th colspan="2" align="center"><div align="center">Waktu</div></th>
                                <th width="3%" rowspan="2"><div align="center">Tamat</div></th>
                                <th width="25%" rowspan="2"><div align="center">Tujuan/Tempat</div></th>
                                <th width="5%" rowspan="2"><div align="center">Jarak(KM)</div></th>
                             </tr>
                            <tr>
                                <th width="22%"><div align="center">Bertolak</div></th>
                                <th width="22%"><div align="center">Sampai</div></th>
                            </tr>
                            
                        </thead>
                        <tbody id="tableID_KenyataanTuntutan" >
                            <tr style="display: none;">
                                <td>
                                  <input type="checkbox" name="chckItem" id="chckItem" class="list_chckItem form-control-checkbox-tbl"  value="1" />                               
                                </td>
                                <td> 
                                    <input type="text"  ID="txtBil" class="list_Bil form-control-input-tbl" size="5" placeholder="&nbsp;" />                                         
                                </td>
                                <td>
                                    <input type="checkbox" id="chckMula" name="chckMula" class="list_chckMula form-control-checkbox-tbl" value="1"  />                                    
                                </td>
                                <td>                                
                                    <input type="date" id="tkhTuntut" name="tkhTuntut" class="list_tkhTuntut form-control" size="2" placeholder="&nbsp;"> 
                                </td>
                                <td><div align="center">                                
                                    <select id="ddlJamTolak" name="ddlJamTolak" class="input-group__select ui search dropdown list_ddlJamTolak"  >                                        
                                        <option value="01">01</option>
                                        <option value="02">02</option>
                                        <option value="03">03</option>
                                        <option value="04">04</option>
                                        <option value="05">05</option>
                                        <option value="06">06</option>
                                        <option value="07">07</option>
                                        <option value="08">08</option>
                                        <option value="09">09</option>
                                        <option value="10">10</option>
                                        <option value="11">11</option>
                                        <option value="12">12</option>
                                        </select> :  
                                        <select id="ddlMinitSmpai" class="input-group__select ui search dropdown list_ddlMinitSmpai">                                        
                                             <option value="00">00</option>
                                             <option value="10">10</option>
                                             <option value="20">20</option>
                                             <option value="30">30</option>
                                             <option value="40">40</option>
                                             <option value="50">50</option>
                                             <option value="59">59</option>                                        
                                         </select>  :  
                                            <select id="ddlDay" class="input-group__select ui search dropdown list_ddlDay" >                                        
                                              <option value="AM">AM</option>
                                              <option value="PM">PM</option>                                                                              
                                            </select> 
                                    </div>
                                </td>
                                <td><div align="center">
                                    <select id="ddlJamSampai"  class="input-group__select ui search dropdown list_ddlJamSampai" >                                        
                                          <option value="01">01</option>
                                          <option value="02">02</option>
                                          <option value="03">03</option>
                                          <option value="04">04</option>
                                          <option value="05">05</option>
                                          <option value="06">06</option>
                                          <option value="07">07</option>
                                          <option value="08">08</option>
                                          <option value="09">09</option>
                                          <option value="10">10</option>
                                          <option value="11">11</option>
                                          <option value="12">12</option>
                                      </select>   : 
                                        <select id="ddlMinitSampai" class="input-group__select ui search dropdown list_ddlMinitSampai">                                        
                                            <option value="00">00</option>
                                            <option value="10">10</option>
                                            <option value="20">20</option>
                                            <option value="30">30</option>
                                            <option value="40">40</option>
                                            <option value="50">50</option>
                                            <option value="59">59</option>                                        
                                        </select> : 
                                        <select id="ddlDaySampai" class="input-group__select ui search dropdown list_ddlDaySampai">                                        
                                             <option value="AM">AM</option>
                                             <option value="PM">PM</option>                                                                              
                                         </select>  
                                    </div>
                                   

                                </td>
                                <td>
                                  <input type="checkbox" name="chckTamat" class="list_chckTamat form-control-checkbox-tbl" value="1" />
                               </td>
                                <td>
                                <div align="center">  <textarea width="100%" rows="3" cols="25" ID="txtTujuan" class="list_txtTujuan form-control"  placeholder="&nbsp;" MaxLength="300"></textarea></div>
                                </td>
                                <td>                                 
                                 <div align="center"><input type="text"  id="txtJarak" class="list_txtJarak form-control-input-tbl" size="3" placeholder="&nbsp;" >                                      
                                   <br />
                                     <select id="ddlKenderaan" class="input-group__select ui search dropdown list_ddlKenderaan">                                        
                                     <option value="00">--Pilih Kenderaan--</option>
                                     <option value="UTEM1">UTEM1</option>
                                     <option value="UTEM2">UTEM2</option>
                                     <option value="UTEM3">UTEM3</option>
                                                                           
                                 </select>
                                     <br />
                                     <button type="button" class="btn-change" data-toggle="modal" data-target="#myKenderaan">Daftar Kenderaan</button> 
                                     </div>   
                                </td>
                              </tr>
                            </tbody>                            
                        </table>                               

                    
                </div>
                </div>
               

                <div class="row">
                    <div class="col-md-3">
                        <div class="btn-group">
                            <button type="button" class="btn btn-warning btnAddRow-tabK font-weight-bold" data-val="1" value="1"><b>+ Tambah</b></button>
                            <button type="button" class="btn btn-warning dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <div class="dropdown-menu">
                                <a class="dropdown-item btnAddRow-tabK five" value="5" data-val="5">Tambah 5</a>
                                <a class="dropdown-item btnAddRow-tabK" value="10" data-val="10">Tambah 10</a>

                            </div>
                        </div>
                    </div>
                 </div>

            <div class="form-row">
            <%--<div class="form-group col-md-6">
            <asp:LinkButton id="lbtnKembali" class="btn btn-primary" runat="server" onclick="lbtnKembali_Click"><i class="las la-angle-left"></i>Kembali</asp:LinkButton>
            </div>--%>
                <div class="form-group col-md-12" align="right">
                    <button type="button" class="btn btn-danger btnReset" onclick="btnReset()">Hapus</button>
                    <button id="btnSaveKenyataan" type="button" class="btn btn-secondary btnSimpan2" data-toggle="tooltip" data-placement="bottom" title="Draft" onclick="SaveTuntutanData()">Simpan</button>
                </div>
            </div>
            </div>

              <div><h6 style="color: #FF3300; font-size:12px">Klik Senarai Tuntutan untuk senarai tuntutan yang layak</h6></div>
                <div><h6 style="color: #FF3300; font-size:12px">Klik butang Tambah untuk menambah baik Kenyataan Tuntutan</h6></div>
                <div><h6 style="color: #FF3300; font-size:12px">Klik butang Hapus untuk hapus Kenyataan Tuntutan</h6></div>
          </asp:Panel>
     
     </div>  <%--Tutup KenyataanTab--%>

      <%--content tab-3--%> 
    <div id="elaunPjln" class="tab-pane fade" aria-labelledby="tab-elaunPjln" role="tabpanel"> <%--menu 3--%>
         <asp:Panel ID="panel10" runat="server"> 
              <div class="col-md-10">        
                     <div class="form-row">
                     <div class="form-group col-md-5">
                     <input type="text"  id="txtMohonID3"  class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3">
                     <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                       
                     </div>                    
                     <div class="form-group col-md-3">
                     <input type="date" id="tkhMohon3" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly>                                   
                     <label class="input-group__label" for="TarikhMohon">Tarikh Mohon:</label>                        
                     </div>                
                     </div>
                </div>    
              <div class="modal-body">
                    <div>  
                        <h7>Elaun Perjalanan Kenderaan </h7>             
                    </div>
                  <br />
                    <div>                           
                    <div class="col-md-12">  
                        <table  class="table table-striped" id="tblDataEP" style="width: 100%">
                            <thead>
                                <tr>
                                    <th style="width: 20%; text-align: center" scope="col">Kiraan Kilometer</th>
                                    <th style="width: 20%; text-align: center" scope="col">Kenderaan</th>                                
                                    <th style="width: 20%; text-align: center" scope="col">Jumlah Jarak</th>
                                    <th style="width: 20%;text-align: center" scope="col">Kadar Sekilometer (RM)</th>
                                    <th style="width: 20%;text-align: center" scope="col">Amaun</th>

                                </tr>
                            </thead>
                             <tbody id="tblDataEPList">
                                 <tr style="display: none; width: 100%" class="table-list">
                                     <td style="width: 10%">
                                         <input type="text" ID="txtKiraKilometer" class="list-txtKiraKilometer form-control" /><%-- //style="visibility: hidden"--%>
                                         <label id="hidkm" name="hidkm" class="hidkm-list" ></label>
                                     </td>
                                     <td style="width: 20%">
                                         <input id="txtKenderaanEP" name="txtKenderaanEP" type="number" class="list-kenderaanEP form-control" style="text-align: right" />
                                     </td>

                                     <td style="width: 20%">
                                         <input id="txtJumJarakEP" name="txtJumJarakEP" type="text" class="list-txtJumJarakEP form-control" style="text-align: right" />
                                     </td>
                                     <td style="width: 20%">
                                         <input id="txtKadarEP" name="txtKadarEP" type="text" class="list-txtKadarEP form-control" style="text-align: right" />
                                     </td>
                                     <td style="width: 20%">
                                         <input id="txtJumlahEP" name="txtJumlahEP" type="text" class="list-txtJumlahEP form-control" style="text-align: right" />
                                     </td>        

                                 </tr>
                             </tbody>
                            <tfoot>
                                <tr>
                                    <td colspan="3"></td>
                                    <td style="text-align: center">
                                        <%--<input class="form-control underline-input" id="totalDbt" name="totalDbt" style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly />--%>
                                        <label style="font-size:medium; align-items:end"> Jumlah (RM) </label>
                                    </td>
                                    <td>
                                        <input type="text" id="totalEP" class="input-group__input form-control-totalEP" placeholder="&nbsp;" style="background-color:#f3f3f3" >    
                                    </td>
                                </tr>
                                  <tr>
                                    <td colspan="2">
                                        <div class="btn-group">
                                            <button type="button" class="btn btn-warning btnAddRow_tabEP One" data-val="1" value="1"><b>+ Tambah</b></button>
                                            <button type="button" class="btn btn-warning btnAddRow dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                <span class="sr-only">Toggle Dropdown</span>
                                            </button>
                                            <div class="dropdown-menu">
                                                <a class="dropdown-item btnAddRow_tabEP five" value="5" data-val="5">Tambah 5</a>
                                                <a class="dropdown-item btnAddRow_tabEP" value="10" data-val="10">Tambah 10</a>

                                            </div>
                                        </div>
                                    </td>

                                   
                                </tr>
                            </tfoot>
                         </table>
                    </div>
                    </div>  
                   <div class="form-row"> 
                     <div class="form-group col-md-12" align="right">                     
                         <button id="btnSimpantab3" type="button" class="btn btn-secondary btnSimpantab3" data-toggle="tooltip" data-placement="bottom" title="Draft" >Simpan</button>
                     </div>
                 </div>
              </div>  
         </asp:Panel>
    </div>
     <div id="pengangkutan" class="tab-pane fade" aria-labelledby="tab-pengangkutan" role="tabpanel"> <%--menu 4--%>
       <asp:Panel ID="Panel11" runat="server"> 
            <div class="col-md-10">        
                <div class="form-row">
                    <div class="form-group col-md-5">
                        <input type="text"  id="txtMohonID4"  class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3">
                        <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                       
                    </div>                    
                    <div class="form-group col-md-3">
                        <input type="date" id="tkhMohon4" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly>                                   
                        <label class="input-group__label" for="TarikhMohon">Tarikh Mohon:</label>                        
                    </div>                
                </div>
           </div>   
           <div class="modal-body">
            <div>
                <h8>Tambang Pengangkutan Awam</h8>              
            </div>
               <br />
            <div>                           
                <div class="row">
                    <div class="col-md-12">
                        <div class="transaction-table table-responsive">
                            <table class="table table-striped" id="tblTambang" style="width: 100%;">
                                <thead>
                                    <tr style="width: 100%; text-align: center;">
                                        <th scope="col" style="width: 30%;vertical-align:middle; text-align: left;">Jenis Kenderaan Awam</th>
                                        <th scope="col" style="width: 15%;vertical-align:middle">Dengan Resit</th>
                                        <th scope="col" style="width: 15%;vertical-align:middle">Tanpa Resit</th>
                                        <th scope="col" style="width: 20%;vertical-align:middle">No Resit</th>
                                        <th scope="col" style="width: 20%;vertical-align:middle">Amaun(RM)</th>                                       
                                    </tr>
                                </thead>
                                <tbody id="tblTambangList">
                                    <tr class="table-list" width: 100%"  style="display:none;">
                                        <td>                                                      
                                        <select class="ui search dropdown ddlJenisTambangtblAwam-list" name="ddlJenisTambang" id="ddlJenisTambang"></select>
                                        <input type="hidden" class="data-id" value="" />                                     
                                        <label id="lblJenisTambang" name="lblJenisTambang" class="label-jenisTam-list" style="text-align: center;visibility: hidden"></label>
                                        <label id="HidlblJenisTambang" name="HidlblJenisTambang" class="Hid-jenisTam-list" style="visibility: hidden"></label>
                                        </td >
                                        <td style="text-align:center">
                                        <input type="checkbox" name="checkbox_DengResit"  class="lblDengResit_list" style="text-align:center; vertical-align: middle;" >
                                        <label class="lblDengResit" id="lblDengResit" name="lblDengResit"></label>
                                        </td>
                                        <td style="text-align:center">
                                        <input type="checkbox" name="checkbox_TanpaResit" class="lblTanpaResit-list" style="text-align:center; vertical-align: middle;" >
                                        <label class="lblTanpaResit" id="lblTanpaResit" name="lblTanpaResit"></label>
                                        </td>
                                        <td >
                                        <center><input type="text" class="form-control input-md lblnoResit-list" id="noResit" style="background-color:#f3f3f3;font-size:small"></center>
                                        <label class="lblnoResit" id="lblnoResit" name="lblnoResit"></label>
                                        </td>
                                        <td>
                                        <input type="text" class="form-control input-md AmaunTambang-list" id="AmaunTambang" style="background-color:#f3f3f3;font-size:small" >
                                        <label class="lblAmaunTambang" id="lblAmaunTambang" name="lblAmaunTambang"></label>
                                        </td>      
   
                                    </tr>

                                </tbody>    
                                    <tfoot>
                                    <tr>
                                        <td colspan="3"></td>
                                        <td>
                                            <%--<input class="form-control underline-input" id="totalDbt" name="totalDbt" style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly />--%>
                                            <label style="font-size:medium; align-items:end"> Jumlah (RM) </label>
                                        </td>
                                        <td>
                                            <input class="form-control underline-input" id="totalTblTambang" name="totalTblTambang" style="text-align: right; font-weight: bold" width="8%" value="0.00" readonly /></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="btn-group">
                                                <button type="button" class="btn btn-warning btnAddRow-tabPA One" data-val="1" value="1"><b>+ Tambah</b></button>
                                                <button type="button" class="btn btn-warning btnAddRow dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    <span class="sr-only">Toggle Dropdown</span>
                                                </button>
                                                <div class="dropdown-menu">
                                                    <a class="dropdown-item btnAddRow-tabPA five" value="5" data-val="5">Tambah 5</a>
                                                    <a class="dropdown-item btnAddRow-tabPA" value="10" data-val="10">Tambah 10</a>

                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    </tfoot>
                            </table>
                        </div>
                        </div>
                    </div>

                  <div class="form-row"> 
                      <div class="form-group col-md-12" align="right">                     
                          <button id="btnSimpantab4" type="button" class="btn btn-secondary btnSimpantab4" data-toggle="tooltip" data-placement="bottom" title="Draft" >Simpan</button>
                      </div>
                  </div>
            </div> 
         </div>  
        </asp:Panel>
     </div>
    <div id="elaunMakan" class="tab-pane fade" aria-labelledby="tab-elaunMakan" role="tabpanel"> <%--menu 5--%>
        <asp:Panel ID="Panel5" runat="server">  
         <div class="col-md-10">        
             <div class="form-row">
             <div class="form-group col-md-5">
             <input type="text"  id="txtMohonID5"  class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3">
             <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                       
             </div>                    
             <div class="form-group col-md-3">
             <input type="date" id="tkhMohon5" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly>                                   
             <label class="input-group__label" for="TarikhMohon">Tarikh Mohon:</label>                        
             </div>                
             </div>
        </div>   
        <div class="modal-body">
        <div>
            <h8>Elaun Makan / Elaun Harian</h8>           
        </div>
            <br />
        <div>                           
         <div class="row">
        <div class="col-md-12">
        <div class="transaction-table table-responsive " style="overflow-x:visible">
             <table class="table table-striped" id="tblElaunMkn" style="width: 100%;">
                    <thead>
                        <tr style="width: 100%; text-align: center;">
                            <th scope="col" style="width: 20%;" rowspan="2">Jumlah Hari(Pergi dan Pulang)</th>
                            <th scope="col" style="width: 10%;" rowspan="2">Jenis Perjalanan</th>
                            <th scope="col" style="width: 20%;" rowspan="2">Pilih/Tandakan</th>
                            <th scope="col" style="width: 15%;" rowspan="2">Harga</th>
                            <th scope="col" style="width: 20%;" rowspan="2">Tempat</th>
                            <th scope="col" style="width: 15%;" rowspan="2">Jumlah(RM)</th>

                           
                        </tr>                                         
                    </thead> 
                    <tbody id="tblElaunMkn-list" style="width: 100%;" class="table-list">
                    <tr style="display:none;">
                        <td style="width: 20%; text-align: justify;">                            
                            <input type="text" id="txtbilEL" class="input-group__input_td form-control-txtbilEL"/>                             
                        </td>
                        <td style="width: 10%; text-align: justify;">                                                                             
                            <select class="ui search dropdown JenisTugasElaunMkn-list" name="ddlJenisTugasElaunMkn" id="ddlJenisTugasElaunMkn"></select>
                            <input type="hidden" class="data-id" value="" />                                     
                            <label id="lblJenisTugasElaunMkn" name="lblJenisTugasElaunMkn" class="label-JenisTugasElaunMkn-list" style="text-align: center;visibility: hidden"></label>
                            <label id="HidJenisTugasElaunMkn" name="HidJenisTugasElaunMkn" class="Hid-JenisTugasElaunMkn-list" style="visibility: hidden"></label>
                        </td>
                        <td style="width: 20%;text-align: left;">Sila Tandakan Pilihan Anda : <br />                                                        
                            <label style="vertical-align:middle" text-align: left;"> <input type="checkbox" name="checkbox4" value="checkbox" /> Sarapan Pagi</label><br /> 
                            <label style="vertical-align:middle" text-align: left;"> <input type="checkbox" name="checkbox5" value="checkbox" />  Makan Tengahari</label> <br />                            
                            <label style="vertical-align:middle" text-align: left;"> <input type="checkbox" name="checkbox6" value="checkbox" />  Makan Malam</label>
                        </td>
                        <td style="width: 15%;text-align: justify;">
                             <input type="text" id="txtHargaEL" class="input-group__input form-control-txtHargaEL"/>
                        </td>
                        <td style="width: 20%;text-align: justify;">
                            <select class="ui search dropdown JnstempatElaunMkn-list" name="ddlTmptElaunMkn" id="ddlTmptElaunMkn"></select>
                             <input type="hidden" class="data-id" value="" />                                     
                             <label id="lblJnstempatElaunMkn" name="lblJnstempatElaunMkn" class="label-JnstempatElaunMkn-list" style="text-align: center;visibility: hidden"></label>
                             <label id="HidJnstempatElaunMkn" name="HidJnstempatElaunMkn" class="Hid-JnstempatElaunMkn-list" style="visibility: hidden"></label>
                        </td>
                        <td style="width: 15%;">
                            <input type="text" id="txtJumlahEL" class="input-group__input form-control-txtJumlahEL" placeholder="&nbsp;" style="background-color:#f3f3f3" >
                        </td>
                   </tr>

                </tbody>    

                <tfoot>
                <tr>
                    <td colspan="4"></td>
                    <td>
           
                        <label style="font-size:medium; align-items:end" > Jumlah (RM) </label>
                    </td>
                    <td>
                        <input class="form-control underline-input" id="totalKt" name="totalKt" style="text-align: right; font-weight: bold" width="8%" value="0.00" readonly /></td>
                    <td></td>
                </tr>
                    <tr>
                    <td colspan="2">
                        <div class="btn-group">
                            <button type="button" class="btn btn-warning btnAddRow-tabElaunMkn One" data-val="1" value="1"><b>+ Tambah</b></button>
                            <button type="button" class="btn btn-warning btnAddRow dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <div class="dropdown-menu">
                                <a class="dropdown-item btnAddRow-tabElaunMkn five" value="5" data-val="5">Tambah 5</a>
                                <a class="dropdown-item btnAddRow-tabElaunMkn" value="10" data-val="10">Tambah 10</a>

                            </div>
                        </div>
                    </td>

                </tr>
            </tfoot>
        </table>
        </div>           

            </div>
        </div>
         </div>
        </div>
      </asp:Panel>
     </div>

     
    <div id="sewaHotel" class="tab-pane fade" aria-labelledby="tab-sewaHotel" role="tabpanel">   <%--menu 6--%>
         <asp:Panel ID="Panel4" runat="server" > 
              <div class="col-md-10">        
                 <div class="form-row">
                 <div class="form-group col-md-5">
                 <input type="text"  id="txtMohonID6"  class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3">
                 <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                       
                 </div>                    
                 <div class="form-group col-md-3">
                 <input type="date" id="tkhMohon6" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly>                                   
                 <label class="input-group__label" for="TarikhMohon">Tarikh Mohon:</label>                        
                 </div>                
                 </div>
            </div>  

            <div class="modal-body">
                  <div>
                      <h8>Tuntutan Bayaran Sewa Hotel </h8>         
                  </div>                 
                <div>
                    <div class="row">
    <div class="col-md-12">
        <div class="transaction-table table-responsive">
            <table class="table table-striped" id="tblSewaHotel" style="width: 100%;">
                <thead>
                    <tr style="width: 100%; text-align: center;">
                        <th scope="col" style="width: 10%;vertical-align:middle; text-align: left;">No</th>
                        <th scope="col" style="width: 10%;vertical-align:middle">Jenis Tugas</th>
                        <th scope="col" style="width: 25%;vertical-align:middle">Tempat</th>
                        <th scope="col" style="width: 15%;vertical-align:middle">No Resit</th>
                        <th scope="col" style="width: 15%;vertical-align:middle">Elaun(RM)/Hari</th> 
                        <th scope="col" style="width: 10%;vertical-align:middle">Hari</th>
                        <th scope="col" style="width: 15%;vertical-align:middle">Amaun Tuntutan (RM)</th>
                    </tr>
                </thead>
                <tbody id="tblSewaHotelData">
                   
                <tr class="table-list" width: 100%" style="display:none;">
                    <td>
                    <input type="text" class="form-control input-md-txtBiltblHotel-list" id="txtBiltblHotel" style="background-color:#f3f3f3;font-size:small" >
                    <label class="lblBiltblHotel-list" id="lblBiltblHotel" name="bilhotel"></label>
                    </td>
                    <td>                                                      
                    <select class="ui search dropdown ddlJenisTugastblHotel-list" name="ddlJenisTugastblHotel" id="ddlJenisTugastblHotel"></select>
                    <input type="hidden" class="data-id" value="" />                                     
                    <label id="lblJenisTugastblHotel" name="lblJenisTugastblHotel" class="label-JenisTugastblHotel-list" style="text-align: center;visibility: hidden"></label>
                    <label id="HidJenisTugastblHotel" name="HidJenisTugastblHotel" class="Hid-JenisTugastblHotel-list" style="visibility: hidden"></label>
                    </td>

                    <td>                                                      
                    <select class="ui search dropdown ddltempattblHotel-list" name="ddlTmpttblHotel" id="ddlTmpttblHotel"></select>
                    <input type="hidden" class="data-id" value="" />                                     
                    <label id="lblJnstempattblHotel" name="lblJnstempattblHotel" class="label-JnstempattblHotel-list" style="text-align: center;visibility: hidden"></label>
                    <label id="HidJnstempattblHotel" name="HidJnstempattblHotel" class="Hid-JnstempattblHotel-list" style="visibility: hidden"></label>
                    </td>

                    <td>
                    <input type="text" class="form-control input-md resittblHotel-list" id="resittblHotel" style="font-size:small" >
                    <label class="lblresittblHotel-list" id="lblresittblHotel" name="lblresittblHotel"></label>
                    </td>

                    <td>
                    <input type="text" class="form-control input-md elauntblHotel-list" id="elauntblHotel" disabled="disabled" style="background-color:#f3f3f3;font-size:small">
                    <label class="lblelauntblHotel-list" id="lblelauntblHotel" name="lblelauntblHotel"></label>
                    </td>

                    <td>
                    <input type="text" class="form-control input-md haritblHotel-list" id="haritblHotel" style="font-size:small" >
                    <label class="lblharitblHotel-list" id="lblharitblHotel" name="lblharitblHotel"></label>
                    </td>
   
                    <td>
                    <input type="text" class="form-control input-md amauntblHotel-list" id="amauntblHotel" style="background-color:#f3f3f3;font-size:small">
                    <label class="lblamauntblHotel-list" id="lblamauntblHotel" name="lblamauntblHotel"></label>
                    </td>                       

                </tr >

                </tbody>    
                    <tfoot>
                    <tr>
                        <td colspan="4"></td>
                        <td colspan="2">
                            <%--<input class="form-control underline-input" id="totalDbt" name="totalDbt" style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly />--%>
                            <label style="font-size:medium; align-items:end"> Jumlah (RM) </label>
                        </td>
                        <td>
                            <input class="form-control underline-input" id="totalTblTambang1" name="totalTblTambang" style="text-align: right; font-weight: bold" width="8%" value="0.00" readonly /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div class="btn-group">
                                <button type="button" class="btn btn-warning btnAddRow-tabHotel One" data-val="1" value="1"><b>+ Tambah</b></button>
                                <button type="button" class="btn btn-warning btnAddRow dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <span class="sr-only">Toggle Dropdown</span>
                                </button>
                                <div class="dropdown-menu">
                                    <a class="dropdown-item btnAddRow-tabHotel five" value="5" data-val="5">Tambah 5</a>
                                    <a class="dropdown-item btnAddRow-tabHotel" value="10" data-val="10">Tambah 10</a>

                                </div>
                            </div>
                        </td>
                    </tr>
                    </tfoot>
            </table>
        </div>
        </div>
    </div>

<div class="row">
     <div>
     <h8>&nbsp;&nbsp;Tuntutan Bayaran Elaun Penginapan Lojing</h8>         
 </div>
<div class="col-md-12">
    <div class="transaction-table table-responsive">
        <table class="table table-striped" id="tblLojing" style="width: 100%;">
            <thead>
                <tr style="width: 100%; text-align: center;">
                    <th scope="col" style="width: 10%;vertical-align:middle; text-align: left;">No</th>
                    <th scope="col" style="width: 10%;vertical-align:middle">Jenis Tugas</th>
                    <th scope="col" style="width: 25%;vertical-align:middle">Tempat</th>
                    <th scope="col" style="width: 15%;vertical-align:middle">No Resit</th>
                    <th scope="col" style="width: 15%;vertical-align:middle">Elaun(RM)/Hari</th> 
                    <th scope="col" style="width: 10%;vertical-align:middle">Hari</th>
                    <th scope="col" style="width: 15%;vertical-align:middle">Amaun Tuntutan (RM)</th>
                </tr>
            </thead>
            <tbody id="tblLojingData">
                <tr class="table-list" width: 100%" style="display:none;">
                    <td>
                    <input type="text" class="form-control input-md-txtBiltblLojing" id="txtBiltblLojing" style="background-color:#f3f3f3;font-size:small" >
                    <label class="lblAmaun" id="lblBiltblLojing" name="bilhotel"></label>
                    </td>
                    <td>                                                      
                    <select class="ui search dropdown ddlJenisTugastblLojingA-list" name="ddlJenisTugastblLojing" id="ddlJenisTugastblLojing"></select>
                    <input type="hidden" class="data-id" value="" />                                     
                    <label id="lblJenisTugastblLojing" name="lblJenisTugastblLojing" class="label-JenisTugastblLojing-list" style="text-align: center;visibility: hidden"></label>
                    <label id="HidJenisTugastblLojing" name="HidJenisTugastblLojing" class="Hid-JenisTugastblLojing-list" style="visibility: hidden"></label>
                    </td>

                    <td>                                                      
                    <select class="ui search dropdown ddlTmpttblLojingA-list" name="ddlTmpttblLojing" id="ddlTmpttblLojing"></select>
                    <input type="hidden" class="data-id" value="" />                                     
                    <label id="lblJnstempattblLojing" name="lblJnstempattblLojing" class="label-JnstempattblLojing-list" style="text-align: center;visibility: hidden"></label>
                    <label id="HidJnstempattblLojing" name="HidJnstempattblLojing" class="Hid-JnstempattblLojing-list" style="visibility: hidden"></label>
                    </td>

                    <td>
                    <input type="text" class="form-control input-md" id="resittblLojing" style="font-size:small" >
                    <label class="resit-list" id="lblresittblLojing" name="lblresittblLojing"></label>
                    </td>

                    <td>
                    <input type="text" class="form-control input-md" id="elauntblLojing" disabled="disabled" style="background-color:#f3f3f3;font-size:small">
                    <label class="lblAmaunLojing-list" id="lblelauntblLojing" name="lblelauntblLojing"></label>
                    </td>

                    <td>
                    <input type="text" class="form-control input-md" id="haritblLojing" style="font-size:small" >
                    <label class="lblharitblLojing-list" id="lblharitblLojing" name="lblharitblLojing"></label>
                    </td>
   
                    <td>
                    <input type="text" class="form-control input-md" id="amauntblLojing" style="background-color:#f3f3f3;font-size:small">
                    <label class="lblAmaunLojing-list" id="lblamauntblLojing" name="lblamauntblLojing"></label>
                    </td>                  
 
                </tr >

            </tbody>    
                <tfoot>
                <tr>                    
                    <td>Alamat Lojing</td>
                    <td colspan="3"><textarea rows="2" cols="45" ID="txtAlamatLojing" name="AlamatLojing" class="input-group__input form-control" placeholder="&nbsp;" MaxLength="500"></textarea></td>
                   
                    <td colspan="2">
                        <%--<input class="form-control underline-input" id="totalDbt" name="totalDbt" style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly />--%>
                        <label style="font-size:medium; align-items:end"> Jumlah (RM) </label>
                    </td>
                    <td>
                        <input class="form-control underline-input" id="totalTblLojing" name="totalTblLojing" style="text-align: right; font-weight: bold" width="8%" value="0.00" readonly /></td>
                    <td></td>
                </tr>
                    
                <tr>
                    <td colspan="2">
                        <div class="btn-group">
                            <button type="button" class="btn btn-warning btnAddRow-tabLojing One" data-val="1" value="1"><b>+ Tambah</b></button>
                            <button type="button" class="btn btn-warning btnAddRow dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                <span class="sr-only">Toggle Dropdown</span>
                            </button>
                            <div class="dropdown-menu">
                                <a class="dropdown-item btnAddRow-tabLojing five" value="5" data-val="5">Tambah 5</a>
                                <a class="dropdown-item btnAddRow-tabLojing" value="10" data-val="10">Tambah 10</a>

                            </div>
                        </div>
                    </td>
                </tr>
                </tfoot>
        </table>
    </div>
    </div>
</div>
              
               </div>              
                  
              
             
          </div>
        
      <div><h8 style="color: #FF3300">Nota:</h8></div>
     <div><h8 style="color: #FF3300">SM = Semenanjung Malaysia</h8></div>
     <div><h8 style="color: #FF3300">SS = Sabah dan Sarawak</h8></div>
                 
    </asp:Panel>
  </div>

     <div id="pelbagai" class="tab-pane fade" aria-labelledby="tab-pelbagai" role="tabpanel">   <%--menu 7--%>
      <asp:Panel ID="Panel6" runat="server" >    
            <div class="col-md-10">        
                 <div class="form-row">
                 <div class="form-group col-md-5">
                 <input type="text"  id="txtMohonID7"  class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3">
                 <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                       
                 </div>                    
                 <div class="form-group col-md-3">
                 <input type="date" id="tkhMohon7" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly>                                   
                 <label class="input-group__label" for="TarikhMohon">Tarikh Mohon:</label>                        
                 </div>                
                 </div>
            </div>   
      <div class="modal-body">
          <div>
              <h8>Tuntutan Pelbagai</h8>
              <br />
          </div>
        <br />
      <div>                           
      <div class="row">
          <div class="col-md-12">
              <div class="transaction-table table-responsive">
                   <table class="table table-striped" id="tblPelbagai" style="width: 100%;">
     <thead>
         <tr style="width: 100%; text-align: center;">
             <th scope="col" style="width: 50%;vertical-align:middle; text-align: left;">Jenis Belanja Pelbagai</th>
             <th scope="col" style="width: 10%;vertical-align:middle">Dengan Resit</th>
             <th scope="col" style="width: 10%;vertical-align:middle">Tanpa Resit</th>
             <th scope="col" style="width: 15%;vertical-align:middle">No Resit</th>
             <th scope="col" style="width: 15%;vertical-align:middle">Amaun(RM)</th>                                       
         </tr>
     </thead>
     <tbody id="tblPelbagaiList">
            <tr class="table-list" width: 100%" style="display:none;">
                <td>                                                      
                    <select class="ui search dropdown JenisPelbagai-list" name="ddlJenisPelbagai" id="ddlJenisPelbagai"></select>
                    <input type="hidden" class="data-id" value="" />                                     
                    <label id="lblJenisPelbagai" name="lblJenisPelbagai" class="label-jenisPelbagai-list" style="text-align: center;visibility: hidden"></label>
                    <label id="HidlblJenisPelbagai" name="HidlblJenisPelbagai" class="Hid-jenisPelbagai-list" style="visibility: hidden"></label>
                </td >
                <td style="text-align:center">
                    <input type="checkbox" name="checkbox_DengResitBP" class="checkbox_DengResitBP-list" style="text-align:center; vertical-align: middle;" >
                    <label class="lblDengResitBP-list" id="lblDengResitBP" name="lblDengResitBP"></label>
                </td>
                <td style="text-align:center">
                    <input type="checkbox" name="checkbox_TanpaResitBP" class="checkbox_TanpaResitBP-list"  style="text-align:center; vertical-align: middle;" >
                    <label class="lblTanpaResitBP-list" id="lblTanpaResitBP" name="lblTanpaResitBP"></label>
                </td>
                <td >
                    <center><input type="text" class="form-control input-md-noResitBP" id="noResitBP" name="noResitBP" style="background-color:#f3f3f3;font-size:small"></center>
                    <label class="lblnoResitBP-list" id="lblnoResitBP" name="lblnoResitBP"></label>
                </td>
                <td>
                    <input type="text" class="form-control input-md-AmaunBP" id="AmaunBP" style="background-color:#f3f3f3;font-size:small" >
                    <label class="lblAmaunBP-list" id="lblAmaunBP" name="lblAmaunBP"></label>
                </td>

            <%--<td class="tindakan">
            <button class="btn btnDelete">
            <i class="fa fa-trash" style="color: red"></i>
            </button>
            <button class="btn"><i class="fa fa-trash"></i> Trash</button>
            </td>--%>
            </tr >

     </tbody>    
         <tfoot>
         <tr>
             <td colspan="3"></td>
             <td>
                 <%--<input class="form-control underline-input" id="totalDbt" name="totalDbt" style="text-align: right; font-weight: bold" width="10%" value="0.00" readonly />--%>
                 <label style="font-size:medium; align-items:end"> Jumlah (RM) </label>
             </td>
             <td>
                 <input class="form-control underline-input" id="totalRm" name="totalRm" style="text-align: right; font-weight: bold" width="8%" value="0.00" readonly /></td>
             <td></td>
         </tr>
         <tr>
             <td colspan="2">
                 <div class="btn-group">
                     <button type="button" class="btn btn-warning btnAddRow-tabPelbagai One" data-val="1" value="1"><b>+ Tambah</b></button>
                     <button type="button" class="btn btn-warning btnAddRow dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                         <span class="sr-only">Toggle Dropdown</span>
                     </button>
                     <div class="dropdown-menu">
                         <a class="dropdown-item btnAddRow-tabPelbagai five" value="5" data-val="5">Tambah 5</a>
                         <a class="dropdown-item btnAddRow-tabPelbagai" value="10" data-val="10">Tambah 10</a>

                     </div>
                 </div>
             </td>
         </tr>
         </tfoot>
 </table>
              </div>
          </div>
      </div>
      </div>

        </div> <%--Tutup Bahagian C modal-body--%>

         </asp:Panel>
     
     </div>  <%--Tutup tab 7--%> 

     <div id="pengesahan" class="tab-pane fade" aria-labelledby="tab-pengesahan" role="tabpanel">   <%--menu 8--%>
     <asp:Panel ID="Panel8" runat="server" >    
           <div class="col-md-10">        
                <div class="form-row">
                <div class="form-group col-md-5">
                <input type="text"  id="txtMohonID8"  class="input-group__input form-control" placeholder="&nbsp;" style="background-color:#f3f3f3">
                <label for="noPermohonan" class="input-group__label">No.Permohonan:</label>                       
                </div>                    
                <div class="form-group col-md-3">
                <input type="date" id="tkhMohon8" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" readonly>                                   
                <label class="input-group__label" for="TarikhMohon">Tarikh Mohon:</label>                        
                </div>                
                </div>
           </div>   
     <div class="modal-body">
         <div>
             <h8>Pengakuan Dan Pengesahan</h8>
             <br />
         </div>
       <br />
     <div>                           
     <div class="row">
         <div class="col-md-12">
            <input type="checkbox" ID="chckSah" value="1" checked /> Saya mengaku bahawa <br />
             a) Perjalanan pada tarikh-tarikh tersebut adalah benar dan telah dibuat atas tugas rasmi <br />
             b)Tuntutan ini dibuat mengikut kadar dan syarat seperti yang dinyatakan dibawah peraturan-peraturan bagi pegawai bertugas rasmi dan/ 
             atau pegawai berkursus yang berkuatkuasa semasa. <br />
             c) Perbelanjaan yang bertanda (*) berjumlah RM0.00 telah sebenarnya dilakukan dan dibayar oleh saya.<br />
             d) Butir - butir seperti yang dinyatakan diatas adalah benar dan saya bertanggungjawab terhadapnya.

         </div>
     </div>
     </div>

          <div class="form-row">
     <div class="form-group col-md-12" align="right">
        <%-- <button type="button" class="btn btn-danger">Padam</button>--%>
         <button type="button" class="btn btn-secondary btnSimpan">Simpan</button>
     </div>
 </div>

       </div> <%--Tutup Bahagian Pengesahan modal-body--%>

        </asp:Panel>
    
    </div>  <%--Tutup tab 8--%> 
 
</div>

        
 </div>
    
 

        <!-- Modal myKenderaan -->
<div id="myKenderaan" class="modal fade" role="dialog">
    <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
       
    <!-- Modal content-->
    <div class="modal-content">
    <div class="modal-header"><h4>Daftar Kenderaan</h4>
    <button type="button" class="close" data-dismiss="modal"></button>
    <h4 class="modal-title"></h4> 
    </div>
    <div class="modal-body">
       
             <asp:Panel ID="Panel3" runat="server" >
                         <div class="form-row">                                      
                            <div class="form-group col-sm-3">
                                 <input type="text" id="txtNamaPK" name="Nama" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"/> 
                                <label class="input-group__label" for="Nama">Nama Pegawai</label>                                       
                                 <%--<asp:TextBox ID="txtNamaP" runat="server" Width="100%" class="form-control input-sm" style="background-color:#f3f3f3"></asp:TextBox>--%>
                            </div>                                    
                            <div class="form-group col-sm-3">
                                 <input type="text" ID="txtNoPekerjaK"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />  
                                <label class="input-group__label"  for="No.Pekerja">No.Staf</label>                                                               
                            </div>  
                              <div class="form-group col-sm-3">
                                  <input type="text" ID="txtPtjK"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />  
                                 <label class="input-group__label"  for="No.Pekerja">PTJ</label>                                                               
                             </div> 
                              <div class="form-group col-sm-3">
                                 <input type="text" ID="txtTujuanPjlnK"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" />  
                                <label class="input-group__label"  for="Tujuan Perjalanan">Tujuan Perjalanan</label>                                                               
                            </div>  
                       </div>

                        <div class="form-row">
                            <div class="form-group col-sm-3">
                                 <input type="text" ID="TxtJnsKenderaan"  Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0"  />  
                                <label class="input-group__label" for="Jenis Kenderaan">Jenis Kenderaan</label>                        
                            </div>
                            <div class="form-group col-sm-3">
                                 <input type="text" ID="txtNoDaftarK" Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" /> 
                                <label class="input-group__label" for="No Daftar">No Daftar Kenderaan</label>                                     
                            </div>
                            <div class="form-group col-sm-3">
                                 <input type="text" ID="txtKelasK" Width="100%" class="input-group__input" placeholder="&nbsp;" readonly style="background-color: #f0f0f0" /> 
                                <label class="input-group__label" for="KelasKendeaan">Kuasa/Kelas</label>                                     
                            </div>
                            <div class="form-group col-sm-3">
                                  <input type="date" id="tkhPjln" class="input-group__input form-control tkhPjln"  placeholder="&nbsp;">
                                    <label class="input-group__label" for="Tarikh Perjalanan">Tarikh Perjalanan</label>                                     
                            </div>
                        </div>

                         <div class="form-row">
                            <asp:Panel ID="Panel7" runat="server">           
                                <b>*Saya seperti nama diatas dengan ini memohon kebenaran tuan menggunakan Kenderaan sendiri kerana :</b><br />
                                    
                                    <br /><input type="checkbox" ID="sebab1" value="1" /> &nbsp;&nbsp;Jarak sehala antara kedua-dua tempat adalah kurang dari 240km
                                    <br /><input type="checkbox" ID="sebab2" value="2" /> &nbsp;&nbsp;Dikehendaki menjalankan tugas rasmi di beberapa tempat di sepanjang perjalanan
                                    <br /><input type="checkbox" ID="sebab3" value="3" /> &nbsp;&nbsp;Keperluan mustahak/keperluan lain : &nbsp;
                                        &nbsp;&nbsp;<textarea rows="2" cols="45" name="sebabLain" class="input-group__input form-control"  ></textarea>
                                       
                                        <br /><input type="checkbox" ID="sebab4" value="4" />Berkongsi kenderaan dengan : <br />
                                           <table class="table table-striped"  width="75%" border="1" cellpadding="1" cellspacing="1">
                                              <tr>
                                                <td width="5%" bgcolor="#CCCCCC"><div align="center">No</div></td>
                                                <td width="10%" bgcolor="#CCCCCC"><div align="center">No.Staf</div></td>
                                                <td width="60%" bgcolor="#CCCCCC"> <div align="center">Nama</div></td>
                                              </tr>
                                              <tr>
                                                <td>1.</td>
                                                <td><div align="center"><input type="text" name="namaBersama1" /></div></td>
                                                <td><input type="text" name="nostafBersama1" size="75%" /></td>
                                              </tr>
                                              <tr>
                                                <td>2.</td>
                                                <td><div align="center"><input type="text" name="namaBersama2" /></div></td>
                                                <td><div align="left"><input type="text" name="nostafBersama2"  size="75%" /></div></td>
                                              </tr>
                                               <tr >
                                                  <td>3.</td>
                                                  <td><div align="center"><input type="text" name="namaBersama3" /></div></td>
                                                  <td><input type="text" name="nostafBersama3" size="75%" /></td>
                                                </tr>
                                            </table>
                                        
                                    
                                  <br />
                                * Sekiranya perjalanan melebihi 240 km sehala, saya bersetuju untuk dibayar
                                    tambang gantian mengikut kadar tambang keretapi/kapal terbang.
                               <br />
                               </asp:Panel>
                            
                        </div>

                      
            </asp:Panel>
    </div>
    <div class="modal-footer">
         <button type="button" class="btn btn-secondary btnSimpan">Simpan</button>
    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
    </div>
    </div>

    </div>
</div>

  


    <script type="text/javascript">
        var curNumObject = 0;
        var bilKenyataan = 0; 
        var bilKenyataan = 0; 
        var tbl = null
        var tbl2 = null
        var shouldPop = true;
        var isClicked = false;
        const dateInput = document.getElementById('tkhMohonCL');
        document.getElementById("tkhMohonCL").disabled = true;
       

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

        $(document).ready(function () {            
            getDataPeribadi();            

            $('#ddlStaf').dropdown({
                fullTextSearch: false,
                onChange: function () {   //function bila klik ddlstaf.pilih nama staf then auto load maklumat staf.
                    getDataPeribadi($(this).val())  //baca value bila pilih nama pada ddlStaf selection
                },
                apiSettings: {
                    url: 'MohonTuntutan_WS.asmx/fnCariStaf?q={query}',
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

                        $(obj).dropdown('show');

                    }
                }
            });
        });

        function ShowPopup(elm) {

            if (elm == "1") {
                $('#permohonan').modal('toggle');


            }
            else if (elm == "2") {
                $(".modal-body div").val("");
                $('#SenaraiPermohonan').modal('toggle');

            }
        }

        $('.btnSearch').click(async function () {
            isClicked = true;
            tbl.ajax.reload();
        })


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
                    "url": "MohonTuntutan_WS.asmx/LoadOrderRecord_PermohonanSendiri",
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
                    "url": "MohonTuntutan_WS.asmx/LoadRecord_PermohonanPP",
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
            
            $('#selectedNoPendahuluan').val(orderDetail.No_Pendahuluan);
            $('#selectedJumlah').val(orderDetail.Jum_Lulus);
            console.log("1876")
            console.log(orderDetail.No_Pendahuluan);
           


        }

        $(function () {
            console.log("1869")
            //$("#listInfo").tabs({
            //    disabled: [0, 1]
            //});

            //$('#listInfo').data('disabled.tabs', [2]);
           // $('#listInfo').data('disabled.tabs', [2,4]);
        }); 

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
            console.log("1947")
            console.log(orderDetail.No_Pendahuluan)
            //ni digunakan untuk reload semula datatable  tblListPend n check nopendahuluan yang telah disimpan
            $('#tblListPend > tbody > tr').each(function (ind, obj) {
                $row = $(obj);
                $checkBox = $row.eq("0").find("input");
                if ($checkBox.data("nomohon") === $('#selectedNoPendahuluan').val()) {
                    $checkBox.prop("checked", true);
                    console.log("masuk check true")
                } else {
                    $checkBox.prop("checked", false);
                    console.log("masuk check false")
                }
            })


            var newId = $('#ddlKumpWang')
            var ddlKumpWang = $('#ddlKumpWang')
            var ddlSearch = $('#ddlKumpWang')
            var ddlText = $('#ddlKumpWang')
            var selectObj_KumpWang = $('#ddlKumpWang')
            $(ddlKumpWang).dropdown('set selected', orderDetail.Kod_Kump_Wang);
            selectObj_KumpWang.append("<option value = '" + orderDetail.Kod_Kump_Wang + "'>" + orderDetail.colKW + "</option>")

            var newId = $('#ddlOperasi')
            var ddlKO = $('#ddlOperasi')
            var ddlSearch = $('#ddlOperasi')
            var ddlText = $('#ddlOperasi')
            var selectObj_ddlKO = $('#ddlOperasi')
            $(ddlKO).dropdown('set selected', orderDetail.Kod_Operasi);
            selectObj_ddlKO.append("<option value = '" + orderDetail.Kod_Operasi + "'>" + orderDetail.colKO + "</option>")

            var newId = $('#ddlProjek')
            var ddlKP = $('#ddlProjek')
            var ddlSearch = $('#ddlProjek')
            var ddlText = $('#ddlProjek')
            var selectObj_ddlKP = $('#ddlProjek')
            $(ddlKP).dropdown('set selected', orderDetail.Kod_Projek);
            selectObj_ddlKP.append("<option value = '" + orderDetail.Kod_Projek + "'>" + orderDetail.colKp + "</option>")

            var newId = $('#ddlPTJ')
            var ddlKPtj = $('#ddlPTJ')
            var ddlSearch = $('#ddlPTJ')
            var ddlText = $('#ddlPTJ')
            var selectObj_ddlKPtj = $('#ddlPTJ')
            $(ddlKPtj).dropdown('set selected', orderDetail.Kod_PTJ);
            selectObj_ddlKPtj.append("<option value = '" + orderDetail.Kod_PTJ + "'>" + orderDetail.ButiranPTJ + "</option>")
        }





        function btnAddrowHandler() {

        }

        // Get the current year
        const currentYear = new Date().getFullYear();

        // Calculate the last year (current year - 1)
        const lastYear = currentYear - 1;

        // Get a reference to the select element for years
        const ddlTahun = document.getElementById("ddlTahun");

        // Create option elements for the current year and last year
        const currentYearOption = new Option(currentYear, currentYear);
        const lastYearOption = new Option(lastYear, lastYear);

        // Append the option elements to the select element
        ddlTahun.appendChild(currentYearOption);
        ddlTahun.appendChild(lastYearOption);

        // Get a reference to the select element
        const selectElement = document.getElementById("ddlBulan");

        // Array of month names
        const monthNames = [
            "Januari", "Februari", "March", "April", "Mei", "Jun",
            "Julai", "Ogos", "September", "Oktober", "November", "Desember"
        ];

        // Create option elements for each month and append them to the select element
        monthNames.forEach((month, index) => {
            const option = new Option(month, index + 1);
            selectElement.appendChild(option);
        });

        function openCalendar() {
            var tarikhInput = document.getElementById("tarikh");
            tarikhInput.click(); // Simulate a click on the date input field to open the calendar popup
        }


       

        document.addEventListener("DOMContentLoaded", function () {
            const tabs = document.querySelectorAll('.nav-link');
            const tabContents = document.querySelectorAll('.tab-pane');

            tabs.forEach(function (tab, index) {
                tab.addEventListener('click', function () {
                    //// Hide all tab contents
                    tabContents.forEach(function (content) {
                        content.classList.remove('show', 'active');
                    });

                    $('.nav-link.active').removeClass("active");
                    tab.classList.add("active");

                    // Show the selected tab content
                    tabContents[index].classList.add('show', 'active');
                });
            });
        });


        function SaveSucces() {
            $('#MessageModal').modal('toggle');

        }


        $('.btnPapar').click(async function () {
            tbl.ajax.reload();
        });


        // Populate dropdowns with hours and minutes
        const hoursDropdowns = document.querySelectorAll('[id^="hours-"]');
        const minutesDropdowns = document.querySelectorAll('[id^="minutes-"]');

        for (let i = 0; i <= 12; i++) {
            const option = document.createElement('option');
            option.value = i;
            option.textContent = i < 10 ? `0${i}` : i;

            hoursDropdowns.forEach(dropdown => {
                dropdown.appendChild(option.cloneNode(true));
            });
        }

        for (let i = 0; i < 60; i++) {
            const option = document.createElement('option');
            option.value = i;
            option.textContent = i < 10 ? `0${i}` : i;

            minutesDropdowns.forEach(dropdown => {
                dropdown.appendChild(option.cloneNode(true));
            });
        }     

        

        $(function () {
            $('.btnAddRow-tabK.One').click();
        });

        $('.btnAddRow-tabK').click(async function () {             
           
            var totalClone = $(this).data("val");

            await AddRow_tabK(totalClone);
        });

        async function AddRow_tabK(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblKenyataan');

            if (objOrder !== null && objOrder !== undefined) {
                //totalClone = objOrder.Payload.OrderDetails.length;
                totalClone = objOrder.Payload.length;

            }

            while (counter <= totalClone) {

                curNumObject += 1;
                
                //var newId_coa = "ddlCOA" + curNumObject;
                var newId_checkbox1 = "checkbox1" + curNumObject; //create new object pada table
                var newId_checkbox2 = "checkbox2" + curNumObject;
                var newId_tarikh = "tarikh" + curNumObject;
                var newId_hoursbertolak = "hours-bertolak" + curNumObject;
                var newId_minutesbertolak = "minutes-bertolak" + curNumObject;
                var newId_ampmbertolak = "ampm-bertolak" + curNumObject;
                var newId_hourssampai = "hours-sampai" + curNumObject;     
                var newId_minutessampai = "minutes-sampai" + curNumObject;   
                var newId_ampmsampai = "ampm-sampai" + curNumObject;
                var newId_checkbox3 = "checkbox3" + curNumObject;
                var newId_txtTujuan = "txtTujuan" + curNumObject;
                var newId_ddlJenInv = "ddlJenInv" + curNumObject;
                var newBil = "txtBil" + curNumObject;
                

                var row = $('#tblKenyataan tbody>tr:first').clone();

                // var dropdown5 = $(row).find(".COA-list").attr("id", newId_coa);
                var Checkbox1 = $(row).find(".Checkbox1").attr("id", newId_checkbox1);
                var Checkbox2 = $(row).find(".Checkbox2").attr("id", newId_checkbox2);
                var formcontrol = $(row).find(".form-control").attr("id", newId_tarikh);
                var hoursTolak = $(row).find(".hoursTolak").attr("id", newId_hoursbertolak);
                var minutesTolak = $(row).find(".minutesTolak").attr("id", newId_minutesbertolak);
                var ampmTolak = $(row).find(".ampmTolak").attr("id", newId_ampmbertolak);
                var hoursSampai = $(row).find(".hoursSampai").attr("id", newId_hourssampai);
                var minutesSampai = $(row).find(".minutesSampai").attr("id", newId_minutessampai);
                var ampmSampai = $(row).find(".ampmSampai").attr("id", newId_ampmsampai);
                var Checkbox3 = $(row).find(".Checkbox3").attr("id", newId_checkbox3);
                var formControl = $(row).find(".form-control").attr("id", newId_txtTujuan);
                var formcontroldropdown = $(row).find(".form-control input-sm ui search dropdown").attr("id", newId_ddlJenInv);

                var $objBil = $(row).find("#txtBil");
                $objBil.attr("id", newBil);
                
                //var curBil = 1;
                //$('#tblKenyataan tbody').find(".list_Bil").each(function (ind, obj) {
                //    if (ind === 0) {
                //        return;
                //    }

                //    $(obj).val(curBil);
                //    curBil += 1;
                //})

                row.attr("style", "");
                var val = "";
                $objBil.val($('#tblKenyataan tbody').find(".list_Bil").length);

                $('#tblKenyataan tbody').append(row);

                counter += 1;
            }
        }

         //tuk elaun Perjalanan
        $(function () {
            $('.btnAddRow_tabEP.One').click();
        });

        $('.btnAddRow_tabEP').click(async function () {

            var totalClone = $(this).data("val");

            await AddRow_tabEP(totalClone);
        });

        async function AddRow_tabEP(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblDataEP');

            if (objOrder !== null && objOrder !== undefined) {
                //totalClone = objOrder.Payload.OrderDetails.length;
                totalClone = objOrder.Payload.length;

            }

            while (counter <= totalClone) {

                curNumObject += 1;

               

                //var newId_coa = "ddlCOA" + curNumObject;
                var newId_kiraKilo = "txtKiraKilometer" + curNumObject; //create new object pada table
                var newId_hidKM = "hidkm" + curNumObject;
                var newId_kenderaanEP = "txtKenderaanEP" + curNumObject;
                var newId_jarakEP = "txtJumJarakEP" + curNumObject;
                var newId_kadarEP = "txtKadarEP" + curNumObject;
                var newId_jumlahEP = "txtJumlahEP" + curNumObject;                


                var row = $('#tblDataEP tbody>tr:first').clone();

                // var dropdown5 = $(row).find(".COA-list").attr("id", newId_coa);
                var txtKiraKM = $(row).find(".list-txtKiraKilometer").attr("id", newId_kiraKilo);
                var hidKM = $(row).find(".hidkm-list").attr("id", newId_hidKM);
                var kenderaanEP = $(row).find(".list-kenderaanEP").attr("id", newId_kenderaanEP);
                var jarakEP= $(row).find(".list-txtJumJarakEP").attr("id", newId_jarakEP);
                var kadarEP = $(row).find(".list-txtKadarEP").attr("id", newId_kadarEP);
                var jumlahEP = $(row).find(".list-txtJumlahEP").attr("id", newId_jumlahEP);               

                row.attr("style", "");
                var val = "";
               

                $('#tblDataEP tbody').append(row);

                counter += 1;
            }
        }

        //function AddRoe bagi tbl Pengangkutan Awam
        $(function () {
            $('.btnAddRow-tabPA.One').click();
        });

        $('.btnAddRow-tabPA').click(async function () {

            var totalClone = $(this).data("val");

            await AddRow_tabPA(totalClone);
        });

        async function AddRow_tabPA(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblTambang');

            if (objOrder !== null && objOrder !== undefined) {
                //totalClone = objOrder.Payload.OrderDetails.length;
                totalClone = objOrder.Payload.length;

            }

            while (counter <= totalClone) {

                curNumObject += 1;             

               

                //var newId_coa = "ddlCOA" + curNumObject;
                var newId_JenisTambang = "ddlJenisTambangtblAwam-list" + curNumObject; //create new object pada table
                var newId_lbljenisTambang = "label-jenisTam-list" + curNumObject;
                var newId_hidjenisTambang = "Hid-jenisTam-list" + curNumObject;
                var newId_DengResit = "checkbox_DengResit" + curNumObject;
                var newId_TanpaResit = "checkbox_TanpaResit" + curNumObject;
                var newId_noResit = "noResit" + curNumObject;
                var newId_AmaunTamb = "lblAmaunTambang" + curNumObject;
                


                var row = $('#tblTambang tbody>tr:first').clone();

                var JenisTambangdropdown = $(row).find(".ddlJenisTambangtblAwam-list").attr("id", newId_JenisTambang);              
                var lblJenisTamb = $(row).find(".label-jenisTam-list").attr("id", newId_lbljenisTambang);
                var hidJenisTamb = $(row).find(".Hid-jenisTam-list").attr("id", newId_hidjenisTambang);
                var DengResit = $(row).find(".lblDengResit_list").attr("id", newId_DengResit);
                var TanpaResit = $(row).find(".lblTanpaResit-list").attr("id", newId_TanpaResit);
                var noResit = $(row).find(".lblnoResit-list").attr("id", newId_noResit);
                var AmaunTamb = $(row).find(".lblAmaunTambang").attr("id", newId_AmaunTamb);              

                row.attr("style", "");
                var val = "";

                $('#tblTambang tbody').append(row);

                generateDropdown_list("#" + newId_JenisTambang, "MohonTuntutan_WS.asmx/GetKendAwam")

                counter += 1;
            }
        }



        //function AddRow bagi tbl Elaun Makan
        $(function () {
            $('.btnAddRow-tabElaunMkn.One').click();
        });

        $('.btnAddRow-tabElaunMkn').click(async function () {

            var totalClone = $(this).data("val");

            await AddRow_tabElaunMkn(totalClone);
        });

        async function AddRow_tabElaunMkn(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblElaunMkn');

            if (objOrder !== null && objOrder !== undefined) {
                //totalClone = objOrder.Payload.OrderDetails.length;
                totalClone = objOrder.Payload.length;

            }

            while (counter <= totalClone) {

                curNumObject += 1;

                //var newId_coa = "ddlCOA" + curNumObject;
                var newId_bil = "txtbilEL" + curNumObject; //create new object pada table
                var newId_TugasEL = "ddlJenisTugasElaunMkn" + curNumObject;
                var newId_lblTugas = "label-JenisTugasElaunMkn-list" + curNumObject;
                var newId_hidTugas = "Hid-JenisTugasElaunMkn-list" + curNumObject;
                var newId_pagi = "chckPagi" + curNumObject;
                var newId_tghari = "chckTghari" + curNumObject;
                var newId_petand = "chckPtg" + curNumObject;
                var newId_tempatEL = "ddlTmptElaunMkn" + curNumObject;
                var newId_lblTempatEL = "lblJnstempatElaunMkn" + curNumObject;
                var newId_hidTempatEL = "HidJnstempatElaunMkn" + curNumObject;
                var newId_hargaEL = "txtHargaEL-list" + curNumObject;
                var newId_JumlahEL = "txtJumlahEL" + curNumObject;


                var row = $('#tblElaunMkn tbody>tr:first').clone();

                //var bilEL = $(row).find(".txtbilEL").attr("id", newId_bil);
                var jenisTugasEL = $(row).find(".JenisTugasElaunMkn-list").attr("id", newId_TugasEL);
                var lblTugasEL = $(row).find(".label-JenisTugasElaunMkn-list").attr("id", newId_lblTugas);
                var hidTugasEL = $(row).find(".Hid-JenisTugasElaunMkn-list").attr("id", newId_hidTugas);
                var DengResit = $(row).find(".chckPagi").attr("id", newId_pagi);
                var TanpaResit = $(row).find(".chckTghari").attr("id", newId_tghari);
                var noResit = $(row).find(".chckPtg").attr("id", newId_petand);
                var jenisTempatEL = $(row).find(".JnstempatElaunMkn-list").attr("id", newId_tempatEL);
                var lblTempatEL = $(row).find(".label-JnstempatElaunMkn-list").attr("id", newId_lblTempatEL);
                var hidTempatEL = $(row).find(".Hid-JnstempatElaunMkn-list").attr("id", newId_hidTempatEL);
                var AmaunEL = $(row).find(".txtHargaEL-list").attr("id", newId_hargaEL);
                var JumlahEL = $(row).find(".txtJumlahEL").attr("id", newId_JumlahEL)

                var $objBil = $(row).find(".txtbilEL");
                $objBil.attr("id", newId_bil);

                row.attr("style", "");

                var val = "";

                $objBil.val($('#tblElaunMkn tbody').find(".txtbilEL").length);               

                $('#tblElaunMkn tbody').append(row);                

                counter += 1;

                generateDropdown_list("#" + newId_TugasEL, "MohonTuntutan_WS.asmx/GetTugasElaunMkn")
                generateDropdown_list("#" + newId_tempatEL, "MohonTuntutan_WS.asmx/GetTempatTblElaunMkn") 
               

                //ni tuk code hapus, perlu masukkan code ni untuk recalculate bil
                //var curBil = 1;
                //$('#tblKenyataan tbody').find(".list_Bil").each(function (ind, obj) {
                //    if (ind === 0) {
                //        return;
                //    }

                //    $(obj).val(curBil);
                //    curBil += 1;
                //})

                

                
            }
        }

        //function addrow bagi tab lojing
        $(function () {
            $('.btnAddRow-tabLojing.One').click();
        });

        $('.btnAddRow-tabLojing').click(async function () {

            var totalClone = $(this).data("val");

            await AddRow_tabLojing(totalClone);
        });

        async function AddRow_tabLojing(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblLojing');

            if (objOrder !== null && objOrder !== undefined) {
                //totalClone = objOrder.Payload.OrderDetails.length;
                totalClone = objOrder.Payload.length;

            }

            while (counter <= totalClone) {

                curNumObject += 1;

                //var newId_coa = "ddlCOA" + curNumObject;
                var newId_bilLojing = "txtBiltblLojing" + curNumObject; //create new object pada table
                var newId_jnstugasLojing = "ddlJenisTugastblLojing" + curNumObject;
                var newId_lbljnstugasLojing = "lblJenisTugastblLojing" + curNumObject;
                var newId_hidjnstugasLojing= "HidJenisTugastblLojing" + curNumObject;
                var newId_tempatLojing = "ddlTmpttblLojing" + curNumObject;
                var newId_lbltempatLojing = "lblJnstempattblLojing" + curNumObject;
                var newId_hidtempatLojing = "HidJnstempattblLojing" + curNumObject;
                var newId_resitLojing = "resittblLojing" + curNumObject;
                var newId_lblresitLojing = "lblresittblLojing" + curNumObject;
                var newId_elaunLojing = "elauntblLojing" + curNumObject;
                var newId_lblelaunLojing = "lblelauntblLojing" + curNumObject;
                var newId_hariLojing = "haritblLojing" + curNumObject;
                var newId_lblhariLojing = "lblharitblLojing" + curNumObject;
                var newId_amaunLojing = "amauntblLojing" + curNumObject;
                var newId_lblamaunLojing = "lblamauntblLojing" + curNumObject;


                var row = $('#tblLojing tbody>tr:first').clone();

                // var dropdown5 = $(row).find(".COA-list").attr("id", newId_coa);
                var bilLojing = $(row).find(".input-md-txtBiltblLojing").attr("id", newId_bilLojing).val($(".input-md-txtBiltblLojing").length);
                //var bilLojing = $(row).find(".txtBiltblLojing").attr("id", newId_bilLojing);
                var jnstugasLojing = $(row).find(".ddlJenisTugastblLojingA-list").attr("id", newId_jnstugasLojing);
                var lbljnstugasLojing = $(row).find(".form-control lblJenisTugastblLojing").attr("id", newId_lbljnstugasLojing);
                var hidjnstugasLojing = $(row).find(".HidJenisTugastblLojing").attr("id", newId_hidjnstugasLojing);
                var tempatLojing = $(row).find(".ddlTmpttblLojingA-list").attr("id", newId_tempatLojing);
                var lbltempatLojing = $(row).find(".lblJnstempattblLojing").attr("id", newId_lbltempatLojing);
                var hidtempatLojing = $(row).find(".HidJnstempattblLojing").attr("id", newId_hidtempatLojing);
                var resitLojing = $(row).find(".resittblLojing").attr("id", newId_resitLojing);
                var lblresitLojing = $(row).find(".lblresittblLojing").attr("id", newId_lblresitLojing);
                var elaunLojing = $(row).find(".elauntblLojing").attr("id", newId_elaunLojing);
                var lblelaunLojing = $(row).find(".lblelauntblLojing").attr("id", newId_lblelaunLojing);
                var hariLojing = $(row).find(".haritblLojing").attr("id", newId_hariLojing);
                var lblelaunLojing = $(row).find(".lblharitblLojing").attr("id", newId_lblhariLojing);
                var amaunLojing = $(row).find(".amauntblLojing").attr("id", newId_amaunLojing);
                var lblamaunLojing = $(row).find(".lblamauntblLojing").attr("id", newId_lblamaunLojing);
                

                row.attr("style", "");
                var val = "";
                //$objBil.val($('#tblLojing tbody').find(".txtBiltblLojing").length);        

                $('#tblLojing tbody').append(row);

                generateDropdown_list("#" + newId_jnstugasLojing, "MohonTuntutan_WS.asmx/GetTugasElaunMknLojing")
                generateDropdown_list("#" + newId_tempatLojing, "MohonTuntutan_WS.asmx/GetTempatTblLojing")
                

                counter += 1;
            }
        }


        //function addrow bagi tab SEWA HOTEL
        $(function () {
            $('.btnAddRow-tabHotel.One').click();
        });

        $('.btnAddRow-tabHotel').click(async function () {

            var totalClone = $(this).data("val");

            await AddRow_tabHotel(totalClone);
        });

        async function AddRow_tabHotel(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblSewaHotel');

            if (objOrder !== null && objOrder !== undefined) {
                //totalClone = objOrder.Payload.OrderDetails.length;
                totalClone = objOrder.Payload.length;

            }

            while (counter <= totalClone) {

                curNumObject += 1;

                //var newId_coa = "ddlCOA" + curNumObject;
                var newId_bilHotel = "txtBiltblHotel" + curNumObject; //create new object pada table
                var newId_jnstugasHotel = "ddlJenisTugastblHotel" + curNumObject;
                var newId_lbljnstugasHotel = "lblJenisTugastblHotel" + curNumObject;
                var newId_hidjnstugasHotel = "HidJenisTugastblHotel" + curNumObject;
                var newId_tempatHotel = "ddlTmpttblHotel" + curNumObject;               
                var newId_lbltempatHotel = "lblJnstempattblHotel" + curNumObject;
                var newId_hidtempatHotel = "HidJnstempattblHotel" + curNumObject;
                var newId_resitHotel = "resittblHotel" + curNumObject;
                var newId_lblresitHotel = "lblresittblHotel" + curNumObject;
                var newId_elaunHotel = "elauntblHotel" + curNumObject;
                var newId_lblelaunHotel = "lblelauntblHotel" + curNumObject;
                var newId_hariHotel = "haritblHotel" + curNumObject;
                var newId_lblhariHotel = "lblharitblHotel" + curNumObject;
                var newId_amaunHotel = "amauntblHotel" + curNumObject;
                var newId_lblamaunHotel = "lblamauntblHotel" + curNumObject;
               


                var row = $('#tblSewaHotel tbody>tr:first').clone();               

                // var dropdown5 = $(row).find(".COA-list").attr("id", newId_coa);
                var bilHotel = $(row).find(".input-md-txtBiltblHotel-list").attr("id", newId_bilHotel).val($(".input-md-txtBiltblHotel-list").length);
                var jnstugasHotel = $(row).find(".ddlJenisTugastblHotel-list").attr("id", newId_jnstugasHotel);
                var lbljnstugasHotel = $(row).find(".label-JenisTugastblHotel-list").attr("id", newId_lbljnstugasHotel);
                var hidjnstugasHotel = $(row).find(".Hid-JenisTugastblHotel-list").attr("id", newId_hidjnstugasHotel);
                var tempatHotel = $(row).find(".ddltempattblHotel-list").attr("id", newId_tempatHotel);
                var lbltempatHotel = $(row).find(".lblJnstempattblHotel").attr("id", newId_lbltempatHotel);
                var hidtempatHotel = $(row).find(".Hid-JnstempattblHotel-list").attr("id", newId_hidtempatHotel);
                var resitHotel = $(row).find(".resittblHotel-list").attr("id", newId_resitHotel);
                var lblresitHotel = $(row).find(".lblresittblHotel-list").attr("id", newId_lblresitHotel);
                var elaunHotel = $(row).find(".elauntblHotel-list").attr("id", newId_elaunHotel);
                var lblelaunHotel = $(row).find(".lblelauntblHotel-list").attr("id", newId_lblelaunHotel);
                var hariHotel = $(row).find(".haritblHotel-list").attr("id", newId_hariHotel);
                var lblelaunHotel = $(row).find(".lblharitblHotel-list").attr("id", newId_lblhariHotel);
                var amaunHotel = $(row).find(".input-md amauntblHotel-list").attr("id", newId_amaunHotel);
                var lblamaunHotel = $(row).find(".lblamauntblHotel-list").attr("id", newId_lblamaunHotel);


                row.attr("style", "");
                var val = "";

                $('#tblSewaHotel tbody').append(row);

                generateDropdown_list("#" + newId_jnstugasHotel, "MohonTuntutan_WS.asmx/GetJenisTugasTblSewaHotel")
                generateDropdown_list("#" + newId_tempatHotel, "MohonTuntutan_WS.asmx/GetTempatTblHotel")

                

                counter += 1;
            }
        }


      
        //function addrow bagi tuk tab pelbagai
        $(function () {
            $('.btnAddRow-tabPelbagai.One').click();
        });

        $('.btnAddRow-tabPelbagai').click(async function () {

            var totalClone = $(this).data("val");

            await AddRow_tabPelbagai(totalClone);
        });

        async function AddRow_tabPelbagai(totalClone, objOrder) {
            var counter = 1;
            var table = $('#tblPelbagai');

            if (objOrder !== null && objOrder !== undefined) {
                //totalClone = objOrder.Payload.OrderDetails.length;
                totalClone = objOrder.Payload.length;

            }

            while (counter <= totalClone) {

                curNumObject += 1;

               

                //var newId_coa = "ddlCOA" + curNumObject;
                var newId_jenisBP = "ddlJenisPelbagai" + curNumObject;
                var newId_lbljenisBP = "lblJenisPelbagai" + curNumObject;
                var newId_hidjenisBP = "HidlblJenisPelbagai" + curNumObject;
                var newId_DengResitBP = "checkbox_DengResitBP" + curNumObject;
                var newId_lblDengResitBP = "lblDengResitBP" + curNumObject;
                var newId_TanpaResitBP = "checkbox_TanpaResitBP" + curNumObject;
                var newId_lblTanpaResitBP = "lblTanpaResitBP" + curNumObject;
                var newId_noResitBP = "noResitBP" + curNumObject;
                var newId_lblnoResitBP = "lblnoResitBP" + curNumObject;
                var newId_AmaunBP = "AmaunBP" + curNumObject;
                var newId_lblAmaunBP = "lblAmaunBP" + curNumObject;          

                var row = $('#tblPelbagai tbody>tr:first').clone();

                
                var JenisPelbagaiBP = $(row).find(".JenisPelbagai-list").attr("id", newId_jenisBP);
                var lbljnsPelbagaiBP = $(row).find(".label-jenisPelbagai-list").attr("id", newId_lbljenisBP);
                var hidjnsPelbagaiBP = $(row).find(".Hid-jenisPelbagai-list").attr("id", newId_hidjenisBP);
                var checkbox_DengResitBP = $(row).find(".checkbox_DengResitBP-list").attr("id", newId_DengResitBP);
                var lblDengResitBP = $(row).find(".lblDengResitBP-list").attr("id", newId_lblDengResitBP);
                var checkbox_TanpaResitBP = $(row).find(".checkbox_TanpaResitBP-list").attr("id", newId_TanpaResitBP);
                var lblTanpaResitBP = $(row).find(".lblTanpaResitBP-list").attr("id", newId_lblTanpaResitBP);
                var noResitBP = $(row).find(".input-md-noResitBP").attr("id", newId_noResitBP);
                var lblnoResitBP = $(row).find(".lblnoResitBP-list").attr("id", newId_lblnoResitBP);
                var amaunBP = $(row).find(".input-md-AmaunBP").attr("id", newId_AmaunBP);
                var lblAmaunBP = $(row).find(".lblAmaunBP-list").attr("id", newId_lblAmaunBP);
               


                row.attr("style", "");
                var val = "";

                $('#tblPelbagai tbody').append(row);

                generateDropdown_list("#" + newId_jenisBP, "MohonTuntutan_WS.asmx/GetJenisPelbagai")
               
                
               
                counter += 1;
            }
        }

        function getDataPeribadi() {
            //Cara Pertama
            console.log("load info pemohon 2587")
            var nostaf = $('#ddlStaf').val()
          
            if (nostaf === null) {

                nostaf = '<%=Session("ssusrID")%>'

                         }

                         else {

                             nostaf = $('#ddlStaf').val();
                         }

                                fetch('MohonTuntutan_WS.asmx/GetUserInfo', {
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

            $('#txtNamaP').val(data[0].Param1);
            $('#txtNoPekerja').val(data[0].StafNo);
            $('#txtJawatan').val(data[0].Param3);
            $('#txtGredGaji').val(data[0].Param6);
            $('#txtPejabat').val(data[0].Param5);
            $('#txtKump').val(data[0].Param4);
            $('#txtTel').val(data[0].Param7);
            $('#hidPtjPemohon').val(data[0].Param2)
            $('#txtNama').val(data[0].Param1);
            $('#txtNoStaf').val(data[0].StafNo);            
            $('#tblListPend').DataTable().ajax.reload();
            <%--hadGaji = data[0].GredGaji;
            console.log($('#hidPtjPemohon').val());
             //$('#<%'=txtMemangku.ClientID%>').val(data[0].Param3);--%>
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


                        // Refresh dropdown
                        $(obj).dropdown('refresh');

                        $(obj).dropdown('show');
                    }
                }
            });
        }

        $(document).ready(function () {

            ////generateDropdown("ddlTugas", "Pendahuluan_WS.asmx/GetJenisTugas", null, function () {
            ////    kiraElaunMakan();
            ////})
            //generateDropdown("ddlTugas", "Pendahuluan_WS.asmx/GetJenisTugas", null, kiraElaunMakan)
            generateDropdown("ddlProjek", "MohonTuntutan_WS.asmx/GetJenisProjek")
            generateDropdown("ddlPTJ", "MohonTuntutan_WS.asmx/GetKodPtj")
            generateDropdown("ddlOperasi", "MohonTuntutan_WS.asmx/GetJenisOperasi")
            generateDropdown("ddlKumWang", "MohonTuntutan_WS.asmx/GetJenisKumpWang")
            //generateDropdown("ddlJenisTambang", "MohonTuntutan_WS.asmx/GetKendAwam")
            //generateDropdown("ddlJenisTugastblHotel", "MohonTuntutan_WS.asmx/GetJenisTugasTblSewaHotel")
            //generateDropdown(".ddltempattblHotel-list", "MohonTuntutan_WS.asmx/GetTempatTblHotel")
            //generateDropdown("ddlJenisTugasElaunMkn", "MohonTuntutan_WS.asmx/GetTugasElaunMkn")
            //generateDropdown("ddlTmptElaunMkn", "MohonTuntutan_WS.asmx/GetTempatTblElaunMkn")
            //initDropdownCOA("ddlCOA");
           

        });

      

        function generateDropdown_list(id, url, param, fn) {
            console.log("mASUK 2418")
            var inParam = "";

            if (param !== null && param !== undefined) {
                inParam = param;
            }
            $(id).dropdown({
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


                        // Refresh dropdown
                        $(obj).dropdown('refresh');

                        $(obj).dropdown('show');
                    }
                }
            });
        }

        function getDataPeribadiPemohon(pemohon) {
            //Cara Pertama
            console.log("getDataPeribadiPemoho-2764")
            var nostaf = pemohon
            //alert(pemohon)

            fetch('MohonTuntutan_WS.asmx/GetUserInfo', {
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

          // console.log('<%=Session("ssusrID")%>')

        //tuk radioButton
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


        //event bila klik button simpan  btnSaveKenyataan
        $('.btnSimpanInfo').click(async function () {
            var jumRecord = 0;
            var acceptedRecord = 0;
            console.log("2836")
            console.log($('#selectedNoPendahuluan').val())
            console.log($('#selectedJumlah').val())

            var pemohon = $('#txtNoStaf').val()
            var statusPemohon

            if ('<%=Session("ssusrID")%>' !== pemohon) {
                statusPemohon = "0"  //mohon tuk org lain
            } else {
                statusPemohon = "N"  //mohon tuk sendiri
            }

            var msg = "";
            var newTuntutanDN = {
                listClaim: {
                    OrderID: $('#noPermohonan').val(),
                    StafID: pemohon,  //nama org yg login
                    Tahun: $('#ddlTahun').val(),
                    Bulan: $('#ddlBulan').val(),
                    KumpWang: $('#ddlKumpWang').val(),
                    KodOperasi: $('#ddlOperasi').val(),
                    KodPtj: $('#ddlPTJ').val(),
                    KodProjek: $('#ddlProjek').val(),
                    staPemohon: statusPemohon,
                    NoPemohon: $('#txtNoPekerja').val(),   //nama pemohon 
                    sebabLewat: $('#txtsebab').val(),
                    noPendahuluan: $('#selectedNoPendahuluan').val(),
                    jumlahBaucer: $('#selectedJumlah').val(),
                    hidPtjPemohon: $('#hidPtjPemohon').val(), 
                    TkhMohon: $('#tkhMohonCL').val(),

                }

            }

            console.log(newTuntutanDN);
            console.log(newTuntutanDN.listClaim.OrderID);

            //1`ShowPopup("msg")
            msg = "Anda pasti ingin menyimpan rekod ini?"

            if (!confirm(msg)) {
                return false;
            }

            var result = JSON.parse(await ajaxSaveRecord(newTuntutanDN));
            alert(result.Message)
            //$('#orderid').val(result.Payload.OrderID)

            await clearAllRowsHdr();
        });

        async function ajaxSaveRecord(tuntutan) {

            return new Promise((resolve, reject) => {
                $.ajax({
                    url: 'MohonTuntutan_WS.asmx/SaveRecordTuntutan',
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

        async function clearAllRowsHdr() {

            $('#noPermohonan').val("");
            $('#ddlTahun').val("");
            $('#ddlBulan').val("");
            $('#ddlKumpWang').val("");
            $('#ddlOperasi').val("");
            $('#ddlPTJ').val("");
            $('#ddlProjek').val("");

        }
      

        //function bila klil pada Tab-Kenyataan
        $('#tab-Kenyataan').click(async function () {
            console.log("masuk tab_Kenyataan")
            $('#noPermohonan').val();
            var id = $('#noPermohonan').val();
            var tkhMohon1 = $('#tkhMohonCL').val();
            $('#txtMohonID').val(id); 
            $('#tkhMohon2').val(tkhMohon1);

           
        });

        //event bila klik button simpan  btnSaveKenyataan
        $('#btnSaveKenyataan').click(async function () {
            var item
            id = $('#noPermohonan').val();

            var item = {
                keperluan: {
                    mohonID: $('#noPermohonan').val(),
                    Jumlah: $('#totalKt').val(),
                    GroupItem: []
                }

            }

            $('#tableID_KenyataanTuntutan tr').each(function (index, obj) {
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

       

        //$('#Permohonan').click(async function () {
        //    // $('#noPermohonan').val(orderDetail.No_Tuntutan) 
        //    console.log("2887")
        //    $('#tab-Kenyataan').disable();
        //    $('#tab-elaunPjln').disable();
        //    $('#tab-pengangkutan').disable();
        //    $('#tab-elaunMakan').disable();
        //    $('#tab-sewaHotel').disable();
        //    $('#tab-pelbagai').disable();
        //    $('#tab-pengesahan').disable();

        //});

        


        
  
    </script>
</asp:Content>
