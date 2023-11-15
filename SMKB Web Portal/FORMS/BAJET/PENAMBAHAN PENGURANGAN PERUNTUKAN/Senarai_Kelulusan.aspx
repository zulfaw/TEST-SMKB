<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Senarai_Kelulusan.aspx.vb" Inherits="SMKB_Web_Portal.Senarai_Kelulusan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <style>
        .panel
        {
            width:80%;
        }

        @media (max-width: 1600px) {
            .panel {
                width: 95%;
            }
        }
    </style>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div id="divlst" runat="server">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Senarai Tambah/Kurang yang telah diluluskan
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="GvTopPanel">
                                    <div style="float: left; margin-top: 8px; margin-left: 10px;">
                                        <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                                    </div>
                                    <div class="panel-title pull-right" style="margin-top: 5px; margin-right: 5px;">
                                        <i class="fas fa-info-circle fa-lg" aria-hidden="true" data-html="true" data-toggle="popover" style="cursor: pointer; color: #ba2818;" data-placement="top"
                                            title="Petunjuk" data-content="<span style='color:#0055FF;'><i class='fas fa-arrow-circle-up fa-lg'></i> </span> - Penambahan Peruntukan   <br/><span style='color:#D50000;'><i class='fas fa-arrow-circle-down fa-lg'></i> </span> - Pengurangan Peruntukan"></i>
                                    </div>
                                </div>
                                <asp:GridView ID="gvSenarai" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" "
                                    CssClass="table table-striped table-bordered table-hover" Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnTB" runat="server">
      <a href="#" data-toggle="tooltip" title="Penambahan peruntukan" style="cursor:default;">
          <span style="color:#0055FF;"><i class="fas fa-arrow-circle-up fa-lg"></i> </span></a>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnKG" runat="server">
      <a href="#" data-toggle="tooltip" title="Pengurangan peruntukan" style="cursor:default;"> 
          <span style="color:#D50000;"><i class="fas fa-arrow-circle-down fa-lg"></i> </span></a>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bil">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="3%" />
                                        </asp:TemplateField>


                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblKw" runat="server" Text="KW" />                            
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblKw" runat="server" Text='<%# Eval("kodKw")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblButKW" runat="server" Text='<%# Eval("ButKW")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblKo" runat="server" Text="KO" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblKo" runat="server" Text='<%# Eval("kodKo")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblButKO" runat="server" Text='<%# Eval("ButKO")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblPTjKod" runat="server" Text="PTJ" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPTjKod" runat="server" Text='<%# Eval("KodPTJ")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPTjBut" runat="server" Text='<%# Eval("ButPTJ")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblKodKP" runat="server" Text="KP" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblKodKP" runat="server" Text='<%# Eval("KodKP")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblButKP" runat="server" Text='<%# Eval("ButKP")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblKodObjSbg" runat="server" Text="Objek Sebagai" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblKodObjSbg" runat="server" Text='<%# Eval("KodObjSbg")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblButObjSbg" runat="server" Text='<%# Eval("ButObjSbg")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblKodObjAm" runat="server" Text='<%# Eval("KodObjAm")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblButObjAm" runat="server" Text='<%# Eval("ButObjAm")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblAmaun" runat="server" Text="Amaun (RM)" />&nbsp;
                            <asp:LinkButton ID="lnkAmaun" runat="server" CommandName="Sort" CommandArgument="Amaun"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmaun" runat="server" Text='<%# Eval("Amaun")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <HeaderTemplate>
                                                <asp:Label ID="lblJenis" runat="server" Text="Jenis Agih" />&nbsp;
                            <asp:LinkButton ID="lnkJenis" runat="server" CommandName="Sort" CommandArgument="Jenis"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblJenis" runat="server" Text='<%# Eval("Jenis")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTkhMohon" runat="server" Text="Tarikh Mohon" />&nbsp;
                            <asp:LinkButton ID="lnkTkhMohon" runat="server" CommandName="Sort" CommandArgument="TkhMohon"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTkhMohon" runat="server" Text='<%# Eval("TkhMohon")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text="Status" />&nbsp;
                            <asp:LinkButton ID="lnkStatus" runat="server" CommandName="Sort" CommandArgument="Status"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblButiran" runat="server" Text='<%# Eval("Butiran")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih">
                                <i class="fas fa-edit"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="5%" />
                                        </asp:TemplateField>

                                    </Columns>

                                    <HeaderStyle BackColor="#6699FF"></HeaderStyle>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="divDt" runat="server" visible="false">
                    <div class="row">
                        <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn " Width="180px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali ke Senarai
                        </asp:LinkButton>
                    </div>

                    <div class="panel panel-default">
                        <div class="panel-heading">Maklumat Terperinci</div>
                        <div class="panel-body">
                            <div class="row">
                                <label class="control-label" for="">
                                    Tahun :
                                </label>
                                &nbsp;
                            <asp:TextBox ID="txtTahun" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px;"></asp:TextBox>

                            </div>




                            <div class="row">
                                <table style="width: 100%;">

                                    <tr>
                                        <td style="width: 150px; height: 22px;">Jenis Peruntukan :</td>
                                        <td style="height: 22px">

                                            <asp:Label ID="lblJenis" runat="server" Text=""></asp:Label>

                                            <asp:Label ID="lblKodJenis" runat="server" Text="" Visible="false"></asp:Label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                                Tarikh :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTarikh" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                                Kumpulan Wang :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtKodKW" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 50px; text-align: left;"></asp:TextBox>
                                            &nbsp;-
                          <asp:TextBox ID="txtKw" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 400px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                                Kod Operasi :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtKodKo" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 50px; text-align: left;"></asp:TextBox>
                                            &nbsp;-
                          <asp:TextBox ID="txtKo" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                                PTJ :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtKodPTj" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                            &nbsp;-
                          <asp:TextBox ID="txtPTj" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 600px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                                Kod Projek :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtKodKP" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                            &nbsp;-
                          <asp:TextBox ID="txtKP" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 200px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                                Objek Am :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtKodObjAm" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                            &nbsp;-
                          <asp:TextBox ID="txtObjAm" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 300px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                                Objek Sebagai :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtKodObjSbg" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 100px; text-align: left;"></asp:TextBox>
                                            &nbsp;-
                          <asp:TextBox ID="txtObjSbg" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" Style="width: 300px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                                Baki Peruntukan (RM) :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBaki" runat="server" ReadOnly="True" CssClass="form-control rightAlign" Style="width: 100px;" BackColor="#FFFFCC"></asp:TextBox>


                                        </td>
                                    </tr>


                                    <tr style="height: 25px">
                                        <td style="height: 22px; vertical-align: top;">
                                            <label class="control-label" for="">
                                                Butiran :
                                            </label>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" TextMode="multiline" Width="60%" Height="50px" ReadOnly="True" BackColor="#FFFFCC"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 25px">
                                        <td style="height: 25px;">
                                            <label class="control-label" for="">
                                                Amaun (RM) :
                                            </label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmaun" runat="server" CssClass="form-control rightAlign" Width="100px" BackColor="#FFFFCC" ReadOnly="true"></asp:TextBox>
                                            &nbsp;&nbsp;
                          <label id="lblMsgBaki" class="control-label" for="" style="display: none; color: #820303;">
                              Baki tidak mencukupi
                          </label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td colspan="2"></td>
                                    </tr>
                                </table>
                            </div>


                        </div>



                    </div>
                </div>

            </ContentTemplate>
            </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="background-color: #D2D2D2; filter: alpha(opacity=80); opacity: 0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">
            </div>
            <div style="margin: auto; font-family: Trebuchet MS; filter: alpha(opacity=100); opacity: 1; font-size: small; vertical-align: middle; color: #FFFFFF; position: fixed; top: 50%; left: 50%; margin-top: -50px; margin-left: -100px;">
                <table>
                    <tr>
                        <td>
                            <img src="../../../Images/loader.gif" alt="" />
                        </td>
                    </tr>
                </table>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
