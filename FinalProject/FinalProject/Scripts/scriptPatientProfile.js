$(document).ready(function () {

    p = JSON.parse(localStorage.getItem('pat'));
    //successGetDetails(p);
    var numPatient = p.NumPatient;

    //=== data table ====// 

  //===================//
    var d = new Date();
    var year = d.getFullYear();

    ajaxCall("GET", "../api/DayMeals/?Year=" + year, "", successGetDatesForMenu, errorGetDateForMenu);


    ajaxCall("GET", "../api/GetDashboardDataByPerson/?NumPatient=" + numPatient + " &Height=" + p.Height + " &Weight=" + p.Weight, "", function (res) {
        console.log(res);
        patientD = res;
        var sum = patientD.Status.BMI + patientD.Status.Bloode + patientD.Status.Eatting;
        if (sum < 60) {
            $('#btnStatus').addClass("btn-danger");
        }

        else {
            $('#btnStatus').addClass("btn-success");
        }
    
    }, function (err) {
        alert('server error');
            window.location.href = 'pages-error-500-nutrition.html';
        });
    //======= הערות מטפלים ========//
    ajaxCall("GET", "../api/therapistCommentReport/?NumPatient=" + numPatient, "", successGetComments, errorGetComments);
    //=======הערות למטבח========//
    $('#sendKitchenBTN').click(function () {
        //ajaxCall("POST", "../api/kitchenComments/?NumPatient=" + numPatient, "", successPostKComments, errorPostKComments);
        swal('ההודעה נשלחה בהצלחה!')
    });
    //==== תפריט ====//
    for (var i = 1; i <= 54; i++) {

        //var str = getDateOfISOWeek(i,year);

        $('#ddlWeek').append(`<option>` + i + `</option>`);
    }
    $('#ddlWeek').change(function () {
        selectedWeek = $(this).val();


        ajaxCall("GET", "../api/GetMealViewByWeek/" + selectedWeek, "", function (res) {

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
        });
    });


    function successGetComments(data) {
        console.log(data);


        comments = data; // keep the cars array in a global variable;
    
        $("#jsGrid").jsGrid({
            width: "100%",
            align: "center",
            style: "text-align:right",
           
            filtering: true,
            editing: true,
            sorting: true,
            paging: true,
            autoload: true,

            pageSize: 15,
            pageButtonCount: 5,

            deleteConfirm: "Do you really want to delete the report?",
           
            data: data,
            fields: [
  
                {
                    name: "NameTherapist",
                    type: "text",
                    title: "שם מטפל"



                    //width: 40
                }, {
                    name: "Date",
                    type: "text",
                    title: "תאריך"
                    // width: 40
                }, {


                    name: "FoodComments",
                    type: "text",
                    title: "צריכת מזון",
                    sorting: false

                },

                {
                    name: "Comments",
                    type: "text",
                    title: "הערות",
                    sorting: false
                    // width: 40
                },


                { type: "control" }


            ]
        });

    }


    //function errorGetComments(){
    //    alert('error comments');
    //}
  

 
    //$('dates').on('change', function () {
    //    successGetDate();

    //    });
    ajaxCall("GET", "../api/patients/?NumPatient=" + numPatient, "", successGetDetails, errorGetDetails);
    ajaxCall("GET", "../api/BMI/?NumPatient=" + numPatient, "", successGetBMI, errorGetBMI);
    ajaxCall("GET", "../api/laboratoryTests/?NumPatient" + numPatient, "", successGetDate, errorGetDate);
    ajaxCall("GET", "../api/labRange", "", successGetRange, errorGetRange);
    //ajaxCall("GET", "../api/therapistComment/?NumPatient=" + numPatient, "", successGetComment, errorGetComment);

    //ajaxCall("GET", "../api/productForWeeklyMenu/?MenuNumber=" + menuNumber, "", successGetMenu, errorGetMenu);

    //ajaxCall("GET", "../api/laboratoryTests/?NumPatient=" + numPatient, "", successGetDate, errorGetDate);

    //$('#example23').DataTable({
    //    dom: 'Bfrtip',
    //    buttons: [
    //        'copy', 'csv', 'excel', 'pdf', 'print'
    //    ]
    //});
    //$('.buttons-copy, .buttons-csv, .buttons-print, .buttons-pdf, .buttons-excel').addClass('btn btn-primary mr-1');


  

});

