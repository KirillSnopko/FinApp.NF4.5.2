$(document).ready(function () {

    // let b;

    $(window).on('load', function () {
        $.get("/Credit/List", {},
            function (history) {
                var table = $('<table class="table"><tr><th>ID</th><th>Info</th><th>Returned</th><th>Balance owed</th><th>Open date</th><th>Closed date</th><th>Status</th><th>Options</th></tr>');
                $(history).each(function (index, item) {
                    var id = item.id;
                    var info = item.comment;
                    var returned = item.returned;
                    var balanceOwed = item.balanceOwed;
                    var date1 = item.date1;
                    var date2 = item.date2;

                    if (returned != balanceOwed) {
                        table.append('<tr class="table-danger"><td>' + id + '</td><td>' + info + '</td><td>' + returned + '</td><td><strong>' + balanceOwed + '</strong></td > <td>' + date1 + '</td><td>' + date2 + '</td><td>' + getProgress(balanceOwed, returned) + '</td><td>' + button1(id) + '</td></tr >');
                    } else {
                        table.append('<tr class="table-danger"><td>' + id + '</td><td>' + info + '</td><td>' + returned + '</td><td><strong>' + balanceOwed + '</strong></td > <td>' + date1 + '</td><td>' + date2 + '</td><td>' + getProgress(balanceOwed, returned) + '</td><td>' + button2() + '</td></tr >');
                    }
                });
                table.append('</table>');
                $('#credits').html(table);


                $(".delete").click(function (e) {
                    var b = e.target.getAttribute('data-value');
                    document.cookie = 'currentId=' + b;
                });
            }
        );

        /*
                function getId() {
                    return b;
                }
        
        */

        function getProgress(x1, x2) {
            var diff
            if (x2 != 0) {
                diff = x2 / x1 * 100;
            } else {
                diff = 0;
            }
            diff = diff.toFixed(2);
            return '<div class="progress"><div class="progress-bar progress-bar-striped active" role = "progressbar" aria - valuenow=' + diff + 'aria - valuemin="0" aria - valuemax="100" style = "width:' + diff + '%" >' + diff + '%</div ></div>'
        }

        function button1(id) {
            return '<div class="dropdown">' +
                '<button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                ' Settings' +
                ' </button>' +
                '<div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
                ' <a class="dropdown-item" href="#" data-toggle="modal" data-target="#reduce">Reduce</a>' +
                '<button class="dropdown-item delete" href="#" data-toggle="modal" data-target="#delete" id="delete_credit" data-value = "' + id + '" >Delete</button>' +
                ' </div>' +
                '</div>'
        }

        function button2(id) {
            return '<div class="dropdown">' +
                '<button class="btn btn-success dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                ' Settings' +
                ' </button>' +
                '<div class="dropdown-menu" aria-labelledby="dropdownMenuButton">' +
                '<a class="dropdown-item" href="#" data-toggle="modal" data-target="#delete">Delete</a>' +
                ' </div>' +
                '</div>'
        }





    });

});


