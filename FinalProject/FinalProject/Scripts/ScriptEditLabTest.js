$(document).ready(function () {

    User = JSON.parse(localStorage.getItem('User'));
    if (User.roleId != "1") {
        location.href = "LoginAdminUsers.html";
    }

    labTest = JSON.parse(localStorage.getItem('lt'));
    $("#numPatient").val(labTest.NumPatient);
    $("#dateLab").val(labTest.DateLab);
    $("#albumin").val(labTest.Albumin);
    $("#lymphocytes").val(labTest.Lymphocytes);
    $("#cholesterol").val(labTest.Cholesterol);
    $("#crp").val(labTest.Crp);
    $("#proteinTotal").val(labTest.ProteinTotal);

    $('#pLabTest').submit(f1UpdateLabTest);

});

function CancelLabTest() {
    window.location.href = "showLabTest.html"

    //return false;
}
function GotoAddLabTest() {
    window.location.href = "addLabTest.html";
}

function f1UpdateLabTest() {

    editLabTest();
    return false;
}

function editLabTest() {
    var numPatient = $('#numPatient').val();
    var dateLab = $('#dateLab').val().toString();
    var albumin = $('#albumin').val();
    var lymphocytes = $('#lymphocytes').val();
    var cholesterol = $('#cholesterol').val();
    var crp = $('#crp').val();
    var proteinTotal = $('#proteinTotal').val();

    LaboratoryTests = {

        IdTest: labTest.IdTest,
        NumPatient: numPatient,
        DateLab: dateLab,
        Albumin: albumin,
        Lymphocytes: lymphocytes,
        Cholesterol: cholesterol,
        Crp: crp,
        ProteinTotal: proteinTotal,
    }

    ajaxCall("PUT", "../api/laboratoryTests", JSON.stringify(LaboratoryTests), successPost, errorPost);
}

function successPost() {
    swal("עדכון בדיקות הדם עבר בהצלחה");
}

function errorPost() {
    alert('error Edit laboratorytests');
}