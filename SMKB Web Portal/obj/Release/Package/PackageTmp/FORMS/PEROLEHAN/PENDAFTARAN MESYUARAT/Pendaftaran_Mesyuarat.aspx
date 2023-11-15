<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pendaftaran_Mesyuarat.aspx.vb" Inherits="SMKB_Web_Portal.Pendaftaran_Mesyuarat1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="col-sm-9 col-md-6 col-lg-8">
			<p></p>
			<div class="row" style="width:100%;">
                <div class="panel panel-default">
                  <div class="panel-heading">
                    <h3 class="panel-title">
                               Maklumat Mesyuarat
                    </h3>
                   </div>

                    <div class="panel-body" style="overflow-x:auto">

                    <table style="width:90%">
                        <tr>
                            <td style="width: 20%;"><Label class="control-label" for="">Jenis Dokumen</Label></td>
                            <td>
                                <asp:DropDownList ID="ddlJenDokumen" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                                    <asp:ListItem Selected="True" Value="All">Keseluruhan</asp:ListItem>
                                    <asp:ListItem Value="SH">Sebut Harga</asp:ListItem>
                                    <asp:ListItem Value="TD">Tender</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                        </table>
                        <br/>
                        <asp:GridView ID="gvSHTD" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" emptydatatext="no data"
			         cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
			            <columns>
                                 <asp:TemplateField HeaderText="Pilih" >
                                    <ItemTemplate>
                                    <asp:CheckBox ID="chkPilih" runat="server" Checked='<%# Convert.ToBoolean(Eval("Pilih")) %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="7%"/>
                                    </asp:TemplateField>
					            <asp:BoundField DataField="SH/TD" HeaderText="No SH/TD" SortExpression="SH/TD" ReadOnly="True">
						            <ItemStyle Width="10%" />
					            </asp:BoundField>
					            <asp:BoundField HeaderText="Nama Projek" DataField="NamaProjek" SortExpression="NamaProjek" ReadOnly="true">
						            <ItemStyle Width="30%" />
					            </asp:BoundField>
                                <asp:BoundField HeaderText="Kategori Perolehan" DataField="KategoriPerolehan" SortExpression="KategoriPerolehan" ReadOnly="true">
						            <ItemStyle Width="10%" />
					            </asp:BoundField>
                                <asp:BoundField HeaderText="PTJ" DataField="PTJ" SortExpression="PTJ" ReadOnly="true">
						            <ItemStyle Width="20%" />
					            </asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" ReadOnly="true">
						            <ItemStyle Width="10%" />
					            </asp:BoundField>
					            <asp:BoundField HeaderText="Tarikh Mesyuarat" DataField="TarikhMesyuarat" SortExpression="TarikhMesyuarat" ReadOnly="true">
						            <ItemStyle Width="10%" />
					            </asp:BoundField>
                                 <asp:BoundField HeaderText="Masa Mesyuarat" DataField="MasaMesyuarat" SortExpression="MasaMesyuarat" ReadOnly="true">
						            <ItemStyle Width="10%" />
					            </asp:BoundField>                                  
					               
				            </columns>
			            </asp:GridView>


                        <table style="width:90%">
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Jenis Mesyuarat</Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlJenMesyuarat" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                                        <asp:ListItem Selected="True">Perlantikan Jawatankuasa</asp:ListItem>
                                        <asp:ListItem>Penilaian Teknikal</asp:ListItem>
                                        <asp:ListItem>Perlantikan Syarikat</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Tarikh</Label></td>
                                <td>
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control centerAlign" Text="<%# System.DateTime.Now.ToShortDateString() %>" Width="50%"></asp:TextBox>
									<%--<ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtStartDate" TodaysDateFormat="dd/MM/yyyy"/>--%>
									
                                </td>
                            </tr>
                            
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Masa</Label></td>
                                <td>
                                    <asp:TextBox ID="txtMasa" runat="server" CssClass="form-control" Width="50%" ReadOnly="false"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvdllKw" runat="server" ControlToValidate="ddlKw" ErrorMessage="" ForeColor="#820303" Text="*Perlu dipilih" ValidationGroup="btnStep1" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                             <tr>
                                <td  style="vertical-align:top; width: 20%;"><Label class="control-label" for="">Tujuan Mesyuarat</Label></td>
                                <td>
                                    <asp:TextBox ID="txtTujuan" runat="server" style="width: 100%; height:auto; min-height:100px;" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvtxtTujuan" runat="server" ControlToValidate="txtTujuan" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnStep2" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                                </td>
							</tr>

                            </table>
                       <br/>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title">Senarai Kehadiran </h3>
                            </div>
                            <div class="panel-body" style="overflow-x:auto">
                                <table style="width:90%">
                                    <tr>
                                        <td style="width: 20%;">
                                            <Label class="control-label" for="">No Staf</Label>
                                        </td>
                                        <td style="width: 50%;">
                                            <asp:DropDownList ID="ddlStaf" runat="server" AutoPostBack="True" CssClass="form-control" Width="80%">
                                            <asp:ListItem Selected="True" Value="02534">02534 - Shahira</asp:ListItem>
                                            <asp:ListItem Value="02543">02543 - Hazrin</asp:ListItem>
                                            <asp:ListItem Value="02544">02544 - Faezah</asp:ListItem>
                                            <asp:ListItem Value="02554">02554 - Hanafi</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lbtnTambahStaf" runat="server" CssClass="btn btn-info" ToolTip="Tambah ke senarai" ValidationGroup="btnSaveButiran">
                                            <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                                            </asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <br/>
                                <asp:GridView ID="gvJawatanKuasa" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" 
                                    EmptyDataText=" Tiada rekod" ShowFooter="true" ShowHeaderWhenEmpty="True" Width="100%" Font-Size="8pt">
                                    <columns>
                                        <asp:BoundField HeaderText="Bil" DataField="Bil" >
						                    <ItemStyle Width="5%" />
					                    </asp:BoundField>

                                        <asp:BoundField HeaderText="No Staf" DataField="NoStaf" SortExpression="NoStaf" ReadOnly="true">
						                    <ItemStyle Width="10%" />
					                    </asp:BoundField>
                                        <asp:BoundField HeaderText="Nama" DataField="Nama" SortExpression="Nama" ReadOnly="true">
						                    <ItemStyle Width="30%" />
					                    </asp:BoundField>
                                       <asp:BoundField HeaderText="PTJ" DataField="PTJ" SortExpression="PTJ" ReadOnly="true">
						                    <ItemStyle Width="30%" />
					                    </asp:BoundField>
                                         <asp:BoundField HeaderText="Jawatan" DataField="Jawatan" SortExpression="Jawatan" ReadOnly="true">
						                    <ItemStyle Width="30%" />
					                    </asp:BoundField>
                            
                                        <%--<asp:TemplateField>
			                           <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
                                            <i class="fa fa-hand-o-left fa-lg"></i>
                                        </asp:LinkButton>
                            
                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                            OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
                                        <i class="fa fa-trash-o fa-lg"></i>
                                        </asp:LinkButton>
			                        </ItemTemplate>
			                        <ItemStyle Width="3%" />
			                    
			                </asp:TemplateField>--%>
                                    </columns>
                                </asp:GridView>
                                <br/>
                                
                            </div>
                        </div>
                     
                    </div>
                    
                     <div style="text-align:center">                    
                    <asp:Button ID="btnSimpan" text="Simpan" runat="server" CssClass="btn" ValidationGroup="btnSimpan" /> &nbsp;                    
                    <asp:Button ID="btnHapus" text="Reset" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
                    </div>
                    <br>
             	</div>
                </div>
            </div>
        </div>
    </ContentTemplate>
	</asp:UpdatePanel>     
</asp:Content>
