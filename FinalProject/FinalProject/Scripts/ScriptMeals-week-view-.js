$(document).ready(function () {


    for (var i = 1; i <= 54; i++) {
        $('#ddlWeek').append(`<option>${i}</option>`)
    }

    $('#ddlWeek').change(function () {
        selectedWeek = $(this).val();

        ajaxCall("GET", "../api/GetMealViewByWeek/" + selectedWeek, "", function (res) {
            var weekDays = res;

            var mealsDS = {};
            for (var i = 0; i < 6; i++) {
                mealsDS['m' + i] = [];

                for (var j = 0; j < 7; j++) {
                    mealsDS['m' + i][j] = weekDays[j].WeekMeals[i].Products;
                }
            }

            $('.td-cell').remove();
            var tdStr;
            for (var i = 0; i < 6; i++) {
                tdStr = '';

                for (var j = 0; j < 7; j++) {
                    var products = mealsDS['m' + i][j]
                    tdStr += '<td class="td-cell"><div class="prod-cell">';
                    products.forEach(function (p) {
                        tdStr += `<span>${p.NameProduct}</span>`;
                    });
                    tdStr += '</div></td>';
                }
                $('#tr' + (i + 1)).append(tdStr);
            }

        }, function (err) { });

    });
});