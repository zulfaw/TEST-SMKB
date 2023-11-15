<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="PenerimaanPP.aspx.vb" Inherits="SMKB_Web_Portal.PenerimaanPP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <style>
         #tblDataSenarai td:hover {
            cursor: pointer;
          }
    </style>
<div class="table-title">                    
        Senarai Permohonan Pendahuluan Diri
        </div> 
    <br />
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
                                            <th scope="col" style="width: 20%">Tujuan</th>
                                            <th scope="col" style="width: 15%">Jumlah Mohon (RM)</th>
                                            <th scope="col" style="width: 15%">Status Terkini </th>                                          

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
                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Maklumat Permohonan</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close" id="btnCloseModal">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
 <div class="modal-body">
                 <div class="panel panel-default">
              <div class="panel-heading"></div>
              <div class="container"> 
                  <div class="form-row">
                      <div class="col-sm-12"> 
                    <div class="form-group col-sm-5">                  
                    </div>
                     </div>
                  </div>
 
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
                        <label for="noPermohonan" class="col-form-label">No.Permohonan:</label>
                        <input type="text" class="form-control input-md" id="noPermohonan" style="background-color:#f3f3f3" >
                    </div>                    
                    <div class="form-group col-md-3">
                        <label for="TarikhMohon" class="col-form-label">Tarikh Mohon:</label>
                        <input type="date" class="form-control input-sm" id="tkhMohon" style="background-color:#f3f3f3" />                                   
                    </div>                
                </div>
               </div>
                <div class="col-md-12">
                
                    <table class="table table-bordered" id="tblData">
                        <thead>
                            <tr>
                                <th scope="col">COA</th>
                                <th scope="col">PTj</th>
                                <th scope="col">Kumpulan Wang</th>
                                <th scope="col">Operasi</th>
                                <th scope="col">Projek</th>
                                
                            </tr>
                        </thead>
                        <tbody id="tableID">
                            <tr style="width: 100%" class="table-list">
                                <td style="width: 30%">
                                    <select class="ui search dropdown COA-list" name="ddlCOA" id="ddlCOA" style="width:99%"></select>
                                    <input type="hidden" class="data-id" value="" />
                                    <label id="hidVot" name="hidVot"  class="Hid-vot-list" hidden="hidden"></label>
                                </td>
                                <td style="width: 12%">
                                    <label id="lblPTj" name="lblPTj" class="label-ptj-list" ></label>
                                    <label id="HidlblPTj" name="HidlblPTj" class="Hid-ptj-list" style="visibility: hidden"></label>
                                </td>

                                <td style="width: 12%">
                                    <label id="lblKw" name="lblKw" class="label-kw-list"></label>
                                    <label id="HidlblKw" name="HidlblKw" class="Hid-kw-list" style="visibility: hidden"></label>
                                </td>
                                <td style="width: 10%">
                                    <label id="lblKo" name="lblKo" class="label-ko-list"></label>
                                    <label id="HidlblKo" name="HidlblKo" class="Hid-ko-list" style="visibility: hidden"></label>
                                </td>
                                <td style="width: 10%">
                                    <label id="lblKp" name="lblKp" class="label-kp-list"></label>
                                    <label id="HidlblKp" name="HidlblKp" class="Hid-kp-list" style="visibility: hidden"></label>
                                </td>                              

                            </tr>
                        </tbody>                        
                    </table>
                    </div>

                <div class="col-md-12">             
                    <div class="form-row">
                        <div class="form-group col-md-4">                                      
                            <label for="kodModul">Tarikh Pendahuluan Dikehendaki</label>                                       
                            <input type="date" class="form-control" id="tkh_Adv" >
                        </div>   
                        <div class="form-group col-md-4">                                      
                            <label for="kodModul">Kaedah Pembayaran</label>  <br />
                            <select class="ui search dropdown bayar-list" name="ddlBayar" id="ddlBayar" style="left: 0px; top: 0px"></select>
                        </div>  
                        <div class="form-group col-md-4">
                            <label for="kodModul">Peruntukan Program</label><br />
                            <input type="text" class="form-control" id="txtPeruntukan" readonly>
                        </div>      
                                                         
                    </div>                       
                </div>
           
                <div class="col-md-12">                 
                    <div class="form-row">
                        <div class="form-group col-md-4">
                            <label for="date-input">Tarikh Mula</label>  <br />                                      
                            <input type="date" class="form-control" id="tkhMula">                                       
                        </div>
                        <div class="form-group col-md-4">
                            <label for="date-input2">Tarikh Tamat</label> <br />                                       
                            <input type="date" class="form-control" id="tkhTamat">
                        </div>
                        
                                                                  
                    </div>   
                </div> 

                <div class="col-md-12">                 
                    <div class="form-row">
                         <div class="form-group col-md-4">
                            <label for="kodModul">Tujuan Pendahuluan/Nama Program</label><br />
                            <textarea rows="2" cols="30" ID="txtTujuan" class="form-control" MaxLength="500"></textarea>
                        </div>   

                        <div class="form-group col-md-4">
                            <label for="date-input2">Tempat Program Diadakan</label> <br />  
                            <textarea rows="2" cols="30" ID="txtTempat" class="form-control" MaxLength="500"></textarea>                                      
                        </div>
                                                
                                                                 
                    </div>   
                </div> 

             <div class="col-md-12">                 
                    <div class="form-row">
                         
                        <div class="form-group col-md-8">
                            <label for="date-input2">Sila Nyatakan Mengapa Pembelian Barang/Perkhidmatan Tersebut <br />
                                Tidak Boleh Dibuat Melalui Pesanan Tempatan/Pembekal Tidak Dapat Mengeluarkan Invois</label> <br />  
                            <textarea rows="2" cols="30" ID="txtSebab" class="form-control" MaxLength="500"></textarea>
                        </div>                               
                                                                 
                    </div>   
                </div> 


    </asp:Panel>
        
    <div class="form-row">
                <div class="form-group col-md-12" align="right">                   
                    <button type="button" hidden="hidden" class="btn btn-danger btnSimpan">Simpan</button>
                </div>
            </div>
    </div>


  <%--content Senarai Keperluan--%>
      <br />
    <div id="menu2" class="tab-pane fade" aria-labelledby ="tab-Keperluan" role="tabpanel">
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
                         <button type="button" hidden="hidden" class="btn btn-danger btnSimpan2" id="btnSimpanTblData2">Simpan</button>
                    </div>
                </div>         
        </asp:Panel> 
      
    </div>

 <%--content Pengesahan--%>
      <br />
    <div id="menu3" class="tab-pane fade" aria-labelledby="tab-Pengesahan" role="tabpanel">
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
                <input type="file" id="fileInputSurat" style="width:350px"  hidden="hidden"/><br />
                <input type="button" id="uploadBtnSurat" value="Upload Surat" hidden="hidden" onclick="uploadFileSurat()" />
                    <span id=""><br /></span>
                <span id="uploadSurat" style="display: none;"></span>
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
                        <input type="file" id="fileBajet" hidden="hidden" />
                        <input type="button" id="uploadBtnBajet" hidden="hidden" value="Upload Dokumen" onclick="uploadFileBajet()" />
                          <span id="uploadBajet" style="display: none;"></span>
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
                    <button type="button"  runat="server"  class="btn btn-secondary lbtnSimpan" data-toggle="modal" data-target="#penerimaan">Penerimaan</button>
               </div> 
        </div>
             
        </asp:Panel>
    </div>
 
