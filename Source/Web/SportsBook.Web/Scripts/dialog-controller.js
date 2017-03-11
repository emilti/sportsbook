$(function () {
    var dialog = $("#dialog-form").dialog({
        autoOpen: false,
        width: 200,
        height: 300,
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
