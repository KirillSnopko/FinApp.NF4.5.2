$(document).ready(function () {
    var current_id_depo = window.location.href.split('Details?id=')[1];
    var name;
    var value;
    var currency;

    $(window).on('load', function () {
        $.get("/Depository/GetById?id=" + current_id_depo, {},
            function (data) {
                name = data.name;
                value = data.value;
                currency = data.currency;
                if (value < 20) {
                    $('#titleDep').html('<h2 class="d-inline p-2">' + name + '</h2>' + '<span class="badge badge-danger">' + value + currency + '</span>');
                } else {
                    $('#titleDep').html('<h2 class="d-inline p-2">' + name + '</h2>' + '<span class="badge badge-success">' + value + currency + '</span>');
                }

                document.rename_depo.name.placeholder = name;

                $('#select_category').html(
                    '<label for="exampleFormControlSelect1">Category </label>' +
                    '<select class="form-control" id="exampleFormControlSelect1" name="category">' +
                    '<option> Addition </option>' +
                    ' </select > ');

                $('#operation_footer').html('<h5>In stock ' + value + currency + '</h5>');




            }
        );

        $('#customRadio2').click(function () {
            $('#select_category').html(
                '<label for="exampleFormControlSelect1">Category </label>' +
                '<select class="form-control" id="exampleFormControlSelect1" name="category">' +
                '<option> Home </option>' +
                '<option> Repair </option>' +
                '<option>Supermarkets</option>' +
                '<option>Pharmacy</option>' +
                '<option>Entertainment</option>' +
                '<option>Transport</option>' +
                '<option>Clothing</option>' +
                '<option>Electronics</option>' +
                '<option>Others</option></select > ')
        });

        $('#customRadio').click(function () {
            $('#select_category').html('');
            $('#select_category').html(
                '<label for="exampleFormControlSelect1">Category </label>' +
                '<select class="form-control" id="exampleFormControlSelect1" name="category">' +
                '<option> Addition </option>' +
                ' </select > ')
        });




        //rename
        $("#button_rename_depo").click(function () {
            var name = document.rename_depo.name;
            var token = $('input[name="__RequestVerificationToken"]', rename_depo).val();


            if (name.value.trim() == "" || name.value == null) {
                document.getElementById('err_rename_depo').innerHTML = "please, write correct name";
                name.focus();
            } else {

                $.post("../Depository/Rename",
                    {
                        id: current_id_depo,
                        name: name.value,
                        __RequestVerificationToken: token
                    },
                    function (status) {
                        if (status['status'] == 200) {
                            location.reload();
                        } else {
                            document.getElementById('err_rename_depo').innerHTML = "Server error" + "\nStatus: " + status['status'] + "\nMessage: " + status['message'];
                        }
                    }
                );
            }
        });

        //new operation

        $("#button_operation_depo").click(function () {
            var value = document.new_operation.amountOfMoney;
            var comment = document.new_operation.comment;
            var flag = document.new_operation.isSpending;
            var category;

            category = document.new_operation.category;

            var token = $('input[name="__RequestVerificationToken"]', new_operation).val();


            if (!typeof value.value == 'number' || value.value <= 0) {
                document.getElementById('err_new_operation').innerHTML = "please, write correct value";
                value.focus();
            } else if (comment.value.trim() == "" || comment.value == null) {
                document.getElementById('err_new_operation').innerHTML = "please, write correct comment";
                comment.focus();
            } else {

                $.post("../Depository/Change",
                    {
                        idDepository: current_id_depo,
                        amountOfMoney: value.value,
                        __RequestVerificationToken: token,
                        comment: comment.value,
                        category: category.value,
                        isSpending: flag.value
                    },
                    function (status) {
                        if (status['status'] == 200) {
                            location.reload();
                        } else {
                            document.getElementById('err_new_operation').innerHTML = "Server error" + "\nStatus: " + status['status'] + "\nMessage: " + status['message'];
                        }
                    }
                );
            }
        });









        //delete

        $("#button_delete_depo").click(function () {
            var token = $('input[name="__RequestVerificationToken"]', delete_depo).val();
            $.post("/Depository/Delete",
                {
                    id: current_id_depo,
                    __RequestVerificationToken: token,
                },
                function (status) {
                    if (status['status'] == 200) {
                        location.assign('/Depository/List');
                       // window.open('/Depository/List');
                    } else {
                        document.getElementById('err_delete_depo').innerHTML = status['message'];
                    }
                });
        });


    });
});
