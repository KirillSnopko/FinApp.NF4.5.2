$(document).ready(function () {

    $("#button_login").click(function () {
        var name = document.form_login.name;
        var password = document.form_login.password;
        var token = $('input[name="__RequestVerificationToken"]', form_login).val();
        if (name.value.trim() === "") {
            document.getElementById('err_login').innerHTML = "please, write name";
            name.focus();
        } else if (password.value.trim() == "") {
            document.getElementById('err_login').innerHTML = "please, write password";
            password.focus();
        }
        else {
            $.post("../Account/Login",
                {
                    name: name.value,
                    password: password.value,
                    __RequestVerificationToken: token,
                },
                function (status) {
                    if (status['status'] == 200) {
                        location.reload();
                    } else {
                        document.getElementById('err_login').innerHTML = "Server error" + "\nStatus: " + status['status'] + "\nMessage: " + status['message'];
                    }
                });
        }
    });
});