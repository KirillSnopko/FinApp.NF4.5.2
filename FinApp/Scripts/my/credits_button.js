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
        var id = $.cookie("currentId");
        // var id = getId();
        var token = $('input[name="__RequestVerificationToken"]', delete_credit).val();
        $.post("/Credit/Delete",
            {
                idCredit: id,
                __RequestVerificationToken: token,
            },
            function (status) {
                $.removeCookie('currentId');
                if (status['status'] == 200) {
                    location.reload();

                } else {
                    document.getElementById('err_delete_credit').innerHTML = status['message'];
                }

            });

    });




});
