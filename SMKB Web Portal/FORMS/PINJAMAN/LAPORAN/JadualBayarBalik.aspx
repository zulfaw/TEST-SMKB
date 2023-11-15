<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="JadualBayarBalik.aspx.vb" Inherits="SMKB_Web_Portal.JadualBayarBalik" %>

<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlCariTahap]").select2();
        });
    </script>

    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
   <%-- <link type="text/css" rel="stylesheet" href="../../../Scripts/bootstrap 3.3.7/bootstrap.min.js" />--%>
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>

 
   
<link rel="stylesheet" href="../../../Content/datatables/buttons.dataTables.min.css">

<script src='../../../Scripts/datatables/dataTables.buttons.min.js' ></script>
<script src ='../../../Scripts/datatables/jszip.min.js'></script>
<script src='../../../Scripts/datatables/pdfmake.min.js'></script>
<script src='../../../Scripts/datatables/vfs_fonts.js'></script>
<script src='../../../Scripts/datatables/buttons.html5.min.js'></script>
  
<%--    <script type="text/javascript">
        var gvTestClientId = '<% =gvkod.ClientID %>';
    </script>--%>

    <script type="text/javascript">
        function OpenWindowPB(url, width, height) {
            var options = 'scrollbars=yes,resizable=yes,status=yes,toolbar=no,menubar=no,location=no';
            options += ',width=' + width + 'px,height=' + height + 'px,';
            options += ',screenX=0,screenY=0,top=' + ((screen.height - height) / 2) + ',left=' + ((screen.width - width) / 2);
            var win = window.open(url, '', options);
            win.focus();
        }

        $(function () {
            var table = $(".grid").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend:    'pdfHtml5',
                        text:      '<i class="fa fa-files-o green"></i> PDF',
                        titleAttr: 'PDF',
                        className: 'ui green basic button',
                        filename: "test",
                        orientation: 'portrait',
                        customize: function (doc) {

                            doc.content[1].table.widths =
                                Array(doc.content[1].table.body[0].length + 1).join('*').split('');

                            // Define the custom page header HTML
                            var customHeader = 'Cetakan Jadual';

                            // Set the custom header template
                            var header = {
                                columns: [
                                    {
                                        margin: [200, 20, 0, 0],
                                        width: '*',
                                        text: customHeader
                                    }
                                ]
                            };

                            // Apply the custom header to the document
                            doc.content.splice(0, 0, header);
                        },
                        exportOptions: {
                            columns: ':visible'
                        },
                        action: function (e, dt, button, config) {
                            var counter = 0;
                            $.fn.dataTable.ext.buttons.pdfHtml5.action.call(this, e, dt, button, config);
                        }
                    },
                    'copy', 'csv', 'excel', 'print'

                    //{
                    //    extend: 'pdfHtml5',
                    //    text: 'Export to PDF',
                    //    customize: function (doc) {
                    //            // Define the custom page header HTML
                    //            var customHeader = 'Cetakan Jadual';

                    //            // Set the custom header content as a PDFMake element
                    //            var header = {
                    //                margin: [0, 20, 0, 0],
                    //                layout: 'noBorders',
                    //                table: {
                    //                    widths: ['*'],
                    //                    body: [
                    //                        [customHeader]
                    //                    ]
                    //                }
                    //            };

                    //            // Apply the custom header to the document
                    //        doc.content.splice(0, 0, header);

                    //        doc.content[1].table.widths =
                    //           Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                 
                    //    }
                    //},
                ],
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
            })
            console.log(table);
            //$("div.toolbar").html('<b>This is my custom title</b>');

            //// Add a custom PDF export button with customization
            //$.extend(true, $.fn.dataTable.Buttons.defaults, {
            //    dom: {
            //        button: {
            //            className: 'btn'
            //        }
            //    }
            //});

            //var exportButton = {
            //    extend: 'pdfHtml5',
            //    text: 'Export to PDF',
            //    customize: function (doc) {
            //        // Define the custom page header HTML
            //        var customHeader = '<div style="text-align: center; background-color: #f2f2f2; padding: 10px;">' +
            //            '<h2 style="color: #333;">Custom Page Header</h2>' +
            //            '<p style="color: #888;">Some additional information</p>' +
            //            '</div>';

            //        // Convert the custom header HTML to PDFMake format
            //        var customHeaderHtml = $('<div>').html(customHeader).contents();
            //        console.log(doc.styles);
            //        console.log(doc.fontSizes);
            //        console.log(doc)
            //        var customHeaderNode = doc['htmlToPdfMake'](customHeaderHtml, doc.styles, doc.fontSizes, doc);
            //        customHeaderNode.forEach(function (node) {
            //            // Set the text color of the header
            //            if (node.text && node.text[0] && node.text[0].text) {
            //                node.text[0].color = '#333'; // Set the desired text color
            //            }
            //            doc.content.splice(0, 0, node);
            //        });
            //    }
            //};

            //// Add the export button to the DataTable
            //table.buttons().container().prependTo('#exportButtons');
            //table.buttons().container().addClass('btn-group');
            //table.buttons().container().find('button').removeClass('dt-button');

            //// Add the custom PDF export button to the DataTable
            //table.buttons().add(exportButton);
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
                            <div class="form-group row col-md-6">
                                <label for="inputEmail3" class="col-sm-3 col-form-label">Cetakan Jadual Pinjaman :</label>
                                <div class="col-sm-8">
                                    <div id="divautocomplete" class="tt-menu" style="display:none">
                                        <div class="input-group">
                                        </div>
                                    </div>
                                    <div class="input-group">
                                               <asp:DropDownList ID="ddlCariTahap" runat="server" CssClass="form-control"  EnableFilterSearch="true" FilterType="StartsWith"></asp:DropDownList>
                                        <div class="input-group-append">
                                            <button id="lbtnCari" runat="server" class="btn btn-outline" type="button"><i
                                            class="fa fa-search"></i>Cari</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

            <div class="table-title">
