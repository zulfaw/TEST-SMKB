﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" Async="true"  MasterPageFile="~/NestedMasterPage2.master" CodeBehind="TestApi.aspx.vb" Inherits="SMKB_Web_Portal.TestApi" %>

<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">


    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
   
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
       
            <div class="box-body" align="center">
           
        <div>
            <span id="errorMessage" runat="server" style="color: red;"></span>
        </div>
    <br><br><br>
        <div>
            <asp:Button ID="GetDataApi" runat="server" Text="Get Data API" OnClick="GetDataApi_Click" />
        </div>
        <div>
            <label for="Data1">Method:</label>
            <input type="text" id="txtData1" runat="server" value="AR"/>
        </div>
        <div>
            <label>Tarikh Transaksi</label>
            <input id="txtTkhTrans" runat="server" type="date" enable="true">
        </div>
        <div>
            <label for="Data2">Rujukan:</label>
            <input type="text" id="txtData2" runat="server" value="H00001170523"/>
        </div>
        <div>
            <label for="Data3">KW:</label>
            <input type="text" id="txtData3" runat="server" value="01"/>
        </div>
        <div>
            <label for="Data4">PTj:</label>
            <input type="text" id="txtData4" runat="server" value="430000"/>
        </div>
        <div>
            <label for="Data5">Vot:</label>
            <input type="text" id="txtData5" runat="server" value="76107"/>
        </div>
        <div>
            <label for="Data6">KO:</label>
            <input type="text" id="txtData6" runat="server" value="00"/>
        </div>
        <div>
            <label for="Data7">KP:</label>
            <input type="text" id="txtData7" runat="server" value="0000000"/>
        </div>
        <div>
            <label for="Data8">Amaun:</label>
            <input type="text" id="txtData8" runat="server" value="500.00"/>
        </div>
        <div>
            <label for="Data9">Debit/Kredit:</label>
            <input type="text" id="txtData9" runat="server" value="DR"/>
        </div>

    <br><br><br>
        <div>
            <asp:Button ID="InsertData" runat="server" Text="Insert Data" OnClick="InsertData_Click"/>
        </div>



            </div>

            <!-- Modal -->
            <div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-s" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Tambah Maklumat Zon                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">

                            <h6>Maklumat Zon</h6>
                            <hr>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>Kod Zon</label>
                                            <input id="txtKodZon" runat="server" type="text" class="form-control" enable="true">
                                        </div>
                                        <div class="form-group col-md-12">
                                            <label>Butiran</label>
                                            <input id="txtButiran" runat="server" type="text" class="form-control" enable="true">
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