<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="SenaraiInvois.aspx.vb" Inherits="SMKB_Web_Portal.SenaraiInvois" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
    <ContentTemplate>
        <div id="SenaraiInvoisTab" class="tabcontent" style="display:block">
            <div id="divSenaraiInvois" runat="server">
                <div>
                    <h5  id="exampleModalCenterTitle">Senarai Invois</h5>
                </div>
                <div >
                    <h6>Maklumat Invois</h6>
                    <hr>
                    <div class="row">
                            <div class="col-md-12">
                                <div class="transaction-table table-responsive">
                                    <table class="table table-bordered" id="tblDataInvois">
                                        <thead>
                                            <tr style="width:100%;text-align:center">
                                                <th scope="col" style="width:2%">Bil</th>
                                                <th scope="col" style="width:15%">No Invois</th>
                                                <th scope="col" style="width:10%">ID Penghutang</th>
                                                <th scope="col" style="width:10%">Tarikh Mula</th>
                                                <th scope="col" style="width:10%">Tarikh Tamat</th>
                                                <th scope="col" style="width:10%">Jenis Urusniaga</th>
                                                <th scope="col" style="width:30%">Tujuan</th>
                                                <th scope="col" style="width:10%;">Jumlah (RM)</th>
                                                <th scope="col" style="width:3%">Tindakan</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr >
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </tbody>
                                    </table>

                                </div>
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:Content>
