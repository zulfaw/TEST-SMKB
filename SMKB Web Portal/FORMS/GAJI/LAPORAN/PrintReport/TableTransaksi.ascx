<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="TableTransaksi.ascx.vb" Inherits="SMKB_Web_Portal.TableTransaksi" %>

<div runat="server" class="vot-list">
         <asp:Repeater runat="server" ID="rptone" OnItemDataBound="rptone_ItemDataBound">
             <ItemTemplate> 
                <div class="print-div">
                    <table id="tableTransaksi" class="table-with-border">
                        <thead>
                            <tr>
                               <td>  <%--colspan="22"--%>
                                    <div class="header-space">&nbsp;</div>
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: center; width: 5%;" class="tdbg1 No" rowspan="2">No</td>
                                <td style="text-align: center; width: 15%;" class="tdbg1 Nama" rowspan="2">Nama</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 No_KP" rowspan="2">No KP</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Gaji_Pokok" rowspan="2">Gaji Pokok</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Bonus" rowspan="2">Bonus</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Elaun" rowspan="2">Elaun</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 OT" rowspan="2">OT</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Gaji_Kasar" rowspan="2">Gaji Kasar</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Potongan" rowspan="2">Potongan</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Cuti" rowspan="2">Cuti</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 No_KWSP" rowspan="2">No KWSP</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 KWSP" colspan="2">KWSP</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 No_Cukai" rowspan="2">No Cukai</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Kat_Cukai" rowspan="2">Kategori Cukai</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Cukai" rowspan="2">Cukai</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Gaji_Bersih" rowspan="2">Gaji Bersih</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 No_Perkeso" rowspan="2">No Perkeso</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 SOCP" colspan="2">Perkeso</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 No_Pencen" rowspan="2">No Pencen</td>
                                <td style="text-align: center; width: 5%;" class="tdbg1 Pencen" rowspan="2">Pencen</td>
                            </tr>
                            <tr>
                                <td class="tdbg1 KWSP">Pekerja</td>
                                <td class="tdbg1 KWSM">Majikan</td>
                                <td class="tdbg1 SOCP">Pekerja</td>
                                <td class="tdbg1 SOCM">Majikan</td>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="repeaterDetail" runat="server">
                                     <ItemTemplate>
                                        <tr>
                                         <td class="column-to-hide No" style="text-align: center;width: 5%;"><%#Eval("No_Staf")%></td>
                                         <td class="column-to-hide Nama" style="width: 15%"><%#Eval("Nama")%></td>
                                         <td class="column-to-hide No_KP" style="text-align: center; width: 5%;"><%#Eval("KP")%></td>
                                         <td class="column-to-hide Gaji_Pokok" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Gaji_Pokok")) %></td>
                                         <td class="column-to-hide Bonus" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Bonus")) %></td>
                                         <td class="column-to-hide Elaun" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Elaun")) %></td>
                                         <td class="column-to-hide OT" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "OT")) %></td>
                                         <td class="column-to-hide Gaji_Kasar" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Gaji_Kasar")) %></td>
                                         <td class="column-to-hide Potongan" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Potongan")) %></td>
                                         <td class="column-to-hide Cuti" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Cuti")) %></td>
                                         <td class="column-to-hide No_KWSP" style="text-align: center;width: 5%;"><%#Eval("No_KWSP")%></td>
                                         <td class="column-to-hide KWSP" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "KWSP")) %></td>
                                         <td class="column-to-hide KWSM" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "KWSM")) %></td>
                                         <td class="column-to-hide No_Cukai" style="text-align: center;width: 5%;"><%#Eval("No_Cukai")%></td>
                                         <td class="column-to-hide Kat_Cukai" style="text-align: center;width: 5%;"><%#Eval("Kategori_Cukai")%></td>
                                         <td class="column-to-hide Cukai" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Cukai")) %></td>
                                         <td class="column-to-hide Gaji_Bersih" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Gaji_Bersih")) %></td>
                                         <td class="column-to-hide No_Perkeso" style="text-align: center;width: 5%;"><%#Eval("No_Perkeso")%></td>
                                         <td class="column-to-hide SOCP" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SOCP")) %></td>
                                         <td class="column-to-hide SOCM" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "SOCM")) %></td>
                                         <td class="column-to-hide No_Pencen" style="text-align: center; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "No_Pencen"))  %></td>
                                         <td class="column-to-hide Pencen" style="text-align: right; width: 5%;"><%# String.Format("{0:N2}", DataBinder.Eval(Container.DataItem, "Pencen"))  %></td>
                                      </tr>
                                 </ItemTemplate>
                            </asp:Repeater>
                            <script>
                            </script>
                        </tbody>
                    </table>
                </div>
             </ItemTemplate>
        </asp:Repeater>
</div> 

<script type="text/javascript">
    $(document).ready(function () {
        hideAllColumnsByClass("tdbg1");
        hideAllColumnsByClass("column-to-hide");

        var columnClassMap = {
            0: "No",
            1: "Nama",
            2: "No_KP",
            3: "Gaji_Pokok",
            4: "Bonus",
            5: "Elaun",
            6: "OT",
            7: "Gaji_Kasar",
            8: "Potongan",
            9: "Cuti",
            10: "No_KWSP",
            11: "KWSP",
            12: "KWSM",
            13: "No_Cukai",
            14: "Kat_Cukai",
            15: "Cukai",
            16: "Gaji_Bersih",
            17: "No_Perkeso",
            18: "SOCP",
            19: "SOCM",
            20: "No_Pencen",
            21: "Pencen"
            // Add more mappings as needed
        };
        // Retrieve the serialized JSON string from the cookie
        var visibleColumnsJson = getCookie("VisibleColumns");

        if (visibleColumnsJson) {
            // Parse the JSON string to a JavaScript array
            var visibleColumns = JSON.parse(visibleColumnsJson);
            visibleColumns.forEach(function (index) {
                var columnClass = columnClassMap[index];
                hideColumnsByClass(columnClass, false);
            });
        }

        function hideColumnsByClass(className, isHidden) {
            var elements = document.querySelectorAll("." + className);
            for (var i = 0; i < elements.length; i++) {
                elements[i].style.display = isHidden ? "none" : "";
            }
        }

        // Function to initially hide all elements with the specified class
        function hideAllColumnsByClass(className) {
            var elements = document.querySelectorAll("." + className);
            for (var i = 0; i < elements.length; i++) {
                elements[i].style.display = "none";
            }
        }

        // Function to get a cookie value by name
        function getCookie(name) {
            var value = "; " + document.cookie;
            var parts = value.split("; " + name + "=");

            if (parts.length === 2) {
                return parts.pop().split(";").shift();
            }
        }
    });
   
</script>