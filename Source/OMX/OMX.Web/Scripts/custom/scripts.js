function HideLoadingPopup() {
    $("#loadingPictures").hide();
}

function ShowLoadingPopup() {
    $("#loadingPictures").show();
}

$(document).ready(function () {
    $("#searchform").validate({
        rules: {
            AdSubstring: {
                required: true
            }
        }
    });
});

$(document).ready(function () {
    $("#replyForm").validate({
        rules: {
            Content: {
                required: true
            }
        }
    });
});

function ShowSuccessMsg(data) {
    notificationHelper.showSuccessMessage(data);
}

function ShowErrorMsg(request, error) {
    notificationHelper.showErrorMessage(request.statusText);
}