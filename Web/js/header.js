$(document).ready(function () {
    DisplayAnonymous(true);
    $('#LoginButton').click(function () {
        Login();
    });
    $('#Logout').click(function () {
        Logout();
    });

    if (userInfo != null) {
        LoginUser(userInfo);
    }
});

function DisplayAnonymous(showAnonymous) {
    if (showAnonymous) {
        $('#AnonymousLogin').show();
        $('#LoggedIn').hide();
    }
    else {
        $('#AnonymousLogin').hide();
        $('#LoggedIn').show();
    }
}

function Login() {
    
    var email = $('input[id=emailAddress]').val();
    if (email == null || email.length == 0)
        return;

    var password = $('input[id=password]').val();
    if (password == null || password.length == 0)
        return;

    var parameters = "{'email':'" + email + "','password':'" + password + "'}";
    
    //"http://localhost:54368/header.asmx/ValidateUser";
    var webMethod = $.myURL() + "header.asmx/ValidateUser";

    if (customPort != null)
        webMethod = webMethod.replace('localhost', 'localhost:' + customPort);

    $.ajax({
        type: "POST",
        url: webMethod,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            info = JSON.parse(msg.d);
            if (info.IsValid == "False") {
                alert(info.Message);
            }
            else {
                LoginUser(info);
                $.cookie('user', info, { expires: 7 });
            }
        },
        error: function (e) {
            alert("Unavailable");
        }
    });
}

function LoginUser(info) {
    DisplayAnonymous(false);
    $('#DisplayName').text(info.DisplayName);
    $.cookie('user', info, { expires: 7 });
}

function Logout() {
    DisplayAnonymous(true);
    $.removeCookie('user');
}