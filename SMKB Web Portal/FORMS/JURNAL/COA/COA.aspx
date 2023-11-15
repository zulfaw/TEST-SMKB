<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="COA.aspx.vb" Inherits="SMKB_Web_Portal.COA" %>

<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">



    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
   <%-- <link type="text/css" rel="stylesheet" href="../../../Scripts/bootstrap 3.3.7/bootstrap.min.js" />--%>
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>




    <%-- <script type="text/javascript">
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

            $(".Vot").DataTable({
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

            if (elm == "1") {

                $('#permohonan').modal('toggle');


            }
            else if (elm == "2") {

                $(".modal-body div").val("");
                $('#permohonan').modal('toggle');



            }

        }


    </script>

    <style type="text/css">
        .hideGridColumn {
            display: none;
        }

        .dataTables_wrapper .dataTables_paginate .paginate_button {
            padding: 7px;
            margin-left: 5px;
            display: inline;
            border: 1px;
        }

            .dataTables_wrapper .dataTables_paginate .paginate_button:hover {
                border: 1px;
            }

        .table-responsive {
            display: inline-Table !important;
            width: 98%;
        }
    </style>
   
    <div id="PermohonanTab" class="tabcontent" style="display: block">
        <div class="search-filter">
            <div class="form-row justify-content-center">
                <div class="form-group row col-md-6">
                    <label for="inputEmail3" class="col-sm-3 col-form-label">Pusat Tanggungjawab:</label>
                    <div class="col-sm-8">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlCari" runat="server" CssClass="form-control"></asp:DropDownList>

                            <div class="input-group-append">
                                <button id="lbtnCari" runat="server" class="btn btn-outline" type="button">
                                    <i class="fa fa-search"></i>Cari</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row justify-content-md-center">
            <div class="table-responsive">
                <div class="table-title">
                    <h6>Senarai Carta Akaun</h6>
                    <div class="btn btn-primary">
                        <i class="fa fa-plus"></i>
                        <button type="button" runat="server" id="btnSelect" class="btn btn-primary">Tambah Senarai</button>
                    </div>
                </div>
                <%--        <div class="filter-table-function">
                <div class="show-record">
                    <p>Tunjukkan</p>
                    <select class="form-control">
                        <option>5</option>
                        <option>10</option>
                        <option>20</option>
                        <option>50</option>
                    </select>
                    <p>Rekod</p>
                </div>
                <div class="search-form">
                    <i class="las la-search"></i>
                    <input class="form-control" id="myInput" onkeyup="Search_Gridview(this, gvTestClientId)" type="text" placeholder="Cari">
                </div>
            </div>--%>
                <div class="box-body" align="center">
                    <asp:GridView ID="gvKod" CssClass="table table-bordered table-striped grid" Width="100%" runat="server"
                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                        <Columns>
                            <asp:BoundField DataField="Kod_KW" HeaderText="Kod KW" ReadOnly="True" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Kod_KO" HeaderText="Kod Ko" ReadOnly="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Kod_KP" HeaderText="Kod KP" ReadOnly="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Kod_PTJ" HeaderText="Kod PTJ" ReadOnly="true" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Kod_VOT" HeaderText="Kod Vot" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Butir_KW" HeaderText="Kumpulan Wang" ReadOnly="True">
                                <ItemStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Butir_KO" HeaderText="Operasi" ReadOnly="true">
                                <ItemStyle Width="5%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Butir_KP" HeaderText="Projek" ReadOnly="true">
                                <ItemStyle Width="10%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Butir_PTJ" HeaderText="PTJ" ReadOnly="true">
                                <ItemStyle Width="15%" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Kod Vot" ItemStyle-Width="30">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server"
                                        Style="text-decoration: none; font: bold; color: blue"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Kod_VOT") %>' class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" ssClass="btn-xs" ToolTip="Pilih" />
                                </ItemTemplate>
                                <ItemStyle Width="10%" HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="5%" />
                            <asp:TemplateField HeaderText="Kemaskini">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" Text="Edit" ToolTip="Kemaskini">
                              <i class="fa fa-edit"></i></asp:LinkButton>

                                </ItemTemplate>
                                <ItemStyle Width="3%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <!-- Modal -->
        <div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalCenterTitle">Tambah Carta Akaun</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                   
                    <div class="modal-body" >
                     
                        <h6>Maklumat Carta Akaun</h6>
                        <hr>                      
                       
                        <div class="col-md-12">
                            <div class="row">
                                <div class="form-group col-md-4">
                                    <label>Kumpulan Wang </label>
                                    <asp:DropDownList ID="ddlKw" runat="server" CssClass="form-control" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlVot_IndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Operasi </label>
                                    <asp:DropDownList ID="ddlKO" runat="server" CssClass="form-control" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlVot_IndexChanged"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-4">
                                    <label>Projek </label>
                                    <asp:DropDownList ID="ddlKP" runat="server" CssClass="form-control" Width="95%" AutoPostBack="true" OnSelectedIndexChanged="ddlVot_IndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <label>Pusat Tanggungjawab </label>
                                    <asp:DropDownList ID="ddlPTJ" runat="server" CssClass="form-control" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="ddlVot_IndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" id="dvCheck" runat="server">
                                <div class="form-group col-md-6">
                                    <label>Vot </label>
                                    <asp:DropDownList ID="ddlVot" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>
                                </div>
                                <div class="form-group col-md-6">
                                    <label>Status</label>
                                    <asp:RadioButtonList runat="server" ID="rblStatus" RepeatDirection="Horizontal">
                                        <asp:ListItem Selected="True" Value="1">Aktif </asp:ListItem>
                                        <asp:ListItem Value="0"> Tidak Aktif</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                              
                            <div class="row" id="divVot" runat="server">
                                <div class="col-md-12" style="width: 100%; height: auto;">
                                    <asp:GridView ID="gvVot" CssClass="table table-bordered table-striped Vot" Width="100%" runat="server"
                                        AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDataBound="gvVot_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="Kod_Vot" HeaderText="Kod Vot" ReadOnly="True">
                                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Kod Vot" ItemStyle-Width="30">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblKodVot" Text='<%# Eval("Kod_Vot") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" HorizontalAlign="left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Butiran" HeaderText="Butiran" ReadOnly="true">
                                                <ItemStyle Width="80%" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <%--<asp:TemplateField HeaderText="Status" ItemStyle-Width="30" HeaderStyle-CssClass="hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblStatus" Text='<%# Eval("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" HorizontalAlign="left" />
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Pilih">
                                                <EditItemTemplate>
                                                    <asp:CheckBox ID="chkBox" runat="server" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkBox" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10%" HorizontalAlign="center" />
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
     
                    </div>

                     

                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                        <button type="button" runat="server" id="lbtnSimpan" class="btn btn-secondary">Simpan</button>
                        <button type="button" runat="server" id="lbtnKemaskini" class="btn btn-secondary">Simpan</button>

                    </div>
                        
    </div>
    </div>
        </div>

        <div class="modal fade" id="MessageModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Tolong Sahkan?</h5>
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
             
</asp:Content>
