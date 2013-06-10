$(document).ready(function () {
    enableBidButtons();
});

function displayBidButton() {
    $(":button").each(function () {
        itemId = $(this).attr('itemid');
        if (itemId != null) {
            if (userInfo != null) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        }
    });
}

function enableBidButtons() {
    $(":button").each(function () {
        itemId = $(this).attr('itemid');
        if (itemId != null) {
            $(this).click({ param1: itemId }, CreateBid);
        }
    });
}

function disableBidButtons() {
    $(":button").each(function () {
        itemId = $(this).attr('itemid');
        if (itemId != null) {
            disableButton($(this).attr('id'));
        }
    });
}

function CreateBid(event) {
    disableBidButtons();

    itemId = event.data.param1;
    data = {};

    if (!validation.userLoggedIn()) {
        alerts.requireLogin();
        enableBidButtons();
        return;
    }

    data.userId = userInfo.Id;
    data.itemId = itemId;
    data.amount = $('input[itemid="' + itemId + '"][type="text"]').val()

    if (validation.hasData(data.amount)) {
        Ajax("wsItem.asmx/Bid", JSON.stringify(data), function (data) {
            if (isFalse(data.IsValid)) {
                alerts.displayAlert(data.Message);
            }
            else {
                alerts.success();
            }

            $('span[itemid="' + itemId + '"]').text(formatCurrency(data.Amount));
            enableBidButtons();
        });
    }
};