</div>
</div>
</div>
           
        </div>  <%--tutup modal-content--%>
    </div>  <%--tutup class-document--%>
</div>  <%--tutup modal-idSenarai--%>
 

<div class="modal-footer">   
    <button type="button"  runat="server" id="lbtnSimpan" class="btn btn-secondary">Simpan</button>
</div>

<div id="myPersonal" class="modal fade" role="dialog">
 <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
               
<!-- Modal content-->
<div class="modal-content">
<div class="modal-header">
<button type="button" class="close" data-dismiss="modal"></button>
<h4 class="modal-title">Maklumat Pegawai</h4> 
</div>
<div class="modal-body">
               
    <asp:Panel ID="Panel4" runat="server" >
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
<button type="button" class="btn btn-default tutupInfo" data-dismiss="modal">Close</button>
</div>
</div>

</div>
</div>

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

<script type="text/javascript">
    var isClicked = false;
    var tbl = null
    const dateInput = document.getElementById('tkhMohon');
    document.getElementById("tkhMohon").disabled = true;

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
            $('#modalSenarai').modal('toggle');
        }
        else if (elm == "2") {
            $(".modal-body div").val("");
            $('#modalSenarai').modal('toggle');
        }
    }

    var shouldPop = true;

    var tbl = null;
    var senaraiPemohon = null;

    $(document).ready(function () {

        var bil = 1;

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
                "url": "Penerimaan_WS.asmx/LoadOrderRecordPP",
                type: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                "dataSrc": function (json) {
                    //var data = JSON.parse(json.d);
                    //console.log(data.Payload);
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
                { "data": "Tarikh_Mohon" },
                { "data": "NamaPemohon" },
                { "data": "Tujuan" },
                /*{ "data": "Jum_Mohon"},*/
                {
                    "data": "Jum_Mohon",
                    render: function (data, type, full) {
                        return parseFloat(data).toFixed(2);
                    }

                },
                { "data": "Butiran" }

            ]

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

    $('.btnSearch').click(async function () {
        isClicked = true;
        tbl.ajax.reload();
    })

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
        var jumLulus = $('#totalKt').val()
        $('#txtJumLulus').val(jumLulus);

        console.log($('#jumSemua').val());
        console.log($('#txtJumLulus').val());

        loadRecordPelulus($('#noPermohonan').val())

    });

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
                jumlahMohon: $('#totalKt').val(),
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



    // andling the row click event
    function rowClickHandler(orderDetail) {

        // change .btnSimpan text to Simpan
        $('.btnSimpan').text('Simpan')
        $('.btnSimpan').removeClass('btn-success');
        $('.btnSimpan').addClass('default-primary');
        $('#modalSenarai').modal('toggle');

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
        $(ddl).dropdown('set selected', orderDetail.Kod_Vot);
        selectObj.append("<option value = '" + orderDetail.Kod_Vot + "'>" + orderDetail.Kod_Vot + "</option>")

        $(".Hid-vot-list").html(orderDetail.Kod_Vot);


        var butirptj = $(".label-ptj-list");
        butirptj.html(orderDetail.ButiranPTJ);


        var hidbutirptj = $(".Hid-ptj-list");
        hidbutirptj.html(orderDetail.Ptj);

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
        $('#txtPeruntukan').val(butirptj.html());
    }

    async function clearAllRowsHdr() {
        console.log("1312")
        $('#uploadBajet').html("")
        $('#uploadSurat').html("")
    }

    function getDataPeribadiPemohon(pemohon) {
        //Cara Pertama

        var nostaf = pemohon
        //alert(pemohon)

        fetch('Penerimaan_WS.asmx/GetUserInfo', {
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

<%--         $(document).ready(function () {
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

         });--%>

    $('.btnPapar').click(function () {
        tbl.ajax.reload();
    });

    function getDataPeribadi() {
        //Cara Pertama

        var nostaf = '<%=Session("ssusrID")%>'

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
        var mohonID = $('#noPermohonan').val()
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
                $('#txtPeruntukan').val(recordIDPtj.html());




            },

            apiSettings: {
                url: 'Penerimaan_WS.asmx/GetVotCOA?q={query}',
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

        generateDropdown("ddlBayar", "Penerimaan_WS.asmx/GetKaedahBayar")
        initDropdownCOA("ddlCOA");

        //$('#ddlStaf').dropdown({
        //    fullTextSearch: false,
        //    onChange: function () {   //function bila klik ddlstaf.pilih nama staf then auto load maklumat staf.
        //        getDataPeribadi($(this).val())  //baca value bila pilih nama pada ddlStaf selection
        //    },
        //    apiSettings: {
        //        url: 'Pendahuluan_WS.asmx/fnCariStaf?q={query}',
        //        method: 'POST',
        //        dataType: "json",
        //        contentType: 'application/json; charset=utf-8',
        //        cache: false,
        //        beforeSend: function (settings) {
        //            // Replace {query} placeholder in data with user-entered search term
        //            settings.data = JSON.stringify({ q: settings.urlData.query });
        //            searchQuery = settings.urlData.query;
        //            return settings;
        //        },
        //        onSuccess: function (response) {
        //            // Clear existing dropdown options
        //            var obj = $(this);

        //            var objItem = $(this).find('.menu');
        //            $(objItem).html('');

        //            // Add new options to dropdown
        //            if (response.d.length === 0) {
        //                $(obj.dropdown("clear"));
        //                return false;
        //            }

        //            var listOptions = JSON.parse(response.d);

        //            $.each(listOptions, function (index, option) {
        //                $(objItem).append($('<div class="item" data-value="' + option.StafNo + '">').html(option.MS01_Nama));
        //            });

        //            // Refresh dropdown
        //            $(obj).dropdown('refresh');

        //            if (shouldPop === true) {
        //                $(obj).dropdown('show');

        //            }

        //        }
        //    }
        //});

    });

    //event bila klik button simpan  
    $('.btnSimpan').click(async function () {
        var jumRecord = 0;
        var acceptedRecord = 0;
        var msg = "";
        var newPelbagai = {
            OtherList: {
                mohonID: $('#noPermohonan').val(),
                stafID: '<%=Session("ssusrID")%>',
                     NoTel: $('#txtTel').val(),
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

            const response = await fetch('Penerimaan_WS.asmx/LoadListingKeperluan', {
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
        $('#totalKt').val(parseFloat(result.Payload.Jumlah).toFixes(2));
        console.log($('#totalKt').val(result.Payload.Jumlah))
        close_loader();
    });

    //async function saveRecordItem(keperluan) {

    //    return new Promise((resolve, reject) => {
    //        $.ajax({
    //            url: 'Pelbagai_WS.asmx/SaveRecordItem',
    //            method: 'POST',
    //            data: JSON.stringify(keperluan),
    //            dataType: 'json',
    //            contentType: 'application/json; charset=utf-8',
    //            success: function (data) {
    //                resolve(data.d);
    //                //alert(resolve(data.d));
    //            },
    //            error: function (xhr, textStatus, errorThrown) {
    //                console.error('Error:', errorThrown);
    //                reject(false);
    //            }

    //        });
    //    })
    //}

    //async function ajaxSaveRecord(pelbagai) {

    //    return new Promise((resolve, reject) => {
    //        $.ajax({
    //            url: 'Pelbagai_WS.asmx/SaveRecordPelbagai',
    //            method: 'POST',
    //            data: JSON.stringify(pelbagai),
    //            dataType: 'json',
    //            contentType: 'application/json; charset=utf-8',
    //            success: function (data) {
    //                resolve(data.d);
    //                //alert(resolve(data.d));
    //            },
    //            error: function (xhr, textStatus, errorThrown) {
    //                console.error('Error:', errorThrown);
    //                reject(false);
    //            }

    //        });
    //    })

    //}

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
        
        if (id !== "") {
             //BACA Data Surat
            $('#uploadBajet').html("")
            $('#uploadSurat').html("")
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

            const response = await fetch('Penerimaan_WS.asmx/LoadDataSurat', {
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

            const response = await fetch('Penerimaan_WS.asmx/LoadDataBajet', {
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
        console.log("masuk tabkeperluan")
        console.log(id)
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

            const response = await fetch('Penerimaan_WS.asmx/LoadListingKeperluan', {
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

    //function uploadFileSurat() {
    //    alert("masuk upload surat")
    //    var fileInputSurat = document.getElementById("fileInputSurat");
    //    var file = fileInputSurat.files[0];

    //    if (file) {
    //        var fileSize = file.size; // File size in bytes
    //        var maxSize = 3 * 1024 * 1024; // Maximum size in bytes (3MB)

    //        if (fileSize <= maxSize) {
    //            // File size is within the allowed limit

    //            var fileName = file.name;
    //            var fileExtension = fileName.split('.').pop().toLowerCase();

    //            // Check if the file extension is PDF or Excel
    //            if (fileExtension === 'pdf' || fileExtension === 'xlsx' || fileExtension === 'xls') {
    //                var reader = new FileReader();
    //                reader.onload = function (e) {



    //                    var fileData = e.target.result; // Base64 string representation of the file data
    //                    var fileName = file.name;

    //                    var requestData = {
    //                        fileData: "test",
    //                        fileName: fileName,
    //                        resolvedUrl: resolveAppUrl("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/")
    //                    };

    //                    var frmData = new FormData();

    //                    frmData.append("fileSurat", $('input[id="fileInputSurat"]').get(0).files[0]);
    //                    frmData.append("fileName", fileName);
    //                    frmData.append("fileSize", fileSize);

    //                    $('#hidFolder').val(fileExtension);
    //                    $('#hidFileName').val(fileName);


    //                    $.ajax({
    //                        url: "Pelbagai_WS.asmx/UploadFileSurat",
    //                        type: 'POST',
    //                        data: frmData,
    //                        cache: false,
    //                        contentType: false,
    //                        processData: false,
    //                        success: function (response) {
    //                            // Show the uploaded file name on the screen
    //                            // $("#uploadedFileNameLabel").text(fileName);

    //                            var fileLink = document.createElement("a");
    //                            fileLink.href = requestData.resolvedUrl + fileName;
    //                            fileLink.textContent = fileName;

    //                            var uploadedFileNameLabel = document.getElementById("uploadSurat");
    //                            uploadedFileNameLabel.appendChild(fileLink);


    //                            $("#uploadSurat").show();
    //                            // Clear the file input
    //                            $("#fileInputSurat").val("");

    //                            $("#progressContainer").text("File uploaded successfully.");
    //                        },
    //                        error: function () {
    //                            $("#progressContainer").text("Error uploading file.");
    //                        }
    //                    });
    //                };

    //                reader.readAsArrayBuffer(file);
    //            } else {
    //                // Invalid file type
    //                alert("Only PDF and Excel files are allowed.");
    //            }
    //        } else {
    //            // File size exceeds the allowed limit
    //            alert("File size exceeds the maximum limit of 3MB");
    //        }
    //    } else {
    //        // No file selected
    //        alert("Please select a file to upload");
    //    }
    //}



    function resolveAppUrl(relativeUrl) {
        // Make a separate AJAX request to the server to resolve the URL
        var resolvedUrl = "";
        $.ajax({
            type: "POST",
            //url: "Penerimaan_WS.asmx/GetBaseUrl",
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


    //function uploadFileBajet() {
    //    var fileInputBajet = document.getElementById("fileBajet");
    //    var file = fileInputBajet.files[0];

    //    if (file) {
    //        var fileSize = file.size; // File size in bytes
    //        var maxSize = 3 * 1024 * 1024; // Maximum size in bytes (3MB)

    //        if (fileSize <= maxSize) {
    //            // File size is within the allowed limit

    //            var fileName = file.name;
    //            var fileExtension = fileName.split('.').pop().toLowerCase();

    //            // Check if the file extension is PDF or Excel
    //            if (fileExtension === 'pdf' || fileExtension === 'xlsx' || fileExtension === 'xls') {
    //                var reader = new FileReader();
    //                reader.onload = function (e) {



    //                    var fileData = e.target.result; // Base64 string representation of the file data
    //                    var fileName = file.name;

    //                    var requestData = {
    //                        fileData: "test",
    //                        fileName: fileName,
    //                        resolvedUrl: resolveAppUrl("~/UPLOAD/DOCUMENT/PENDAHULUAN DAN TUNTUTAN/PP/")
    //                    };

    //                    var frmData = new FormData();

    //                    frmData.append("fileSurat", $('input[id="fileBajet"]').get(0).files[0]);
    //                    frmData.append("fileName", fileName);
    //                    frmData.append("fileSize", fileSize);

    //                    $('#hidFolderBajet').val(fileExtension);
    //                    $('#hidFileBajet').val(fileName);


    //                    $.ajax({
    //                        url: "Pelbagai_WS.asmx/UploadFileBajet",
    //                        type: 'POST',
    //                        data: frmData,
    //                        cache: false,
    //                        contentType: false,
    //                        processData: false,
    //                        success: function (response) {
    //                            // Show the uploaded file name on the screen
    //                            // $("#uploadedFileNameLabel").text(fileName);

    //                            var fileLink = document.createElement("a");
    //                            fileLink.href = requestData.resolvedUrl + fileName;
    //                            fileLink.textContent = fileName;

    //                            var uploadedFileNameLabel = document.getElementById("uploadBajet");
    //                            uploadedFileNameLabel.appendChild(fileLink);


    //                            $("#uploadBajet").show();
    //                            // Clear the file input
    //                            $("#fileBajet").val("");

    //                            $("#progressContainer2").text("File uploaded successfully.");
    //                        },
    //                        error: function () {
    //                            $("#progressContainer2").text("Error uploading file.");
    //                        }
    //                    });
    //                };

    //                reader.readAsArrayBuffer(file);
    //            } else {
    //                // Invalid file type
    //                alert("Only PDF and Excel files are allowed.");
    //            }
    //        } else {
    //            // File size exceeds the allowed limit
    //            alert("File size exceeds the maximum limit of 3MB");
    //        }
    //    } else {
    //        // No file selected
    //        alert("Please select a file to upload");
    //    }
    //}


    $('.btnHantar').click(async function () {
        show_loader();
        var msg = "";
        var mohonID = $('#noPermohonan').val()
        var check_surat = $('#hidFileName').val()
        var check_suratBajet = $('#hidFileBajet').val()
        var statusHantar = 1
        var idSurat = "4"
        var idBajet = "5"
        console.log("masuk simpan")

        console.log(mohonID);
        console.log(check_surat);
        console.log(check_suratBajet);
        if (check_surat === "" || check_suratBajet === "") {
            alert("Sila upload dokumen")
        }

        if ($('#chckSah').prop('checked', true)) {
            alert("Sila tick pada kotak perakuan")

        }
        else {
            alert("Sila tick pada kotak perakuan")
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

    $('.lbtnSimpan').click(async function () {
        console.log("657")
        $('#penerimaan').modal('toggle');
        $('#modalSenarai').modal('toggle');
        var jumLulus = $('#totalKt').val()
        $('#txtJumLulus').val(jumLulus);

        console.log($('#jumSemua').val());
        console.log($('#txtJumLulus').val());

        loadRecordPelulus($('#noPermohonan').val())

    });
   

   


    function SaveSucces() {
        $('#MessageModal').modal('toggle');

    }



</script>

</asp:Content>
