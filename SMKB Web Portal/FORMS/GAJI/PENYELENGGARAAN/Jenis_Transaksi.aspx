<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Jenis_Transaksi.aspx.vb" Inherits="SMKB_Web_Portal.Jenis_Transaksi" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

<script type="text/javascript">

    $(function () {
        $('#<%= gvJenis.ClientID%>').prepend($("<thead></thead>").append($("#<%= gvJenis.ClientID%>").find("tr:first"))).DataTable({
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
    $(".modal-body input").val("");
}

function ShowPopup(elm) {

    if (elm == "1") {
        
        $(".modal-body input").val("");

       // $(".modal-body #hdnSimpan").val('1'); 
       
        $('#tambah').modal('toggle');
    }
    else if (elm == "2") {
       // $("#hdnSimpan").val("2");

        $('#tambah').modal('toggle');
    }

}
var win = null;
function OpenPopUp(mypage, myname, w, h, scroll, pos) {
    if (pos == "random") { LeftPosition = (screen.width) ? Math.floor(Math.random() * (screen.width - w)) : 100; TopPosition = (screen.height) ? Math.floor(Math.random() * ((screen.height - h) - 75)) : 100; }
    if (pos == "center") { LeftPosition = (screen.width) ? (screen.width - w) / 2 : 100; TopPosition = (screen.height) ? (screen.height - h) / 2 : 100; }
    else if ((pos != "center" && pos != "random") || pos == null) { LeftPosition = 0; TopPosition = 20 }
    settings = 'width=' + w + ',height=' + h + ',top=' + TopPosition + ',left=' + LeftPosition + ',scrollbars=' + scroll + ',location=no,directories=no,status=no,menubar=no,toolbar=no,resizable=yes';
    win = window.open(mypage, myname, settings);
}
</script>

<div id="PermohonanTab" class="tabcontent" style="display: block">
    <div class="table-title">
        <h6>Senarai Transaksi Mengikut Jenis</h6>
        <hr>
        <div class="btn btn-primary"  onclick="ShowPopup('1')">
            <i class="fa fa-plus"></i>Tambah Transaksi              
        </div>&nbsp;&nbsp;
        
    </div>
           
        <div class="box-body" align="center" >               
            <asp:GridView ID="gvJenis" CssClass="table table-bordered table-striped grid" Width="100%" runat="server"
                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" >                                    
                <Columns>
                    <asp:BoundField DataField="Jenis_Trans" HeaderText="Jenis Transaksi">
                        <ItemStyle Width="10%" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Butiran" HeaderText="Butiran">
                        <ItemStyle Width="50%" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Daripada" HeaderText="Daripada">
                        <ItemStyle Width="20%" HorizontalAlign="center" />
                    </asp:BoundField>
                   
                        <asp:TemplateField HeaderText="Kemaskini">
                            <ItemTemplate>

                                <asp:LinkButton ID="lbtnEdit" runat="server" ToolTip="Kemaskini" CommandName="EditRow" class="lnk" CssClass="btn-xs" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" >
                                        <i class="fa fa-edit"></i></asp:LinkButton>
    
                                    <asp:LinkButton ID="lbtnHapus" runat="server" ToolTip="Hapus" CommandName="DeleteRow" class="lnk" CssClass="btn-xs" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" OnClientClick="return confirm('Adakah anda pasti untuk padam rekod ini?');" >
                                        <i class="fa fa-trash-o delete"></i>
                                    </asp:LinkButton>

                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>                   
        </div>

        <!-- Modal -->
        <div class="modal fade" id="tambah" tabindex="-1" role="dialog"
            aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
            <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="eMCTitle">Tambah Jenis Transaksi</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>

                    <div class="modal-body">
                            <h6>Maklumat Transaksi</h6>
                            <hr>
                            <div class="row">
                                <%--<input id="hdnSimpan" name="hdnSimpan"  runat="server" type="text" class="form-control" enable="true">--%>
                                <div class="col-md-6">
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label>Jenis Transaksi</label>
                                            <asp:TextBox runat="server" ID="txtJenis" CssClass="form-control" Style="width: 100%;" ></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="form-group col-md-6">
                                            <label>Butiran</label>
                                            <asp:TextBox runat="server" ID="txtButir" CssClass="form-control" Style="width: 100%;" ></asp:TextBox>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <label>Daripada</label>
                                            <asp:TextBox runat="server" ID="txtDaripada" CssClass="form-control" Style="width: 100%;" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                            <hr>  
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">Tutup</button>
                        <button type="button" runat="server" id="lbtnSave" class="btn btn-secondary">Simpan</button>

                    </div>
                </div>
            </div>
        </div>
    
        <!-- End Modal -->
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
