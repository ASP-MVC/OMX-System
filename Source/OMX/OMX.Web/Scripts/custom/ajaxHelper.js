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

    return {
        hideNotification: hideNotification,
        showReplySuccess: showReplySuccess,
        showReplyFailed: showReplyFailed
    }
})();