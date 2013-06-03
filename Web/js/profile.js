var billingAddressType = 1;
var shippingAddressType = 2;

$(document).ready(function () {
    $('#updateBillingAddress').click(function () {
        UpdateBillingAddress();
    });

    $('#updateShippingAddress').click(function () {
        UpdateShippingAddress();
    });

    LoadBillingAddress();
    LoadShippingAddress();
});

function LoadBillingAddress() {
    data = {};
    data.userId = userInfo.Id;
    data.addressType = billingAddressType;
    Ajax("wsUsers.asmx/GetAddress", JSON.stringify(data), function (data) {
        if (data) {
            setFields('Billing', data);
        }
    });
}

function LoadShippingAddress() {
    data = {};
    data.userId = userInfo.Id;
    data.addressType = shippingAddressType;
    Ajax("wsUsers.asmx/GetAddress", JSON.stringify(data), function (data) {
        if (data) {
            setFields('Shipping', data);
        }
    });
}

function setFields(key, data) {
    $('#' + key + 'Address1').val(data.Address1);
    $('#' + key + 'Address2').val(data.Address2);
    $('#' + key + 'City').val(data.City);
    $('#' + key + 'State').val(data.State);
    $('#' + key + 'Zipcode').val(data.Zipcode);
}

function getFields(key, data) {
    data.address1 = $('#' + key + 'Address1').val();
    data.address2 = $('#' + key + 'Address2').val();
    data.city = $('#' + key + 'City').val();
    data.state = $('#' + key + 'State').val();
    data.zipcode = $('#' + key + 'Zipcode').val();
}

function UpdateBillingAddress() {
    data = {};
    data.userId = userInfo.Id;
    data.addressType = billingAddressType;
    getFields('Billing', data);
    UpdateAddress(data);
}

function UpdateShippingAddress() {
    data = {};
    data.userId = userInfo.Id;
    data.addressType = shippingAddressType;
    getFields('Shipping', data);
    UpdateAddress(data);
}

function UpdateAddress(data) {
    Ajax("wsUsers.asmx/UpdateAddress", JSON.stringify(data), function (data) {
        if (isTrue(data)) {
            alert('Update Successful');
        }
        else if (isFalse(data)) {
            alert('Update Failed');
        }
        else {
            alert('Unknown response');
        }
    });
}