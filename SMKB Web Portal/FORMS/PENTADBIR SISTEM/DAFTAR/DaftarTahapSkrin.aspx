<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="DaftarTahapSkrin.aspx.vb" Inherits="SMKB_Web_Portal.DaftarTahapSkrin" %>

<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">


    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>



    <script type="text/javascript">
        $(function () {
            $(".ui.dropdown").dropdown({
                fullTextSearch: true
            });
        })

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

            }
            else if (elm == "2") {

                $(".modal-body input").val("");
                $('#permohonan').modal('toggle');
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
    </script>

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
    </style>
   

        <div id="PermohonanTab" class="tabcontent" style="display: block">
           
                <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="col-sm-12 col-xl-12 ">
                                <div id="divautocomplete" class="tt-menu" style="display:none">
                                    <div class="input-group">
                                    </div>
                                </div>
                                <div class="input-group justify-content-md-center" style="">
                                    <div class="col-xl-2">
                                        <label for="inputEmail3" class="col-form-label">Tahap :</label>
                                    </div>
                                    <div class="col-xl-4">
                                        <asp:DropDownList ID="ddlCariTahap" runat="server" DataTextField="Jen_Tahap" DataValueField="Kod_Tahap" EnableFilterSearch="true" FilterType="StartsWith" class="form-control ui search dropdown selection custom-select"></asp:DropDownList>
                                    </div>
                                    <div class="input-group-append col-xl-2">
                                         <button id="lbtnCari" runat="server" class="btn btn-outline" type="button"><i
                                         class="fa fa-search"></i>Cari</button>
                                     </div>
                                </div>    
                            </div>
                        </div>

                <div class="table-title">
                    <h6>Senarai Skrin Mengikut Tahap</h6>
                    <div class="btn btn-primary" onclick="ShowPopup('2')">
                        <i class="fa fa-plus"></i>Skrin              
                    </div>

                </div>

            <div class="box-body" align="center">
       

                 <asp:GridView ID="gvKod" CssClass="table table-bordered table-striped grid" Width="100%" runat="server"
                     AutoGenerateColumns="false" ShowHeaderWhenEmpty ="true">
                    <Columns>
                        <asp:BoundField DataField="Kod_Sub_Menu" HeaderText="Kod Sub Menu" HeaderStyle-HorizontalAlign="Center"> 
                            <ItemStyle Width="10%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Nama Sub Menu" ItemStyle-Width="20">
                            <ItemTemplate>
                                <asp:LinkButton runat="server"
                                    Style="text-decoration: none; font: bold; color: blue"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "Nama_Sub_Menu") %>' class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" ssClass="btn-xs" ToolTip="Pilih" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Tkh_Mula" HeaderText="Tarikh Mula" >
                            <ItemStyle Width="8%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Tkh_Tamat" HeaderText="Tarikh Tamat" ItemStyle-Width="8%" />
                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="10%" />
                        <asp:TemplateField HeaderText="Tindakan">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" Text="Edit" ToolTip="Tindakan untuk Kemaskini atau Hapus rekod.">
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
                <div class="modal-dialog modal-dialog-centered modal-s" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Tambah Skrin                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">

                            <h6>Maklumat Skrin</h6>
                            <hr>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Tahap </label>
                                            <asp:DropDownList ID="ddlTahap" runat="server" DataTextField="Jen_Tahap" DataValueField="Kod_Tahap" EnableFilterSearch="true" FilterType="StartsWith" class="form-control ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                            <%--<asp:DropDownList ID="ddlTahap" runat="server" CssClass="form-control" ></asp:DropDownList>--%>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>Kod Sub Menu</label>
                                            <input id="txtKodSubMenu" runat="server" type="text" class="form-control" enable="true" readonly>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Sub Menu </label>
                                            <asp:DropDownList ID="ddlNamaSubMenu" runat="server" DataTextField="Nama_Sub_Menu" DataValueField="Kod_Sub_Menu" EnableFilterSearch="true" FilterType="StartsWith" class="form-control ui fluid search dropdown selection custom-select"></asp:DropDownList>
                                            <%--<asp:DropDownList ID="ddlNamaSubMenu" runat="server" CssClass="form-control" ></asp:DropDownList>--%>
                                        </div>

                                    </div>
                                    
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>Tarikh Mula</label>
                                            <input id="txtTkhMula" runat="server" type="date" class="form-control" enable="true">
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Tarikh Tamat</label>
                                            <input id="txtTkhTamat" runat="server" type="date" class="form-control" enable="true">
                                        </div>
                                    </div>
                                    <div class="row">                                        
                                        <div class="form-group col-md-6">
                                            <label>Jenis Capaian</label>
                                            <input id="txtJenCapai" runat="server" type="text" class="form-control" enable="true">
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>Status</label>
                                            <asp:RadioButtonList runat="server" ID="rblStatus" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Aktif </asp:ListItem>
                                                <asp:ListItem Value="0"> Tidak Aktif</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                            <button type="button" runat="server" id="lbtnSimpan" class="btn btn-secondary">Simpan</button>

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
    
</asp:Content>
