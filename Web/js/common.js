userInfo = null;
customPort = 54368;

$(document).ready(function () {
    $.cookie.json = true;

    userInfo = $.cookie('user');
});
