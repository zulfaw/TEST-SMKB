<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Bidang_MOF.aspx.vb" Inherits="SMKB_Web_Portal.Bidang_MOF" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };
    </script>
    
    <h1>Bidang MOF</h1>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			
			<p></p>
            <div class="row">
            <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="tabCtrl">                                    
				<ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText ="Bidang Utama">
					<ContentTemplate>
                        
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="panel panel-default" style="width:auto">
                        <div class="panel-body">
                            <table style="width:100%" class="table table table-borderless">
                                        
                            <tr>
                                <td style="width: 15%;">Kod :</td>
                                <td>
                                    <asp:TextBox ID="txtKod" runat="server" Width="50px" CssClass="form-control" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtKod" runat="server" ControlToValidate="txtKod" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;">Butiran :</td>
                                <td><asp:TextBox ID="txtButiran" runat="server" Width="80%" CssClass="form-control" onkeyup="upper(this)"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvtxtButiran" runat="server" ControlToValidate="txtButiran" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                        </table>
                        <div style="text-align:center">   
                             &nbsp;&nbsp;<asp:LinkButton ID="lbtnRekodBaru" runat="server" CssClass="btn btn-info">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                            </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					    </asp:LinkButton>  
                         </div>
                        </div>
                        </div>

                    <div class="panel panel-default" style="width:auto;">
                    <div class="panel-heading">
					<h3 class="panel-title">
						Senarai Bidang Utama
					</h3>
				    </div>
				    <div class="panel-body" style="overflow-x:auto">
					
                        <table >
            
            <tr style="height:30px;">
                <td>
                    Jumlah rekod :&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                </td>
                <td >
                    &nbsp;|&nbsp;&nbsp;Saiz Rekod : 
                </td>
                <td style="width:50px;">
                    <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                        <asp:ListItem Text="10" Value=10 />
                        <asp:ListItem Text="25" Value=25 />
                        <asp:ListItem Text="50" Value=50 Selected="True"/>
                    </asp:DropDownList>
                </td>
                </tr>
            </table>
					    <asp:GridView ID="gvBidangUtama" runat="server" ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
							    cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" PageSize="25" DataKeyNames="Kod">
								    <columns>
								    <asp:TemplateField HeaderText = "Bil">
									    <ItemTemplate>
										    <%# Container.DataItemIndex + 1 %>
									    </ItemTemplate>
								        <HeaderStyle CssClass="centerAlign" />
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
								    </asp:TemplateField>
                                     <asp:BoundField DataField="Kod" HeaderText="Kod" SortExpression="Kod" ReadOnly="True"> 
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>
                                    <asp:BoundField DataField="Butiran" HeaderText="Butiran" SortExpression="Butiran" ReadOnly="True"> 
                                        <ItemStyle Width="60%" />
                                        </asp:BoundField>
								    <asp:TemplateField>                  
								    <ItemTemplate>
										    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select"  CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit fa-lg"></i>
                                            </asp:LinkButton>									    
                                    
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
											OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="far fa-trash-alt fa-lg"></i>
										</asp:LinkButton>
                                        </ItemTemplate>
									    <ItemStyle Width="10%" HorizontalAlign="Center"/>
								    </asp:TemplateField>
							    </columns>
                            <SelectedRowStyle ForeColor="Blue" />
						    </asp:GridView>
				    </div>
                    </div> 
                </div> 
            </div>
            

                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText ="Sub Bidang">
					<ContentTemplate>
                        <div class="panel panel-default">
                <div class="panel-body">
                    <div class="panel panel-default" style="width:auto">
                        <div class="panel-body">
                            <table style="width:100%" class="table table table-borderless">
                            <tr>
                                <td style="width: 15%;">Bidang Utama:</td>
                                <td>
                                    <asp:dropdownlist ID="ddlBidangUtama" runat="server" Width="300px" CssClass="form-control" AutoPostBack="True"></asp:dropdownlist>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBidangUtama" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpanSub" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>            
                            <tr>
                                <td style="width: 15%;">Kod Sub Bidang:</td>
                                <td>
                                    <asp:TextBox ID="txtKodSub" runat="server" Width="60px" CssClass="form-control" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtKodSub" runat="server" ControlToValidate="txtKodSub" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpanSub" Display="Dynamic" ></asp:RequiredFieldValidator>
                                    <%--&nbsp;
                                    <asp:CheckBox ID="ChxEnable" runat="server" ToolTip="Klik untuk enable textbox"/>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;">Butiran :</td>
                                <td><asp:TextBox ID="txtButiranSub" runat="server" Width="80%" CssClass="form-control" onkeyup="upper(this)"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvtxtButiranSub" runat="server" ControlToValidate="txtButiranSub" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpanSub" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                        </table>
                        <div style="text-align:center">   
                             &nbsp;&nbsp;<asp:LinkButton ID="lbtnRekodBaruSub" runat="server" CssClass="btn btn-info">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                            </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnSimpanSub" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?')">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					    </asp:LinkButton>  
                         </div>
                        </div>
                        </div>

                    <div class="panel panel-default" style="width:auto;">
                    <div class="panel-heading">
					<h3 class="panel-title">
						Senarai Sub Bidang
					</h3>
				    </div>
				    <div class="panel-body" style="overflow-x:auto">
					
                        <table >
            
            <tr style="height:30px;">
                <td>
                    Jumlah rekod :&nbsp;<label class="control-label" id ="lblJumRekodSub" runat="server" style="color:mediumblue;" ></label>
                </td>
                <td >
                    &nbsp;|&nbsp;&nbsp;Saiz Rekod : 
                </td>
                <td style="width:50px;">
                    <asp:DropDownList ID="ddlSaizSub" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                        <asp:ListItem Text="10" Value=10 />
                        <asp:ListItem Text="25" Value=25 />
                        <asp:ListItem Text="50" Value=50 Selected="True"/>
                        <asp:ListItem Text="100" Value=100 />
                    </asp:DropDownList>
                </td>
                </tr>
            </table>
					    <asp:GridView ID="gvSubBidang" runat="server" ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
							    cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" PageSize="25" DataKeyNames="Kod">
								    <columns>
								    <asp:TemplateField HeaderText = "Bil">
									    <ItemTemplate>
										    <%# Container.DataItemIndex + 1 %>
									    </ItemTemplate>
								        <HeaderStyle CssClass="centerAlign" />
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
								    </asp:TemplateField>
                                     <asp:BoundField DataField="Kod" HeaderText="Kod" SortExpression="Kod" ReadOnly="True"> 
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>
                                    <asp:BoundField DataField="Butiran" HeaderText="Butiran" SortExpression="Butiran" ReadOnly="True"> 
                                        <ItemStyle Width="50%" />
                                        </asp:BoundField>
                                    <asp:BoundField DataField="KodBdgUtama" HeaderText="Kod Bidang Utama" SortExpression="KodBdgUtama" ReadOnly="True"> 
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>
								    <asp:TemplateField>                  
								    <ItemTemplate>
										    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select"  CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit fa-lg"></i>
                                            </asp:LinkButton>									    
                                    
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
											OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="far fa-trash-alt fa-lg"></i>
										</asp:LinkButton>
                                        </ItemTemplate>
									    <ItemStyle Width="10%" HorizontalAlign="Center"/>
								    </asp:TemplateField>
							    </columns>
                            <SelectedRowStyle ForeColor="Blue" />
						    </asp:GridView>
				    </div>
                    </div> 
                </div> 
            </div>

                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText ="Bidang">
					<ContentTemplate>
                        <div class="panel panel-default">
                <div class="panel-body">
                    <div class="panel panel-default" style="width:auto">
                        <div class="panel-body">
                            <table style="width:100%" class="table table table-borderless">
                            <tr>
                                <td style="width: 15%;">Sub Bidang :</td>
                                <td>
                                    <asp:dropdownlist ID="ddlSubBidang" runat="server" Width="300px" CssClass="form-control" AutoPostBack="True"></asp:dropdownlist>
                                    <asp:RequiredFieldValidator ID="rfvddlSubBidang" runat="server" ControlToValidate="ddlSubBidang" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpanSub" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>            
                            <tr>
                                <td style="width: 15%;">Kod Bidang:</td>
                                <td>
                                    <asp:TextBox ID="txtKodBidang" runat="server" Width="80px" CssClass="form-control" Enabled="False"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvtxtKodBidang" runat="server" ControlToValidate="txtKodBidang" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpanSub" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;">Butiran :</td>
                                <td><asp:TextBox ID="txtButiranBdg" runat="server" Width="80%" CssClass="form-control" onkeyup="upper(this)"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="rfvtxtButiranBdg" runat="server" ControlToValidate="txtButiranBdg" ForeColor="Red" Text="*Sila Isi" ValidationGroup="btnSimpanSub" Display="Dynamic" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                        </table>
                        <div style="text-align:center">   
                             &nbsp;&nbsp;<asp:LinkButton ID="lbtnRekodBaruBdg" runat="server" CssClass="btn btn-info">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                            </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnSimpanBdg" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?')">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					    </asp:LinkButton>  
                         </div>
                        </div>
                        </div>

                    <div class="panel panel-default" style="width:auto;">
                    <div class="panel-heading">
					<h3 class="panel-title">
						Senarai Bidang
					</h3>
				    </div>
				    <div class="panel-body" style="overflow-x:auto">
					
                        <table >
            
            <tr style="height:30px;">
                <td>
                    Jumlah rekod :&nbsp;<label class="control-label" id ="lblRekodBdg" runat="server" style="color:mediumblue;" ></label>
                </td>
                <td >
                    &nbsp;|&nbsp;&nbsp;Saiz Rekod : 
                </td>
                <td style="width:50px;">
                    <asp:DropDownList ID="ddlSaizRekodBdg" runat="server" Width="50px" AutoPostBack="True" class="form-control">
                        <asp:ListItem Text="10" Value=10 />
                        <asp:ListItem Text="25" Value=25 />
                        <asp:ListItem Text="50" Value=50 Selected="True"/>
                        <asp:ListItem Text="100" Value=100 />
                    </asp:DropDownList>
                </td>
                </tr>
            </table>
					    <asp:GridView ID="gvBidang" runat="server" ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
							    cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" PageSize="25" DataKeyNames="KodBidang">
								    <columns>
								    <asp:TemplateField HeaderText = "Bil">
									    <ItemTemplate>
										    <%# Container.DataItemIndex + 1 %>
									    </ItemTemplate>
								        <HeaderStyle CssClass="centerAlign" />
                                        <ItemStyle HorizontalAlign="Right" Width="2%" />
								    </asp:TemplateField>
                                     <asp:BoundField DataField="KodBidang" HeaderText="Kod" SortExpression="KodBidang" ReadOnly="True"> 
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>
                                    <asp:BoundField DataField="Butiran" HeaderText="Butiran" SortExpression="Butiran" ReadOnly="True"> 
                                        <ItemStyle Width="50%" />
                                        </asp:BoundField>
                                    <asp:BoundField DataField="KodSubBidang" HeaderText="Kod Sub Bidang" SortExpression="KodSubBidang" ReadOnly="True"> 
                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>
								    <asp:TemplateField>                  
								    <ItemTemplate>
										    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select"  CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit fa-lg"></i>
                                            </asp:LinkButton>									    
                                    
                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
											OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
										<i class="far fa-trash-alt fa-lg"></i>
										</asp:LinkButton>
                                        </ItemTemplate>
									    <ItemStyle Width="10%" HorizontalAlign="Center"/>
								    </asp:TemplateField>
							    </columns>
                            <SelectedRowStyle ForeColor="Blue" />
						    </asp:GridView>
				    </div>
                    </div> 
                </div> 
            </div>

                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>

                </ajaxToolkit:TabContainer>
                </div>
                </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
