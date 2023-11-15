<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Pindah_Bajet.aspx.vb" Inherits="SMKB_Web_Portal.Pindah_Bajet" EnableEventValidation="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <style>
        @media (min-width: 1450px){
            .panel
            {
                width :70%;
            }
            .row
            {
                width :70%;
            }
        }

        @media (max-width: 1450px) {
            .panel {
                width: 80%;
            }        
        }

        @media (max-width: 1310px) {
              .panel {
                  width: 90%;
              }
          }

          @media (max-width: 1050px) {
              .panel {
                  width: 100%;
              }
          }         
    </style>

    <script>
        function fConfirm() {

            try {
                if (confirm('Anda pasti untuk meneruskan proses?')) {
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

        <div class="row">
            <div runat="server" id="alert" class="alert alert-danger">
Kaedah proses bawa bajet belum ditetapkan!
</div>
            </div>
      
    <div class="row">
      <div class="panel panel-default">
      <div class="panel-body"> 
        <table class="nav-justified">
              <tr>
                  <td style="width: 100px"><label class="control-label" for="">Tahun:</label></td>
                  <td>
                      <asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 20%;">
                      </asp:DropDownList>
                  </td>
              </tr>
              <tr style="height:25px">
                  <td><label class="control-label" for="">Kumpulan Wang:   
                  </td>
                  <td>
                    <asp:DropDownList ID="ddlKW" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 70%;">
                    </asp:DropDownList>
                  </td>
              </tr>
             <tr style="height:25px">
                  <td ><label class="control-label" for="">PTJ:   
                  </td>
                  <td>
                    <asp:DropDownList ID="ddlPTJ" runat="server" AutoPostBack="True" CssClass="form-control" style="width: 95%;">
                    </asp:DropDownList>
                  </td>
              </tr>
            <tr style="height:55px;">
                    <td>
                        &nbsp;</td>
                    <td>
                        
                        <asp:LinkButton ID="lbtnPapar" runat="server" CssClass="btn "  ValidationGroup="btnPapar">
									<i class="far fa-list-alt fa-lg"></i>&nbsp;&nbsp;&nbsp;Papar
								</asp:LinkButton>
                    </td>
            </tr>
            
          </table>
        </div>
        </div>
</div>
<div class="row">
    
    <div class="GvTopPanel">
                <div style="float:left;margin-top: 8px;margin-left: 10px;">
                <label class="control-label" for="">Jumlah rekod :</label>&nbsp;<label class="control-label" id ="lblJumRekod" runat="server" style="color:mediumblue;" ></label>
                    </div>  
                     
                        </div>           
      <asp:GridView ID="gvPindahBajet" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" 
        CssClass= "table table-striped table-bordered table-hover" Width="100%"  Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True" AllowPaging="True" EmptyDataText=" "  PageSize="25" >
          <columns>

            <asp:TemplateField HeaderText = "Bil">
        <ItemTemplate>
            <asp:Label ID="lblBil" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
                                      <ItemStyle Width="30px" HorizontalAlign="Center" />
    </asp:TemplateField>

            <asp:BoundField HeaderText="KW" DataField="kodKW" ReadOnly="True" >
            <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:BoundField>

            <asp:BoundField HeaderText="KO" DataField="kodKO" >
              <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:BoundField>

            <asp:BoundField HeaderText="PTj" DataField="kodPTj" SortExpression="PTj" >
              <ItemStyle HorizontalAlign="Center" Width="10%" />
            </asp:BoundField>

              <asp:BoundField DataField="kodKP" HeaderText="KP" >

              <ItemStyle HorizontalAlign="Center" Width="10%" />
              </asp:BoundField>

            <asp:BoundField HeaderText="Vot" DataField="Kodvot" SortExpression="Vot" >
              <ControlStyle Width="50px" />
              <ItemStyle Width="10%" HorizontalAlign="Center" />
            <ItemStyle Width="8%" />
            </asp:BoundField>

            <asp:BoundField HeaderText="Baki Peruntukan (RM)" DataField="Baki" SortExpression="Baki" >
            <ItemStyle Width="20%" HorizontalAlign="Right" />
            </asp:BoundField>

            <asp:BoundField HeaderText="Bawa ke Hadapan (RM)" DataField="Bf" SortExpression="Bf" NullDisplayText="-">
            <ItemStyle Width="20%" HorizontalAlign="Right" />
            </asp:BoundField>
               <asp:TemplateField>
                   <ItemTemplate>
                       <%--<asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="25px" ImageUrl="~/Images/Edit_48.png" 
                        ToolTip="Kemas Kini" Width="20px" OnItemCommand="gvPeruntukan_ItemCommand" />--%>&nbsp;&nbsp; <%--<asp:ImageButton ID="btnDelete0" runat="server" CommandName="Delete" Height="18px" ImageUrl="~/Images/Delete_32x32.png" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" ToolTip="Padam" Width="15px" />--%>
                       <asp:LinkButton ID="btnDelete0" runat="server" CommandName="Delete" CssClass="btn-xs" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" ToolTip="Padam">
						                <i class="fa fa-trash-o fa-lg"></i>
					                </asp:LinkButton>
                   </ItemTemplate>
                   <ItemStyle Width="5%" />
              </asp:TemplateField>
               </columns>
           
          <HeaderStyle BackColor="#6699FF" />

          <RowStyle Height="5px" />

          <SelectedRowStyle  ForeColor="Blue" />

    </asp:GridView>
          <div style="text-align:center;" >
              <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " OnClientClick="return fConfirm();">
									<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
								</asp:LinkButton>
          </div>                
    </div>
    
    </ContentTemplate>
    </asp:UpdatePanel>  

</asp:Content>
