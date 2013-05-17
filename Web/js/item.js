$(document).ready(function () {
    $(":button").each(function () {
        itemId = $(this).attr('itemid');
        if (itemId != null) {
            $(this).click({ param1: itemId }, CreateBid);
        }
    });
});

function CreateBid(event) {
    itemId = event.data.param1;
    data = {};

    if (!validateUser()) {
        return;
    }

    data.userId = userInfo.Id;
    data.itemId = itemId;
    data.amount = $('input[itemid="' + itemId + '"][type="text"]').val()

    validate = new Validation();
    if (validate.hasData(data.amount)) {
        if (!validate.hasData(data.userId)) {
            alert('User need to be logged in');
        }

        Ajax("wsItem.asmx/Bid", JSON.stringify(data), function (data) {
            if (data.IsValid == "False") {
                alert(data.Message);
            }
            else {
                alert("success");
            }

            $('span[itemid="' + itemId + '"]').text(Globalize.format(Number(data.Amount), 'C'));
        });
    }
};