<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="SuratPeringatan.aspx.vb" Inherits="SMKB_Web_Portal.SuratPeringatan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/css/select2.min.css" />
  <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/select2@4.0.13/dist/js/select2.min.js"></script>
  <script type="text/javascript">
      $(function () {
          $("[id*=ddlPenghutang]").select2();
      });
  </script>
    <script type="text/javascript">
        $(function () {
            $("[id*=ddlBil]").select2();
        });
</script>

    <style>
        .dropdown-list {
            width: 100%;
            /* You can adjust the width as needed */
        }
    
        .align-right {
            text-align: right;
        }
    
        .center-align {
            text-align: center;
        }
    </style>

     <contenttemplate>
        <div id="PermohonanTab" class="tabcontent" style="display: block">
            <!-- Modal -->
            <div id="permohonan">
                <div>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalCenterTitle">Surat Peringatan</h5>
                        </div>
            
                        <!-- Create the dropdown filter -->
                        <div class="search-filter">
                            <div class="form-row justify-content-center">
                                <div class="form-group row col-md-8">
                                    <label for="tahun" class="col-sm-4 col-form-label" style="text-align: right">Penghutang :</label>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlPenghutang" runat="server" CssClass="form-control" EnableFilterSearch="true" AutoPostBack="true" OnSelectedIndexChanged="idModul_SelectedIndexChanged" FilterType="StartsWith"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row justify-content-center mt-4" id="BilSection" runat="server" Visible="false">
                                <div class="form-group row col-md-8">
                                    <label for="ptj" class="col-sm-4 col-form-label" style="text-align: right">No. Bil :</label>
                                    <div class="col-sm-6">
                                        <div class="input-group">
                                            <%--<input type="text" id="noPenghutang" name="noPenghutang" autocomplete="off" placeholder="No. Penghutang / ID Penghutang" class="form-control"/>--%>
                                            <asp:DropDownList ID="ddlBil" runat="server" CssClass="form-control" EnableFilterSearch="true" FilterType="StartsWith" ></asp:DropDownList>
                                        </div>
                                        <%--<div class="col-6">--%>
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="form-row justify-content-center mt-4" id="btnSearchSection" visible="false">
                                <asp:Button ID="btnSearch" runat="server" Text="Surat Peringatan" CssClass="btn btn-primary btnSearch" OnClick="btnSearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
         <%--<script>
             $(document).ready(function () {
                 $('.btnSearch').click(async function () {
               /*      e.preventDefault();*/
                     var params = `scrollbars=no,resizable=no,status=no,location=no,toolbar=no,menubar=no,width=0,height=0,left=-1000,top=-1000`;

                     // Get the selected value of ddlBil
                     var nobil = $('#ddlBil').val();

                     alert(nobil)

                     // Check if nobil is not empty or zero
                     if (nobil !== '0') {
                         // Redirect to CetakSuratPeringatan page with nobil as a query parameter
                         /* window.location.href = 'CetakSuratPeringatan.aspx?nobil=' + nobil;*/
                         window.open('<%=ResolveClientUrl("~/FORMS/AKAUN PENERIMA/Laporan/CetakSuratPeringatan.aspx")%>?nobil=' + nobil, '_blank', params);
                     } else {
                         // Handle the case where nobil is empty or zero
                         alert('Please select a valid No. Bil');
                     }
                 });
             });
</script>--%>
    </contenttemplate>
</asp:Content>