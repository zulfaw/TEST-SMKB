<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Master_Gaji.aspx.vb" Inherits="SMKB_Web_Portal.Master_Gaji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
<div id="PermohonanTab" class="tabcontent" style="display: block">
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

        <div class="col-sm-6 col-md-6">
            <div class="card border-white">
                <div class="card-header" style="text-align: left">
                    <label id="lbl1" style="color:blue"></label>
                    <label id="lbl2" style="color:blue"></label>
                     <label id="hidNostaf" style="visibility:hidden"></label>
                    <label id="hidSave" style="visibility:hidden"></label>
                </div>

            </div>
        </div>
        <div class="col-sm-6 col-md-3">
            </div>
         
        <div class="btn btn-primary" style="text-align:right" onclick="ShowPopup(this,'2');">
            <i class="fa fa-plus"></i>Tambah Transaksi              
        </div>
            
        &nbsp;&nbsp; 
        <%-- <asp:LinkButton ID="lbtnKira" runat="server" class="btn btn-primary" OnClientClick="return confirm('Adakah anda pasti untuk kira semula slip gaji rekod ini?');" >
                                    <i class="fa fa-plus"></i>Kiraan Semula Slip Gaji      
                                </asp:LinkButton>&nbsp;&nbsp;
        <div class="btn btn-primary"  onclick="OpenPopUp('View_Slip.aspx','mywin','1050','555','yes','center');return false">
            <i class="fa fa-plus"></i>Papar Slip Gaji              
        </div>--%>
    </div>
       
        <div class="form-row">
             
            <div class="col-md-12">
                
                <div class="transaction-table table-responsive">
                    
                    <table id="tblListMaster" class="table table-striped" style="width:100%">
                        <thead>
                            <tr>
                                <th scope="col" style="width:50px">Sumber</th>
                                <th scope="col" style="width:100px">Jenis Transaksi</th>
                                <th scope="col" style="width:100px">Kod Transaksi</th>
                                <th scope="col" style="width:100px">Tarikh Mula</th>
                                <th scope="col" style="width:100px">Tarikh Tamat</th>
                                <th scope="col" style="width:100px">Amaun (RM)</th>
                                <th scope="col" style="width:100px">No. Rujukan</th>
                                <th scope="col" style="width:100px">Catatan</th>
                                <th scope="col" style="width:100px">Status</th>
                                <th scope="col" style="width:100px">Tindakan</th>
                            </tr>
                        </thead>
                        <tbody id="tableID_ListMaster">
                                        
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
                    <h5 class="modal-title" id="fMCTitle">Maklumat Staf</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <h6 style="color:blue">Maklumat Butiran Staf</h6>
                        <hr>
        
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <label>No. Staf</label>
                                <asp:TextBox runat="server" ID="txtNoStaf" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>                     
                            </div>

                            <div class="form-group col-md-3">
                                <label>No. KP</label>
                                <asp:TextBox runat="server" ID="txtNoKp" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Gaji Pokok</label>
                                <asp:TextBox runat="server" ID="txtGjPokok" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Status</label>
                                <asp:TextBox runat="server" ID="txtStatus" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <label>Nama</label>
                                <asp:TextBox runat="server" ID="txtNama" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                             
                            </div>

                            <div class="form-group col-md-3">
                                <label>Jawatan</label>
                                <asp:TextBox runat="server" ID="txtJwtn" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Pejabat</label>
                                <asp:TextBox runat="server" ID="txtPjbt" Enabled="false" CssClass="form-control" Style="width: 100%;" ></asp:TextBox>
                                
                            </div>
                           <div class="form-group col-md-3">
                                <label>Taraf</label>
                                <asp:TextBox runat="server" ID="txtTaraf" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <label>Gred Gaji</label>
                                <asp:TextBox runat="server" ID="txtGred" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                             
                            </div>

                            <div class="form-group col-md-3">
                                <label>Tarikh Lantik</label>
                                <asp:TextBox runat="server" ID="txtTkhLantik" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>

                            <div class="form-group col-md-3">
                                <label>Skim</label>
                                <asp:TextBox runat="server" ID="txtSkim" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                           <div class="form-group col-md-3">
                                <label>Pilihan</label>
                                <asp:TextBox runat="server" ID="txtPilihan" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <label>No. Pencen</label>
                                <asp:TextBox runat="server" ID="txtNoPencen" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                             
                            </div>

                            <div class="form-group col-md-3">
                                <label>No. KWSP</label>
                                <asp:TextBox runat="server" ID="txtNoKwsp" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>

                            <div class="form-group col-md-3">
                                <label>No. Perkeso</label>
                                <asp:TextBox runat="server" ID="txtNoPerkeso" CssClass="form-control" Enabled="True" Style="width: 100%;" ></asp:TextBox>
                            </div>
                           <div class="form-group col-md-3">
                                <label>No. Cukai</label>
                                <asp:TextBox runat="server" ID="txtNoCukai" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-3">
                                <label>Bank</label>
                                <asp:TextBox runat="server" ID="txtBank" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                                             
                            </div>

                            <div class="form-group col-md-3">
                                <label>No. Akaun</label>
                                <asp:TextBox runat="server" ID="txtNoAcc" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                            <div class="form-group col-md-3">
                                <label>Umur</label>
                                <asp:TextBox runat="server" ID="txtUmur" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                        </div>
                    <h6 style="color:blue">Maklumat Butiran Gaji</h6>
                        <hr>
        
                        <div class="form-row">
                            <div class="form-group col-md-4">
                                <label>KWSP Pekerja (%)</label>
                                <asp:TextBox runat="server" ID="txtKwspPek" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>                     
                            </div>

                            <div class="form-group col-md-4">
                                <label>KWSP Majikan (%)</label>
                                <asp:TextBox runat="server" ID="txtKwspMaj" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Pencen (%)</label>
                                <asp:TextBox runat="server" ID="txtPencen" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-row">
                           <div class="form-group col-md-4">
                                <label>Kategori Cukai</label>
                                <asp:TextBox runat="server" ID="txtKatCukai" CssClass="form-control" Enabled="false" Style="width: 100%;" ></asp:TextBox>
                            </div>
                            <div class="form-group col-md-4">
                                <label>Kategori Perkeso</label>
                                    <asp:DropDownList ID="ddlKatSocso" runat="server" CssClass="form-control" Width="70px">
                                    </asp:DropDownList>
                                             
                            </div>


                            <div class="form-group col-md-4">
                                <asp:CheckBox ID="chkTahanGaji" runat="server" Text="Tahan Gaji" /> <br />
                                <asp:CheckBox ID="chkByrCek" runat="server" Text="Bayar Gaji(Cek)" />
                            </div>
      
                        </div>
                    <h6 style="color:blue">Kawalan Proses</h6>
                    <hr>

                    <asp:CheckBox ID="chkGaji" Text="Gaji" runat="server"   /><br />
                    <asp:CheckBox ID="chkKwsp" Text="KWSP" runat="server"  /><br />
                    <asp:CheckBox ID="chkPencen" Text="Pencen" runat="server" /><br />
                    <asp:CheckBox ID="chkCukai" Text="Cukai" runat="server" /><br />
                    <asp:CheckBox ID="chkPerkeso" Text="Perkeso" runat="server" /><br />
                    <asp:CheckBox ID="chkBonus" Text="Bonus" runat="server" />
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="$('#tambahgaji').modal('hide'); return false;">Tutup</button>
                    <asp:LinkButton  runat="server" autopostback ="true"  CssClass="btn btn-secondary lbtnSimpanStaf"> 
                                    &nbsp;&nbsp;&nbsp;Simpan </asp:LinkButton>
                                
                </div>
            </div>
        </div>
    </div>
    <!-- End Modal -->

        <!-- Modal -->
        <div class="modal fade" id="tambahgaji" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="eMCTitle">Tambah Transaksi</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                            <h6>Maklumat Transaksi</h6>
                            <hr>
                                    <label>No. Staf : </label>&nbsp;<label id="lblnostaf"></label>&nbsp;&nbsp;&nbsp;
                                    <label>Nama : </label>&nbsp;<label id="lblnama"></label>
                                    <hr>
                                    <div class="form-row">
                                        <div class="form-group col-md-4">
                                            <label>Sumber</label>
                                            <input id="txtSumber" runat="server" type="text" class="form-control" disabled>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Jenis</label>
                                            <asp:DropDownList ID="ddlJenis"  CssClass="form-control searchable-dropdown" data-validator="reqJenis|" runat="server">
                                                        
                                            </asp:DropDownList>      
                                             <asp:RequiredFieldValidator ID="rqdJenis" runat="server" ControlToValidate="ddlJenis" CssClass="text-danger" ErrorMessage="*Sila Masukkan Jenis" ValidationGroup="Semak" Display="Dynamic"/>
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>Kod</label>
                                                <asp:DropDownList ID="ddlKodTrans" CssClass="form-control searchable-dropdown" runat="server">
                                                        
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqdKod" runat="server" ControlToValidate="ddlKodTrans" CssClass="text-danger" ErrorMessage="*Sila Masukkan Kod" ValidationGroup="Semak" Display="Dynamic"/>
                                        </div>
                                    </div>

                                    <div class="form-row">
                                            <div class="form-group col-md-4">
                                                <label>Tarikh Mula</label>
                                                <input id="txtTkhMula" runat="server" type="date" class="form-control" enable="true">
                                                <asp:RequiredFieldValidator ID="rqdTkhMula" runat="server" ControlToValidate="txtTkhMula" CssClass="text-danger" ErrorMessage="*Sila Masukkan Tarikh Mula" ValidationGroup="Semak" Display="Dynamic"/>
                                            </div>

                                            <div class="form-group col-md-4">
                                                <label>Tarikh Tamat</label>
                                                <input id="tkhTamat" runat="server" type="date" class="form-control" enable="true">
                                                <asp:RequiredFieldValidator ID="rqdTkhTmt" runat="server" ControlToValidate="tkhTamat" CssClass="text-danger" ErrorMessage="*Sila Masukkan Tarikh Tamat" ValidationGroup="Semak" Display="Dynamic"/>

                                            </div>
                                        <div class="form-group col-md-4">
                                            <label>Amaun</label> 
                                            <input id="txtamaun" runat="server" type="number" class="form-control" enable="true">
                                            <asp:RequiredFieldValidator ID="rqdAmaun" runat="server" ControlToValidate="txtamaun" CssClass="text-danger" ErrorMessage="*Sila Masukkan Amaun" ValidationGroup="Semak" Display="Dynamic"/>

                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group col-md-4">
                                            <label>No. Rujukan</label>
                                            <input id="txtnoruj" runat="server" type="text" class="form-control" enable="true">
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Catatan</label>
                                            <input id="txtcatatan" runat="server" type="text" class="form-control" enable="true">
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>Status</label>
                                                <asp:DropDownList ID="ddlStatus" CssClass="form-control searchable-dropdown" runat="server">
                                                        <asp:ListItem  Value="">Sila Pilih</asp:ListItem>  
                                                        <asp:ListItem Value="AKTIF">AKTIF</asp:ListItem>  
                                                        <asp:ListItem Value="BATAL">BATAL</asp:ListItem>  
                                                </asp:DropDownList>
                                        </div>
                                    </div>
                                            
                                            

                            <hr>  
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="$('#tambahgaji').modal('hide'); return false;">Tutup</button>
                            <asp:LinkButton  runat="server" autopostback ="true"  CssClass="btn btn-secondary lbtnSimpan" data-validation-group="Semak"> 
                                    &nbsp;&nbsp;&nbsp;Simpan </asp:LinkButton>

                    </div>
                </div>
            </div>
        </div>
    
    <!-- End Modal -->

