<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Mohon_Baru.aspx.vb" Inherits="SMKB_Web_Portal.Mohon_Baru" EnableEventValidation ="False"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
	<ContentTemplate>
        <div class="col-sm-9 col-md-6 col-lg-8">
        <p></p>

        <div class="panel-group">
        <div class="panel panel-default">
        <div class="panel-body">
            Status<label class="control-label" for="">&nbsp;:</label> 
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
            <br />
            <h4>Senarai Status Permohonan</h4>
            <br />
            <asp:GridView ID="gvMohonBajet" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false"
				cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
					<columns>
					<asp:BoundField DataField="Bil" HeaderText="BIL" SortExpression="Bil" ReadOnly="True">
						<ItemStyle Width="2%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="No Permohonan" DataField="NoPermohonan" SortExpression="NoPermohonan" ReadOnly="true">
						<ItemStyle Width="5%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="Program/ Aktiviti" DataField="Program" SortExpression="SubMenuKuantitiProgram" ReadOnly="true">
						<ItemStyle Width="30%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="Tarikh Mohon" DataField="TarikhMohon" SortExpression="TarikhMohon">
						<ItemStyle Width="5%" />
					</asp:BoundField>
                    <asp:BoundField HeaderText="Anggaran Perbelanjaan" DataField="AngBelanja" SortExpression="AngBelanja">
						<ItemStyle Width="10%" />
					</asp:BoundField>
                    <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status">
						<ItemStyle Width="5%" />
					</asp:BoundField>                                    
					<asp:TemplateField>
			            <ItemTemplate>
				            &nbsp;&nbsp;
				            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
			            </ItemTemplate>
			            <ItemStyle Width="5%" />
			        </asp:TemplateField>
				</columns>
			</asp:GridView>
            <br />
            <asp:Button ID="btnMohonBaru" runat="server" Text="Mohon Baru" CssClass="btn" OnClick = "OnBtnMohonBaru" />

            <br /><br />
            
            <div class="panel panel-default">
            <div class="panel-body">
            <table style="width:100%">
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">No Mohon</Label></td>
                    <td>
                        <asp:TextBox ID="txtNoMohon" runat="server" Width="100px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        &nbsp &nbsp &nbsp &nbsp
                        <Label class="control-label" for="">Status</Label>
                        &nbsp &nbsp
                        <asp:TextBox ID="txtStatus" runat="server" Width="30%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Tarikh</Label></td>
                    <td>
                        <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control" Width="140px" ReadOnly="true" TextMode="Date"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Maksud</Label></td>
                    <td>
                        <asp:TextBox ID="txtMaksud" runat="server" CssClass="form-control" Width="60%" ReadOnly="true"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Agensi</Label></td>
                    <td>
                        <asp:TextBox ID="txtAgensi" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">PTj</Label></td>
                    <td>
                        <asp:TextBox ID="txtPtj" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Bahagian</Label></td>
                    <td>
                        <asp:TextBox ID="txtBahagian" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Unit</Label></td>
                    <td>
                        <asp:TextBox ID="txtUnit" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Bajet pada tahun</Label></td>
                    <td>
                        <asp:DropDownList ID="ddlTahunBajet" runat="server" CssClass="form-control" Width="80%"></asp:DropDownList>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Jenis Dasar</Label></td>
                    <td>
                        <asp:TextBox ID="txtJenDasar" runat="server" CssClass="form-control" Width="30%"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Kod Operasi</Label></td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Width="50%"></asp:DropDownList>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Justifikasi</Label></td>
                    <td>
                        <asp:TextBox ID="txtJust" runat="server" textmode="multiline" CssClass="form-control" Width="80%"></asp:TextBox>
                    </td>
				</tr>
            </table>
            <br />
            <div class="panel panel-primary">
            <div class="panel-heading">Butiran</div>
            <div class="panel-body">
            <table style="width:100%">
                 <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Butiran</Label></td>
                    <td>
                        <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" Width="50%"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Kuantiti</Label></td>
                    <td>
                        <asp:TextBox ID="txtKuantiti" runat="server" CssClass="form-control" Width="100px" TextMode="Number"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Anggaran Jumlah</Label></td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="100px"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td>&nbsp</td>
                    <td style="height:40px">
					<asp:Button ID="btnReset" runat="server" Text="Reset" ValidationGroup="btnReset" CssClass="btn" />
                    &nbsp &nbsp
                    <asp:Button ID="btnSaveButiran" runat="server" Text="Tambah" ValidationGroup="btnSaveButiran" CssClass="btn" />
					</td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false"
				cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
					<columns>
					<asp:BoundField DataField="Bil" HeaderText="BIL" SortExpression="Bil" ReadOnly="True">
						<ItemStyle Width="2%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="No Butiran" DataField="NoButiran" SortExpression="NoButiran" ReadOnly="true">
						<ItemStyle Width="5%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="Butiran" DataField="ProgramButiran" SortExpression="Butiran" ReadOnly="true">
						<ItemStyle Width="30%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="Kuantiti" DataField="Kuantiti" SortExpression="Kuantiti">
						<ItemStyle Width="5%" />
					</asp:BoundField>
                    <asp:BoundField HeaderText="Anggaran (RM)" DataField="Anggaran" SortExpression="Anggaran">
						<ItemStyle Width="10%" />
					</asp:BoundField>                                                        
					<asp:TemplateField>
                        <EditItemTemplate>
                            <asp:ImageButton ID="btnSave1" runat="server" CommandName="Update" Height="20px" ImageUrl="~/Images/Save_48x48.png" 
                                ToolTip="Simpan" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                            <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="20px" ImageUrl="~/Images/Cancel16x16.png" 
                                    ToolTip="Batal" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                                &nbsp;&nbsp;
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit_48.png" 
                                ToolTip="Kemas Kini" Width="15px" OnItemCommand="gvButiranModul_ItemCommand" />
                            &nbsp;&nbsp;
				            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
			            </ItemTemplate>
			            <ItemStyle Width="5%" />
			        </asp:TemplateField>
				</columns>
			</asp:GridView>
            </div>
            </div>
            </div>
            </div>
            <br />
            <div style="text-align:center">
                <asp:Button ID="btnSimpan" runat="server" Text="Simpan" CssClass="btn" />
                &nbsp;&nbsp;
                <asp:Button ID="btnHantar" runat="server" Text="Hantar" CssClass="btn" />
            </div>
            
        </div>
        </div>    
        </div>  
    
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
         
</asp:Content>
