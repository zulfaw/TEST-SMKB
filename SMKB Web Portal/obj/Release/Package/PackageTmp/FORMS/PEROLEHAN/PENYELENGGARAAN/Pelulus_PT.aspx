<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pelulus_PT.aspx.vb" Inherits="SMKB_Web_Portal.Pelulus_PT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                                <td style="width: 15%;"><Label class="control-label" for="">PTj</Label></td>
                                 <td>:&nbsp<asp:DropDownList OnSelectedIndexChanged="ddlPTJ_SelectedIndexChanged" ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%"></asp:DropDownList>
                                 </td>
                             </tr>
                            <tr>
                                <td style="width: 15%;">
                                    <Label class="control-label" for="">No Staf</Label>
                                </td>
                                <td>:&nbsp<asp:DropDownList ID="ddlStaf" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                                    </asp:DropDownList>
                                </td>
                             </tr>
                            <tr>
                                <td style="width: 15%;">
                                    <Label class="control-label" for="">Status</Label>
                                </td>
                                <td>:&nbsp<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                                    <asp:ListItem Selected="True" Value="1">Aktif</asp:ListItem>      
                                    <asp:ListItem  Value="0">Tidak Aktif</asp:ListItem>
                                    </asp:DropDownList>
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
					
					    <asp:GridView ID="gvPelulusPT" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" EmptyDataText=" Tiada rekod"
							    cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" PageSize="25" Font-Size="8pt">
								    <columns>
								    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									    <ItemTemplate>
										    <%# Container.DataItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateField> 
								    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblPTJ" runat ="server" text="PTJ" />&nbsp;
                                            <asp:LinkButton id="lnkPTJ" runat="server" CommandName="Sort" CommandArgument="PTJ"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblPTJ" runat ="server" text='<%# Eval("PTJ")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="40%"  />
                                    </asp:TemplateField> 
                                        
                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblNoStaf" runat ="server" text="No Staf" />&nbsp;
                                            <asp:LinkButton id="lnkNoStaf" runat="server" CommandName="Sort" CommandArgument="NoStaf"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblNoStaf" runat ="server" text='<%# Eval("NoStaf")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="10%"  />
                                    </asp:TemplateField> 		
                                     <%--<asp:BoundField HeaderText="No Staf" DataField="NoStaf" SortExpression="NoStaf" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="5%" HorizontalAlign="Center"/>
								    </asp:BoundField>--%>	
                                        <asp:BoundField HeaderText="Nama Staf" DataField="NamaStaf" SortExpression="NamaStaf" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="30%" HorizontalAlign="left"/>
								    </asp:BoundField>	

                                        <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblStatus" runat ="server" text="Status" />&nbsp;
                                            <asp:LinkButton id="lnkStatus" runat="server" CommandName="Sort" CommandArgument="Status"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat ="server" text='<%# Eval("Status")%>' />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="10%"  />
                                    </asp:TemplateField> 
                                    <%-- <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="10%" HorizontalAlign="Center"/>
								    </asp:BoundField>	--%>                                       
								    <asp:TemplateField>                        
								    <ItemTemplate>
										    <asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Maklumat Lanjut">
											    <i class="fas fa-edit fa-lg"></i>
										    </asp:LinkButton>
									    </ItemTemplate>
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
