var ajaxHelper = (function() {
    function hideNotification(id) {
        $(id).hide(300);
    }

    function showReplySuccess(data) {
        $("#replyTextBox").val('');
        $("#replyTextBox").text('');
        return notificationHelper.showSuccessMessage(data.msg);
    }

    function showReplyFailed(err, stats) {
        $("#replyTextBox").text('');
        notificationHelper.showErrorMessage(err.msg);
    }

    function showPictureRemoveSuccess() {
        return notificationHelper.showSuccessMessage("Successfully removed picture");
    }

    function showPictureRemoveFailure() {
        return notificationHelper.showErrorMessage("Error during removing a picture.");
    }

    return {
        hideNotification: hideNotification,
        showReplySuccess: showReplySuccess,
        showReplyFailed: showReplyFailed,
        showPictureRemoveSuccess: showPictureRemoveSuccess,
        showPictureRemoveFailure: showPictureRemoveFailure
    }
})();