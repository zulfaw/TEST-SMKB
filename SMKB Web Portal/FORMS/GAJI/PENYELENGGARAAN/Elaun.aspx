<%@ Page Title="" Language="vb" AutoEventWireup="True" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Elaun.aspx.vb" Inherits="SMKB_Web_Portal.Elaun" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script>



        function fSimpanElaun()
        {
            var status = true;
                //Kod elaun
                if (document.getElementById('<%=txtKodElaun.ClientID%>').value === "") 
                {
                    //blnComplete = false
                    alert("Sila masukkan kod elaun!")
                    status = false;
                    return false;
                    
                }
                if (document.getElementById('<%=txtNamaElaun.ClientID%>').value === "") {
                    //blnComplete = false
                    alert("Sila masukkan nama elaun!")
                    status = false;
                    return false;

                }
                if (document.getElementById('<%=ddlVotTetap.ClientID%>').value === "") {
                    //blnComplete = false
                    alert("Sila masukkan vot tetap!")
                    status = false;
                    return false;

                }
                if (document.getElementById('<%=ddlVotBknTetap.ClientID%>').value === "") {
                    //blnComplete = false
                    alert("Sila masukkan vot bukan tetap!")
                    status = false;
                    return false;

                }


            if (confirm('Anda pasti untuk simpan rekod ini?')) {
                return true;
               

            } else

                    return false;

        }

        function fUpdateElaun() {
            var status = true;

            //Kod elaun
            if (document.getElementById('<%=txtKodE.ClientID%>').value === "") {
                        //blnComplete = false
                        alert("Sila masukkan kod elaun!")
                        status = false;
                        return false;

                    }
                    if (document.getElementById('<%=txtNmElaunU.ClientID%>').value === "") {
                    //blnComplete = false
                    alert("Sila masukkan nama elaun!")
                    status = false;
                    return false;

                }
                if (document.getElementById('<%=ddlVotE.ClientID%>').value === "") {
                    //blnComplete = false
                    alert("Sila masukkan vot tetap!")
                    status = false;
                    return false;

                }
                    if (document.getElementById('<%=ddlVotxE.ClientID%>').value === "") {
                        //blnComplete = false
                        alert("Sila masukkan vot bukan tetap!")
                        status = false;
                        return false;

                    }


            if (confirm('Anda pasti untuk simpan rekod ini?')) {
                
                //$('#updateelaun').modal('hide');
                        return true;
                        
                        
                    } else

                        return false;

                }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="PermohonanTab" class="tabcontent" style="display: block">

                <div class="table-title">
                    <h6>Senarai Elaun</h6>

                    <div class="btn btn-primary" data-toggle="modal" data-target="#tambahelaun">
                        <i class="fa fa-plus"></i>Tambah
                            Elaun
                    </div>
                </div>
                
                <div class="filter-table-function">
                    <div class="search-form">
                        <i class="las la-search"></i>

                        <asp:TextBox runat="server" ID="TextBox1" AutoPostBack="true"   OnTextChanged="TextBox1_TextChanged" CssClass="form-control" Style="width: 100%;" placeholder="Cari"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="transaction-table table-responsive">
                            <asp:ListView ID="lvElaun" runat="server" GroupPlaceholderID="groupPlaceHolder1" OnItemEditing="lvElaun_ItemEditing" OnItemDataBound="lvElaun_ItemDataBound" OnItemCanceling="lvElaun_ItemCanceling" OnItemDeleting="lvElaun_ItemDeleting" OnItemUpdating="lvElaun_ItemUpdating" ItemPlaceholderID="itemPlaceHolder1" OnPagePropertiesChanging="lvElaun_PagePropertiesChanging">
                                <LayoutTemplate>
                                    <table cellspacing="0" cellpadding="3" rules="all" border="0" class="table table-bordered">
                                        <tr>
                                            <th scope="col">Kod</th>
                                            <th scope="col">Butiran</th>
                                            <th scope="col">Vot Tetap</th>
                                            <th scope="col">Vot Bukan Tetap</th>
                                            <th scope="col">Kira KWSP</th>
                                            <th scope="col">Kira Perkeso</th>
                                            <th scope="col">Kira Cukai</th>
                                            <th scope="col">Kira Pencen</th>
                                            <th scope="col">Tindakan</th>

                                        </tr>
                                        <tbody >
                                            
                                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                            
                                            <tr >

                                                <td colspan="9"  >
                                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvElaun" PageSize="10" >
                                                        <Fields  >
                                                            <asp:NextPreviousPagerField ButtonType="Button" PreviousPageText="Sebelumnya" ShowFirstPageButton="false" ShowPreviousPageButton="true"
                                                                ShowNextPageButton="false" />
                                                            <asp:NumericPagerField ButtonType="Button" />
                                                            <asp:NextPreviousPagerField ButtonType="Button" NextPageText="Seterusnya" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                                                        </Fields>
                                                    </asp:DataPager>
                                                </td>
                                                
                                            </tr>
                                                
                                            
                                        </tbody>
                                    </table>
                                </LayoutTemplate>
                                <GroupTemplate>
                                    <tr>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceHolder1"></asp:PlaceHolder>
                                    </tr>
                                </GroupTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("Kod")%></td>
                                        <asp:HiddenField ID="hfKod" runat="server" Value='<%#Eval("Kod") %>' />
                                        <td><%# Eval("Butiran")%></td>
                                        <td><%# Eval("Vot_Tetap")%></td>
                                        <td><%# Eval("Vot_Bukan_Tetap")%></td>
                                        <td><%# Eval("Kira_KWSP")%></td>
                                        <td><%# Eval("Kira_Perkeso")%></td>
                                        <td><%# Eval("Kira_Cukai")%></td>
                                        <td><%# Eval("Kira_Pencen")%></td>
                                        <td class="tindakan">
                                             <asp:LinkButton ID="LinkButton1" runat="server" data-id='<%#Eval("Kod") %>' OnClick="LinkButton1_Click">
                                            <i class="fa fa-edit edit"></i></asp:LinkButton>
 
                                            <asp:LinkButton ID="btnHapus" runat="server" ToolTip="Hapus" CommandName="Delete" OnClientClick="return confirm('Adakah anda pasti untuk padam rekod ini?');" >
                                               <i class="fa fa-trash-o delete"></i>
                                            </asp:LinkButton>

                                        </td>
                                    </tr>
                                </ItemTemplate>
