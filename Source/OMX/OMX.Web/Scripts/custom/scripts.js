var notyBuilder = function (text, type) {
    return noty({
        text: text,
        type: type,
        animation: {
            open: { height: 'toggle' }, // jQuery animate function property object
            close: { height: 'toggle' }, // jQuery animate function property object
            easing: 'swing', // easing
            speed: 500 // opening & closing animation speed
        }
    });
}

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


function ShowSuccessMsg(data) {
    notyBuilder(data, 'success');
}

function ShowErrorMsg(request, error) {
    notyBuilder(request.statusText, error);
}