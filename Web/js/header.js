$(document).ready(function () {
    DisplayAnonymous(true);

    $('#SignInOptions').hide();
    $('#RegisterOptions').hide();

    $('#LoginButton').click(function () {
        Login();
    });
    $('#Logout').click(function () {
        Logout();
    });

    $('#ForgotPassword').click(function () {
        ForgotPassword();
    });

    $('#RegisterButton').click(function () {
        RegisterUser();
    });

    $('#SignIn').click(function () {
        DisplaySignIn();
    });

    $('#Register').click(function() {
        DisplayRegister();
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

function DisplaySignIn() {
    $('#SignInOptions').show();
    $('#SignIn').addClass('selected');

    $('#RegisterOptions').hide();
    $('#Register').removeClass('selected');
}

function DisplayRegister() {
    $('#SignInOptions').hide();
    $('#SignIn').removeClass('selected');

    $('#RegisterOptions').show();
    $('#Register').addClass('selected');
}

function Login() {
    var email = $('input[id=emailAddress]').val();
    if (email == null || email.length == 0)
        return;

    var password = $('input[id=password]').val();
    if (password == null || password.length == 0)
        return;

    var parameters = "{'email':'" + email + "','password':'" + password + "'}";

    Ajax("header.asmx/ValidateUser", parameters, function(data) {
        if (data.IsValid == "False") {
            alert(data.Message);
        }
        else {
            LoginUser(data);
            $.cookie('user', data, { expires: 7 });
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

function ForgotPassword() {
    var email = $('input[id=emailAddress]').val();
    if (!validateEmail(email))
        return;

    var parameters = "{'email':'" + email + "'}";
    Ajax("header.asmx/ForgotPassword", parameters, function(data) {
        alert(data.Message);
    });
}

function RegisterUser() {
}