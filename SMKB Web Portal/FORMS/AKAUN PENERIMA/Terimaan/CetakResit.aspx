<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CetakResit.aspx.vb" Inherits="SMKB_Web_Portal.CetakResit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cetak Resit</title>
    <script src="https://code.jquery.com/jquery-3.5.1.js" crossorigin="anonymous"></script>
        
</head>
<body>
    <style>
        * {
            font-family: 'Arial', Times, serif;
        }
        /* Custom CSS for GridView styling */
        .table th {
            text-align: center;
            font-weight: bold;
        }

        body {
            /* Set page size to A4 (21cm x 29.7cm) and add margins */
            size: A4 portrait;
            margin: 2cm;
            max-width: 100%;
        }

        /* Hide the print button when printing */
        @media print {
            body {
                /* Reset body styles for printing */
                size: Letter portrait;
                margin: 0;
                max-width: 100%;
            }

            @page {
                /* Define page size and margins for printing */
                size: Letter portrait;
                margin: 1cm; /* Adjust margin to fit Letter size */
            }

            .container {
                /* Adjust container styles for printing */
                width: 100%;
                margin: 0;
                padding: 0;
            }

            #lblTarikhLaporan {
                /* Adjust label styles for printing */
                width: 15%;
            }

            #gvPenghutang {
                /* Add page break for the GridView */
                page-break-inside: auto;
            }

            /* Hide unnecessary elements */
            #btnPrint, hr {
                display: none;
            }
        }
    </style>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <table align="center">
                <tr >
                    <td>
                        <img src="../../../Images/logo.png" />
                    </td>
                    <td align="left">
                        <h4>UNIVERSITI TEKNIKAL MALAYSIA MELAKA<br />
                            HANG TUAH JAYA,<br />
                            76100, DURIAN TUNGGAL, MELAKA</h4>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center;">
            <h2>RESIT</h2>
        </div>
        <br />
        <div>
            <table  class="form-control"  style="width: 100%;">
                <tr>
                    <td style="width:60%" ><b>Daripada :</b></td>
                    <td style="width:10%">&nbsp;</td>
                    <td style="width:1%">&nbsp;</td>
                    <td style="width:29%;text-align:left">&nbsp;</td>
                </tr>
                <tr>
                    <td ><label class="form-control" id="Nama" name="Nama" runat="server"></label></td>
                    <td>No. Resit </td>
                    <td></td>
                    <td><b><label class="" id="txtnoResit" name="txtnoResit" runat="server"></label></b></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Tarikh</td>
                    <td>:</td>
                    <td><label class="" id="tkhBil" name="tkhBil" runat="server"></label></td>
                </tr>
                <tr>
                    <td><label class="" id="Alamat1" name="Alamat1" runat="server"></label></td>
                    <td>No. Bil</td>
                    <td>:</td>
                    <td><label class="" id="txtNoBil" name="txtNoBil" runat="server"></label></td>
                </tr>
                <tr>
                    <td><label class="" id="Alamat2" name="Alamat2" runat="server"></label></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><label class="" id="Alamat3" name="Alamat3" runat="server"></label></td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <div>
            <label><b>Tujuan : </b></label>
            <label class="" id="txtTujuan" name="txtTujuan" runat="server"></label>
        </div>
        <br />
        <%--<div>
            <table style="width: 100%;">
                <thead id="table_Header">
                    <tr style="text-align: center">
                        <th style="width: 40%">Perkara</th>
                        <th style="width: 10%">Kuantiti</th>
                        <th style="width: 10%">Harga Seunit (RM)</th>
                        <th style="width: 10%">Cukai (%)</th>
                        <th style="width: 10%">Diskaun (%)</th>
                        <th style="width: 20%">Jumlah (RM)</th>
                    </tr>
                </thead>
                <tbody id="table_Details">
                </tbody>
            </table>
        </div>--%>
        <asp:GridView ID="gvTransaksi" runat="server" Width="99%" AutoGenerateColumns="False" BorderWidth="1">
            <Columns>
                <asp:TemplateField HeaderText="Bil" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>.
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" Width="2%" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="Perkara" DataField="Butiran" SortExpression="Perkara" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="48%" ItemStyle-HorizontalAlign="Justify"/>
                <asp:BoundField HeaderText="Carta Akaun" DataField="COA" SortExpression="Kuantiti" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField HeaderText="Jumlah (RM)" DataField="Kredit" SortExpression="Jumlah Bayar" HeaderStyle-CssClass="centerAlign" ItemStyle-Width="29%" ItemStyle-HorizontalAlign="Right"  />
            </Columns>
            <HeaderStyle />
        </asp:GridView>
        <br />
        <div class="modal-footer modal-footer--sticky">
            <table style="width: 99%;text-align:right;">
                <%--<tr style="display:none">
                    <td style="width: 40%"></td>
                    <td style="width: 40%;text-align:right">Jumlah (RM)</td>
                    <td style="width: 20%"><label id="txtJumlah" name="txtJumlah" runat="server"></label></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Jumlah Cukai (RM)</td>
                    <td><label id="txtJumlahCukai" name="txtJumlahCukai" runat="server"></label></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Jumlah Diskaun (RM)</td>
                    <td><label id="txtJumlahDiskaun" name="txtJumlahDiskaun" runat="server"></label></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>Jumlah Sebenar(RM)</td>
                    <td><label id="txtJumlahSebenar" name="txtJumlahSebenar" runat="server"></label></td>
                </tr>--%>
                <tr>
                    <td>&nbsp;</td>
                    <td><b>JUMLAH DIBAYAR(RM)</b></td>
                    <td><label id="txtDibayar" name="txtDibayar" runat="server"></label></td>
                </tr>
                <%--<tr>
                    <td>&nbsp;</td>
                    <td><b>BAKI(RM)</b></td>
                    <td><label >0.00</label></td>
                </tr>--%>
            </table>
        </div>
        <br />
        <div>
            <hr />
            <div style="text-align:center">
                <b>*****Terima Kasih*****</b>
            </div>
            <hr />
        </div>
    </form>
