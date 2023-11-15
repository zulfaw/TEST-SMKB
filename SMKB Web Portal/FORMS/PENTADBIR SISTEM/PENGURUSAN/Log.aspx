<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Log.aspx.vb" Inherits="SMKB_Web_Portal.Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-9 col-md-6">
                            <p></p>
                            <table style="width:50%" >
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Tarikh" style="width: 20%;"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="30%"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calStartDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtStartDate" TodaysDateFormat="dd/MM/yyyy"/>
                                    <asp:RequiredFieldValidator ID="rfvTarikhMula" runat="server" ControlToValidate="txtStartDate" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnCari" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                    &nbsp
                                    <asp:Label ID="Label8" runat="server" Text="Hingga" style="width: 18%;"></asp:Label>
                                    &nbsp
                                    <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" Text="<%# System.DateTime.Now%>" Width="30%"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="calEndDate" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtEndDate" TodaysDateFormat="dd/MM/yyyy"/>
                                    <asp:RequiredFieldValidator ID="rfvTarikhAkhit" runat="server" ControlToValidate="txtEndDate" ErrorMessage="" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnCari" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                                
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Modul" style="width: 20%;"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlKodModul" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 80%;"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Sub Modul" style="width: 20%;"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlKodSubModul" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 80%;"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Sub Menu" style="width: 20%;"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlKodSubMenu" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 80%;"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="User ID" style="width: 20%;"></asp:Label>
                                </td>
                                <td>
                                    <asp:textbox ID="txtUserID" runat="server" style="width: 30%;"></asp:textbox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Cari" ValidationGroup="btnCari" CssClass="btn" OnClick = "OnBtnCari" />
                                </td>
                            </tr>
                            </table>
                            
                            <br />
                            
                            <asp:Label ID="lblSenaraiGV" runat="server" style="text-decoration:underline" Text="Senarai Audit Log"></asp:Label>
                            <asp:GridView ID="gvAuditLog" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" PageSize="1000"
                                cssclass="table table-striped table-bordered table-hover" Width="50%" HeaderStyle-BackColor="#6699FF" Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="false">
                                 <columns>
                                    <asp:BoundField DataField="Bil" HeaderText="BIL" SortExpression="Bil" ReadOnly="True">
                                        <ItemStyle Width="2.5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="User ID" DataField="UserID" SortExpression="UserID" ReadOnly="true">
                                         <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Tarikh" DataField="Tarikh" SortExpression="Tarikh" ReadOnly="true">
                                         <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Masa" DataField="Masa" SortExpression="Masa" ReadOnly="true">
                                         <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Keterangan" DataField="Keterangan" SortExpression="Keterangan" ReadOnly="true">
                                         <ItemStyle Width="5%" />
                                    </asp:BoundField> 
                                    <asp:BoundField HeaderText="User Ubah" DataField="UserUbah" SortExpression="UserUbah" ReadOnly="true">
                                         <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="SubMenu" DataField="SubMenu" SortExpression="SubMenu" ReadOnly="true">
                                         <ItemStyle Width="5%" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="InfoTable" DataField="InfoTable" SortExpression="InfoTable" ReadOnly="true">
                                         <ItemStyle Width="5%" />
                                    </asp:BoundField>                                    
                                    <%--<asp:CommandField ButtonType="Button" ShowSelectButton="True" >
                                        <ItemStyle Width="75px" CssClass="foo" />
                                    </asp:CommandField>--%>
                                </columns>
                            </asp:GridView>
                           
                        </div>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>
