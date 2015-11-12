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

$(document).ready(function () {
    $("#back-to-top").hide();
    $(window).scroll(function () {
        if ($(window).scrollTop() > 200) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    $('#back-to-top img').click(function () {
        $('body').animate({
            scrollTop: 0
        }, 1000);
    });
});

function ShowSuccessMsg(data) {
    notificationHelper.showSuccessMessage(data);
}

function ShowErrorMsg(request, error) {
    notificationHelper.showErrorMessage(request.statusText);
}