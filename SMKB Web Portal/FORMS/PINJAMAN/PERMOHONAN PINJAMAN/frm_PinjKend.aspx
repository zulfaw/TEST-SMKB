<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frm_PinjKend.aspx.vb" Inherits="SMKB_Web_Portal.frm_PinjKend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Permohonan Skim Pinjaman Kenderaan Staf UTeM</h1>
    <link rel="stylesheet" type="text/css" href="../../../Content/Site.css">
    <script type="text/javascript">
        function upper(ustr) {
            var str = ustr.value;
            ustr.value = str.toUpperCase();
        };

        function trim(ustr) {
            var str = ustr.value;
            ustr.value = str.trim();
        };

        var kodvalue= 1;
        var kodLulus= 1;
        $(function () {

            if (kodvalue == 1) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 2) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 3) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 4) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
            }
            if (kodvalue == 5) {
                $('#btnStep1').addClass('btn-success').removeClass('btn-default');
                $('#btnStep2').addClass('btn-success').removeClass('btn-default');
                $('#btnStep3').addClass('btn-success').removeClass('btn-default');
                $('#btnStep4').addClass('btn-success').removeClass('btn-default');
                
            }
        });

        function isInputNumber(evt)
        {
            var char = String.fromCharCode(evt.which);

            if(!(/[0-9]/.test(char))){
                evt.preventDefault();
            }
        }

    </script>
    <script type="text/javascript" src="../../../Scripts/calendar.js"></script>
    <style type="text/css">
        .highlight{
            background: rgba(192,192,192,0.2);
            padding:10px;
        }
    </style>
    <div class="stepwizard">
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep1" type="button" class="btn-default btn-circle">1</button>
                <p>Maklumat Pemohon</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep2" type="button" class="btn-default btn-circle" runat="server">2</button>
                <p>Maklumat Kenderaan</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep3" type="button" class="btn-default btn-circle" runat="server" >3</button>
                <p>Maklumat Syarikat</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep4" type="button" class="btn-default btn-circle" runat="server" >4</button>
                <p>Penjamin</p>
            </div>
            <div class="stepwizard-step" style="width:20%">
                <button id="btnStep5" type="button" class="btn-default btn-circle" runat="server">5</button>
                <p>Senarai Semak</p>
            </div>
    </div>
    <div class="alert alert-warning" role="alert" runat="server" id="lblWarning" visible="false">
        <strong><i class="fas fa-exclamation-circle fa-lg"></i></strong>&nbsp;&nbsp;<asp:Label ID="lblErr" runat="server" Text="Label" />
    </div>
    <div id="trUlasan"  runat="server" class="alert alert-danger" visible="false">
        <strong><i class="fas fa-exclamation-circle fa-lg"></i></strong>&nbsp;&nbsp;Ulasan Kelulusan :&nbsp;&nbsp;<asp:Label runat="server" ID="ltrUlasan"/>
    </div>
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
        <ContentTemplate>
            <table style="width:100%">
                <tr>
                    <td>   
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h4 class="panel-title">Maklumat Pemohon
                                </h4>
                            </div>
                            <div class="panel-body">
