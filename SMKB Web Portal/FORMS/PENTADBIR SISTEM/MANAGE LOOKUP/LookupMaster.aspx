<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="LookupMaster.aspx.vb" Inherits="SMKB_Web_Portal.LookupMaster" %>

<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlModul]").select2();
        });
    </script>

    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>

     <script type="text/javascript">

         $(function () {
            $("#<%=gvKod.ClientID%>").prepend($("<thead>").append($("#<%=gvKod.ClientID%>").find("tr:first"))).DataTable({
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

            $("#<%=gvDetail.ClientID%>").prepend($("<thead></thead>").append($("#<%=gvDetail.ClientID%>").find("tr:first"))).DataTable({
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
           // alert(elm);

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
       
        .ul li{
        list-style: none;
        }

        .custom-dropdown {
        width: 300px; /* Set your desired width here */
        }
    </style>

  
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <div class="search-filter">
            <div class="table-title">
                <h6>Senarai Lookup Master</h6>
                <div class="btn btn-primary" onclick="ShowPopup('2')">
                    <i class="fa fa-plus"></i>Master             
                </div>
            </div>
       
            <div class="box-body" align="center">
                 <asp:GridView ID="gvKod" CssClass="table table-bordered table-striped grid" Width="100%" runat="server"
                     AutoGenerateColumns="false" ShowHeaderWhenEmpty ="true">
                    <Columns>
                        <asp:BoundField DataField="Kod" HeaderText="Kod" HeaderStyle-HorizontalAlign="Center"> 
                            <ItemStyle Width="8%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Butiran" ItemStyle-Width="20">
                            <ItemTemplate>
                                <asp:LinkButton runat="server"
                                    Style="text-decoration: none; font: bold; color: blue" 
                                    Text='<%# DataBinder.Eval(Container.DataItem, "Butiran") %>' class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="ShowDetail" ssClass="btn-xs" ToolTip="Pilih" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Dis_Modul" HeaderText="Modul" >
                            <ItemStyle Width="12%" HorizontalAlign="center" />
                        </asp:BoundField>
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

            <div class="modal fade" id="viewDetail" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="detailCenter">Detail                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">
                            <asp:GridView ID="gvDetail" CssClass="table table-bordered table-striped grid2" Width="100%" runat="server"
                                 AutoGenerateColumns="false" ShowHeaderWhenEmpty ="true">
                                <Columns>
                                    <asp:BoundField DataField="Kod_Detail" HeaderText="Kod" HeaderStyle-HorizontalAlign="Center"> 
                                        <ItemStyle Width="5%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Butiran" HeaderText="Status" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Tarikh_Mula" HeaderText="Tarikh Mula" >
                                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Tarikh_Tamat" HeaderText="Tarikh Tamat" >
                                        <ItemStyle Width="8%" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="10%" />
<%--                                    <asp:TemplateField HeaderText="Tindakan">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" Text="Edit" ToolTip="Tindakan untuk Kemaskini atau Hapus rekod.">
                                            <i class="las la-edit"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                                    </asp:TemplateField>--%>
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
            <div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-s" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Tambah Lookup Master                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">

                            <h6>Maklumat Lookup Master</h6>
                            <hr>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>Kod</label>
                                            <input id="txtKod" runat="server" type="text" class="form-control" enable="true">
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label>Butiran</label>
                                            <input id="txtButiran" runat="server" type="text" class="form-control" enable="true">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-8">
                                            <label>Modul</label>
                                            <div id="divautocomplete" class="col-sm-6" style="display:none">
                                                <div class="input-group">
                                                </div>
                                            </div>
                                            <div class="input-group">
                                                <asp:DropDownList ID="ddlModul" runat="server" CssClass="custom-dropdown"  EnableFilterSearch="true" FilterType="StartsWith"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group col-sm-4">
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
