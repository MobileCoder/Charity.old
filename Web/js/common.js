userInfo = null;
customPort = 54368;

$(document).ready(function () {
    $.cookie.json = true;

    $('#UpdateRegisterUserProfile').click(function () {
        UpdateRegisterUserProfile();
    });

    userInfo = $.cookie('user');
    if (userInfo) {
        data = {};
        data.Id = userInfo.Id;
        Ajax("wsUsers.asmx/ValidateUserById", JSON.stringify(data), function (data) {
            if (data.IsValid == "False") {
                Logout();
                alert(data.Message);
            }
            else {
                LoginUser(data);
            }
        });
    }
});

function RootUrl() {
    var root = $.myURL();

    if (customPort != null)
        root = root.replace('localhost', 'localhost:' + customPort);

    return root;
}

function Ajax(method, parameters, cb) {
    var webMethod = RootUrl() + method;

    $.ajax({
        type: "POST",
        url: webMethod,
        data: parameters,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            data = JSON.parse(msg.d);
            cb(data);
        },
        error: function (e) {
            alert(e.responseText);
        }
    });
}

function UpdateRegisterUserProfile() {
    data = {};

    data.id = userInfo.Id;
    data.firstName = $('input[id=RegisterUserProfile_FirstName]').val();
    data.lastName = $('input[id=RegisterUserProfile_LastName]').val();
    data.password = $('input[id=RegisterUserProfile_Password]').val();

    Ajax("wsUsers.asmx/UpdateUser", JSON.stringify(data), function (data) {
        alert(data);
    });
}

function validateUser() {
    if (userInfo == null) {
        alert('User needs to be logged in');
        return false;
    }
    return true;
}

function isTrue(v) {
    return ((v.toUpperCase() == "TRUE") || (v == '1'))
}

function isFalse(v) {
    return ((v.toUpperCase() == "FALSE") || (v == '0'))
}

function validateEmail(email) {
    return true;
}