</body>
<script type="text/javascript">
    $(document).ready(function () {
        console.log("Hai");
        window.print();
    });

    //    var bilno = urlParams.get('bilid');
    //    if (bilno !== "") {
    //        alert("Hai")
    //        LoadData(bilno)
    //    }
        
    //    //async function LoadData(bilno) {
    //    //    if (bilno !== "") {

    //    //        //BACA HEADER Bil
    //    //        var recordHdr = await AjaxGetRecordHdrJurnal(bilno);
    //    //        await AddRowHeader(null, recordHdr);
    //    //        //BACA DETAIL Bil
    //    //        var record = await AjaxGetRecordJurnal(bilno);
    //    //        await clearAllRows();
    //    //        await AddRow(null, record);
    //    //    }
    //    //}
    //    //async function AjaxGetRecordHdrJurnal(id) {

    //    //    try {

    //    //        const response = await fetch('InvoisWS.asmx/LoadRecordBil_Header', {
    //    //            method: 'POST',
    //    //            headers: {
    //    //                'Content-Type': 'application/json'
    //    //            },
    //    //            body: JSON.stringify({ id: id })
    //    //        });
    //    //        const data = await response.json();
    //    //        return JSON.parse(data.d);
    //    //    } catch (error) {
    //    //        console.error('Error:', error);
    //    //        return false;
    //    //    }
    //    //}
    //    //async function AjaxGetRecordJurnal(id) {

    //    //    try {

    //    //        const response = await fetch('InvoisWS.asmx/LoadRecordBil', {
    //    //            method: 'POST',
    //    //            headers: {
    //    //                'Content-Type': 'application/json'
    //    //            },
    //    //            body: JSON.stringify({ id: id })
    //    //        });
    //    //        const data = await response.json();
    //    //        return JSON.parse(data.d);
    //    //    } catch (error) {
    //    //        console.error('Error:', error);
    //    //        return false;
    //    //    }
    //    //}

    //    //async function AddRowHeader(totalClone, objOrder) {
    //    //    var counter = 1;
    //    //    //var table = $('#tblDataSenarai');

    //    //    if (objOrder !== null && objOrder !== undefined) {
    //    //        totalClone = objOrder.Payload.length;
    //    //    }


    //    //    if (counter <= objOrder.Payload.length) {
    //    //        await setValueToRow_HdrBil(objOrder.Payload[counter - 1]);
    //    //    }
    //    //    // console.log(objOrder)
    //    //}
    //    //async function setValueToRow_HdrBil(orderDetail) {

    //    //    $('#txtnobil').val(orderDetail.No_Bil)
    //    //    $('#tkhBil').val(orderDetail.Tkh_Lulus)
    //    //    $('#tkhTamat').val(orderDetail.Tkh_Tamat)
    //    //    $('#txtJumlah').val(orderDetail.Jumlah)
    //    //    $('#txtJumlahDiskaun').val(orderDetail.Diskaun)
    //    //    $('#txtJumlahCukai').val(orderDetail.Cukai)
    //    //    $('#txtJumlahSebenar').val(orderDetail.JumlahSebenar)

    //    //}

    //    //async function AddRow(totalClone, objOrder) {
    //    //    var counter = 1;
    //    //    var table = $('#tblData');

    //    //    if (objOrder !== null && objOrder !== undefined) {
    //    //        totalClone = objOrder.Payload.length;
    //    //        console.log(totalClone)
    //    //    }

    //    //    while (counter <= totalClone) {
    //    //        curNumObject += 1;
                
    //    //        if (objOrder !== null && objOrder !== undefined) {
    //    //            //await setValueToRow(row, objOrder.Payload.OrderDetails[counter - 1]);
    //    //            if (counter <= objOrder.Payload.length) {
    //    //                await setValueToRow_Transaksi(row, objOrder.Payload[counter - 1]);

    //    //            }
    //    //        }
    //    //        counter += 1;
    //    //    }
    //    //}

    //    //async function setValueToRow_Transaksi(row, orderDetail) {

    //    //    var details = $(row).find("td > .details");
    //    //    details.val(orderDetail.Perkara);

    //    //    var quantity = $(row).find("td > .quantity");
    //    //    quantity.val(orderDetail.Kuantiti);
    //    //    console.log(orderDetail)
    //    //    var cukai = $(row).find("td > .cukai");
    //    //    cukai.val(orderDetail.Cukai.toFixed(2));

    //    //    var kdr_hrga = $(row).find("td > .price");
    //    //    kdr_hrga.val(orderDetail.Kadar_Harga.toFixed(2));

    //    //    var diskaun = $(row).find("td > .diskaun");
    //    //    diskaun.val(orderDetail.Diskaun.toFixed(2));

    //    //    var amount = $(row).find("td > .amount");
    //    //    amount.val(orderDetail.Jumlah);

    //        //var hddataid = $(row).find("td > .data-id");
    //        //hddataid.val(orderDetail.dataid)

    //        //var quantity = $(curTR).find("td > .quantity");
            
    //        //var JUMcukai = $(row).find("td > .JUMcukai");
    //        //var JUMdiskaun = $(row).find("td > .JUMdiskaun");
    //        //var amountwocukai = $(row).find("td > .amountwocukai");

    //        //var totalPrice = NumDefault(quantity.val()) * NumDefault(kdr_hrga.val())
    //        //var amauncukai = NumDefault(cukai.val()) / 100
    //        //var total_cukai = totalPrice * amauncukai
    //        //var amaundiskaun = NumDefault(diskaun.val()) / 100
    //        //var total_diskaun = totalPrice * amaundiskaun
    //        //var amountxcukai = totalPrice - total_diskaun

    //        //totalPrice = totalPrice + total_cukai - total_diskaun
    //        //amount.val(totalPrice.toFixed(2));
    //        //JUMcukai.val(total_cukai.toFixed(2));
    //        //JUMdiskaun.val(total_diskaun.toFixed(2));
    //        //amountwocukai.val(amountxcukai.toFixed(2));
    //        //calculateGrandTotal();

    //    }
    //});
    </script>
</html>
