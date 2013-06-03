$(document).ready(function () {
    $('#uploadImage').click(function () {
        $('#UploadDetails').show();
    });

    $('#UploadDetails_Upload').click(function () {
        Upload();
    });
});

function Upload() {
    if (!validateUser()) {
        return;
    }

    var reader = new FileReader();
    reader.onload = function (event) {
        data = {};
        data.userId = userInfo.Id;
        data.itemId = 1;
        data.description = $('input[id=UploadDetails_Description]').val();
        data.data = event.target.result;

        Ajax("wsItem.asmx/AddImage", JSON.stringify(data), function (data) {
            if (data.IsValid == "False") {
                alert(data.Message);
            }
            else {
                alert("success");
            }
        });
    };

    reader.onerror = function (event) {
        console.error("File could not be read! Code " + event.target.error.code);
    };

    file = $('input[type=file]').val();
    reader.readAsDataURL(file);
}