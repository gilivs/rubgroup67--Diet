var dateformat = "DD-MM-YYYY";
function getWeekDaysByWeekNumber(weeknumber) {
    var startDate = moment('2019/01/01');
    var date = startDate.isoWeek(weeknumber || 1).startOf("week"), weeklength = 7, result = [];
    while (weeklength--) {
        result.push(date.format(dateformat));
        date.add(1, "day")
    }
    return result;
}


var allMeals = [];
var categories = [];
var selectedDay;
var selectedWeek;
$(document).ready(function () {


    //לא לשכוח להחזיר את ההתחברות
    //User = JSON.parse(localStorage.getItem('User'));
    //if (User.roleId != "2") {
    //    location.href = "LoginAdminUsers.html";
    //}

    //alert("load");
    for (var i = 1; i <= 54; i++) {
        $('#ddlWeek').append(`<option>${i}</option>`)
    }

    $('#ddlWeek').change(function () {
        selectedWeek = $(this).val();
        //var startDate = moment().week(weekNum).startOf('week');
        var weekDays = getWeekDaysByWeekNumber(selectedWeek);
        let lis = $('#daysTab').find('li');

        for (var i = 0; i < lis.length; i++) {
            $(lis[i]).find('span.date').text(weekDays[i]);
        }

        $('#daysTab').find('li:first').find('a').click();


    })
    ajaxCall("GET", "../api/meals", "", function (meals) {
        allMeals = meals;
        for (var i = 0; i < meals.length; i++) {
            $('#ddlMeals').append(`<option value="${meals[i].Id}">${meals[i].Name}-${meals[i].Description}</option>`)
        }
    }, function () { });

    $('#daysTab').find('li').click(function () {
        selectedDay = $(this).attr('day');

        ajaxCall("GET", "../api/DayMeals/" + selectedDay + "/week/" + selectedWeek, "", function (dayMeals) {
            var $container = $('#itemsEdit');
            $container.empty();

            dayMeals.forEach(function (c) {
                $container.append(

                    `<div class="input-group mb-3 meal-item">
                      <input type="text" readonly  value="${c.Name}-${c.Description}" class="form-control" aria-label="" aria-describedby="basic-addon2">
                            <div class="input-group-append">
                                <button class="btn btn-outline-danger removeCat" meal-id="${c.Id}" type="button">X</button>
                            </div>
                        </div>`
                );

            });
        }, function () { });
    });

    $('body').on('click', '.removeCat', function () {

        var mealid = $(this).attr('meal-id');


        ajaxCall("DELETE", "../api/DayMeals/" + mealid + "/" + selectedDay + "/" + selectedWeek, "",
            function () {
                $('#daysTab').find('li a.active').click();

            }, function () {

            });

    });
    $('body').on('click', '.meal-item input', function () {
        var mealId = $(this).parent().find('.removeCat').attr('meal-id');

        ajaxCall("GET", "../api/meal/" + mealId + "/products", "",
            function (products) {
                var $cont = $('#items');
                $cont.empty();
                if (products.length > 0) {
                    products.forEach(function (p) {
                        $cont.append(`<li>${p.NameProduct}</li>`)
                    });
                } else {
                    $cont.append(`<h3 class="text-warning">No items found for the selected category</h3>`);
                }


                $('#productsModal').modal('show');
            }, function () {

            });

    });

    $('#btnAddMeal').click(function () {
        var activeDateArr = $('#daysTab').find('li a.active').find('span.date').text().split('-');
        var d = new Date(activeDateArr[2], activeDateArr[1], activeDateArr[0])
        var mealDayObj = {
            Day: selectedDay,
            Week: selectedWeek,
            MealId: $('#ddlMeals').val(),
            Date: d
        };
        ajaxCall("POST", "../api/DayMeals/", JSON.stringify(mealDayObj), function (meals) {
            $('#daysTab').find('li a.active').click();
        }, function () { });
    });

});