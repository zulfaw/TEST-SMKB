<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Proses_Gaji.aspx.vb" Inherits="SMKB_Web_Portal.Proses_Gaji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script type="text/javascript">


    </script>  
<div id="PermohonanTab" class="tabcontent" style="display: block">
        <div class="container" style=" margin-bottom: 10%;margin-top:0%;margin-left:0%;">
        <div class="row">
            <div class="col-md-4 col-sm-6 
                            col-xl-6 my-3">
                <div class="card d-block h-100 
                    box-shadow-hover pointer" >
                  <div class="card-header">
                    Bulan/Tahun Gaji : <label id="lblBlnThnGaji"></label>
                  </div>
                    
                    <div class="card-body p-6">

                        <div class="form-row">
                           <div class="form-group col-md-6">
                                <label>PTj Dari</label>
                                <asp:DropDownList ID="ddlPtjDr" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%">
                          </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-6">
                                <label>PTj Hingga</label>
                                     <asp:DropDownList ID="ddlPtjHg" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%">
                          </asp:DropDownList>
                                             
                            </div>
      
                        </div>
                         <div class="form-row">
                           <div class="form-group col-md-6">
                                <label>No. Staf Dari</label>
                                <asp:DropDownList ID="ddlStafDr" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%">
                          </asp:DropDownList>
                            </div>
                            <div class="form-group col-md-6">
                                <label>No. Staf Hingga</label>
                                     <asp:DropDownList ID="ddlStafHg" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%">
                          </asp:DropDownList>
                                             
                            </div>
      
                        </div>

                       
                    <br />
                    <button type="button" class="btn btn-secondary btnSimpan" data-toggle="tooltip" data-placement="bottom" title="Proses">Proses</button>
                    </div>
                </div>
               <label id="hidBulan" style="visibility:hidden"></label>
                    <label id="hidTahun" style="visibility:hidden"></label>
            </div>
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

            fetch('Proses_WS.asmx/LoadBlnThnGaji', {
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
        $('#hidBulan').text(sBulan + data[0].tahun);
        $('#hidTahun').text(data[0].tahun);

    }

        $('.btnSimpan').click(function ()
        {
            //alert('masuk');
            var bln = $('#hidBulan').text();

            show_loader();
            $.ajax({
                "url": "Proses_WS.asmx/fProsesGaji",
                method: 'POST',
                "contentType": "application/json; charset=utf-8",
                "dataType": "json",
                data: JSON.stringify({ ptjDr: $('#<%=ddlPtjDr.ClientID%>').val(), ptjHg: $('#<%=ddlPtjHg.ClientID%>').val(), nostafDr: $('#<%=ddlStafDr.ClientID%>').val(), nostafHg: $('#<%=ddlStafHg.ClientID%>').val(), kodparam : bln }),
                success: function (response) {
                    console.log('Success', response);
                    var response = JSON.parse(response.d);
                    console.log(response);
                    alert(response.Message);
                    window.location.reload(1);
                }   
            });
        });
   
    </script>

</asp:Content>
