$(document).ready(function () {

    $("#button_rename_user").click(function () {
        var name = document.rename_user.name;
        if (name.value.trim() === "") {
            document.getElementById('err_rename_user').innerHTML = "please, write name";
            name.focus();
        } else {
            var token = $('input[name="__RequestVerificationToken"]', rename_user).val();
            $.post("../Account/Rename",
                {
                    name: name.value,
                    __RequestVerificationToken: token,
                },
                function (status) {
                    if (status['status'] == 200) {
                        location.reload();
                    } else {
                        document.getElementById('err_rename_user').innerHTML = "Server error" + "\nStatus: " + status['status'] + "\nMessage: " + status['message'];
                    }
                });
        }
    });

    $("#button_ch_pass").click(function () {
        var old_pass = document.change_password.old_password;
        var new_pass = document.change_password.new_password;
        if (old_pass.value.trim() === "") {
            document.getElementById('err_ch_pass').innerHTML = "please, write current password";
            old_pass.focus();
        }
        else if (new_pass.value.trim() === "") {
            document.getElementById('err_ch_pass').innerHTML = "please, write new password";
            new_pass.focus();
        }
        else {
            var token = $('input[name="__RequestVerificationToken"]', change_password).val();
            $.post("../Account/ChangePassword",
                {
                    old_password: old_pass.value,
                    new_password: new_pass.value,
                    __RequestVerificationToken: token,
                },
                function (status) {
                    if (status['status'] == 200) {
                        location.reload();
                    } else {
                        document.getElementById('err_ch_pass').innerHTML = "Server error" + "\nStatus: " + status['status'] + "\nMessage: " + status['message'];
                    }
                });
        }
    });

    $("#button_remove_user").click(function () {
        var password = document.remove_user.password;
        if (password.value.trim() === "") {
            document.getElementById('err_remove_user').innerHTML = "please, write password";
            password.focus();
        } else {
            var token = $('input[name="__RequestVerificationToken"]', rename_user).val();
            $.post("../Account/Remove",
                {
                    password: password.value,
                    __RequestVerificationToken: token,
                },
                function (status) {
                    if (status['status'] == 200) {
                        location.reload();
                    } else {
                        document.getElementById('err_remove_user').innerHTML = status['message'];
                    }
                });
        }
    });

});

