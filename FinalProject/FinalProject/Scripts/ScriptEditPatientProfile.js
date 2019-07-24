$(document).ready(function () {

    User = JSON.parse(localStorage.getItem('User'));
    if (User.roleId != "1") {
        location.href = "LoginAdminUsers.html";
    }

    ajaxCall("GET", "../api/sensetivities", "", successGetSensitivities, errorGetSensitivities);

    $("#updateBTN").click(updateDetails);
});

function updateDetails(e) {
    e.preventDefault();

    var number = document.getElementById("contactPhone").value;

    if (number[0] == '0' && number.length == 10) {
        saveDetails();
        return false;
    } else {
        alert('הטלפון של איש הקשר לא תקין');
        return false;
    }
}

function saveDetails() {

    var id = $('#idNumber').val();
    var firstName = $('#firstName').val();
    var lastName = $('#lastName').val();
    var dateOfBirth = $('#bdate').val().toString();
    var contactName = $('#contactName').val();
    var contactRelation = $('#contactRelation').val();
    var phoneContact = $('#contactPhone').val();
    var height = $('#height').val();
    var weight = $('#weight').val();
    var diseases = $('#diseases').val();
    var clasification = $('#clasification').val();
    var comments = $('#comments').val();

    var selected = [];
    $('input[name=sens]').each(function () {
        if ($(this)[0].checked) {
            selected.push($(this)[0].id.replace('sens_', ""));
        }
    });

    var textureSel = "";
    var texture1 = document.getElementById("texture_1").checked;
    var texture2 = document.getElementById("texture_2").checked;
    var texture3 = document.getElementById("texture_3").checked;

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

    Patient = {

        NumPatient: p.NumPatient,
        Id: id,
        FirstName: firstName,
        LastName: lastName,
        DateOfBirth: dateOfBirth,
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
    }

    ajaxCall("PUT", "../api/patients", JSON.stringify(Patient), successUpdate, errorUpdate);
}

function successUpdate() {
    alert('העדכון עבר בהצלחה');
    window.location.href = "show-patient-list-NEW.html";
}

function errorUpdate() {
    alert('update error');
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

    p = JSON.parse(localStorage.getItem('pat'));
    $("#firstName").val(p.FirstName);
    $("#lastName").val(p.LastName);
    $("#idNumber").val(p.Id);
    $("#bdate").val(p.dateOfBirth);
    $("#height").val(p.Height);
    $("#weight").val(p.Weight);
    $("#contactName").val(p.ContactName);
    $("#contactRelation").val(p.ContactRelation);
    $("#contactPhone").val(p.ContactPhone);
    $("#clasification").val(p.Classification);
    $("#comments").val(p.Comments);
    $("#diseases").val(p.Diseases);
    $("#image").val(p.Image);
    nameimg = p.Image;
    $("#ph").html("<img src='../" + p.Image + "'/>");

    for (var i = 0; i < p.Sensitivities.length; i++) {
        document.getElementById('sens_' + p.Sensitivities[i]).checked = true;
    }

    $("#texture_" + p.IdTexture)[0].checked = true;
}

function GotoAddPatient() {
    window.location.href = "addPatient.html";
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

function errorGetSensitivities() {
    alert('error sensetivities');
}

function error(data) {
    console.log(data);
}