<table cellpadding=2 cellspacing=0>
  	<tr>
	<td>	</tr>
    <tr>
      <td>Nama Penuh </td>
      <td>:&nbsp;<strong><asp:Label ID="lblNamaPemohon" runat="server"></asp:Label></strong></td>
      <td>No Pekerja </td>
      <td>: <strong><asp:Label ID="lblNoPmhn" runat="server"></asp:Label></strong></td>
    </tr>
    <tr>
      <td>No. Kad Pengenalan </td>
      <td>:&nbsp;<strong><asp:Label ID="lblNoKP" runat="server"></asp:Label></strong></td>
		<td>No Pinjaman Sementara </td>
	  <td>:&nbsp;<strong><asp:Label ID="lblNoSem" runat="server"></asp:Label></strong></td>
    </tr>
    <tr>
      <td>Tarikh Lahir </td>
      <td>:&nbsp;<strong><asp:Label ID="lblTkhLahir" runat="server"></asp:Label></strong></td>
      <td>Umur </td>
      <td>:&nbsp;<strong><asp:Label ID="lblUmur" runat="server"></asp:Label></strong></td>
    </tr>
    <tr>
      <td>Taraf Pegawai <br></td>
      <td>:&nbsp;<strong><strong><asp:Label ID="lbltarafPerkhidmatan" runat="server"></asp:Label></strong></td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td colspan="2"><em>(samada dalam percubaan/tetap/kontrak)</em>&nbsp;</td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td>Jawatan lain yang disandang </td>
      <td>:&nbsp;<strong><asp:Label ID="lbljawGiliranNama" runat="server"></asp:Label></strong></td>
      <td>mulai <strong><asp:Label ID="tkhMula" runat="server"></asp:Label></strong></td>
      <td>hingga <strong><asp:Label ID="lbltkhTamat" runat="server"></asp:Label></strong></td>
    </tr>
    <tr>
      <td colspan="2"><em> (*Dekan/Timbalan Dekan/Ketua Jabatan dll) </em></td>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td>Tarikh Perlantikan </td>
      <td>:&nbsp;<strong><asp:Label ID="lblTkhLantik" runat="server"></asp:Label></strong></td>
      <td>Tarikh Pengesahan Perkhidmatan </td>
      <td>:&nbsp;<strong><asp:Label ID="lblTkhSah" runat="server"></asp:Label></strong></td>
    </tr>
    <tr>
      <td>Jawatan Sekarang </td>
      <td>:&nbsp;<strong><asp:Label ID="lblJawSkrang" runat="server"></asp:Label></strong></td>
      <td>Gred Jawatan </td>
      <td>:&nbsp;<strong><asp:Label ID="lblGredGaji" runat="server"></asp:Label></strong></td>
    </tr>
    <tr>
      <td width=16%>Gaji Bulanan (Pokok) </td>
      <td width=35%>:&nbsp;<strong>RM <asp:Label ID="lblJumGaji" runat="server"></asp:Label></strong></td>
      <td width=16%>Elaun Memangku (jika ada) </td>
      <td width=33%>:&nbsp;<strong>RM <asp:Label ID="lblElaunMangku" runat="server"></asp:Label></strong></td>
    </tr>
    <tr>
      <td valign="top">Fakulti/ Jabatan <br>
Alamat Jabatan </td>
      <td valign="top">:&nbsp;<strong><asp:Label ID="lblNamaPejabat" runat="server"></asp:Label></strong><br>
:&nbsp;<strong><asp:Label ID="lblAlamatPejabat" runat="server"></asp:Label></strong></td>
      <td valign="top"><p>Adalah memangku kenaikan pangkat <br>
          <br>
      No Telefon </p>
      <p>Telefon bimbit </p></td>
      <td valign="top"><p><strong>:&nbsp;<strong><asp:Label ID="lblJawMangku" runat="server"></asp:Label></strong><br>
          </p><br />
      <p><strong>: <asp:Label ID="lblNoTelPejabat" runat="server"></asp:Label></strong></p></td>
    </tr>    
    <tr>
      <td>Nombor Lesen Memandu </td>
      <td>:&nbsp;<strong><asp:Label ID="lblNoLesen" runat="server"></asp:Label></strong>&nbsp;&nbsp;&nbsp;&nbsp;Kelas Memandu &nbsp;&nbsp;:&nbsp;<strong>
      <input name="hKelasLesen" type="hidden" value=""></strong></td>
      <td colspan="2"><table cellpadding=1 cellspacing=0>
    <tr> 
      	<td width="16%">Tarikh Tamat Lesen Memandu <span class="fontdberror">*</span></td>
      	<td width="35%"> <asp:TextBox ID="txtTmtLesen" runat="server" CssClass="form-control rightAlign" Width="120px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="cexttxtTarikh" runat="server" Animated="true" DefaultView="Days" FirstDayOfWeek="Monday" Format="dd/MM/yyyy" TargetControlID="txtTmtLesen" TodaysDateFormat="dd/MM/yyyy" PopupButtonID="lbtntxtTarikh" />
                                <asp:LinkButton ID="lbtntxtTarikh" runat="server" ToolTip="Klik untuk papar kalendar">
                                        <i class="far fa-calendar-alt fa-lg"></i>
                                </asp:LinkButton>					
			</td>
      	<td width="16%">&nbsp;</td>
      	<td width="33%">&nbsp;</td>
    </tr>
  </table>
                            </div>


                        </div>
                    </td>
                </tr>
            </table>
            
            <div class="row" style="text-align:center;">
                <asp:LinkButton ID="lbtnSimpan" runat="server" CssClass="btn btn-primary" ValidationGroup="btnSimpan">
						<i class="far fa-save fa-lg"></i>&nbsp;&nbsp;&nbsp;Simpan
                </asp:LinkButton>&nbsp;&nbsp; <asp:LinkButton ID="lbtnNext" runat="server" CssClass="btn btn-default btn-circle btn-lg" ToolTip="Seterusnya" ValidationGroup="btnSimpan">
                    <i class="glyphicon glyphicon-chevron-right"></i>
                </asp:LinkButton></div>

        </ContentTemplate>
        <Triggers>  
<%--           <asp:PostBackTrigger ControlID="lbtnUpPenyata" />  
            <asp:PostBackTrigger ControlID="lbtnProfilSya" /> --%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