<%--                                <EditItemTemplate>
                                    <td>
                                        <asp:Label ID="lblKod" runat="server" Text='<%# Eval("Kod") %>'></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNmE" runat="server" Text='<%# Eval("Butiran") %>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVotE" runat="server">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblVotE" runat="server" Text='<%# Eval("Vot_Tetap") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlVotXE" runat="server">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblVotXE" runat="server" Text='<%# Eval("Vot_Bukan_Tetap") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rbKWSPE" runat="server" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                        <asp:Label ID="lblrbkwsp" runat="server" Text='<%# Eval("Kira_KWSP") %>' Visible="false"></asp:Label>
                                    </td>
                                   <td>
                                        <asp:RadioButtonList ID="rbPerkesoE" runat="server" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                        <asp:Label ID="lblrbperkeso" runat="server" Text='<%# Eval("Kira_Perkeso") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rbCukaiE" runat="server" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                        <asp:Label ID="lblrbcukai" runat="server" Text='<%# Eval("Kira_Cukai") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="rbPencenE" runat="server" RepeatDirection="Horizontal">
                                        </asp:RadioButtonList>
                                        <asp:Label ID="lblrbPencen" runat="server" Text='<%# Eval("Kira_Pencen") %>' Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnEdit" runat="server" Text='Update' CommandName="Update" />
                                        <asp:Button ID="Button1" runat="server" Text='Cancel' CommandName="Cancel" />
                                    </td>
                                </EditItemTemplate>--%>
                                <AlternatingItemTemplate>
                                    <tr>
                                        <td><%# Eval("Kod")%>
                                            <asp:HiddenField ID="hfKod" runat="server" Value='<%#Eval("Kod") %>' />
                                        </td>
                                        <td><%# Eval("Butiran")%></td>
                                        <td><%# Eval("Vot_Tetap")%></td>
                                        <td><%# Eval("Vot_Bukan_Tetap")%></td>
                                        <td><%# Eval("Kira_KWSP")%></td>
                                        <td><%# Eval("Kira_Perkeso")%></td>
                                        <td><%# Eval("Kira_Cukai")%></td>
                                        <td><%# Eval("Kira_Pencen")%></td>
                                        <td class="tindakan">

                                            <asp:LinkButton ID="LinkButton1" runat="server" data-id='<%#Eval("Kod") %>' OnClick="LinkButton1_Click">
                                            <i class="fa fa-edit edit"></i></asp:LinkButton>
                                            <asp:LinkButton ID="btnHapus" runat="server" ToolTip="Pilih" CommandName="Delete" OnClientClick="return confirm('Adakah anda pasti untuk padam rekod ini?');">
                                               <i class="fa fa-trash-o delete"></i>
                                            </asp:LinkButton>
                                                                              
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>

                </div>

