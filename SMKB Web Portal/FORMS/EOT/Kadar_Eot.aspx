<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.Master" CodeBehind="Kadar_Eot.aspx.vb" Inherits="SMKB_Web_Portal.Kadar_Eot" %>
<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">

<link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/bootstrap 3.3.7/bootstrap.min.js" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <%--    <script type="text/javascript" src="../../../Scripts/jquery 3.5.1/jquery-3.5.1.js"></script>--%>
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>


    
    <script type="text/javascript">
        
    </script>

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

        function ShowPopup(elm) {

            if (elm == "1") {
                $('#permohonan').modal('toggle');

            }
            else if (elm == "2") {

               /* $(".modal-body input").val("");*/
                $('#permohonan').modal('toggle');
            }


        }

        function SaveSucces() {
                        
           /* $('#MessageModal').modal('toggle');*/
            $('#MessageModal').modal('hide');
           

        }


        function fnSave() {


            try {

                var blnComplete = true;
                Kod = document.getElementById('<%=txtKod.ClientID%>')
                if (Kod.value == "") {
                    blnComplete = false

                }

                if (blnComplete == false) {
                    alert('Sila lengkapkan maklumat!')
                    return false;
                }

                if (confirm('Anda pasti untuk simpan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }

            }

            catch (err) {
                alert(err)
                return false;
            }

            return true;
        }

    </script>

        <style type="text/css">
        .hideGridColumn {
            display: none;
        }
    </style>

    <div id="PermohonanTab" class="tabcontent" style="display: block">
            <div class="table-title">
                <h6>Senarai Kadar EOT</h6>
                <div class="btn btn-primary" onclick="ShowPopup('2')">
                    <i class="fa fa-plus"></i>Tambah Senarai              
                </div>

            </div>
      
            <div class="box-body">
                <asp:GridView  ID="grdOT" CssClass="table table-bordered table-striped grid" Width="100%" runat="server" CellPadding="5" CellSpacing="2" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Kod" HeaderText="Kod"></asp:BoundField>
                        <asp:BoundField DataField="Butiran" HeaderText="Butiran"></asp:BoundField>
                        <asp:BoundField DataField="Kadar" HeaderText="Kadar(Jam)"></asp:BoundField>
                        <asp:BoundField DataField="Min_OT" HeaderText="Min OT"></asp:BoundField>
                        <asp:BoundField DataField="Max_OT" HeaderText="Max OT"></asp:BoundField>
                        <asp:BoundField DataField="Kira_Kwsp" HeaderText="Kira Kwsp"></asp:BoundField>
                        <asp:BoundField DataField="Kira_Perkeso" HeaderText="Kira Perkeso"></asp:BoundField>
                        <asp:BoundField DataField="Kira_Cukai" HeaderText="Kira Cukai"></asp:BoundField>
                        <asp:BoundField DataField="Kira_Pencen" HeaderText="Kira Pencen"></asp:BoundField>
                        <asp:BoundField DataField="Vot_Tetap" HeaderText="Vot Tetap"></asp:BoundField>
                        <asp:BoundField DataField="Vot_Bukan_Tetap" HeaderText="Vot Bukan Tetap"></asp:BoundField>
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
                            <h5 class="modal-title" id="exampleModalCenterTitle">Tambah Kadar</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">

                            <h6>Maklumat Kadar EOT</h6>
                            <hr>
                           
                                    <div class="form-row">
                                        <div class="form-group col-md-4">
                                            <label>Kod</label>
                                            <input id="txtKod" Name="txtKod" runat="server" type="text" style="width:50px"  class="form-control" enable="false">
                                        </div>
                                        <div class="form-group col-md-8">
                                            <label>Butiran</label>
                                            <input id="txtButiran" runat="server" type="text" class="form-control" enable="true">
                                        </div>

                                        
                                    </div>
                                    <div class="form-row">
                                         <div class="form-group col-md-4">
                                            <label>Kadar</label>
                                            <input id="txtKadarEOT" runat="server" type="text" class="form-control" enable="true">
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>Min OT</label>
                                            <input id="txtMin" runat="server" type="text" class="form-control" enable="true">
                                        </div>

                                        <div class="form-group col-md-4">
                                            <label>Max OT</label>
                                            <input id="txtMax" runat="server" type="text" class="form-control" enable="true">
                                        </div>                                   
                                    </div>
                               
                              </div>
                             <div class="form-row">
                                        <div class="form-group col-md-2">
                                            
                                         </div>
                                        <div class="form-group col-md-5">
                                            <label>Kira Kwsp</label>
                                            <asp:RadioButtonList runat="server" ID="rblKwsp" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Aktif </asp:ListItem>
                                            
                                                <asp:ListItem Value="0"> Tidak Aktif</asp:ListItem>
                                            </asp:RadioButtonList> 
                                         </div>
                                         <div class="form-group col-md-5">
                                            <label>Kira Perkeso</label>
                                            <asp:RadioButtonList runat="server" ID="rblPerkeso" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Aktif </asp:ListItem>
                                                <asp:ListItem Value="0"> Tidak Aktif</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                            </div>
                            <div class="form-row">
                                         <div class="form-group col-md-2">
                                            
                                         </div>
                                         <div class="form-group col-md-5">                                                                                                                                            
                                            <label>Kira Cukai</label>
                                            <asp:RadioButtonList runat="server" ID="rblCukai" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Aktif </asp:ListItem>
                                                <asp:ListItem Value="0"> Tidak Aktif</asp:ListItem>
                                            </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;
                                        </div>
                                          <div class="form-group col-md-5"> 
                                            <label>Kira Pencen</label>
                                                <asp:RadioButtonList runat="server" ID="rblPencen" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Aktif </asp:ListItem>
                                                <asp:ListItem Value="0"> Tidak Aktif</asp:ListItem>
                                                </asp:RadioButtonList>                                                 
                                        </div>
                              </div>
                               <div class="form-row"">
                                    
                                        <div class="form-group col-md-1">
                                            
                                         </div>                                       
                                        <div class="form-group col-md-5">
                                            <label>Vot Tetap</label>
                                            <asp:DropDownList ID="cmbVotTetap" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>                                      
                                        </div>
                                        <div class="form-group col-md-5">
                                            <label>Vot Bukan Tetap</label>
                                            <asp:DropDownList ID="cmbVotBTetap" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>                                      
                                        </div>
                               </div>
                              

                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                            <button type="button" runat="server" id="lbtnSimpan" class="btn btn-secondary" data-dismiss="modal"> Simpan</button>

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
   
</div>
</asp:Content>
