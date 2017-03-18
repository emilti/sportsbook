$(function () {
    var dialog = $("#dialog-form").dialog({
        autoOpen: false,
        width: 600,
        height: 600,
        modal: true,
        dialogClass: 'dialog-title'        
    })
    $(".ui-dialog-titlebar").hide()
});

$(document).on("click", "#login-form .login-button", function (e) {
    var username = $("#login-form .username").val(),
    password = $("#login-form .password").val();
    var loginModel = { UserName: username, Password: password, __RequestVerificationToken: gettoken() };
    $.post("/Account/Login",
       loginModel, function success(data, textStatus, jqXHR) {
           $("#dialog-form").dialog("close");        
           location.reload();
       });
})

$(document).on("click", ".favorites-holder", function (e) {   
    // var url = 'Account/GetPopupLogin';     
    $("#dialog-form").dialog('open');
    $.validator.unobtrusive.parse("#dialog-form");
    // $("#dialog-form").dialog("option", "show", { effect: "blind", duration: 800 });
    // $.get(url, function (data) {
    //     $("#dialog-form").html(data)
    //     $("#dialog-form").dialog('open');
    // });
})