function successPostKComments() {
    alert('success post comments');
}

function errorPostKComments() {
    alert('error post comments');
}
function errorGetComments() {
    alert('error comments');
}

function successGetDatesForMenu(data) {
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
    var sumCalories = 0;
    for (var i = 0; i < 6; i++) {
        tdStr = '';


        for (var j = 0; j < 7; j++) {
            var products = mealsDS['m' + i][j]
            tdStr += '<td class="td-cell"><div class="prod-cell">';
            products.forEach(function (p) {
                tdStr += `<span>${p.NameProduct}</span>`;
                //sumCalories += p.Calories;
            });
            tdStr += '</div></td>';
        }
        $('#tr' + (i + 1)).append(tdStr);
    }
    //console.log('Calories:'+sumCalories);

}

function errorGetMenu() {
    alert('error get menu');
}


function errorGetDateForMenu() {
    console.log('err get dates');
}

function successGetComment(data) {
    $('#example23').DataTable({
        dom: 'Bfrtip',
        buttons: [
            'copy', 'csv', 'excel', 'pdf', 'print'
        ]
    });
    $('.buttons-copy, .buttons-csv, .buttons-print, .buttons-pdf, .buttons-excel').addClass('btn btn-primary mr-1');

}
function errorGetComment() {
    alert("ERROR COmments");
}
function getChartBMI(arrayBMI, arrayDate) {
    var ctx = document.getElementById('myChart').getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: arrayDate,
            datasets: [{
                label: 'BMI',
                borderColor: 'rgb(54, 162, 235)',
                borderWidth: 2,
                fill: false,
                data: arrayBMI
            }]
        },
        options: {
            responsive: true,
            title: {
                display: true,
                //text: 'Chart.js Drsw Line on Chart'
            },
            tooltips: {
                mode: 'index',
                intersect: true
            },
            annotation: {
                annotations: [{
                    type: 'line',
                    mode: 'horizontal',
                    scaleID: 'y-axis-0',
                    value: 22,
                    borderColor: 'rgb(75, 192, 192)',
                    borderWidth: 4,
                    label: {
                        enabled: false,
                        content: 'Test label'
                    }
                }]
            }
        }
    });





}



function successGetDetails(dataPatient) {

    //alert('success get details');
    $("p#idP").text(dataPatient.Id);
    strFullName = dataPatient.FirstName + " " + dataPatient.LastName;
    $('p#nameP').text(strFullName);
    $('p#age').text(dataPatient.Age);
    console.log(dataPatient)
    $('p#classificationP').text(dataPatient.Classification);

    if (dataPatient.Image == "") {
        $('#profileImg').css("background-image", "url(/Images/menAvatar.png)");
    }
    else {
        console.log(dataPatient.Image);
        $('#profileImg').css("background-image", "url(/" + dataPatient.Image + ")");

    }
    var textur = "";
    if (dataPatient.IdTexture == 1) {
        textur = 'רגיל';

    }
    else if (dataPatient.IdTexture == 2) {
        textur = 'בלנדר';
    }
    else textur = 'רך';
    $('p#textur').text(textur);

    var diseasesStr = dataPatient.Diseases;

    if (diseasesStr == '') {
        diseasesStr = 'אין מחלות';
        $('p#disease').text(diseasesStr);
    }
    else {
        $('p#disease').text(diseasesStr);
    }

    var sensStr = '';
    for (var i = 0; i < dataPatient.Sensitivities.count; i++) {

        sensStr += dataPatient.Sensitivities[i] + " ";
    }
    if (sensStr == '') {
        sensStr = 'אין רגישויות';
        $('p#sens').text(sensStr);
    }
    else {
        $('p#sens').text(sensStr);
    }

    $('#heightPatient').text(dataPatient.Height);

    $('#weightPatient').text(dataPatient.Weight);
    $('#contactName').text(dataPatient.ContactName);

    $('#contactPhone').text(dataPatient.ContactPhone);

}

function getContact() {

    document.getElementById("contactName").style.visibility = "visible";
    document.getElementById("contactPhonee").style.visibility = "visible";

}
function errorGetDetails() {
    alert('error get details');
}

function successGetRange(dataRange) {
    //alert('success range');

    getTest(dataRange);

}

function errorGetRange() {
    alert('error range');
}



