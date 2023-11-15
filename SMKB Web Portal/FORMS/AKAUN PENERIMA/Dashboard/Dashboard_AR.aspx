<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/NestedMasterPage2.master" CodeBehind="Dashboard_AR.aspx.vb" Inherits="SMKB_Web_Portal.Dashboard_AR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FormContents" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels"></script>
    <%--<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/chartjs-plugin-datalabels/2.2.0/chartjs-plugin-datalabels.min.js"></script>--%>

    <style>
        .card {
            border: none;
            border-top-left-radius: 0;
            border-top-right-radius: 0;
            position: relative;
            z-index: 3;
            margin-bottom: 30px;
        }

        .card-wrapper {
            margin-bottom: 100px;
        }

        .card-title {
            background-color: white;
            padding: 10px;
            margin-top: -15px;
            /*z-index: 2;*/
        }

        .gradient-card {
            /*margin-bottom: 20px;*/
            /* background: linear-gradient(to right, #40E0D0, #87CEEB, #0000FF);*/
            /*background-color: rgb(40, 157, 246);*/
            color: white;
            border: 10px;
            padding-top: 10px;
            padding-bottom: 30px;
        }

        /*.gradient-card::after {
                content: "";
                position: absolute;
                bottom: 0;
                left: 50%;
                transform: translateX(-50%);
                width: 50px;*/ /* Set the width of your stroke line */
        /*height: 10px;*/ /* Set the height of your stroke line */
        /*background-color: #3498db;*/ /* Set the color of your stroke line */
        /*}*/

        .gradient-header {
            color: black;
            background-color: white;
            padding-top: 15px;
            padding-bottom: 15px;
        }

        .card-footer {
            padding: 10px 20px;
            text-align: center;
        }

        .card-body {
            padding: 20px;
        }

        .rounded-top-icon {
            text-align: center;
            margin-top: -30px;
            position: absolute;
            top: -100px;
            left: 30%;
            padding-top: 40px;
            z-index: -2;
        }

            .rounded-top-icon i {
                background-color: white;
                border-radius: 100%;
                padding: 30px;
                box-shadow: 4px 4px 4px rgba(0, 0, 0, 0.5);
            }

        .chart-container {
            position: relative;
            margin: auto;
            height: 40vh;
            width: 35vw;
        }
    </style>

    <div id="dashboard" class="tabcontent" style="display: block;">
        <br />
        <br />
        <br />
        <br />
        <div class="col-md-12 container">
            <div class="form-row justify-content-center">

                <div class="col-md-3" style="z-index: 3;">
                    <div class="card-wrapper">
                        <div class="card gradient-card shadow h-auto py-2" style="background-color: #d0581c;">
                            <div class="gradient-header text-center mx-auto" style="width: 100%;">
                                 <b>AKAUN BELUM TERIMA</b>
                            </div>
                            <div class="card-body">
                                <div id="jumTerima" class="text-center" style="font:bold;font-size:large"></div>
                            </div>
                        </div>
                        <div class="rounded-top-icon">
                            <i class="fas la-file-invoice fa-4x shadow h-auto" style="color: #d0581c"></i>
                        </div>
                    </div>
                </div>

                <div class="col-md-3" style="z-index: 3;">
                    <div class="card-wrapper">
                        <div class="card gradient-card shadow h-auto py-2" style="background-color: #f1ac88;">
                            <div class="gradient-header text-center mx-auto" style="width: 100%;">
                                <b>JUMLAH BIL TERTUNGGAK</b>
                            </div>
                            <div class="card-body">
                                <div id="jumTunggak" class="text-center" style="font:bold;font-size:large"></div>
                            </div>
                        </div>
                        <div class="rounded-top-icon">
                            <i class="fas la-clock fa-4x shadow h-auto" style="color: #f1ac88;"></i>
                        </div>
                    </div>
                </div>

                <div class="col-md-3" style="z-index: 3;">
                    <div class="card-wrapper">
                        <div class="card gradient-card shadow h-auto py-2" style="background-color: #fbb023;">
                            <div class="gradient-header text-center mx-auto" style="width: 100%;">
                                <b>PERATUS BIL TERTUNGGAK</b>
                            </div>
                            <div class="card-body">
                                <div id="jumTunggakPeratus" class="text-center" style="font:bold;font-size:large"></div>
                            </div>
                        </div>
                        <div class="rounded-top-icon">
                            <i class="fas la-clock fa-4x shadow h-auto" style="color: #fbb023"></i>
                        </div>
                    </div>
                </div>
                <div class="col-md-3" style="z-index: 3;">
                    <div class="card-wrapper">
                        <div class="card gradient-card shadow h-auto py-2" style="background-color: #5d6d7c;">
                            <div class="gradient-header text-center mx-auto" style="width: 100%;">
                                <b>TUNAI DI TANGAN</b>
                            </div>
                            <div class="card-body">
                                <div id="jumTunai" class="text-center" style="font:bold;font-size:large"></div>
                            </div>
                        </div>
                        <div class="rounded-top-icon">
                            <i class="fas la-hand-holding-usd fa-4x shadow h-auto" style="color: #5d6d7c;"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12">
            <div class="row">
                <!-- Bar Chart -->
                <div class="col-xl-6 col-md-6 mb-4">
                    <div class="card shadow">
                        <!-- Card Header - Dropdown -->
                        <div
                            class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                            <h6 class="m-0 font-weight-bold " style="color:darkslategrey">Akaun Belum Terima Mengikut Tahun</h6>
                        </div>
                        <!-- Card Body -->
                        <div class="card-body">
                            <div id="bar" class="chart-bar pt-4 pb-2 chart-container">
                                <canvas id="barChart"></canvas>
                            </div>
                        </div>

                    </div>
                </div>

                <!-- Doughnut Chart -->
                <div class="col-xl-6 col-md-6 mb-4">
                    <div class="card shadow">
                        <!-- Card Header - Dropdown -->
                        <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                            <h6 class="m-0 font-weight-bold " style="color:darkslategrey">Bil Mengikut Status</h6>
                        </div>
                        <!-- Card Body -->
                        <div class="card-body">
                            <div id="pie" class="chart-pie pt-4 pb-2 chart-container">
                                <canvas id="pieChart"></canvas>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>

        <script type="text/javascript">

            var tbl = null;
            var id = null;
            var cloneCount = 1;
            var newCanvas;
            var count = 0;
            var index = 0;
            var TotalTunggak = 0;
            var TotalTerima = 0;

            $(document).ready(function () {

                // Get data for pie chart
                $.ajax({
                    url: 'DashboardAR_WS.asmx/LoadJum_Bill',
                    method: 'POST',
                    dataType: 'JSON',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({}),
                    success: function (data) {
                        /* sini amik data dekat db macam aku amik column butiran dan baki*/
                        var data = JSON.parse(data.d);
                        data.forEach(function (item) {
                            Jum_Bill = item.Jum_Bil;
                            console.log(Jum_Bill);
                            //newCanvas = $('#pieChart').clone().attr('id', 'pieChart' + cloneCount++).insertAfter($('[id^=pieChart]:last'));
                            //$(newCanvas).attr('style', '');
                            Pie();
                            bar();
                            index++;
                            count++;
                        })
                    },
                    error: function () {
                        console.log('Error fetching data from the web service.');
                    }
                })

                ////Get data for card
                $.ajax({
                    url: 'DashboardAR_WS.asmx/LoadJumTerima',
                    method: 'POST',
                    dataType: 'JSON',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({}),
                    success: function (data) {
                        /* sini amik data dekat db macam aku amik column butiran dan baki*/
                        var data = JSON.parse(data.d);
                        TotalTerima = data[0].JUMLAH_AKAUN_BELUM_TERIMA;
                        var formattedData = parseFloat(data[0].JUMLAH_AKAUN_BELUM_TERIMA).toLocaleString('en-MY', { style: 'currency', currency: 'MYR', minimumFractionDigits: 2 });
                        $("#jumTerima").html(formattedData);
                        /*TotalTerima = $("#jumTerima").html();*/

                        $.ajax({
                            url: 'DashboardAR_WS.asmx/LoadJumTunggakPeratus',
                            method: 'POST',
                            dataType: 'JSON',
                            contentType: "application/json; charset=utf-8",
                            data: JSON.stringify({ TotalTerima: TotalTerima }),
                            success: function (data) {
                                /* sini amik data dekat db macam aku amik column butiran dan baki*/
                                var data = JSON.parse(data.d);
                                var formattedPercentage = parseFloat(data[0].PeratusTunggakan).toFixed(0) + '%';
                                $("#jumTunggakPeratus").html(formattedPercentage);
                                /*$('.jumLulus').html(data[0].Jumlah_PermohonanLulus);*/
                            },
                            error: function () {
                                console.log('Error fetching data from the web service.');
                            }
                        });
                    },
                    error: function () {
                        console.log('Error fetching data from the web service.');
                    }
                });

                $.ajax({
                    url: 'DashboardAR_WS.asmx/LoadJumTunggak',
                    method: 'POST',
                    dataType: 'JSON',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({}),
                    success: function (data) {
                        /* sini amik data dekat db macam aku amik column butiran dan baki*/
                        var data = JSON.parse(data.d);
                        var formattedData = parseFloat(data[0].Total_Tunggak).toLocaleString('en-MY', { style: 'currency', currency: 'MYR', minimumFractionDigits: 2 });
                        $("#jumTunggak").html(formattedData);
                        TotalTunggak = $("#jumTunggak").html();
                    },
                    error: function () {
                        console.log('Error fetching data from the web service.');
                    }
                });

                $.ajax({
                    url: 'DashboardAR_WS.asmx/LoadTunai',
                    method: 'POST',
                    dataType: 'JSON',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({}),
                    success: function (data) {
                        var data = JSON.parse(data.d);
                        var formattedData = parseFloat(data[0].JUMLAH_TUNAI).toLocaleString('en-MY', { style: 'currency', currency: 'MYR', minimumFractionDigits: 2 });
                        $("#jumTunai").html(formattedData);
                    },
                    error: function () {
                        console.log('Error fetching data from the web service.');
                    }
                });

            });

            function Pie(index) {
                $.ajax({
                    url: 'DashboardAR_WS.asmx/LoadDataStatusPie',
                    method: 'POST',
                    dataType: 'JSON',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({}),
                    success: function (data) {
                        /* sini amik data dekat db macam aku amik column butiran dan baki*/
                        var chartData = JSON.parse(data.d);
                        var values = [];
                        chartData.forEach(function (item) {
                            values.push(item.Bayaran_TepatWaktu);
                            values.push(item.Hutang_lapuk);
                            values.push(item.JumBilTertunggak);
                        });
                        createPieChart(values);
                    },
                    error: function () {
                        console.log('Error fetching data from the web service.');
                    }
                });
            }

            function createPieChart(values) {

                var ctx = document.getElementById('pieChart').getContext('2d');
                return new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: ['Bayaran Tepat Waktu', 'Hutang Lapuk', 'Tunggakan'],     //category untuk value
                        datasets: [{
                            data: values,   //nilai data dekat pie
                            backgroundColor: [                //color untuk setiap category
                                //"rgba(255, 99, 132, 0.6)",
                                "rgba(54, 162, 235, 0.6)",
                                "rgba(255, 193, 7, 0.6)",
                                "rgba(0, 123, 255, 0.6)",
                            ]
                        }]
                    },
                    options: {
                        /*aspectRatio: 1,*/
                        responsive: true,
                        maintainAspectRatio: false,
                        animation: {
                            animateScale: true, // Enable animation for scaling
                            animateRotate: true
                        },
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
                                text: 'Bil Mengikut Status',
                            }
                        }
                    }
                });
            }

            function bar() {
                $.ajax({
                    url: 'DashboardAR_WS.asmx/LoadJumTerima_Tahunan',
                    method: 'POST',
                    dataType: 'JSON',
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({}),
                    success: function (data) {
                        /* sini amik data dekat db macam aku amik column butiran dan baki*/
                        var chartData = JSON.parse(data.d);
                        var categorizeM = [];
                        var valuesL = [];
                        chartData.forEach(function (item) {
                            categorizeM.push(item.Tahun);
                            valuesL.push(parseFloat(item.JUMLAH_AKAUN_BELUM_TERIMA));
                        });
                        /*ni untuk buat pie chart*/
                        createBarChart(categorizeM, valuesL);
                    },
                    error: function () {
                        console.log('Error fetching data from the web service.');
                    }
                });
            }

            function createBarChart(categorizeM, valuesL) {
                var ctx = document.getElementById('barChart').getContext('2d');
                return new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: categorizeM,
                        datasets: [{
                            label: 'Jumlah Akaun Belum Terima',
                            data: valuesL,
                            backgroundColor: 'rgba(75, 192, 192, 0.2)', // Bar color
                            borderColor: 'rgba(75, 192, 192, 1)', // Border color
                            borderWidth: 1
                        }]
                    },
                    options: {
                         maintainAspectRatio: false,
                        /*aspectRatio: 1,*/
                        //animation: {
                        //    duration: 1000,
                        //    easing: 'easeInOutQuart',
                        /*},*/
                        scales: {
                            y: {
                                animation: {
                                    duration: 2000,
                                    easing: 'easeInOutQuart',
                                },
                                beginAtZero: true,
                                //min: 0,
                                //max: 1400000,
                                //ticks: {
                                //    // forces step size to be 50 units
                                //    stepSize: 100000,
                                //    callback: function (value) {
                                //        return value.toLocaleString();
                                //    }
                                //}
                            }
                        },
                        responsive: true,
                        plugins: {
                            legend: {
                                display: true,
                                position: 'top'
                            }
                        },
                    }
                });
            }
        </script>
    </div>
</asp:Content>
