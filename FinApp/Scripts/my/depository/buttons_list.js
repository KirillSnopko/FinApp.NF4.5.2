$(document).ready(function () {
    $(window).on('load', function () {

       
        //Create
        $("#button_create_depo").click(function () {
            var tDep = document.create.tDep;
            var tMoney = document.create.tMoney;
            var name = document.create.name;
            var amount = document.create.amount;
            var token = $('input[name="__RequestVerificationToken"]', create).val();


            if (!typeof amount.value == 'number' || amount.value <= 0) {
                document.getElementById('err_create').innerHTML = "please, write correct value";
                amount.focus();
            } else if (name.value.trim() == "" || name.value == null) {
                document.getElementById('err_create').innerHTML = "please, write correct name";
                name.focus();
            } else {

                $.post("../Depository/Create",
                    {
                        tDep: tDep.value,
                        tMoney: tMoney.value,
                        name: name.value,
                        amount: amount.value,
                        __RequestVerificationToken: token
                    },
                    function (status) {
                        if (status['status'] == 200) {
                            location.reload();
                        } else {
                            document.getElementById('err_create').innerHTML = "Server error" + "\nStatus: " + status['status'] + "\nMessage: " + status['message'];
                        }
                    }
                );
            }
        });


    });
});
