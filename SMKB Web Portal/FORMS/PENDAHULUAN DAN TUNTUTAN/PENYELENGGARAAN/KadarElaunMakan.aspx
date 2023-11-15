<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="KadarElaunMakan.aspx.vb" Inherits="SMKB_Web_Portal.KadarElaunMakan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/bootstrap 3.3.7/bootstrap.min.js" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        //$(function () {
        //    $("[id*=gvSenarai]").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
        //        "pageLength": 5,
        //        "paging": true,
        //        "lengthChange": false,
        //        "searching": true,
        //        "ordering": true,
        //        "info": true,
        //        "autoWidth": true
        //    });
        //})

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

            $(function () {
                console.log($('#ModulForm').modal);
                if (elm == "1") {
                    $('#ModulForm').modal('toggle');

                }
                else if (elm == "2") {
                    $(".modal-body input").val("");
                    $('#ModulForm').modal('toggle');
                }
            })

        }

    </script>
   
    <div class="table-title">                    
        <h6></h6>
        <div class="btn btn-primary" data-toggle="modal" data-target="#ModulForm">+ Tambah Maklumat  </div>
    </div>
     
    
     <div class="box-body" style="overflow: auto; height: 600px">
        <asp:GridView ID="gvSenarai" CssClass="table table-bordered table-striped grid" Width="100%"  runat="server" OnRowCommand="gvSenarai_RowCommand"
            CellPadding="5" CellSpacing="2" AutoGenerateColumns="false">
            <Columns>      
                <asp:BoundField DataField="KumpGred" HeaderText="Kumpulan" />
                <asp:BoundField DataField="GredDari" HeaderText="Gred Dari" />
                <asp:BoundField DataField="GredKe" HeaderText="Gred Hingga" />
                <asp:BoundField DataField="Tempat" HeaderText="Tempat" />
                <asp:BoundField DataField="JenisTugas" HeaderText="Jenis Tugas" /> 
                <asp:BoundField DataField="KadarMkn" HeaderText="Kadar Makan" />
                <asp:BoundField DataField="KadarHotel" HeaderText="Kadar Hotel" />
                <asp:BoundField DataField="KadarLojing" HeaderText="Kadar Lojing" />   
                 <asp:TemplateField HeaderText="Tindakan" ItemStyle-Width="10%">                       
                      <ItemTemplate>
                                    <asp:LinkButton  ID="lnkButton" runat="server"  class="lnk" data-id='<%#Eval("ID_Kdr")%>' CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"   
                                        CommandName="Select" CssClass="btn-xs" Text="Edit" ToolTip="Kemaskini">
                              <i class="fa fa-edit"></i></asp:LinkButton>

                                </ItemTemplate>
                    </asp:TemplateField>
          </Columns>

           
        </asp:GridView>
    </div>

     <!-- Modal -->
    <div class="modal fade" id="ModulForm" tabindex="-1" role="dialog"
        aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalCenterTitle">Tambah Maklumat</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <label for="kodModul">Kumpulan</label>
                            <asp:DropDownList ID="ddlKumpulan" Width="130px"  AutoPostBack="false" runat="server" class="form-control" ></asp:DropDownList> 
                        </div>

                         <div class="form-group col-md-6">
                            <label for="kodModul">Jenis Tugas</label>
                            <asp:DropDownList ID="ddlJnsTugas" Width="130px" AutoPostBack="false" runat="server" class="form-control" ></asp:DropDownList> 
                        </div>   
                        
                    </div>
                       
                    <div class="form-row">                                   
                        <div class="form-group col-md-6">
                            <label for="kodModul">Gred Dari</label>
                            <asp:TextBox ID="txtGredDari" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                        </div> 
                        <div class="form-group col-md-6">
                            <label for="kodModul">Kadar Hotel</label>
                            <asp:TextBox ID="txtKdrHotel" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                        </div>      
                    </div> 
                    
                    <div class="form-row">                                   
                        <div class="form-group col-md-6">
                            <label for="kodModul">Gred Ke</label>
                            <asp:TextBox ID="txtGredKe" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="form-group col-md-6">
                            <label for="kodModul">Kadar Lojing</label>
                            <asp:TextBox ID="txtKdrLojing" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                        </div>     
                    </div>
                                                  
                    <div class="form-row">            
                        <div class="form-group col-md-6">
                            <label for="kodModul">Tempat</label>
                            <asp:TextBox ID="txtTempat" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                        </div> 
                        <div class="form-group col-md-6">
                            <label for="kodModul">Kadar Makan</label>
                            <asp:TextBox ID="txtMakan" runat="server" Width="100px" CssClass="form-control"></asp:TextBox>
                        </div>
                       
                    </div>
                    <div class="form-row">  <%--hidden field --%>          
                        <div class="form-group col-md-6">
                            <asp:HiddenField ID="hidKodID" runat="server" />
                        </div> 
                     </div>

                 </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="lbtnTutup" runat="server" CssClass="btn btn-danger">Tutup </asp:LinkButton>
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info">
					Simpan
					</asp:LinkButton>  
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
