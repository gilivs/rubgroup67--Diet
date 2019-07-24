/// <reference path="ajaxcalls.js" />

$(document).ready(function () {

    User = JSON.parse(localStorage.getItem('User'));
    if (User.roleId != "1") {
        location.href = "LoginAdminUsers.html";
    }

    ajaxCall("GET", "../api/patients", "", successGetforAddLt, errorGetforAddLt);

    $('#pLabTest').submit(f1);
});

function successGetforAddLt(data) {
    var str = 0;
    for (var i = 0; i < data.length; i++) {
        str += "<option value=" + data[i]["NumPatient"] + ">" + data[i]["NumPatient"] + ' ' + data[i]["FirstName"] + ' ' + data[i]["LastName"] + "</option>";
    }

    document.getElementById("numPaNamePa").innerHTML = str;
}

function errorGetforAddLt() {
    swal("error in lt");
}

function f1() {

    addLabTest();
    return false;
}

function addLabTest() {
    var numPatient = $('#numPaNamePa').val();
    var dateLab = $('#dateLab').val().toString();
    var albumin = $('#albumin').val();
    var lymphocytes = $('#lymphocytes').val();
    var cholesterol = $('#cholesterol').val();
    var crp = $('#crp').val();
    var proteinTotal = $('#proteinTotal').val();

    LaboratoryTests = {

        NumPatient: numPatient,
        DateLab: dateLab,
        Albumin: albumin,
        Lymphocytes: lymphocytes,
        Cholesterol: cholesterol,
        Crp: crp,
        ProteinTotal: proteinTotal,
        ActiveLab: 1,
    }
    ajaxCall("POST", "../api/laboratoryTests", JSON.stringify(LaboratoryTests), successPost, errorPost);
}

function successPost() {
    alert('בדיקת הדם נוספה בהצלחה');
    window.location.href = "showLabTest.html";
}

function errorPost() {
    alert('error adding laboratorytests');
}

