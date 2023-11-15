<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Perubahan_Kontrak.aspx.vb" Inherits="SMKB_Web_Portal.Perubahan_Kontrak" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server"> 
    
    <ContentTemplate>
        <div class="col-sm-9 col-md-6 col-lg-8"> <p></p>
	    <div class="row" >
        <div class="panel panel-default">
            <div class="panel-body" >
                <table style="width:90%">
                     <tr>
                        <td style="width: 30%;"><Label class="control-label" for="">No Pesanan Tempatan</Label></td>
                        <td>
                            <asp:DropDownList ID="ddlPT" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                                <asp:ListItem Selected="True" Value="TD10">TENDER UTeM/BEN/10/2016</asp:ListItem>
                                <asp:ListItem Value="TD11">TENDER UTeM/BEN/11/2016</asp:ListItem>
                                <asp:ListItem Value="TD12">TENDER UTeM/BEN/12/2016</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                     </tr>    
                    
                    <tr>
                        <td style="width: 30%;"><Label class="control-label" for="">No Sebut Harga/Tender</Label></td>
                        <td>
                            <asp:DropDownList ID="ddlTender" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                                <asp:ListItem Selected="True" Value="TD10">TENDER UTeM/BEN/10/2016</asp:ListItem>
                                <asp:ListItem Value="TD11">TENDER UTeM/BEN/11/2016</asp:ListItem>
                                <asp:ListItem Value="TD12">TENDER UTeM/BEN/12/2016</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                        
                     </table>

                <br/>
                <div class="panel panel-default">
                    <div class="panel-heading">
                    <h3 class="panel-title">Maklumat Sebut Harga / Tender</h3>
                    </div>
                    <div class="panel-body" >
                        <table style="width:100%">
                            <tr>
                                <td style="width: 20%;">No Permohonan</td>
                                <td>
                                    :&nbsp<asp:Textbox ID="DropDownList1" runat="server" CssClass="form-control" Width="100px" Enabled="false"></asp:Textbox>
                                    
                                </td>
							</tr>
							<tr>
                                <td style="width: 20%;">Vendor</td>
                                <td>
                                    :&nbsp<asp:Textbox ID="DropDownList2" runat="server" CssClass="form-control" Width="50%" Enabled="false"></asp:Textbox>
                                    <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-lg" ToolTip="Cari" >
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
                            
                            
                            
							</table>
                        

                    </div>
                 </div>

                <asp:GridView ID="gvPengesyoran" runat="server" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False" BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" 
                    EmptyDataText=" " ShowFooter="true" ShowHeaderWhenEmpty="True" Width="100%" Font-Size="8pt">
                    <columns>
                        <asp:BoundField HeaderText="Bil" DataField="Bil" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
						    <ItemStyle Width="5%" />
					    </asp:BoundField>

                        <asp:BoundField HeaderText="Petender" DataField="Petender" SortExpression="Petender" ReadOnly="true" HeaderStyle-CssClass="centerAlign">
						    <ItemStyle Width="10%" />
					    </asp:BoundField>
                        <asp:BoundField HeaderText="Markah (%)" DataField="Markah" SortExpression="Markah" ReadOnly="true"  
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign" >
						    <ItemStyle Width="10%" />
					    </asp:BoundField>
                               
                        <asp:TemplateField HeaderText="Syor Teknikal" HeaderStyle-CssClass="centerAlign"  >
                            <ItemTemplate> 
                            <asp:CheckBox ID="chkSyorTeknikal" runat="server" Checked='<%# Convert.ToBoolean(Eval("Syor")) %>' ReadOnly="true" Enabled="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%"/>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Anggaran Harga (RM)" DataField="Harga" SortExpression="Petender" ReadOnly="true"  
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="centerAlign">
						    <ItemStyle Width="10%"/>
					    </asp:BoundField>
                         <asp:TemplateField HeaderText="Syor Lantikan" HeaderStyle-CssClass="centerAlign" >
                            <ItemTemplate> 
                            <asp:CheckBox ID="chkSyorLantikan" runat="server" Checked='<%# Convert.ToBoolean(Eval("Lantikan")) %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%"/>
                        </asp:TemplateField>
                            
                      
                    </columns>
                </asp:GridView>

                <table style="width:90%">
                            <tr>
                                <td style="width: 20%;"><Label class="control-label" for="">Jenis Perubahan</Label></td>
                                <td>
                                    <asp:DropDownList ID="ddlJenMesyuarat" runat="server" AutoPostBack="True" CssClass="form-control" Width="50%">
                                        <asp:ListItem Selected="True">Perlanjutan</asp:ListItem>
                                        <asp:ListItem>Penyingkatan</asp:ListItem>
                                        <asp:ListItem>Penamatan</asp:ListItem>
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
                                <td  style="vertical-align:top; width: 20%;"><Label class="control-label" for="">Catatan</Label></td>
                                <td>
                                    <asp:TextBox ID="txtCatatan" runat="server" style="width: 100%; height:auto; min-height:100px;" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="rfvtxtTujuan" runat="server" ControlToValidate="txtTujuan" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnStep2" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                                </td>
							</tr>

                            </table>

            </div>
        </div>
        </div>
        </div>
    </ContentTemplate>
    </asp:UpdatePanel>  
</asp:Content>
