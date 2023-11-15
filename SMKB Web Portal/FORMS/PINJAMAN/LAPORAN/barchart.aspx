<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="barchart.aspx.vb" Inherits="SMKB_Web_Portal.barchart" %>

<!DOCTYPE html>

<html>
<script src="<%=ResolveClientUrl("~/chartjs/chartjs4.3.js")%>" type="text/javascript"></script>
<script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-datalabels@2.0.0/dist/chartjs-plugin-datalabels.min.js"></script>
<body>
        <div style ="width:700px; margin:0 auto;">
            <canvas id="chart1">
            </canvas>
        </div>
        

    <script>
        const ctx = document.getElementById('chart1');
        Chart.register(ChartDataLabels);
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: [<%=labl%>],
                datasets: [{
                    //label: 'Statistik Permohonan Pinjaman pada Tahun '[<%=Session("tahunChart")%>],
                    label: 'Statistik Permohonan Pinjaman',
                    data: [<%=val%>],
                    backgroundColor: [
                        'rgb(255, 99, 132)',
                        'rgb(54, 162, 235)',
                        'rgb(255, 205, 86)',
                        'rgb(64,224,208)',
                        'rgb(60,179,113)',
                        'rgb(204,204,0)',
                        'rgb(255,99,71)'

                    ],
                    borderWidth: 1
                }]
            },
            options: {
                title: {
                    display: true,
                    text: 'Modul Pinjaman'
                },
                legend: {
                    display: false
                },
                scales: {
                    x: {
                        grid: {
                            display: false
                        },
                        ticks: {
                            font: {
                                family: 'Century Gothic', // Your font family
                                size: 12,
                            },
                        },
                    },
                    y: {
                        grid: {
                            display: true
                        },
                        beginAtZero: true,
                        ticks: {
                            font: {
                                family: 'Century Gothic', // Your font family
                                size: 12,
                            },
                            // Include a dollar sign in the ticks
                            callback: function (value, index, values) {
                                return value;
                            },
                        },
                    }
                },
                datalabels: { // This code is used to display data values
                    anchor: 'end',
                    align: 'top',
                    formatter: Math.round,
                    font: {
                        weight: 'bold',
                        size: 12,
                        family: 'Century Gothic'
                    }
                }
            }
        });

    </script>
</body>
</html>
