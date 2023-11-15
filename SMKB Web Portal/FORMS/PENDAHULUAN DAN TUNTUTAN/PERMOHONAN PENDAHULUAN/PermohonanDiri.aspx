<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PermohonanDiri.aspx.vb" Inherits="SMKB_Web_Portal.PermohonanDiri" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="Server">
  

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
    </style>
     <div id="PermohonanTab" class="tabcontent" style="display: block">
      


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
            </div>

            </div>
        </div>

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
                          <label class="input-group__label" for="NoStaf">NoStaf</label>                  
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

        <asp:Panel ID="pnlJenisPermohonan" runat="server">                         
                    <br />
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
        </asp:Panel>


         <!-- Trigger the modal with a button -->
         <div class="modal-body">
            <div class="table-title">
                
                <button type="button" class="btn-change" data-toggle="modal" data-target="#myPersonal">Maklumat Pegawai</button>                 
                <div class="btn btn-primary btnPapar" onclick="ShowPopup('2')">
                    Senarai Permohonan
                </div>
            </div>

<!-- Trigger the modal with a button -->
<%--dulu place for button info--%>
          <div class="panel panel-default">
              <div class="panel-heading"></div>
              <div class="panel-body"> 
                  <br />
                   <div class="col-md-6"> 
                   <div class="form-group row">                        
                        <div class="col-md-5">
                          <input type="text" class="input-group__input form-control input-md" id="txtHadMin"  placeholder="&nbsp;" readonly style="background-color: #f0f0f0;text-align:right">
                            <label class="input-group__label" for="inputHadMin">Had Minimum Pendahuluan Anda RM </label>                    
                        </div>
                    </div>  
                </div>
                      

        <div class="col-md-12"> 
            <div class="form-row">
                <div class="form-group col-md-3" style="left: -1px; top: 0px">                    
                    <input type="text" id="noPermohonan"  class="input-group__input form-control input-md" style="background-color: #f0f0f0"  readonly>
                    <label class="input-group__label" for="No.Permohonan" >No.Permohonan</label>
                </div>
                <div class="form-group col-md-3">
                     <select name="ddlJnsJalan" id="ddlJnsJalan"  class="input-group__select ui search dropdown jnsjalan-list" placeholder="&nbsp;"></select>
                    <label class="input-group__label" for="Jenis Perjalanan">Jenis Perjalanan</label>                   
                <%--<asp:DropDownList ID="ddlJnsTugas"  AutoPostBack="false" runat="server" CssClass="form-control" ></asp:DropDownList> --%>
                </div>
                <div class="form-group col-md-3">
                     <select name="ddlTugas" id="ddlTugas"  class="input-group__select ui search dropdown tugas-list" placeholder="&nbsp;"></select> 
                    <label class="input-group__label" for="Jenis Tugas">Jenis Tugas</label> <br />
                                                                          
                </div>
                <div class="form-group col-md-3">
                    <input type="date"  id="tkhMohon" class="input-group__input form-control input-sm" style="background-color:#f3f3f3" placeholder="&nbsp;" />  
                    <label class="input-group__label" for="Tarikh Mohon" >Tarikh Mohon:</label>                                      
                </div>                
            </div>
      
          <div class="form-row">

             &nbsp;&nbsp;&nbsp;<h6>Transaksi</h6>
            <div class="col-md-12">
                <div class="">
                    <table  id="tblData"  class="table table-striped"><%-- class="table table-bordered"--%>
                        <thead>
                            <tr>
                                <th scope="col"  style="text-align: left;width: 30%;">Kumpulan Wang</th>
                                <th scope="col"  style="text-align: left;width: 25%;">Kod Operasi</th>
                                <th scope="col"  style="text-align: left;width: 15%;">Kod Ptj</th>
                                <th scope="col"  style="text-align: left;width: 15%;">Kod Projek</th>
                                <th scope="col"  style="text-align: left;width: 15%;">Kod Vot</th>
                            </tr>
                        </thead>
                        <tbody id="tableID" class="table table-striped">
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
            </div>              
   

        </div>
                     
                                          
                             <div class="col-md-12">                   
                                
                                <div class="form-row">
                                    <div class="form-group col-md-3">
                                        <input type="date" id="tkhMula" class="input-group__input form-control tkhMula"  placeholder="&nbsp;" onchange="updatedate();"> 
                                        <label class="input-group__label" for="Tarikh Mula">Tarikh Mula</label>                                      
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input type="date" id="tkhTamat" class="input-group__input form-control tkhTamat"  placeholder="&nbsp;">
                                         <label class="input-group__label" for="Tarikh Tamat">Tarikh Tamat</label> <br />                                    
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input type="text" name="Tempoh" id="txtTempoh" class="input-group__input"  readonly style="background-color: #f0f0f0">
                                        <label class="input-group__label" for="Tempoh">Tempoh</label>                                       
                                    </div> 
                                     <div class="form-group col-md-3">                                             
                                             <%--<textarea rows="2" cols="45" ID="txtTempat" class="form-control" MaxLength="100"></textarea>--%>
                                              <select name="ddlTempat" id="ddlTempat"  class="input-group__select ui search dropdown tempatlist" placeholder="&nbsp;"></select>
                                              <label class="input-group__label"  for="Tempat">Tempat</label>  
                                           <%--<input type ="text" id="txtTempat"  class="form-control" maxlength="100" />  --%>                                    
                                    </div> 
                                </div>   
                              </div> 

                            <div class="col-sm-12"> 
                                <div class="form-row">  
                                    <div class="form-group col-md-3">                                         
                                <textarea rows="2" cols="45" ID="txtLokasi" name="Lokasi Perjalanan"  class="input-group__input form-control" placeholder="&nbsp;" MaxLength="100"></textarea>
                                    <label class="input-group__label"  for="Lokasi Perjalanan">Lokasi Perjalanan</label> 
                                    <%--<input type="text" ID="txtLokasi"  class="form-control" MaxLength="100"/>--%>
                            </div> 
                                <div class="form-group col-md-3">                                         
                                    <textarea rows="2" cols="45" ID="txtTujuan" name="Tujuan Perjalanan" class="input-group__input form-control" placeholder="&nbsp;" MaxLength="500"></textarea>
                                    <label class="input-group__label" for="Tujuan Perjalanan">Tujuan Perjalanan</label>  
                                    <%--  <input type="text" ID="txtTujuan"  Width="100%" class="form-control" MaxLength="500" multiple="multiple" />--%>
                            </div>                                    
                            <div class="form-group col-md-3">
                                    <select  name="ddlPenginapan" id="ddlPenginapan" class="input-group__select ui search dropdown penginapan-list" placeholder="&nbsp;"></select>
                                <label class="input-group__label" for="Penginapan">Penginapan</label>  
                            </div>
                                <div class="form-group col-md-3 form-inline">                                       
                                    <input type="checkbox" name="checkfield" id="chkDN" value="false" />&nbsp;&nbsp;
                                    <label  for="Makanan Disediakan">Makanan Disediakan</label> 
                                </div>                                      
                            </div> 
                        </div>
                            
                        <div class="col-md-12"> 
                            <div class="form-row">
                                 <div class="form-group col-md-3">                          
                                    <input type="date" id="tkh_Adv" name="Tarikh Pendahuluan" class="input-group__input form-control" placeholder="&nbsp;" >
                                       <label class="input-group__label"  for="Tarikh Pendahuluan Dikehendaki">Tarikh Pendahuluan Dikehendaki</label> 
                                 </div>   
                                <div class="form-group col-md-3">    
                                        <select  name="ddlBayar" id="ddlBayar" class="input-group__select ui search dropdown bayar-list" placeholder="&nbsp;"></select>
                                       <label  class="input-group__label" for="Kaedah Pembayaran">Kaedah Pembayaran</label>                                   
                               </div>                                
                                <div class="form-group col-md-6"> 
                                    <label  for="UploadSurat">Upload surat</label><br />
                                        <div class="form-inline">
                                        <input type="file" id="fileInput" />                                            
                                        <input type="button" id="uploadButton" class="btn btn-primary" value="Upload" onclick="uploadFile()" />                                              
                                            <span id="uploadedFileNameLabel" style="display: inline;"></span>
                                            <span id="">&nbsp</span>
                                            <span id="progressContainer"></span>
                                            <lable style="color: #FF3300; font-family:Helvetica Neue,Arial,Noto Sans,Liberation Sans;font-size:12px">*Upload file format .pdf</lable>
                                            <input type ="hidden" id="txtNamaFile" />
                                            <input type="hidden" class="form-control"  id="hidJenDok" style="width:300px" readonly="readonly" /> 
                                            <input type="hidden" class="form-control"  id="hidFileName" style="width:300px" readonly="readonly" /> 
                                    </div>     
                                </div>
                             </div> 
                            </div>
 <%-- </div>--%>
                         <br />
                               <div class="form-row">
                                   &nbsp;&nbsp;&nbsp;<h6>Jumlah Pendahuluan</h6> 
                                   &nbsp;&nbsp;&nbsp; <h6 style="color: #FF3300; font-size:12px">(Kiraan berdasarkan Jenis Perjalanan, Jenis Tugas, Tempat, Penginapan, Tarikh Mula dan Tarikh Tamat)</h6>
                                  <div class="col-md-12"> 
                                        <div class="">
                                         <!-- Table -->
                                      <table class="table">                                          
                                        <tr>                                            
                                            <td style="height: 52px">Elaun Makan :</td>                                             
                                            <td style="width: 396px">
                                                 <div class="form-group col-md-10">
                                                    <input class="input-group__input form-control input-sm" ID="hargaEMakan" type="text"  style="text-align:right; font-weight: bold;" readonly />
                                                    <label class="input-group__label" for="RM">RM</label>
                                                  </div> 
                                            </td>
                                            <td> X </td>
                                            <td>   
                                                <div class="form-group col-md-10">
                                                 <input type="text" ID="bilHariMakan" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;"  readonly />
                                                 <label class="input-group__label" for="Hari">Hari</label>  
                                                </div>
                                            </td>
                                            <td> = </td>
                                            <td>
                                                <div class="form-group col-md-10">
                                                 <input type="text" ID="totalMakan" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;"  readonly />
                                                 <label class="input-group__label" for="RM">RM</label>  
                                                </div>                                                
                                             </td>                                           
                                        </tr>
                                        <tr>
                                            <td style="height: 52px">Elaun Lojing/Hotel :</td>
                                            <td style="width: 396px">
                                                 <div class="form-group col-md-10">
                                                    <input type="text" ID="hargaLojing" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;" readonly/>
                                                     <label class="input-group__label" for="RM">RM</label> 
                                                 </div>
                                            </td>
                                            <td style="height: 52px"> X </td>
                                            <td style="height: 52px">
                                                <div class="form-group col-md-10">
                                                    <input type="text" ID="bilHariHotel" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;"  readonly />
                                                    <label class="input-group__label" for="RM">Hari</label> 
                                                </div> 
                                            </td>
                                            <td style="height:52px"> = </td>
                                            <td>
                                                 <div class="form-group col-md-10">
                                                    <input type="text" ID="totalHotel" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;" readonly />
                                                    <label class="input-group__label" for=" RM"> RM</label> 
                                                 </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Jumlah :</td>
                                            <td style="width: 396px">
                                                <div class="form-group col-md-10">
                                                    <input type="text" ID="jumSemua" class="input-group__input form-control input-sm" style="text-align:right; font-weight: bold;" readonly>
                                                     <label class="input-group__label" for=" RM"> RM</label> 
                                                </div>
                                            </td>
                                       </tr>
                                       <tr>
                                           <td colspan="6"><h6 style="color: #FF3300">Nota : Pendahuluan hanya dikira berdasarkan tempoh berkursus / bertugas rasmi SAHAJA. Tidak termasuk hari perjalanan.</h6></td>
                                       </tr>
                                  </table>  
                              </div>
                           </div>
                     </div>
                                       <%-- </div> --%>
