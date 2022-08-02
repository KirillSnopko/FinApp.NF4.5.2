$(document).ready(function () {
    const colors = [
        'rgba(255, 99, 132)',
        'rgba(54, 162, 235)',
        'rgba(255, 206, 86)',
        'rgba(75, 192, 192)',
        'rgba(102, 54, 255)',
        'rgba(46, 85, 183)',
        'rgba(190, 30, 80)',
        'rgba(190, 30, 199)',
        'rgba(26, 15, 49)'
    ];

    $(window).on('load', function () {
        var current_id_depo = window.location.href.split('Details/')[1];

        $.get('/api/Charts/Spending/CurrentDepository/CurrentMonth/' + current_id_depo, {},
            function (data) {
                const labelsDoughnut = [];
                const dataDoughnut = [];
                for (var i = 0; i < data.length; i++) {
                    labelsDoughnut.push(data[i].Category);
                    dataDoughnut.push(data[i].Sum);
                }
                if (dataDoughnut.length == 0) {
                    $(doughnut).html('<kbd>Chart. No data for current month</kbd>');
                }

                new Chart(myChartDoughnut, {
                    type: 'doughnut',
                    data: {
                        labels: labelsDoughnut,
                        datasets: [{
                            label: '# of Tomatoes',
                            data: dataDoughnut,
                            backgroundColor: colors,
                            borderColor: colors,
                            borderWidth: 1
                        }]
                    },
                    options: {
                        //cutoutPercentage: 40,
                        responsive: false,
                        plugins: {
                            title: {
                                display: true,
                                text: 'Expenses for the current month'
                            }
                        }
                    }
                });
            });
    })
})