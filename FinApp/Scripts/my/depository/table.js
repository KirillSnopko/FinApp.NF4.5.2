$(document).ready(function () {

    $(window).on('load', function () {
        $.get("/Depository/GetData", {},
            function (credits) {
                var table = $('<table class="table table-striped"><tr><th>Name</th><th>Type</th><th>Value</th><th>Currency</th><th></th></tr>');
                $(credits).each(function (index, item) {
                    var id = item.id;
                    var name = item.name;
                    var type = item.type;
                    var value = item.value;
                    var currency = item.currency;

                    table.append('<tr class="table-default"><td>' + name + '</td><td>' + type + '</td><td><strong>' + value + '</strong></td > <td>' + currency + '</td><td>' + button(id) + '</td></tr >');
                });
                table.append('</table>');
                $('#depositories').html(table);


                $(".delete").click(function (e) {
                    var b = e.target.getAttribute('data-value');
                    $('#idCredit_delete').val(b);
                });
                $(".reduce").click(function (e) {
                    var b = e.target.getAttribute('data-value');
                    $('#idCredit_reduce').val(b);
                });
                $(".history").click(function (e) {
                    var b = e.target.getAttribute('data-value');

                    $.get("/Credit/HistoryById?id=" + b, {},
                        function (history) {
                            var table = $('<table class="table"><tr><th>Date</th><th>Info</th><th>Returned</th></tr>');
                            $(history).each(function (index, item) {
                                var date = item.date;
                                var info = item.comment;
                                var returned = item.value;
                                table.append('<tr class="table-default"><td>' + date + '</td><td>' + info + '</td><td><strong>' + returned + '</strong></td ></tr >');

                            });
                            table.append('</table>');
                            $('#credit_history').html(table);
                        });

                }
                );
            }
        );

        function button(id) {
            return '<a href="/Depository/Details?id=' + id + '" class = "btn btn-secondary" >Details</a>';
        }

    });
});