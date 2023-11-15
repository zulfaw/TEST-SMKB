<%@ Page Title="" Language="vb" AutoEventWireup="false" Async="true"  MasterPageFile="~/NestedMasterPage2.master" CodeBehind="statistik_permohonan.aspx.vb" Inherits="SMKB_Web_Portal.statistik_permohonan" %>

<asp:Content ID="FormContents" ContentPlaceHolderID="FormContents" runat="server">
       <div id="PermohonanTab" class="tabcontent" style="display: block">
            <div class="row rowbody">
                <div class="form-group col-sm-2 lblinput">
                    <label >Tahun</label>                                   
                </div>
                <div class="form-group col-sm-2"> 
                    <asp:DropDownList ID="ddlTahun" runat="server" CssClass="form-control textinput" ></asp:DropDownList>
                </div>
                <div class="input-group-append">
                    <button id="lbtnCari" runat="server" class="btn btn-outline" type="button"><i
                    class="fa fa-search"></i>Cari</button>
                </div>
            </div>
           <div class="row rowbody">
               <asp:Label ID="lblTajukStatistik" runat="server" Text=""></asp:Label>
            </div>
            <br>
            <table width="100%">
                <tr>
                    <td class="border"><div class="containerChart"><iframe id="myIframe" runat="server" class="responsive-iframe" frameborder="0" scrolling="no" ></iframe></div></td>
                </tr>
                <tr>
                    <td><%#Eval("tahun")%></td>
                </tr>
            </table> 
        </div>

<%--<script type="text/javascript">
    $(document).ready(function () 
        {
            $('.btnSearch').click(async function () {
                //show_loader();
                alert('111');
                //isClicked = true;
                //tbl.ajax.reload();
            })
        }
</script>--%>

</asp:Content>