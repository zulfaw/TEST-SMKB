<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Proses_Pemiutang.aspx.vb" Inherits="SMKB_Web_Portal.Proses_Pemiutang" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <style>
        @media (min-width: 1450px){
            .panel
            {
                width :70%;
            }
            .row
            {
                width :70%;
            }
        }

        @media (max-width: 1450px) {
            .panel {
                width: 80%;
            }        
        }

        @media (max-width: 1310px) {
              .panel {
                  width: 90%;
              }
          }

          @media (max-width: 1050px) {
              .panel {
                  width: 100%;
              }
          }         
    </style>

    <script>
        function fConfirm() {

            try {
                if (confirm('Anda pasti untuk meneruskan proses?')) {
                    return true;
                } else {
                    return false;
                }
            }
            catch (err) {
                alert(err)
                return false;
            }

        }

        function Validate() {
            debugger;
            var isValid = false;
            try{
               
               
                
                Page_BlockSubmit = false;
                return isValid;

            }
            catch (ex) {
                alert(ex)
                return false
            }
        }


    </script>


 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
      
    <div class="row">
      <div class="panel panel-default">
      <div class="panel-body">
        <table class="nav-justified">
              <tr>
                  <td style="width: 100px"><label class="control-label" for="">Kategori:</label></td>
                  <td>
                      <asp:DropDownList ID="ddlKat" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 20%;">
                      </asp:DropDownList>
                  </td>
              </tr>
             
             <tr style="height:25px">
                  <td ><label class="control-label" for="">PTJ:   
                  </td>
                  <td>
                    <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 95%;">
                    </asp:DropDownList>
                  </td>
              </tr>
             <tr style="height:25px">
                  <td><label class="control-label" for="">Tapisan   
                  </td>
                  <td>
                    <asp:DropDownList ID="ddlTapisan" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 70%;">
                    </asp:DropDownList>
                  </td>
              </tr>
             <tr style="height:25px;" id="NoInv" runat="server" visible="false" >
                  <td><label class="control-label" for=""> 
                  </td>
                  <td>
                      <asp:TextBox ID="txtNoInv" runat="server"/> &nbsp;&nbsp;Hingga&nbsp;&nbsp;<asp:TextBox ID="txtNoInv2" runat="server"/>
                  </td>
              </tr>
            <tr style="height:25px;" id="TrkInv" runat="server" visible="false" >
                  <td><label class="control-label" for=""> 
                  </td>
                  <td>
                      <asp:TextBox ID="dtTerima" runat="server"/> &nbsp;&nbsp;Hingga&nbsp;&nbsp;<asp:TextBox ID="dtTerima2" runat="server"/>
                  </td>
              </tr>
            <tr style="height:25px;" id="Pemb" runat="server" visible="false" >
                  <td><label class="control-label" for=""> 
                  </td>
                  <td>
                      <asp:DropDownList ID="cmbPembekal" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 30%;">
                    </asp:DropDownList> &nbsp;&nbsp;Hingga&nbsp;&nbsp; <asp:DropDownList ID="cmbPembekal2" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 30%;">
                    </asp:DropDownList>
                  </td>
              </tr>
            <tr style="height:25px;" id="KumpW" runat="server" visible="false" >
                  <td><label class="control-label" for=""> 
                  </td>
                  <td>
                      
                       <asp:DropDownList ID="CmbKW" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 70%;">
                    </asp:DropDownList>
                  </td>
              </tr>
            <tr style="height:55px;">
                    <td>
                        &nbsp;</td>
                    <td>
                        
                        <asp:LinkButton ID="lbtnPapar" runat="server" CssClass="btn "  ValidationGroup="btnPapar">
									<i class="far fa-list-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Papar
								</asp:LinkButton>
                    </td>
            </tr>
            
          </table>
        </div>
        </div>
