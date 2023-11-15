<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Tabung_Haji.aspx.vb" Inherits="SMKB_Web_Portal.Tabung_Haji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
            <style>
            .info {background-color: #fcc4c4;} /* Blue */
        </style>
    <script type="text/javascript">

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
        
                        <div class="container">
                            
                            <div class="row justify-content-center">
                                <div class="col-md-9">
                                    <div class="form-row">
                                        <div class="col-md-3 order-md-1">
                                            </div>
                                        <div class="col-md-4 order-md-1">
                                            <div class="form-group">
                                                <label for="txtTarikh1" style="display: block; text-align: center; width: 100%;">Bulan/Tahun</label>
                                                <input type="month" id="txtTarikh" name="txtTarikh"  runat="server" class="form-control date-range-filter">
                                            </div>
                                        </div>

                                        <div class="col-md-10 order-md-1" style="text-align:center" >
                                            <div class="form-group"> 
                                                <asp:LinkButton ID="btnExport" OnClick="btnExport_Click" runat="server" CssClass="btn btn-secondary lbtnSimpan" ToolTip="Kemaskini" >
                                            Eksport</asp:LinkButton>
                                          
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
      
        <div class="card w-2" style="width: 30rem;align-content:center;">
          <h6 class="card-header">Rumusan</h6>
          <div class="card-body">
              Bil. Rekod :<asp:Label ID="jumrekod" runat="server" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Jumlah :<asp:Label ID="jumpcb" runat="server" ></asp:Label>
          </div>
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
                   
                        
                </Columns>
            </asp:GridView>                   
        </div>
    </div>


<div class="modal fade"  id="SpinModal" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-dialog-centered justify-content-center" role="document">
        <span class="fa fa-spinner fa-spin fa-3x"></span>
    </div>
</div>
     <div class="modal fade" id="MessageModal" tabindex="-1" aria-hidden="true">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Ralat!</h5>
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

        <script type="text/javascript">

            $(document).ready(function () {
                //getBlnThn();
            });
            function call_loader() {

                show_loader();
            }
        //    $('.lbtnSimpan').click(function () {
        //        //alert('masuk');
        //        //var bln = $('#hidBulan').text();

        //        show_loader();
             
        //});

        </script>

</asp:Content>
