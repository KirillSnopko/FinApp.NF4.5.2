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
        $.get("/api/Charts/Spending/AllDepository/CurMonth", {},
            function (data) {
                const labelsDoughnut = [];
                const dataDoughnut = [];
                for (var i = 0; i < data.length; i++) {
                    labelsDoughnut.push(data[i].Depository);
                    dataDoughnut.push(data[i].Sum);
                }

                if (dataDoughnut.length == 0) {
                    $(chart1).html("<kbd>Chart (Expenses). No data for current month</kbd>");
                }
                else {
                    new Chart(Sp_CurMonth, {
                        type: 'doughnut',
                        data: {
                            labels: labelsDoughnut,
                            datasets: [{
                                label: '',
                                data: dataDoughnut,
                                backgroundColor: colors,
                                borderColor: colors,
                                borderWidth: 2
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
                }
            });

        $.get("/api/Charts/Addition/AllDepository/CurMonth", {},
            function (data) {
                const labelsDoughnut = [];
                const dataDoughnut = [];
                for (var i = 0; i < data.length; i++) {
                    labelsDoughnut.push(data[i].Depository);
                    dataDoughnut.push(data[i].Sum);
                }
                if (dataDoughnut.length == 0) {
                    $(chart2).html("<kbd>Chart (Addition).No data for current month</kbd>");
                } else {
                    new Chart(Add_CurMonth, {
                        type: 'doughnut',
                        data: {
                            labels: labelsDoughnut,
                            datasets: [{
                                label: '',
                                data: dataDoughnut,
                                backgroundColor: colors,
                                borderColor: colors,
                                borderWidth: 2
                            }]
                        },
                        options: {
                            //cutoutPercentage: 40,
                            responsive: false,
                            plugins: {
                                title: {
                                    display: true,
                                    text: 'Addition for the current month'
                                }
                            }
                        }
                    });
                }
            });
    })

    $.get("/api/Charts/Spending/AllDepository/AllTime", {},
        function (data) {
            const labelsDoughnut = [];
            const dataDoughnut = [];
            for (var i = 0; i < data.length; i++) {
                labelsDoughnut.push(data[i].Depository);
                dataDoughnut.push(data[i].Sum);
            }

            if (dataDoughnut.length == 0) {
                $(chart3).html("<kbd>Chart (Expenses). No data for all time</kbd>");
            }
            else {
                new Chart(Sp_AllTime, {
                    type: 'doughnut',
                    data: {
                        labels: labelsDoughnut,
                        datasets: [{
                            label: '',
                            data: dataDoughnut,
                            backgroundColor: colors,
                            borderColor: colors,
                            borderWidth: 2
                        }]
                    },
                    options: {
                        //cutoutPercentage: 40,
                        responsive: false,
                        plugins: {
                            title: {
                                display: true,
                                text: 'Expenses for all time'
                            }
                        }
                    }
                });
            }
        });

    $.get("/api/Charts/Addition/AllDepository/AllTime", {},
        function (data) {
            const labelsDoughnut = [];
            const dataDoughnut = [];
            for (var i = 0; i < data.length; i++) {
                labelsDoughnut.push(data[i].Depository);
                dataDoughnut.push(data[i].Sum);
            }
            if (dataDoughnut.length == 0) {
                $(chart4).html("<kbd>Chart (Addition).No data for all time</kbd>");
            } else {
                new Chart(Add_AllTime, {
                    type: 'doughnut',
                    data: {
                        labels: labelsDoughnut,
                        datasets: [{
                            label: '',
                            data: dataDoughnut,
                            backgroundColor: colors,
                            borderColor: colors,
                            borderWidth: 2
                        }]
                    },
                    options: {
                        //cutoutPercentage: 40,
                        responsive: false,
                        plugins: {
                            title: {
                                display: true,
                                text: 'Addition for all time'
                            }
                        }
                    }
                });
            }
        });
})

