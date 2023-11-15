<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Statutory.aspx.vb" Inherits="SMKB_Web_Portal.Statutory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">


<div id="PermohonanTab" class="tabcontent" style="display: block">
    <div class="container" style=" margin-bottom: 10%;margin-top:0%;margin-left:0%;">
        <div class="row">
            <div class="col-md-4 col-sm-6 
                            col-xl-4 my-3">
                <div class="card d-block h-100 
                    box-shadow-hover pointer" >
                  <div class="card-header">
                    Bulan/Tahun Gaji : <label id="lblBlnThnGaji"></label>
                  </div>

                    <div class="card-body p-4">

                                <h6>Pilihan Proses</h6>
                     <hr>  

                    <div class="form-check">  
                    <input type = "radio" class = "form-check-input" id = "btn1" name = "optradio" value = "all">  
                    <label class = "form-check-label" for = "btn1"> Keseluruhan </label>  
                    </div>  
                                        <div class="form-check">  
                    <input type = "radio" class = "form-check-input" id = "btn2" name = "optradio" value = "kwsp">  
                    <label class = "form-check-label" for = "btn1"> KWSP </label>  
                    </div> 
                    <div class="form-check">  
                    <input type = "radio" class = "form-check-input" id = "btn3" name = "optradio" value = "socso">  
                    <label class = "form-check-label" for = "btn2"> Perkeso </label>  
                    </div>  
                    <div class="form-check">  
                    <input type = "radio" class = "form-check-input" id = "btn4" name = "optradio" value = "cukai">  
                    <label class = "form-check-label" for = "btn3"> Cukai </label>  
                    </div>  
                    <div class="form-check">  
                    <input type = "radio" class = "form-check-input" id = "btn5" name = "optradio" value = "pencen">  
                    <label class = "form-check-label" for = "btn4"> Pencen </label>  
                    </div>  
                    <br />
                        <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Simpan">Simpan</button>

                    </div>
                </div>
            </div>
        </div>
    </div>
                        <label id="hidBulan" style="visibility:hidden"></label>
                    <label id="hidTahun" style="visibility:hidden"></label>
    <label id="hidParam" style="visibility:hidden"></label>
        <div class="box-body" align="center" >               
            <asp:GridView ID="gvJenis" CssClass="table table-bordered table-striped grid" Width="100%" runat="server"
                    AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" >                                    
                <Columns>
                    <asp:BoundField DataField="no_staf" HeaderText="No. Staf">
                        <ItemStyle Width="10%" HorizontalAlign="center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ms01_nama" HeaderText="Nama">
                        <ItemStyle Width="10%" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="gaji" HeaderText="Gaji (RM)">
                        <ItemStyle Width="10%" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="elaun" HeaderText="Elaun (RM)">
                        <ItemStyle Width="10%" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="potongan" HeaderText="Potongan (RM)">
                        <ItemStyle Width="10%" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="kwsp" HeaderText="KWSP (RM)">
                        <ItemStyle Width="10%" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="perkeso" HeaderText="Perkeso (RM)">
                        <ItemStyle Width="10%" HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cukai" HeaderText="Cukai (RM)">
                        <ItemStyle Width="10%" HorizontalAlign="Right" />
                    </asp:BoundField>
                        <%--<asp:TemplateField HeaderText="Kemaskini">
                            <ItemTemplate>

                                <asp:LinkButton ID="lbtnEdit" runat="server" ToolTip="Kemaskini" CommandName="EditRow" class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" >
                                        <i class="fa fa-edit"></i></asp:LinkButton>
    
                                    <asp:LinkButton ID="lbtnHapus" runat="server" ToolTip="Hapus" CommandName="DeleteRow" class="lnk" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" OnClientClick="return confirm('Adakah anda pasti untuk padam rekod ini?');" >
                                        <i class="fa fa-trash-o delete"></i>
                                    </asp:LinkButton>

                            </ItemTemplate>
                            <ItemStyle Width="3%" HorizontalAlign="Center" />
                        </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>                   
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
<script type="text/javascript">
    $(document).ready(function () {
        getBlnThn();
    });

    function getBlnThn() {
        //Cara Pertama

        fetch('Transaksi_WS.asmx/LoadBlnThnGaji', {
                method: 'POST',
                headers: {
                    'Content-Type': "application/json"
                },
            //body: JSON.stringify({ nostaf: '<%'=Session("ssusrID")%>' })
            body: JSON.stringify()
        })
            .then(response => response.json())
            .then(data => setBlnThn(data.d))

    }
    function setBlnThn(data) {
        var sBulan = "";
        data = JSON.parse(data);

        if (data.bulan === "") {
            alert("Tiada data");
            return false;
        }
        $('#lblBlnThnGaji').text(data[0].butir);
        if (data[0].bulan < 10) {
            sBulan = '0' + data[0].bulan;
        }
        else {
            sBulan = data[0].bulan;
        }

        $('#hidParam').text(sBulan + data[0].tahun);
        $('#hidBulan').text(data[0].bulan);
        $('#hidTahun').text(data[0].tahun);

    }
    $('.btnSimpan').click(function () {

        var vbln = $('#hidBulan').text();
        var vthn = $('#hidTahun').text();

        if ($('input:radio:checked').length > 0) {
            // go on with script

            if ($("#gender_male").attr("checked") == true) {

            }
        } else {
            // NOTHING IS CHECKED
            alert("Sila pilih untuk meneruskan proses");
            return false;
        }

        if (document.getElementById('btn1').checked) {
            //if (data.data.length === 0) {
            //    return false;
            //}
            show_loader();
            $.ajax({
                url: 'Transaksi_WS.asmx/fProsesStatutory',
                method: 'POST',
                data: JSON.stringify(),

                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log('Success', response);
                    var response = JSON.parse(response.d);
                    console.log(response);
                    alert(response.Message);
                    close_loader();
                    window.location.reload(1);
                }

            });
           

        } else if (document.getElementById('btn2').checked) {
            //if (data.data.length === 0) {
            //    return false;
            //}
            var param = $('#hidParam').text();
            show_loader();
            $.ajax({
                url: 'Transaksi_WS.asmx/fProsesKWSP',
                method: 'POST',
                data: JSON.stringify({ kodparam: param }),

                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log('Success', response);
                    var response = JSON.parse(response.d);
                    console.log(response);
                    alert(response.Message);
                    close_loader();
                    window.location.reload(1);
                }

            });
        } else if (document.getElementById('btn3').checked) {
            //if (data.data.length === 0) {
            //    return false;
            //}
            show_loader();
            $.ajax({
                url: 'Transaksi_WS.asmx/fProsesPerkeso',
                method: 'POST',
                data: JSON.stringify(),

                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log('Success', response);
                    var response = JSON.parse(response.d);
                    console.log(response);
                    alert(response.Message);
                    close_loader();
                    window.location.reload(1);
                }

            });
        } else if (document.getElementById('btn4').checked) {
            //if (data.data.length === 0) {
            //    return false;
            //}
            show_loader();
            $.ajax({
                url: 'Transaksi_WS.asmx/fProsesCukai',
                method: 'POST',
                data: JSON.stringify(),

                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log('Success', response);
                    var response = JSON.parse(response.d);
                    console.log(response);
                    alert(response.Message);
                    close_loader();
                    window.location.reload(1);
                }

            });
        } else if (document.getElementById('btn5').checked) {
            //if (data.data.length === 0) {
            //    return false;
            //}
            show_loader();
            $.ajax({
                url: 'Transaksi_WS.asmx/fProsesPencen',
                method: 'POST',
                data: JSON.stringify(),

                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log('Success', response);
                    var response = JSON.parse(response.d);
                    console.log(response);
                    alert(response.Message);
                    close_loader();
                    window.location.reload(1);
                }

            });
        }



     });
</script>
</asp:Content>
