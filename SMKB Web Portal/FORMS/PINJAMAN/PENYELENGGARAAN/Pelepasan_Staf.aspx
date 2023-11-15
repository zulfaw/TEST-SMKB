<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pelepasan_Staf.aspx.vb" Inherits="SMKB_Web_Portal.Pelepasan_Staf" %>
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
                var valTxtButiran = document.getElementById('<%%>').value
                var valDdlPrefix = document.getElementById('<%%>').value
                
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
            var valTxtPrefix = document.getElementById('<%'=txtPrefix.ClientID%>').value
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
              width :65%;
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
         .auto-style2 {
             width: 1538px;
         }
         .auto-style3 {
             height: 22px;
         }
         .auto-style4 {
             height: 22px;
             width: 1599px;
         }
         .auto-style5 {
             height: 22px;
             width: 1538px;
         }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<div class="panel panel-default">
                <div class="panel-heading">
                    <h3 class="panel-title">Senarai Pelepasan Staf
                    </h3>
                </div>
            <div class="row">					     
	  <div class="row1" style="width:100%;"> 
                <div class="panel panel-default" style="width:100%;margin-top:30px;margin-left:10px;">
    <div class="panel-body"  style="overflow-x: auto">
        <table class="nav-justified">              
                  <tr style="height:35px">
                      <td class="auto-style5">
                          <label class="control-label" for="Klasifikasi">
                          Nama Staf :
                          </label>
                      </td>
                      <td>
                          <asp:DropDownList ID="ddlNamaStaf" runat="server"  CssClass="form-control" style="width: 250px;" AutoPostBack="True">
                          </asp:DropDownList>
                      </td>
                  </tr>
        
                  <tr>
                      <td style="vertical-align:top;" class="auto-style5" >
                          <label class="control-label" for="Jenis">
                          Surat Kelulusan :
                          </label>
                      </td>
                      <td class="auto-style3">
                          <asp:TextBox ID="txtButiran" runat="server"  cssClass="form-control" Height="50px" textmode="MultiLine" Width="250px"></asp:TextBox>
                      </td>
                  </tr>
                  <tr style="height:55px;vertical-align :bottom ">
                      <td class="auto-style2">&nbsp; </td>
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
            <asp:GridView ID="gvList" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BorderColor="#333333" 
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
                              <asp:BoundField DataField="NoStaf" HeaderText="No Staf" ReadOnly="True">
                              <ItemStyle Width="20%" HorizontalAlign="Center" />
                              </asp:BoundField>
                              <asp:BoundField DataField="ms01_nama" HeaderText="Nama" ReadOnly="True" >
                              <HeaderStyle HorizontalAlign="Left" />
                              <ItemStyle HorizontalAlign="Left" Width="35%" />
                              </asp:BoundField>
                              <asp:BoundField DataField="Pejabat" HeaderText="Pejabat" />
                              <asp:TemplateField>
                                  <ItemTemplate>
                                      <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn-xs" ToolTip="Kemaskini">
                                            <i class="fas fa-edit"></i>
                                            </asp:LinkButton>
                                  </ItemTemplate>
                                  <ItemStyle Width="25%"  />
                              </asp:TemplateField>
                              <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" />
                          </columns>
                          <HeaderStyle BackColor="#6699FF" />
                      </asp:GridView>
            </div>
	                    
 </div>

            </ContentTemplate></asp:UpdatePanel>

</asp:Content>
