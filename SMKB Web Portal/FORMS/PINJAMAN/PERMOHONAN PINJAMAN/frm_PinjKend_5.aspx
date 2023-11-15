<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frm_PinjKend_5.aspx.vb" Inherits="SMKB_Web_Portal.frm_PinjKend_5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Permohonan Skim Pinjaman Kenderaan Staf UTeM</h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
    <script type="text/javascript">
        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };

        function trim(ustr) {
            var str = ustr.value;
            ustr.value = str.trim();
        };

        function isInputNumber(evt)
        {
            var char = String.fromCharCode(evt.which);

            if(!(/[0-9]/.test(char))){
                evt.preventDefault();
            }
        }

    </script>
    <style type="text/css">
        .highlight{
            background: rgba(192,192,192,0.2);
            padding:10px;
        }
        </style>
    <div class="stepwizard">
        <%--<div class="stepwizard-row">--%>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep1" type="button" class="btn-default btn-circle" runat="server">1</button>
                <p>Maklumat Pemohon</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep2" type="button" class="btn-default btn-circle" runat="server">2</button>
                <p>Maklumat Kenderaan</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep3" type="button" class="btn-default btn-circle" runat="server">3</button>
                <p>Maklumat Syarikat</p>
            </div>
            <div class="stepwizard-step" style="width:20%; top: 55px; left: 0px;">
                <button id="btnStep4" type="button" class="btn-default btn-circle" runat="server">4</button>
                <p>Penjamin</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep5" type="button" class="btn-success btn-circle" runat="server">5</button>
                <p>Senarai Semak</p>
            </div>
        <%--</div>--%>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%">
                <tr>
                    <td>   
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">Senarai Semak
                                </h4>
                            </div>
                            <div class="panel-body">
                                <table style="width: 100%">
                                    <tr>
                                        <td class="subtitle">Sila pastikan dokumen sokongan tersebut dihantar bersama semasa Permohonan Pinjaman.</td>
                                    </tr>
                                </table>
                                <table style="width:100%">
                                    <tr>
                                    <td>
                                        <asp:GridView ID="gvSenSemak" datakeyname="" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
                                        CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" BorderStyle="Solid" ShowFooter="True" Width="100%" DataKeyNames="ROC01_IDBank">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Bil" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Perkara" ItemStyle-Width="70%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPerkara" runat="server" Text=''></asp:Label>
                                                </ItemTemplate>
                                                
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Hantar" FooterStyle-HorizontalAlign="Center">
                                                </asp:TemplateField></Columns><EmptyDataTemplate>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle ForeColor="Blue" />
                                    </asp:GridView>
                                    </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            
 <table style="width:100%">
            <tr>
                <td></td></tr></table><table>
            </table>
            <div class="row" style="text-align:center;">
                <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-primary" ValidationGroup="btnSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                </asp:LinkButton>&nbsp;&nbsp; <asp:LinkButton ID="lbtnNext" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya" ValidationGroup="btnSimpan">
                    <i class="glyphicon glyphicon-chevron-right"></i>
                </asp:LinkButton></div></ContentTemplate><Triggers>  
          <%-- <asp:PostBackTrigger ControlID="lbtnUpPenyata" />  --%>
          <%--  <asp:PostBackTrigger ControlID="lbtnProfilSya" /> --%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
