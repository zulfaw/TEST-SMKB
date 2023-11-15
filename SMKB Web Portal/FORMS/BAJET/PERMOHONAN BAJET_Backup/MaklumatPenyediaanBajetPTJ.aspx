<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="MaklumatPenyediaanBajetPTJ.aspx.vb" Inherits="SMKB_Web_Portal.MaklumatPenyediaanBajetPTJ" EnableEventValidation ="False" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
<%--    <script>
        debugger
    function calculate() {
        var myQty = document.getElementById('txtKuantiti').value;
        var myAvPrice = document.getElementById('txtAngHrgUnit').value;
        var result = document.getElementById('txtAngJum');
        var myResult = myQty * myAvPrice;
		result.value = myResult;
    }
        </script>--%>

   <script type="text/javascript" >

       function CheckNumber(e)
       {
           var charCode = (e.which) ? e.which : event.keyCode
           if (charCode > 31 && (charCode < 48 || charCode > 57))
               return false;
 
           return true;
       }


       function calculate(e) {
           debugger;

           txtKuantiti = document.getElementById('<%=txtKuantiti.ClientID%>');
           txtAngHrgUnit = document.getElementById('<%=txtAngHrgUnit.ClientID%>');
           txtAngJum = document.getElementById('<%=txtAngJum.ClientID%>');
           

           var Kuantiti = txtKuantiti.value;
           var HargaUnit = txtAngHrgUnit.value;
           var Jumlah = parseFloat(Kuantiti) * parseFloat(HargaUnit);
           txtAngJum.value = Jumlah

           <%-- var Kuantiti = document.getElementById("<%=txtKuantiti  %>");
            var HargaUnit = document.getElementById("<%=txtAngHrgUnit %>");
            var Jumlah = document.getElementById(" <%=txtAngJum %>");
            var result =Kuantiti* HargaUnit;
            Jumlah.value = result;--%>
         }
     </script>





    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div class="container-fluid">
         
        <div class="row">
            
            <div class="col-sm-12 col-md-8 col-lg-8">
                <div>
                    <asp:LinkButton ID="lbtnKembali" runat="server" CssClass="btn btn-info">
                    <i class="fa fa-arrow-left fa-lg"></i>&nbsp;&nbsp;&nbsp;Kembali
                    </asp:LinkButton>
                </div>
                <br/>
        <div class="panel-group">
        <div class="panel panel-default">
         
        <div class="panel-heading">
            <h4 class="panel-title">Permohonan</h4>
           
          </div>
            <div id="PanelMohonBaru" class="panel-collapse">
            <div class="panel-body">
            <table style="width:100%" class="table table table-borderless">
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">No Mohon</Label></td>
                    <td>
                        <asp:TextBox ID="txtNoMohon" runat="server" Width="150px" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        &nbsp &nbsp &nbsp &nbsp
                        <Label class="control-label" for="">Status</Label>
                        &nbsp &nbsp
                        <asp:TextBox ID="txtStatus" runat="server" Width="40%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Tarikh Mohon</Label></td>
                    <td>
                        <asp:TextBox ID="txtTarikh" runat="server" CssClass="form-control centerAlign" Width="80px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <%--<tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Maksud</Label></td>
                    <td>
                        <asp:TextBox ID="txtMaksud" runat="server" CssClass="form-control" Width="60%" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Agensi</Label></td>
                    <td>
                        <asp:TextBox ID="txtAgensi" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>--%>
                <%--<tr>
                    <td style="width: 15%;"><Label class="control-label" for="">PTj</Label></td>
                    <td>
                        <asp:TextBox ID="txtPtj" runat="server" CssClass="form-control" Width="80%" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Bahagian</Label></td>
                    <td>
                        <asp:TextBox ID="txtBahagian" runat="server" Width="80%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        <%--<asp:DropDownList ID="ddlBahagian" runat="server" CssClass="form-control" Width="80%" AutoPostBack="true"></asp:DropDownList>--%>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBahagian" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Unit</Label></td>
                    <td>
                        <asp:TextBox ID="txtUnit" runat="server" Width="80%" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        
                 <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Kod Operasi</Label></td>
                    <td>
                        <asp:DropDownList ID="ddlKodOperasi" runat="server" CssClass="form-control" Width="50%"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvKodOperasi" runat="server" ControlToValidate="ddlKodOperasi" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Kump. Wang</Label></td>
                    <td>
                        <asp:DropDownList ID="ddlKW" runat="server" CssClass="form-control" Width="80%" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvddlKW" runat="server" ControlToValidate="ddlKW" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Program</Label></td>
                    <td>
                        <asp:TextBox ID="txtProgram" runat="server" textmode="multiline" CssClass="form-control" Width="100%"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvProg" runat="server" ControlToValidate="txtProgram" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 15%;"><Label class="control-label" for="">Justifikasi</Label></td>
                    <td>
                        <asp:TextBox ID="txtJust" runat="server" textmode="multiline" CssClass="form-control" Width="100%" Rows="5"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="rfvJust" runat="server" ControlToValidate="txtJust" ErrorMessage="" ForeColor="#820303" Text="*Sila Isi" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
            </table>
            <br />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>

                
            <div class="panel panel-default">
            <div class="panel-heading">Butiran Perbelanjaan</div>
            <div class="panel-body">
           
            <table style="width:100%" class="table table table-borderless">
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Vot Sebagai</Label></td>
                    <td>
                        <asp:dropdownlist ID="ddlVotSbg" runat="server" CssClass="form-control" Width="80%"></asp:dropdownlist>
                        <asp:RequiredFieldValidator ID="rfvVotSbg" runat="server" ControlToValidate="ddlVotSbg" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                    <td>
                         <asp:TextBox ID="txtNoButiran" runat="server" CssClass="form-control" Width="30px" TextMode="Number" AutoPostBack="true" Visible="false"></asp:TextBox>
                    </td>
                     <td>
                         <asp:TextBox ID="txtBil" runat="server" CssClass="form-control" Width="30px" TextMode="Number" AutoPostBack="true" Visible="false"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Butiran</Label></td>
                    <td>
                        <asp:TextBox ID="txtButiran" runat="server" CssClass="form-control" Width="100%" TextMode="MultiLine" Rows="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvButiran" runat="server" ControlToValidate="txtButiran" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Kuantiti</Label></td>
                    <td>
                        <%--<input id="txtKuantiti" type="text" oninput="calculate()" />--%>
                        <asp:TextBox ID="txtKuantiti" runat="server" CssClass="form-control" Width="100px" Text="0" onkeypress="return CheckNumber(event)" onkeyup="calculate(this)" TextMode="Number" ></asp:TextBox>
                       <%--<asp:TextBox ID="txtJumLulus" runat="server" onkeypress="return CheckNumber(event)" onkeyup="AddValue(this)" Text='<%# Eval("JumLulus") %>'></asp:TextBox>--%>
                        <a href="#" title="Kuantiti" data-toggle="popover" data-trigger="hover" data-content="Masukkan nilai '0' jika tiada jumlah kuantiti"><i class="fa fa-info-circle fa-lg"></i></a>

                        <asp:RequiredFieldValidator ID="rfvKuantiti" runat="server" ControlToValidate="txtKuantiti" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Anggaran Harga Seunit</Label></td>
                    <td>
                        <%--<input id="txtAngHrgUnit" type="text" oninput="calculate()" />--%>
                        <asp:TextBox ID="txtAngHrgUnit" runat="server"  CssClass="form-control rightAlign" Width="100px" TextMode="Number" Text="0" onkeypress="return CheckNumber(event)" onkeyup="calculate(this)"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvAnggHrgUnit" runat="server" ControlToValidate="txtAngHrgUnit" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    <%-- <td></td>--%>
                    
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;"><Label class="control-label" for="">Jumlah Perbelanjaan</Label></td>
                    <td>
                        <%--<input id="txtAngJum" />--%>
                        <asp:TextBox ID="txtAngJum" runat="server" CssClass="form-control rightAlign" Width="100px" ReadOnly="true" Text="0"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="tfvAngJum" runat="server" ControlToValidate="txtAngJum" ErrorMessage="" ForeColor="#820303" Text="*Sila isi" ValidationGroup="btnSaveButiran" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                     <%--<td></td>--%>
                    </td>
                </tr>
                <tr>
                    
                    <td style="height:40px; text-align:center;" colspan="2" >
                    <asp:LinkButton ID="lbtnReset" runat="server" CssClass="btn btn-info" ToolTip="Kosongkan Butiran Perbelanjaan">
                        <i class="fa fa-refresh fa-lg"></i>&nbsp;&nbsp;&nbsp;Reset
                    </asp:LinkButton> 
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnTambah" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Tambah ke senarai">
                        <i class="fa fa-plus-circle fa-lg"></i>&nbsp;&nbsp;&nbsp;Tambah
                    </asp:LinkButton>
                    <asp:LinkButton ID="lbtnKemaskini" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Kemaskini" Visible="false">
                        <i class="fa fa-pencil fa-lg"></i>&nbsp;&nbsp;&nbsp;Kemaskini
                    </asp:LinkButton> 
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="lbtnUndo" runat="server" CssClass="btn btn-info" ValidationGroup="btnSaveButiran" ToolTip="Kemaskini" Visible="false">
                        <i class="fa fa-undo fa-lg"></i>&nbsp;&nbsp;&nbsp;Undo
                    </asp:LinkButton>                   
                    </td>
                </tr>
            </table>
            <br />

            <asp:GridView ID="gvChartOfAccount" runat="server" AutoGenerateColumns="false" 
                cssclass="table table-striped table-bordered table-hover" Width="100%" Font-Size="8pt" ShowFooter="True"
                AllowSorting="True" AllowPaging="True" PageSize="10">
                    <columns>
                    <asp:BoundField DataField="Bil" HeaderText="Bil" SortExpression="Bil" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="3%" HorizontalAlign="Center"/>
                    </asp:BoundField>                  
                     
                        
                    <asp:BoundField DataField="KodVot" HeaderText="Kod Vot" SortExpression="NoPermohonan" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="5%" HorizontalAlign="Center"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="Butiran" HeaderText="Butiran" SortExpression="Butiran" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="30%" HorizontalAlign="Left"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="Kuantiti" HeaderText="Kuantiti" SortExpression="Kuantiti" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="5%" HorizontalAlign="Right"/>
                    </asp:BoundField>
                        

                    <asp:BoundField DataField="AnggaranSeunit" HeaderText="Anggaran Harga Seunit (RM)" SortExpression="AnggaranSeunit" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="10%" HorizontalAlign="Right"/>
                    </asp:BoundField>

                    <asp:BoundField DataField="AnggaranJumlah" HeaderText="Anggaran Perbelanjaan (RM)" SortExpression="AnggaranJumlah" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="10%" HorizontalAlign="Right"/>
                    </asp:BoundField>

                     <asp:TemplateField Visible=false>
                         <ItemTemplate>
                             <asp:Label id="lblNoButiran" runat ="server" text='<%# Eval("NoButiran")%>' ></asp:Label>
                         </ItemTemplate>
                      </asp:TemplateField>   

                   <%-- <asp:BoundField DataField="NoButiran" HeaderText="NoButiran" SortExpression="AnggaranJumlah" ReadOnly="True" HeaderStyle-CssClass="centerAlign">
                    <ItemStyle Width="1%" HorizontalAlign="Right"/>
                    </asp:BoundField>--%>
                                                                                                                  
                    <asp:TemplateField>
                          <ItemTemplate>
                            <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" CssClass="btn-xs" ToolTip="Pilih Kemaskini">
                                <i class="fa fa-hand-o-left fa-lg"></i>
                            </asp:LinkButton>
                            
                            <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn-xs" ToolTip="Padam"
                                OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
                                <i class="fa fa-trash-o fa-lg"></i>
                            </asp:LinkButton>
                          
			            </ItemTemplate>
			            <ItemStyle Width="5%" HorizontalAlign="Center" />
			        </asp:TemplateField>
                </columns>
                    <HeaderStyle BackColor="#996633" />
            </asp:GridView>

            

           <%-- <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="true" PageSize="5" emptydatatext="no data"
                CssClass= "table table-striped table-bordered table-hover" Width="100%"  Font-Size="8pt" BorderColor="#333333" BorderStyle="Solid" ShowFooter="True" >
                  <columns>

                    <asp:BoundField DataField="Bil" HeaderText="Bil" SortExpression="Bil" ReadOnly="True">
                    <ItemStyle Width="3%" />
                    </asp:BoundField>

                    <asp:BoundField DataField="Butiran" HeaderText="Butiran" SortExpression="Butiran" ReadOnly="True">
                    <ItemStyle Width="30%" />
                    </asp:BoundField>

                    <asp:TemplateField HeaderText = "KW">
                    <ItemTemplate>
                        <asp:Label ID="lblKW" runat="server" Text='<%# Eval("KW") %>' Visible = "false" />
                        <asp:DropDownList ID="ddlKW" runat="server">
                        </asp:DropDownList>
                    </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText = "Kod Operasi">
                    <ItemTemplate>
                        <asp:Label ID="lblKO" runat="server" Text='<%# Eval("KO") %>' Visible = "false" />
                        <asp:DropDownList ID="ddlKO" runat="server">
                        </asp:DropDownList>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText = "Objek AM">
                    <ItemTemplate>
                        <asp:Label ID="lblObjekAM" runat="server" Text='<%# Eval("Objek AM") %>' Visible = "false" />
                        <asp:DropDownList ID="ddlObjekAM" runat="server" OnSelectedIndexChanged="ddlObjekAM_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText = "Objek Sebagai">
                    <ItemTemplate>
                        <asp:Label ID="lblObjekSebagai" runat="server" Text='<%# Eval("ObjekSebagai") %>' Visible = "false" />
                        <asp:DropDownList ID="ddlObjekSebagai" runat="server" OnSelectedIndexChanged="ddlObjekSebagai_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText = "Objek Lanjut">
                    <ItemTemplate>
                        <asp:Label ID="lblObjekLanjut" runat="server" Text='<%# Eval("ObjekLanjut") %>' Visible = "false" />
                        <asp:DropDownList ID="ddlObjekLanjut" runat="server">
                        </asp:DropDownList>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Anggaran" HeaderText="Anggaran Perbelanjaan (RM)" SortExpression="Anggaran" ReadOnly="True">
                    <ItemStyle Width="10%" />
                    </asp:BoundField>

                    <asp:TemplateField>
                    <ItemTemplate>
                         <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" Height="20px" ImageUrl="~/Images/Edit_48.png" 
                            ToolTip="Kemas Kini" Width="15px" OnItemCommand="gvChartOfAccount_ItemCommand" />
                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete"  ImageUrl="~/Images/Delete_32x32.png" 
                            ToolTip="Padam" Width="15px" Height="18px"  OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />
                    </ItemTemplate>
              
                    <ItemStyle Width="3%" HorizontalAlign="Center" />
                    </asp:TemplateField>
                   
                   
                  </columns>
           
                  <HeaderStyle BackColor="#6699FF" />

                  <RowStyle Height="5px" />

                  <SelectedRowStyle  ForeColor="Blue" />

            </asp:GridView>--%>
            
            </div>
            </div>

            
            </ContentTemplate>
            </asp:UpdatePanel>
                <br />
                <div style="text-align:center">
                    <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-info" ValidationGroup="btnSimpan">
                        <i class="fa fa-floppy-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                    </asp:LinkButton>
                    &nbsp;
                      &nbsp;
                    <asp:LinkButton ID="lbtnHapus" runat="server" CssClass="btn btn-info" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');">
                        <i class="fa fa-trash-o fa-lg"></i>&nbsp;&nbsp;&nbsp;Hapus
                    </asp:LinkButton>
                    <%--<asp:Button ID="btnKemaskini" text="Kemaskini" runat="server" CssClass="btn" />&nbsp;
                    <asp:Button ID="btnHapus" text="Hapus" runat="server" CssClass="btn" OnClientClick="return confirm('Anda pasti untuk padam rekod ini?');" />--%>
                </div>
            </div>
            </div>
            </div>
            </div>
        </div>
    </div>
    </div>
     </ContentTemplate>
    </asp:UpdatePanel>  

</asp:Content>