</div>        
                         </div>
                            <%-- </div>--%>
                            
          
           
    </div>
 
    <div class="form-row">
        <div class="form-group col-md-12" align="right">
           <%-- <button type="button" class="btn btn-danger">Padam</button>--%>
            <button type="button" class="btn btn-secondary btnSimpan">Simpan</button>
        </div>
    </div>
 </div>

</div>

    

         <script type="text/javascript">

             var tableSenaraiPTj = null;
             var hadGaji 
             var checkMakan
             var isClicked = false;
             const dateInput = document.getElementById('tkhMohon');
             document.getElementById("tkhMohon").disabled = true;

             // ✅ Using the visitor's timezone
             dateInput.value = formatDate();

             console.log(formatDate());
             console.log(dateInput);

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

             function updatedate() {                
                 var firstdate = $('#tkhMula').val();
                 $('#tkhTamat').value = "";
                 document.getElementById("tkhTamat").setAttribute("min", firstdate);
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


             //function tuk kira tempoh
             function calculateDifference() {
                 // Get both values from input field and convert them into Javascript Date object
                 var start = new Date($('#tkhMula').val());
                 var end = new Date($('#tkhTamat').val());

                 // end - start returns difference in milliseconds 
                 var diff = end - start;
              

                 // get days
                 var days = diff / 1000 / 60 / 60 / 24;
                 console.log(days)
                 $('#txtTempoh').val(days);
                 $('#bilHariMakan').val(days); 
                 $('#bilHariHotel').val(days); 
                 
                 
             }

             

             var tbl = null
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
                         "url": "Pendahuluan_WS.asmx/LoadOrderRecord_PermohonanSendiri",
                         type: 'POST',
                         "contentType": "application/json; charset=utf-8",
                         "dataType": "json",
                         "dataSrc": function (json) {
                             //var data = JSON.parse(json.d);
                             //console.log(data.Payload);
                             return JSON.parse(json.d);
                         },
                         //data: function (d) {
                         //    return JSON.stringify({ id: $('#txtNoPekerja').val() })
                         //},
                         
                         data: function () {
                             //Filter date bermula dari sini - 20 julai 2023
                             var startDate = $('#txtTarikhStart').val()
                             var endDate = $('#txtTarikhEnd').val()
                             return JSON.stringify({
                                 category_filter: $('#categoryFilter').val(),
                                 isClicked: isClicked,
                                 tkhMula: startDate,
                                 tkhTamat: endDate,
                                 staffP: $('#txtNoPekerja').val()
                             })
                             //akhir sini
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
                         { "data": "Tarikh_MohonDisplay" },
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
                     ]
                 });
             });


             $('.btnPapar').click(async function () {
                 tbl.ajax.reload();
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
                         //if (fileExtension === 'pdf' || fileExtension === 'xlsx' || fileExtension === 'xls') {
                         if (fileExtension === 'pdf') { 
                             var reader = new FileReader();
                             reader.onload = function (e) {



                                 var fileData = e.target.result; // Base64 string representation of the file data
                                 var fileName = file.name;

                                 var requestData = {
                                     fileData: "test",
                                     fileName: fileName,
                                     resolvedUrl: resolveAppUrl("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PD/")
                                 };

                                 var frmData = new FormData();

                                 frmData.append("fileSurat", $('input[id="fileInput"]').get(0).files[0]);
                                 frmData.append("fileName", fileName);
                                 frmData.append("fileSize", fileSize);

                                 $('#hidJenDok').val(fileExtension);
                                 $('#hidFileName').val(fileName);

                                  
                                 $.ajax({
                                     url: "Pendahuluan_WS.asmx/UploadFile",
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

                                         var uploadedFileNameLabel = document.getElementById("uploadedFileNameLabel");
                                         uploadedFileNameLabel.appendChild(fileLink);


                                         $("#uploadedFileNameLabel").show();
                                         // Clear the file input
                                         $("#fileInput").val("");

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
                     url: "Pendahuluan_ws.asmx/GetBaseUrl",
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


             function getDataPeribadi() {
                 //Cara Pertama
               
                 var nostaf = $('#ddlStaf').val()
                
                 if (nostaf === null) {
                    
                     nostaf = '<%=Session("ssusrID")%>'
                     
                 }

                 else {

                     nostaf = $('#ddlStaf').val();
                 }
                 
             
                 fetch('Pendahuluan_WS.asmx/GetUserInfo', {
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
                 hadGaji = data[0].GredGaji; 
                 console.log($('#hidPtjPemohon').val());
                 //$('#<%'=txtMemangku.ClientID%>').val(data[0].Param3);
                 getHadMinPendahuluan();
             }

            
             function getHadMinPendahuluan() {
                
                 //Cara Pertama          
                   fetch('Pendahuluan_WS.asmx/GetHadMin', {
                     method: 'POST',
                     headers: {
                         'Content-Type': "application/json"
                     },
                     //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
                       body: JSON.stringify({ hadMin: hadGaji  })
                 })
                 .then(response => response.json())
                  .then(data => setDataHadMinimum(data.d))

             }
             //$('.numbers').keyup(function () {
             //    this.value = this.value.replace(/[^0-9\.]/g, '');
             //});

             function setDataHadMinimum(data) {                 
                 data = JSON.parse(data);
                 if (data.Nostaf === "") {
                     alert("Tiada data");
                     return false;
                 }
                
                         
               //$('#txtHadMin').val(data[0].param6);
                 $('#txtHadMin').val(parseFloat(data[0].param6).toFixed(2)) 
               
                 //console.log($('#txtHadMin').val(data[0].param6));
                 
             }

             var searchQuery = "";
             var oldSearchQuery = "";
             var curNumObject = 0;
             var tableID = "#tblData";
             var tableID_Senarai = "#tblDataSenarai";
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

             async function initDropdownCOA(id) {

                 $('#' + id).dropdown({
                     fullTextSearch: true,
                     onChange: function (value, text, $selectedItem) {

                         console.log($selectedItem);

                         var curTR = $(this).closest("tr");

                         var recordIDkwHd = curTR.find("td > .Hid-kw-list");
                         recordIDkwHd.html($($selectedItem).data("coltambah6"));

                         //var recordID_ = curTR.find("td > .label-kw-list");
                         //recordID_.html($($selectedItem).data("coltambah2"));

                        
                         //recordIDVotHd.html($($selectedItem).data("coltambah5"));

                         //var selectObj = $($selectedItem).find("td > .COA-list > select");
                         //selectObj.val($($selectedItem).data("coltambah5"));



                         var recordIDPtj = curTR.find("td > .label-ptj-list");
                         recordIDPtj.html($($selectedItem).data("coltambah1"));

                         var recordIDPtjHd = curTR.find("td > .Hid-ptj-list");
                         recordIDPtjHd.html($($selectedItem).data("coltambah5"));

                         console.log(recordIDPtjHd.html());

                         var recordIDVotHd = curTR.find("td > .Hid-vot-list");
                         recordIDVotHd.html("74101");  

                         var recordID_Vot = curTR.find("td > .label-vot-list");
                         recordID_Vot.html("74101");

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
                         url: 'Pendahuluan_WS.asmx/GetVotCOA?q={query}',
                         method: 'POST',
                         dataType: "json",
                         contentType: 'application/json; charset=utf-8',
                         cache: false,
                         fields: {

                             value: "value",      // specify which column is for data- amik data vot
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

             //function kiraElaunMakan() {
                

             //    $('#hargaEMakan').val("RM100")
             //}

             function kiraElaunMakan() {
            
                 var jtugas = $('#ddlTugas').val()
                 var jtempat = $('#ddlTempat').dropdown("get value")
                 var jjalan = $('#ddlJnsJalan').val()
                 var jpginap = $('#ddlPenginapan').val()

                 if (jtugas === "null" || jtempat === "null" || jjalan === "null" || jpginap === "null") 
                  {
                     alert("Sila Pilih Jenis Tugas, Jenis Tempat, Jenis Perjalanan atau Jenis Penginapan")
                     return false;
                 }

                 var param = {
                     "jtugas": jtugas,
                     "jtempat": jtempat,
                     "jjalan": jjalan,
                     "jpginap": jpginap
                 };
                
                 fetch('Pendahuluan_WS.asmx/GetKiraAdv', {
                     method: 'POST',
                     headers: {
                         'Content-Type': "application/json; charset=utf-8'"
                     },
                     //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
                     body: JSON.stringify(param)
                 })
                     .then(response => response.json())
                     .then(data => setDataKiraAdv(data.d))

             };

             function setDataKiraAdv(data) {
                 data = JSON.parse(data);
                 if (data.JenisTugas === "") {
                     alert("Tiada data");
                     return false;
                 }
                
                 var jnsPenginap = $('#ddlPenginapan').val()                

                 if (jnsPenginap === "L") {  //perlu cari jenis penginapan sebab rate harga dia berbeza
                     $('#hargaLojing').val(data[0].KadarLojing.toFixed(2));
                 }
                 else {
                     $('#hargaLojing').val(data[0].KadarHotel.toFixed(2));
                 }

                 var totalMakan
                 var totalLojing
                 var totalPenuh
                 var semakStaMakan
                 semakStaMakan = $("#chkDN").val(); 
                 //console.log("semakStaMakan");
                 //console.log(semakStaMakan);
                 //console.log("932");

                 if ($('#chkDN').prop('checked')) {
                 //if ($('#chkDN').prop('checked') === "true") {                    
                     $('#hargaEMakan').val("0.00");
                     totalMakan = 0.00;
                 }
                 else {
                     //alert("masuk else 934");
                     //console.log("masuk tidak check")               
                     $('#hargaEMakan').val(data[0].KadarMkn.toFixed(2));
                     totalMakan = parseFloat(data[0].KadarMkn) * parseFloat($('#bilHariMakan').val())
                 }

                 totalLojing = parseFloat($('#hargaLojing').val()) * parseFloat($('#bilHariHotel').val())
                 totalPenuh = parseFloat(totalMakan) + parseFloat(totalLojing)
                 
                 if (isNaN(totalMakan) === false) {    //ni tuk cari total makan                                     
                     $('#totalMakan').val(parseFloat(totalMakan).toFixed(2))
                 }
                 else {                     
                     $('#totalMakan').val(parseFloat(totalMakan).toFixed(2)) 
                 }


                 if (isNaN(totalLojing) === false) { //ni tuk cari total lojing atau hotel                     
                     $('#totalHotel').val(parseFloat(totalLojing).toFixed(2))
                 }
                 else {                    
                     $('#totalHotel').val(parseFloat(totalLojing).toFixed(2))
                 }

                 if (isNaN(totalPenuh) === false) {  //ni tuk cari total keseluruhan                     
                     $('#jumSemua').val(parseFloat(totalPenuh).toFixed(2))
                 }
                 else {
                   
                     $('#jumSemua').val(parseFloat(totalPenuh).toFixed(2))
                 }

             }



             $("#chkDN").change(function () {  //jika user tick  checkbox sediakan makan function ni akan dilakukan
                 if (this.checked) {                        
                     $('#totalMakan').val("0.00")
                     $('#hargaEMakan').val("0.00")
                    var totalPenuh = parseFloat($('#totalMakan').val()) + parseFloat($('#totalHotel').val())                     
                     if (isNaN(totalPenuh) === false) {
                         //alert(totalMakan);
                         $('#jumSemua').val(parseFloat(totalPenuh).toFixed(2))
                     }
                     else {
                         $('#jumSemua').val(parseFloat(totalPenuh).toFixed(2))
                     }

                 }
                 else {
                     //$('#chkDN').val("Y")
                     kiraElaunMakan()

                 }
                 //console.log("992")
                 //console.log($('#chkDN').val())
             });

             $(document).ready(function () {
                 $("#chkDN").click(function () {
                     if (this.checked) {
                         /*alert('checked 996');*/
                         checkMakan = "Y";
                        /* console.log("satu checked")*/
                     }
                     if (!this.checked) {
                         /*alert('Unchecked tidak disediakan makanan');*/
                         checkMakan = "N";
                         /*console.log("satu checked 0")*/
                     }
                     console.log(checkMakan)
                 });
                 
             })


             $(document).ready(function () {
                
                 //generateDropdown("ddlTugas", "Pendahuluan_WS.asmx/GetJenisTugas", null, function () {
                 //    kiraElaunMakan();
                 //})
                 generateDropdown("ddlTugas", "Pendahuluan_WS.asmx/GetJenisTugas", null, kiraElaunMakan) 
                 generateDropdown("ddlPenginapan", "Pendahuluan_WS.asmx/GetPenginapan", null, kiraElaunMakan)
                 generateDropdown("ddlTempat", "Pendahuluan_WS.asmx/GetTempat", null, kiraElaunMakan)
                 generateDropdown("ddlBayar", "Pendahuluan_WS.asmx/GetKaedahBayar")
                 generateDropdown("ddlJnsJalan", "Pendahuluan_WS.asmx/GetJenisJalan", null, kiraElaunMakan)
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

              $(function () {
                 //$('.btnAddRow.five').click();
             });


             //Ni digunakan untuk papar senarai permohonan, kemudian klik event pada gridview untuk papar data pada form
             $(tableID_Senarai).on('click', '.btnView', async function () {                
                 //event.preventDefault();
                 var curTR = $(this).closest("tr");
                 var recordID = curTR.find("td > .noPermohonan");
                 //var bool = true;
                 var id = recordID.val();
                
                 if (id !== "") {

                     //BACA HEADER JURNAL
                     var recordHdr = await AjaxGetRecordHdrPermohonan(id);
                     console.log(recordHdr);
                     await setValueToRow_HdrPermohonan(recordHdr.Payload);
                     setValueToRow_Transaksi
                     //await AddRowHeader(null, recordHdr);                     
                 }

                 return false;
             })


 
             async function clearAllRows() {
                 $(tableID + " > tbody > tr ").each(function (index, obj) {
                     if (index > 0) {
                         obj.remove();
                     }
                 })
                
             };


             //event bila klik button simpan  
             $('.btnSimpan').click(async function () {
                
                 var jumRecord = 0;
                 var acceptedRecord = 0;
                 var ifMakanS;
                
                 var linkSurat = $('#uploadedFileNameLabel').text();
                 //var linkSurat = $('uploadedFileNameLabel').html;
                 var msg = "";
                 var pemohon = $('#txtNoStaf').val()
                 var statusPemohon

                
                 //console.log("linkSurat")
                 //console.log(linkSurat)
                 //console.log(linkSurat.text())
                 //console.log(linkSurat.html());

                 if ('<%=Session("ssusrID")%>' !== pemohon) {
                     statusPemohon = "0"  //mohon tuk org lain
                 } else {
                     statusPemohon = "N"  //mohon tuk sendiri
                 }


                 if (linkSurat === "") {
                     alert("Sila Upload Surat Arahan ")
                     return false;
                 }
                 
                 var newAdvance = {
                     AdvList: {
                         OrderID: $('#noPermohonan').val(),                         
                         stafID: $('#txtNoStaf').val(),
                         noTel: $('#txtTel').val(),
                         PtjMohon: $('#hidPtjPemohon').val(),
                         JnsTugas: $('#ddlTugas').val(),
                         JnsJalan: $('#ddlJnsJalan').val(),
                         JnsTempat: $('#ddlTempat').val(),
                         JnsPginapan: $('#ddlPenginapan').val(),
                         JnsBayar: $('#ddlBayar').val(),
                         TkhMula: $('#tkhMula').val(),
                         TkhTamat: $('#tkhTamat').val(),
                         Tempoh: $('#txtTempoh').val(),
                         Lokasi: $('#txtLokasi').val(),
                         Tujuan: $('#txtTujuan').val(),
                         //ArahanK: $('#txtArahan').val(),
                        
                         TkhAdvance: $('#tkh_Adv').val(),
                         JumlahAll: $('#jumSemua').val(),
                         hadMin: $('#txtHadMin').val(),
                         tkhMohon: $('#tkhMohon').val(),
                         Folder: $('#hidJenDok').val(),
                         File_Name :$('#hidFileName').val(),
                         //File_Name: $('#uploadedFileNameLabel').val(),
                         staMakan: checkMakan,
                         statusPemohon: statusPemohon,
                         
                         KodVot: $('.Hid-vot-list').eq(0).html(),
                         kodPTj: $('.Hid-ptj-list').eq(0).html(),
                         kodKW: $('.Hid-kw-list').eq(0).html(),
                         kodKO: $('.Hid-ko-list').eq(0).html(),
                         kodKP: $('.Hid-kp-list').eq(0).html(),                      
                         
                     }
                 }
                                 
                 console.log(newAdvance);
                 console.log(newAdvance.AdvList.tkhMohon);
                 console.log(newAdvance.AdvList.File_Name);
                 

                 //1`ShowPopup("msg")
                 msg = "Anda pasti ingin menyimpan rekod ini?"

                 if (!confirm(msg)) {
                     return false;
                 }
                 show_loader();
                 var result = JSON.parse(await ajaxSaveRecord(newAdvance));
                 alert(result.Message)                 
                 close_loader();
                 //console.log(result.Payload.No_Pendahuluan);
                // console.log(newAdvance.AdvList.OrderID);
                 $('#noPermohonan').val(newAdvance.AdvList.OrderID);
                 tbl.ajax.reload();
                 //await clearAllRowsHdr();               

             });

             async function clearAllRowsHdr() {
                 console.log("1312")
                 $('#noPermohonan').val("");
                 $('#txtLokasi').val("");
                 $('#txtTujuan').val("");
                 $('#txtTempoh').val("");
                 $('#txtNoRujukan').val("");
                 $('#TarikhMohon').val("");
                 $('#tkhMula').val("");
                 $('#tkhTamat').val("");
                 $('#tkh_Adv').val("");
                 $('#hidFileName').val("");
                 $('#hidJenDok').val("");
                 $('#uploadedFileNameLabel').html("");
                 $('#uploadBajet').html("");
                 $('#hidPtjPemohon').val("");
             }
                         
             async function ajaxSaveRecord(order) {

                 return new Promise((resolve, reject) => {
                     $.ajax({
                         url: 'Pendahuluan_WS.asmx/SaveRecordAdv',
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


             async function setValueToRow_HdrPermohonan(orderDetail) {  //ni tuk set value bila klik pada gridview permohonan
                 console.log("masuk 1475")
                 var staMkn
                 $('#noPermohonan').val(orderDetail[0].No_Pendahuluan)         
                 $('#txtLokasi').val(orderDetail[0].Tempat_Perjalanan)                 
                 $('#txtTujuan').val(orderDetail[0].Tujuan)
                 $('#txtTempoh').val(orderDetail[0].Tempoh_Pjln)                              
                 $('#tkhMula').val(orderDetail[0].Tarikh_Mula)
                 $('#tkhTamat').val(orderDetail[0].Tarikh_Tamat)
                 $('#tkh_Adv').val(orderDetail[0].Tkh_Adv_Perlu)
                 $('#txtArahan').val(orderDetail[0].Rujukan_Arahan)
                 $('#tkhMohon').val(orderDetail[0].Tarikh_Mohon)
                 $('#txtLokasi').val(orderDetail[0].Tempat_Perjalanan)
                 $('#txtTujuan').val(orderDetail[0].Tujuan)                 
                 $('#txtHadMin').val(orderDetail[0].Jum_Layak.toFixed(2))
                 $('#jumSemua').val(orderDetail[0].Jum_Mohon.toFixed(2))
                 $('#bilHariMakan').val(orderDetail[0].Tempoh_Pjln)
                 $('#bilHariHotel').val(orderDetail[0].Tempoh_Pjln)                
                 //alert($('#tkh_Adv').val(orderDetail[0].Tkh_Adv_Perlu));
                 $('#hidJenDok').val(orderDetail.Folder)
                 $('#hidFileName').val(orderDetail.File_Name)

                 var newId = $('#ddlTempat')

                 var ddlTempat = $('#ddlTempat')
                 var ddlSearch = $('#ddlTempat')
                 var ddlText = $('#ddlTempat')
                 var selectObj_JenisTempat = $('#ddlTempat')
                 $(ddlTempat).dropdown('set selected', orderDetail[0].JenisTempat);
                 selectObj_JenisTempat.append("<option value = '" + orderDetail[0].JenisTempat + "'>" + orderDetail[0].ButiranJenisTempat + "</option>")

                 var newId = $('#ddlTugas')
                 var ddlTugas = $('#ddlTugas')
                 var ddlSearch = $('#ddlTugas')
                 var ddlText = $('#ddlTugas')
                 var selectObj_JenisTugas = $('#ddlTugas')
                 $(ddlTugas).dropdown('set selected', orderDetail[0].JenisTugas);
                 selectObj_JenisTugas.append("<option value = '" + orderDetail[0].JenisTugas + "'>" + orderDetail[0].ButiranJenisTugas + "</option>")

                 var newId = $('#ddlJnsJalan')
                 var ddlJnsPjln = $('#ddlJnsJalan')
                 var ddlSearch = $('#ddlJnsJalan')
                 var ddlText = $('#ddlJnsJalan')
                 var selectObj_JenisPjln = $('#ddlJnsJalan')
                 $(ddlJnsJalan).dropdown('set selected', orderDetail[0].JenisPjln);
                 selectObj_JenisPjln.append("<option value = '" + orderDetail[0].JenisPjln + "'>" + orderDetail[0].ButiranJenisPjln + "</option>")

                 var newId = $('#ddlPenginapan')
                 var ddlJnsPnginap = $('#ddlPenginapan')
                 var ddlSearch = $('#ddlPenginapan')
                 var ddlText = $('#ddlPenginapan')
                 var selectObj_JenisPnginap = $('#ddlPenginapan')
                 $(ddlPenginapan).dropdown('set selected', orderDetail[0].JenisPginap);
                 selectObj_JenisPnginap.append("<option value = '" + orderDetail[0].JenisPginap + "'>" + orderDetail[0].ButiranJenisPginap + "</option>")

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
                 $(ddl).dropdown('set selected', orderDetail[0].colKW);
                 selectObj.append("<option value = '" + orderDetail[0].Kod_Kump_Wang + "'>" + orderDetail[0].colKW + "</option>")

                 $(".Hid-vot-list").html(orderDetail[0].Kod_Vot);
                

                 //var butirptj = $(".label-ptj-list");
                 //butirptj.html(orderDetail[0].Kod_PTJ);


                 var hidbutirptj = $(".Hid-ptj-list");
                 hidbutirptj.html(orderDetail[0].Kod_PTJ);

                 var butirKW = $(".label-kw-list");
                 butirKW.html(orderDetail[0].colKW);

                 var hidbutirkw = $(".Hid-kw-list");
                 hidbutirkw.html(orderDetail[0].colhidkw);

                 var butirVot = $(".label-vot-list");
                 butirVot.html(orderDetail[0].ButiranVot);

                 var hidbutirVot = $(".Hid-vot-list");
                 hidbutirVot.html(orderDetail[0].Kod_Vot);

                 var butirKo = $(".label-ko-list");
                 butirKo.html(orderDetail[0].colKO);

                 var hidbutirko = $(".Hid-ko-list");
                 hidbutirko.html(orderDetail[0].colhidko);

                 var butirKp = $(".label-kp-list");
                 butirKp.html(orderDetail[0].colKp);

                 var hidbutirkp = $(".Hid-kp-list");
                 hidbutirkp.html(orderDetail[0].colhidkp);
                 kiraElaunMakan()


                 var sumTotal
                 staMkn = orderDetail[0].If_Mkn
                 //console.log("1340--loaddata")
                 //console.log(staMkn)
                 if (staMkn === "Y") {                     
                     $('#chkDN').prop('checked', true)                     
                     $('#totalMakan').val("0.00");
                     $('#hargaEMakan').val("0.00");  
                     //alert($('#totalMakan').val());
                     sumTotal = $('#totalHotel').val();                    
                     $('#jumSemua').val(parseFloat(sumTotal).toFixed(2))
                 }
                 else {
                     
                     $('#chkDN').prop('checked', false)
                    sumTotal = parseFloat($('#totalMakan').val()) + parseFloat($('#totalHotel').val())
                     $('#jumSemua').val(parseFloat(sumTotal).toFixed(2))
                   
                 }              

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


             // andling the row click event
             function rowClickHandler(orderDetail) {
                 clearAllRowsHdr();
                 getDataPeribadiPemohon(orderDetail.Nopemohon)
                 $('#SenaraiPermohonan').modal('toggle');
             
                 console.log(orderDetail.No_Pendahuluan)
                 // change .btnSimpan text to Simpan
                 var staMkn
                 $('#noPermohonan').val(orderDetail.No_Pendahuluan)
                 $('#txtLokasi').val(orderDetail.Tempat_Perjalanan)
                 $('#txtTujuan').val(orderDetail.Tujuan)
                 $('#txtTempoh').val(orderDetail.Tempoh_Pjln)
                 $('#tkhMula').val(orderDetail.Tarikh_Mula)
                 $('#tkhTamat').val(orderDetail.Tarikh_Tamat)
                 $('#tkh_Adv').val(orderDetail.Tkh_Adv_Perlu)
                 $('#txtArahan').val(orderDetail.Rujukan_Arahan)
                 $('#tkhMohon').val(orderDetail.TkhMohonPapar)
                 $('#txtLokasi').val(orderDetail.Tempat_Perjalanan)
                 $('#txtTujuan').val(orderDetail.Tujuan)
                 //$('#txtHadMin').val(parseFloat(orderDetail.Jum_Layak).toFixed(2))
                 //$('#jumSemua').val(parseFloat(orderDetail.Jum_Mohon).toFixed(2))
                 $('#bilHariMakan').val(orderDetail.Tempoh_Pjln)
                 $('#bilHariHotel').val(orderDetail.Tempoh_Pjln)
                 console.log("1540")
                 $('#hidJenDok').val(orderDetail.Folder)
                 $('#hidFileName').val(orderDetail.File_Name)                
                 $('#uploadedFileNameLabel').val(orderDetail.File_Name)
                 console.log(orderDetail.File_Name)
                 $('#txtNamaFile').val(orderDetail.File_Name)
                 console.log(orderDetail.Nopemohon)

                
                // resolvedUrl: resolveAppUrl("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PD/")    
                var fileName = orderDetail.File_Name
                 //var resolvedUrl = "../UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PD/"
                var fileLink = document.createElement("a");
                 fileLink.href = orderDetail.url;
                 fileLink.textContent = fileName;
                 console.log(fileLink.href)
                 var uploadedFileNameLabel = document.getElementById("uploadedFileNameLabel");
                 uploadedFileNameLabel.appendChild(fileLink);


                 $("#uploadedFileNameLabel").show();
                 // Clear the file input
                 $("#fileInput").val("");
               

                 var newId = $('#ddlTempat')

                 var ddlTempat = $('#ddlTempat')
                 var ddlSearch = $('#ddlTempat')
                 var ddlText = $('#ddlTempat')
                 var selectObj_JenisTempat = $('#ddlTempat')
                 $(ddlTempat).dropdown('set selected', orderDetail.JenisTempat);
                 selectObj_JenisTempat.append("<option value = '" + orderDetail.JenisTempat + "'>" + orderDetail.ButiranJenisTempat + "</option>")

                 var newId = $('#ddlTugas')
                 var ddlTugas = $('#ddlTugas')
                 var ddlSearch = $('#ddlTugas')
                 var ddlText = $('#ddlTugas')
                 var selectObj_JenisTugas = $('#ddlTugas')
                 $(ddlTugas).dropdown('set selected', orderDetail.JenisTugas);
                 selectObj_JenisTugas.append("<option value = '" + orderDetail.JenisTugas + "'>" + orderDetail.ButiranJenisTugas + "</option>")

                 var newId = $('#ddlJnsJalan')
                 var ddlJnsPjln = $('#ddlJnsJalan')
                 var ddlSearch = $('#ddlJnsJalan')
                 var ddlText = $('#ddlJnsJalan')
                 var selectObj_JenisPjln = $('#ddlJnsJalan')
                 $(ddlJnsJalan).dropdown('set selected', orderDetail.JenisPjln);
                 selectObj_JenisPjln.append("<option value = '" + orderDetail.JenisPjln + "'>" + orderDetail.ButiranJenisPjln + "</option>")

                 var newId = $('#ddlPenginapan')
                 var ddlJnsPnginap = $('#ddlPenginapan')
                 var ddlSearch = $('#ddlPenginapan')
                 var ddlText = $('#ddlPenginapan')
                 var selectObj_JenisPnginap = $('#ddlPenginapan')
                 $(ddlPenginapan).dropdown('set selected', orderDetail.Jenis_Penginapan);
                 selectObj_JenisPnginap.append("<option value = '" + orderDetail.Jenis_Penginapan + "'>" + orderDetail.ButiranJenisPginap + "</option>")

                 var newId = $('#ddlBayar')
                 var ddlJnsPnginap = $('#ddlBayar')
                 var ddlSearch = $('#ddlBayar')
                 var ddlText = $('#ddlBayar')
                 var selectObj_JenisBayar = $('#ddlBayar')
                 $(ddlBayar).dropdown('set selected', orderDetail.CaraBayar);
                 selectObj_JenisBayar.append("<option value = '" + orderDetail.CaraBayar + "'>" + orderDetail.ButiranJenisBayar + "</option>")

                 console.log(orderDetail);
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
                 hidbutirptj.html(orderDetail.Ptj);

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
                 kiraElaunMakan()


                 var sumTotal
                 
                 staMkn = orderDetail.If_Mkn
                 checkMakan = staMkn;

                 //console.log("1340--loaddata")
                 //console.log(staMkn)
                 if (staMkn === "Y") {
                     $('#chkDN').prop('checked', true)
                     $('#totalMakan').val("0.00");
                     $('#hargaEMakan').val("0.00");
                     //alert($('#totalMakan').val());
                     sumTotal = $('#totalHotel').val();
                     $('#jumSemua').val(parseFloat(sumTotal).toFixed(2))
                 }
                 else {

                     $('#chkDN').prop('checked', false)
                     sumTotal = parseFloat($('#totalMakan').val()) + parseFloat($('#totalHotel').val())
                     $('#jumSemua').val(parseFloat(sumTotal).toFixed(2))

                 }

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



             async function AddRowHeader(totalClone, objOrder) {
                 var counter = 1;
                 //var table = $('#tblDataSenarai');

                 if (objOrder !== null && objOrder !== undefined) {
                     totalClone = objOrder.Payload.length;
                 }


                 if (counter <= objOrder.Payload.length) {
                     await setValueToRow_HdrJurnal(objOrder.Payload[counter - 1]);
                 }
                 // console.log(objOrder)
             }

             //ajak untuk keluarkan maklumat permohonan dr gridview
             async function AjaxGetRecordPermohonan(id) {

                 try {

                     const response = await fetch('Pendahuluan_WS.asmx/LoadRecordPermohonan', {
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

             async function AjaxGetRecordHdrPermohonan(id) {

                 try {

                     const response = await fetch('Pendahuluan_WS.asmx/LoadHdrPermohonan', {
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

        //$(document).ready(function () {
        //    $('#tblDataSenarai').DataTable({
        //        "responsive": true,
        //        "sPaginationType": "full_numbers",
        //        "oLanguage": {
        //            "oPaginate": {
        //                "sNext": '<i class="fa fa-forward"></i>',
        //                "sPrevious": '<i class="fa fa-backward"></i>',
        //                "sFirst": '<i class="fa fa-step-backward"></i>',
        //                "sLast": '<i class="fa fa-step-forward"></i>'
        //            },
        //            "sLengthMenu": "Tunjuk _MENU_ rekod",
        //            "sZeroRecords": "Tiada rekod yang sepadan ditemui",
        //            "sInfo": "Menunjukkan _END_ dari _TOTAL_ rekod",
        //            "sInfoEmpty": "Menunjukkan 0 ke 0 dari rekod",
        //            "sInfoFiltered": "(ditapis dari _MAX_ jumlah rekod)",
        //            "sEmptyTable": "Tiada rekod.",
        //            "sSearch": "Carian"
        //        }
        //    });
        //});
         </script>
    

     
</asp:Content>
