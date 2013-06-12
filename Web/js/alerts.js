function Alerts() {
    var _this = {
        displayAlert: displayAlert,
        requireLogin: requireLogin,
        requireEmail: requireEmail,
        requirePassword: requirePassword,
        success: success,
        afterToday: afterToday,
        expireLaterThanStart: expireLaterThanStart
    }

    return _this;

    function displayAlert(text) {
        alert(text);
    }

    function requireLogin() {
        displayAlert('User needs to be logged in');
    }

    function requireEmail() {
        displayAlert('Please enter an email address');
    }

    function requirePassword() {
        displayAlert('Please enter a password');
    }

    function success() {
        displayAlert('success');
    }

    function afterToday() {
        displayAlert('The start date needs to be after today');
    }

    function expireLaterThanStart() {
        displayAlert('The expiration date needs to be later than the start date');
    }
}