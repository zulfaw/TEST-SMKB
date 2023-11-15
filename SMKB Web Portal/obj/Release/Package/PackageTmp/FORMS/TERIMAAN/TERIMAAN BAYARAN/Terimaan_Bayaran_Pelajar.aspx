<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Terimaan_Bayaran_Pelajar.aspx.vb" Inherits="SMKB_Web_Portal.Terimaan_Bayaran_Pelajar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    
    <script type="text/javascript">

        function fCloseKatPel() {
            $find("mpe1").hide();
        }

        function fClosePraSiswa() {
            $find("mpe2").hide();
        }

        function fCloseSiswaPG() {
            $find("mpe3").hide();
        }

        function fClosebil() {
            $find("mpe4").hide();
        }
    </script>

    <%--<div class="row">
				<asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
					<i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
				</asp:LinkButton>
			</div>
			<br />--%>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            
                <div class="row">

                    <div class="panel panel-default">
                        
                        <div class="panel-heading">Maklumat Penerima</div>
                        <div class="panel-body">
                            <table class="table table-borderless table-striped">
                                <tr>
                                    <td style="width: 106px">Tahun</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblTahun" runat="server" ></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Tarikh Terima</td>
                                    <td >:</td>
                                    <td>
                                        <asp:label ID="lblTkhMhn" runat="server" ></asp:label>
                                    </td>

                                </tr>
                                <tr>
                                    <td style="width: 106px">PTJ</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblKodPTJ" runat="server" ></asp:label>
                                        &nbsp;-&nbsp;<asp:label ID="lblNamaPTJ" runat="server"></asp:label>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 106px; height: 23px;">Nama Penerima</td>
                                    <td style="height: 23px">:</td>
                                    <td style="height: 23px">
                                        <asp:label ID="lblNoPmhn" runat="server" ></asp:label>
                                        &nbsp;-&nbsp;<asp:label ID="lblNamaPemohon" runat="server" ></asp:label>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Jawatan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblJawatan" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Jenis Terimaan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblJnsTerimaan" runat="server"></asp:label>
                                        s</td>
                                </tr>
                                
                            </table>
                        </div>
                    </div>
                </div>

                <div class="row">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Maklumat Terimaan
                        </div>
                        <div class="panel-body">
                            <table class="table table-borderless table-striped">
                                <tr>
                                    <td style="width:12%;">No Resit :</td>
                                    <td style="width:40%;"><asp:TextBox ID="txtNoResit" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="100%"></asp:TextBox>
                                        <asp:HiddenField ID="hdNoDok" runat="server" />
                                        <asp:HiddenField ID="hdNoDokSem" runat="server" />
                                        <asp:HiddenField ID="hdNosiriCek" runat="server" />
                                    </td>
                                    <td style="width:2%;">&nbsp;</td>
                                    <td style="width:12%;"">Bank UTeM :</td>
                                    <td style="width:30%;""><asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvddlBankUtem" runat="server" ControlToValidate="ddlBank" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnModTerimaan" Display="Dynamic" ></asp:RequiredFieldValidator></td>

                                </tr>
                                <tr>
                                    <td>Jenis :</td>
                                    <td style="width: 40%"><asp:DropDownList ID="ddlJenis" runat="server" AutoPostBack="True" CssClass="form-control" Width="120px"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvddlJenis" runat="server" ControlToValidate="ddlJenis" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                    <asp:label ID="lblKategori" runat="server"></asp:label>
                                    </td>
                                    <td style="width: 2%">&nbsp;</td>
                                    <td>Mod Terimaan</td>
                                    <td><asp:DropDownList ID="ddlModTerimaan" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvddlModTerimaan" runat="server" ControlToValidate="ddlModTerimaan" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                        <asp:LinkButton ID="lbtnModTerimaan" runat="server" CssClass="btn-xs" ToolTip="Pilih Mod Terima" Visible="false">
											<i class="fas fa-plus-square fa-lg"></i>
										</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Jenis Urusniaga :</td>
                                    <td style="width: 40%"><asp:DropDownList ID="ddlJnsUrusniaga" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvddlJnsUrusniaga" runat="server" ControlToValidate="ddlJnsUrusniaga" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator></td>
                                    <td style="width: 2%">&nbsp;</td>
                                    <td rowspan="2" style="vertical-align:top">Tujuan :</td>
                                    <td rowspan="2"><asp:TextBox ID="txtTujuan" runat="server" TextMode="MultiLine" CssClass="form-control" Width="100%" Rows="3"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtTujuan" runat="server" ControlToValidate="txtTujuan" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="hdTarikh" runat="server" />
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td>Pembayar :</td>
                                    <td style="width: 40%"><asp:DropDownList ID="ddlPembayar" runat="server" AutoPostBack="True" CssClass="form-control" Width="85%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvddlPembayar" runat="server" ControlToValidate="ddlPembayar" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator>
                                    &nbsp; <asp:LinkButton ID="lbtnTambahOA" runat="server" CssClass="btn-circle" ToolTip="Tambah Orang Awam" Visible="false">
                                            <i class="fas fa-plus-square fa-lg" aria-hidden="true"></i>                                                
                                          </asp:LinkButton>
                                        </td>
                                    <td style="width: 2%">&nbsp;</td>                                    
                                </tr>
                                
                            </table>
                            <br />

                            <div class="panel panel-default" style="width: auto;overflow-x:auto;">
                                <div class="panel-heading">
                                    Butiran
                                </div>
                                <div class="panel-body">
                                   
		
					<asp:GridView ID="gvTerimaan" datakeyname="" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
                                            CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" BorderStyle="Solid" ShowFooter="True" Width="100%" DataKeyNames="ID">
                                            <Columns>
                                                <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Right"><ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate></asp:TemplateField>
                                                 <asp:TemplateField HeaderText="KW" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblKw" runat ="server" text='<%# IIf(IsDBNull(Eval("kodKw")), "", Eval("kodKw")) %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlKW" runat="server" CssClass="form-control" style="width:45px;" AutoPostBack="true" ></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KO" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblKo" runat ="server" text='<%# IIf(IsDBNull(Eval("kodKo")), "", Eval("kodKo")) %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlKo" runat="server" CssClass="form-control" style="width:45px;" AutoPostBack="true" ></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PTj" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblPTj" runat ="server" text='<%# IIf(IsDBNull(Eval("KodPTJ")), "", Eval("KodPTJ")) %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlPTj" runat="server" CssClass="form-control" style="width:70px;" AutoPostBack="true" ></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="KP" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblKP" runat ="server" text='<%# IIf(IsDBNull(Eval("KodKP")), "", Eval("KodKP")) %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlKP" runat="server" CssClass="form-control" style="width:75px;" AutoPostBack="true" ></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Vot" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblVot" runat ="server" text='<%# IIf(IsDBNull(Eval("KodVot")), "", Eval("KodVot")) %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlVot" runat="server" CssClass="form-control" style="width:65px;" AutoPostBack="true" ></asp:DropDownList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Butiran" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblButiran" runat ="server" text='<%# IIf(IsDBNull(Eval("RC01_Butiran")), "", Eval("RC01_Butiran")) %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" TextMode="MultiLine" style="width:100%;" Text='<%# Eval("RC01_Butiran") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Debit" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblDebit" runat ="server" text='<%# Eval("RC01_Debit", "{0:N2}") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                        <asp:TextBox ID="txtDebit" runat="server" CssClass="form-control rightAlign" style="width:90px;" Text='<%# Eval("RC01_Debit", "{0:N2}") %>' TextMode="Number"></asp:TextBox>
                                                    </EditItemTemplate>--%>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kredit" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label id="lblKredit" runat ="server" text='<%# Eval("RC01_Kredit", "{0:N2}") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                        <asp:TextBox ID="txtKredit" runat="server" CssClass="form-control rightAlign" style="width:90px;" Text='<%# Eval("RC01_Kredit", "{0:N2}") %>' TextMode="Number"></asp:TextBox>
                                                    </EditItemTemplate>--%>
                                                </asp:TemplateField>
							                   <%-- <asp:TemplateField ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Center" HeaderText="Tindakan" FooterStyle-HorizontalAlign="Center">
                                                    <EditItemTemplate>											
								                        <asp:LinkButton ID="lbtnSave" runat="server" CommandName="Update" CssClass="btn-xs" ToolTip="Simpan">
												            <i class="far fa-save fa-lg"></i>
											                    </asp:LinkButton>
												                    &nbsp;
											                     <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" CssClass="btn-xs" ToolTip="Undo">
												                    <i class="fas fa-undo fa-lg"></i>
											                    </asp:LinkButton>
										                    </EditItemTemplate>
										                    <ItemTemplate>
											                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CssClass="btn-xs" ToolTip="Kemas Kini">
												                <i class="far fa-edit fa-lg"></i>
											                </asp:LinkButton>
                                                                    &nbsp;
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
											                    OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										                    <i class="far fa-trash-alt fa-lg"></i>
										                    </asp:LinkButton>
                                                    </ItemTemplate>
							                    </asp:TemplateField>--%>
                                                
                                            </Columns>
                            <EmptyDataTemplate>

                            </EmptyDataTemplate>
                        <selectedrowstyle ForeColor="Blue" />
                                        </asp:GridView>				
						    <div style="text-align:right;">
                                <table class="table table-borderless table-striped">
                                    <tr>
                                        <td>
                                            Jumlah Kredit (RM) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtJumlahSebenar" runat="server" CssClass="form-control rightAlign" Width="100px" ReadOnly="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Jumlah Debit (RM) :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtJumBayaran" runat="server" CssClass="form-control rightAlign" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trEFT" runat="server" visible ="false">
                                        <td>
                                            No EFT :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNoEft" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                                <table id="tblCek" runat="server" visible="false" class="table table-borderless table-striped">
                                    <tr>
                                        <td>
                                            Bank Pembayar :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBankPembayar" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
                                        </td>
                                        <td>&nbsp</td>
                                        <td>
                                            No Cek/ Dokumen :
                                        </td>
                                        <td>
                                             <asp:TextBox ID="txtNoCek" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cawangan :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCawBank" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp</td>
                                        <td>
                                            Jenis Cek :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtJnsCek" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Tarikh Cek :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrkCek" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                                            <ajaxtoolkit:CalendarExtender ID="caltxtTrkCek" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTrkCek" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtntxtTrkCek" />
                                        <asp:LinkButton ID="lbtntxtTrkCek" runat="server" ToolTip="Klik untuk papar kalendar">
                                                <i class="far fa-calendar-alt fa-lg"></i>
                                        </asp:LinkButton>
                                        </td>
                                        <td>&nbsp</td>
                                        <td>
                                            Tarikh Terima Cek :
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTrkTerimaCek" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                                            <ajaxtoolkit:CalendarExtender ID="CaltxtTrkTerimaCek" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTrkTerimaCek" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtntxtTrkTerimaCek" />
                                        <asp:LinkButton ID="lbtntxtTrkTerimaCek" runat="server" ToolTip="Klik untuk papar kalendar">
                                                <i class="far fa-calendar-alt fa-lg"></i>
                                        </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
						    


                                </div>
                            </div>
                                 <div style="text-align:center">
                        <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');" ValidationGroup="lbtnHantar" >
                            <i class="fas fa-paper-plane"></i>&nbsp;&nbsp;&nbsp;Hantar
                        </asp:LinkButton>
                                     </div>
                            <div>


                            
        
            

                </div>                           
             </div></div>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID = "btnOpenKatPelajar" runat = "server" Text = ""  style="display:none" />
        <ajaxToolkit:ModalPopupExtender ID="MPEKatPelajar" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe1" 
            CancelControlID="btnCloseKatPelajar" PopupControlID="pnlKatPelajar" TargetControlID="btnOpenKatPelajar"> </ajaxToolkit:ModalPopupExtender>
        
                     <asp:Panel ID="pnlKatPelajar" runat="server" BackColor="White" Width="300px" Height="80px">
                         <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >                  
                   
                       <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                           <td colspan="2" style="height: 10%;  font-weight: bold; text-align:center;">Maklumat Lanjut </td>
                           <td style="width: 10px; text-align: center;">
                               <button id="btnCloseKatPelajar" runat="server" class="btnNone " title="Tutup" onclick="fCloseKatPel();">
                                   <i class="far fa-window-close fa-2x"></i>
                               </button>
                           </td>
                       </tr>
                                       
                       <tr style="height:35px;">                           
                           <td colspan="3">
                            <asp:RadioButtonList ID="rbKatPel" runat="server" Height="35px" RepeatDirection="Horizontal" Width="273px" AutoPostBack="true">
                                    <asp:ListItem Text="Pra Siswazah" Value=0 />
                                    <asp:ListItem Text="Pasca Siswazah" Value=1 />
                                </asp:RadioButtonList>
                           </td>
                       </tr>
                </table>

                </ContentTemplate>
                             
        </asp:UpdatePanel>
            </asp:Panel>


    <asp:Button ID = "btnOpenPraSiswa" runat = "server" Text = ""  style="display:none;" />
        <ajaxToolkit:ModalPopupExtender ID="MPEPraSiswa" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe2" 
            CancelControlID="btnClosePraSiswa" PopupControlID="pnlPraSiswa" TargetControlID="btnOpenPraSiswa"> </ajaxToolkit:ModalPopupExtender>
        
                     <asp:Panel ID="pnlPraSiswa" runat="server" BackColor="White" Width="600px" Height="600px" style="overflow:auto;">
                         <asp:UpdatePanel runat="server" ID="UpdatePanel6">
            <ContentTemplate>
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >                  
                   
                       <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                           <td colspan="2" style="height: 10%;  font-weight: bold; text-align:center;">Maklumat Pra Siswazah </td>
                           <td style="width: 10px; text-align: center;">
                               <button id="btnClosePraSiswa" runat="server" class="btnNone " title="Tutup" onclick="fClosePraSiswa();">
                                   <i class="far fa-window-close fa-2x"></i>
                               </button>
                           </td>
                       </tr>
                                       
                       <tr>                           
                           <td colspan="3" style="height: 30px">
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodPraSiswa" runat="server" class="control-label" style="color:mediumblue;"></label>
                           &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekodPraSiswa" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true"/>
                                   <asp:ListItem  Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>
                               &nbsp;&nbsp;
                           Cari bulan : 
                           &nbsp;&nbsp;                               
                               <asp:DropDownList ID="ddlCariPraSiswa" runat="server" Width="150px" class="form-control">                                    
                                    <asp:ListItem Text="-Sila Pilih-" Value=0 selected="True"/>
                                   <asp:ListItem Text="1" Value=1/>
                                    <asp:ListItem Text="2" Value=2/>
                                   <asp:ListItem Text="3" Value=3/>
                                    <asp:ListItem Text="4" Value=4/>
                                   <asp:ListItem Text="5" Value=5/>
                                    <asp:ListItem Text="6" Value=6/>
                                   <asp:ListItem Text="7" Value=7/>
                                    <asp:ListItem Text="8" Value=8/>
                                   <asp:ListItem Text="9" Value=9/>
                                    <asp:ListItem Text="10" Value=10/>
                                   <asp:ListItem Text="11" Value=11/>
                                    <asp:ListItem Text="12" Value=12/>                                   
                                </asp:DropDownList>
                           &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCariPraSiswa" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariPraSiswa">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                      </asp:LinkButton>                         
                           </td>
                       </tr>
                   
                   
                    <tr style="vertical-align:top;">
                        
                        <td colspan="3">
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                              <asp:GridView ID="gvPraSiswa" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" PageSize="25">
                    <columns>
                    <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Mod Bayar" DataField="SMP56_ModBayar" SortExpression="SMP56_ModBayar" ItemStyle-Width="20%"/>			            
					<asp:BoundField HeaderText="Tarikh Bayar" DataField="Tarikh" SortExpression="Tarikh" ItemStyle-Width="20%" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center"/>
					<asp:BoundField HeaderText="Jumlah" DataField="SMP56_JumBayar" SortExpression="SMP56_JumBayar" ItemStyle-Width="20%" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:BoundField HeaderText="No Siri" DataField="nosiri" SortExpression="nosiri" ItemStyle-Width="20%"/>
                    <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">                                                    
						<ItemTemplate>
							<asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
							</asp:LinkButton>                                                                   
                        </ItemTemplate>                                                    
					</asp:TemplateField>
                </columns>
            </asp:GridView>
                          </div>
                        </td>
                    </tr>                  
                   
                </table>

                </ContentTemplate>
                             
        </asp:UpdatePanel>
            </asp:Panel>

    <asp:Button ID = "btnOpenSiswaPG" runat = "server" Text = ""  style="display:none;" />
        <ajaxToolkit:ModalPopupExtender ID="MPESiswaPG" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe3" 
            CancelControlID="btnCloseSiswaPG" PopupControlID="pnlSiswaPG" TargetControlID="btnOpenSiswaPG"> </ajaxToolkit:ModalPopupExtender>
        
                     <asp:Panel ID="pnlSiswaPG" runat="server" BackColor="White" Width="600px" Height="600px" style="overflow:auto;">
                         <asp:UpdatePanel runat="server" ID="UpdatePanel7">
            <ContentTemplate>
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >                  
                   
                       <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                           <td colspan="2" style="height: 10%;  font-weight: bold; text-align:center;">Maklumat Pasca Siswazah</td>
                           <td style="width: 10px; text-align: center;">
                               <button id="btnCloseSiswaPG" runat="server" class="btnNone " title="Tutup" onclick="fCloseSiswaPG();">
                                   <i class="far fa-window-close fa-2x"></i>
                               </button>
                           </td>
                       </tr>
                                       
                       <tr>                           
                           <td colspan="3" style="height: 30px">
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodSiswaPG" runat="server" class="control-label" style="color:mediumblue;"></label>
                           &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekodSiswaPG" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true"/>
                                   <asp:ListItem  Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>
                               &nbsp;&nbsp;
                           Cari bulan : 
                           &nbsp;&nbsp;                               
                               <asp:DropDownList ID="ddlCariPascaSiswa" runat="server" Width="150px" class="form-control">                                    
                                    <asp:ListItem Text="-Sila Pilih-" Value=0 selected="True"/>
                                   <asp:ListItem Text="1" Value=1/>
                                    <asp:ListItem Text="2" Value=2/>
                                   <asp:ListItem Text="3" Value=3/>
                                    <asp:ListItem Text="4" Value=4/>
                                   <asp:ListItem Text="5" Value=5/>
                                    <asp:ListItem Text="6" Value=6/>
                                   <asp:ListItem Text="7" Value=7/>
                                    <asp:ListItem Text="8" Value=8/>
                                   <asp:ListItem Text="9" Value=9/>
                                    <asp:ListItem Text="10" Value=10/>
                                   <asp:ListItem Text="11" Value=11/>
                                    <asp:ListItem Text="12" Value=12/>                                   
                                </asp:DropDownList>
                           &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCariPascaSiswa" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariPraSiswa">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                      </asp:LinkButton>                         
                           </td>
                       </tr>
                   
                   
                    <tr style="vertical-align:top;">
                        
                        <td colspan="3">
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                              <asp:GridView ID="gvSiswaPG" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" PageSize="25">
                    <columns>
                    <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Mod Bayar" DataField="SMG31_ModBayar" SortExpression="SMG31_ModBayar" ItemStyle-Width="20%"/>			            
					<asp:BoundField HeaderText="Tarikh Bayar" DataField="Tarikh" SortExpression="Tarikh" ItemStyle-Width="20%" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center"/>
					<asp:BoundField HeaderText="Jumlah" DataField="SMG31_JumBayar" SortExpression="SMG31_JumBayar" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N2}"/>
                    <asp:BoundField HeaderText="No Siri" DataField="nosiri" SortExpression="nosiri" ItemStyle-Width="20%"/>
                    <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">                                                    
						<ItemTemplate>
							<asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
							</asp:LinkButton>                                                                   
                        </ItemTemplate>                                                    
					</asp:TemplateField>
                </columns>
            </asp:GridView>
                          </div>
                        </td>
                    </tr>                  
                   
                </table>

                </ContentTemplate>                             
        </asp:UpdatePanel>
            </asp:Panel>

    <asp:Button ID = "btnOpenBilTuntutan" runat = "server" Text = ""  style="display:none" />
        <ajaxToolkit:ModalPopupExtender ID="MPEBilTuntutan" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe4" 
            CancelControlID="btnClose1" PopupControlID="pnlBilTuntutan" TargetControlID="btnOpenBilTuntutan"> </ajaxToolkit:ModalPopupExtender>
        
                     <asp:Panel ID="pnlBilTuntutan" runat="server" BackColor="White" Width="70%" Height="70%" style="overflow:auto;">
                         <asp:UpdatePanel runat="server" ID="UpdatePanel3">
            <ContentTemplate>
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >                  
                   
                       <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                           <td colspan="2" style="height: 10%;  font-weight: bold; text-align:center;">Maklumat Lanjut </td>
                           <td style="width: 10px; text-align: center;">
                               <button id="btnClose1" runat="server" class="btnNone " title="Tutup" onclick="fClosebil();">
                                   <i class="far fa-window-close fa-2x"></i>
                               </button>
                           </td>
                       </tr>
                                       
                       <tr style="height:30px;">                           
                           <td colspan="3">
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekod" runat="server" class="control-label" style="color:mediumblue;"></label>
                           &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekod" runat="server" AutoPostBack="true" class="form-control" Width="60px">
                                   <asp:ListItem Text="10" Value="10" />
                                   <asp:ListItem Text="25" Value="25" Selected="true"/>
                                   <asp:ListItem Text="50" Value="50" />
                                   <asp:ListItem Text="100" Value="100" />
                                   <asp:ListItem Text="200" Value="200" />
                                   <asp:ListItem Text="500" Value="500" />
                                   <asp:ListItem Text="1000" Value="1000" />
                               </asp:DropDownList>
                           
                           &nbsp;&nbsp;
                           Cari : 
                           &nbsp;&nbsp;
                               <asp:TextBox ID="txtCari" runat="server" CssClass="form-control" Width="150px"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="rfvtxtCari" runat="server" ControlToValidate="txtCari" Display="Dynamic" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnCari"></asp:RequiredFieldValidator>
                               &nbsp;
                               <asp:DropDownList ID="ddlCariBil" runat="server" Width="150px" class="form-control">
                                    <asp:ListItem Text="Nama" Value=1 selected="True"/>
                                    <asp:ListItem Text="No Bil" Value=2 />
                                    <asp:ListItem Text="Id Penerima" Value=3 />
                                </asp:DropDownList>
                           &nbsp;&nbsp;&nbsp;
                               <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCari">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                      </asp:LinkButton>
                           </td>
                       </tr>
                   
                   
                    <tr style="vertical-align:top;">
                        
                        <td colspan="3">
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                              <asp:GridView ID="gvBil" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
                cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" ShowFooter="false" Font-Size="8pt" DataKeyNames="AR01_NoBilsem" PageSize="25">
                    <columns>
                    <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">                                    
                        <ItemTemplate>
                            <asp:RadioButton id="rbPilih" runat="server" GroupName="rbPilih" onclick = "RadioCheck(this);"/>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="right">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="No Bil" DataField="AR01_NoBil" SortExpression="AR01_NoBil" ItemStyle-Width="10%"/>			            
					<asp:BoundField HeaderText="Id Penerima" DataField="AR01_IdPenerima" SortExpression="AR01_IdPenerima" ItemStyle-Width="10%"/>
					<asp:BoundField HeaderText="Nama" DataField="AR01_NamaPenerima" SortExpression="AR01_NamaPenerima" ItemStyle-Width="40%"/>
                    <asp:BoundField HeaderText="Jumlah (RM)" DataField="AR01_JumBlmByr" SortExpression="AR01_JumBlmByr" DataFormatString="{0:N}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right"/>                                     
                    <asp:BoundField HeaderText="Jenis Pelajar" DataField="AR01_JenisPljr" SortExpression="AR01_JenisPljr" ItemStyle-Width="10%"/>
                    <asp:TemplateField ItemStyle-Width="3%" ItemStyle-CssClass="Center" HeaderText="Tindakan">                                                    
						<ItemTemplate>
							<asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih">
								<i class="far fa-edit fa-lg"></i>
							</asp:LinkButton>                                                                   
                        </ItemTemplate>                                                    
					</asp:TemplateField>
                </columns>
            </asp:GridView>
                          </div>
                        </td>
                    </tr>
                </table>

                </ContentTemplate>
                             
        </asp:UpdatePanel>
            </asp:Panel>
</asp:Content>
