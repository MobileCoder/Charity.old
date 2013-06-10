$(document).ready(function () {
    $('#CreateItem').click(function () {
        $('#CreateItemDetails').show();
    });

    enableCreateItem();
});

function displayCreateItem() {
    if (userInfo != null) {
        $('#CreateItem').show();
    }
    else {
        $('#CreateItem').hide();
    }
}

function enableCreateItem() {
    enableButton('CreateItem', RegisterUser);
}

function CreateItem() {
    disableButton('CreateItem');
    data = {};

    data.userId = userInfo.Id;
    data.title = $('input[id=CreateItemDetails_Title]').val();
    data.description = $('input[id=CreateItemDetails_Description]').val();
    data.startDate = $('input[id=CreateItemDetails_StartDate]').val();
    data.expireDate = $('input[id=CreateItemDetails_ExpireDate]').val();
    data.cashValue = $('input[id=CreateItemDetails_CashValue]').val();
    data.initialBid = $('input[id=CreateItemDetails_InitialBid]').val();

    Ajax("wsItem.asmx/Create", JSON.stringify(data), function (data) {
        if (isFalse(data.IsValid)) {
            alerts.displayMessage(data.Message);
        }
        else {
            alerts.success();
        }

        enableCreateItem();
    });
}