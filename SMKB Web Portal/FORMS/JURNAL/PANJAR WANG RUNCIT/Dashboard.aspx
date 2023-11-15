<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Dashboard.aspx.vb" Inherits="SMKB_Web_Portal.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <%--<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/chartjs-plugin-datalabels/2.2.0/chartjs-plugin-datalabels.min.js"></script>--%>

    <div id="dashboard" class="tabcontent" style="display: block">

        <div class="col-md-12">
            <div class="form-row justify-content-center">
                <div class="col-md-4 card-deck">
                    <div class="card bg-info text-center">
                        <p class="card-text text-white font-weight-bold">Permohonan</p>
                        <span class="bg-white jumMohon"></span>
                    </div>
                </div>

                <div class="col-md-4 card-deck">
                    <div class="card bg-success text-center">
                        <p class="card-text text-white font-weight-bold">Permohonan Lulus</p>
                        <span class="bg-white jumLulus"></span>
                    </div>
                </div>

                <div class="col-md-4 card-deck">
                    <div class="card bg-danger text-center">
                        <p class="card-text text-white font-weight-bold">Permohonan Ditolak</p>
                        <span class="bg-white jumTolak"></span>
                    </div>
                </div>

            </div>
            <br />

            <div class="form-row">

                <div id="pie" style="width: 30%">
                    <canvas id="pieChart" width="400" height="400" style="display: none"></canvas>
                </div>

                <div id="line" style="width: 40%">
                    <canvas id="lineChart" width="400" height="400"></canvas>
                </div>
            </div>

        </div>



    </div>



    <script>

        //make an ajax call to the webservice

        var ptj = <%=Session("ssusrKodPTj")%>;
        var kump = "";
        var cloneCount = 1;
        var newCanvas;
        var count = 0;
        var index = 0;


        $(document).ready(function () {

            $.ajax({
                url: 'Panjar_WS.asmx/LoadOrder_KumpulanPTJ',
                method: 'POST',
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ptj: ptj }),
                success: function (data) {
                    /* sini amik data dekat db macam aku amik column butiran dan baki*/
                    var data = JSON.parse(data.d);
                    data.forEach(function (item) {
                        kump = item.Kod_Kump_Wang;
                        console.log(kump);
                        newCanvas = $('#pieChart').clone().attr('id', 'pieChart' + cloneCount++).insertAfter($('[id^=pieChart]:last'));
                        $(newCanvas).attr('style', '');
                        pie(ptj, kump, $(newCanvas).attr('id'), index);
                        line(ptj, kump);
                        index++;
                        count++;
                    })
                },
                error: function () {
                    console.log('Error fetching data from the web service.');
                }
            })

            $.ajax({
                url: 'Panjar_WS.asmx/LoadJumlah_Mohon',
                method: 'POST',
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ptj: ptj }),
                success: function (data) {
                    /* sini amik data dekat db macam aku amik column butiran dan baki*/
                    var data = JSON.parse(data.d);
                    $('.jumMohon').html(data[0].Jumlah_Permohonan);
                },
                error: function () {
                    console.log('Error fetching data from the web service.');
                }
            });

            $.ajax({
                url: 'Panjar_WS.asmx/LoadJumlah_Lulus',
                method: 'POST',
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ptj: ptj }),
                success: function (data) {
                    /* sini amik data dekat db macam aku amik column butiran dan baki*/
                    var data = JSON.parse(data.d);
                    $('.jumLulus').html(data[0].Jumlah_PermohonanLulus);
                },
                error: function () {
                    console.log('Error fetching data from the web service.');
                }
            });

            $.ajax({
                url: 'Panjar_WS.asmx/LoadJumlah_Tolak',
                method: 'POST',
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ptj: ptj }),
                success: function (data) {
                    /* sini amik data dekat db macam aku amik column butiran dan baki*/
                    var data = JSON.parse(data.d);
                    $('.jumTolak').html(data[0].Jumlah_PermohonanTolak);
                },
                error: function () {
                    console.log('Error fetching data from the web service.');
                }
            });
        });

        function line(ptj, kump) {
            $.ajax({
                url: 'Panjar_WS.asmx/LoadTahunan',
                method: 'POST',
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ptj: ptj, kumpulan: kump }),
                success: function (data) {
                    /* sini amik data dekat db macam aku amik column butiran dan baki*/
                    var chartData = JSON.parse(data.d);
                    var categorizeM = [];
                    var valuesL = [];
                    chartData.forEach(function (item) {
                        categorizeM.push(item.Bulan);
                        valuesL.push(parseFloat(item.Jumlah_Lulus));
                    });
                    /*ni untuk buat pie chart*/
                    createLineChart(categorizeM, valuesL)
                },
                error: function () {
                    console.log('Error fetching data from the web service.');
                }
            })
        }


        function pie(ptj, kump, id, index) {
            $.ajax({
                url: 'Panjar_WS.asmx/LoadBaki_Pengagihan',
                method: 'POST',
                dataType: 'JSON',
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ptj: ptj, kumpulan: kump }),
                success: function (data) {
                    /* sini amik data dekat db macam aku amik column butiran dan baki*/
                    var chartData = JSON.parse(data.d);
                    var values = [];
                    var belanja = chartData[index].Jumlah_Agih;
                    var baki = chartData[index].Baki;
                    var pej = chartData[index].Pejabat;
                    var namaKump = chartData[index].Butiran;
                    /*ni untuk buat pie chart*/
                    belanja -= baki;
                    values.push(baki);
                    values.push(belanja);
                    createChart(values, id, pej, namaKump)
                },
                error: function () {
                    console.log('Error fetching data from the web service.');
                }
            })
        }

        function createChart(values, id, pej, namaKump) {
            var ctx = document.getElementById(id).getContext('2d');
            return new Chart(ctx, {
                type: 'pie',
                data: {
                    labels: ['Baki', 'Belanja'],     //category untuk value
                    datasets: [{
                        data: values,   //nilai data dekat pie
                        backgroundColor: [                //color untuk setiap category
                            //"rgba(255, 99, 132, 0.6)",
                            //"rgba(54, 162, 235, 0.6)",
                            "rgba(255, 193, 7, 0.6)",
                            "rgba(0, 123, 255, 0.6)",
                        ]
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        datalabels: {
                            formatter: function (value, context) {
                                var dataset = context.chart.data.datasets[context.datasetIndex];
                                var total = dataset.data.reduce(function (previousValue, currentValue) {
                                    return previousValue + currentValue;
                                });
                                var currentValue = dataset.data[context.dataIndex];
                                var percentage = ((currentValue / total) * 100).toFixed(2) + '%';
                                return percentage; // Display the percentage on the pie chart segment
                                alert("masuk");
                            },
                            color: 'black', // Color of the percentage text
                            font: {
                                weight: 'bold', // Font weight of the percentage text
                            },
                        },
                        legend: {
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: namaKump,
                        }
                    }
                }
            });
        }

        function createLineChart(categorizeM, valuesL) {

            var valuesLulus = [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0];
            var i = 0;
            var d = new Date();
            year = d.getFullYear();

            while (i < 12) {
                var bulan = categorizeM[i];
                var jum = valuesL[i];
                if (bulan == categorizeM[i - 1]) {
                    jum += valuesL[i - 1];
                }
                valuesLulus[bulan - 1] = jum;
                console.log(jum);
                i++;
            }

            console.log(valuesLulus);

            var months = ["Januari", "Februari", "Mac", "April", "Mei", "Jun", "Julai", "Ogos", "September", "Oktober", "November", "Disember"];

            var ctxL = document.getElementById('lineChart').getContext('2d');
            return new Chart(ctxL, {
                type: 'line',
                data: {
                    labels: months,     //category untuk value
                    datasets: [{
                        labels: "Amaun",
                        data: valuesLulus,   //nilai data dekat pie
                        backgroundColor: [                //color untuk setiap category
                            'rgba(255, 99, 132, 0.7)',  // Red
                            'rgba(54, 162, 235, 0.7)',  // Blue
                            'rgba(255, 206, 86, 0.7)',  // Yellow
                            'rgba(75, 192, 192, 0.7)',  // Teal
                            'rgba(153, 102, 255, 0.7)', // Purple
                            'rgba(255, 159, 64, 0.7)',  // Orange
                            'rgba(0, 204, 0, 0.7)',     // Green
                            'rgba(255, 0, 255, 0.7)',   // Magenta
                            'rgba(128, 128, 128, 0.7)', // Gray
                            'rgba(255, 0, 0, 0.7)',     // Bright Red
                            'rgba(0, 0, 255, 0.7)',     // Bright Blue
                            'rgba(0, 128, 0, 0.7)'      // Bright Green
                        ],
                        borderColor: 'rgb(75, 192, 192, 0.5)'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: {
                            display: false,
                            position: 'top',
                        },
                        title: {
                            display: true,
                            text: "Jumlah Terimaan Sepanjang Tahun " + year,
                            fontColor: "black"
                        }
                    }
                }
            });
        }

    </script>

</asp:Content>
