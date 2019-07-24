//START select js



//END select js


$(document).ready(function () {
    date = '20/05/2019';
    hour = '12';
    //ajaxCall("GET", "../api/mealType", "", successGetMealType, errorGetMealType);
    ajaxCall("GET", "../api/productForWeeklyMenu/?Date=" + date + "&hour=" + hour, "", successGetProducts, errorGetProducts);

});


function successGetProducts(data) {
    productsList = data;
    alert("success get Products");
    f1(productsList);
}

function errorGetProducts() {
    alert("error");
}

function f1(productsList) {

    sortProducts(productsList);
    days = {
        "day": [
            { "id": "sunday", "name": "ראשון" },
            { "id": "monday", "name": "שני" },
            { "id": "tusday", "name": "שלישי" },
            { "id": "wendsday", "name": "רביעי" },
            { "id": "thursday", "name": "חמישי" },
            { "id": "friday", "name": "שישי" },
            { "id": "saturday", "name": "שבת" }

        ]
    };

    weekDays = 7;
    str = '';
    for (var j = 0; j < weekDays; j++) {
        strDays = '';
        idDay = j + 1;
        //strDays += '<div class="list" id="day' + idDay + '">';
        strDays += '<h3 class="list-title" dir="rtl" id=' + days.day[j].id + '>יום ' + days.day[j].name + '</h3>';

        for (var i = 0; i < mealType.length; i++) {
            strDays += '<h3 class="list-title" dir="rtl" id=mealType' + mealType[i].IdMealType + '_' + idDay + '>ארוחת ' + mealType[i].NameMealType + '</h3>';
            //if (mealType[i].NameMealType === meals.mealTypeProducts[i].name) {
            //    for (var n = 0; n < meals.mealTypeProducts.length; n++) {
            //        if (meals.mealTypeProducts[n].meals.mealTypeProducts[i].name ==)
            //        strDays += ' <optgroup label="' + meals.mealTypeProducts[i] + '">';

            //    }
            }
            
                //for (var m = 0; m < mealDay[i].length; m++) {
                //    strDay += '<select class="selectpicker" data-style="form-control  dropdown-toggle btn-secondary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" dir="rtl">';
                //    str += ' <optgroup label="דייסה">'

                //        <optgroup label="דייסה">
                //            <option>דייסת סולת עם קינמון</option>
                //            <option>דייסת קוואקר</option>
                //            <option>דייסת סולת עם קקאו</option>
                //            <option>דייסת סולת </option>
                //        </optgroup>
                //    </select>


                //}

            

         

        } 
        //strDays += str + '</div>';

        document.getElementById("listContainer" + idDay).innerHTML = strDays;
    }


