<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Ukuran.aspx.vb" Inherits="SMKB_Web_Portal.Ukuran" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };

    </script>
     <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
            <div class="container-fluid">
            <div class="row">
            <div class="panel-group">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="panel panel-default" style="width:90%">
                        <div class="panel-body">
                            <table style="width:100%" class="table table table-borderless">
                                        
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Kod Ukuran</Label></td>
                                <td>
                                    <asp:TextBox ID="txtKodUkuran" runat="server" Width="50px" CssClass="form-control"  ReadOnly="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtKodUkuran" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Butiran</Label></td>
                                <td><asp:TextBox ID="txtButiran" runat="server" Width="80%" CssClass="form-control" ReadOnly="false" onkeyup="upper(this)"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtButiran" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                        </table>
                        <div style="text-align:center">   
                             &nbsp;&nbsp;<asp:LinkButton ID="lbtnRekodBaru" runat="server" CssClass="btn btn-info">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                            </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?')">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					    </asp:LinkButton>  
                         &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn btn-info" OnClientClick="return confirm(Anda pasti untuk hapus rekod ini?')">
						<i class="fas fa-trash-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
					    </asp:LinkButton>             
                            <%--<asp:Button ID="btnReset" text="Reset" runat="server" CssClass="btn" ValidationGroup="btnReset" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');"/> &nbsp;    &nbsp; &nbsp;                
                            <asp:Button ID="btnSimpan" text="Simpan" runat="server" CssClass="btn" ValidationGroup="btnSimpan" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?');"/> &nbsp;  &nbsp; &nbsp;                       
                             <asp:Button ID="btnKemasKini" text="KemasKini" runat="server" CssClass="btn" ValidationGroup="btnKemasKini" OnClientClick="return confirm('Anda pasti untuk kemas kini rekod ini?');"/> &nbsp;                    --%>
                         </div>
                        </div>
                        </div>

                    <div class="panel panel-default" style="width:auto;">
                    <div class="panel-heading">
					<h3 class="panel-title">
						Senarai Kod Ukuran
					</h3>
				    </div>
				    <div class="panel-body" style="overflow-x:auto">
					
					    <asp:GridView ID="gvUkuran" runat="server" ShowHeaderWhenEmpty="True" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText=" Tiada rekod"
							    cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" PageSize="25" Font-Size="8pt">
								    <columns>
								    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%"  ItemStyle-HorizontalAlign="Right">
									    <ItemTemplate>
										    <%# Container.DataItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateField> 
								   <%-- <asp:BoundField HeaderText="Kod Ukuran" DataField="KodUkuran" SortExpression="NoID" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="10%" HorizontalAlign="Center"/>
								    </asp:BoundField>	--%>
                                     <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblKodUkuran" runat ="server" text="Kod Ukuran" />&nbsp;
                                            <asp:LinkButton id="lnkKodUkuran" runat="server" CommandName="Sort" CommandArgument="KodUkuran"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblKodUkuran" runat ="server" text='<%# Eval("Kod")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%"  />
                                    </asp:TemplateField> 
                                       
                                    <%-- <asp:BoundField HeaderText="Butiran" DataField="Butiran" SortExpression="NoID" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="50%" HorizontalAlign="Center"/>
								    </asp:BoundField>	--%>
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblButiran" runat ="server" text="Butiran" />&nbsp;
                                            <asp:LinkButton id="lnkButiran" runat="server" CommandName="Sort" CommandArgument="Butiran"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblButiran" runat ="server" text='<%# Eval("Butiran")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="20%"  />
                                    </asp:TemplateField> 
                                         <%--<asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblUrutan" runat ="server" text="Urutan" />&nbsp;
                                            <asp:LinkButton id="lnkUrutan" runat="server" CommandName="Sort" CommandArgument="Urutan"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUrutan" runat ="server" text='<%# Eval("Urutan")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="5%"  />
                                    </asp:TemplateField>  --%>                                    
								    <asp:TemplateField>                  
								    <ItemTemplate>
										    <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit fa-lg"></i>
                                            </asp:LinkButton>
									    </ItemTemplate>
                                    <%--<ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" Height="18px" ImageUrl="~/Images/Delete_32x32.png" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" ToolTip="Padam" Width="15px" />
                                    </ItemTemplate>
                                     <ItemStyle Width="5%" />--%>
									    <ItemStyle Width="3%" HorizontalAlign="Center"/>
								    </asp:TemplateField>
							    </columns>
						    </asp:GridView>
					    <%--<div style="text-align:center">
                            <asp:Button ID="btnMohonBaru" text="Daftar Baru" runat="server" CssClass="btn" />
						    &nbsp;&nbsp;&nbsp;
						    <asp:Button ID="btnHantar" text="Hantar" runat="server" CssClass="btn" />
					    </div>--%>

				    </div>
                    </div> 
                </div> 
            </div>
            </div>
            </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
