<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="TahapPengguna.aspx.vb" Inherits="SMKB_Web_Portal.TahapPengguna" %>

<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>
<%--    <script type="text/javascript">
        $(function () {
            $("[id*=ddlCariTahapDT]").select2();
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $("[id*=ddlTahapDT]").select2();
        });
    </script>--%>

    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
   <%-- <link type="text/css" rel="stylesheet" href="../../../Scripts/bootstrap 3.3.7/bootstrap.min.js" />--%>
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>

 
   
  
<%--    <script type="text/javascript">
        var gvTestClientId = '<% =gvkod.ClientID %>';
    </script>--%>

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

            }
            else if (elm == "2") {

                $(".modal-body input").val("");
                $('#permohonan').modal('toggle');
            }
            else if (elm == "3") {

                $(".modal-body input").val("");
                $('#viewDetail').modal('toggle');
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
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                <div class="search-filter">
                        <div class="form-row justify-content-center">
                            <div class="form-group row col-md-6">
<%--                                <label for="inputEmail3" class="col-sm-3 col-form-label">Tahap:</label>
                                <div class="col-sm-8">
                                    <div id="divautocomplete" class="tt-menu" style="display:none">
                                        <div class="input-group">
                                        </div>
                                    </div>
                                    <div class="input-group">
                                               <asp:DropDownList ID="ddlCariTahapDT" runat="server" CssClass="form-control"  EnableFilterSearch="true" FilterType="StartsWith"></asp:DropDownList>
                                        <div class="input-group-append">
                                            <button id="lbtnCari" runat="server" class="btn btn-outline" type="button"><i
                                            class="fa fa-search"></i>Cari</button>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>

            <div class="table-title">
                <h6>Senarai Pengguna Sistem</h6>
<%--                <div class="btn btn-primary" onclick="ShowPopup('2')">
                    <i class="fa fa-plus"></i>Tambah Senarai              
                </div>--%>

            </div>
       
            <div class="box-body" align="center">
                 <asp:GridView ID="gvTahapDT" CssClass="table table-bordered table-striped grid" Width="100%" runat="server"
                     AutoGenerateColumns="false" ShowHeaderWhenEmpty ="true">
                    <Columns>
                        <asp:BoundField DataField="Bil" HeaderText="Bil" HeaderStyle-HorizontalAlign="Center"> 
                            <ItemStyle Width="3%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="No_Staf" HeaderText="No Staf" HeaderStyle-HorizontalAlign="Center" > 
                            <ItemStyle Width="8%" HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Nama" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:LinkButton runat="server"
                                    Style="text-decoration: none; font: bold; color: blue"
                                    Text='<%# DataBinder.Eval(Container.DataItem, "ms01_nama") %>' class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="ShowDetail" ssClass="btn-xs" ToolTip="Pilih" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="pejabat" HeaderText="Pejabat" >
                            <ItemStyle Width="25%" HorizontalAlign="left" />
                        </asp:BoundField>
                       
<%--                        <asp:TemplateField HeaderText="Tindakan">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" Text="Edit" ToolTip="Tindakan untuk Kemaskini atau Hapus rekod.">
                                <i class="las la-edit"></i></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>

            </div>

            <!-- Modal Detail-->
            <div class="modal fade" id="viewDetail" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="detailCenter">Senarai Tahap Pengguna                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">
                            <div class="form-group row col-sm-12">
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Nama Pengguna </label>
                                            <asp:Label ID="lblNamaPengguna" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            <asp:GridView ID="gvDetail" CssClass="table table-bordered table-striped grid2" Width="100%" runat="server"
                                 AutoGenerateColumns="false" ShowHeaderWhenEmpty ="true">
                                <Columns>
                                    <asp:BoundField DataField="Kod_Tahap" HeaderText="Kod" HeaderStyle-HorizontalAlign="Center"> 
                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Jen_Tahap" HeaderText="Jenis Tahap" ItemStyle-Width="15%" />
                                    <asp:BoundField DataField="Butiran" HeaderText="Tahap" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="10%" />
                                </Columns>
                            </asp:GridView>                            
                        </div>

                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Modal -->
            <%--<div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-s" role="document">
                     <asp:UpdatePanel ID="UpdatePanel1" runat="server"  ChildrenAsTriggers="true" >
                                   <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Tambah Maklumat Pengguna                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">

                            <h6>Maklumat Pengguna</h6>
                            <hr>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Tahap</label>
                                            <asp:DropDownList ID="ddlTahapDT" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>No Staf</label>
                                            <input id="txtNoStaf" runat="server" type="text" class="form-control" readonly>
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label>Nama</label>
                                            <asp:DropDownList ID="ddlPenggunaSis" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPenggunaSis_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Pejabat</label>
                                            <input id="txtPejabat" runat="server" type="text" class="form-control" readonly>
                                        </div>
                                    </div>
                                    <div class="row">
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
                    </ContentTemplate>
                              <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlPenggunaSis" EventName="SelectedIndexChanged"  />
                              </Triggers>
                         </asp:UpdatePanel>
                </div>
            </div>--%>

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