</div>
<div class="row">
    
    <div class="GvTopPanel"  style="height: 33px;">
                <div style="float: left; margin-top: 5px; margin-left: 10px;">
                    &nbsp;&nbsp;<asp:CheckBox runat="server"></asp:CheckBox> Semua&nbsp;&nbsp;&nbsp;&nbsp;
                      <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id="lblJumRekod" runat="server" style="color: mediumblue;"></label>
                                    &nbsp;&nbsp;   <b style="color: #969696;">|</b> &nbsp;&nbsp;
                <label class="control-label" for="Klasifikasi">Saiz Rekod :</label>
                                    <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="25" Value="25" Selected="True" />
                                        <asp:ListItem Text="50" Value="50" />
                                        <asp:ListItem Text="100" Value="100" />
                                    </asp:DropDownList>&nbsp;&nbsp;
                    <label class="control-label" for="Klasifikasi">Jumlah Amauan (RM) :  <asp:Label ID="lblJum" Text="" runat="server" /> </label>
                <%--<label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>--%>
                    </div>  
                     
                        </div>           
      <asp:GridView ID="gvProsesPemiutang" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" 
        CssClass= "table table-striped table-bordered table-hover" Width="100%"  Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True" AllowPaging="True" EmptyDataText=" "  PageSize="25" >
          <columns>
          <asp:TemplateField HeaderText = "Pilih" >
                <ItemTemplate>
                  <asp:CheckBox runat="server" ID="chkRow"></asp:CheckBox>
                    <asp:Label ID="lblNoID" runat="server" Visible="false" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("AP01_NoId"))) %>' />
                </ItemTemplate>
                 <ItemStyle Width="5%" HorizontalAlign="Center" />
            </asp:TemplateField>
              
            <asp:TemplateField HeaderText = "Bil" >
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
            
        </ItemTemplate>
                                      <ItemStyle Width="5%" HorizontalAlign="Center" />
    </asp:TemplateField>

            <asp:BoundField HeaderText="KW" DataField="kodKW" ReadOnly="True" >
            <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:BoundField>

            <asp:BoundField HeaderText="Invois" DataField="Invois" >
              <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:BoundField>

              <asp:BoundField HeaderText="Tarikh Invois" DataField="TkhInv" >
              <ItemStyle HorizontalAlign="Center" Width="5%" />
            </asp:BoundField>

            <asp:BoundField HeaderText="No. PT" DataField="NoPt" >
              <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:BoundField>

              <asp:BoundField DataField="Pembekal" HeaderText="Pembekal" >

              <ItemStyle HorizontalAlign="Center" Width="10%" />
              </asp:BoundField>

         

            <asp:BoundField HeaderText="Amaun (RM)" DataField="Amaun" SortExpression="Amaun" >
            <ItemStyle Width="20%" HorizontalAlign="Right" />
            </asp:BoundField>

            
                  <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
                                                        <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="3%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
               </columns>
           
          <HeaderStyle BackColor="#6699FF" />

          <RowStyle Height="5px" />

          <SelectedRowStyle  ForeColor="Blue" />

    </asp:GridView>
                      
    </div>
   <div class="row">
        <div class="panel panel-default">
            <div class="panel-body">
                <table class="nav-justified">
                    <tr>
                          <td style="width: 200px">
                              <asp:RadioButton ID="Thn1" runat="server"  Text-="Tahun Semasa" GroupName="ThnJ" /> &nbsp;&nbsp;</td>
                          <td>
                               <asp:RadioButton ID="Thn" runat="server" text="Melangkau Tahun" GroupName="ThnJ"/>&nbsp;&nbsp;
                          </td>
                    </tr>
                    <tr>
                          <td >
                              No Jurnal :  <asp:TextBox ID="lblNoJurnal" runat="server"></asp:TextBox></td>
                          <td>
                    </tr>

                 </table>
            </div>
         </div>



   </div>
         <div style="text-align:center;" >
              <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm();">
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
								</asp:LinkButton>
          </div>   
              

                 <asp:Button ID="btnPopup1" runat="server" style="display:none;"   />                
                    <ajaxToolkit:ModalPopupExtender ID="mpeLstInv" runat="server" BackgroundCssClass="modalBackground" PopupControlID="PnlLstInv" TargetControlID="btnPopup1" CancelControlID="lnkBtnBack">
                                     </ajaxToolkit:ModalPopupExtender>                       
            <asp:Panel ID="PnlLstInv" runat="server" BackColor="White" Width="800px" style="display:none;">
                <table  style="border: Solid 3px #D5AA00; width: 100%; height: 100%;">
                    <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                         <td style="height: 10%;text-align:center;" class="">
                            <b> Senarai Invois</b></td>
                        <td style="width:50px;text-align:center;">   
                           <button runat="server" id="Button2" title="Tutup" class="btnNone ">
    <i class="far fa-window-close fa-2x"></i>