function getTest(dataRange) {


    rangeDATA = dataRange;
    var numPatient = p.NumPatient;
    ajaxCall("GET", "../api/laboratoryTests/?NumPatient=" + numPatient, "", successGetDate, errorGetDate);


}


function successGetDate(data) {

    localStorage.setItem('tests', JSON.stringify(data));

    if (data.length == 0) {
        strNULL = '';
        strNULL += '<div class="col bg-light border p-3">';
        strNULL += '<span id="test_parameter"> <strong>NO Test</strong></span></div>';

        $('#result1').html(strNULL);
    }
    else {
        //alert('success get date');
        var DatesDDL = document.getElementById('dates');
        var str = "";
        for (var i = data.length - 1; i >= 0; i--) {
            str += "<option>" + data[i].DateLab + "</option>";
        }
        DatesDDL.innerHTML = str;

        var selectedDate = $("#dates option:selected").text();


        for (var j = 0; j < data.length; j++) {

            if (data[j].DateLab == selectedDate) {

                loadTests(data[j], selectedDate)

            }
        }


    }
}

function getDateValue(val) {

    var dateSelected = $("#dates option:selected").text();

    p = JSON.parse(localStorage.getItem('tests'));
    for (var j = 0; j < p.length; j++) {
        if (p[j].DateLab == dateSelected) {

            loadTests(p[j], dateSelected)

        }
    }
    //loadTests(p, dateSelected);

}

