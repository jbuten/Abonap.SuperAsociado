export function showChart() {
    var xValues = [100, 200, 300, 400, 500, 600, 700, 800, 900, 1000];

    new Chart("myChart", {
        type: "line",
        data: {
            labels: xValues,
            datasets: [{
                data: [860, 1140, 1060, 1060, 1070, 1110, 1330, 2210, 7830, 2478],
                borderColor: "red",
                fill: false
            }, {
                data: [1600, 1700, 1700, 1900, 2000, 2700, 4000, 5000, 6000, 7000],
                borderColor: "green",
                fill: false
            }, {
                data: [300, 700, 2000, 5000, 6000, 4000, 2000, 1000, 200, 100],
                borderColor: "blue",
                fill: false
            }]
        },
        options: {
            legend: { display: false }
        }
    });


    var xValues = ["Usuarios", "Fianzas", "Acuerdos"];
    var yValues = [55, 49,  35];
    var barColors = [
        "#b91d47",
        "#00aba9",
        "#2b5797",
    ];

    new Chart("myChart2", {
        type: "doughnut",
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            title: {
                display: false,
                text: ""
            }
        }
    });


	return true;
}




export function showChart1(data) {
    //console.log(data[0].values1);
    var data1 = JSON.stringify(data);
    //console.log(data1);
    var data2 = JSON.parse(data);
  
    //console.log(data2);




    const data5 = {
        labels: data2.Meses,
        datasets: [{
            label: `Empresa: ${data2.Empresa}`,
            data: data2.MesEmpresaUltimoAnyoMontos,
            fill: false,
            borderColor: 'rgb(75, 192, 192)',
            tension: 0.1
        }]
    };
    $("#cardBodyMyChartMontoMensualPorEmpresaSeleccionada").html('<canvas id="myChartMontoMensualPorEmpresaSeleccionada" style="width:100%;"></canvas>');
    new Chart("myChartMontoMensualPorEmpresaSeleccionada", {
        type: "line",
        data: data5

    });

    const data3 = {
        labels: data2.TipoInversiones,
        datasets: [{
            label: 'Tipo Inversion',
            data: data2.TipoInversionValores,
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(255, 159, 64, 0.2)',
                'rgba(255, 205, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(201, 203, 207, 0.2)'
            ],
            borderColor: [
                'rgb(255, 99, 132)',
                'rgb(255, 159, 64)',
                'rgb(255, 205, 86)',
                'rgb(75, 192, 192)',
                'rgb(54, 162, 235)',
                'rgb(153, 102, 255)',
                'rgb(201, 203, 207)'
            ],
            borderWidth: 1
        }]
    };
   

    var xValues = [100, 200, 300, 400, 500, 600, 700, 800, 900, 1000];
    $("#cardBodyMyChartTipoInversion").html('<canvas id="myChartTipoInversion" style="width:100%;"></canvas>');
    new Chart("myChartTipoInversion", {
        type: "bar",
        data: data3,
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
        
    });



    const data4 = {
        labels: data2.Areas,
        datasets: [{
            label: 'Gerencia o Departamento',
            data: data2.AreaValores,
            backgroundColor: [
                'rgb(255, 99, 132)',
                'rgb(54, 162, 235)',
                'rgb(255, 205, 86)',
                'rgb(0, 0, 0)',
                '#0000FF',
                '#A52A2A',
                '#F5F5DC',
                '#8A2BE2',
                '#D2691E',
                '#FF7F50',
                '#6495ED',
                '#FF8C00',
                '#9400D3',
                '#FF1493',
                '#FFD700',
                '#9ACD32',
                '#FFFF00',
                '#F5DEB3',
                '#EE82EE',
                '#40E0D0',
                '#FF6347',
                '#D8BFD8',
                '#008080',
                '#D2B48C',
                '#4682B4',
                '#6A5ACD',
                '#87CEEB',
                '#A0522D',
                '#2E8B57',
                '#FF0000',
                '#800080',
                '#DDA0DD',
                '#FFC0CB',
                '#FFA500',
                '#C71585',
                '#00FA9A',


            ],
            hoverOffset: 4
        }]
    };
    $("#cardBodyMyChartArea").html('<canvas id="myChartArea" style="width:100%;"></canvas>');
    new Chart("myChartArea", {
        type: "pie",
        data: data4
        

    });


    
   


    const data5Bar = {
        labels: data2.Empresas,
        datasets: [{
            label: 'Empresa',
            data: data2.EmpresaValores,
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(255, 159, 64, 0.2)',
                'rgba(255, 205, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(201, 203, 207, 0.2)'
            ],
            borderColor: [
                'rgb(255, 99, 132)',
                'rgb(255, 159, 64)',
                'rgb(255, 205, 86)',
                'rgb(75, 192, 192)',
                'rgb(54, 162, 235)',
                'rgb(153, 102, 255)',
                'rgb(201, 203, 207)'
            ],
            borderWidth: 1
        }]
    };
    $("#cardBodyMyChartBarCantidadPorEmpresa").html('<canvas id="myChartBarCantidadPorEmpresa" style="width:100%;"></canvas>');
    new Chart("myChartBarCantidadPorEmpresa", {
        type: "bar",
        data: data5Bar,
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }

    });


    


    const data5BarMonto = {
        labels: data2.Empresas,
        datasets: [{
            label: 'Empresa',
            data: data2.EmpresaValoresMonto,
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(255, 159, 64, 0.2)',
                'rgba(255, 205, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(153, 102, 255, 0.2)',
                'rgba(201, 203, 207, 0.2)'
            ],
            borderColor: [
                'rgb(255, 99, 132)',
                'rgb(255, 159, 64)',
                'rgb(255, 205, 86)',
                'rgb(75, 192, 192)',
                'rgb(54, 162, 235)',
                'rgb(153, 102, 255)',
                'rgb(201, 203, 207)'
            ],
            borderWidth: 1
        }]
    };
    $("#cardBodyMyChartBarMontoPorEmpresa").html('<canvas id="myChartBarMontoPorEmpresa" style="width:100%;"></canvas>');
    new Chart("myChartBarMontoPorEmpresa", {
        type: "bar",
        data: data5BarMonto,
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }

    });


    //Dona

    var xValues = data2.Estados;
    var yValues = data2.EstadoValores;
    var barColors = [
        "#C0C0C0",
        "#000000",
        "#FF0000",
        "#800000",
        "#FFFF00",
        "#00FF00",


        "#008000",
        "#00FFFF",
        "#008080",
        "#0000FF",
        "#000080",
        "#FF00FF",
        "#800080",
    ];

    $("#cardBodyMyChartEstadoSolicitud").html('<canvas id="myChartEstadoSolicitud" style="width:100%;"></canvas>');
    new Chart("myChartEstadoSolicitud", {
        type: "doughnut",
        data: {
            labels: xValues,
            datasets: [{
                backgroundColor: barColors,
                data: yValues
            }]
        },
        options: {
            events: ['click'],
            title: {
                display: false,
                text: ""
            },
            onClick: function (e) {
                console.log("prueba realizada");
                console.log(e);
                var activePoints = myChart.getElementsAtEvent(e);
                var selectedIndex = activePoints[0]._index;
                console.log(this.data.datasets[0].data[selectedIndex]);
            }
        }
    });


    var barColors1 = [
        "#008000",
        "#C0C0C0",
        
    ];

    $("#cardBodyMyChartPorcentajeFinalizacion").html('<canvas id="myChartPorcentajeFinalizacion" style="width:100%;"></canvas>');
    new Chart("myChartPorcentajeFinalizacion", {
        type: "doughnut",
        data: {
            labels: data2.PorcentajeFinalizado,
            datasets: [{
                backgroundColor: barColors1,
                data: data2.PorcentajeFinalizadoValores
            }]
        },
        options: {
            rotation: 1 * Math.PI,
            circumference: 1 * Math.PI,
     
            title: {
                display: false,
                text: "8"
            }
        }
    });
    return true;
}