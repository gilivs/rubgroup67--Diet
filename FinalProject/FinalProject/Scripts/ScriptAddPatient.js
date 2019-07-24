$(document).ready(function () {

    User = JSON.parse(localStorage.getItem('User'));
    if (User.roleId != "1") {
        location.href = "LoginAdminUsers.html";
    }

    ajaxCall("GET", "../api/sensetivities", "", successGetSensitivities, errorGetSensitivities);
    nameimg = "";

    $('#pForm').submit(f1);
});

// this function is activated in case of a success
function successGetSensitivities(sensitivitiesdata) {

    var sensitivitiesselect = document.getElementById('sensitivitiesselect');
    var str = "";
    for (var i = 0; i < sensitivitiesdata.length; i++) {
        str += "<div class='custom-control custom-checkbox'>";
        str += "<input type='checkbox' name='sens' class='custom-control-input' id='sens_" + sensitivitiesdata[i].NumSensitivity + "'>";
        str += "<label class='custom-control-label' for='sens_" + sensitivitiesdata[i].NumSensitivity + "'>" + sensitivitiesdata[i].NameSensitivity + "</label>";
        str += "</div>";
    }

    sensitivitiesselect.innerHTML = str;
}

// this function is activated in case of a failure
function errorGetSensitivities(err) {
    swal("Error in getting sensitivities");
}

function f1() {

    var number = document.getElementById("phoneContact").value;

    if (number[0] == '0' && number.length == 10) {
        addPatient();
        return false;
    } else {
        alert('הטלפון של איש הקשר לא תקין');
        return false;
    }

    //addPatient();
    //return false;
}

function addPatient() {

    var id = $('#idNumber').val();
    var firstName = $('#firstName').val();
    var lastName = $('#lastName').val();
    var dateOfBirth = $('#dateOfBirth').val().toString();
    var age = getAge(dateOfBirth);
    var contactName = $('#contactName').val();
    var contactRelation = $('#contactRelation').val();
    var phoneContact = $('#phoneContact').val();
    var height = $('#height').val();
    var weight = $('#weight').val();
    var diseases = $('#diseases').val();
    var clasification = $('#clasification').val();
    var comments = $('#comments').val();

    var selected = [];
    $('#sensitivities :selected').each(function () {
        selected.push($(this).val());
    });

    var textureSel = "";
    var texture1 = document.getElementById("regular").checked;
    var texture2 = document.getElementById("blender").checked;
    var texture3 = document.getElementById("soft").checked;

    if (!texture1 && !texture2 && !texture3) {
        alert("בחר מרקם מזון");
        return false;
    }
    if (texture1 == true) {
        textureSel = 1;
    } else
        if (texture2 == true) {
            textureSel = 2;
        }
        else {
            textureSel = 3;
        }

    Idnumber = document.getElementById("idNumber").value;

    if (Idnumber.length != 9) {
        alert('תעודת הזהות של המטופל לא תקינה');
        return false;
    }

    var selected = [];
    $('input[name=sens]').each(function () {
        if ($(this)[0].checked) {
            selected.push($(this)[0].id.replace('sens_', ""));
        }
    });

    Patient = {
        Id: id,
        FirstName: firstName,
        LastName: lastName,
        DateOfBirth: dateOfBirth,
        Age: age,
        ContactName: contactName,
        ContactPhone: phoneContact,
        ContactRelation: contactRelation,
        Height: height,
        Weight: weight,
        Diseases: diseases,
        IdTexture: textureSel,
        Sensitivities: selected,
        Classification: clasification,
        Image: nameimg,
        Comments: comments,
        Active: 1,
    }

    ajaxCall("POST", "../api/patients", JSON.stringify(Patient), successPost, errorPost);
}

function successPost() {
    swal("המטופל נוסף בהצלחה");
}

function errorPost() {
    alert('error adding patient');
}

function getAge(dateString) {
    var today = new Date();
    var birthDate = new Date(dateString);
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
        age--;
    }
    return age;
}

function Change() {
    var data = new FormData();
    var files = $("#files").get(0).files;

    // Add the uploaded file to the form data collection
    if (files.length > 0) {
        for (f = 0; f < files.length; f++) {
            data.append("UploadedImage", files[f]);
        }
    }

    // Ajax upload
    $.ajax({
        type: "POST",
        url: "../Api/FileUpload",
        contentType: false,
        processData: false,
        data: data,
        success: showImages,
        error: error
    });

    return false;
}

function showImages(data) {
    nameimg = data[0];
    console.log(data);

    var imgStr = "";

    if (Array.isArray(data)) {

        for (var i = 0; i < data.length; i++) {
            imgStr += "<img src='../" + data[i] + "'/>";
        }
    }

    else {
        imgStr = "<img src='../" + data + "'/>";
    }

    document.getElementById("ph").innerHTML = imgStr;
}

function error(data) {
    console.log(data);
}