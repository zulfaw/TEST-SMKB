<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Permohonan_Bajet_Penyelidikan.aspx.vb" Inherits="SMKB_Web_Portal.Permohonan_Bajet_Penyelidikan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 


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
            <h4>Senarai Permohonan</h4>
            <br />
            <asp:GridView ID="gvMohonBajet" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="false" EmptyDataText=""
				cssclass="table table-striped table-bordered table-hover" Width="100%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
					<columns>
					<asp:BoundField DataField="Bil" HeaderText="BIL" SortExpression="Bil" ReadOnly="True">
						<ItemStyle Width="2%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="Principal Researcher" DataField="principle" SortExpression="principle" ReadOnly="true">
						<%--<ItemStyle Width="5%" />--%>
					</asp:BoundField>
					<asp:BoundField HeaderText="Project No" DataField="ProjNo" SortExpression="ProjNo" ReadOnly="true">
						<ItemStyle Width="30%" />
					</asp:BoundField>
					<asp:BoundField HeaderText="Date Approval" DataField="dateapp" SortExpression="dateapp">
						<ItemStyle Width="5%" />
					</asp:BoundField>
                    <asp:BoundField HeaderText="Total Approve Budget (RM)" DataField="totalapp" SortExpression="totalapp">
						<ItemStyle Width="10%" />
					</asp:BoundField>                               
					<%--<asp:TemplateField>
			            <ItemTemplate>
				            &nbsp;&nbsp;
				            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
					            ToolTip="Padam" Width="15px" Height="18px" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
			            </ItemTemplate>
			            <ItemStyle Width="5%" />
			        </asp:TemplateField>--%>
				</columns>
			</asp:GridView>
       
            <br />
            <br />

           <%-- <div class="panel-group">--%>

            <asp:MultiView ID="MultiView1" runat="server"></asp:MultiView>
            <asp:View ID="View1" runat="server">  
                    <table style="width: 100%;">  
                        <tr>  
                            <td class="auto-style4"><strong>Student Details</strong></td>  
  
                        </tr>  
                        <tr>  
                            <td class="auto-style4">Student FirstName</td>  
                            <td>  
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>  
                            <td></td>  
                        </tr>  
                        <tr>  
                            <td class="auto-style4">Student LastName</td>  
                            <td>  
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>  
                            <td> </td>  
                        </tr>  
                        <tr>  
                            <td class="auto-style4"> </td>  
                            <td>  
<%--                                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Next" />  --%>
                            </td>  
                            <td> </td>  
                        </tr>  
                    </table>  
  
                </asp:View>  

            <br />

            <div class="panel panel-default">
            <div class="panel-heading">
            <h4 class="panel-title">
              <b><a data-toggle="collapse" href="#PanelMohonBaru">A. PROJECT DETAILS</a></b>
            </h4>
          </div>
            <div id="PanelMohonBaru" class="panel-collapse">
            <div class="panel-body">
            <table style="width:100%">
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Principal Researcher</Label></td>
                    <td>
                        <asp:TextBox ID="txtPrincipal" runat="server" Width="100px" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                        &nbsp &nbsp &nbsp &nbsp
                        <Label class="control-label" for="">Tarikh Mohon</Label>
                        &nbsp &nbsp
                        <asp:TextBox ID="txtTarikhMohon" runat="server" Width="30%" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Faculty/Centre</Label></td>
                    <td>
                        <asp:DropDownList ID="ddlFac" runat="server" CssClass="form-control" Width="60%">
                        </asp:DropDownList>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Contact No.(Off./HP)</Label></td>
                    <td>
                        <asp:TextBox ID="txtNoHP" runat="server" CssClass="form-control" Width="60%"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Email</Label></td>
                    <td>
                        <asp:TextBox ID="txtAgensi" runat="server" CssClass="form-control" Width="60%"  value=""></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Project Title</Label></td>
                    <td>
                        <asp:TextBox ID="txtProjTitle" runat="server" CssClass="form-control" Width="60%" textmode="multiline"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Project No.</Label></td>
                    <td>
                        <asp:TextBox ID="txtProjNo" runat="server" CssClass="form-control" Width="60%" ></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Date of Approval (dd/mm/yy)</Label></td>
                    <td>
                        <asp:TextBox ID="txtDateApproval" runat="server" CssClass="form-control" Width="60%" ReadOnly="true"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Project Duration (date)</Label></td>
                    <td>
                     <%--   <asp:DropDownList ID="ddlTahunBajet" runat="server" CssClass="form-control" Width="60%"></asp:DropDownList>
                    </td>
                    <td>--%>
                   <%--   <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                         <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDateFrom" TodaysDateFormat="dd/MM/yyyy" />  
                      &nbsp; hingga &nbsp;
                      <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                      <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDateTo" TodaysDateFormat="dd/MM/yyyy" />
                     </td>         
                    <td>--%>
                        &nbsp; From &nbsp;
                      <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                         <cc1:CalendarExtender ID="calDateFrom" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDateFrom" TodaysDateFormat="dd/MM/yyyy" />  
                      &nbsp; To &nbsp;
                      <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="100px"></asp:TextBox>
                      <cc1:CalendarExtender ID="calDateTo" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtDateTo" TodaysDateFormat="dd/MM/yyyy" />
                     </td>         

				</tr>
                <tr>
                    <td style="width: 25%;"><Label class="control-label" for="">Total Approved Duration</Label></td>
                    <td>
                        <asp:TextBox ID="txtTotalDura" runat="server" CssClass="form-control" ReadOnly ="true" Width="60%"></asp:TextBox>
                    </td>
				</tr>
                <tr>
                    <td style="width: 15%; height: 22px;"><Label class="control-label" for="">Total Approved Budget (RM)</Label></td>
                    <td style="height: 22px">
                        <asp:TextBox ID="txtAppBg" runat="server" CssClass="form-control" Width="60%"></asp:TextBox>
                    </td>
				</tr>

            </table>
            
            <br /><br />
            
            
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
