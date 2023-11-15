<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Orang_Awam.aspx.vb" Inherits="SMKB_Web_Portal.Orang_Awam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>

     <script type="text/javascript">

         function isNumberKey(evt) {
             var charCode = (evt.which) ? evt.which : evt.keyCode;
             if (charCode > 31 && (charCode < 48 || charCode > 57))
                 return false;
             return true;
         }

         function fConfirm() {           
            try {
                var valTxtIDNO = document.getElementById('<%=txtIDNo.ClientID%>').value
                var valTxtNama = document.getElementById('<%=txtNama.ClientID%>').value
                var valTxtAlamat1 = document.getElementById('<%=txtAlamat1.ClientID%>').value
                var valTxtAlamat2 = document.getElementById('<%=txtAlamat2.ClientID%>').value
                var valTxtBandar = document.getElementById('<%=txtBandar.ClientID%>').value
                 var valTxtPoskod = document.getElementById('<%=txtPoskod.ClientID%>').value
                var valTxtNegeri = document.getElementById('<%=ddlNegeri.ClientID%>').value
                var valTxtNegara = document.getElementById('<%=ddlNegara.ClientID%>').value
                var valTxtNoTel = document.getElementById('<%=txtNoTel.ClientID%>').value
                var valTxtNoFax = document.getElementById('<%=txtNoFax.ClientID%>').value
                var valTxtEmel = document.getElementById('<%=txtEmel.ClientID%>').value
                
                if (valTxtIDNO == "")
                {
                    alert('Sila masukkan ID/No Kad Pengenalan!')
                    return false;
                }

                if (valTxtNama == "")
                {
                    alert('Sila masukkan nama!')
                    return false;
                }
                if (valTxtNama == "") {
                    alert('Sila masukkan nama!')
                    return false;
                }

                if (valTxtAlamat1 == "") {
                    alert('Sila masukkan alamat pertama!')
                    return false;
                }

                if (valTxtAlamat2 == "") {
                    alert('Sila masukkan alamat kedua!')
                    return false;
                }

                if (valTxtBandar == "") {
                    alert('Sila masukkan bandar!')
                    return false;
                }
                if (valTxtPoskod == "") {
                    alert('Sila masukkan poskod!')
                    return false;
                }

                if (valTxtNegeri == "") {
                    alert('Sila pilih negeri!')
                    return false;
                }
                if (valTxtNegara == "") {
                    alert('Sila pilih negara!')
                    return false;
                }
                if (valTxtNoTel == "") {
                    alert('Sila masukkan no tel!')
                    return false;
                }
                if (valTxtNoFax == "") {
                    alert('Sila masukkan no fax!')
                    return false;
                }
                if (valTxtEmel == "") {
                    alert('Sila masukkan emel!')
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
                <div class="panel panel-default" style="width:100%;margin-top:30px;margin-left:10px;">
    <div class="panel-body">
        <table class="nav-justified">
              
                  <tr>
                      <td style="height: 21px; width: 142px;">
                          <label class="control-label" for="">ID/No. Kad Pengenalan </label>
                      </td>
                      <td style="height: 21px;">:</td>
                      <td style="height: 21px">
                          <asp:TextBox ID="txtIDNo" runat="server" class="form-control" Width="20%"  onkeypress="return isNumberKey(event)" MaxLength ="12"></asp:TextBox> 
                           <button runat="server" id="btnFindOA" title="Tapisan" class="btnNone" style="margin-top:-4px;">                         
                           <i class="fas fa-search"></i>
                           </button>

						  &nbsp;  <i aria-hidden="true" class="fas fa-info-circle fa-lg" data-html="true" data-placement="right" data-toggle="tooltip" title="&nbsp;Masukkan nombor sahaja."></i>
                          <label class="control-label"style="color:red;" for="" > &nbsp;*(Cth:900101101100)</label>
                     
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIDNo" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                          <asp:RegularExpressionValidator ID="reftxtIDNo" runat="server" ControlToValidate="txtIDNo" ErrorMessage=" Sila isi 12 digit" ForeColor="#820303" ValidationGroup="btnSaveModul" ValidationExpression="^\d{12}$"></asp:RegularExpressionValidator>
                          
                      </td>
                      <td style="height: 21px"></td>
                      <td style="height: 21px"></td>
                      <td style="height: 21px"></td>
                  </tr>
        
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top; width: 142px;" >
                          <label class="control-label" for="">Nama</label>
                      </td>
                      <td style="height: 22px;vertical-align:top;">:</td>
                      <td style="height: 22px">
                         <asp:TextBox ID="txtNama" runat="server" class="form-control" Width="52%"></asp:TextBox>
						  &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNama" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator></td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top; width: 142px;">Alamat</td>
                      <td style="height: 22px;vertical-align:top;">:</td>
                      <td style="height: 22px">
                          <asp:TextBox ID="txtAlamat1" runat="server" class="form-control" Width="52%"></asp:TextBox>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAlamat1" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                      </td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top; width: 142px;">&nbsp;</td>
                      <td style="height: 22px;vertical-align:top;">&nbsp;</td>
                      <td style="height: 22px">
                          <asp:TextBox ID="txtAlamat2" runat="server" class="form-control" Width="52%"></asp:TextBox>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtAlamat2" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                      </td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top; width: 142px;">Negara</td>
                      <td style="height: 22px;vertical-align:top;">:</td>
                      <td style="height: 22px">
                          <asp:DropDownList ID="ddlNegara" runat="server" AutoPostBack="True" CssClass="form-control" value="<%=strKodNegeri%>" Height="21px" Width="52%">
                          </asp:DropDownList>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlNegara" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                      </td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                  </tr>
                  <tr style="height:25px">
                      <td style="height: 22px;vertical-align:top; width: 142px;">
                          Negeri</td>
                      <td style="height: 22px;vertical-align:top;">:</td>
                      <td style="height: 22px">
                          <asp:DropDownList ID="ddlNegeri" runat="server" AutoPostBack="True" CssClass="form-control" value="<%=strKodNegeri%>" style="width: 52%; height: 21px;">
                          </asp:DropDownList>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlNegeri" Display="Dynamic" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul"></asp:RequiredFieldValidator>
                      </td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                  </tr>
                  <tr>
                     <td >Bandar</td>
                      <td>:</td>
                      <td>
                          <asp:TextBox ID="txtBandar" runat="server" CssClass="form-control"  Width="185px"></asp:TextBox>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtBandar" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                          &nbsp;&nbsp; &nbsp;Poskod  &nbsp;:&nbsp;&nbsp; <asp:TextBox ID="txtPoskod" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" MaxLength ="5" Width="185px"></asp:TextBox>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtPoskod" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPoskod" ErrorMessage=" Sila isi 5 digit" ForeColor="#820303" ValidationGroup="btnSaveModul" ValidationExpression="^\d{5}$"></asp:RegularExpressionValidator>
                      </td> 
                      <td>&nbsp;</td>
                      <td style="height: 22px"></td>
                      <td style="height: 22px"></td>
                      <td style="height: 22px"></td>
                  </tr>
                  <tr style="height:25px">
                      <td>No. Tel</td>
                      <td>:</td>
                      <td>
                          <asp:TextBox ID="txtNoTel" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" Width="185px" MaxLength ="10"></asp:TextBox>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtNoTel" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                          &nbsp; &nbsp; No.Fax &nbsp;&nbsp;:&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNoFax" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event)" style="width: 185px;" MaxLength ="10"></asp:TextBox>
                          &nbsp;        <label class="control-label"style="color:red;" for="" > *(Cth:0426543210)</label>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtNoFax" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                    
                      </td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                  </tr>
                  <tr style="height:25px">
                      <td>Emel </td>
                      <td>:</td>
                      <td>
                          <asp:TextBox ID="txtEmel" runat="server" CssClass="form-control"  Width="52%"></asp:TextBox>
                          &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmel" ForeColor="#820303" Text="*Perlu diisi" ValidationGroup="btnSaveModul" Display="Dynamic" ></asp:RequiredFieldValidator>
                      </td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                  </tr>
                  <tr style="height:25px">
                      <td>Status</td>
                      <td>:</td>
                       <td style="height: 22px;">                           
						   <asp:RadioButtonList ID="rbStatus" runat="server" Height="25px" RepeatDirection="Horizontal" Width="273px">
                               <asp:ListItem Selected="True" Text=" Aktif" Value="1"></asp:ListItem>
                               <asp:ListItem Text=" Tidak Aktif" Value="0"></asp:ListItem>
                           </asp:RadioButtonList>
											  
					  </td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                      <td style="height: 22px">&nbsp;</td>
                  </tr>
                  <tr style="height:55px;vertical-align :bottom ">
                      <td style="width: 142px">&nbsp; </td>
                       <td>
                             &nbsp;</td>
                       <td style="text-align:left">
                             <asp:LinkButton ID="lbtnBaru" runat="server" CssClass="btn btn-info">
						<i class="fas fa-file-alt"></i>&nbsp;&nbsp;&nbsp;Rekod Baru
					</asp:LinkButton>
                           &nbsp;&nbsp;
						   <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveModul" >
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
					</asp:LinkButton>
						   &nbsp;&nbsp;
						 <asp:Button ID="btnHapus" runat="server" CssClass="btn" Text="Hapus" ToolTip="Hapus" OnClientClick="return fConfirmDel()"/>
											  
					  </td>
                          </td>
                      <td style="text-align:left">&nbsp;</td>
                      <td style="text-align:left">&nbsp;</td>
                      <td style="text-align:left">&nbsp;</td>
                  </tr>

          </table>
        </div>
              </div>
                </div>

	<div class="col-sm-3 col-md-6">      
		<table>
			
	<tr style="height:30px;">
		<td style="width:100px;">
			<%--<label class="control-label" for="Klasifikasi">Saiz Rekod : </label>--%>
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
		<asp:GridView ID="gvOrangAwam" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" EmptyDataText=" " ShowFooter="false"
			 BorderColor="#333333" BorderStyle="Solid" CssClass="table table-striped table-bordered table-hover" Font-Size="8pt" PageSize="25" ShowHeaderWhenEmpty="True" Width="100%">
			<Columns>
				<asp:TemplateField HeaderText = "Bil">
		<ItemTemplate>
			<asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
		</ItemTemplate>
	<ItemStyle Width="5px" />
	</asp:TemplateField>
				<asp:BoundField DataField="IDNo_KP" HeaderText="ID/NO Kad Pengenalan" ReadOnly="True" SortExpression="IDNo_KP">
				<ControlStyle Width="10px" />
				<ItemStyle Width="20%" HorizontalAlign="Center"/>
				</asp:BoundField>
				<asp:BoundField DataField="IDNama" HeaderText="Nama" SortExpression="IDNama">
				<ControlStyle Width="30%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                <asp:BoundField DataField="Alamat1" HeaderText="Alamat 1" SortExpression="Alamat1">
				<ControlStyle Width="50%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                <asp:BoundField DataField="Alamat2" HeaderText="Alamat 2" SortExpression="Alamat2">
				<ControlStyle Width="50%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                 <asp:BoundField DataField="KodNegara" HeaderText="Negara" SortExpression="KodNegara">
				<ControlStyle Width="20%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                 <asp:BoundField DataField="KodNegeri" HeaderText="Negeri" SortExpression="KodNegeri">
				<ControlStyle Width="20%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                <asp:BoundField DataField="Bandar" HeaderText="Bandar" SortExpression="Bandar">
				<ControlStyle Width="50%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                <asp:BoundField DataField="Poskod" HeaderText="Poskod" SortExpression="Poskod">
				<ControlStyle Width="20%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                <asp:BoundField DataField="No_Tel" HeaderText="No. Tel" SortExpression="NoTel">
				<ControlStyle Width="20%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                <asp:BoundField DataField="No_Fax" HeaderText="No. Fax" SortExpression="NoFax">
				<ControlStyle Width="20%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                <asp:BoundField DataField="Emel" HeaderText="Emel" SortExpression="Emel">
				<ControlStyle Width="30%" />
				<ItemStyle Width="80%" />
				</asp:BoundField>
                 <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
				<ControlStyle Width="20%" />
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
		 <%-- <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
		<asp:Button ID="Button3" runat="server" Text="+" />
					<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgBtnCancel" PopupControlID="pnlpopup" TargetControlID="btnShowPopup" BehaviorID="_content_ModalPopupExtender1" DynamicServicePath="">
									 </ajaxToolkit:ModalPopupExtender>
		<ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" CancelControlID="imgBtnCancel" PopupControlID="pnlpopup" TargetControlID="Button3" BehaviorID="_content_ModalPopupExtender1" DynamicServicePath="">
									 </ajaxToolkit:ModalPopupExtender>     
		<br />
		<br />
		<asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
		<asp:HiddenField ID="HidModulStatus" runat="server" />--%>
	</div>
	
  </div>
					</ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>