function sortProducts(productsList) {

    //var productsByCategory = {
    //    products: []
    //};

    //i = 0;

    //productsList.map(function (item) {
    //    productsByCategory.products.push({
    //        "name": item.NameProduct,
    //        "categoryID": item.lastName,
    //        "age": item.age
    //    });
    //}

   

    //mealTypeCategory = {};
    //for (var i = 0; i < mealType.length; i++) {



    //}
    mealsArr = [];
    breakFast = [];        
    brunch = [];
    lunch = [];
    four = [];
    dinner = [];
    night = [];
    for (var i = 0; i < productsList.length; i++) {

        if (productsList[i].IdCategory >= 1 && productsList[i].IdCategory <= 7) {
            breakFast.push(productsList[i].NameProduct);
            dinner.push(productsList[i].NameProduct);
        }
        else if (productsList[i].IdCategory === 8) {
            breakFast.push(productsList[i].NameProduct);
            night.push(productsList[i].NameProduct);
        }
        else if (productsList[i].IdCategory === 9 || productsList[i].IdCategory === 10) {
            brunch.push(productsList[i].NameProduct);
            four.push(productsList[i].NameProduct);
        }
        else if (productsList[i].IdCategory >= 11 && productsList[i].IdCategory <= 15) {

            lunch.push(productsList[i].NameProduct);

        }
        else if (productsList[i].IdCategory === 15) {
            dinner.push(productsList[i].NameProdect);
        }
        else if (productsList[i].IdCategory === 1 || productsList[i].IdCategory ===  10) {

            night.push(productsList[i].NameProduct);

        }
    }
    breakFast.push('משקה');
    brunch.push('משקה');
    lunch.push('משקה');
    four.push('משקה');
    dinner.push('משקה');
    night.push('משקה');

    //var meals = {
    //    mealTypeProducts: []
    //};

    //breakFast.map(function (item) {
    //    meals.mealTypeProducts.push({
    //        "name": 'breakFast',
    //        "productsArr": breakFast,
    //        "lengthArr": breakFast.length

    //    });
    //});

    //console.log(breakFast);

    //brunch.map(function (item) {
    //    meals.mealTypeProducts.push({
    //        "name": 'brunch',
    //        "productsArr": brunch,
    //        "lengthArr": brunch.length

    //    });
    //});
    //console.log(meals);

    //lunch.map(function (item) {
    //    meals.mealTypeProducts.push({
    //        "name": 'lunch',
    //        "productsArr": lunch,
    //        "lengthArr": lunch.length

    //    });
    //});
    //four.map(function (item) {
    //    meals.mealTypeProducts.push({
    //        "name": 'four',
    //        "productsArr": four,
    //        "lengthArr": four.length,


    //    });
    //});
    //dinner.map(function (item) {
    //    meals.mealTypeProducts.push({
    //        "name": 'dinner',
    //        "productsArr": dinner,
    //        "lengthArr": dinner.length,

    //    });
    //});
    //night.map(function (item) {
    //    meals.mealTypeProducts.push({
    //        "name": 'night',
    //        "productsArr": night,
    //        "lengthArr": night.length,

    //    });
    //});

    //console.log(meals);
    //mealsArr.push(breakFast);
    //mealsArr.push(brunch);
    //mealsArr.push(lunch);
    //mealsArr.push(four);
    //mealsArr.push(dinner);
    //mealsArr.push(night);



    //var productsByCategoryList = {
    //    products: []
    //};

 

    //productsList.map(function (item) {
    //    productsByCategoryList.products.push({
    //        "name": item.NameProduct,
    //        "categoryID": item.IdCategory,
    //        "categoryName": function () {
    //            if (item.IdCategory === category.categoryMeal.id) {
    //                return category.categoryMeal.name;
    //            }
    //        },


    //    });
    //});
    

    //mealDay = {
    //    "meal": [
    //        { "brakFast": breakFas },
    //        { "brunch": brunch },
    //        { "lunch": lunch },
    //        { "four": four },
    //        { "dinner": dinner },
    //        { "night": night }

    //    ]
    //};



}


function successGetMealType(data) {
    //swal('success getting Meal Type!!!');
    mealType = data;

    //var mealTypeCategory = {};
    //mealType.forEach(function (column) {
    //    var id = column.metadata.colName;
    //    jsonData[id] = column.value;
    //    var name = column.metadata.colName;
    //    jsonData[name] = column.value;
    //    var category = column.metadata.colName;
    //    jsonData[category] = column.value;
    //});
    //viewData.mtCategory.push(mealTypeCategory);

    mealTypeCategor = {
        "mtCategory": [
            { "id": mealType.id, "name": "ראשון" },
            { "id": "monday", "name": "שני" },
            { "id": "tusday", "name": "שלישי" },
            { "id": "wendsday", "name": "רביעי" },
            { "id": "thursday", "name": "חמישי" },
            { "id": "friday", "name": "שישי" },
            { "id": "saturday", "name": "שבת" }
        ]
    };

    
    ajaxCall("GET", "../api/products", "", successGetProducts, errorGetProducts);

}
function errorGetMealType() {
    swal('ERROR getting Meal Type!!!');

}