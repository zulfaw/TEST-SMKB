<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Potongan.aspx.vb" Inherits="SMKB_Web_Portal.Potongan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script>

        function fConfirm()
        {
            var status = true;


                //Kod elaun
                if (document.getElementById('<%=txtKodPot.ClientID%>').value === "") 
                {
                    //blnComplete = false
                    alert("Sila masukkan kod potongan!")
                    status = false;
                    return false;
                    
                }
                if (document.getElementById('<%=txtNmPot.ClientID%>').value === "") {
                    //blnComplete = false
                    alert("Sila masukkan butiran!")
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

        function fConfirmUpdt() {
            var status = true;


            //Kod elaun
            if (document.getElementById('<%=txtKodE.ClientID%>').value === "") {
                        //blnComplete = false
                        alert("Sila masukkan kod potongan!")
                        status = false;
                        return false;

                    }
                    if (document.getElementById('<%=txtNamaE.ClientID%>').value === "") {
                    //blnComplete = false
                    alert("Sila masukkan butiran!")
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
                        return true;
                        

                    } else

                        return false;

                }
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="PermohonanTab" class="tabcontent" style="display: block">

                <div class="table-title">
                    <h6>Senarai Potongan</h6>

                    <div class="btn btn-primary" data-toggle="modal" data-target="#tambahpot">
                        <i class="fa fa-plus"></i>Tambah
                            Potongan
                    </div>
                </div>

                <div class="filter-table-function">
                    <div class="show-record">
                        <%--<p>Tunjukkan</p>--%>
    <%--                    <select class="form-control">
                            <option>5</option>
                            <option>10</option>
                            <option>20</option>
                            <option>50</option>
                        </select>
                        <p>Rekod</p>--%>
                    </div>
                    <div class="search-form">
                        <i class="las la-search"></i>
                        <asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true" CssClass="form-control" Style="width: 100%;" placeholder="Cari"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="transaction-table table-responsive">

                            <asp:ListView ID="lvPotongan" runat="server" GroupPlaceholderID="groupPlaceHolder1" ItemPlaceholderID="itemPlaceHolder1" OnItemDataBound="lvPotongan_ItemDataBound" OnPagePropertiesChanging="lvPotongan_PagePropertiesChanging" OnItemDeleting="lvPotongan_ItemDeleting">
                                <LayoutTemplate>
                                    <table cellspacing="0" cellpadding="3" rules="all" border="0" class="table table-bordered">
                                        <tr>
                                            <th>Kod</th>
                                            <th>Butiran</th>
                                            <th>Vot Tetap</th>
                                            <th>Vot Bukan Tetap</th>
                                            <th>Kira KWSP</th>
                                            <th>Kira Perkeso</th>
                                            <th>Kira Cukai</th>
                                            <th>Kira Pencen</th>
                                            <th>Tindakan</th>

                                        </tr>
                                        <tbody>
                                            <asp:PlaceHolder runat="server" ID="groupPlaceHolder1"></asp:PlaceHolder>
                                            <tr>
                                                <td colspan="9">
                                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="lvPotongan" PageSize="10">
                                                        <Fields>
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


                <!-- Modal -->
                <div class="modal fade" id="tambahpot" tabindex="-1" role="dialog"
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
                                
                                    <h6>Maklumat Potongan</h6>
                                    <hr>
                                    <div class="row">

                                        <div class="col-md-6">
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <label>Kod Potongan</label>
                                                    <asp:TextBox runat="server" ID="txtKodPot" CssClass="form-control" Style="width: 100%;" placeholder="Kod Potongan"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div class="form-group">
                                                <label>Butiran</label>
                                                <asp:TextBox Rows="3" runat="server" ID="txtNmPot" CssClass="form-control" Style="width: 100%;" placeholder="Butiran"></asp:TextBox>
                                            </div>
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <label>Jenis Potongan</label>
                                                    <asp:DropDownList ID="ddlJnsPot" CssClass="form-control searchable-dropdown" runat="server">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>


                                                    </select>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label>Kod Agensi</label>
                                                    <asp:DropDownList ID="ddlAgensi" CssClass="form-control searchable-dropdown" runat="server">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>


                                                    </select>
                                                </div>
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
              
                                            <div class="form-row">
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
                                            </div>
                                             <div class="form-row">
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
                                            <div class="form-group col-md-6">
                                                <label>Kira AP</label>
                                                <div class="radio-btn-form">
                                                    <asp:RadioButtonList ID="rbAP" runat="server" RepeatDirection="Horizontal">
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
                                <button type="button" class="btn btn-danger" data-dismiss="modal" autopostback ="true" onclick="$('#tambahpot').modal('hide'); return false;">Tutup</button>
                                <asp:LinkButton ID="lbtnSimpan" runat="server" autopostback ="true" OnClick="lbtnSimpan_Click" CssClass="btn btn-secondary" OnClientClick="return fConfirm();"> 
                                            &nbsp;&nbsp;&nbsp;Simpan </asp:LinkButton>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Modal -->

            <!-- Modal kemaskini -->
                    <div class="modal fade" id="updatepot" tabindex="-1" role="dialog"
                        aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-xl" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="Mtitle">Kemaskini Potongan
                                    </h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>

                                <div class="modal-body">
                                   
                                    <h6>Maklumat Potongan</h6>
                                    <hr>
                                    <div class="row">

                                        <div class="col-md-6">
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <label>Kod Potongan</label>
                                                    <asp:TextBox runat="server" ID="txtKodE" CssClass="form-control" Style="width: 100%;" placeholder="Kod Potongan" ReadOnly="true"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div class="form-group">
                                                <label>Butiran</label>
                                                <asp:TextBox Rows="3" runat="server" ID="txtNamaE" CssClass="form-control" Style="width: 100%;" placeholder="Butiran"></asp:TextBox>
                                            </div>
                                                  <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <label>Jenis Potongan</label>
                                                    <asp:DropDownList ID="ddlJnsPotE" CssClass="form-control searchable-dropdown" runat="server">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>


                                                    </select>
                                                </div>
                                                <div class="form-group col-md-6">
                                                    <label>Kod Agensi</label>
                                                    <asp:DropDownList ID="ddlAgensiE" CssClass="form-control searchable-dropdown" runat="server">
                                                        <asp:ListItem></asp:ListItem>
                                                    </asp:DropDownList>


                                                    </select>
                                                </div>
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
                                            <div class="form-row">
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
                                            </div>
                                            <div class="form-row">
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
                                            <div class="form-row">
                                                <div class="form-group col-md-6">
                                                    <label>Kira AP</label>
                                                    <div class="radio-btn-form">
                                                        <asp:RadioButtonList ID="rbAPU" runat="server">
                                                            <asp:ListItem Value="1" Text="Ya"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Tidak"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <hr>
                               

                                </div>

                                <div class="modal-footer">
                                    <button type="button" class="btn btn-danger" data-dismiss="modal" autopostback ="true" onclick="$('#updatepot').modal('hide'); return false;">Tutup</button>
                                    
                                    <asp:LinkButton ID="lbtnUpdate" OnClick="lbtnUpdate_Click" runat="server" autopostback ="true" CssClass="btn btn-secondary" OnClientClick="return fConfirmUpdt();"> 
                                            &nbsp;&nbsp;&nbsp;Simpan </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            <!--end  Modal -->

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
