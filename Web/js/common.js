userInfo = null;
customPort = 54368;

$(document).ready(function () {
    $.cookie.json = true;

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
            alert("Unavailable");
        }
    });
}
