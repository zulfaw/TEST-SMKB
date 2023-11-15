<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Laporan_ABM.aspx.vb" Inherits="SMKB_Web_Portal.Laporan_ABM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%= Convert.ToString(Request.QueryString("Title")) %></h1>
     <style>
         .text-right {
    text-align:right;
    margin-top:5px;
    padding:5px;
  }

         .text-left {
    text-align:left;
     margin-top:5px;
     padding:5px;
  }

         @media (max-width: 850px) {
  .text-right {
    text-align:center;
     margin-top:5px;
     padding:5px;
  }

         .text-left {
    text-align:center;
     margin-top:5px;
     padding:5px;
  }
}



     </style>
    
    
    
    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
    <div class="col-sm-12">
        <div class="panel panel-default" style="width:40%">
            <div class="panel-heading">Jana Laporan ABM</div>
            <div class="panel-body">     
               <div class="panel-body"> 
                   <table style="width: 100%">
            <tr>
                <td style="height: 19px;">
                   <label class="control-label">Tahun: 
                    </label>
                </td>
                <td style="height: 19px;">
                   <asp:TextBox runat="server" ReadOnly="True" BackColor="#FFFFCC" Width="100px" ID="txtTahun" class="form-control"></asp:TextBox>
                </td>
            </tr>

                        <tr>
                <td style="height: 19px;">
                   <label class="control-label">Maksud: 
                    </label>
                </td>
                <td style="height: 19px;">
                   <asp:TextBox runat="server" ReadOnly="True" BackColor="#FFFFCC" Width="300px" ID="txtMaksud" class="form-control" Text ="B49 - SEKTOR PENGAJIAN TINGGI"></asp:TextBox>

                </td>
            </tr>

                        <tr>
                <td style="height: 19px;">
                   <label class="control-label" > Program: 
                    </label>
                </td>
                <td style="height: 19px;">
                   <asp:TextBox runat="server" ReadOnly="True" BackColor="#FFFFCC" Width="400px" ID="txtProgram" class="form-control" Text ="UNIVERSITI TEKNIKAL MALAYSIA MELAKA"></asp:TextBox>

                </td>
            </tr>

                        <tr>
                <td style="height: 19px;">
                   <label class="control-label"> Aktiviti: 
                    </label>
                </td>
                <td style="height: 19px;">
                   <asp:TextBox runat="server" ReadOnly="True" BackColor="#FFFFCC" Width="150px" ID="txtAktiviti" class="form-control" Text ="KESELURUHAN"></asp:TextBox>

                </td>
            </tr>
                       <tr>
                           <td style="height: 55px;">&nbsp;</td>
                           <td style="height: 19px;">
                               <asp:Button ID="btnJana" runat="server" CssClass="btn" Text="Jana" />
                           </td>
                       </tr>
        </table>



                   </div>           
            </div>
        </div></div>
</ContentTemplate></asp:UpdatePanel>
</asp:Content>
