$(document).ready(function () {
    $('#uploadImage').click(function () {
        $('#UploadDetails').show();
    });

    $('#UploadDetails_Upload').click(function () {
        Upload();
    });
});

function Upload() {
    data = {};

    if (!validateUser()) {
        return;
    }

    data.userId = userInfo.Id;
    data.itemId = 1;
    data.description = $('input[id=UploadDetails_Description]').val();
    data.filename = 'c:/temp/m.png';

    Ajax("wsItem.asmx/AddImage", JSON.stringify(data), function (data) {
        if (data.IsValid == "False") {
            alert(data.Message);
        }
        else {
            
            alert("success");
        }
    });
}