$(function () {
    var dialog = $("#dialog-form").dialog({
        autoOpen: false,
        width: 600,
        height: 500,
        modal: true,
        dialogClass: 'dialog-title'
    })
    $(".ui-dialog-titlebar").hide()
});

$(function () {
    var dialog = $("#dialog-register-form").dialog({
        autoOpen: false,
        width: 600,
        height: 500,
        modal: true,
        dialogClass: 'dialog-title'
    })
    $(".ui-dialog-titlebar").hide()
});

$(document).on("click", ".favorites-holder", function (e) {
    $.get("/account/CheckLogin", "", function (result) {
        var isUserLoggedIn = result;
        if (isUserLoggedIn === "False") {
            $("#dialog-form").dialog('open');
            $.validator.unobtrusive.parse("#dialog-form");
        }
    });
})


$(".go-to-register-form").on("click", function (e) {
    $("#dialog-register-form").dialog('open');
    $.validator.unobtrusive.parse("#dialog-register-form");
})

$(document).on("click", "#go-to-register-form", function (e) {
    $("#dialog-form").dialog("close");
    $("#dialog-register-form").dialog("open");
    $.validator.unobtrusive.parse("#dialog-register-form");
})

