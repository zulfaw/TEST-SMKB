<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PenerimaanPD.aspx.vb" Inherits="SMKB_Web_Portal.PenerimaanPD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
     <style>
         #tblDataSenarai td:hover {
            cursor: pointer;
          }
    </style>
<div class="form-row justify-content-right">
    <br />
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
                                <table id="tblDataSenarai" class="table table-striped" style="width:95%">
                                    <thead>
                                        <tr>
                                            <th scope="col" style="width: 10%">No. Permohonan</th>
                                            <th scope="col" style="width: 10%">Tarikh Mohon</th>
                                            <th scope="col" style="width: 30%">Nama Pemohon</th>                                          
                                            <th scope="col" style="width: 15%">Jumlah Mohon (RM)</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tableID_SenaraiPermohonan">
                                        
                                    </tbody>

                                </table>

                                 

                            </div>
                        </div>                  
                    </div>
    <!-- Modal Permohonan -->
 <div class="modal fade" id="modalSenarai" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle"
                aria-hidden="true">
               <div class="modal-dialog modal-dialog-centered modal-xl modal-dialog-scrollable"  role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Maklumat Permohonan</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="btnCloseModal">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
 <div class="modal-body">
     <button type="button" class="btn btn-info" data-toggle="modal" data-target="#myPersonal">Maklumat Pegawai</button> <asp:Image ID="Image2" runat="server"  AlternateText="Klik Lihat Maklumat Pegawai" ImageUrl="~/assets/icon/info-solid.svg" Height="2%" Width="2%" />
                 <div class="panel panel-default">
              <div class="panel-heading"></div>
             

                 <%-- <div class="form-row">                  
                        <label for="inputHadMin" class="col-sm-3 col-form-label" style="font-size:small">Had Minimum Pendahuluan Anda Ialah RM </label>
                        <div class="col-sm-2">
                          <input type="text" class="form-control input-md" id="txtHadMin"  style="text-align:right; font-weight: bold;font-size:small" readonly >
                    
                        </div>--%>
                    
           </div>

        <div class="col-sm-12"> 
            <div class="form-row">
                <div class="form-group col-sm-3" style="left: -1px; top: 0px">
                    <label for="noPermohonan" class="col-form-label" style="font-size:small">No.Permohonan:</label>
                    <input type="text" class="form-control input-md" id="noPermohonan" style="background-color:#f3f3f3;font-size:small" >
                </div>
                <div class="form-group col-sm-3">
                    <label for="kodModul" style="font-size:small">Jenis Perjalanan</label><br />
                    <select style="font-size:2px" class="ui search dropdown jnsjalan-list" name="ddlJnsJalan" id="ddlJnsJalan"></select>
                <%--<asp:DropDownList ID="ddlJnsTugas"  AutoPostBack="false" runat="server" CssClass="form-control" ></asp:DropDownList> --%>
                </div>
                <div class="form-group col-sm-3">
                    <label for="date-input" style="font-size:small">Jenis Tugas</label> <br />
                    <select class="ui search dropdown tugas-list" name="ddlTugas" id="ddlTugas" style="left: 0px; top: 0px;font-size:small"></select>                                                        
                </div>
                <div class="form-group col-sm-3">
                    <label for="TarikhMohon" class="col-form-label" style="font-size:small">Tarikh Mohon:</label>
                    <input type="date" class="form-control input-sm" id="tkhMohon" style="background-color:#f3f3f3;font-size:small" />                                   
                </div>
                
            </div>
            </div>
      
 <div class="form-row">  <%--buka tuk content transaksi--%>
    <div class="title">Transaksi</div>
        <div class="col-sm-12">
            <div class="">
                <table  id="tblData"  class="table table-striped"><%-- class="table table-bordered"--%>
                    <thead>
                        <tr>
                            <th scope="col"  style="width: 30%;font-size:small">Vot</th>
                            <th scope="col"  style="width: 20%;font-size:small">PTj</th>
                            <th scope="col"  style="width: 20%;font-size:small">Kumpulan Wang</th>
                            <th scope="col"  style="width: 15%;font-size:small">Operasi</th>
                            <th scope="col"  style="width: 15%;font-size:small">Projek</th>
                        </tr>
                    </thead>
                    <tbody id="tableID">
                        <tr  class="table-list">
                            <td>
                            <select class="ui search dropdown COA-list" name="ddlCOA" id="ddlCOA" style="width:300px;font-size:small"></select>
                            <input type="hidden" class="data-id" value="" />
                            <label id="hidVot" name="hidVot" class="Hid-vot-list" style="visibility: hidden"></label>
                            </td>
                            <td>
                            <label id="lblPTj" name="lblPTj" class="label-ptj-list" style="justify-items:center;font-size:small" ></label>
                            <label id="HidlblPTj" name="HidlblPTj" class="Hid-ptj-list" style="visibility: hidden"></label>
                            </td>

                            <td>
                            <label id="lblKw" name="lblKw" class="label-kw-list" style="justify-items:center;font-size:small"></label>
                            <label id="HidlblKw" name="HidlblKw" class="Hid-kw-list" style="visibility: hidden"></label>
                            </td>
                            <td>
                            <label id="lblKo" name="lblKo" class="label-ko-list" style="justify-items:center;font-size:small"></label>
                            <label id="HidlblKo" name="HidlblKo" class="Hid-ko-list" style="visibility: hidden"></label>
                            </td>
                            <td>
                            <label id="lblKp" name="lblKp" class="label-kp-list" style="justify-items:center;font-size:small"></label>
                            <label id="HidlblKp" name="HidlblKp" class="Hid-kp-list" style="visibility: hidden"></label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
              
        <div class="col-sm-12">             
            <div class="form-row">
                <div class="form-group col-sm-4">
                    <label for="date-input" style="font-size:small">Tarikh Mula</label>  <br />                                      
                        <input type="date" class="form-control" style="font-size:small" id="tkhMula">                                       
                </div>
                <div class="form-group col-sm-4">
                        <label for="date-input2" style="font-size:small">Tarikh Tamat</label> <br />                                       
                        <input type="date" class="form-control" style="font-size:small" id="tkhTamat">
                </div>
                                    
                <div class="form-group col-sm-4">
                    <label for="kodModul" style="font-size:small">Tempoh</label><br />
                    <input type="text" class="form-control" style="font-size:small" id="txtTempoh" readonly>
                </div>                              
            </div>   
        </div> 

        <div class="col-sm-12"> 
            <div class="form-row"> 
                <div class="form-group col-sm-4">
                <label for="date-input" style="font-size:small">Tempat</label> <br />       
                        <%--<textarea rows="2" cols="45" ID="txtTempat" class="form-control" MaxLength="100"></textarea>--%>
                        <select class="ui search dropdown tempatlist" name="ddlTempat" id="ddlTempat" style="left: 0px; top: 0px"></select>
                    <%--<input type ="text" id="txtTempat"  class="form-control" maxlength="100" />  --%>                                    
                </div>                                      
                <div class="form-group col-sm-4">
                    <label for="kodModul" style="font-size:small">Lokasi Perjalanan</label>  <br /> 
                    <textarea rows="2" cols="45" ID="txtLokasi" class="form-control" MaxLength="100"></textarea>
                    <%--<input type="text" ID="txtLokasi"  class="form-control" MaxLength="100"/>--%>
                </div> 
                    <div class="form-group col-sm-4">
                    <label for="date-input2" style="font-size:small">Tujuan Perjalanan</label> <br />  
                    <textarea rows="2" cols="45" ID="txtTujuan" class="form-control" MaxLength="500"></textarea>
                <%--  <input type="text" ID="txtTujuan"  Width="100%" class="form-control" MaxLength="500" multiple="multiple" />--%>
                </div>                                    
            </div> 
        </div>
        <div class="col-sm-12"> 
            <div class="form-row"> 
                <div class="form-group col-sm-4">
                <label for="kodModul" style="font-size:small">Arahan Rujukan</label>    <br />                                    
                <input type="text" ID="txtArahan"   Width="100%" class="form-control" maxaria-describedby="sizing-addon3" MaxLength="50"/>
                </div>
                <div class="form-group col-sm-3">
                <label for="kodModul" aria-describedby="sizing-addon3" style="font-size:small">Penginapan</label>   <br />
                <select class="ui search dropdown penginapan-list" name="ddlPenginapan" id="ddlPenginapan" ></select>
                                                      
                </div>
                <div class="form-group col-sm-1">
                <div class="input-group">
                <label for="kodModul"  style="font-size: small">Makanan Disediakan</label><br />
                <input type="checkbox" name="checkfield" id="chkDN" value="false" />
                </div>
                </div> 
            </div>
        </div>

        <div class="col-sm-12">                            
            <div class="form-row">
            <div class="form-group col-sm-4">                                      
            <label for="kodModul" style="font-size:small">Tarikh Pendahuluan Dikehendaki</label>                                       
            <input type="date" class="form-control" style="font-size:small" id="tkh_Adv" >
                                       
            </div>   
            <div class="form-group col-sm-4">                                      
            <label for="kodModul" style="font-size:small">Kaedah Pembayaran</label>  <br />
            <select class="ui search dropdown bayar-list" name="ddlBayar" id="ddlBayar" style="left: 0px; top: 0px"></select>
                                        
            </div>  
            </div> 
        </div> 
     </div>
           <%-- tutup content transaksi--%>
      
           <%-- buka jadual Jumlah PP--%>                
            <div class="title">Jumlah Pendahuluan</div>
            <div class="panel-body">
                <div class="form-row">
                    <div class="form-group col-sm-12"> 
                        <!-- Table -->
                        <table class="table">
                        <tr>
                            <td style="font-size:small;height: 52px;width:200px">Elaun Makan :</td>
                                             
                            <td>RM <input type="text" ID="hargaEMakan" class="form-control" style="text-align:right; font-weight: bold;font-size:small"  readonly />
                                <h6 style="color: #FF3300;font-family:Helvetica Neue,Arial,Noto Sans,Liberation Sans;font-size:12px">*Sila Pilih Jenis Perjalanan, Jenis Tugas dan Tempat</h6></td>
                            <td> X </td>
                            <td style="height: 52px;width:100px"> Hari <input type="text" ID="bilHariMakan" class="form-control" style="text-align:right; font-weight: bold;font-size:small"  readonly />
                                <h7 style="color: #FF3300; font-family:Helvetica Neue,Arial,Noto Sans,Liberation Sans;font-size:12px">*Sila Pilih Tarikh Mula dan Tarikh Tamat</h7>
                            </td>
                            <td> X </td>
                            <td style="height: 52px;width:100px"><label></label>
                                <input type="text" ID="catPercent" class="form-control" style="text-align:right; font-weight: bold;font-size:small"  readonly />
                                
                            </td>
                            <td> = </td>
                            <td>RM <input type="text" ID="totalMakan" class="form-control" style="text-align:right; font-weight: bold;font-size:small" readonly /></td>
                        </tr>
                        <tr>
                            <td style="height: 52px">Elaun Lojing/Hotel :</td>
                            <td style="height: 52px">RM <input type="text" ID="hargaLojing" class="form-control" style="text-align:right; font-weight: bold;font-size:small" readonly/>
                                <h7 style="color: #FF3300; font-family:Helvetica Neue,Arial,Noto Sans,Liberation Sans;font-size:12px">*Sila Pilih Jenis Tugas,Jenis Perjalanan, Tempat dan Penginapan</h7>
                            </td>
                            <td style="height: 52px;width:50px"> X </td>
                            <td style="height: 52px;width:100px">Hari <input type="text" ID="bilHariHotel" class="form-control" style="text-align:right; font-weight: bold;font-size:small" readonly />
                                <h7 style="color: #FF3300; font-family:Helvetica Neue,Arial,Noto Sans,Liberation Sans;font-size:12px">*Sila Pilih Tarikh Mula dan Tarikh Tamat</h7>
                            </td>
                            <td> X </td>
                            <td style="height: 52px;width:100px"><label></label>
                                <input type="text" ID="catPercent1" class="form-control" style="text-align:right; font-weight: bold;font-size:small"  readonly />
                                
                            </td>
                            <td style="height: 52px"> = </td>
                            <td style="height: 52px">RM <input type="text" ID="totalHotel" class="form-control" style="text-align:right; font-weight: bold;font-size:small" readonly /></td>
                        </tr>
                        <tr>
                            <td  style="font-size:small;height: 52px;width:200px">Kelayakan Minimum : </td>  
                             <td>RM <input type="text" ID="txtHadMin" class="form-control" style="text-align:right; font-weight: bold;font-size:small"  readonly />
                          
                           <td colspan="3"></td>
                             <td style="height: 52px;width:100px">Jumlah Mohon<label></label>                              
                            </td>
                            <td> = </td>
                            <td>RM <input type="text" ID="jumSemua" class="form-control" style="text-align:right; font-weight: bold;font-size:small" readonly /></td>
              
                        </tr>
                        <tr>
                            <td colspan="6"><h7 style="color: #FF3300; font-family:Helvetica Neue,Arial,Noto Sans,Liberation Sans;font-size:12px">Nota : Pendahuluan hanya dikira berdasarkan tempoh berkursus / bertugas rasmi SAHAJA. Tidak termasuk hari perjalanan.</h7></td>
                        </tr>
                        </table>      
                    </div>
                </div>
            </div>                             
            <%-- tutup jadual Jumlah PP--%>

     
