$(document).ready(function () {
    DisplayCreateItem();

    $('#CreateItem').click(function () {
        $('#CreateItemDetails').show();
    });
    
    $('#CreateItemDetails_CreateItem').click(function () {
        CreateItem();
    });
});

function DisplayCreateItem() {
    if (userInfo != null) {
        $('#CreateItem').show();
    }
    else {
        $('#CreateItem').hide();
    }
}

function CreateItem() {
    data = {};

    data.userId = userInfo.Id;
    data.title = $('input[id=CreateItemDetails_Title]').val();
    data.description = $('input[id=CreateItemDetails_Description]').val();
    data.startDate = $('input[id=CreateItemDetails_StartDate]').val();
    data.expireDate = $('input[id=CreateItemDetails_ExpireDate]').val();
    data.cashValue = $('input[id=CreateItemDetails_CashValue]').val();
    data.initialBid = $('input[id=CreateItemDetails_InitialBid]').val();

    Ajax("wsItem.asmx/Create", JSON.stringify(data), function (data) {
        if (data.IsValid == "False") {
            alert(data.Message);
        }
        else {
            
            alert("success");
        }
    });
}