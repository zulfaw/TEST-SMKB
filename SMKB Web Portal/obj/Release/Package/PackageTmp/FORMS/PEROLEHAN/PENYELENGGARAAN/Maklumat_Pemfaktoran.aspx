<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Maklumat_Pemfaktoran.aspx.vb" Inherits="SMKB_Web_Portal.Bayar_Atas_Nama" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 

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
                                <td style="width: 15%;"><Label class="control-label" for="">No ID</Label></td>
                                 <td>
                                    <asp:TextBox ID="txtID" runat="server" Width="50%" CssClass="form-control"  ReadOnly="True"></asp:TextBox>

                                </td>
                            </tr>
                                                    
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Bayar Atas Nama</Label></td>
                                <td>
                                    <asp:TextBox ID="txtBayarAtasNama" runat="server" Width="50%" CssClass="form-control"  ReadOnly="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBayarAtasNama" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">No Akaun</Label></td>
                                <td><asp:TextBox ID="txtNoAkaun" runat="server" Width="50%" CssClass="form-control" ReadOnly="false"></asp:TextBox></td>
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNoAkaun" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Email</Label></td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" Width="50%" CssClass="form-control"  ReadOnly="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Alamat</Label></td>
                                <td>
                                    <asp:TextBox ID="txtAlamat1" runat="server" Width="80%" CssClass="form-control"  ReadOnly="false" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAlamat1" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for=""></Label></td>
                                <td>
                                    <asp:TextBox ID="txtAlamat2" runat="server" Width="80%" CssClass="form-control"  ReadOnly="false"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Bandar</Label></td>
                                <td>
                                    <asp:TextBox ID="txtBandar" runat="server" Width="20%" CssClass="form-control"  ReadOnly="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtBandar" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                   &nbsp &nbsp &nbsp &nbsp
                                    <Label class="control-label" for="">Negeri</Label>
                                    &nbsp &nbsp
                                   <asp:DropDownList ID="ddlNegeri" runat="server" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvNamaBank" runat="server" ControlToValidate="ddlNegeri" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>

                                 </td>

                            </tr>
                            <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Poskod</Label></td>
                                <td>
                                    <asp:TextBox ID="txtPoskod" runat="server" Width="20%" CssClass="form-control"  ReadOnly="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPoskod" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                     &nbsp &nbsp &nbsp &nbsp
                                    <Label class="control-label" for="">Negara</Label>
                                    &nbsp &nbsp
                                    <asp:DropDownList ID="ddlNegara" runat="server" CssClass="form-control" Width="50%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlNegara" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <div style="text-align:center">                    
                            <asp:Button ID="btnReset" text="Reset" runat="server" CssClass="btn" ValidationGroup="btnReset" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');"/> &nbsp;    &nbsp; &nbsp;                
                            <asp:Button ID="btnSimpan" text="Simpan" runat="server" CssClass="btn" ValidationGroup="btnSimpan" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?');"/> &nbsp;                    
                    
                         </div>
                        </div>
                        </div>

                    <div class="panel panel-default" style="width:auto;">
                    <div class="panel-heading">
					<h3 class="panel-title">
						Senarai Bayar Atas Nama 
					</h3>
				    </div>
				    <div class="panel-body" style="overflow-x:auto">
					
					    <asp:GridView ID="gvBayarAtasNama" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" EmptyDataText=" Tiada rekod"
							    cssclass="table table-striped table-bordered table-hover" Width="100%" BorderStyle="Solid" ShowFooter="true" PageSize="10" Font-Size="8pt">
								    <columns>
								    <asp:TemplateField HeaderText = "Bil" ItemStyle-Width="2%" >
									    <ItemTemplate>
										    <%# Container.DataItemIndex + 1 %>
									    </ItemTemplate>
								    </asp:TemplateField> 
								    <asp:BoundField HeaderText="No ID" DataField="IdByr" SortExpression="NoID" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="5%" HorizontalAlign="Center"/>
								    </asp:BoundField>		
                                   <%-- <asp:BoundField HeaderText="Nama Bank" DataField="NamaBank" SortExpression="NamaBank" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="5%" HorizontalAlign="Center"/>
								    </asp:BoundField>--%>		
                                    <asp:BoundField HeaderText="Bayar Atas Nama" DataField="PO07_ByrAtasNama" SortExpression="BayarAtasNama" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="20%" HorizontalAlign="left"/>
								    </asp:BoundField>     
                                    <asp:BoundField HeaderText="No Akaun" DataField="PO07_NoAkaun" SortExpression="NoAkaun" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="5%" HorizontalAlign="Center"/>
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
