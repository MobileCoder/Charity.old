userInfo = null;
customPort = 54368;

$(document).ready(function () {
    $.cookie.json = true;

    $('#UpdateRegisterUserProfile').click(function () {
        UpdateRegisterUserProfile();
    });

    userInfo = $.cookie('user');
});

function Ajax(method, parameters, cb) {
    var webMethod = $.myURL() + method;

    if (customPort != null)
        webMethod = webMethod.replace('localhost', 'localhost:' + customPort);

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