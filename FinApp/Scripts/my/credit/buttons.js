$(document).ready(function () {

    $("#button_create_credit").click(function () {
        var value = document.create_credit.value;
        var comment = document.create_credit.comment;
        var date = document.create_credit.closeDate;

        if (!typeof value.value == 'number' || value.value <= 0) {
            document.getElementById('err_create_credit').innerHTML = "please, write correct value";
            value.focus();
        } else if (comment.value.trim() == "" || comment.value == null) {
            document.getElementById('err_create_credit').innerHTML = "please, write correct comment";
            comment.focus();
        } else if (date.value == "") {
            document.getElementById('err_create_credit').innerHTML = "please, write correct date";
            date.focus();
        } else {
            var token = $('input[name="__RequestVerificationToken"]', create_credit).val();
            $.post("../Credit/Create",
                {
                    value: value.value,
                    __RequestVerificationToken: token,
                    comment: comment.value,
                    closeDate: date.value
                },
                function (status) {
                    if (status['status'] == 200) {
                        location.reload();
                    } else {
                        document.getElementById('err_create_credit').innerHTML = "Server error" + "\nStatus: " + status['status'] + "\nMessage: " + status['message'];
                    }
                }
            );
        }
    });


    $("#button_delete_credit").click(function () {
        var id = $('#idCredit_delete').val();
        var token = $('input[name="__RequestVerificationToken"]', delete_credit).val();
        $.post("/Credit/Delete",
            {
                idCredit: id,
                __RequestVerificationToken: token,
            },
            function (status) {
                if (status['status'] == 200) {
                    location.reload();
                    $('#idCredit_delete').val("");
                } else {
                    document.getElementById('err_delete_credit').innerHTML = status['message'];
                }
            });
    });

    $("#button_reduce_credit").click(function () {
        var id = $('#idCredit_reduce').val();
        var value = document.reduce_credit.value;
        var comment = document.reduce_credit.comment;
        var token = $('input[name="__RequestVerificationToken"]', reduce_credit).val();

        if (!typeof value.value == 'number' || value.value <= 0) {
            document.getElementById('err_reduce_credit').innerHTML = "please, write correct value";
            value.focus();

        } else if (comment.value.trim() == "" || comment.value == null) {
            document.getElementById('err_reduce_credit').innerHTML = "please, write correct comment";
            comment.focus();
        }
        else {
            $.post("/Credit/Reduce",
                {
                    idCredit: id,
                    value: value.value,
                    comment: comment.value,
                    __RequestVerificationToken: token,
                },
                function (status) {
                    if (status['status'] == 200) {
                        location.reload();
                        $('#idCredit_reduce').val("");
                    } else {
                        document.getElementById('err_reduce_credit').innerHTML = status['message'];
                    }
                });
        }
    });
});
