<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Senarai_Invois.aspx.vb" Inherits="SMKB_Web_Portal.Senarai_Invois" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="Server">

    <link type="text/css" rel="stylesheet" href="../../../Scripts/jquery 1.13.3/css/jquery.dataTables.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/dataTables.bootstrap.min.css" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/bootstrap 3.3.7/bootstrap.min.js" />
    <link type="text/css" rel="stylesheet" href="../../../Scripts/cdn/css/responsive.bootstrap.min.css" />
    <script type="text/javascript" src="../../../Scripts/jquery 3.5.1/jquery-3.5.1.js"></script>
    <script type="text/javascript" src="../../../Scripts/jquery 1.13.3/js/jquery.dataTables.min.js"></script>

    <script type="text/javascript">
        $(function () {
            $("[id*=gvSenarai]").prepend($("<thead></thead>").append($(this).find("tr:first"))).DataTable({
                "pageLength":2,
                "paging": true,
                "lengthChange": false,
                "searching": true,
                "ordering": true,
                "info": true,
                "autoWidth": true
            });
        })
    </script>
        
    <div class="box-body" style="overflow: scroll; height: 300px">
        <asp:GridView ID="gvSenarai" CssClass="table table-bordered table-striped" Width="100%" runat="server"
            CellPadding="5" CellSpacing="2" AutoGenerateColumns="false">
            <columns>                                                                     
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="NoInv" HeaderText="No. Invois" />
                <asp:BoundField DataField="NPembekal" HeaderText="Pembekal" />

            </Columns>
        </asp:GridView>
    </div>
    
</asp:Content>

