<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Terima.aspx.vb" Inherits="SMKB_Web_Portal.Terima" %>

<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">

   <%-- <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>--%>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlBankUTeM]").select2();
        });
    </script>

    <%--<link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../mod_function.js"></script>--%>

    <script type="text/javascript">

        $(function () {
            $(".grid").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "responsive": true,
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
                }
            });

        })

        function Search_Gridview(strKey, strGV) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

        function SaveSucces() {
            $('#MessageModal').modal('toggle');

        }

        function ShowPopup(elm) {

            if (elm == "1") {
                $('#permohonan').modal('toggle');
                TableTuntutan();

            }
            else if (elm == "2") {

                $(".modal-body input").val("");
                $('#permohonan').modal('toggle');
            }
            else if (elm == "3") {

                $(".modal-body input").val("");
                $('#terimaBayaran').modal('toggle');
                let Jumsbr = $('#MainContent_FormContents_totalbil').val();
                $('#MainContent_FormContents_txtJumlahSbnr').val(Jumsbr);
                //$('#MainContent_FormContents_txtAmaunBaki').val(Jumsbr);
            }

        }

        $(function () {
            $('#txtSearch').keyup(function () {
                $.ajax({
                    url: "VOT.aspx/GetAutoCompleteData",
                    data: "{'username':'" + $('#txtSearch').val() + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        var val = '<ul id="userlist">';
                        $.map(data.d, function (item) {
                            var itemval = item.split('/')[0];
                            val += '<li class=tt-suggestion>' + itemval + '</li>'
                        })
                        val += '</ul>'
                        $('#divautocomplete').show();
                        $('#divautocomplete').html(val);
                        $('#userlist li').click(function () {
                            $('#txtSearch').val($(this).text());
                            $('#divautocomplete').hide();
                        })
                    },
                    error: function (response) {
                        alert(response.responseText);
                    }
                });
            })
            $(document).mouseup(function (e) {
                var closediv = $("#divautocomplete");
                if (closediv.has(e.target).length == 0) {
                    closediv.hide();
                }
            });
        });

        

        //cara kalau ada pass parameter tambahan, lain nama field
       /* initDropdown("ddlMasterLookup", "smkb_mod_function.aspx/getListMasterLookup?q={query}", {
            value: 'ddlvot',      // specify which column is for data
            name: 'details'      // specify which column is for text
        }, function (settings) {
            //settings.urlData.kodbangunan = document.getElementById("ddlBangunan").value;
            settings.data = JSON.stringify({ q: settings.urlData.query});
            return settings;
        })*/

// Your function to handle the onchange event
        function handleCategoryFilterChange() {
            var selectedItem = $('#categoryFilter').val();
            if (selectedItem == "6") {
                $('#txtTarikhStart').show();
                $('#txtTarikhEnd').show();
                $('#lblMula').show();
                $('#lblTamat').show();

                $('#txtTarikhStart').val("");
                $('#txtTarikhEnd').val("");
            } else if (selectedItem == "7") {
                //alert("masuk7");
                //alert(selectedItem);

                $('#txtTarikhStart').hide();
                $('#txtTarikhEnd').hide();
                $('#lblMula').hide();
                $('#lblTamat').hide();

                $('#MainContent_FormContents_idKodPenghutangInput').show();
                $('#MainContent_FormContents_idKodPenghutangInput').val("");
                $('#MainContent_FormContents_idPenerimaInput').hide();
                $('#MainContent_FormContents_idPenerimaInput').val("");
            } else if (selectedItem == "8") {
                //alert("masuk8");
                //alert(selectedItem);

                $('#txtTarikhStart').hide();
                $('#txtTarikhEnd').hide();
                $('#lblMula').hide();
                $('#lblTamat').hide();

                $('#MainContent_FormContents_idPenerimaInput').show();
                $('#MainContent_FormContents_idPenerimaInput').val("");
                $('#MainContent_FormContents_idKodPenghutangInput').hide();
                $('#MainContent_FormContents_idKodPenghutangInput').val("");
            } else {
                $('#txtTarikhStart').hide();
                $('#txtTarikhEnd').hide();
                $('#lblMula').hide();
                $('#lblTamat').hide();

                $('#MainContent_FormContents_idPenerimaInput').hide();
                $('#MainContent_FormContents_idPenerimaInput').val("");
                $('#MainContent_FormContents_idKodPenghutangInput').hide();
                $('#MainContent_FormContents_idKodPenghutangInput').val("");

                $('#txtTarikhStart').val("");
                $('#txtTarikhEnd').val("");
            }
        }

        function kiraAmaunTerima(){
            let totalAmaun;
            let Jumsbr = $('#MainContent_FormContents_txtJumlahSbnr').val();
            let Amaun = $('#MainContent_FormContents_txtAmaunTerima').val();


           
            // Check if the inputs are valid numbers
            if (isNaN(Amaun)) {
                alert("Please insert number");
                $('#MainContent_FormContents_txtAmaunTerima').val("");
                return false;
            }
          
            // Check if the inputs are valid numbers
            if (Amaun == "") {
                $('#MainContent_FormContents_txtAmaunBaki').val(Jumsbr);

            } else {
                Jumsbr = Jumsbr.replace(",", "");
                totalAmaun = parseFloat(Jumsbr) - parseFloat(Amaun);
                const moneyFormat = totalAmaun.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 });
                $('#MainContent_FormContents_txtAmaunBaki').val(moneyFormat);
            }
           
        }

    </script>

    