<%--                <h6>Jadual Bayar Balik Pinjaman</h6>
                <div class="btn btn-primary" onclick="ShowPopup('2')">
                    <i class="fa fa-plus"></i>Export to Excel              
                </div>
                <div class="btn btn-primary" onclick="ShowPopup('2')">
                    <i class="fa fa-plus"></i>Export to Pdf              
                </div>--%>
                <div>
                    <asp:Button ID="btnExport" runat="server" Text="PRINT" OnClick = "ExportToPDF" />
                </div>
                
                <div>
                    <asp:Button ID="btnView" runat="server" Text="Print Preview" OnClick = "PrintPreview" />
                </div>
            </div>

            <div class="box-body" align="center">     
    
                <div id="exportButtons"></div>
                 <asp:GridView ID="gvKod" CssClass="table table-bordered table-striped grid" Width="100%" runat="server"
                     AutoGenerateColumns="false" ShowHeaderWhenEmpty ="true">
                    <Columns>
                        <asp:BoundField DataField="Bil" HeaderText="Bil" HeaderStyle-HorizontalAlign="Center"> 
                            <ItemStyle Width="5%" HorizontalAlign="center" />
                        </asp:BoundField>
                     <asp:BoundField DataField="Faedah" HeaderText="Ansuran" >
                            <ItemStyle Width="8%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Faedah" HeaderText="Faedah" >
                            <ItemStyle Width="8%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pokok" HeaderText="Pokok" >
                            <ItemStyle Width="8%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="JumFaedah" HeaderText="Jumlah Faedah" >
                            <ItemStyle Width="8%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="JumPokok" HeaderText="Jumlah Pokok" >
                            <ItemStyle Width="8%" HorizontalAlign="center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BakiPokok" HeaderText="Baki Pokok" >
                            <ItemStyle Width="8%" HorizontalAlign="center" />
                        </asp:BoundField>

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

                            <h6>Maklumat Cetakkan</h6>
                            <hr>
                            <div class="row">
                                <%--<div class="col-md-12">
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Tahap </label>
                                            <asp:DropDownList ID="ddlTahap" runat="server" CssClass="form-control" ></asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>Kod Sub Menu</label>
                                            <input id="txtKodSubMenu" runat="server" type="text" class="form-control" enable="true">
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="form-group col-md-12">
                                            <label>Sub Menu </label>
                                            <asp:DropDownList ID="ddlNamaSubMenu" runat="server" CssClass="form-control" ></asp:DropDownList>
                                        </div>

                                    </div>
                                    txtJenis
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
                                </div>--%>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                            <%--<button type="button" runat="server" id="lbtnSimpan" class="btn btn-secondary">Simpan</button>--%>

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
