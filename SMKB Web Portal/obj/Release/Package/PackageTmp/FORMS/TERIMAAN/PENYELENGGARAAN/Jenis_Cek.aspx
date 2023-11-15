<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Jenis_Cek.aspx.vb" Inherits="SMKB_Web_Portal.Jenic_Cek" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

    <script type="text/javascript">

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
                var valTxtKodCek = document.getElementById('<%=txtKodCek.ClientID%>').value
                var valTxtNamaCek = document.getElementById('<%=txtNamaCek.ClientID%>').value
                
                if (valTxtKodCek == "")
                {
                    alert('Sila masukkan kod jenis cek!')
                    return false;
                }

                if (valTxtNamaCek == "")
                {
                    alert('Sila masukkan butiran nama jenis cek!')
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


      function fConfirmDel() {

          try {
              if (confirm('Anda pasti untuk padam maklumat ini?')) {
                  return true;
              } else {
                  return false;
              }
          }
          catch (err) {
              alert(err)
              return false;
          }
      }

   </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="panel panel-default">
                <div class="panel-body">
            <div class="container-fluid">
                
       <div class="row"> 
                <div class="panel panel-default" style="width:70%;margin-top:30px;margin-left:10px;">
    <div class="panel-body">
        <table class="nav-justified">
              
                  <tr style="height:35px">
                      <td style="height: 22px;">
                          <label class="control-label" for="">Kod Jenis Cek:</label>
                      </td>
                      <td>
                          <asp:TextBox ID="txtKodCek" runat="server" class="form-control" Width="20%" MaxLength="1"  onkeypress="return fCheckChar(event,this);"></asp:TextBox>
						  &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtKodCek" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                       <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-html="true" data-placement="right" data-toggle="tooltip" title="&nbsp;Masukkan 1 huruf sahaja."></i>
                      </td>
                  </tr>
        
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top;" >
                          <label class="control-label" for="">Nama Jenis Cek:</label>
                      </td>
                      <td style="height: 22px">
                         <asp:TextBox ID="txtNamaCek" runat="server" class="form-control" Width="80%"></asp:TextBox>
						  &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNamaCek" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator></td>
                  </tr>
                  <tr style="height:55px;vertical-align :bottom ">
                      <td>&nbsp; </td>
                       <td colspan="2" style="text-align:left">
                             <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn btn-info">
						<i class="fas fa-file-alt"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
					</asp:LinkButton>
                           &nbsp;&nbsp;
						   <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" OnClientClick="return fConfirm();">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
						   &nbsp;&nbsp;
						 <asp:Button ID="btnHapus" runat="server" CssClass="btn" Text="Hapus" ToolTip="Hapus" ValidationGroup="btnHapus" OnClientClick="return fConfirmDel()"/>
											  
					  </td>
                          </td>
                  </tr>

          </table>
        </div>
              </div>
                </div>

	<div class="col-sm-3 col-md-6">      
		<table>
			
	<tr style="height:30px;">
		<td style="width:100px;">
             <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
		</td>
		<%--<td style="width:50px;">
			<asp:DropDownList ID="ddlSaizRekod" runat="server" Width="50px" AutoPostBack="True" class="form-control">
				<asp:ListItem Text="10" Value="10" />
				<asp:ListItem Text="25" Value="25" Selected="True" />
				<asp:ListItem Text="50" Value="50" />
				<asp:ListItem Text="100" Value="100" />
			</asp:DropDownList>
		</td>--%>
		</tr>
		</table> 
		<asp:GridView ID="gvJenisCek" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText=" " ShowFooter="false"
			 BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" PageSize="25" ShowHeaderWhenEmpty="True" Width="70%">
			<Columns>
				<asp:TemplateField HeaderText = "Bil">
		<ItemTemplate>
			<asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
		</ItemTemplate>
	<ItemStyle Width="5px" />
	</asp:TemplateField>
				<asp:BoundField DataField="KodJenisCek" HeaderText="Kod Jenis Cek" ReadOnly="True" SortExpression="KodJenisCek">
				<ControlStyle Width="10px" />
				<ItemStyle Width="20%" HorizontalAlign="Center"/>
				</asp:BoundField>
				<asp:BoundField DataField="Butiran" HeaderText="Nama Jenis Cek" SortExpression="NamaJenisCek">
				<ControlStyle Width="50%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
				<asp:TemplateField>
					<ItemTemplate>
					 <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Pilih">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
					</ItemTemplate>

					<ItemStyle Width="3%" />
				</asp:TemplateField>
			</Columns>
			<HeaderStyle BackColor="#6699FF" />
			<RowStyle Height="5px" />
			<SelectedRowStyle ForeColor="Blue" />
		</asp:GridView>

	</div>
	
  </div>
					</ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
