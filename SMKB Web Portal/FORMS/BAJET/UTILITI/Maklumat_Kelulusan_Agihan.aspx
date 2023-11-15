<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Maklumat_Kelulusan_Agihan.aspx.vb" Inherits="SMKB_Web_Portal.Maklumat_Kelulusan_Agihan_" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1>MAKLUMAT PERMOHONAN</h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>    

            <div class="row">
                <asp:LinkButton ID="lnkBtnBack" runat="server" CssClass="btn " Width="100px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
                </asp:LinkButton>
            </div>

            


            <div class="row" style="width: 80%;">

          

                <div id="divAlert" runat="server"  class="alert alert-info" visible="false">
                    <asp:Label ID="lblAlert" runat="server" />
                </div>

             
                <div class="panel-body" style="width: 800px;">
                    <div class="row">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 100px">PTj</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtPTj" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 600px;"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>                                 
                                    Bajet Tahun</td>
                                <td>:</td>
                                <td>
                                    <asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 100px;"></asp:TextBox>
                                </td>
                            </tr>

                        </table>
                        </table>
                    </div>
                </div>
       

                <div class="panel panel-default" style="width: 100%;">
                    <div class="panel-heading">Senarai Permohonan</div>
                    <div class="panel-body" style="max-height: 1000px; overflow: scroll;">
                        <div style="margin: 10px; width: 95%;">
                       <%--     <div class="GvTopPanel">

                                <div class="panel-title pull-right" style="margin-top: 5px; margin-right: 5px;">
                                    <i class="fas fa-info-circle fa-lg" aria-hidden="true" data-html="true" data-toggle="tooltip" data-placement="top" style="cursor: pointer;"
                                        title="Petunjuk : <br/><span style='font-weight:bold'>Jumlah Besar </span> = Jumlah bajet semua Objek Am  <br/><span style='font-weight:bold'>Bajet Objek Am </span>= Jumlah bajet semua Objek Sebagai dalam Objek Am tersebut"></i>
                                </div>
                            </div>--%>

                            Jumlah Rekod :
                            <asp:Label ID="lblRekod" runat="server" />

                            <asp:GridView ID="gvListAgihan" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" EmptyDataText=" " ShowFooter="true" ShowHeaderWhenEmpty="True" Width="100%">
                                <columns>
                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                            .
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="KodKW" HeaderStyle-CssClass="centerAlign" HeaderText="Kod KW" ReadOnly="true" SortExpression="KodKW">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="KodOperasi" HeaderStyle-CssClass="centerAlign" HeaderText="Kod KO" ReadOnly="true" SortExpression="KodOperasi">
                                    <ItemStyle HorizontalAlign="Center" Width="5%" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Program / Aktiviti">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProgram" runat="server" class="gridViewToolTip" Text='<%# Limit(Eval("BG20_Program"), 30) %>' tooltip='<%# Eval("BG20_Program") %>'>
                      </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Justifikasi">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" class="gridViewToolTip" Text='<%# Limit(Eval("BG20_Justifikasi"), 30) %>' tooltip='<%# Eval("BG20_Justifikasi") %>'>
                      </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="30%" />
                                    </asp:TemplateField>
                                   <%-- <asp:BoundField DataField="BG20_NoMohon" HeaderStyle-CssClass="hiddencol" HeaderText="NoMohon" ItemStyle-CssClass="hiddencol" />--%>
                                    <asp:BoundField DataField="AngJumBesar" DataFormatString="{0:N}" HeaderStyle-CssClass="centerAlign" HeaderText="Anggaran Permohonan (RM)" SortExpression="AngJumBesar">
                                    <ItemStyle HorizontalAlign="Right" Width="10%" />
                                    </asp:BoundField>
<%--                                    <asp:BoundField DataField="Status" HeaderStyle-CssClass="centerAlign" HeaderText="Status" SortExpression="Status">
                                    <ItemStyle Width="20%" />
                                    </asp:BoundField>--%>
                                    <%--<asp:buttonfield buttontype="Button" commandname="Select" text="Info" />--%>
<%--                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDetail" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
                                <i class="fa fa-ellipsis-h fa-lg"></i>
                                </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                    </asp:TemplateField>--%>
<%--                                    <asp:TemplateField HeaderStyle-CssClass="centerAlign">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Text="Hantar" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CheckBox1" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="2%" />
                                    </asp:TemplateField>--%>
                                </columns>
                            </asp:GridView>
                        </div>
                       
                    </div>
                </div>

                <div style="text-align: center; margin-bottom: 10px; width: 100%">
                    <asp:LinkButton ID="lbtnLulus" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk luluskan agihan bajet ini?');" Visible="false">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Salin
                    </asp:LinkButton>
                    
                    <asp:LinkButton ID="lbtnSalin" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk salin permohonan bajet 2022 ke permohonan bajet 2023 ?');">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Salin
                    </asp:LinkButton>

                </div>
            </div>

            </ContentTemplate>
        </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="background-color: #D2D2D2; filter: alpha(opacity=80); opacity: 0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">
            </div>
            <div style="margin: auto; font-family: Trebuchet MS; filter: alpha(opacity=100); opacity: 1; font-size: small; vertical-align: middle; top: auto; position: absolute; left: auto; color: #FFFFFF; position: fixed; top: 50%; left: 50%; margin-top: -50px; margin-left: -100px;">
                <table>
                    <tr>
                        <td style="text-align: center;">
                            <img src="../../../Images/loader.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