function loadTests(p, dateSelected) {

    for (var i = 0; i < rangeDATA.length; i++) {
        colorBG = 'dark';
        var resName = rangeDATA[i].Name;

        if (resName == 'CRP') {

            var crpRes = p.Crp;
            var maxCRP = rangeDATA[i].MaxP;
            var minCRP = rangeDATA[i].MinP;
            var goodMaxCRP = rangeDATA[i].GoodMax;
            var goodMinCRP = rangeDATA[i].GoodMin;
            var rangeCRP = maxCRP + minCRP;
            var styleRes = (crpRes / rangeCRP) * 100;

            if (crpRes > maxCRP || crpRes < minCRP) {
                colorBG = 'danger';
                if (crpRes > maxCRP) {
                    styleRes = 80;

                }
                else styleRes = 15;
            }
            else {

                if (styleRes >= 80)
                    styleRes = 60;
                else if (styleRes <= 20) {
                    styleRes = 40;
                }

            }


            strCRP = '';

            strCRP += '<div class="col bg-light border p-3">';
            strCRP += '<span id="test_parameter"> <strong>CRP </strong></span></div>';
            strCRP += '<div class="col  bg-light border p-3" style="background-image:url(../Images/Limit.png); background-repeat: no-repeat; background-position: center; ">';
            strCRP += ' <span class="label label-info bg-' + colorBG + ' position-absolute" style="left:' + styleRes + '%">' + crpRes + '</span></br>';

            $('#result1').html(strCRP);

        }
        else if (resName == 'Lymphocytes') {
            var LymphocyteslRes = p.Lymphocytes;
            var maxLymphocytes = rangeDATA[i].MaxP;
            var minLymphocytes = rangeDATA[i].MinP;
            //var goodMaxLymphocytes = rangeDATA[i].GoodMax;
            //var goodMinLymphocytes = rangeDATA[i].GoodMin;
            var rangeLymphocytes = maxLymphocytes + minLymphocytes;
             styleRes = (LymphocyteslRes / rangeLymphocytes) * 100;


            if (LymphocyteslRes > maxLymphocytes || LymphocyteslRes < minLymphocytes) {
                colorBG = 'danger';
                if (LymphocyteslRes > maxLymphocytes) {
                    styleRes = 70;

                }
                else styleRes = 15;
            }
            else {

                if (styleRes >= 80)
                    styleRes = 60;
                else if (styleRes <= 20) {
                    styleRes = 40;
                }

            }

           

            strLymphocytes = '';

            strLymphocytes += '<div class="col bg-light border p-3">';
            strLymphocytes += '<span id="test_parameter"> <strong>Lymphocytes </strong></span></div>';
            strLymphocytes += '<div class="col  bg-light border p-3" style="background-image:url(../Images/Limit.png); background-repeat: no-repeat; background-position: center; ">';
            strLymphocytes += ' <span class="label label-info  bg-' + colorBG + ' position-absolute" style="left:' + styleRes + '%">' + LymphocyteslRes + '</span></br>';

            $('#result2').html(strLymphocytes);

        }
        else if (resName == 'Albumin') {
            var albuminRes = p.Albumin;
            var maxAlbumin = rangeDATA[i].MaxP;
            var minAlbumin = rangeDATA[i].MinP;
            //var goodMaxAlbumin = rangeDATA[i].GoodMax;
            //var goodMinAlbumin = rangeDATA[i].GoodMin;
            var rangeAlbumin = maxAlbumin + minAlbumin;
             styleRes = (albuminRes / rangeAlbumin) * 100;

            if (albuminRes > maxAlbumin) {
                styleRes = 70;
                colorBG = 'danger';

            }
            else if (albuminRes < minAlbumin) {
                styleRes = 15;
                colorBG = 'danger';

            }


            strAlbumin = '';

            strAlbumin += '<div class="col bg-light border p-3">';
            strAlbumin += '<span id="test_parameter"> <strong>Albumin </strong></span></div>';
            strAlbumin += '<div class="col  bg-light border p-3" style="background-image:url(../Images/Limit.png); background-repeat: no-repeat; background-position: center; ">';
            strAlbumin += ' <span class="label label-info bg-' + colorBG + ' position-absolute" style="left:' + styleRes + '%">' + albuminRes + '</span></br>';

            $('#result3').html(strAlbumin);

        }

        else if (resName == 'ProteinTotal') {
            var HemoglobinMenRes = p.ProteinTotal;
            var maxHemoglobinMen = rangeDATA[i].MaxP;
            var minHemoglobinMen = rangeDATA[i].MinP;
            //var goodMaxHemoglobinMen = rangeDATA[i].GoodMax;
            //var goodMinHemoglobinMen = rangeDATA[i].GoodMin;
            var rangeHemoglobinMen = maxHemoglobinMen + minHemoglobinMen;
             styleRes = (HemoglobinMenRes / rangeHemoglobinMen) * 100;

            if (styleRes > 100) {
                colorBG = 'danger';
                if (albuminRes > maxAlbumin) {
                    styleRes = 70;

                }
                else styleRes = 15;
            }

            strHemoglobinMen = '';

            strHemoglobinMen += '<div class="col bg-light border p-3">';
            strHemoglobinMen += '<span id="test_parameter"> <strong>ProteinTotal </strong></span></div>';
            strHemoglobinMen += '<div class="col  bg-light border p-3" style="background-image:url(../Images/Limit.png); background-repeat: no-repeat; background-position: center; ">';
            strHemoglobinMen += ' <span class="label label-info  bg-' + colorBG + ' position-absolute" style="left:' + styleRes + '%">' + HemoglobinMenRes + '</span></br>';

            $('#result4').html(strHemoglobinMen);

        }

        else if (resName == 'Cholesterol') {
            txtColor = 'dark'
            var CholesterolRes = p.Cholesterol;
            var maxCholesterol = rangeDATA[i].MaxP;
            var minCholesterol = rangeDATA[i].MinP;
            var goodMaxCholesterol = rangeDATA[i].GoodMax;
            var goodMinCholesterol = rangeDATA[i].GoodMin;


            if (CholesterolRes > goodMaxCholesterol) {
                txtColor = 'danger';

            }

            strCholesterol = '';

            strCholesterol += '<div class="col bg-light border p-3">';
            strCholesterol += '<span id="test_parameter"> <strong>Cholesterol </strong></span></div>';
            strCholesterol += '<div class="col  bg-light border p-3">';
            strCholesterol += '<p class="text-' + txtColor + ' text-center">' + CholesterolRes + '</p></div>';
            //strAlbumin += ' <span class="label label-info  bg-' + colorBG + ' position-absolute" style="left:' + styleRes + '%">' + HemoglobinMenRes + '</span></br>';

            $('#result5').html(strCholesterol);

        }

    }
}





function errorGetDate() {
    alert('error get date');


}

function successGetLab(data) {
    alert('success lab');
}

function errorGetLab() {
    alert('error lab');
}

function getRange() {




    


}



function successGetBMI(data) {
    //alert("success BMI");
    var arrayBMI = [];
    var arrayDate = []


    for (var i = 0; i < data.length; i++) {

        arrayBMI.push(data[i].Bmi * 10000);
        arrayDate.push(data[i].Date);
    }


    getChartBMI(arrayBMI, arrayDate);
}

function errorGetBMI() {
    alert("fail BMI");
}