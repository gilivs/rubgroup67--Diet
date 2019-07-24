$(document).ready(function () {
    $('#btnGenerate').click(function () {
        ajaxCall("GET", "../api/GenerateDashboardData", function (res) {

        }, function (err) { });
    });

});