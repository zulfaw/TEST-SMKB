<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Daftar_Kod_Terima.aspx.vb" Inherits="SMKB_Web_Portal.Daftar_Kod_Terima" %>

<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">

    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/bootstrap 3.3.7/bootstrap.min.js" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>

     

    <script type="text/javascript">
        var gvTestClientId = '<% =gvKodKW.ClientID %>';
    </script>

    <script type="text/javascript">


        $(function () {
            $("[id*=gvKodKW]").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                //"pageLength": 10,
                //"paging": true,
                //"lengthChange": false,
                //"searching": true,
                //"ordering": true,
                //"info": true,
                //"autoWidth": true
                "responsive": true,
                "sPaginationType": "numbers"
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

        function ShowPopup(elm) {

            if (elm == "1") {
                $('#permohonan').modal('toggle');

            }
            else if (elm == "2") {

                $(".modal-body input").val("");
                $('#permohonan').modal('toggle');
            }


        }

        function SaveSucces() {
                        
            $('#MessageModal').modal('toggle');

        }

    </script>

        <style type="text/css">
        .hideGridColumn {
            display: none;
        }
    </style>

 
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <div class="table-title">
                <h6>Senarai Kod Terima</h6>
                <div class="btn btn-primary" onclick="ShowPopup('2')">
                    <i class="fa fa-plus"></i>Tambah Kod Terima             
                </div>

            </div>
             <div class="box-body" > 
                  <asp:GridView ID="gvKodKW"  CssClass="table table-bordered table-striped" Width="100%" runat="server"
            CellPadding="5" CellSpacing="2" AutoGenerateColumns="false" >
                    <Columns>
                        <asp:BoundField DataField="Kod_Terima" HeaderText="Kod">
                            <ItemStyle Width="10%" HorizontalAlign="center" />
                            <HeaderStyle HorizontalAlign="Center"/>
                        </asp:BoundField>               
                          <asp:BoundField DataField="Butiran" HeaderText="Butiran" HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                            <ItemStyle Width="10%" HorizontalAlign="center" />
                        </asp:BoundField> 
                     <asp:TemplateField HeaderText = "Butiran" ItemStyle-Width="30">  
                            <ItemTemplate>                                 
                                <asp:LinkButton runat="server"  
                                    Style="text-decoration:none;font:bold;color:blue"                                      
                                    Text='<%# DataBinder.Eval(Container.DataItem, "Butiran") %>'  class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" ssClass="btn-xs" ToolTip="Pilih"/>  
                            </ItemTemplate>  
                        </asp:TemplateField>  
                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="30%" />
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

            <!-- Modal -->
            <div class="modal fade" id="permohonan" tabindex="-1" role="dialog"
                aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-s" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Tambah Kod Terima</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">

                            <h6>Maklumat Kod Terima</h6>
                            <hr>
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="form-group col-md-6">
                                        <label>Kod Terima</label>
                                        <input id="txtKodKW" runat="server" type="text" class="form-control" enable="true">
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label>Butiran</label>
                                        <input id="txtButiran" runat="server" type="text" class="form-control" enable="true">
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
                            <h5 class="modal-title" id="exampleModalLabel">Tolong Sahkan?</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <asp:Label runat="server" ID="lblModalMessaage"/>                          
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                        
                        </div>
                    </div>
                </div>
            </div>
        </div>
   

</asp:Content>