</div>
<div class="modal-footer">   
    <button type="button"  runat="server"  class="btn btn-secondary lbtnSimpan" data-toggle="modal" data-target="#penerimaan">Penerimaan</button>
</div> 
        </div>  <%--tutup modal-content--%>
    </div>  <%--tutup class-document--%>
</div>  <%--tutup modal-idSenarai--%>
 

 
    
  <!-- Modal Info Pegawai -->
            <div id="myPersonal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
               
            <!-- Modal content-->
            <div class="modal-content">
            <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal"></button>
            <h4 class="modal-title">Maklumat Pegawai</h4> 
            </div>
            <div class="modal-body">
               
                     <asp:Panel ID="Panel2" runat="server" >
                                 <div class="form-row">  
                                    
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Nama</label><br />
                                        <input type="text" id="txtNamaP"  class="form-control"  />
                                         <%--<asp:TextBox ID="txtNamaP" runat="server" Width="100%" class="form-control input-sm" style="background-color:#f3f3f3"></asp:TextBox>--%>
                                    </div>                                    
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">No.Pekerja</label><br />
                                        <input type="text" ID="txtNoPekerja"  Width="100%" class="form-control"  />                                       
                                    </div>                               
                               </div>

                                <div class="form-row">
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Jawatan</label><br />
                                        <input type="text" ID="txtJawatan"  Width="100%" class="form-control"  />                                        
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Gred Gaji</label><br />
                                        <input type="text" ID="txtGredGaji" Width="100%" class="form-control"   />                                       
                                    </div>
                                </div>

                                 <div class="form-row">
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Pejabat/Jabatan/Fakulti</label><br />
                                        <input type="text" ID="txtPejabat"  Width="100%" class="form-control" /> 
                                        <%-- <asp:TextBox ID="txtPejabat" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Kumpulan</label><br />
                                        <input type="text"  ID="txtKump"  Width="100%" class="form-control"/>
                                       <%--<asp:TextBox ID="txtKump" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                                    </div>
                                </div>

                               <div class="form-row">
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Memangku Jawatan</label><br />
                                        <input type="text"  ID="txtMemangku"  Width="100%" class="form-control"  />
                                        <%-- <asp:TextBox ID="txtMemangku" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Samb. Tel</label><br />
                                        <input type="text"  ID="txtTel"  Width="100%" class="form-control"  />
                                       <%--<asp:TextBox ID="txtTel" runat="server" Width="100%" CssClass="form-control" style="background-color:#f3f3f3"></asp:TextBox>--%>
                                    </div>
                                </div>
                    </asp:Panel>
            </div>
            <div class="modal-footer">
            <button type="button" class="btn btn-default tutupInfo"  data-dismiss="modal">Close</button>
            </div>
            </div>

            </div>
        </div>
  <%-- tutup modal info pegawai--%>

    <!-- Modal Penerimaan -->
            <div id="penerimaan" class="modal fade hide" role="dialog">
            <div class="modal-dialog modal-lg" role="dialog">
               
            <!-- Modal content-->
            <div class="modal-content">
            <div class="modal-header"><h4>Maklumat Pegawai Yang Melulus</h4>
            <button type="button" class="close" data-dismiss="modal"></button>
            <h4 class="modal-title"></h4> 
            </div>
            <div class="modal-body">
               
                     <asp:Panel ID="Panel1" runat="server" >
                                 <div class="form-row">  
                                    
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Dilulus Oleh</label><br />
                                        <input type="text" id="namaPelulus"  class="form-control"  />
                                         <%--<asp:TextBox ID="txtNamaP" runat="server" Width="100%" class="form-control input-sm" style="background-color:#f3f3f3"></asp:TextBox>--%>
                                    </div>                                    
                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Jawatan Pelulus</label><br />
                                        <input type="text" ID="jwtnPelulus"  Width="100%" class="form-control"  />                                       
                                    </div>                               
                               </div>

                                <fieldset>
                                  <h6>Kelulusan Permohonan Pendahuluan</h6>

                                  <div>
                                    <input type="radio"  class="radioBtnClass" name="status" value="06" checked />
                                    <label for="huey">Ya</label>
                                  </div>

                                  <div>
                                    <input type="radio" class="radioBtnClass" name="status" value="15" />
                                    <label for="dewey">Tidak</label>
                                  </div>

                                    <div class="form-group col-sm-6">                                        
                                        <input type="text" ID="txtCatatan"  Width="100%" class="form-control"  />                                       
                                    </div>   

                                </fieldset>

                                    <div class="form-group col-sm-6">
                                        <label for="kodModul">Jumlah Lulus (RM)</label><br />
                                        <input type="text" ID="txtJumLulus"  Width="100%" class="form-control"  />                                       
                                    </div>            
                           

                                 
                    </asp:Panel>
            </div>
            <div class="modal-footer">
            <button type="button" runat="server" class="btn btn-secondary btnSavePelulus"  data-dismiss="modal">Simpan</button>
            </div>
            </div>

            </div>
        </div>
  <%-- tutup modal info pegawai--%>
   
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
           kiraElaunMakan()

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
                   "url": "Penerimaan_WS.asmx/LoadOrderRecordPD",
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
                           staffP: '<%=Session("ssusrID")%>'
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
                   {
                       "data": "Jum_Mohon",
                       render: function (data, type, full) {
                           return parseFloat(data).toFixed(2);
                       }

                   }                  
                  
               ]
           });
       });

       
       $('.btnPapar').click(async function () {
           tbl.ajax.reload();
       });


       $('.btn-info').click(async function () {
           $('#myPersonal').modal('toggle');
          $('#modalSenarai').modal('toggle');

       });

       $('.tutupInfo').click(async function () {
           //$('#myPersonal').modal('toggle');
           $('#modalSenarai').modal('toggle');

       });
       
       
       $('.lbtnSimpan').click(async function () {
           console.log("657")
           $('#penerimaan').modal('toggle');
           $('#modalSenarai').modal('toggle');
           var jumLulus = $('#jumSemua').val()
           $('#txtJumLulus').val(jumLulus);

           console.log($('#jumSemua').val());
           console.log($('#txtJumLulus').val());
           
           loadRecordPelulus($('#noPermohonan').val())

       });

       async function ajaxSavePelulus(id) {

           return new Promise((resolve, reject) => {
               $.ajax({
                   url: 'Penerimaan_WS.asmx/SaveRecordPenerimaan',
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

       function loadRecordPelulus(nomohon) {
           //Cara Pertama
           console.log("masuk loadrecord")
           var mohonID = nomohon
           console.log(mohonID)

           fetch('Penerimaan_WS.asmx/GetDataPelulus', {
               method: 'POST',
               headers: {
                   'Content-Type': "application/json"
               },
                     //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
               body: JSON.stringify({ mohonID: mohonID })
                 })
                .then(response => response.json())
                .then(data => setDataPelulus(data.d))
                 
             }

       function setDataPelulus(data) {
                 data = JSON.parse(data);
                 if (data.mohonID === "") {
                     alert("Tiada data"); 
                     return false;
                 }
           console.log(data);
           $('#namaPelulus').val(data[0].NamaPelulus);
           $('#jwtnPelulus').val(data[0].Jawatan);
        
                 //$('#<%'=txtMemangku.ClientID%>').val(data[0].Param3);
                 
           }


       $('.btnSavePelulus').click(async function () {
           console.log("masuk 697")
           $('#modalSenarai').modal('hide');
          //$('#penerimaan').modal('hide');

           if ($("input[type='radio'].radioBtnClass").is(':checked')) {
               var card_type = $("input[type='radio'].radioBtnClass:checked").val();
               //alert(card_type);
           }

           console.log("status")
           console.log(card_type)

           var UpdateData = {
               Terimaan: {
                   mohonID: $('#noPermohonan').val(),
                   stafID: $('#txtNoStaf').val(),
                   catatan: $('#txtCatatan').val(),
                   TkhMula: $('#tkhMula').val(),
                   TkhTamat: $('#tkhTamat').val(),
                   Tempoh: $('#txtTempoh').val(),
                   jumlahMohon: $('#jumSemua').val(),
                   jumlahLulus: $('#txtJumLulus').val(),
                   statusDok: card_type,

               }
           }

           msg = "Anda pasti ingin menyimpan rekod ini?"
           if (!confirm(msg)) {
               return false;
           }
           var result = JSON.parse(await ajaxSavePelulus(UpdateData));
           alert(result.Message)
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

       function getDataPeribadi() {
           //Cara Pertama

           var nostaf = '<%=Session("ssusrID")%>'          
           console.log(nostaf)

            fetch('Penerimaan_WS.asmx/GetUserInfo', {
                method: 'POST',
                headers: {
                    'Content-Type': "application/json"
                },
                     //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
                     body: JSON.stringify({ nostaf: nostaf })
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
                 hadGaji = data[0].GredGaji; 
                 console.log(hadGaji);
                 //$('#<%'=txtMemangku.ClientID%>').val(data[0].Param3);
                 getHadMinPendahuluan();
             }

            
             function getHadMinPendahuluan() {
                
                 //Cara Pertama          
                   fetch('Penerimaan_WS.asmx/GetHadMin', {
                     method: 'POST',
                     headers: {
                         'Content-Type': "application/json"
                     },
                       //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
                       body: JSON.stringify({ hadMin: hadGaji })
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
                   url: 'Penerimaan_WS.asmx/GetVotCOA?q={query}',
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

       $(document).ready(function () {

           //generateDropdown("ddlTugas", "Pendahuluan_WS.asmx/GetJenisTugas", null, function () {
           //    kiraElaunMakan();
           //})
           generateDropdown("ddlTugas", "Penerimaan_WS.asmx/GetJenisTugas", null, kiraElaunMakan)
           generateDropdown("ddlPenginapan", "Penerimaan_WS.asmx/GetPenginapan", null, kiraElaunMakan)
           generateDropdown("ddlTempat", "Penerimaan_WS.asmx/GetTempat", null, kiraElaunMakan)
           generateDropdown("ddlBayar", "Penerimaan_WS.asmx/GetKaedahBayar")
           generateDropdown("ddlJnsJalan", "Penerimaan_WS.asmx/GetJenisJalan", null, kiraElaunMakan)
       });

       // andling the row click event
       function rowClickHandler(orderDetail) {

           // change .btnSimpan text to Simpan
           $('.btnSimpan').text('Simpan')
           $('.btnSimpan').removeClass('btn-success');
           $('.btnSimpan').addClass('default-primary');
           $('#modalSenarai').modal('toggle');

           var staMkn
           $('#noPermohonan').val(orderDetail.No_Pendahuluan)
           $('#txtLokasi').val(orderDetail.Tempat_Perjalanan)
           $('#txtTujuan').val(orderDetail.Tujuan)
           $('#txtTempoh').val(orderDetail.Tempoh_Pjln)
           $('#tkhMula').val(orderDetail.Tarikh_Mula)
           $('#tkhTamat').val(orderDetail.Tarikh_Tamat)
           $('#tkh_Adv').val(orderDetail.Tkh_Adv_Perlu)
           $('#txtArahan').val(orderDetail.Rujukan_Arahan)
           $('#tkhMohon').val(orderDetail.Tarikh_Mohon)
           $('#txtLokasi').val(orderDetail.Tempat_Perjalanan)
           $('#txtTujuan').val(orderDetail.Tujuan)
           //$('#txtHadMin').val(parseFloat(orderDetail.Jum_Layak).toFixed(2))
           //$('#jumSemua').val(parseFloat(orderDetail.Jum_Mohon).toFixed(2))
           $('#bilHariMakan').val(orderDetail.Tempoh_Pjln)
           $('#bilHariHotel').val(orderDetail.Tempoh_Pjln)
           console.log(orderDetail.Nopemohon)


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
           $(ddlPenginapan).dropdown('set selected', orderDetail.JenisPginap);
           selectObj_JenisPnginap.append("<option value = '" + orderDetail.JenisPginap + "'>" + orderDetail.ButiranJenisPginap + "</option>")

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
           $(ddl).dropdown('set selected', orderDetail.KodVot);
           selectObj.append("<option value = '" + orderDetail.Kod_Vot + "'>" + orderDetail.ButiranVot + "</option>")

           var hidVot = $(".Hid-vot-list");
           hidVot.html(orderDetail.ButiranVot);

           var butirptj = $(".label-ptj-list");
           butirptj.html(orderDetail.Ptj);

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

           console.log($('#ddlJnsJalan').val())
           if ($('#ddlJnsJalan').val() === "DN") {
               $('#catPercent').val("90%")
               $('#catPercent1').val("90%")
           }
           else {
               $('#catPercent').val("100%")
               $('#catPercent1').val("100%")
           }
           
           kiraElaunMakan()


           var sumTotal
           staMkn = orderDetail.If_Mkn
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

       function kiraElaunMakan() {

           var jtugas = $('#ddlTugas').val()
           var jtempat = $('#ddlTempat').dropdown("get value")
           var jjalan = $('#ddlJnsJalan').val()
           var jpginap = $('#ddlPenginapan').val()

           if (jtugas === "null" || jtempat === "null" || jjalan === "null" || jpginap === "null") {
               alert("Sila Pilih Jenis Tugas, Jenis Tempat, Jenis Perjalanan atau Jenis Penginapan")
               return false;
           }

           var param = {
               "jtugas": jtugas,
               "jtempat": jtempat,
               "jjalan": jjalan,
               "jpginap": jpginap
           };

           fetch('../PERMOHONAN PENDAHULUAN/Pendahuluan_WS.asmx/GetKiraAdv', {
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
           var peratus
           var semakStaMakan
          
           if ($('#ddlJnsJalan').val() === "DN") {
               $('#catPercent').val("90%")
               $('#catPercent1').val("90%")
               peratus = "0.9"
           }
           else {
               $('#catPercent').val("100%")
               $('#catPercent1').val("100%")
               peratus = "1.0"
           }


           console.log($('#ddlJnsJalan').val())
           console.log($('#catPercent').val())
           console.log($('#catPercent1').val())
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
               totalMakan = parseFloat(data[0].KadarMkn) * parseFloat($('#bilHariMakan').val()) * peratus
           }

           totalLojing = parseFloat($('#hargaLojing').val()) * parseFloat($('#bilHariHotel').val()) * peratus
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



       function SaveSucces() {
           $('#MessageModal').modal('toggle');

       }


   </script>

  


</asp:Content>
