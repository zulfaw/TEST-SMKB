<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Tetapan_Peringkat_Agihan.aspx.vb" Inherits="SMKB_Web_Portal.Penentuan_Peringkat_PTJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">

    <style >

        .col-sm-12 {
    width :80%;
  }
        @media (max-width: 1500px) {
            .col-sm-12 {
                width: 80%;
            }
        }

        @media (max-width: 1150px) {
  .col-sm-12 {
    width :100%;
  }
}

    </style>


    <script type="text/javascript">
         
        function pageLoad() {
            $('[data-toggle="tooltip"]').tooltip()

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">        
    <ContentTemplate>
  
        <div class="row">  
            <div class="col-sm-12" >               
        <div class="panel panel-default" style="margin-top:10px;">
        <div class="panel-heading">
            <%--<h3 class="panel-title">Tetapan Peringkat Agihan PTj
                <i class="fa fa-info-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="top" title="Jumlah 'Anggaran Disyorkan' akan ditentukan setelah permohonan dihantar ke pejabat Bendahari.">&nbsp;</i>           
                </h3>--%>
            <div class="panel-title pull-left">
             Tetapan Peringkat Agihan PTj
         </div>
        <div class="panel-title pull-right"><i class="fa fa-question-circle fa-lg" aria-hidden="true" data-toggle="tooltip" data-placement="right" title="Peruntukan yang telah diluluskan akan diagih hingga ke peringkat yang ditetapkan."></i></div>
        <div class="clearfix"></div>
            
        </div>
        <div class="panel-body">
            <table style="width:100%;margin-bottom: 0;" class="table table table-borderless">
                <tr>
                    <td style="width: 80px;">
                        <label class="control-label" for="">
                        PTJ :
                        </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPTJ" runat="server" CssClass="form-control" ReadOnly="true" Width="95%"></asp:TextBox>
                        <asp:TextBox ID="hidTxtKodpTj" runat="server" CssClass="form-control" ReadOnly="true" Visible="false" Width="40px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="height: 27px;"><label class="control-label" for="">Peringkat : </label></td>
                    <td style="height: 27px">
                        <asp:RadioButtonList ID="rbPeringkat" runat="server" Height="25px" Width="273px" RepeatDirection="Horizontal">
                            <asp:ListItem Text="  PTj" Value="1" Selected="True" />
                            <asp:ListItem Text="  Bahagian" Value="2" />
                            <asp:ListItem Text="  Unit" Value="3" />
                          </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rbPeringkat" ErrorMessage="" ForeColor="#820303" Text="*Sila Pilih" ValidationGroup="btnSimpan" Display="Dynamic" CssClass="fontValidatation" ></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td><Label class="control-label" for="">Tahun Bajet : </Label></td>
                    <td>
                        <asp:TextBox ID="txtTahunBajet" runat="server" CssClass="form-control" Width="60px" ReadOnly="true"></asp:TextBox>
                       
                    </td>
                </tr>
                <tr style="text-align:center;height:60px;">
                    <td colspan="2">
                        <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn " ValidationGroup="btnSimpan">
                    <i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                </asp:LinkButton>
                    </td>
                </tr>
            </table>            
        </div>
        </div>
        </div></div>

    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
