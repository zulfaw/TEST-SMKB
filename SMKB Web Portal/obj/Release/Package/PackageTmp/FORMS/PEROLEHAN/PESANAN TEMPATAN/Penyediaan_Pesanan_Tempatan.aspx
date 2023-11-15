<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Penyediaan_Pesanan_Tempatan.aspx.vb" Inherits="SMKB_Web_Portal.Penyediaan_PT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row">
            <div class="col-sm-12 col-md-8 col-lg-8">
	        <%--<div>
                <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
                    <i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
                </asp:LinkButton>
            </div>--%>
            <br />
             <div class="row">
                <div class="panel panel-default" style="width:100%">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Maklumat Perolehan
                            </h3>
                        </div>
                        <div class="panel-body">
                            <asp:GridView ID="gvMohonPO" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false" EmptyDataText=" Tiada rekod"
							cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="true" Font-Size="8pt">
								<columns>
                                 <%--<asp:TemplateField HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="2%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat="server" text="Hantar" onclick = "checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick = "Check_Click(this)" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
								<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%">
									<ItemTemplate>
										<%# Container.DataItemIndex + 1 %>
									</ItemTemplate>
								</asp:TemplateField> 
								<asp:BoundField HeaderText="No Perolehan" DataField="NoPO" SortExpression="NoPO" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>
                                <asp:BoundField HeaderText="No PT" DataField="NoPT" SortExpression="NoPO" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="5%" HorizontalAlign="Center"/>
								</asp:BoundField>
                                <asp:BoundField HeaderText="Tarikh PT" DataField="TarikhMohon" SortExpression="TarikhMohon" HeaderStyle-CssClass="centerAlign">
                                    <ItemStyle Width="5%" HorizontalAlign="Center"/>
                                </asp:BoundField>							            
								<asp:BoundField HeaderText="Tujuan" DataField="Tujuan" SortExpression="Tujuan" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="25%" />
								</asp:BoundField>
								<asp:BoundField HeaderText="Nama Pembekal" DataField="NamaPembekal" SortExpression="Kategori" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="25%" />
								</asp:BoundField>                                
                                <asp:BoundField HeaderText="Jumlah Perbelanjaan (RM)" DataField="JumBesar" SortExpression="AngJumBesar" HeaderStyle-CssClass="centerAlign">
                                    <ItemStyle Width="8%" HorizontalAlign="Right"/>                       
                                </asp:BoundField>
								<asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-CssClass="centerAlign">
									<ItemStyle Width="10%" />
								</asp:BoundField>                                                                                             
								<asp:TemplateField>                        
								<ItemTemplate>
										<asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											<i class="fa fa-ellipsis-h fa-lg"></i>
										</asp:LinkButton>
									</ItemTemplate>
									<ItemStyle Width="3%" HorizontalAlign="Center"/>
								</asp:TemplateField>
							</columns>
						</asp:GridView>

                            <table style="width:100%">
                            <tr>
                                <td style="width: 20%;">No Permohonan</td>
                                <td>
                                    :&nbsp<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    
                                </td>
							</tr>
							<tr>
                                <td style="width: 20%;">Vendor</td>
                                <td>
                                    :&nbsp<asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-lg" ToolTip="Cari">
					                    <i class="fa fa-hand-o-left fa-lg"></i>
				                    </asp:LinkButton>
                                </td>
							</tr>
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">No PT</Label></td>
                                <td>
                                    :&nbsp<asp:TextBox ID="TextBox3" runat="server" Enabled="false" CssClass="form-control" Width="100px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;Tarikh PT
                                    &nbsp;&nbsp;
                                    :&nbsp<asp:TextBox ID="TextBox9" runat="server" Enabled="false" CssClass="form-control" Width="100px"></asp:TextBox>
                                </td>
							</tr>
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Tarikh Mohon</Label></td>
                                <td>
                                    :&nbsp<asp:TextBox ID="txtTarikh" runat="server" Enabled="false" CssClass="form-control" Width="100px"></asp:TextBox>
                                     &nbsp;&nbsp;&nbsp;Tempoh
                                    &nbsp;&nbsp;
                                    :&nbsp<asp:TextBox ID="TextBox2" runat="server" Enabled="false" CssClass="form-control" Width="100px"></asp:TextBox>
                                </td>
							</tr>
                            
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Tarikh Mula</Label></td>
                                <td>
                                    :&nbsp<asp:TextBox ID="TextBox5" runat="server" Enabled="false" CssClass="form-control" Width="100px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;Tarikh Tamat
                                    &nbsp;&nbsp;
                                    :&nbsp<asp:TextBox ID="TextBox4" runat="server" Enabled="false" CssClass="form-control" Width="100px"></asp:TextBox>
                                </td>
							</tr>
                            
                            
                            <tr>
                                <td  style="vertical-align:top; width: 20%;"><Label class="control-label" for="">Tujuan Perolehan</Label></td>
                                <td>
                                    :&nbsp<asp:TextBox ID="txtTujuan" runat="server" style="width: 90%; height:auto; min-height:100px;" TextMode="MultiLine" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvtxtTujuan" runat="server" ControlToValidate="txtTujuan" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnStep2" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                                </td>
							</tr>
                            
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Kategori Perolehan</Label></td>
                                <td>
                                    :&nbsp<asp:TextBox ID="TextBox6" runat="server" Enabled="false" CssClass="form-control" Width="100px"></asp:TextBox>
                                </td> 
                            </tr>
                            <tr>
                                <td style="width: 20%; vertical-align:top">
                                    <asp:Label ID="Label27" runat="server" Text="Lampiran" CssClass="control-label"></asp:Label>                                 
                                </td>
                                
                                <td>                                    
                                    <asp:GridView ID="gvLampiran" runat="server" AutoGenerateColumns="false" EmptyDataText = "No files uploaded" Width="350px" CssClass="form-control">
                                        <Columns>
                                            <asp:BoundField DataField="Text" HeaderText="File Name" ItemStyle-Width="70%" />                                                
                                            <asp:TemplateField ItemStyle-Width="3%">
                                                <ItemTemplate>                                                    
                                                    <asp:LinkButton ID="lnkDownload"  CssClass="btn-xs" ToolTip="Download" CommandArgument = '<%# Eval("Value") %>' runat="server" OnClick = "DownloadFile">
                                                        <i class="fa fa-download fa-lg"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID = "lnkDelete"  CssClass="btn-xs" ToolTip="Delete" CommandArgument = '<%# Eval("Value") %>' runat = "server" OnClick = "DeleteFile">
                                                        <i class="fa fa-trash-o fa-lg"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>

                            </tr>
                            
                            
							</table>
                            <br />

                    
                    <div class="panel panel-default" style="width:100%">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Maklumat Bajet dan Spesifikasi
                            </h3>
                        </div>
                    <div class="panel-body">
                        <asp:RadioButtonList ID="rbKodProjek" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true">
                            <asp:ListItem Text=" Bajet Komited" Value="0" Selected="True" />
                            <asp:ListItem Text=" Bajet Mengurus" Value="1" />
                        </asp:RadioButtonList>         
                
                       <br />

                    <div id="divBajetKomited" runat="server">
                    <div class="panel panel-default" style="width:100%">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                Maklumat Bajet
                            </h4>
                        </div>
                        <div class="panel-body">

                        <table style="width:90%">
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Kumpulan Wang</Label></td>
                                <td>
                                    :&nbsp<asp:DropDownList ID="ddlKWKomited" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtKW" runat="server" CssClass="form-control" Width="50%" ReadOnly="true"></asp:TextBox>--%>
                                    <%--<asp:RequiredFieldValidator ID="rfvdllKw" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
								<td style="width: 20%;"><Label class="control-label" for="">PTj</Label></td>
                                <td >
                                    :&nbsp<asp:TextBox ID="txtPtj" runat="server" CssClass="form-control" Width="80%" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Bahagian</Label></td>
                                <td>
                                    :&nbsp<asp:DropDownList ID="ddlBahagian" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvBahagian" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Unit</Label></td>
                                <td>
                                    :&nbsp<asp:DropDownList ID="ddlUnitPtj" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;"><asp:Label id="lblProjKom" class="control-label" runat="server" Visible="false">Projek Komited</asp:Label></td>
                                <td>
                                    :&nbsp<asp:DropDownList ID="ddlProjKomited" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%" Visible="false"></asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="ddlProjKomited" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                                </td> 
                            </tr>
                            
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Objek Sebagai</Label></td>
                                <td>
                                    :&nbsp<asp:DropDownList ID="ddlVotSbgKomited" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <%--<asp:TextBox ID="txtVotSbg" runat="server" CssClass="form-control" Width="50%" ReadOnly="true"></asp:TextBox>--%>
                                    <%--<asp:RequiredFieldValidator ID="rfvdllKw" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <%--<tr>
                                <td style="width: 20%;"><label class="control-label" for="">Operasi Bajet</label></td>
                                <td>
                                    <asp:RadioButtonList ID="rbOperasiBajet" runat="server" Height="25px" RepeatDirection="Horizontal" Width="30%" AutoPostBack="true">
                                        <asp:ListItem Selected="True" Text="  Mengurus" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="  Komited" Value="2"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td> 
                            </tr>--%>
                            
                            <tr>
								<td style="width: 20%;"><Label class="control-label" for="">Baki Bajet</Label></td>
                                <td >
                                    :&nbsp<asp:TextBox ID="txtBakiBajet" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                                </div></div></div>
                    
                   <%-- <div id="divBajetMengurus" runat="server">
                    <div class="panel panel-default" style="width:100%">
                        <div class="panel-heading">
                            <h4 class="panel-title">
                                Maklumat Bajet Mengurus
                            </h4>
                        </div>
                        <div class="panel-body">

                        <table style="width:90%">
    
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Kumpulan Wang</Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlKWUrus" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdllKw" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
								<td style="width: 20%;"><Label class="control-label" for="">PTj</Label></td>
                                <td >
                                    <asp:TextBox ID="ddlPTjUrus" runat="server" CssClass="form-control" Width="80%" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Bahagian</Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlBahagianUrus" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvBahagian" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Unit</Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlUnitUrus" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvUnit" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                            
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Objek Sebagai</Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlObjekSbgUrus" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvdllKw" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                                                        
                            <tr>
								<td style="width: 20%;"><Label class="control-label" for="">Baki Bajet</Label></td>
                                <td >
                                    <asp:TextBox ID="txtBakiBajetUrus" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            </table>
                                </div>

                    </div>

                    </div>--%>
                         <table id="tableSpek" style="width:100%">
                        <tr>
                            <td style="width: 25%;"><Label class="control-label" for="">Barang / Perkara</Label></td>
                            <td>
                                :&nbsp<asp:TextBox ID="txtPerkara" runat="server" style="width: 90%; height:auto; min-height:50px;" TextMode="MultiLine" CssClass="form-control" Enabled="false"></asp:TextBox>                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%;"><Label class="control-label" for="">Kuantiti</Label></td>
                            <td >
                                :&nbsp<asp:TextBox ID="txtKuantiti" runat="server" TextMode="Number" CssClass="form-control" Width="100px" AutoPostBack="true" Enabled="false"></asp:TextBox>                                
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%;"><Label class="control-label" for="">Ukuran</Label></td>
                            <td>
                                :&nbsp<asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control" Width="150px" Enabled="false"></asp:DropDownList>
                                                                
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%;"><asp:Label class="control-label" runat="server">Harga Seunit <small>(Tanpa GST) </small>(RM)</asp:Label></td>
                            <td>
                                :&nbsp<asp:TextBox ID="txtAngHrgSeunit" runat="server" CssClass="form-control rightAlign" Width="150px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%;"><asp:Label class="control-label" runat="server">Jumlah Harga <small>(Tanpa GST) </small>(RM)</asp:Label></td>
                            <td>
                                :&nbsp<asp:TextBox ID="txtJumAngHrg" runat="server" Enabled="false" CssClass="form-control rightAlign" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%;"><asp:Label class="control-label" runat="server">Kod GST</asp:Label></td>
                            <td>
                                :&nbsp<asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control" Width="150px" ></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%;">Vot GST</td>
                            <td>
                                :&nbsp<asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-control" Width="150px" ></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%;">
                                Inclusive of GST
                            </td>
                            <td>
                                :&nbsp<asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true">
                                    <asp:ListItem Text=" Ya" Value="0" Selected="True" />
                                    <asp:ListItem Text=" Tidak" Value="1" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%;"><asp:Label class="control-label" runat="server">Jumlah GST (RM)</asp:Label></td>
                            <td>
                                :&nbsp<asp:TextBox ID="TextBox7" runat="server" Enabled="false" CssClass="form-control rightAlign" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 25%;"><asp:Label class="control-label" runat="server">Jumlah Harga (RM)</asp:Label></td>
                            <td>
                                :&nbsp<asp:TextBox ID="TextBox8" runat="server" Enabled="false" CssClass="form-control rightAlign" Width="150px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height:40px; text-align:center;" colspan="2" >
                                <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
                                    <i class="fa fa-refresh fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
                                </asp:LinkButton> 
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnSaveButiran" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
                                    <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                                </asp:LinkButton>
                                <%--<asp:LinkButton ID="lbtnEditButiran" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Kemaskini">
                                    <i class="fa fa-pencil-square-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemaskini
                                </asp:LinkButton>--%>
                                <asp:Button ID="btnEdit" runat="server" CssClass="btn" Text="Kemaskini" ValidationGroup="btnSaveButiran" ToolTip="Kemaskini" />
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnUndo" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Kemaskini">
                                    <i class="fa fa-undo fa-lg"></i>&nbsp;&nbsp;&nbsp;Undo
                                </asp:LinkButton>                   
                            </td>
                        </tr>
                                
                    </table>
                        
                    <div>
                        <table>
                        <tr style="height:30px;">
                        <td style="width:80px;">
                            <label class="control-label" for="Klasifikasi">Saiz Rekod : </label>
                        </td>
                        <td style="width:50px;">
                            <asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="true" CssClass="form-control">
                                <asp:ListItem Text="5" Value="5" Selected="true" />
                                <asp:ListItem Text="10" Value="10" />
                                <asp:ListItem Text="25" Value="25" />
                                <asp:ListItem Text="50" Value="50" />
                            </asp:DropDownList>
                        </td>
                        </tr>
                        </table>
                    </div>
					<asp:GridView ID="gvSpekAM" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false"
						cssclass="table table-striped table-bordered table-hover" Width="100%"  BorderStyle="Solid" ShowFooter="True" Font-Size="8pt">
							<columns>
							<asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="KW" SortExpression="KW" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
                                    <%# Eval("KW") %>
								</ItemTemplate>
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="Kod Operasi" SortExpression="KW" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
                                    <%# Eval("KW") %>
								</ItemTemplate>
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="PTJ" SortExpression="PTJ" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
                                    <%# Eval("PTJ") %>
								</ItemTemplate>
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="Kod Projek" SortExpression="KW" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
                                    <%# Eval("KW") %>
								</ItemTemplate>
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="Vot Sebagai" SortExpression="Vot" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
                                    <%# Eval("Vot") %>
								</ItemTemplate>
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="Vot Lanjut" SortExpression="Vot" ItemStyle-Width="3%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="Center">
								<ItemTemplate>
                                    <%# Eval("Vot") %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Barang / Perkara" SortExpression="Perkara" ItemStyle-Width="40%" HeaderStyle-CssClass="centerAlign">
								<ItemTemplate>
                                    <%# Eval("Perkara") %>
								</ItemTemplate>
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Kuantiti" SortExpression="Kuantiti" ItemStyle-Width="2%" HeaderStyle-CssClass="centerAlign" ItemStyle-HorizontalAlign="right">
								<ItemTemplate>
                                    <%# Eval("Kuantiti") %>
								</ItemTemplate>
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="Ukuran" SortExpression="Unit" ItemStyle-Width="4%" HeaderStyle-CssClass="centerAlign">
								<ItemTemplate>
                                    <%# Eval("Unit") %>
								</ItemTemplate>	
							</asp:TemplateField>
							<asp:TemplateField HeaderText="Jum. Harga Seunit (RM)" SortExpression="AngHargaSeunit" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
								<ItemTemplate>
                                    <%# Eval("AngHargaSeunit") %>
								</ItemTemplate>	
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="Jum. Harga (Tanpa GST) (RM)" SortExpression="AngHargaTotal" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
								<ItemTemplate>
                                    <%# Eval("AngHargaTotal") %>
								</ItemTemplate>	
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="Jum. GST (RM)" SortExpression="AngHargaTotal" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
								<ItemTemplate>
                                    <%# Eval("AngHargaTotal") %>
								</ItemTemplate>	
							</asp:TemplateField>
                            <asp:TemplateField HeaderText="Jumlah (RM)" SortExpression="AngHargaTotal" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
								<ItemTemplate>
                                    <%# Eval("AngHargaTotal") %>
								</ItemTemplate>	
							</asp:TemplateField>                                     
							<asp:TemplateField>
			                    
