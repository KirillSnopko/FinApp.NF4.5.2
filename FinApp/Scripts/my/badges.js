$(window).on('load', function () {
    $.get("/Depository/Count", {},
        function (counts) {
            $('#dep_count').html(counts['dep_count']);
            $('#credit_count').html(counts['credit_count']);
        }
    );
});