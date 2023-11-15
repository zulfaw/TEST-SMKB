<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Proses_Tanggungan.aspx.vb" Inherits="SMKB_Web_Portal.Proses_Tanggungan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>


    <script> 
        
        //validate sama ada terdapat rekod yang dipilih untuk diproses
        function validate() {
            var flag = false;
     
            var gridView = document.getElementById("<%=gvProses.ClientID %>");
        var checkBoxes = gridView.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked ) {
                flag = true;
                break;
            }
        }

        if (!flag) {
            alert('Sila pilih rekod tanggungan untuk dibawa ke tahun hadapan!');
            flag = false;
        }
        else {
            var r = confirm("Anda pasti untuk proses tanggungan dalam senarai ke tahun hadapan?");
            if (r == true) {
                flag = true;
            } else {
                flag = false;
            }
            
        }
        return flag;
        }



        //validate sama ada terdapat rekod yang dipilih untuk dihapuskan
        function valGvProses() {
            var flag = false;
     
            var gridView = document.getElementById("<%=gvProses.ClientID %>");
        var checkBoxes = gridView.getElementsByTagName("input");
        for (var i = 0; i < checkBoxes.length; i++) {
            if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked ) {
                flag = true;
                break;
            }
        }

        if (!flag) {
            alert('Sila pilih rekod tanggungan untuk dihapuskan!');
            flag = false;
        }
        else {
            var r = confirm("Anda pasti hapuskan rekod tanggungan ini?");
            if (r == true) {
                flag = true;
            } else {
                flag = false;
            }
            
        }
        return flag;
        }

    </script>

    <style>
        .gv{
            
        }
    </style>

   
    <asp:UpdatePanel ID="up1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
             
        <div style="width:1500px;">
            
            <div class="well" style="width:25%;margin-left:35px;">
                <div style="margin-bottom:15px;">
                        Pilih Tahun :  <asp:DropDownList ID="ddlTahun" runat="server" CssClass="form-control" AutoPostBack="true">                            
                          </asp:DropDownList>
                    </div>
            </div>
            
             
            <div class="row" style="width:100%;margin-bottom:0px;">
                <div class="panel panel-default" style="width:60%;max-height:700px;">
                    <div class="panel-heading"><asp:Label ID="lblTitle1" runat ="server"/></div>
                <div class="panel-body">    
                                                     
                    <div class="GvTopPanel" style="height:40px;">
                <div style="float:left;margin-top: 5px;margin-left: 10px;width:100%;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                 &nbsp;&nbsp;   <b style="color:#969696;">|</b> &nbsp;&nbsp;
                <label class="control-label" for="">Tapisan :</label>&nbsp;
                 <asp:DropDownList ID="ddlCarian" runat="server" AutoPostBack="true"  CssClass="form-control" >                           
                 </asp:DropDownList>
                           &nbsp;&nbsp;
                          <asp:TextBox ID="txtCarian" runat="server" Width="250px" Enabled="false"></asp:TextBox>
                          <asp:DropDownList ID="ddlPTj" runat="server"  CssClass="form-control" Visible="false">                            
                          </asp:DropDownList>
                         
                    &nbsp;
                    <asp:LinkButton ID="lbtnCari" runat="server" CssClass="btn " Width="25px" Height="25px">
						<i class="fas fa-search"></i>
					</asp:LinkButton>                  
                </div>                                                 
            </div>                    
                    <asp:GridView ID="gvLstPT" runat="server"
                        AllowPaging="true"
                         AllowSorting="True" 
                             ShowHeaderWhenEmpty="True" 
                             AutoGenerateColumns="False" 
                             EmptyDataText="Tiada rekod" 
                             PageSize="15" cssclass="table table-striped table-bordered table-hover" 
                             Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" 
                             Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                      <columns>
                         <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" onclick = "checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="true" onclick = "Check_Click(this)" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bil">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No. PT">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoPT" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("PO19_NoPt"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ID Syarikat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdSya" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ROC01_IDSya"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nama Syarikat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNmSya" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ROC01_NamaSya"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="left"/>
                                                </asp:TemplateField>

                                               <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblJumlah" runat ="server" text="Jumlah Bayar (RM)" />&nbsp;
                                                        <asp:LinkButton id="lnkJumlah" runat="server" CommandName="Sort" CommandArgument="Jumlah"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJumlah" runat ="server" ForeColor="#003399" text='<%# Eval("PO02_JumSebenar", "{0:###,###,###.00}")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="120px"/>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblJumBaki" runat ="server" text="Jumlah Baki (RM)" />&nbsp;
                                                        <asp:LinkButton id="lnkJumBaki" runat="server" CommandName="Sort" CommandArgument="JumBaki"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJumBaki" runat ="server" text='<%# Eval("PO02_JumBlmByr", "{0:###,###,###.00}")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="120px"/>
                                                </asp:TemplateField>
                                            </columns>
                                            <HeaderStyle BackColor="#6699FF" />
                                        </asp:GridView>
                                     
                    </div>
                    <div class="panel-footer">
                        <div style="text-align:left;width:60%;">                      
                        <asp:LinkButton ID="lBtnTmbh" runat="server" CssClass="btn " ToolTip="Tambah ke senarai proses" Width="150px">
				            <i class="fas fa-plus"></i>&nbsp;&nbsp;&nbsp;Tambah ke senarai
			            </asp:LinkButton>                                              
                    </div>
                    </div>
                </div>      
                     
                </div>

            

            <div class="row" style="width:100%;margin-bottom:0px;">
                <div class="panel panel-default" style="width:60%;">
                    <div class="panel-heading"><asp:Label ID="lblTitle2" runat ="server"/><asp:Label ID="lblTahun" runat ="server" ForeColor="#003399"/></div>
                <div class="panel-body">
                                  
                    <div class="GvTopPanel" style="height:40px;">
                <div style="float:left;margin-top: 5px;margin-left: 10px;width:100%;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumPros" runat="server" style="color:mediumblue;" ></label>
                 &nbsp;&nbsp;   <b style="color:#969696;">|</b>          
                </div>
                            
                      
            </div>
                    <asp:GridView ID="gvProses" runat="server"
                        AllowPaging="true" 
                           AllowSorting="True" 
                             ShowHeaderWhenEmpty="True" 
                             AutoGenerateColumns="False" 
                             EmptyDataText="Tiada rekod" 
                             PageSize="15" cssclass="table table-striped table-bordered table-hover" 
                             Width="100%" Height="100%" HeaderStyle-BackColor="#6699FF" 
                             Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True">
                      <columns>
                         <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="cbSelectAll" runat="server" AutoPostBack="true" onclick = "checkAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="cbSelect" runat="server" AutoPostBack="true" onclick = "Check_Click(this)" />

                                <asp:LinkButton ID="btnChecked" runat="server">
                                    <a href="#" data-toggle="tooltip" title="Telah diproses" style="cursor:default;">
                                    <span style="color:#4CAF50;"><i class="fas fa-check-circle fa-lg"></i></span></a>
                                </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1px" />
                        </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bil">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBil" runat="server" Text="<%# Container.DataItemIndex + 1 %>" />
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No. PT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoPT" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("PO19_NoPt"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ID Syarikat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIdSya" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ROC01_IDSya"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="center" Width="100px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nama Syarikat">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNmSya" runat="server" Text='<%# Server.HtmlEncode(Convert.ToString(Eval("ROC01_NamaSya"))) %>' />
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="left"/>
                                                </asp:TemplateField>

                                               <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblJumlah" runat ="server" text="Jumlah Bayar (RM)" />&nbsp;
                                                        <asp:LinkButton id="lnkJumlah" runat="server" CommandName="Sort" CommandArgument="Jumlah"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJumlah" runat ="server" ForeColor="#003399" text='<%# Eval("PO02_JumSebenar", "{0:###,###,###.00}")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="120px"/>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblJumBaki" runat ="server" text="Jumlah Baki (RM)" />&nbsp;
                                                        <asp:LinkButton id="lnkJumBaki" runat="server" CommandName="Sort" CommandArgument="JumBaki"><span style="color:#4B4B4B"><i class="fas fa-sort fa-sm"></i></span></asp:LinkButton>                    
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJumBaki" runat ="server" text='<%# Eval("PO02_JumBlmByr", "{0:###,###,###.00}")%>' />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="120px"/>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField Visible="false">                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRujukan" runat ="server" text='<%# Eval("MK06_Rujukan")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </columns>
                                            <HeaderStyle BackColor="#6699FF" />
                                        </asp:GridView>
                                        
                    </div>
                    <div class="panel-footer">
                        <div style="text-align:left;width:60%;">                                    
                        <asp:LinkButton ID="lBtnHapus" runat="server" CssClass="btn " ToolTip="Hapus dari senarai proses" OnClientClick="return valGvProses();" Width="150px">
					        <i class="fas fa-trash-alt"></i>&nbsp;&nbsp;&nbsp;Hapus dari senarai
				        </asp:LinkButton>                           
                    </div>

                    </div>
                </div>      
                     
                </div>

            

            <div style="text-align:center;width:60%;">                      
              <asp:LinkButton ID="lnkBtnProses" runat="server" CssClass="btn " Width="80px" ToolTip="Proses tanggungan ke tahun hadapan" OnClientClick="return validate();">
			    <i class="far fa-save fa-lg"></i> &nbsp;&nbsp; Proses
		      </asp:LinkButton>                          
            </div>
        </div>

        </ContentTemplate>
        </asp:UpdatePanel>

     <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="up1">
                              <ProgressTemplate>                                 
                                    <div style="background-color:#D2D2D2;filter:alpha(opacity=80); opacity:0.80; width: 100%; top: 0px; left: 0px; position: fixed; height: 100%;">
                                        </div>
                                    <div style="margin:auto;font-family:Trebuchet MS;filter:alpha(opacity=100);opacity:1;font-size:small;vertical-align: middle;color: #FFFFFF;
position: fixed;top: 50%;left: 50%;margin-top: -50px;margin-left: -100px;">
                                        <table>
                                            <tr>
                                                <td style="text-align:center;">
                                                    <img src="../../../Images/loader.gif" alt=""  />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>                                 
                              </ProgressTemplate>
       </asp:UpdateProgress>
    
</asp:Content>