</div>
<script type="text/javascript">
    var tbl = null

    $(document).ready(function () {

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
                    render: function (data, type, row, meta) {

                        if (type !== "display") {

                            return data;

                        }

                        var link = `<td style="width: 10%" >
                                             <label  name="noStaf"  value="${data}" ><a id="myLink" class="yourButton" href="#" onclick="ShowPopup(this,'1');">${data}</a></label>
                                            <input type ="hidden" class = "noStaf" value="${data}"/>
                                        </td>`;
                        return link;
                    }
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

                        var link = `<button id="btnView" runat="server" class="btn btnView" type="button" style="color: blue" data-dismiss="modal">
                                                    <i class="fa fa-edit"></i>
                                        </button>`;
                        return link;
                    }
                }
            ]

        });

    });
   
    function ShowPopup(obj, elm) {
        if (elm == "1") {

            $('#infostaf').modal('toggle');

            getInfoStaf($(obj).text());
            getInfoProses($(obj).text());

        }
        else if (elm == "2") {
            //$(".modal-body input").val("");
            if ($('#lbl1').text() === "") {
                alert("Sila pilih rekod dari senarai staf diatas")
            }
            else {
                $('#tambahgaji').modal('toggle');
                $('#hidSave').text("1");
                $('#eMCTitle').text("Tambah Transaksi");
                $('#lblnostaf').text($('#lbl1').text());
                $('#lblnama').text($('#lbl2').text());
                $('#<%=txtSumber.ClientID%>').val('GAJI');

                $("#<%=ddlJenis.ClientID%>").prop("disabled", false);
                $('#<%=ddlKodTrans.ClientID%>').val('');
                $('#<%=ddlJenis.ClientID%>').val('');
                $('#<%=txtamaun.ClientID%>').val('');
                $('#<%=txtTkhMula.ClientID%>').val('');
                $('#<%=tkhTamat.ClientID%>').val('');
                $('#<%=txtnoruj.ClientID%>').val('');
                $('#<%=txtcatatan.ClientID%>').val('');
                 $('#<%=ddlStatus.ClientID%>').val('');


                var ddlFruits = document.getElementById("<%=ddlStatus.ClientID %>");
                var selectedText = ddlFruits.options[ddlFruits.selectedIndex[1]].innerHTML;
                var selectedValue = ddlFruits.value;
               // alert("Selected Text: " + selectedText + " Value: " + selectedValue);

             }   

             
        } 
        clear();
    }
    function getInfoDet(obj) {
        alert($(obj).text());


    }
    function getInfoStaf(nostaf) {
        //Cara Pertama
        //alert(nostaf);
        fetch('Transaksi_WS.asmx/LoadRekodStaf', {
            method: 'POST',
            headers: {
                'Content-Type': "application/json"
            },
            //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
            body: JSON.stringify({ nostaf: nostaf })
        })
            .then(response => response.json())
            .then(data => setInfoStaf(data.d))

    }
    function getInfoProses(nostaf) {
        //Cara Pertama
        //alert(nostaf);
        fetch('Transaksi_WS.asmx/LoadProsesStaf', {
            method: 'POST',
            headers: {
                'Content-Type': "application/json"
            },
            //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
            body: JSON.stringify({ nostaf: nostaf })
        })
            .then(response => response.json())
            .then(data => setInfoProsesStaf(data.d))

    }
    function listStatus() {
        $('[id*=ddlStatus]').html('<option value="">Sila Pilih</option>');
        $('[id*=ddlStatus]').html('<option value="A">AKTIF</option>');
        $('[id*=ddlStatus]').html('<option value="B">BATAL</option>');
    }
    function setInfoProsesStaf(data) {
        data = JSON.parse(data);
        if (data.No_Staf === "") {
            alert("Tiada data");
            return false;
        }

        $('#<%=txtKatCukai.ClientID%>').val(data[0].Kategori_Cukai);
        $('#<%=txtNoPerkeso.ClientID%>').val(data[0].No_Perkeso);
        $('#<%=ddlKatSocso.ClientID%>').val(data[0].Kategori_Perkeso);
        
        if (data[0].Proses_Gaji == true) {
          
            $('#<%=chkGaji.ClientID%>').attr('checked', true);
        }
        if (data[0].Proses_Kwsp == true) {

            $('#<%=chkKwsp.ClientID%>').attr('checked', true);
        }
        if (data[0].Proses_Cukai == true) {

            $('#<%=chkCukai.ClientID%>').attr('checked', true);
        }
        if (data[0].Proses_Perkeso == true) {

            $('#<%=chkPerkeso.ClientID%>').attr('checked', true);
        }
        if (data[0].Proses_Pencen == true) {

            $('#<%=chkPencen.ClientID%>').attr('checked', true);
        }

        if (data[0].Tahan_Gaji == true) {

            $('#<%=chkTahanGaji.ClientID%>').attr('checked', true);
        }
        if (data[0].Bayar_Cek == true) {

            $('#<%=chkByrCek.ClientID%>').attr('checked', true);
        }



<%--        $('#<%=chkKwsp.ClientID%>').val(data[0].Proses_Kwsp); 
        $('#<%=chkCukai.ClientID%>').val(data[0].Proses_Cukai); 
        $('#<%=chkPerkeso.ClientID%>').val(data[0].Proses_Perkeso);
        $('#<%=chkPencen.ClientID%>').val(data[0].Proses_Pencen);--%>

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
        $('#<%=txtStatus.ClientID%>').val(data[0].status_staf);
        $('#<%=txtTaraf.ClientID%>').val(data[0].tarafkhidmat);
        $('#<%=txtGjPokok.ClientID%>').val(data[0].jumlahgajis);
        $('#<%=txtNoPencen.ClientID%>').val(data[0].MS01_NoPencen);
        $('#<%=txtNoKwsp.ClientID%>').val(data[0].MS01_NoKWSP);
        $('#<%=txtNoCukai.ClientID%>').val(data[0].MS01_NoCukai);
        $('#<%=txtUmur.ClientID%>').val(data[0].umur);
        $('#<%=txtNoAcc.ClientID%>').val(data[0].MS01_NoAkaun);
        $('#<%=txtTkhLantik.ClientID%>').val(data[0].MS01_TkhKhidmat);
        $('#<%=txtPilihan.ClientID%>').val(data[0].MS01_Pilihan);
        $('#<%=txtBank.ClientID%>').val(data[0].bank);
        

    }
    $('#tblListMaster').on('click', '.btnEdit', function () {

        $('#tambahgaji').modal('toggle');
        $('#hidSave').text("2");
        $('#eMCTitle').text("Kemaskini Transaksi");
        $('#lblnostaf').text($('#lbl1').text());
        $('#lblnama').text($('#lbl2').text());
        
        //GetDropDownData($(this).closest('tr').find('td:eq(1)').text())
   
        $('#<%=txtSumber.ClientID%>').val($(this).closest('tr').find('td:eq(0)').text());
        $('#<%=ddlJenis.ClientID%>').val($(this).closest('tr').find('td:eq(1)').text());  
        $("#<%=ddlJenis.ClientID%>").prop("disabled", true);
        $('#<%=txtamaun.ClientID%>').val($(this).closest('tr').find('td:eq(5)').text().replace(",", ""));   
        $('#<%=txtTkhMula.ClientID%>').val(formatDate($(this).closest('tr').find('td:eq(3)').text()));
        $('#<%=tkhTamat.ClientID%>').val(formatDate($(this).closest('tr').find('td:eq(4)').text()));
        $('#<%=txtnoruj.ClientID%>').val($(this).closest('tr').find('td:eq(6)').text());
        $('#<%=txtcatatan.ClientID%>').val($(this).closest('tr').find('td:eq(7)').text());
        $('#<%=ddlStatus.ClientID%>').val($(this).closest('tr').find('td:eq(8)').text());
        GetDropDownData($('#<%=ddlJenis.ClientID%>').val(), $(this).closest('tr').find('td:eq(2)').text())

    });
    $('#tblListMaster').on('click', '.btnDelete', function () {

        $('#tambahgaji').modal('toggle');
        $('#hidSave').text("3");
        $('#eMCTitle').text("Hapus Transaksi");
        $('#lblnostaf').text($('#lbl1').text());
        $('#lblnama').text($('#lbl2').text());

        //GetDropDownData($(this).closest('tr').find('td:eq(1)').text())

        $('#<%=txtSumber.ClientID%>').val($(this).closest('tr').find('td:eq(0)').text());
        $('#<%=ddlJenis.ClientID%>').val($(this).closest('tr').find('td:eq(1)').text());
        $("#<%=ddlJenis.ClientID%>").prop("disabled", true);
        $('#<%=txtamaun.ClientID%>').val($(this).closest('tr').find('td:eq(5)').text().replace(",", ""));
        $("#<%=txtamaun.ClientID%>").prop("disabled", true);
        $('#<%=txtTkhMula.ClientID%>').val(formatDate($(this).closest('tr').find('td:eq(3)').text()));
        $("#<%=txtTkhMula.ClientID%>").prop("disabled", true);
        $('#<%=tkhTamat.ClientID%>').val(formatDate($(this).closest('tr').find('td:eq(4)').text()));
        $("#<%=tkhTamat.ClientID%>").prop("disabled", true);
        $('#<%=txtnoruj.ClientID%>').val($(this).closest('tr').find('td:eq(6)').text());
        $("#<%=txtnoruj.ClientID%>").prop("disabled", true);
        $('#<%=txtcatatan.ClientID%>').val($(this).closest('tr').find('td:eq(7)').text());
        $("#<%=txtcatatan.ClientID%>").prop("disabled", true);
        $('#<%=ddlStatus.ClientID%>').val($(this).closest('tr').find('td:eq(8)').text());
        $("#<%=ddlStatus.ClientID%>").prop("disabled", true);
        $('#<%=ddlKodTrans.ClientID%>').val($(this).closest('tr').find('td:eq(2)').text());
        $("#<%=ddlKodTrans.ClientID%>").prop("disabled", true);
        GetDropDownData($('#<%=ddlJenis.ClientID%>').val(), $(this).closest('tr').find('td:eq(2)').text())
    });
    function clear() {
        $('#<%=txtSumber.ClientID%>').val('GAJI');
        $("#<%=ddlJenis.ClientID%>").prop("disabled", false);
        $('#<%=ddlKodTrans.ClientID%>').val('');
        $('#<%=ddlJenis.ClientID%>').val('');
        $('#<%=txtamaun.ClientID%>').val('');
        $('#<%=txtTkhMula.ClientID%>').val('');
        $('#<%=tkhTamat.ClientID%>').val('');
        $('#<%=txtnoruj.ClientID%>').val('');
        $('#<%=txtcatatan.ClientID%>').val('');
        $('#<%=ddlStatus.ClientID%>').val('');

    }

    function formatDate(tkh) {
        var date1 = tkh.split('/')
        var newDate = date1[2] + '-' + date1[1] + '-' + date1[0];
        return newDate;
       // var date = new Date(newDate);
       // alert(newDate);
    }
    $('#tblListStaf').on('click', '.btnView', function () {
        
        //var val = $(this).closest('tr').find('td:eq(3)').text(); // amend the index as needed
        var curTR = $(this).closest("tr");
        var recordID = curTR.find("td > .noStaf");
        //var bool = true;
        var id = recordID.val();
        $('#lbl1').text(id);
        $('#hidNostaf').text(id);
        $('#lbl2').text($(this).closest('tr').find('td:eq(1)').text());

        ListMaster(id);
        //var data = table.row($(this).parents('tr')).data();

        //alert(data[0] + "'s salary is: " + data[5]);


        
        //$("div.header").html('Charges list');
    });
    function ListMaster(vNostaf) {

        tbl = $("#tblListMaster").DataTable({
            "bDestroy": true,
            "responsive": true,
            "searching": true,
            "bLengthChange": false,
            "aaSorting": [],
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
                "url": "Transaksi_WS.asmx/LoadListMaster",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: function () {
                    return JSON.stringify({ nostaf: vNostaf })
                },
                "dataSrc": function (json) {
                    //var data = JSON.parse(json.d);
                    //console.log(data.Payload);
                    return JSON.parse(json.d);
                }
            },
            "columns": [
                { "data": "Kod_Sumber", className: "text-center" },
                { "data": "Jenis_Trans", className: "text-center" },
                { "data": "Kod_Trans", className: "text-center" },
                { "data": "Tkh_Mula", className: "text-center" },
                { "data": "Tkh_Tamat", className: "text-center" },
                { "data": "Amaun", className: "text-right", render: $.fn.dataTable.render.number(',', '.', 2, '') },
                { "data": "no_trans" },
                { "data": "catatan" },
                { "data": "status", className: "text-center" },
                {
                    //className: "btnEdit",
                    "data": "no_staf",
                    render: function (data, type, row, meta) {

                        var btnkuar;
                        var btnedit;
                        btnedit = `<button id="btnEdit" runat="server" class="btn btnEdit" type="button" style="color: blue" data-dismiss="modal">
                                                    <i class="fa fa-edit"></i> </button>`;


                        if (row.Kod_Sumber === 'ROC') {
                            btnkuar = btnedit;
                        }
                        else {
                            if (row.Jenis_Trans === 'K' || row.Jenis_Trans === 'S' || row.Jenis_Trans === 'N' || row.Jenis_Trans === 'T') {
                                btnkuar = '';
                            }
                            else {
                               
                                btnkuar = btnedit + `<button id="btnDelete" runat="server" class="btn btnDelete" type="button" style="color: blue" data-dismiss="modal">
                                                    <i class="fa fa-trash" style="color:red"></i>
                                        </button>`;
                            }
                            
                        };

                        return btnkuar;
                    }



                }
            ]

        });
    }
    $(document).ready(function () {

        $.ajax({

            "url": "Transaksi_WS.asmx/GetPerkeso",
            method: 'POST',
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            success: function (data) {

                var json = JSON.parse(data.d);

                var option = json.map(x => "<option value='" + x.kod + "'>" + x.butiran + "</option>");

                $('[id*=ddlKatSocso]').html('<option value="">Sila Pilih</option>');
                $('[id*=ddlKatSocso]').append(option.join(' '));

            }

        });

    });
    $(document).ready(function () {

        $.ajax({

            "url": "Transaksi_WS.asmx/GetJenisTrans",
            method: 'POST',
            "contentType": "application/json; charset=utf-8",
            "dataType": "json",
            success: function (data) {

                var json = JSON.parse(data.d);

                var option = json.map(x => "<option value='" + x.Jenis_Trans + "'>" + x.Butiran + "</option>");

                $('[id*=ddlJenis]').html('<option value="">Sila Pilih</option>');
                $('[id*=ddlJenis]').append(option.join(' '));

            }

        });

        $("#<%=ddlJenis.ClientID%>").on("change", function () {
            var country = $("#<%=ddlJenis.ClientID%>").val();
            //alert(country)

            GetDropDownData(country)
            
            
        });  

    });

    function GetDropDownData(jenis, val) {
        $.ajax({
            type: "POST",
            url: "Transaksi_WS.asmx/GetKodTrans",
            data: { 'jenis': jenis },
            data: "{'jenis': '" + jenis + "'}",
            /*data: '{jenis: "P" }',*/
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data)
        {
                var json = JSON.parse(data.d);

                var option = json.map(x => "<option value='" + x.Kod_Trans + "'>" + x.Butiran + "</option>");

                $('[id*=ddlKodTrans]').html('<option value="">Sila Pilih</option>');
                $('[id*=ddlKodTrans]').append(option.join(' '));
                if (val !== null) {     
                    $('#<%=ddlKodTrans.ClientID%>').val(val);
                }
        },
        failure: function () {
            alert("Failed!");
        }
    });
    }


    $('.lbtnSimpan').click(async function (evt) {
        evt.preventDefault();
        
        var msg = "";
        var isave = $('#hidSave').text();

        var result = await performCheck("Semak");

        if (result === false) {
            return false;
        }

        var newMaster = {
            DataMaster: {
                   // Kod_PTJ: $('#<%'=hPTJ.ClientID%>').val(),
                    No_Staf: $('#lbl1').text(),
                    Kod_Sumber: $('#<%=txtSumber.ClientID%>').val(),
                    Jenis_Trans: $('#<%=ddlJenis.ClientID%>').val(),
                    Kod_Trans: $('#<%=ddlKodTrans.ClientID%>').val(),
                    Tkh_Mula_Trans: $('#<%=txtTkhMula.ClientID%>').val(),
                    Tkh_Tamat_Trans: $('#<%=tkhTamat.ClientID%>').val(),
                AmaunTrans: $('#<%=txtamaun.ClientID%>').val(),
                No_Trans: $('#<%=txtnoruj.ClientID%>').val(),
                Catatan: $('#<%=txtcatatan.ClientID%>').val(),
                Sta_Trans: $('#<%=ddlStatus.ClientID%>').val(),


            }
        }



        if (isave === "1")
        {
            //console.log(newMaster);
            msg = "Anda pasti ingin menyimpan rekod ini?"

            if (!confirm(msg) && result === true) {
                return false;
            }
            var response = await ajaxSaveMaster(newMaster);
        }
        else if (isave === "2")
        {
            //console.log(newMaster);
            msg = "Anda pasti ingin mengemaskini rekod ini?"

            if (!confirm(msg) && result === true) {
                return false;
            }
            var response = await ajaxUpdateMaster(newMaster);
        }
        else if (isave === "3") {
            //console.log(newMaster);
            msg = "Anda pasti ingin menghapus rekod ini?"

            if (!confirm(msg) && result === true) {
                return false;
            }
            var response = await ajaxDeleteMaster(newMaster);
        }
        
    });
    async function ajaxUpdateMaster(arahanK) {
        $.ajax({

            url: 'Transaksi_WS.asmx/UpdateMaster',
            method: 'POST',
            data: JSON.stringify(arahanK),

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var response = JSON.parse(data.d)
                console.log(response);
                alert(response.Message);
                var payload = response.Payload;
                $("#<%=txtNoStaf.ClientID%>").val(payload.No_Staf);
                $('#lbl1').text(payload.No_Staf);
                ListMaster(payload.No_Staf);

            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error:', errorThrown);
                reject(false);
            }
        });
    }
    async function ajaxDeleteMaster(arahanK) {
        $.ajax({

            url: 'Transaksi_WS.asmx/DeleteMaster',
            method: 'POST',
            data: JSON.stringify(arahanK),

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var response = JSON.parse(data.d)
                console.log(response);
                alert(response.Message);
                var payload = response.Payload;
                $("#<%=txtNoStaf.ClientID%>").val(payload.No_Staf);
                $('#lbl1').text(payload.No_Staf);
                ListMaster(payload.No_Staf);

            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error:', errorThrown);
                reject(false);
            }
        });
        }
    async function ajaxSaveMaster(arahanK) {
        $.ajax({

            url: 'Transaksi_WS.asmx/SimpanMaster',
            method: 'POST',
            data: JSON.stringify(arahanK),

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var response = JSON.parse(data.d)
                console.log(response);
                alert(response.Message);
                var payload = response.Payload;
                $("#<%=txtNoStaf.ClientID%>").val(payload.No_Staf);

            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error:', errorThrown);
                reject(false);
            }

        });

         //})

    }

    $('.lbtnSimpanStaf').click(async function (evt) {
        evt.preventDefault();
        var msg = "";
        //var isave = $('#hidSave').text();
        //var result = await performCheck("Semak");

        //if (result === false) {
        //    return false;
        //}

        var newStaf = {
            DataStaf: {
                   // Kod_PTJ: $('#<%'=hPTJ.ClientID%>').val(),
                No_Staf: $('#<%=txtNoStaf.ClientID%>').val(),
                Kat_Perkeso: $('#<%=ddlKatSocso.ClientID%>').val(),
                No_Perkeso: $('#<%=txtNoPerkeso.ClientID%>').val(),
                Proses_Gaji: $('#<%=chkGaji.ClientID%>').val(),
                Proses_Pencen: $('#<%=chkPencen.ClientID%>').val(),
                Proses_Kwsp: $('#<%=chkKwsp.ClientID%>').val(),
                Proses_Cukai: $('#<%=chkCukai.ClientID%>').val(),
                Proses_Perkeso: $('#<%=chkPerkeso.ClientID%>').val(),
                Proses_Bonus: $('#<%=chkBonus.ClientID%>').val(),
                Tahan_Gaji: $('#<%=chkTahanGaji.ClientID%>').val(),
                Bayar_Cek: $('#<%=chkByrCek.ClientID%>').val(),

                }
            }
        //console.log(newMaster);
                msg = "Anda pasti ingin menyimpan rekod ini?"

                if (!confirm(msg) && result === true) {
                    return false;
                }
        var response = await ajaxSaveStaf(newStaf);

    });
    async function ajaxSaveStaf(vStaf) {
        $.ajax({

            url: 'Transaksi_WS.asmx/SimpanStaf',
            method: 'POST',
            data: JSON.stringify(vStaf),

            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                var response = JSON.parse(data.d)
                console.log(response);
                alert(response.Message);
                var payload = response.Payload;
                $("#<%=txtNoStaf.ClientID%>").val(payload.No_Staf);

            },
            error: function (xhr, textStatus, errorThrown) {
                console.error('Error:', errorThrown);
                reject(false);
            }

        });

        //})

    }
    function clearValidation(){

        $("[id*=rqdJenis]").css("display", "none");
        $("[id*=rqdKod]").css("display", "none");
    }
    async function performCheck(e) {
        //alert(e);
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


</script>
</asp:Content>
