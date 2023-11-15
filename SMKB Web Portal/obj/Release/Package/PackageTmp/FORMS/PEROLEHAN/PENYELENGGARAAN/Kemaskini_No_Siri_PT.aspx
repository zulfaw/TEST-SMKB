<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kemaskini_No_Siri_PT.aspx.vb" Inherits="SMKB_Web_Portal.Kemaskini_No_Siri_PT" %>
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
                    <div class="panel panel-default" style="width:50%" id="divCarian">
                        <div class="panel-heading"><h4 class="panel-title">Tapisan</h4></div>
                        <div class="panel-body">
                 <table style="width: 100%;">
                    
                    <tr id="trPTJ" runat="server">
                    <td style="width: 100px; height: 22px;">PTj</td>
                    <td>:&nbsp<asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 200px;"></asp:DropDownList>
                        
                    </td>
			        </tr>

                    <tr style="height:25px" id="trRunningNo" runat="server">
                    <td style="width: 100px; height: 22px;">ID Running No</td>
                    <td> :&nbsp<asp:DropDownList ID="ddlIDRunningNo" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 200px;">
                        </asp:DropDownList>
                    </td>
                  </tr>
                   <tr>
                    <td>&nbsp</td>
                    <td style="height:40px">
                    <asp:LinkButton ID="lbtnPapar" runat="server" CssClass="btn btn-info">
                        <i class="fa fa-folder-open-o"></i>&nbsp;&nbsp;&nbsp;Papar
                    </asp:LinkButton>                    &nbsp &nbsp
                   	</td>
                </tr>
                  </table>
          
            </div>
                         </div>

                    <div class="panel panel-default" style="width:90%">
                        <div class="panel-body">
                            <table style="width:100%" class="table table table-borderless">
                             <tr>
                                    <td style="width: 15%;">
                                        <Label class="control-label" for="">No Siri</Label>
                                    </td>
                                    <td>:&nbsp
                                        <asp:TextBox ID="txtNoSiri" runat="server" CssClass="form-control" ReadOnly="true" Width="20%"></asp:TextBox>
                                         &nbsp; &nbsp;
                                     <Label class="control-label" for="">Staf</Label>
                                     &nbsp; :&nbsp;
                                     <asp:TextBox ID="txtStaf" runat="server" CssClass="form-control" ReadOnly="true" Width="50%"></asp:TextBox>
                                    </td>
                                </tr>
                            <tr>
                                <td style="width: 15%;">Status</td>
                                 <td>:&nbsp<asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" CssClass="form-control" Width="20%">                              
                            <asp:ListItem Value="A">A - Aktif</asp:ListItem>
                            <asp:ListItem Value="B">B - Batal</asp:ListItem>
                                           </asp:DropDownList>
                                &nbsp; &nbsp;
                                     <Label class="control-label" for="">Tarikh Ambil</Label>
                                     :&nbsp
                                    <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control" ReadOnly="true" Width="20%"></asp:TextBox>
                    </td>
                                                               
                        <tr>
                            <td style="width: 15%;">
                                <Label class="control-label" for="">Catatan</Label>
                            </td>
                            <td>:&nbsp
                                <asp:TextBox ID="txtCatatan" runat="server" CssClass="form-control" ReadOnly="false" Width="40%"></asp:TextBox>
                                    &nbsp; &nbsp;
                                <Label class="control-label" for="">No PT</Label>
                                &nbsp; :&nbsp;<asp:DropDownList ID="ddlPT" runat="server" AutoPostBack="True" CssClass="form-control" Width="30%">  
                                           </asp:DropDownList>
                            </td>
                        </tr>
                        </table>
                            <div style="text-align:center">                    
                            <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?')">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
                            <%--<asp:Button ID="btnKemasKini" text="Simpan" runat="server" CssClass="btn" ValidationGroup="btnSimpan" OnClientClick="return confirm('Anda pasti untuk jana no siri rekod ini?');"/> &nbsp;                    --%>
                    
                         </div>
                        </div>
                        </div>

                    <div class="panel panel-default" style="width:auto;">
                    <div class="panel-heading">
					<h3 class="panel-title">
						Senarai Running No 
					</h3>
				    </div>
				    <div class="panel-body" style="overflow-x:auto">
					<Label class="control-label" for="">ID Running No</Label>
                    &nbsp; :&nbsp;
                    <asp:TextBox ID="txtIDRunningNo" runat="server" CssClass="form-control" ReadOnly="false" Width="150px"></asp:TextBox>
                                     
                        <br></br>
                        <asp:GridView ID="gvRunningNo" runat="server" AllowPaging="true" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" 
                            EmptyDataText=" Tiada rekod" PageSize="10" ShowFooter="true" ShowHeaderWhenEmpty="True" Width="100%" Font-Size="8pt">
                            <columns>
                                <asp:TemplateField HeaderStyle-CssClass="centerAlign" HeaderText="Bil" ItemStyle-Width="2%" >
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RunningNo" HeaderStyle-CssClass="centerAlign" HeaderText="Running No" SortExpression="RunningNo">
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Status" HeaderStyle-CssClass="centerAlign" HeaderText="Status" SortExpression="Status">
                                <ItemStyle HorizontalAlign="Center" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Catatan" HeaderStyle-CssClass="centerAlign" HeaderText="Catatan" SortExpression="Catatan">
                                <ItemStyle HorizontalAlign="left" Width="20%" />
                                </asp:BoundField>
                                <%-- <asp:BoundField HeaderText="Flag" DataField="Flag" SortExpression="Flag" HeaderStyle-CssClass="centerAlign">
									    <ItemStyle Width="5%" HorizontalAlign="left"/>
								    </asp:BoundField>  --%>
                                <asp:BoundField DataField="NoStaf" HeaderStyle-CssClass="centerAlign" HeaderText="Staf" SortExpression="NoStaf">
                                <ItemStyle HorizontalAlign="left" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Tarikh" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="centerAlign" HeaderText="Tarikh" SortExpression="Tarikh">
                                <ItemStyle HorizontalAlign="left" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NoPT" HeaderStyle-CssClass="centerAlign" HeaderText="No PT" SortExpression="NoPT">
                                <ItemStyle HorizontalAlign="left" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BatalPT" HeaderStyle-CssClass="centerAlign" HeaderText="Batal PT" SortExpression="BatalPT">
                                <ItemStyle HorizontalAlign="left" Width="5%" />
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CommandName="Select" CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                    </ItemTemplate>
                                    <%--<ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" Height="18px" ImageUrl="~/Images/Delete_32x32.png" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" ToolTip="Padam" Width="15px" />
                                    </ItemTemplate>
                                     <ItemStyle Width="5%" />--%>
                                    <ItemStyle HorizontalAlign="Center" Width="3%" />
                                </asp:TemplateField>
                            </columns>
                        </asp:GridView>
                        <%--<div style="text-align:center">
                            <asp:Button ID="btnMohonBaru" text="Daftar Baru" runat="server" CssClass="btn" />
						    &nbsp;&nbsp;&nbsp;
						    <asp:Button ID="btnHantar" text="Hantar" runat="server" CssClass="btn" />
					    </div>--%>
                      <br></br>
				    </div>
                    </div>
                    <br></br>
                </div>
                </div>
                </div>
         
                
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