</button></td>
                                                                            
                    </tr>
    <tr>
        <td>
        <div style="margin-top:20px;">

                    <div class="row">
                    <asp:LinkButton ID="lnkBtnBack" runat="server" CssClass="btn " Width="100px" ToolTip="">
						<i class="fas fa-long-arrow-alt-left  fa-lg"></i> &nbsp;&nbsp; Kembali
                    </asp:LinkButton>
                   </div>
                <div class="panel panel-default" style="width: 95%;">
                            <div class="panel-heading">
                                <h3 class="panel-title">Maklumat Invois</h3>
                            </div>
                            <div class="panel-body">

                                <asp:GridView ID="gvInvDt" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
                                    CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" BorderStyle="Solid" ShowFooter="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="2%" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="KodKw" runat="server" Text='<%# Eval("KodKw")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                         <asp:BoundField HeaderText="KW" DataField="kodKW" ReadOnly="True" >
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                         </asp:BoundField>

                                         <asp:BoundField HeaderText="PTj" DataField="KodPtj" ReadOnly="True" >
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                         </asp:BoundField>

                                        <asp:BoundField HeaderText="Vot" DataField="kodVot" ReadOnly="True" >
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                         </asp:BoundField>


                                  

                                        <asp:TemplateField HeaderText="Kadar Harga (RM)">
                                            <ItemTemplate>
                                                <asp:Label ID="txtKHarga" runat="server" Text='<%#Eval("AP01_KadarHarga", "{0:###,###,###.00}")%>' />
                                            </ItemTemplate>
                                            
                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                             <FooterTemplate>
                                                <div style="text-align: right; font-weight: bold;">
                                                    <asp:Label runat="server"  text="Jumlah"/>
                                                   
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amaun Asal (RM)">
                                            <ItemTemplate>
                                                <asp:Label ID="txtHargaAsal" runat="server" Text='<%#Eval("AP01_AmaunAkanByr", "{0:###,###,###.00}")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                             <FooterTemplate>
                                                <div style="text-align: right; font-weight: bold;">
                                                    <asp:Label runat="server"  ID="JumDt" />
                                                   
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Invois Amaun (RM)">
                                            <ItemTemplate>
                                                <asp:Label ID="txtHargaInv" runat="server" Text='<%#Eval("AP01_AmaunAkanByr", "{0:###,###,###.00}")%>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" Width="150px" />
                                              <FooterTemplate>
                                                <div style="text-align: right; font-weight: bold;">
                                                    <asp:Label runat="server"  ID="JumInv" />
                                                   
                                                </div>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                    </Columns>
                                    <EmptyDataTemplate>
                                    </EmptyDataTemplate>
                                    <SelectedRowStyle ForeColor="Blue" />
                                </asp:GridView>

                            </div>
                        </div>
    </div>
            </td>
        </tr>
                    </table>
                </asp:Panel>
                <%--<div class="panel panel-default" style="width:95%;">
                    <div class="panel-heading">
                        <h3 class="panel-title">Muat Naik Dokumen Sokongan</h3>
                    </div>
                    <div class="panel-body">
                        <div class="well" >
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="450px" />
                            <div style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnUpload" runat="server" CssClass="btn " Width="100px">
						<i class="fas fa-file-upload fa-lg"></i> &nbsp; Muat Naik
                                </asp:LinkButton>                             
                            </div>
                        </div>

                        <div class="row">
                            <asp:GridView ID="gvLamp" runat="server" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" BorderStyle="Solid"
                                CssClass="table table-striped table-bordered table-hover" EmptyDataText=" " Font-Size="8pt" HeaderStyle-BackColor="#6699FF"
                                Height="100%" PageSize="10" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="600px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Bil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="3%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblId" runat="server" Text='<%# Eval("AR11_ID")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoBilSem" runat="server" Text='<%# Eval("AR01_NoBilSem")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nama Dokumen">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNamaDok" runat="server" Text='<%# Eval("AR11_NamaDok")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDokPath" runat="server" Text='<%# Eval("AR11_Path")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("AR11_Status")%>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%# Eval("GUID") %>' CommandName="Delete" CssClass="btn-xs" OnClientClick="return confirm('Anda pasti untuk padam lampiran ini?');" ToolTip="Padam">
										                    <i class="far fa-trash-alt fa-lg"></i>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Lihat Lampiran">
                              <i class="fab fa-readme fa-lg"></i>                           
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#6699FF" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>--%>

               
             
            </div>
    
    </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
