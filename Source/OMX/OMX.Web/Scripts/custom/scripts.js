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