
$(document).ready(function () {

    $("#button_registr").click(function () {
        var name = document.form_registr.name;
        var email = document.form_registr.email;
        var password = document.form_registr.password;
        var confirm = document.form_registr.confirm;
        var token = $('input[name="__RequestVerificationToken"]', form_registr).val();
        if (name.value.trim() === "") {
            document.getElementById('err_registr').innerHTML = 'please, write name';
            name.focus();
        } else if (email.value.trim() == "") {
            document.getElementById('err_registr').innerHTML = "please, write email";
            email.focus();
        } else if (password.value.trim() == "") {
            document.getElementById('err_registr').innerHTML = "please, write password";
            password.focus();
        } else if (confirm.value.trim() == "") {
            document.getElementById('err_registr').innerHTML = "please, write confirm password";
            confirm.focus();
        } else if (password.value.trim() != confirm.value.trim()) {
            document.getElementById('err_registr').innerHTML = "Passwords do not match";
            confirm.focus();
        }
        else {
            $.post("../Account/Register",
                {
                    name: name.value.trim(),
                    email: email.value.trim(),
                    password: password.value.trim(),
                    __RequestVerificationToken: token,
                },
                function (status) {
                    if (status['status'] == 200) {
                        location.reload();
                    } else {
                        document.getElementById('err_registr').innerHTML = "Server error" + "\nStatus: " + status['status'] + "\nMessage: " + status['message'];
                    }
                });
        }
    });
});