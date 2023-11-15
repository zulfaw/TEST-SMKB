<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Kod_Projek.aspx.vb" Inherits="SMKB_Web_Portal.Kod_Projek" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
    <script >

        function fCheckChar(e, t) {
            try {
              
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }

                if (charCode == 48) {
                    return true;
                }

                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))

                    return true;
                else
                    return false;

            }
            catch (err) {
                alert(err.Description);
            }
        }
        
        function fConfirm() {           
            try {
                var valTxtButiran = document.getElementById('<%=txtButiran.ClientID%>').value
                var valDdlPrefix = document.getElementById('<%=ddlPrefix.ClientID%>').value
                
                if (valDdlPrefix == "0")
                {
                    alert('Sila pilih Prefix!')
                    return false;
                }

                if (valTxtButiran == "")
                {
                    alert('Sila masukkan Butiran Projek!')
                    return false;
                }

                
                if (confirm('Anda pasti untuk simpan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }
            }
            catch (err) {
                return false
            }
        }

        function fConfirmPfx() {
            try {

                <%--var valTxtPrefix = document.getElementById('<%=txtPrefix.ClientID%>').value
                var valTxtButiranPfx = document.getElementById('<%=txtButiranPfx.ClientID%>').value
                if (valTxtPrefix == "" || valTxtButiranPfx == ""){
                    alert('Sila masukkan Prefix dan Butiran!')
                    return false
                }

                if (confirm('Anda pasti untuk simpan rekod ini?')) {
                    return true;
                } else {
                    return false;
                }--%>

            }

            catch (err) {

            }
        }

        function fConfirmDelPfx() {
            var valTxtPrefix = document.getElementById('<%=txtPrefix.ClientID%>').value
            if (valTxtPrefix == ""){
                alert('Sila pilih rekod yang hendak dihapuskan!');
                return false;
            }

            if (confirm('Anda pasti untuk hapuskan rekod ini?')) {
                return true;
            } else {
                return false;
            }

        }
        
                

    </script>

     <style>

         .row
            {
                width :80%;
                margin-left:10px;
            }

         .row1{
              width :70%;
         }

       @media (max-width: 1500px){
           
            .row
            {
                width :95%;
                
            }

            .row1{
                width :95%;
            }
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            

            <div class="row">
           <ajaxToolkit:TabContainer ID="TabContainer1"  runat="server" Width="100%" Height="800px"  CssClass="tabCtrl" ActiveTabIndex="1" AutoPostBack="true" >                                    
				<ajaxToolkit:TabPanel ID="tabKodProjek" runat="Server" HeaderText ="Daftar Kod Projek">
					<ContentTemplate>
					     
	  <div class="row1"> 
                <div class="panel panel-default" style="width:100%;margin-top:30px;margin-left:10px;">
    <div class="panel-body">
        <table class="nav-justified">
              
                  <tr style="height:35px">
                      <td style="height: 22px;width:100px;">
                          <label class="control-label" for="Klasifikasi">
                          Kod Projek :
                          </label>
                      </td>
                      <td>
                          <asp:DropDownList ID="ddlPrefix" runat="server"  CssClass="form-control" style="width: 150px;" AutoPostBack="True">
                          </asp:DropDownList>
                          &nbsp;-
                          <asp:TextBox ID="txtKodKP" runat="server" BackColor="#FFFFCC" CssClass="form-control" ReadOnly="True" style="width: 250px;" ></asp:TextBox>
                          <br />
                          <asp:TextBox ID="HidtxtId" runat="server" Visible="False" style="width: 100px;"></asp:TextBox>
                      </td>
                  </tr>
        
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top;" >
                          <label class="control-label" for="Jenis">
                          Butiran Projek :
                          </label>
                      </td>
                      <td style="height: 22px">
                          <asp:TextBox ID="txtButiran" runat="server"  cssClass="form-control" Height="50px" textmode="MultiLine" Width="90%"></asp:TextBox>
                      </td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top;">Status :</td>
                      <td style="height: 22px">
                          <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                              <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                              <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                          </asp:RadioButtonList>
                      </td>
                  </tr>
                  <tr style="height:55px;vertical-align :bottom ">
                      <td>&nbsp; </td>
                      <td>&nbsp;
                                                    
                          <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm()">
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
								</asp:LinkButton>
                          &nbsp;&nbsp; &nbsp;&nbsp;
                          <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn ">
									<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
								</asp:LinkButton>
                          &nbsp;&nbsp; &nbsp;&nbsp;
                          </td>
                  </tr>

          </table>
        </div>
              </div>
                </div>

            <div class="row" style="width:100%;">
                <div class="GvTopPanel">
                <div style="float:left;margin-top: 8px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                    </div>  
                                      
                        </div>
            <asp:GridView ID="gvProjek" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" 
                          BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" Font-Size="8pt" 
                          PageSize="20" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" EmptyDataText=" "
                DataKeyNames="ID">
                          <columns>
                             <asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="5%" HorizontalAlign="Center" />
    </asp:TemplateField>
                              <asp:BoundField DataField="KodKP" HeaderText="Kod Projek" ReadOnly="True">
                              <ItemStyle Width="20%" HorizontalAlign="Center" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Butiran" HeaderText="Butiran" ReadOnly="True" >
                              <HeaderStyle HorizontalAlign="Left" />
                              <ItemStyle HorizontalAlign="Left" Width="75%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Status" HeaderText="Status" />
                              <asp:TemplateField>
                                  <ItemTemplate>
                                      <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle Width="20px" />
                              </asp:TemplateField>
                              <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                          </columns>
                          <HeaderStyle BackColor="#6699FF" />
                      </asp:GridView>
            </div>
	                    
					</ContentTemplate>
				</ajaxToolkit:TabPanel>

					<ajaxToolkit:TabPanel ID="tabPrefix" runat="Server" HeaderText ="Daftar Prefix">
					<ContentTemplate>

						<div class="row1"> 
                <div class="panel panel-default" style="width:100%;margin-top:30px;margin-left:10px;">
    <div class="panel-body">
        <table class="nav-justified">
              
                  <tr style="height:35px">
                      <td style="height: 22px;width:100px;">
                          <label class="control-label" for="Klasifikasi">
                          Prefix :
                          </label>
                      </td>
                      <td>
                          
                          <asp:TextBox ID="txtPrefix" runat="server" class="form-control" Width="20%" onkeypress="return fCheckChar(event,this);" MaxLength="2"></asp:TextBox>
                          
                          <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-html="true" data-placement="right" data-toggle="tooltip" title="&nbsp;Masukkan 2 huruf sahaja."></i>
                          
                          </td>
                  </tr>
        
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top;">KW :</td>
                      <td style="height: 22px">
                          <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 280px;">
                          </asp:DropDownList>
                      </td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top;">Status :</td>
                      <td style="height: 22px">
                          <asp:RadioButtonList ID="rbStatusPfx" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                              <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                              <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                          </asp:RadioButtonList>
                      </td>
                  </tr>
                  <tr style="height:55px;vertical-align :bottom ">
                      <td>&nbsp; </td>
                      <td>&nbsp;&nbsp;&nbsp;
                          
                          <asp:LinkButton ID="lbtnSimpanPfx" runat="server" CssClass="btn " OnClientClick="return fConfirmPfx()" >
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
								</asp:LinkButton>
                          &nbsp;&nbsp;&nbsp; &nbsp;
                          <asp:LinkButton ID="lbtnBaruPfx" runat="server" CssClass="btn " >
									<i class="fas fa-file-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Rekod baru
								</asp:LinkButton>
                          </td>
                  </tr>

          </table>
        </div>
              </div>
                </div>

            <div class="row" style="width:100%;">
                <div class="GvTopPanel">
                <div style="float:left;margin-top: 8px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRecPfx" runat="server" style="color:mediumblue;" ></label>
                    </div>  
                                      
                        </div>
            <asp:GridView ID="gvPfx" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" 
                          BorderStyle="Solid" cssclass="table table-striped table-bordered table-hover" Font-Size="8pt" 
                          PageSize="20" ShowFooter="True" ShowHeaderWhenEmpty="True" Width="100%" EmptyDataText=" ">
                          <columns>
                             <asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="5%" HorizontalAlign="Center" />
    </asp:TemplateField>
                              <asp:BoundField DataField="KodPrefix" HeaderText="Prefix" ReadOnly="True">
                              <ItemStyle Width="10%" HorizontalAlign="Center" />
                              </asp:BoundField>
                              <asp:BoundField DataField="KW" HeaderText="KW" ReadOnly="True" >
                              <HeaderStyle HorizontalAlign="Left" />
                              <ItemStyle HorizontalAlign="Left" Width="75%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="status" HeaderText="Status">
                              <ItemStyle Width="10%" />
                              </asp:BoundField>
                              <asp:TemplateField>
                                  <EditItemTemplate>
                                      <asp:ImageButton ID="btnUpdate" runat="server" CommandName="Update" Height="25px" ImageUrl="~/Images/Save_48x48.png" OnItemCommand="gvSubMenu_ItemCommand" ToolTip="Simpan" Width="20px" />
                                      &nbsp;&nbsp;
                                      <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" Height="25px" ImageUrl="~/Images/Cancel16x16.png" OnItemCommand="gvSubMenu_ItemCommand" ToolTip="Batal" Width="20px" />
                                      &nbsp;&nbsp;
                                  </EditItemTemplate>
                                  <ItemTemplate>
                                      <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle Width="5%" />
                              </asp:TemplateField>
                          </columns>
                          <HeaderStyle BackColor="#6699FF" />
                      </asp:GridView>
            </div>
					</ContentTemplate>
				</ajaxToolkit:TabPanel>

					

				</ajaxToolkit:TabContainer>               
 </div>

            </ContentTemplate></asp:UpdatePanel>

</asp:Content>
