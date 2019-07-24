$(document).ready(function () {

    User = JSON.parse(localStorage.getItem('User'));
    if (User.roleId != "2") {
        location.href = "LoginAdminUsers.html";
    }

    ajaxCall("GET", "../api/categories", "", successGetCat, errorGetCat);

    $('#productT').submit(f1);
});

function GotoAddMenu() {
    window.location.href = "buildMenu.html";
}
function successGetCat(data) {
    var str = 0;
    for (var i = 0; i < data.length; i++) {
        str += "<option value=" + data[i]["IdCategory"] + ">" + data[i]["CategoryName"] + "</option>";
    }
    document.getElementById("category").innerHTML = str;
}

function errorGetCat() {
    swal("error in Sen");
}

function f1() {


    addProduct();
    return false;
}

function addProduct() {
    var nameProduct = $('#NameProduct').val();
    var idCategory = $('#category').val();
    var mainGroop = $('#MainGroop').val();
    var calories = $('#Calories').val();
    var weight = $('#Weight').val();

    Product = {

        NameProduct: nameProduct,
        IdCategory: idCategory,
        MainGroop: mainGroop,
        Calories: calories,
        Weight: weight,

    }
   
    ajaxCall("POST", "../api/products", JSON.stringify(Product), successPost, errorPost);
}

function successPost() {
    alert('success adding product');
    location.reload();
}

function errorPost() {
    alert('error adding product');
}
