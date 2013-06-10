function Validation() {
    var _this = {
        hasData: hasData,
        userLoggedIn: userLoggedIn,
        isEmail: isEmail
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
}