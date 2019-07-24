

var patients = [];
google.charts.load('current', { 'packages': ['corechart', 'gauge'] });
function drawChart(numHealthy, numNotHelthy) {

    // Create the data table.
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Patients');
    data.addColumn('number', 'Slices');
    data.addRows([
        ['מצב תקין', numHealthy],
        ['מצב חריג', numNotHelthy]


    ]);

    // Set chart options
    var options = {
        'title': 'מצב המטופלים',
        width: 50, height: 500, 
        pieHole: 0.5
    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.PieChart(document.getElementById('chart_div'));
    chart.draw(data, options);
}

//ריק אוכל
function drawEmptyGaugeEating(eatting) {

    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['אכילה', eatting]
     
    ]);

    var options = {
        width: 100, height: 100, 
        minorTicks: 5,
       
        min: 0,
        max: 50
    };

    var chartEat = new google.visualization.Gauge(document.getElementById('gaugeEat'));

    chartEat.draw(data, options);
}
//מלא אוכל 

function drawGaugeEatingStatus(eatting) {

    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['אכילה', eatting]

    ]);

    var options = {
        width: 100, height: 100,
        minorTicks: 5,
        redFrom: 0, redTo: 20,
        yellowFrom: 21, yellowTo: 24,
        greenFrom: 25,
        greenTo: 50,
        redColor: "#fa8072",
        greenColor: "#a8e4a0",
        yellowColor: "#ffdead",
        min: 0,
        max: 50
    };

    var chartEatStatus = new google.visualization.Gauge(document.getElementById('gaugeEat'));

    chartEatStatus.draw(data, options);
}

//ריק דם
function drawEmptyGaugeBlood(blood) {

    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['בדיקות דם', blood]
    ]);

    var options = {
        width: 100, height: 100,
        minorTicks: 5,

        min: 0,
        max: 50
    };

    var chartBlood = new google.visualization.Gauge(document.getElementById('gaugeBlood'));

    chartBlood.draw(data, options);
}
//מלא דם
function drawGaugeBloodStatus(blood) {

    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['בדיקות דם', blood]
    ]);

    var options = {
        width: 100, height: 100,
        minorTicks: 5,
        redFrom: 0, redTo: 20,
        yellowFrom: 21, yellowTo: 24,
        greenFrom: 25,
        greenTo: 50,
        redColor: "#fa8072",
        greenColor: "#a8e4a0",
        yellowColor: "#ffdead",
        min: 0,
        max: 50
     
    };

    var chartBloodStatus = new google.visualization.Gauge(document.getElementById('gaugeBlood'));

    chartBloodStatus.draw(data, options);
}
//ריק bmi
function drawEmptyGaugeBMI(bmi) {

    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['BMI', bmi]

    ]);

    var options = {
        width: 100, height: 100,
        minorTicks: 5,
 
        min: 0,
        max: 100
     
    };

    var chartBMIStatus = new google.visualization.Gauge(document.getElementById('gaugeBMI'));

    chartBMIStatus.draw(data, options);
}
//מלא BMI
function drawGaugeBMIStatus(bmi) {

    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['BMI', bmi]

    ]);

    var options = {
        width: 100, height: 100,
        minorTicks: 5,
        redFrom: 0, redTo: 50,
        greenFrom: 50,
        greenTo: 100,
        redColor: "#fa8072",
        greenColor: "#a8e4a0",
        yellowColor: "#ffdead",
        min: 0,
        max: 100

    };

    var chartBMIStatus = new google.visualization.Gauge(document.getElementById('gaugeBMI'));

    chartBMIStatus.draw(data, options);
}
//function drawEmptyGauge(eatting, blood, bmi) {

//    var data = google.visualization.arrayToDataTable([
//        ['Label', 'Value'],
//        ['אכילה', eatting],
//        ['BMI', bmi],
//        ['בדיקות דם', blood]
//    ]);

//    var options = {
//        width: 100, height: 100,
//        minorTicks: 5,

//        min: 0,
//        max: 50
//    };

//    var chart2 = new google.visualization.Gauge(document.getElementById('gauge_div'));

//    chart2.draw(data, options);
//}

function drawGauge(eatting, blood, bmi) {

    var data = google.visualization.arrayToDataTable([
        ['Label', 'Value'],
        ['אכילה', eatting],
        //['BMI', bmi],
        ['בדיקות דם', blood]
    ]);

    var options = {
        width: 200, height: 120,
        redFrom: 0, redTo: 20,
        yellowFrom: 21, yellowTo: 24,
        minorTicks: 5,
        greenFrom: 25,
        greenTo: 50,
        redColor: "#fa8072",
        greenColor: "#a8e4a0",
        yellowColor: "#ffdead",
        min: 0,
        max: 50,
        forceIFrame: true
    };

    var chart = new google.visualization.Gauge(document.getElementById('gauge_div'));

    chart.draw(data, options);
}