<%--                <div class="send-button">
                    <div class="btn btn-secondary">
                        <i class="fa fa-send"></i>Hantar
                    </div>
                </div>--%>


                <!-- Modal -->
                <div class="modal fade" id="tambahelaun" tabindex="-1" role="dialog"
                    aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalCenterTitle">Tambah Elaun</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>

                            <div class="modal-body">
                                
                                    <h6>Maklumat Elaun</h6>
                                    <hr>
                                    <div class="row">

                                        <div class="col-md-6">
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <label>Kod Elaun</label>
                                                    <asp:TextBox runat="server" ID="txtKodElaun" CssClass="form-control" Style="width: 100%;" placeholder="Kod Elaun"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div class="form-group">
                                                <label>Nama Elaun</label>
                                                <asp:TextBox Rows="3" runat="server" ID="txtNamaElaun" CssClass="form-control" Style="width: 100%;" placeholder="Nama Elaun"></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <label>Vot Tetap</label>
                                                    <asp:DropDownList ID="ddlVotTetap" CssClass="form-control searchable-dropdown" runat="server">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>

                                                       
                                                    </select>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label>Vot Bukan Tetap</label>
                                                    <asp:DropDownList ID="ddlVotBknTetap" CssClass="form-control searchable-dropdown" runat="server">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>


                                                    </select>
                                                </div>
                                            </div>

                                            <div class="form-group col-md-6">
                                                <label>Kira KWSP</label>
                                                <div class="radio-btn-form">
                                                    <asp:RadioButtonList ID="rbKWSP" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Ya">  </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                            </div>
                                            <div class="form-group col-md-6">
                                                <label>Kira Perkeso</label>
                                                <div class="radio-btn-form">
                                                    <asp:RadioButtonList ID="rbPerkeso" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Ya">  </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <label>Kira Cukai</label>
                                                <div class="radio-btn-form">
                                                    <asp:RadioButtonList ID="rbCukai" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Ya">  </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="form-group col-md-6">
                                                <label>Kira Pencen</label>
                                                <div class="radio-btn-form">
                                                    <asp:RadioButtonList ID="rbPencen" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Ya">  </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                    <hr>
                               
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger" data-dismiss="modal" onclick="$('#tambahelaun').modal('hide'); return false;">Tutup</button>
                                <asp:LinkButton ID="lbtnSimpan" runat="server" autopostback ="true" CssClass="btn btn-secondary" OnClick="lbtnSimpan_Click" OnClientClick="return fSimpanElaun();"> 
                                            &nbsp;&nbsp;&nbsp;Simpan </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Modal -->

            <!-- Modal kemaskini -->
                    <div class="modal fade" id="updateelaun" tabindex="-1" role="dialog"
                        aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="Mtitle">Kemaskini elaun
                                    </h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>

                                <div class="modal-body">
                                   <%--<form >--%>
                                    <h6>Maklumat Elaun</h6>
                                    <hr>
                                    <div class="row">

                                        <div class="col-md-6">
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <label>Kod Elaun</label>
                                                    <asp:TextBox runat="server" ID="txtKodE" CssClass="form-control" Style="width: 100%;" placeholder="Kod Elaun" ReadOnly="true"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div class="form-group">
                                                <label>Nama Elaun</label>
                                                <asp:TextBox Rows="3" runat="server" ID="txtNmElaunU" CssClass="form-control" Style="width: 100%;" placeholder="Nama Elaun"></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <label>Vot Tetap</label>
                                                    <asp:DropDownList ID="ddlVotE" CssClass="form-control searchable-dropdown" runat="server">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>

                                                       
                                                    </select>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label>Vot Bukan Tetap</label>
                                                    <asp:DropDownList ID="ddlVotxE" CssClass="form-control searchable-dropdown" runat="server">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>


                                                    </select>
                                                </div>
                                            </div>

                                            <div class="form-group col-md-6">
                                                <label>Kira KWSP</label>
                                                <div class="radio-btn-form">
                                                    <asp:RadioButtonList ID="rbKwspE" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Ya">  </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>

                                            </div>
                                            <div class="form-group col-md-6">
                                                <label>Kira Perkeso</label>
                                                <div class="radio-btn-form">
                                                    <asp:RadioButtonList ID="rbPerkesoE" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Ya">  </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="form-group col-md-6">
                                                <label>Kira Cukai</label>
                                                <div class="radio-btn-form">
                                                    <asp:RadioButtonList ID="rbCukaiE" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Ya">  </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                            <div class="form-group col-md-6">
                                                <label>Kira Pencen</label>
                                                <div class="radio-btn-form">
                                                    <asp:RadioButtonList ID="rbPencenE" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1" Text="Ya">  </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                    <hr>
                              <%--  </form>--%>

                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" onclick="$('#updateelaun').modal('hide'); return false;">Tutup</button>
                                    
                                    <asp:LinkButton ID="lbtnUpdate" CausesValidation="false" OnClick="lbtnUpdate_Click" runat="server" autopostback ="true" CssClass="btn btn-secondary" OnClientClick="return fUpdateElaun();"> 
                                            &nbsp;&nbsp;&nbsp;Simpan </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            <!--end  Modal -->

             <!-- Modal Message Box -->
                    <div class="modal fade" id="MessageModal" tabindex="-1" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">Tolong Sahkan?</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    Adakah anda pasti mahu menambah modul?
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Tidak</button>
                                    <button type="button" class="btn btn-secondary" data-toggle="modal"
                                        data-target="#ModulForm" data-dismiss="modal">Ya</button>
                                </div>
                            </div>
                        </div>
                    </div>
            <!--end  Modal -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
