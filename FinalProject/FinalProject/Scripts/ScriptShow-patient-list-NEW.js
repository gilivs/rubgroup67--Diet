$(document).ready(function () {
    senList = 0;
    ajaxCall("GET", "../api/sensetivities", "", successGetSen, errorGetSen);

});
function GotoAddNewPatient() {
    window.location.href = "addPatient.html";
}
function successGetSen(sendata) {
    senList = sendata;
    //alert("seccues");
    ajaxCall("GET", "../api/patients", "", getSuccess, error);
}
function errorGetSen() {
    swal("error in Sen");
}

function search_patient() {
    var search_patient_txt = document.getElementById('search_patient_txt').value;
    ajaxCall("GET", "../api/patients/?searchTXT=" + search_patient_txt, "", getSuccess, error);
}

function getSuccess(data) {
    //debugger;
    $("#patientsTable").jsGrid({
        height: "500px",
        width: "100%",
        align: "center",
        style: "text-align:right",
        //sorting: !0,
        //filtering: true,
        //editing: true,
        sorting: true,
        paging: true,
        autoload: true,
        //search: true,
        //paging: !0,
        data: data,
        fields: [
            {
                type: "control", title: "צפייה", width: 100,
                itemTemplate: function (value, item) {
                    var $result = jsGrid.fields.control.prototype.itemTemplate.apply(this, arguments);

                    var $customshowButton = $("<button>").append('<i class="far fa-eye"></i>').attr({ class: 'ShowButoon' })
                        .click(function (e) {
                            ShowPatient(item.NumPatient);
                            e.stopPropagation();
                        });


                    return $("<div>").append($customshowButton);
                }
            },
            {
                name: "Id",
                type: "text",
                title: "תעודת זהות",



                //width: 40
            }, {
                name: "FirstName",
                type: "text",
                title: "שם פרטי",
                // width: 40
            }, {
                name: "LastName",
                type: "text",
                title: "שם משפחה",
                // width: 40
            }, {
                name: "Kind0fTextureFood",
                type: "text",
                title: "מרקם",
                sorting: false,
                // width: 40
            },

            {

                name: "Sensitivities",
                type: "text",
                // width: 40,
                // align: "center",

                title: "רגישויות",
                cellRenderer: function (value, item) {
                    //debugger;
                    str = "";


                    for (i = 0; i < senList.length; i++) {
                        for (j = 0; j < value.length; j++) {
                            if (senList[i]["NumSensitivity"] == value[j])
                                str += senList[i]["NameSensitivity"] + "</br>";
                        }
                    }
                    return $("<td>").addClass("my-row-custom-class").append(str);
                }

            },

            {
                type: "control", title: "עריכה/מחיקה", width: 100, editButton: false, deleteButton: false,
                itemTemplate: function (value, item) {
                    var $result = jsGrid.fields.control.prototype.itemTemplate.apply(this, arguments);

                    var $customEditButton = $("<button>").attr({ class: "customGridEditbutton jsgrid-button jsgrid-edit-button" })
                        .click(function (e) {
                            //alert("ID: " + item.id);
                            UpdatePatient(item.NumPatient);
                            e.stopPropagation();
                        });

                    var $customDeleteButton = $("<button>").attr({ class: "customGridDeletebutton jsgrid-button jsgrid-delete-button" })
                        .click(function (e) {
                            //alert("Title: " + item.title);
                            DeletePatient(item.NumPatient);
                            e.stopPropagation();
                        });

                    return $("<div>").append($customEditButton).append($customDeleteButton);
                    //return $result.add($customButton);
                }
            },


        ]
    })
}
function error(err) {
    swal("Error: " + err);
}
function UpdatePatient(number) {
    //alert("working!!! i got the NumPatient " + number);
    numPatient = number;
    ajaxCall("GET", "../api/patients/?NumPatient=" + numPatient, "", successGetPatient, errorGetPatient);
}
function DeletePatient(numPatient) {
    ajaxCall("POST", "../api/patients/?NumPatient=" + numPatient, "", successDelete, errorDelete);
}
function ShowPatient(number) {
    alert("working!!! i got the NumPatient " + number);
    numPatient = number;
    ajaxCall("GET", "../api/patients/?NumPatient=" + numPatient, "", successGetPatientShow, errorGetPatientShow);
}

function successGetPatient(data) {

    localStorage.setItem('pat', JSON.stringify(data));
    window.location.href = "edit-patient-profile.html";

}
function successGetPatientShow(data) {

    localStorage.setItem('pat', JSON.stringify(data));
    window.location.href = "profile.html";
    //window.location.href = "patientProfilePage.html";

}
function errorGetPatientShow(err) {
    swal("Error: " + err);
}
function errorGetPatient(err) {
    swal("Error: " + err);
}
function successDelete() {

    alert('delete sucess');
    location.reload();

}
function errorDelete() {
    alert('delete error');

}