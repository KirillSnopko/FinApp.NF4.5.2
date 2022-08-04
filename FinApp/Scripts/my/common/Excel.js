$(document).ready(function () {
    var current_id_depo = window.location.href.split('Details?id=')[1];
    $("#Excel").on("click", function () {
        window.open("/Export/ExportById?idDepository=" + current_id_depo);
    });
});