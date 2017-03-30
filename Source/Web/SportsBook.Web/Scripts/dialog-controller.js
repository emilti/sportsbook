$(function () {
    var dialog = $("#dialog-login-form").dialog({
        autoOpen: false,
        draggable: true,        
        width: 350,
        height: 390,
        modal: true,
        dialogClass: 'dialog-title'
    })
    $(".ui-dialog-titlebar").hide()
});

$(function () {
    var dialog = $("#dialog-register-form").dialog({
        autoOpen: false,
        width: 350,
        height: 650,
        modal: true,
        dialogClass: 'dialog-title'
    })
    $(".ui-dialog-titlebar").hide()
});

$(document).on("click", ".favorites-holder", function (e) {
    $.get("/account/CheckLogin", "", function (result) {
        var isUserLoggedIn = result;
        if (isUserLoggedIn === "False") {
            $("#dialog-login-form").dialog('open');
            $.validator.unobtrusive.parse("#dialog-login-form");
        }
    });
})


$(".go-to-register-form").on("click", function (e) {
    $("#dialog-register-form").dialog('open');
    $.validator.unobtrusive.parse("#dialog-register-form");
})

$(document).on("click", "#go-to-register-form", function (e) {
    $("#dialog-login-form").dialog("close");
    $("#dialog-register-form").dialog("open");
    $.validator.unobtrusive.parse("#dialog-register-form");
})

$(".go-to-login-form").on("click", function (e) {
    $("#dialog-login-form").dialog('open');
    $.validator.unobtrusive.parse("#dialog-login-form");
})

$(document).on("click", "#go-to-login-form", function (e) {
    $("#dialog-register-form").dialog("close");
    $("#dialog-login-form").dialog("open");
    $.validator.unobtrusive.parse("#dialog-login-form");
})

