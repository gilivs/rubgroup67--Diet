$(function () {
    $(".preloader").fadeOut();
});


$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

$(document).ready(function () {
    var password = getCookie("passwordUser");
    var email = getCookie("emailUser");
    if (password != null && email != null) {
        $("#emailUser").val(email);
        $("#passwordUser").val(password);
    }
    $("#loginForm").submit(exist);
    //// $("#upBTN").click(exist);
    ////pass = $("#passwordUser").val();
    ajaxCall("GET", "../api/adminUsersUserN/?UserName=" + userName + "&PasswordUser=" + pass, "", successGetAdminApp, errorGetAdminApp);

});

//==============================================================
//Login and Recover Password
//==============================================================
$('#to-recover').on("click", function () {
    $("#loginform").slideUp();

    $("#recoverform").fadeIn();

});

function exist(e) {
    e.preventDefault();
    mail = $("#emailUser").val();
    pass = $("#passwordUser").val();

    pass = $("#passwordUser").val();

    ajaxCall("GET", "../api/adminUsers/?EmailUser=" + mail + "&PasswordUser=" + pass, "", successGetAdminUsers, errorGetAdminUsers);
}


function successGetAdminApp(data) {

    alert('success ' + data);

}

function errorGetAdminApp() {
    alert('error app admin');

}

function successGetAdminUsers(data) {
    if (data.NumUser === 0) {
        swal("המשתמש לא נמצא, הוא אינו קיים");
    }
    else {
        if (data.NumUser != 0) {

            if (data.roleId == "1") {
                localStorage.setItem('User', JSON.stringify(data));

                window.location.href = "dashboard.html";
            }
            else
                if (data.roleId == "2") {
                    localStorage.setItem('User', JSON.stringify(data));

                    window.location.href = "meal-day-week-maintenance.html";
                }
        }

        if ($("#rememberCheckBox").is(':checked')) {

            pass = $("#passwordUser").val();
            setCookie("passwordUser", pass, 300);
            mail = $("#emailUser").val();
            setCookie("emailUser", mail, 300);
        }
    }
}

function errorGetAdminUsers() {
    swal("error in get AdminUsers");
}
