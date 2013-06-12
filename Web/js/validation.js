function Validation() {
    var _this = {
        hasData: hasData,
        userLoggedIn: userLoggedIn,
        isEmail: isEmail,
        isAfterToday: isAfterToday,
        isLaterThan: isLaterThan
    }

    return _this;

    function hasData(text) {
        return (text != null && text.length > 0);
    }

    function userLoggedIn() {
        return (userInfo != null);
    }

    function isEmail(email) {
        var re = /\S+@\S+\.\S+/;
        return re.test(email);
    }

    function isAfterToday(d) {
        return isLaterThan(d, (new Date()).toDateString());
    }

    function isLaterThan(d1, d2) {
        var date1 = new Date(d1);
        var date2 = new Date(d2);
        return date1 > date2;
    }
}