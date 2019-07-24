var selectedMeal = {};
var newMeal = {
    Description: '',
    MealTypeId: '',
    Categories: []
}
var allMeals = [];
var categories = [];
//var categories = [];
$(document).ready(function () {

    //לא לשכוח להחזיר את ההתחברות
    //User = JSON.parse(localStorage.getItem('User'));
    //if (User.roleId !== "2") {
    //    location.href = "LoginAdminUsers.html";
    //}

    //alert("load");

    ajaxCall("GET", "../api/mealType", "", successGetMealType, errorGetMealType);
    ajaxCall("GET", "../api/categories", "", successGetCategories, errorGetCategories);
    $('#btnAddCatNew').click(function () {
        if (newMeal.MealTypeId !== '') {

            var id = $('#ddlItemsNew').val();
            var name = $('#ddlItemsNew').find('option:selected').text();
            //var selected = categories.find(function (c) {
            //    return c.IdCategory == id;
            //});
            newMeal.Categories.push(id);

            //$('#itemsNew').append(`<li class="list-group-item">${selected.CategoryName}  <button cat-id="${selected.IdCategory}" class="btn-danger deleteCatNew">X</button></li>`);
            $('#itemsNew').append(`<li class="list-group-item">${name}  <button cat-id="${id}" class="btn-danger deleteCatNew">X</button></li>`);
        }
    });
    $('#btnAddCatEdit').click(function () {
        var id = $('#ddlItemsEdit').val();
        var name = $('#ddlItemsEdit').find('option:selected').text();

        ajaxCall("POST", "../api/meal/" + selectedMeal.Id + "/product/" + id, "",
            function () {
                loadSelectedMeal(selectedMeal.Id);
            }, function () {

            });
    });

    $('body').on('click', '.deleteCatNew', function () {
        var id = $(this).attr('cat-id');
        var index = newMeal.Categories.indexOf(id);
        newMeal.Categories.splice(index, 1);
        $(this).parent().remove();
    });

    $('body').on('click', '.meal-item', function () {
        $(this).parent().find('a').removeClass('active');
        $(this).addClass('active');
        var mealId = $(this).attr('meal-id');
        loadSelectedMeal(mealId);
    });

    function loadSelectedMeal(mealId) {
        selectedMeal = allMeals.find(function (m) {
            return m.Id === mealId;
        });
        $('#txtEditDesc').val(selectedMeal.Description);
        getMealCategories(mealId, appendMealCategories);
        $('#selectedCat').text(selectedMeal.Name + " " + selectedMeal.Description || '')
        $('#edit-sec').show();
    }

    function appendMealCategories(categories, mealId) {
        var $container = $('.selected-meal-categories .list-group');
        $container.empty();

        categories.forEach(function (c) {
            $container.append(

                `<div class="input-group mb-3">
                          <input type="text" readonly  value="${c.NameProduct}" class="form-control" aria-label="" aria-describedby="basic-addon2">
                                <div class="input-group-append">
                                    <button class="btn btn-outline-danger removeCat" meal-id="${mealId}" cat-id="${c.IdProduct}" type="button">X</button>
                                </div>
                            </div>`
            );

        });

    }

    function getMealCategories(mealId, cb) {
        ajaxCall("GET", "../api/meal/" + mealId + "/products", "",
            function (products) {
                selectedMeal.Categories = products.map(function (c) {
                    return c.IdProduct;
                });
                cb(products, mealId);
            }, function () {

            });
    }
    $('body').on('click', '.removeCat', function () {
        var catid = $(this).attr('cat-id');
        var mealid = $(this).attr('meal-id');

        ajaxCall("DELETE", "../api/meal/" + mealid + "/products/" + catid, "",
            function () {
                getMealCategories(mealid, appendMealCategories);
                alert('Meal Deleted successfully');
            }, function () {

            });

    });


    $('#btnUpdate').click(function () {
        selectedMeal.Description = $('#txtEditDesc').val();

        selectedMeal.Categories = selectedMeal.Categories.map(function (c) {
            return (c.IdCategory) ? c.IdCategory : c;
        })
        ajaxCall("POST", "../api/updateMeal", JSON.stringify(selectedMeal),
            function (data) {
                var d = data;

                init();
                alert('Meal Updated successfully');
            }
            , function () {
            });
    });

    $('#btnCreate').click(function () {
        newMeal.Description = $('#newDescription').val();
        ajaxCall("POST", "../api/createMeal", JSON.stringify(newMeal),
            function (data) {
                var d = data;
                $('#createNewModal').modal('hide');
                $(".modal-backdrop").remove();
                $('#itemsNew').empty();
                $('#newDescription').val('');
                newMeal.Categories = [];
                init();
                alert('Meal created successfully');
            }
            , function () {
            });
    });

    function init() {
        ajaxCall("GET", "../api/meals", "", function (meals) {
            allMeals = meals;
            var $lst = $('#lstMeals');
            $lst.empty();

            allMeals.forEach(function (m) {
                $lst.append(`<a class="list-group-item list-group-item-action meal-item" meal-id="${m.Id}">
                                ${m.Name} - ${m.Description}
                                </a>`);
            });

            $('#txtEditDesc').val('');
            $('#selectedCat').text('');
            var selectedMeal = {};
        }, function () {
        });
    }

    init();
});


function successGetMealType(data) {
    var mealTypes = data;
    mealTypes.forEach(function (m) {

        $('#ddlMeals').append(`<option value="${m.IdMealType}">${m.NameMealType}</option>`).change(function () {
            newMeal.MealTypeId = $(this).val();
        })
        //$('#ddlMealsUpdate').append(`<option id="${m.IdMealType}">${m.NameMealType}</option>`)
    })
}

function errorGetMealType() {

}
function successGetCategories(data) {
    var mealTypes = data;
    mealTypes.forEach(function (m) {
        categories.push(m);
        $('#ddlCategoriesNew').append(`<option value="${m.IdCategory}">${m.CategoryName}</option>`);
        $('#ddlCategoriesEdit').append(`<option value="${m.IdCategory}">${m.CategoryName}</option>`)
    });

    $('#ddlCategoriesNew').change(function () {
        var catId = $(this).val();
        ajaxCall("GET", "../api/getCatItems/" + catId, "",
            function (items) {
                $('#ddlItemsNew').empty();

                items.forEach(function (m) {
                    $('#ddlItemsNew').append(`<option value="${m.IdProduct}">${m.NameProduct}</option>`);
                });
            }
            , function () {
            });
    });
    $('#ddlCategoriesEdit').change(function () {
        var catId = $(this).val();
        ajaxCall("GET", "../api/getCatItems/" + catId, "",
            function (items) {
                $('#ddlItemsEdit').empty();
                items.forEach(function (m) {
                    $('#ddlItemsEdit').append(`<option value="${m.IdProduct}">${m.NameProduct}</option>`);
                });
            }
            , function () {
            });
    });
}

function errorGetCategories() {

}