//function drawGaugeBMIStatus(bmi) {

//    var data = google.visualization.arrayToDataTable([
//        ['Label', 'Value'],
//        ['BMI', bmi]
//    ]);

//    var options = {
//        redFrom: 0, redTo: 50,
//        //yellowFrom: 21, yellowTo: 24,
//        minorTicks: 5,
//        greenFrom: 50,
//        greenTo: 100,
//        redColor: "#fa8072",
//        greenColor: "#a8e4a0",
//        yellowColor: "#ffdead",
//        min: 0,
//        max: 100,
//        forceIFrame: true
//    };

//    var chart1 = new google.visualization.Gauge(document.getElementById('gauge_divbmi'));

//    chart1.draw(data, options);
//}

function blockUiActive() {
    $.blockUI({
        message: 'Loading Next Project',
        css: {
            border: 'none',
            padding: '15px',
            backgroundColor: '#000',
            '-webkit-border-radius': '10px',
            '-moz-border-radius': '10px',
            opacity: '.5',
            color: '#fff',
            fontSize: '18px',
            fontFamily: 'Verdana,Arial',
            fontWeight: 200
        }
    });

}

$(document).ready(function () {
    var bmi = 50;
    var blood = 25;
    var eating = 25;
    drawEmptyGaugeBMI(bmi);
    drawEmptyGaugeEating(eating);
    drawEmptyGaugeBlood(blood);

    var d = new Date();
    var year = d.getFullYear();
    ajaxCall("GET", "../api/DayMeals/?Year=" + year, "", successGetDates, errorGetDate);


    //לא לשכוח להוריד מהערה
    User = JSON.parse(localStorage.getItem('User'));
    if (User.roleId != "1") {
        console.log(User);
        location.href = "LoginAdminUsers.html";
    }
    //$('#userSpan').text(User.NumUser);
    //$('#menuUserSpan').text(Num.User);
    //blockUiActive();

    ajaxCall("GET", "../api/GetDashboardData", "", function (res) {
        //     $.unblockUI();
        patients = res.Patients;
        console.log(patients);
        var numHealthy = 0;
        var numNotHelthy = 0;
        var reg = 0;
        var blender = 0;
        var soft = 0;
        var $badTbl = $('#selectBad');
        var $goodTbl = $('#selectGood');

        patients.forEach(function (u) {
            var sum = u.Status.BMI + u.Status.Bloode + u.Status.Eatting;
            if (sum < 60) {
                $badTbl.append(`<option value=` + u.Id + `>` + u.FirstName + " " + u.LastName + `</option>`);
                numNotHelthy++;
            } else {

                $goodTbl.append(`<option value=` + u.Id + `>` + u.FirstName + " " + u.LastName + `</option>`);
                numHealthy++;

            }


            if (u.Kind0fTextureFood === 'רגיל') {
                reg++;
            }
            else if (u.Kind0fTextureFood === 'רך') {
                soft++;
            }
            else blender++;


        });
        var sumPatients = patients.length;
        $('#sumPatients').text(sumPatients);
        var goodPercent = (numHealthy / sumPatients) * 100;
        var badPercent = (numNotHelthy / sumPatients) * 100;


        $('#goodTitle').text(goodPercent.toFixed(1) + "%");
        $('#badTitle').text(badPercent.toFixed(1) + "%");


        $('#selectGood').on('change', function () {
            val = $('#selectGood').val();
            $('#selectBad').val(2);

            showMeter(val);
        });

        $('#selectBad').on('change', function () {
            val = $('#selectBad').val();
            $('#selectGood').val(1);
            $('#btnProfile').show();

            showMeter(val);
        });

        $("#btnProfile").click(function () {

            var optValGood = $('#selectGood').val();
            var optValBad = $('#selectBad').val();

            patients.forEach(function (p) {

                if (p.Id === optValGood) {

                    localStorage.setItem('pat', JSON.stringify(p));
                    window.location.href = "profile.html";
                }
                else if (p.Id === optValBad) {

                    localStorage.setItem('pat', JSON.stringify(p));
                    window.location.href = "profile.html";
                }
            });
        });

        var ctx = document.getElementById('myChart').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: ['תקין', 'חריג'],
                datasets: [{
                    label: '# of Votes',
                    data: [numHealthy, numNotHelthy],
                    backgroundColor: [
                        "rgb(168, 228, 160,0.5)",
                        "rgb(250,143,131,0.6)"

                        //'rgba(75, 192, 192, 0.2)',
                        //'rgba(255, 99, 132, 0.2)'

                    ],
                    borderColor: [
                        "rgb(168, 228, 160,1)",
                        "rgb(250,143,131,1)"
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                   
                }
            }
        });


        var ctx1 = document.getElementById('barChartFood');
        var myChart1 = new Chart(ctx1, {
            type: 'bar',
            data: {
                labels: ['רגיל', 'רך', 'בלנדר'],
                datasets: [{
                    label: 'מרקם מזון',
                    data: [reg, soft, blender],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        //'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
        //var currentTime = new Date()
        //var year = currentTime.getFullYear();
        //תפריט
       
        //function getDateOfISOWeek(w, y) {
        //    var d = (1 + (w - 1) * 7); // 1st of January + 7 days for each week

        //    return new Date(y, 0, d);
        //}

        //$('#ddlWeek').change(function () {
        //    selectedWeek = $(this).val();

        //    ajaxCall("GET", "../api/GetMealViewByWeek/" + selectedWeek, "", function (res) {

        //        var weekDays = res;
        //        console.log(weekDays);
        //        var mealsDS = {};
        //        for (var i = 0; i < 6; i++) {
        //            mealsDS['m' + i] = [];

        //            for (var j = 0; j < 7; j++) {
        //                mealsDS['m' + i][j] = weekDays[j].WeekMeals[i].Products;
        //            }
        //        }

        //        $('.td-cell').remove();
        //        var tdStr;
        //        for (var i = 0; i < 6; i++) {
        //            tdStr = '';

        //            for (var j = 0; j < 7; j++) {
        //                var products = mealsDS['m' + i][j]
        //                tdStr += '<td class="td-cell"><div class="prod-cell">';
        //                products.forEach(function (p) {
        //                    tdStr += `<span>${p.NameProduct}</span>`;
        //                });
        //                tdStr += '</div></td>';
        //            }
        //            $('#tr' + (i + 1)).append(tdStr);
        //        }
        //    });
        //});



            // Set a callback to run when the Google Visualization API is loaded.
       google.charts.setOnLoadCallback(drawChart(numHealthy, numNotHelthy));
    }, function (err) {
            alert('server error');
            window.location.href ='pages-error-500-nutrition.html';
        });
        //});
});

function successGetDates(data) {
    console.log(data);
    dateList = data;
    var week = 0;

    check = dateList[1].DateToWeek;
    console.log('check: ' + check);
    for (var i = 0; i < dateList.length; i++) {
        week++;
        var date = dateList[i].DateToWeek;
        $('#ddlWeek').append(`<option value=` + week + `>` + date + " - " + dateList[i].Week + `</option>`);

    }
    Date.prototype.getWeek = function () {
        var onejan = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((((this - onejan) / 86400000) + onejan.getDay() + 1) / 7);
    }

    var today = new Date();
    var weekNumber = today.getWeek();
    console.log(weekNumber);
    $("#ddlWeek").val(weekNumber);
    selectedWeek = $("#ddlWeek").val();


    ajaxCall("GET", "../api/GetMealViewByWeek/" + selectedWeek, "", successGetMenu, errorGetMenu);


    $('#ddlWeek').change(function () {
        selectedWeek = $(this).val();

        ajaxCall("GET", "../api/GetMealViewByWeek/" + selectedWeek, "", successGetMenu, errorGetMenu);
    });
}

function errorGetDate() {
    console.log('err get dates');
}

function successGetMenu(res) {
    var weekDays = res;
    console.log(weekDays);
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
}

function errorGetMenu() {
    alert('error get menu');
}
    function showMeter(id) {
        var patient = patients.find(function (p) {
            return p.Id === id;

        });

        //$('#userGauge').text(`${patient.FirstName} ${patient.LastName}`);

        drawGaugeEatingStatus(patient.Status.Eatting);
        drawGaugeBMIStatus(patient.Status.BMI);
        drawGaugeBloodStatus(patient.Status.Bloode);


    }




    //var ctx = document.getElementById('userGauge').getContext('2d');
    //var myChart = new Chart(ctx, {
    //    type: 'doughnut',
    //    data: {
    //        //labels: ['Red',  'Green', 'Purple', 'Orange'],
    //        datasets: [{
    //            label: '# of Votes',
    //            data: [0,10,5,2,20],
    //            backgroundColor: [
    //                'rgba(0, 99, 132)',
    //                'rgba(255, 99, 132, 0.5)',
    //                'rgba(255, 159, 64, 0.5)',
    //                'rgba(75, 192, 192, 0.5)'
                    
    //            ],
    //            borderColor: [
    //                //'rgba(255, 99, 132, 1)',
    //                //'rgba(54, 162, 235, 1)',
    //                //'rgba(255, 206, 86, 1)',
    //                //'rgba(75, 192, 192, 1)',
    //                //'rgba(153, 102, 255, 1)',
    //                //'rgba(255, 159, 64, 1)'
    //            ],
    //            borderWidth: 0
    //        }]
    //    },
    //    options: {

    //        circumference: 1 * Math.PI,
    //        rotation: 1 * Math.PI,
    //        cutoutPercentage: 80
    //    }
    //});