<style>
    option[style*="display: none;"] {
        display: none;
    }

    .form-control {
    display: initial;
    width: 100%;
    height: calc(1.5em + 0.75rem + 2px);
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    color: #495057;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid #ced4da;
    border-radius: 0.25rem;
    transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
}
</style>


    <style type="text/css">
        .hideGridColumn {
            display: none;
        }

      .dataTables_wrapper .dataTables_paginate .paginate_button {
            padding : 7px;
            margin-left: 5px;
            display: inline;
            border: 1px;
        }

        .dataTables_wrapper .dataTables_paginate .paginate_button:hover {
            border: 1px;
        }
       
        .ul li
{
list-style: none;
}


        .textinput {
    display: initial;
    width: 100%;
    height: calc(1.5em + 0.75rem + 2px);
    padding: 0.375rem 0.75rem;
    font-size: 14px;
    font-weight: 400;
    line-height: 1.5;
    color: #495057;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid #ced4da;
    border-radius: 0.25rem;
    transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
}

 .form-group.col-sm-2.lblinput {
    margin-right: -6%;
}
  .form-group.col-sm-2.lblinput1 {
    margin-right: -6%;
}

 .row.rowbody {
    padding-bottom: 1%;
}
    </style>

  
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <div class="search-filter">
                <div class="form-row justify-content-center">
                    <div class="form-group row col-md-6">
                        <label for="inputEmail3" class="col-sm-3 col-form-label">Kod Penghutang :</label>
                            <div class="col-sm-8">
                                <div class="input-group">
                                    <select id="categoryFilter" class="custom-select" onchange ="handleCategoryFilterChange()">
                                        <option value="">SEMUA</option>
                                        <option value="1" selected="selected">Hari Ini</option>
                                        <option value="2">Semalam</option>
                                        <option value="3">7 Hari Lepas</option>
                                        <option value="4">30 Hari Lepas</option>
                                        <option value="5">60 Hari Lepas</option>
                                        <option value="6">Pilih Tarikh</option>
                                        <option value="7">Pilih Kod Penghutang</option>
                                        <option value="8">Pilih ID Penerima</option>
                                    </select>
                                        <button id="btnSearch" runat="server" class="btn btnSearch" type="button">
                                            <i class="fa fa-search"></i>
                                        </button>
                                </div>
                            </div>
                            <div class="col-sm-11">
                                    <div class="form-row">
                                        <div class="form-group col-sm-1">
                                            <label id="lblMula" style="text-align: right;display:none;"  >Mula: </label>
                                        </div>
                                        
                                        <div class="form-group col-sm-4">
                                            <input type="date" id="txtTarikhStart" name="txtTarikhStart" style="display:none;" class="form-control date-range-filter">
                                        </div>
                                         <div class="form-group col-sm-1">
                                     
                                        </div>
                                        <div class="form-group col-sm-1">
                                            <label id="lblTamat" style="text-align: right;display:none;" >Tamat: </label>
                                        </div>
                                        <div class="form-group col-sm-4">
                                            <input type="date" id="txtTarikhEnd" name="txtTarikhEnd" style="display:none;" class="form-control date-range-filter">
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-8">
                                    <div class="input-group">
                                        <asp:TextBox ID="idKodPenghutangInput" runat="server" style="display:none;" CssClass="form-control date-range-filter"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-8">
                                    <div class="input-group">
                                        <asp:TextBox ID="idPenerimaInput" runat="server" style="display:none;" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                        <div class="col-sm-8">
                            <div class="input-group">
                                <asp:TextBox ID="txtInput" runat="server" CssClass="form-control" style="display:none;"></asp:TextBox>
                                    <button id="lbtnCari" runat="server" class="btn btn-outline" type="button" style="display:none;"><i
                                class="fa fa-search"></i>Cari</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="table-title">
                    <h6>Senarai Bil</h6>
                </div>

                <div class="box-body" align="center">
                     <asp:GridView ID="gvKod" CssClass="table table-bordered table-striped grid" Width="100%" runat="server"
                         AutoGenerateColumns="false" ShowHeaderWhenEmpty ="true">
                        <Columns>
                            <asp:BoundField DataField="No_Bil" HeaderText="No Bil" HeaderStyle-HorizontalAlign="Center"> 
                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Kod Penghutang" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server"
                                        Style="text-decoration: none; font: bold; color: blue"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Kod_Penghutang") %>' class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" ssClass="btn-xs" ToolTip="Pilih" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="Nama_Penghutang" HeaderText="Nama" >
                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tkh_Mohon" HeaderText="Tarikh Mohon" >
                                <ItemStyle Width="8%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Tujuan" HeaderText="Tujuan" >
                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Jumlah" HeaderText="Jumlah (RM)" >
                                <ItemStyle Width="8%" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Tindakan">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-s" Text="Edit" ToolTip="Tindakan untuk Kemaskini atau Hapus rekod.">
                                    <i class="las la-edit"></i></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="3%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>

                <!-- Modal -->
                <div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
                    aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalCenterTitle">Terimaan Bayaran                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div class="modal-body">
                                  <div class="row rowbody">
                                    <div class="form-group col-sm-2 lblinput">
                                        <label >No Bil</label>
                                    </div>
                                     <div class="form-group col-sm-3"> 
                                            <input id="txtNoBil" runat="server" type="text" class="form-control textinput"  enable="true" readonly>
                                     </div>
                                      <div class="form-group col-sm-2 lblinput1">
                                        <label >Jenis Urusniaga</label>
                                    </div>
                                      <div class="form-group col-sm-4"> 
                                           <input id="txtJenisUrusniaga" runat="server" type="text" class="form-control textinput" enable="true" readonly>
                                        </div>
                                </div>

                                <div class="row rowbody">
                                    <div class="form-group col-sm-2 lblinput">
                                        <label >Kod Penghutang</label>
                                   
                                    </div>
                                      <div class="form-group col-sm-3"> 
                                         <input id="txtKodPenghutang" runat="server" type="text" class="form-control textinput" enable="true" readonly>
                                     </div>
                                     <div class="form-group col-sm-2 lblinput1">
                                         <label class="lblinput">Nama</label>
                                      
                                     </div>
                                     <div class="form-group col-sm-4"> 
                                         <input id="txtNamaPenghutang" runat="server" type="text" class="form-control textinput" enable="true" readonly>
                                     </div>
                                </div>


                                <div class="row rowbody">
                                    <div class="form-group col-sm-2 lblinput">
                                         <label class="lblinput">Tarikh</label>
                                     
                                    </div>
                                    <div class="form-group col-sm-3"> 
                                         <input id="txtTkhMohon" runat="server" type="text" class="form-control textinput" enable="true" readonly>
                                     </div>
                                     <div class="form-group col-sm-2 lblinput1">
                                          <label class="lblinput">Tujuan</label>
                                     </div>
                                     <div class="form-group col-sm-4"> 
                                         <textarea id="txtTujuan" runat="server" class="form-control textinput" rows="3" cols="40" readonly>    </textarea>
                                    <%--    <input id="txtTujuan" runat="server" type="text" class="form-control textinput" enable="true">--%>
                                     </div>
                                </div>

                                 <div class="row rowbody">
                                    <div class="form-group col-sm-2 lblinput">
                                         <label class="lblinput">Jumlah (RM)</label>
                                    
                                    </div>
                                      <div class="form-group col-sm-3"> 
                                         <input id="txtJumlah" runat="server" type="text" class="form-control textinput" enable="true" style="text-align:right" readonly>
                                     </div>
                                </div>
                             </div>

                            <%-- TAMBAH DETAIL BIL DISINI --%>
                        <div>
                            <%--<h6>Transaksi</h6>--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="transaction-table table-responsive">
                                        <table class="table table-bordered table-striped" id="tblData" style="width: 100%;">
                                            <thead>
                                                <tr style="width: 100%; text-align: center">
                                                    <th scope="col" style="width: 5%">No Item</th>
                                                    <th scope="col" style="width: 5%">KW</th>
                                                    <th scope="col" style="width: 5%">PTj</th>
                                                    <th scope="col" style="width: 10%">Vot</th>                                                
                                                    <th scope="col" style="width: 5%">Kod Operasi</th>
                                                    <th scope="col" style="width: 5%">Kod Projek</th>
                                                    <th scope="col" style="width: 20%">Butiran</th>
                                                    <th scope="col" style="width: 7%">Amaun (RM)</th>
                                                    <th scope="col" style="width: 10%">Debit (RM)</th>
                                                    <th scope="col" style="width: 10%">Kredit (RM)</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tableID_trans">                                          

                                            </tbody>
                                        </table>
                                        <table class="table" style="width: 100%; border: none">
                                            <tr style="border-top: none">
                                                <td style="width: 1%; border-top: none"></td>
                                                <td style="width: 20%; border-top: none"></td>
                                                <td style="width: 50%; border-top: none"></td>
                                                <td style="width: 15%; border-top: none"></td>
                                                <td style="width: 2%; border-top: none"></td>
                                                <td style="width: 10%; border-top: none"></td>
                                                <td style="width: 2%; border-top: none"></td>
                                            </tr>
                                            <tr style="border-top: none">
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; font-size: medium;">Jumlah<br />
                                                    <i>( Tolak Diskaun RM
                                                        <input class="underline-input" id="TotalDiskaun" name="TotalDiskaun" style="border: none; width: 20%; font-style: italic" placeholder="0.00" />
                                                        )</i>

                                                </td>
                                                <td></td>
                                                <td style="text-align: right">
                                                    <input class="form-control underline-input" id="totalwoCukai" name="totalwoCukai" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly /></td>
                                                <td></td>
                                            </tr>
                                            <tr style="border-top: none">
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; font-size: medium;">Jumlah Cukai</td>
                                                <td></td>
                                                <td style="text-align: right">
                                                    <input class="form-control underline-input" id="TotalTax" name="TotalTax" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly /></td>
                                                <td></td>
                                            </tr>
                                            <tr style="border-top: none">
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td style="text-align: right; font-size: large">JUMLAH (RM)</td>
                                                <td></td>
                                                <td style="text-align: right">
                                                    <input class="form-control underline-input" id="totalbil" name="totalbil" runat="server" style="text-align: right; font-size: medium; font-weight: bold" placeholder="0.00" readonly /></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-row">
                            <%--<div class="form-group col-md-6">
                                <asp:LinkButton id="lbtnKembali" class="btn btn-primary" runat="server" onclick="lbtnKembali_Click"><i class="las la-angle-left"></i>Kembali</asp:LinkButton>
                            </div>--%>

                        </div>

                            <%-- ---END  DETAIL BIL --%>

                            <div class="modal-footer">
                                <div class="form-group col-md-12" align="right">
                                    <%--<button type="button" class="btn btn-danger" >Padam</button>
                                    <button type="button" class="btn btn-secondary btnSimpan" >Simpan</button>
                                    <input type ="text" id="orderid" value=""/> ORDER ID
                                    <button type="button" class="btn btn-secondary btnLoad" >Load Order Records</button>
                                    <button type="button" class="btn btn-danger btnPadam">Padam</button>--%>
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>                                
                                    <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Cetak Resit">Cetak Resit</button>
      
                                    <button type="button" class="btn btn-success btnTerima" data-toggle="tooltip" data-placement="bottom" title="Terima Bayaran" onclick="ShowPopup('3')">Terima Bayaran</button>
                                </div>


                            
                                <%--<button type="button" runat="server" id="lbtnSimpan" class="btn btn-secondary">Simpan</button>--%>

                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal fade" id="terimaBayaran" tabindex="-1" role="dialog"
                    aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-m" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalCenterTitle1">Terimaan Bayaran                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div class="modal-body">
                                <div class="row rowbody">
                                    <div class="form-group col-sm-4 lblinput">
                                        <label >Jumlah (RM)</label>
                                   
                                    </div>
                                      <div class="form-group col-sm-6"> 
                                         <input id="txtJumlahSbnr" runat="server" type="text" class="form-control textinput" enable="true" readonly style="text-align:right" >
                                     </div>
                                </div>

                                 <div class="row rowbody">
                                     <div class="form-group col-sm-4 lblinput">
                                         <label class="lblinput">Mod Terimaan</label>
                                      
                                     </div>
                                     <div class="form-group col-sm-6"> 
                                          <asp:DropDownList ID="ddlModTerimaan" runat="server" CssClass="form-control"  EnableFilterSearch="true" FilterType="StartsWith"></asp:DropDownList>
                                     </div>
                                </div>
                                 <div class="row rowbody">
                                     <div class="form-group col-sm-4 lblinput">
                                         <label class="lblinput">Bank UTeM</label>
                                      
                                     </div>
                                    <div class="form-group col-sm-6">   
                                        <asp:DropDownList ID="ddlBankUTeM" runat="server" CssClass="form-control"  EnableFilterSearch="true" FilterType="StartsWith"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="row rowbody">
                                     <div class="form-group col-sm-4 lblinput">
                                         <label class="lblinput">Amaun Terima (RM)</label>                                      
                                     </div>
                                     <div class="form-group col-sm-6"> 
                                         <input id="txtAmaunTerima" runat="server" type="text" class="form-control textinput" enable="true" onkeyup="kiraAmaunTerima()" style="text-align:right">
                                     </div>
                                </div>
                                 <div class="row rowbody">
                                     <div class="form-group col-sm-4 lblinput1">
                                         <label class="lblinput">Baki (RM)</label>                                      
                                     </div>
                                     <div class="form-group col-sm-6"> 
                                         <input id="txtAmaunBaki" runat="server" type="text" class="form-control textinput" enable="true" readonly style="text-align:right">
                                     </div>
                                </div>
                             </div>

                    

                        <div class="form-row">
                        </div>

                            <div class="modal-footer">
                                <div class="form-group col-md-12" align="right">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>                                
                                    <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Simpan">Simpan</button>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>



                <div class="modal fade" id="MessageModal" tabindex="-1" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h6 class="modal-title" id="exampleModalLabel">Sistem Maklumat Kewangan Bersepadu</h6>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:Label runat="server" ID="lblModalMessaage" />
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    <script>
        function TableTuntutan() {
           
            var tbl = null
            $(document).ready(function () {

                tbl = $("#tblData").DataTable({
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
                        "url": "TerimaWS.asmx/LoadRecordTuntutan",
                        method: 'POST',
                        "contentType": "application/json; charset=utf-8",
                        "dataType": "json",
                        //data: function (d) {
                        //    return JSON.stringify(d)
                        //},
                        data: function () {
                            return JSON.stringify({ NoBil: $('#<%=txtNoBil.ClientID%>').val() })
                        },
                        "dataSrc": function (json) {
                            return JSON.parse(json.d);
                        }

                    },


                    "columns": [
                        //{
                        //    "data": "MS01_NOSTAF",
                        //    render: function (data, type, row, meta) {

                        //        if (type !== "display") {

                        //            return data;

                        //        }
                        //        var link = `<td style="width: 10%" >
                        //                                <label id="lblNo" name="lblNo" class="lblNo" value="${data}" >${data}</label>
                        //                                <input type ="hidden" class = "lblNo" value="${data}"/>
                        //                            </td>`;
                        //        return link;
                        //    }
                        //},
                        { "data": "No_Item" },
                        { "data": "Kod_Kump_Wang" },
                        { "data": "Kod_PTJ" },
                        { "data": "Kod_Vot" },
                        { "data": "Kod_Operasi" },
                        { "data": "Kod_Projek" },                        
                        { "data": "Perkara" },
                        {
                            "data": "Kadar_Harga",
                            className: "text-right"
                        },
                        {
                            "data": "Debit",
                            className: "text-right"
                        },
                        {
                            className: "text-right",
                            "data": "Kadar_Harga",
                            render: function (data, type, row, meta) {

                                if (type !== "display") {

                                    return data;

                                }
                                var link = `<input type ="text" style="text-align:right;" name="textKredit" value="${data}"/>`;
                                return link;
                                this.width = "5%";
                            }

                        }
                    ]
                });
            });
        }
    </script>


</asp:Content>
