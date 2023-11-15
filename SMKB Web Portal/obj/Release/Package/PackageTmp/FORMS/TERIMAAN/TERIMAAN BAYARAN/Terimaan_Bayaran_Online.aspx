<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Terimaan_Bayaran_Online.aspx.vb" Inherits="SMKB_Web_Portal.Terimaan_Bayaran_Online" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <script type="text/javascript">
        function fCloseTerimaan() {
            $find("mpe1").hide();
        }
        </script>

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
                                    <td style="width: 106px; height: 23px;">Nama Urusetia</td>
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
                                    <td style="width: 106px">Zon</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblZon" runat="server"></asp:label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 106px">Jenis Terimaan</td>
                                    <td>:</td>
                                    <td>
                                        <asp:label ID="lblJnsTerimaan" runat="server"></asp:label>
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                    </div>
                </div>

            <div class="row">
			<p></p>			
			<div class="panel panel-default" style="width:auto;">
				<div class="panel-heading">
					<h3 class="panel-title">
						Maklumat Terimaan Online (MIGS)
					</h3>
				</div>
				<div class="panel-body" style="overflow-x:auto">
                    <table >
            
            <tr style="height:30px;">
                <td>
                    Jumlah rekod :&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                </td>               
                <td style="width:40px;"></td>
                  <td>
                      Cari No Resit : 
                </td>
                
                <td style="width:110px;">
                      <asp:TextBox ID="txtCari" runat="server" class="form-control" Width="100px" ToolTip="Masukkan Nilai"></asp:TextBox>
                  </td>
                    
                    <td>                                  
                      <asp:LinkButton runat="server" ID="btnCari" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="btnCari">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                      </asp:LinkButton>         
                      
                  </td>
                  
              </tr>
        </table>
        <asp:GridView ID="gvTerimaan" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="true" DataKeyNames="RC04_ID" Font-Size="8pt">
								<columns>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField>                                 
								<asp:BoundField HeaderText="No Resit MIGS" DataField="RC04_NoBil" SortExpression="RC04_NoBil" itemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
								<asp:BoundField HeaderText="Tarikh Resit" DataField="RC04_TarikhBayar" SortExpression="RC04_TarikhBayar" itemStyle-HorizontalAlign="Center" ItemStyle-Width="8%" DataFormatString="{0:dd/MM/yyyy}"/>
								<asp:BoundField HeaderText="Maklumat Pembayar" DataField="ROC01_NamaSya" SortExpression="ROC01_NamaSya"  ItemStyle-Width="20%" />
                                <asp:BoundField HeaderText="Jenis Urusniaga" DataField="Butiran" SortExpression="Butiran"  ItemStyle-Width="10%" />                                
                                <asp:BoundField HeaderText="Tujuan" DataField="Tujuan" SortExpression="Tujuan" ItemStyle-Width="30%" />
                                <asp:BoundField HeaderText="Jumlah (RM)" DataField="RC04_Jumlah" SortExpression="RC04_Jumlah" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right" DataFormatString="{0:N2}"/>	                                                                                             
								<%--<asp:TemplateField>                        
								<ItemTemplate>
										<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>--%>
							</columns>
						</asp:GridView>
                    <br />
                    <table class="table table-borderless table-striped">
                                <tr>
                                    <td style="width:12%;">No Resit :</td>
                                    <td style="width:40%;"><asp:TextBox ID="txtNoResit" runat="server" BackColor="#FFFFCC" ReadOnly="true" CssClass="form-control" Width="100%"></asp:TextBox>
                                    </td>
                                    <td style="width:2%;">&nbsp;</td>
                                    <td style="width:12%;"">Bank UTeM :</td>
                                    <td style="width:30%;""><asp:DropDownList ID="ddlBank" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvddlBankUtem" runat="server" ControlToValidate="ddlBank" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnModTerimaan" Display="Dynamic" ></asp:RequiredFieldValidator></td>

                                </tr>
                                <tr>
                                    <td>Jenis :</td>
                                    <td style="width: 40%">
                                        <asp:LinkButton ID="lbtnTerimaan" runat="server" CssClass="btn btn-info" ToolTip="Paparan Terimaan untuk Kelulusan">
                                            <i class="fa fa-search fa-lg"></i>&nbsp;&nbsp;&nbsp;Terimaan
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 2%">&nbsp;</td>
                                    <td>Mod Terimaan</td>
                                    <td><asp:DropDownList ID="ddlModTerimaan" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%" Enabled="false"></asp:DropDownList>									
                                    </td>
                                </tr>
                        <tr>
                                    <td>Jenis Urusniaga :</td>
                                    <td><asp:DropDownList ID="ddlJnsUrusniaga" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList>
									<asp:RequiredFieldValidator ID="rfvddlJnsUrusniaga" runat="server" ControlToValidate="ddlJnsUrusniaga" ErrorMessage="" ForeColor="Red" Text="*Sila pilih" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator></td>
                                    <td style="width: 2%">&nbsp;</td>
                                    <td>Kategori Pembayar :</td>
                                    <td><asp:DropDownList ID="ddlKtgPembayar" runat="server" AutoPostBack="True" CssClass="form-control" Width="100%"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    
                                    <td rowspan="2">Tujuan :</td>
                                    <td rowspan="2"><asp:TextBox ID="txtTujuan" runat="server" TextMode="MultiLine" CssClass="form-control" Width="100%" Rows="4" onkeyup="upper(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtTujuan" runat="server" ControlToValidate="txtTujuan" ErrorMessage="" ForeColor="Red" Text="*Sila isi" ValidationGroup="lbtnHantar" Display="Dynamic" ></asp:RequiredFieldValidator></td>
                                    <td style="width: 2%">&nbsp;</td>
                                    <td>Pembayar :</td>
                                    <td><asp:textbox ID="txtPembayar" runat="server" readonly="true" CssClass="form-control" Width="100%" Text="BENDAHARI UTeM"></asp:textbox>								
                                        </td>
                                </tr>
                            </table>
        
                    <div class="panel panel-default" style="width: auto;overflow-x:auto;">
                                <div class="panel-heading">
                                    Butiran
                                </div>
                                <div class="panel-body">
                                   
		
					<asp:GridView ID="gvTerimaanDt" datakeyname="" runat="server" AllowSorting="true" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
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
						    <asp:HiddenField ID="hdJumlah" runat="server" />


                                </div>
                            </div>
                    <br />
                    <div style="text-align:center">
                        
                       
                        <asp:LinkButton ID="lbtnHantar" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk hantar rekod ini?');" ValidationGroup="lbtnHantar" >
                            <i class="fas fa-paper-plane fa-lg"></i>&nbsp;&nbsp;&nbsp;Hantar
                        </asp:LinkButton>
                                     </div>
                    <br />
				</div>
                </div>            
             </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Button ID = "btnOpenTerimaan" runat = "server" Text = ""  style="display:none;" />
        <ajaxToolkit:ModalPopupExtender ID="MPETerimaan" runat="server" BackgroundCssClass="modalBackground" BehaviorID="mpe1" 
            CancelControlID="btnCloseTerimaan" PopupControlID="pnlTerimaan" TargetControlID="btnOpenTerimaan"> </ajaxToolkit:ModalPopupExtender>
        
                     <asp:Panel ID="pnlTerimaan" runat="server" BackColor="White" Width="600px" Height="600px" style="overflow:auto;">
                         <asp:UpdatePanel runat="server" ID="UpdatePanel2">
            <ContentTemplate>
               <table style="border: Solid 3px #D5AA00; width: 100%; height: 100%; " >                  
                   
                       <tr style="background: linear-gradient(to bottom, #fceabb 0%,#fccd4d 50%,#f8b500 51%,#fbdf93 100%);">
                           <td colspan="2" style="height: 10%;  font-weight: bold; text-align:center;">Maklumat Terimaan</td>
                           <td style="width: 10px; text-align: center;">
                               <button id="btnCloseTerimaan" runat="server" class="btnNone " title="Tutup" onclick="fCloseTerimaan();">
                                   <i class="far fa-window-close fa-2x"></i>
                               </button>
                           </td>
                       </tr>
                                       
                       <tr>                           
                           <td colspan="3" style="height: 30px">
                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Jumlah rekod :&nbsp;<label id="lblJumRekodTerimaan" runat="server" class="control-label" style="color:mediumblue;"></label>
                           &nbsp;|&nbsp;&nbsp;Saiz Rekod : &nbsp;&nbsp;                           
                               <asp:DropDownList ID="ddlSaizRekodTerimaan" runat="server" AutoPostBack="true" class="form-control" Width="60px">
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
                               <asp:DropDownList ID="ddlCariTerimaan" runat="server" Width="150px" class="form-control">                                    
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
                               <asp:LinkButton ID="lbtnCariTerimaan" runat="server" CssClass="btn-circle" ToolTip="Cari" ValidationGroup="lbtnCariTerimaan">
                        <i class="fa fa-search fa-2x" aria-hidden="true"></i>
                      </asp:LinkButton>                         
                           </td>
                       </tr>
                   
                   
                    <tr style="vertical-align:top;">
                        
                        <td colspan="3">
                           <div style="margin-bottom:10px;margin-top:10px;margin-left:10px;margin-right:10px;">
                              <asp:GridView ID="gvTerimaan2" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="true" AllowPaging="true" EmptyDataText=" Tiada rekod"
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
                    <asp:BoundField HeaderText="Mod Bayar" DataField="ModBayaran" SortExpression="ModBayaran" ItemStyle-Width="20%"/>			            
					<asp:BoundField HeaderText="Tarikh Tutup" DataField="TarikhClosing" SortExpression="TarikhClosing" ItemStyle-Width="20%" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center"/>
					<asp:BoundField HeaderText="Jumlah" DataField="Jumlah" SortExpression="Jumlah" ItemStyle-Width="20%" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right"/>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblKodMod" runat="server" Text='<%# Eval("kod")%>'></asp:Label>
                        </ItemTemplate>
					</asp:TemplateField>
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
