<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Sah_ROC.aspx.vb" Inherits="SMKB_Web_Portal.Sah_ROC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <style>
.box {
  box-shadow:
  0 2.8px 2.2px rgba(0, 0, 0, 0.034),
  0 6.7px 5.3px rgba(0, 0, 0, 0.048),
  0 12.5px 10px rgba(0, 0, 0, 0.06),
  0 22.3px 17.9px rgba(0, 0, 0, 0.072),
  0 41.8px 33.4px rgba(0, 0, 0, 0.086),
  0 100px 80px rgba(0, 0, 0, 0.12)
;

  
  
  min-height: 100px;
  width: 30vw;
  margin: 100px auto;
  background: white;
  border-radius: 5px;
  margin-bottom: 15%;
}

</style>
    <div id="PermohonanTab" class="tabcontent" style="display: block">

                         <div class="container">
                            
                            <div class="row justify-content-center">
                                <div class="col-md-9 justify-content-center">
                                    <div class="form-row justify-content-center">
                                        <div class="col-md-8 order-md-1">
                                            <div class="form-group align-content-center">
                                                 <label>Bulan/Tahun Gaji : </label><label id="lblBlnThn"></label><br />
                           
                                                <asp:CheckBox ID="chkSah" runat="server" Text="Saya mengesahkan bahawa semua Proses ROC telah selesai dilaksanakan" />
                                            </div>
                                        </div>
                                        <div class="col-md-8 order-md-1">
                                            <div class="form-group">
                     
                                                <button type="button" class="btn btn-secondary btnSah" data-toggle="tooltip" data-placement="bottom" title="Simpan">Simpan</button>
        
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
        <label id="hidBulan" style="visibility:hidden"></label>
        <label id="hidTahun" style="visibility:hidden"></label>

        <div class="modal fade" id="SpinModal" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-dialog-centered justify-content-center" role="document">
                <span class="fa fa-spinner fa-spin fa-3x"></span>
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
<script type="text/javascript">

    $(document).ready(function () {
        getBlnThn();
    });

    function getBlnThn() {
        //Cara Pertama

        fetch('ROC_WS.asmx/LoadBlnThnGaji', {
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
        $('#lblBlnThn').text(data[0].butir);
        if (data[0].bulan < 10) {
            sBulan = '0' + data[0].bulan;
        }
        else {
            sBulan = data[0].bulan;
        }
        $('#hidBulan').text(sBulan + data[0].tahun);
        $('#hidTahun').text(data[0].tahun);

    }

    $('.btnSah').click(function () {

        if ($('#<%=chkSah.ClientID%>').is(':checked')) {
            var bln = $('#hidBulan').text();

            //var thn = $('#hidTahun').text;
            //alert(bln);
            $.ajax({
                "url": "ROC_WS.asmx/SahROC",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: JSON.stringify({ kodparam: bln }),
                success: function (response) {
                    console.log('Success', response);
                    var response = JSON.parse(response.d);
                    console.log(response);
                    alert(response.Message);
                    window.location.reload(1);
                }

            });
        } else {
            alert("Sila pilih untuk membuat pengesahan");
        }
 
     });
</script>
</asp:Content>
