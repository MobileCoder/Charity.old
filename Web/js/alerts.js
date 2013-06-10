function Alerts() {
    var _this = {
        displayAlert: displayAlert,
        requireLogin: requireLogin,
        requireEmail: requireEmail,
        requirePassword: requirePassword,
        success: success
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
}