<%--				                    <EditItemTemplate>
                                        <asp:ImageButton ID="btnSave1" runat="server" CommandName="Update" Height="20px" ImageUrl="~/Images/Save_48x48.png" 
                                            ToolTip="Simpan" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                            &nbsp;&nbsp;
                                        <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel16x16.png" 
                                                ToolTip="Batal" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                            &nbsp;&nbsp;
                                    </EditItemTemplate>--%>
                                    <ItemTemplate>
                                        <%--<asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit_48.png" 
                                            ToolTip="Kemas Kini" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                        &nbsp;&nbsp;
				                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					                        ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />--%>

                                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
                                            <i class="fa fa-pencil-square-o fa-lg"></i>
                                        </asp:LinkButton>
                            
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                            OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
                                        <i class="fa fa-trash-o fa-lg"></i>
                                        </asp:LinkButton>
			                        </ItemTemplate>
			                        <ItemStyle Width="3%" />
			                    
			                </asp:TemplateField>
						</columns>
					</asp:GridView>
                        
                        </div>
                    </div>
                 </div>

                 <%--<div id="divSHTD" runat="server">
                     <div class="panel panel-default" style="width:100%">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                Maklumat Sebut Harga / Tender
                            </h3>
                        </div>
                    <div class="panel-body">
                            <table style="width:100%">					
                            <tr>
                                <td  style="vertical-align:top; width: 15%;"><Label class="control-label" for=""><b>A. Title</b></Label></td>
                                <td>
                                    <asp:TextBox ID="TextBox2" runat="server" style="height:auto; min-height:100px; width: 100%;" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </td>
							</tr>
                            <tr>
                                <td  style="vertical-align:top; width: 15%;"><Label class="control-label" for=""><b>B. Scope</b></Label></td>
                                <td>
                                    <asp:TextBox ID="TextBox3" runat="server" style="height:auto; min-height:100px; width: 100%;" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </td>
							</tr>
                            <tr>
                                <td  style="vertical-align:top; width: 15%;"> 
                                     <Label class="control-label" for=""><b>Borang Teknikal</b></Label>
                                </td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                    <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-info" ToolTip="Upload file">
						                <i class="fa fa-upload fa-lg"></i>&nbsp;&nbsp;&nbsp;Upload
					                </asp:LinkButton>
                                    <br />
                                    <asp:Label ID="lblMessage" ForeColor="Green" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Jenis Perolehan</Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlJenisPO" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvddlJenisPerolehan" runat="server" ControlToValidate="ddlJenisPerolehan" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep2" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td> 
                            </tr>
							</table>
                            
                            <br />
                           
                            
                    </div>

                     </div>
                </div>--%>
                <div class="col-sm-3 col-md-6">
                <div class="panel panel-default">
                <div class="panel-body">
                    <table style="width: 100%;">
                     <tr style="height:25px">
                        <td>
                          <asp:RadioButtonList ID="rbKelulusan" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbKelulusan_SelectedIndexChanged">
                            <asp:ListItem Text=" Lulus" Value="0" Selected="True" />
                            <asp:ListItem Text=" Tidak Lulus" Value="1" />
                          </asp:RadioButtonList>

                        </td>
                    </tr>
                     
                  </table>
                    <br>
                    

                    <div id="divUlasan" runat="server">
                        <label class="control-label" for="Ulasan">Ulasan Tidak Lulus</label>
                        <asp:TextBox ID="txtUlasan" runat="server" TextMode="MultiLine" Width="100%" rows="3" ></asp:TextBox>
                     </div>
                </div>
           </div></div>
                        </div>
             </div>
             <br />
                <div style="text-align:center">                    
                    <asp:Button ID="btnSimpan" text="Simpan" runat="server" CssClass="btn" ValidationGroup="btnSimpan" /> &nbsp;                    
                    <asp:Button ID="btnHantar" text="Hantar" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
                </div>

                 </div>
            </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
