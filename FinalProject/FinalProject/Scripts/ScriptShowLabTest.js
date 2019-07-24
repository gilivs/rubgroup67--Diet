$(document).ready(function () {

    User = JSON.parse(localStorage.getItem('User'));
    if (User.roleId != "1") {
        location.href = "LoginAdminUsers.html";
    }
    ajaxCall("GET", "../api/laboratoryTests", "", getSuccess, error);

});
     
function getSuccess(data) {
    //debugger;
    $("#laboratoryTestsTable").jsGrid({
        height: "500px",
        width: "100%",
        align: "center",
        style: "text-align:right",
        //dir:"rtl",
        sorting: !0,
        paging: !0,
        data: data,
        fields: [
            //{

            //// //< img src = "../images/84380.png" />
            // render: function (data, type, row, meta) {
            // type: "button",
            // str = "<button class='btn' data-id='" + row["Id"] + "' onclick='" + GetId('Id') +"'>עריכה</button>"

            // return str;
            // }
            //},
            //{
            {
                name: "DateLab",
                type: "text",
                width: 150,
                title: "תאריך בדיקה",
            },
            {
                name: "NumPatient",
                type: "text",
                width: 150,
                title: "מספר מטופל",
            },
            {
                name: "IdTest",
                type: "text",
                width: 150,
                title: "מספר בדיקה",
            },
            {
                type: "control", title: "עריכה/מחיקה", width: 100, editButton: false, deleteButton: false,
                itemTemplate: function (value, item) {
                    var $result = jsGrid.fields.control.prototype.itemTemplate.apply(this, arguments);

                    var $customEditButton = $("<button>").attr({ class: "customGridEditbutton jsgrid-button jsgrid-edit-button" })
                        .click(function (e) {
                            GetId(item.IdTest);
                            e.stopPropagation();
                        });

                    var $customDeleteButton = $("<button>").attr({ class: "customGridDeletebutton jsgrid-button jsgrid-delete-button" })
                        .click(function (e) {
                            //alert("Title: " + item.title);
                            DeleteLabTest(item.IdTest);
                            e.stopPropagation();
                        });

                    return $("<div>").append($customEditButton).append($customDeleteButton);


                    //type: "control", width: 100, editButton: false, deleteButton: false,
                    //itemTemplate: function (value, item) {
                    //    var $result = jsGrid.fields.control.prototype.itemTemplate.apply(this, arguments);

                    //    var $customEditButton = $("<button>").attr({ class: "customGridEditbutton jsgrid-button jsgrid-edit-button" })
                    //        .click(function (e) {
                    //            GetId(item.IdTest);
                    //            e.stopPropagation();
                    //        });

                    //var $customDeleteButton = $("<button>").attr({ class: "customGridDeletebutton jsgrid-button jsgrid-delete-button" })
                    //    .click(function (e) {
                    //        //alert("Title: " + item.title);
                    //        GetId(item.IdTest);
                    //        e.stopPropagation();
                    //    });

                    //return $("<div>").append($customEditButton).append($customDeleteButton);
                    //return $("<div>").append($customEditButton);

                }
            },
        ]
    })
}
function error(err) {
    swal("Error: " + err);
}

function GotoAddLabTestInShow() {
    window.location.href = "addLabTest.html";
}

function GetId(num) {
    //alert("working!!! i got the ID " + num);
    idTest = num;
    ajaxCall("GET", "../api/laboratoryTests/?IdTest=" + idTest, "", successGetLabTest, errorGetLabTest);
}

function DeleteLabTest(idTest) {
    ajaxCall("POST", "../api/laboratoryTests/?IdTest=" + idTest, "", successDelete, errorDelete);
}

function successDelete() {
    alert("בדיקת המעבדה נמחקה בהצלחה");
    location.reload();
}

function errorDelete() {
    alert('delete error');
}

function successGetLabTest(data) {

    localStorage.setItem('lt', JSON.stringify(data));
    window.location.href = "EditLabTest.html";

}
function errorGetLabTest(err) {
    swal("Error: " + err);
}