<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="No_Siri_PT.aspx.vb" Inherits="SMKB_Web_Portal.No_Cetakan_PT" %>
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
                    
                     <tr style="height:25px">
                      <td style="width: 100px; height: 22px;">Kategori</td>
                      <td>:&nbsp<asp:DropDownList ID="ddlKategori" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 200px;">
                          <asp:ListItem Selected="True" Value="0">Sila Pilih</asp:ListItem>      
                          <%--<asp:ListItem  Value="1">Running No</asp:ListItem>--%>
                            <asp:ListItem Value="2">PTJ</asp:ListItem>
                            <asp:ListItem Value="3">Tarikh</asp:ListItem>
                          </asp:DropDownList>
                      </td>
                  </tr>
                     <tr id="trRunningNo" runat="server">
                    <td style="width: 100px; height: 22px;">Running No</td>
                    <td>:&nbsp<asp:TextBox ID="txtRunningNo" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" style="width: 200px;"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDari" TodaysDateFormat="dd/MM/yyyy" />
                         
                    </td>
			        </tr>
                    <tr id="trPTJ" runat="server">
                    <td style="width: 100px; height: 22px;">PTj</td>
                    <td>:&nbsp<asp:DropDownList ID="ddlPTJCari" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 200px;"></asp:DropDownList>
                        
                    </td>
			        </tr>

                     <tr id="trTarikh" runat="server">
                    <td style="width: 100px; height: 22px;">Tarikh</td>
                    <td>:&nbsp<Label class="control-label" for="">Hingga</Label>
                        &nbsp<asp:TextBox ID="txtDari" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="50px"></asp:TextBox>
                         <cc1:CalendarExtender ID="calDari" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDari" TodaysDateFormat="dd/MM/yyyy" />
                         &nbsp<Label class="control-label" for="">Hingga</Label>
                        &nbsp<asp:TextBox ID="txtHingga" runat="server" CssClass="form-control centerAlign" Width="50px" ></asp:TextBox>
                        <cc1:CalendarExtender ID="calHingga" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtHingga" TodaysDateFormat="dd/MM/yyyy" />
                       
                    </td>
			        </tr>
                    <tr style="height:25px" id="trNoPT" runat="server">
                    <td style="width: 100px; height: 22px;">ID Running No</td>
                    <td> :&nbsp<asp:DropDownList ID="ddlSiriPT" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 200px;">
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
                                <td style="width: 15%;"><Label class="control-label" for="">PTj</Label></td>
                                 <td>:&nbsp<asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" Width="40%"></asp:DropDownList>
                                &nbsp; &nbsp;
                                     <Label class="control-label" for="">Staf </Label>
                                     :&nbsp
                                    <asp:DropDownList ID="ddlStaf" runat="server" AutoPostBack="True" CssClass="form-control" Width="40%"></asp:DropDownList>
                    </td>
                               
                                <tr>
                                <td style="width: 15%;"><Label class="control-label" for="">Tahun</Label></td>
                                 <td>:&nbsp
                                     <asp:TextBox ID="txtTahun" runat="server" CssClass="form-control" ReadOnly="true" Width="50px"></asp:TextBox>
                                     &nbsp; &nbsp;
                                     <Label class="control-label" for="">Bulan </Label>
                                     &nbsp; :&nbsp;
                                     <asp:TextBox ID="txtBulan" runat="server" CssClass="form-control" ReadOnly="true" Width="50px"></asp:TextBox>
                                      &nbsp; :&nbsp;
                                     <Label class="control-label" for="">No Mula</Label>
                                     &nbsp; :&nbsp;
                                     <asp:TextBox ID="txtNoMula" runat="server" CssClass="form-control" ReadOnly="true" Width="50px"></asp:TextBox>
                                     &nbsp; &nbsp;
                                     <Label class="control-label" for="">No Akhir</Label>
                                     &nbsp; :&nbsp;
                                     <asp:TextBox ID="txtNoAkhir" runat="server" CssClass="form-control" ReadOnly="false" Width="50px"></asp:TextBox>
                        
                                </td>
                                
                                <tr>
                                    <td style="width: 15%;">
                                        <Label class="control-label" for="">Tarikh Ambil</Label>
                                    </td>
                                    <td>:&nbsp
                                        <asp:TextBox ID="txtTarikhAmbil" runat="server" CssClass="form-control" ReadOnly="true" Width="40%"></asp:TextBox>
                                         &nbsp; &nbsp;
                                     <Label class="control-label" for="">No Siri Terakhir</Label>
                                     &nbsp; :&nbsp;
                                     <asp:TextBox ID="txtNoSiriTerakhir" runat="server" CssClass="form-control" ReadOnly="true" Width="50px"></asp:TextBox>
                                    </td>
                                </tr>
                        </table>
                            <div style="text-align:center">                    
                           
                            <asp:Button ID="btnJanaNoSiri" text="Jana No Siri" runat="server" CssClass="btn" ValidationGroup="btnSimpan" OnClientClick="return confirm('Anda pasti untuk jana no siri rekod ini?');"/> &nbsp;                    
                    
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
                        <asp:GridView ID="gvRunningNo" runat="server" AllowPaging="true" AllowSorting="True" AutoGenerateColumns="False" 
                            BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" EmptyDataText=" Tiada rekod" PageSize="10" 
                            ShowFooter="true" ShowHeaderWhenEmpty="True" Width="100%" Font-Size="8pt">
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
                                <asp:BoundField DataField="NoStaf" HeaderStyle-CssClass="centerAlign" HeaderText="No Staf" SortExpression="NoStaf">
                                <ItemStyle HorizontalAlign="left" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataFormatString="{0:dd/MM/yyyy}" DataField="Tarikh" HeaderStyle-CssClass="centerAlign" HeaderText="Tarikh" SortExpression="Tarikh">
                                <ItemStyle HorizontalAlign="left" Width="5%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NoPT" HeaderStyle-CssClass="centerAlign" HeaderText="No PT" SortExpression="NoPT">
                                <ItemStyle HorizontalAlign="left" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="BatalPT" HeaderStyle-CssClass="centerAlign" HeaderText="Batal PT" SortExpression="BatalPT">
                                <ItemStyle HorizontalAlign="left" Width="10%" />
                                </asp:BoundField>
                                <%-- <asp:TemplateField>                        
								    <ItemTemplate>
										    <asp:LinkButton ID="lbDetail" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs btn-primary" ToolTip="Maklumat Lanjut">
											    <i class="fa fa-ellipsis-h fa-lg"></i>
										    </asp:LinkButton>
									    </ItemTemplate>
									    <ItemStyle Width="3%" HorizontalAlign="Center"/>
								    </asp:TemplateField>--%>
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
                    <div style="text-align:center">
                        &nbsp;&nbsp;<asp:LinkButton ID="lbtnRekodBaru" runat="server" CssClass="btn btn-info">
						<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
                            </asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?')">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
                        <%-- <asp:Button ID="btnReset" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" text="Reset" ValidationGroup="btnReset" />
                        &nbsp; &nbsp; &nbsp;
                        <asp:Button ID="btnSimpan" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk simpan rekod ini?');" text="Simpan" ValidationGroup="btnSimpan" />
                        &nbsp;--%>
                    </div>
                   
                    <br>
                   
                    </br>
                </div>
                </div>
                </div>
         
                
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>  

</asp:Content>
