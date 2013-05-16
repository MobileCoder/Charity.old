function Validation() {
    var _this = {
        hasData: hasData,
        isEmail: isEmail
    }

    return _this;

    function hasData(text) {
        return (text != null && text.length > 0);
    }


    function isEmail(email) {
        if (email == null || email.length == 0)
            return false;

        return true;
    }
}