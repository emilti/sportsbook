﻿$(function () {
    var dialog = $("#dialog-login-form").dialog({
        autoOpen: false,
        draggable: true,        
        width: 350,
        height: 390,
        modal: true,
        dialogClass: 'dialog-title',
        show: { effect: "blind", duration: 800 },
        hide: { effect: "blind", duration: 800 }
    })
    $(".ui-dialog-titlebar").hide()
});

$(function () {
    var dialog = $("#dialog-register-form").dialog({
        autoOpen: false,
        width: 350,
        height: 700,
        modal: true,
        dialogClass: 'dialog-title',
        show: { effect: "blind", duration: 800 },
        hide: { effect: "blind", duration: 800 }
    })
    $(".ui-dialog-titlebar").hide()
});

$(document).on("click", ".details-favorites-button", function (e) {
    getPopUpLogin();
})

$(document).on("click", ".favorites-holder", function (e) {
    getPopUpLogin();
})

$(document).on("click", ".submit-comment-button", function (e) {
    getPopUpLogin();
})

$(document).on("click", ".not-logged-user-star", function (e) {
    getPopUpLogin();
})

function getPopUpLogin() {
    $.get("/account/CheckLogin", "", function (result) {
        var isUserLoggedIn = result;
        if (isUserLoggedIn === "False") {
            $("#dialog-login-form").dialog('open');
            $.validator.unobtrusive.parse("#dialog-login-form");
        }
    });
}

$(".go-to-register-form").on("click", function (e) {
    $("#dialog-register-form").dialog('open');
    $.validator.unobtrusive.parse("#dialog-register-form");
})

$(document).on("click", "#go-to-register-form", function (e) {
    $("#dialog-login-form").dialog("close");

    setTimeout(function() {
        $("#dialog-register-form").dialog("open");
        $.validator.unobtrusive.parse("#dialog-register-form");  }, 1500); 
})

$(".go-to-login-form").on("click", function (e) {
    $("#dialog-login-form").dialog('open');
    $.validator.unobtrusive.parse("#dialog-login-form");
})

$(document).on("click", "#go-to-login-form", function (e) {
    $("#dialog-register-form").dialog("close");

    setTimeout(function() {
        $("#dialog-login-form").dialog("open");
        $.validator.unobtrusive.parse("#dialog-login-form");
    }, 1500);
})


$(document).on("click", ".close-button-container", function (e) {
    $("#dialog-register-form").dialog("close");
    $("#dialog-login-form").dialog("close");   
})


