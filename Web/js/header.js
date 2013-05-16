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

    $('#signinTable').show();
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
    data = {};

    data.email = $('input[id=emailAddress]').val();
    if (data.email == null || data.email.length == 0)
        return;

    data.password = $('input[id=password]').val();
    if (data.password == null || data.password.length == 0)
        return;

    Ajax("wsUsers.asmx/ValidateUser", JSON.stringify(data), function(data) {
        if (data.IsValid == "False") {
            alert(data.Message);
        }
        else {
            LoginUser(data);
            SaveLoginCookie(info);
        }
    });
}

function LoginUser(info) {
    DisplayAnonymous(false);
    $('#DisplayName').text(info.DisplayName);
    userInfo = info;
    SaveLoginCookie(info);
}

function SaveLoginCookie(info) {
    cookie = $.cookie('user');
    if (cookie != null) {
        if (cookie.Version != info.Version) {
            $.removeCookie('user');
        }
    }
    $.cookie('user', info, { expires: 30 });
}

function Logout() {
    DisplayAnonymous(true);
    $.removeCookie('user');
}

function ForgotPassword() {
    data = {};

    data.email = $('input[id=emailAddress]').val();
    if (!validateEmail(data.email))
        return;

    Ajax("wsUsers.asmx/ForgotPassword", JSON.stringify(data), function(data) {
        alert(data.Message);
    });
}

function RegisterUser() {
    data = {};

    data.email = $('input[id=registerEmail]').val();
    if (!validateEmail(data.email))
        return;

    Ajax("wsUsers.asmx/RegisterUser", JSON.stringify(data), function (data) {
        if (!data.IsValid) {
            alert(data.Message);
        }
        else {
            LoginUser(data);
            $('div[id=RegisterUserProfile]').show();
        }
    });
}