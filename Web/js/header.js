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

    enableForgotPassword();
    enableRegisterButton();

    $('#SignIn').click(function () {
        DisplaySignIn();
    });

    $('#Register').click(function() {
        DisplayRegister();
    });

    $('#DisplayName').click(function () {
        ShowProfilePage();
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

function enableLogin() {
    enableButton('Login', ForgotPassword);
}

function Login() {
    disableButton('Login');

    data = {};
    data.email = $('input[id=emailAddress]').val();
    data.password = $('input[id=password]').val();

    isValid = true;

    if (!validation.isEmail(data.email)) {
        alerts.requireEmail();
        isValid = false;
    }

    if (!validation.hasData(data.password)) {
        alerts.requirePassword();
        isValid = false;
    }

    if (!isValid) {
        enableLogin();
    }
    else {
        Ajax("wsUsers.asmx/ValidateUser", JSON.stringify(data), function (data) {
            if (isFalse(data.IsValid)) {
                alerts.displayAlert(data.Message);
            }
            else {
                if (isFalse(data.IsActive)) {
                    alerts.displayAlert('Inactive user');
                }
                else {
                    LoginUser(data);
                    SaveLoginCookie(data);
                }
            }
            enableLogin();
        });
    }
}

function LoginUser(info) {
    DisplayAnonymous(false);
    $('#DisplayName').text(info.DisplayName);
    userInfo = info;
    SaveLoginCookie(info);
    ProcessLoginEvents();
}

function SaveLoginCookie(info) {
    cookie = $.cookie('user');
    if (cookie != null) {
        if (cookie.Version != info.Version) {
            $.removeCookie('user');
        }
    }
    //$.cookie('user', info, { expires: 30 });
    $.cookie('user', info);
}

function Logout() {
    DisplayAnonymous(true);
    userInfo = null;
    $.removeCookie('user');
    ProcessLoginEvents();
}

function enableForgotPassword() {
    enableButton('ForgotPassword', ForgotPassword);
}

function ForgotPassword() {
    disableButton('ForgotPassword');
    data = {};
    data.email = $('input[id=emailAddress]').val();

    if (!validation.isEmail(data.email)) {
        alerts.requireEmail();
        enableForgotPassword();
        return;
    }

    Ajax("wsUsers.asmx/ForgotPassword", JSON.stringify(data), function(data) {
        alert(data.Message);
        enableForgotPassword();
    });
}

function enableRegisterButton() {
    enableButton('RegisterButton', RegisterUser);
}

function RegisterUser() {
    disableButton('RegisterButton');
    data = {};
    data.email = $('input[id=registerEmail]').val();

    if (!validation.isEmail(data.email)) {
        alerts.requireEmail();
        enableRegisterButton();
        return;
    }

    Ajax("wsUsers.asmx/RegisterUser", JSON.stringify(data), function (data) {
        if (isFalse(data.IsValid)) {
            alerts.displayAlert(data.Message);
        }
        else {
            alerts.displayAlert('Your password has been sent to you');
        }

        enableRegisterButton();
    });
}

function ShowProfilePage() {
    var url = RootUrl() + "profile.aspx";
    window.location = url;
}